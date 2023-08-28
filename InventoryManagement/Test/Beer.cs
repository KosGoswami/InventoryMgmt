using InventoryMgmt.Common;
using InventoryMgmt.Controllers;
using InventoryMgmtBL.IModule;
using InventoryMgmtBL.Module;
using InventoryMgmtDE.DataEntity;
using InventoryMgmtDL.DataLayer;
using InventoryMgmtDL.IDataLayer;
using Xunit;

namespace InventoryManagement.TEST
{
    public class Beer
    {
        #region global variable
        private BeerLst? beerLst = new();
        private StatusDO? statusDO = new();
        #endregion

        #region constructor
        private readonly BeerController beer;
        private readonly IBeerBL _iBeerBl;
        private readonly IBeerDL _iBeerDl;
        private readonly ILogBL _iLogBL;
        private readonly ILogDL _iLogDL;

        public Beer()
        {
            _iBeerDl = new BeerDL();
            _iLogDL = new LogDL();

            _iLogBL = new LogBL(_iLogDL);
            _iBeerBl = new BeerBL(_iBeerDl);

            beer = new BeerController(_iBeerBl, _iLogBL);
        }
        #endregion

        #region get tests
        [Fact]
        public void GetBeerWhenRequestIdNotExist_InValidReqId()
        {
            // Request Id is null or blank, should return validation error

            beerLst = beer.GetBeer(requestId: null, id: null,null,null);

            Assert.NotNull(beerLst);
            Assert.Null(beerLst.RequestId);
            Assert.Null(beerLst.Beers);
            Assert.NotNull(beerLst.StatusDO);

            statusDO = beerLst.StatusDO;

            Assert.NotNull(statusDO.Status);
            Assert.NotEmpty(statusDO.Status);
            Assert.True(statusDO.Status == Status.ReqIdError);
            Assert.True(statusDO.StatusCode == StatusCode.Fail);
        }

        [Fact]
        public void GetBeerWhenRequestIdExistAndNotId_ReturnResult()
        {
            //Request Id has value, Id is blank, should return all records
            beerLst = beer.GetBeer("IPF_IMApp", id: null,null,null);

            Assert.NotNull(beerLst);
            Assert.NotNull(beerLst.RequestId);
            Assert.NotNull(beerLst.Beers);
            Assert.NotNull(beerLst.StatusDO);

            statusDO = beerLst.StatusDO;
            Assert.NotNull(statusDO.Status);
            Assert.NotEmpty(statusDO.Status);
            Assert.NotNull(statusDO.StatusCode);

            Assert.True(statusDO.Status == Status.Success);
            Assert.True(statusDO.StatusCode == StatusCode.Success);
        }

        [Fact]
        public void GetBeerWhenRequestIdExistAndNotId_ReturnResultWithRecord()
        {
            //Id not passed, should return all records
            beerLst = beer.GetBeer("IPF_IMApp", id: null,null, null);
         
            int? count = beerLst.Beers?.Count;

            Assert.True(count >= 0);
        }

        [Fact]
        public void GetBeerWhenRequestIdExistAndId_ReturnResultWithRecord()
        {
            //Id not passed, should return all records
            beerLst = beer.GetBeer("IPF_IMApp", id: 1,null,null);

            int? count = beerLst.Beers?.Count;

            Assert.True(count == 1);
        }

        [Fact]
        public void GetBeerWhenRequestIdExistAndId_ReturnResultNoRecord()
        {
            //Id not passed and no records in database, should return no records
            beerLst = beer.GetBeer("IPF_IMApp", id: 999, null, null);

            int? count = beerLst.Beers?.Count;

            Assert.True(count == 0);

            statusDO = beerLst.StatusDO;
            Assert.NotNull(statusDO?.Message);
            Assert.True(statusDO.Message == UserMessages.NoRecordFound);
        }

        [Fact]
        public void GetBeerWhenAlcoholByPerValid_ReturnResultWithRecord()
        {
            //Id not passed, should return all records
            beerLst = beer.GetBeer("IPF_IMApp", id: null,1.0,90.0);

            statusDO = beerLst.StatusDO;
            Assert.NotNull(statusDO?.Status);
            Assert.NotEmpty(statusDO.Status);
            Assert.NotNull(statusDO.StatusCode);

            Assert.True(statusDO.Status == Status.Success);
            Assert.True(statusDO.StatusCode == StatusCode.Success);
        }

        [Fact]
        public void GetBeerWhenAlcoholByPerInvalid_ReturnResultWithRecord()
        {
            beerLst = beer.GetBeer("IPF_IMApp", id: null, 8.0, 1.0);

            statusDO = beerLst.StatusDO;
            Assert.NotNull(statusDO?.Status);
            Assert.NotEmpty(statusDO.Status);
            Assert.NotNull(statusDO.StatusCode);

            Assert.True(statusDO.Status == Status.RangeNotValid);
            Assert.True(statusDO.StatusCode == StatusCode.Fail);
            Assert.True(statusDO.Message == UserMessages.RangeNotValid);

            Assert.True(beerLst.Beers==null);
        }

        #endregion

        #region insert/update
        /*Request Id has value, should insert data
            * into db & return respective id */
        [Fact]
        public void InsertBeerWhenModelisValid()
        {
            beerLst = GetBeerModel(ActionType.Insert);
     
            statusDO = beer.InsertBeer(beerLst);

            Assert.NotNull(statusDO);
            Assert.True(statusDO.StatusCode == StatusCode.Success);
            Assert.True(statusDO.Status == Status.Success);
            Assert.True(statusDO.Message == UserMessages.InsertSuccess);
            Assert.True(statusDO.Id > 0);
        }

        /*Request Id has value, should update data
           * into db against the id passed in request */
        [Fact]
        public void UpdateBeerWhenModelisValid()
        {
            beerLst = GetBeerModel(ActionType.Update);

            statusDO = beer.UpdateBeer(beerLst);

            Assert.NotNull(statusDO);
            Assert.True(statusDO.StatusCode == StatusCode.Success);
            Assert.True(statusDO.Status == Status.Success);
            Assert.True(statusDO.Message == UserMessages.UpdateSuccess);
            Assert.True(statusDO.Id > 0);
        }
        #endregion

        #region private - get beer model
        private static BeerLst GetBeerModel(ActionType actionType)
        {
            BeerLst beerLst = new BeerLst
            {
                RequestId = "IPF_IMApp",
                Beers = new List<BeerDO>{
                new BeerDO{
                    Name="IPF_IMApp_testing_" + DateTime.Now.Minute,
                    PercentageAlcoholByVolume=5.5,
                    IsActive=true
                }
            }
            };

            if (actionType == ActionType.Insert)
            { beerLst.Beers[0].Id = 0;
                beerLst.Beers[0].CreatedBy = 1;
            }
            else {
                beerLst.Beers[0].Id = 1;
                beerLst.Beers[0].UpdatedBy= 1;
            }

            return beerLst;
        }
        #endregion
    }
}