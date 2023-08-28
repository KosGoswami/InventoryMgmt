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
    public class BeerBrewery
    {
        #region global variable
        private BeerBreweryLst? beerBreweryLst = new();
        private StatusDO? statusDO = new();
        #endregion

        #region constructor
        private readonly BeerBreweryController beerBrewery;
        private readonly IBeerBreweryBL _iBeerBreweryBl;
        private readonly IBeerBreweryDL _iBeerBreweryDl;
        private readonly ILogBL _iLogBL;
        private readonly ILogDL _iLogDL;

        public BeerBrewery()
        {
            _iBeerBreweryDl = new BeerBreweryDL();
            _iLogDL = new LogDL();

            _iLogBL = new LogBL(_iLogDL);
            _iBeerBreweryBl = new BeerBreweryBL(_iBeerBreweryDl);

            beerBrewery = new BeerBreweryController(_iBeerBreweryBl, _iLogBL);
        }
        #endregion

        #region get tests
        [Fact]
        public void GetBeerBreweryWhenRequestIdNotExist_InValidReqId()
        {
            beerBreweryLst = beerBrewery.GetBeerBrewery(requestId: null, breweryId: null, beerId: null);

            Assert.NotNull(beerBreweryLst);
            Assert.Null(beerBreweryLst.RequestId);
            Assert.Null(beerBreweryLst.BeerBreweries);
            Assert.NotNull(beerBreweryLst.StatusDO);

            statusDO = beerBreweryLst.StatusDO;

            Assert.NotNull(statusDO.Status);
            Assert.NotEmpty(statusDO.Status);
            Assert.True(statusDO.Status == Status.ReqIdError);
            Assert.True(statusDO.StatusCode == StatusCode.Fail);
        }

        [Fact]
        public void GetBeerBreweryWhenRequestIdExistAndNotId_ReturnResult()
        {
            beerBreweryLst = beerBrewery.GetBeerBrewery("IPF_IMApp", breweryId: null, beerId: null);

            Assert.NotNull(beerBreweryLst);
            Assert.NotNull(beerBreweryLst.RequestId);
            Assert.NotNull(beerBreweryLst.BeerBreweries);
            Assert.NotNull(beerBreweryLst.StatusDO);

            statusDO = beerBreweryLst.StatusDO;
            Assert.NotNull(statusDO.Status);
            Assert.NotEmpty(statusDO.Status);
            Assert.NotNull(statusDO.StatusCode);

            Assert.True(statusDO.Status == Status.Success);
            Assert.True(statusDO.StatusCode == StatusCode.Success);
        }

        [Fact]
        public void GetBeerBreweryWhenRequestIdExistAndNotId_ReturnResultWithRecord()
        {
            //Id not passed, should return all records
            beerBreweryLst = beerBrewery.GetBeerBrewery("IPF_IMApp", breweryId: null, beerId: null);

            int? count = beerBreweryLst.BeerBreweries?.Count;

            Assert.True(count >= 0);
        }

        [Fact]
        public void GetBeerBreweryWhenRequestIdExistAndId_ReturnResultWithRecord()
        {
            //Id not passed, should return all records
            beerBreweryLst = beerBrewery.GetBeerBrewery("IPF_IMApp", breweryId: 1, beerId: null);

            int? count = beerBreweryLst.BeerBreweries?.Count;

            Assert.True(count >= 1);
            Assert.True(beerBreweryLst?.BeerBreweries?[0].Beers?.Count>=1);
        }

        [Fact]
        public void GetBeerBreweryWhenRequestIdExistAndId_ReturnResultNoRecord()
        {
            //Id not passed and no records in database, should return no records
            beerBreweryLst = beerBrewery.GetBeerBrewery("IPF_IMApp", breweryId: 999, beerId: null);

            int? count = beerBreweryLst.BeerBreweries?.Count;

            Assert.True(count == 0);

            statusDO = beerBreweryLst.StatusDO;
            Assert.NotNull(statusDO?.Message);
            Assert.True(statusDO.Message == UserMessages.NoRecordFound);
        }

        #endregion

        #region insert/update
        /*Request Id has value, should insert data
            * into db & return respective id */
        [Fact]
        public void InsertBeerBreweryWhenModelisInvalid()
        {
            statusDO = GetStatus(false);

            Assert.NotNull(statusDO);            
            Assert.True(statusDO.StatusCode == StatusCode.Success);
            Assert.True(statusDO.Status == Status.Success);
            Assert.True(statusDO.Message == UserMessages.BeerBreweryAlreadyExist);
        }

        [Fact]
        public void InsertBeerBarWhenModelisValid()
        {
            statusDO = GetStatus(true);

            Assert.NotNull(statusDO);
            Assert.True(statusDO.StatusCode == StatusCode.Success);
            Assert.True(statusDO.Status == Status.Success);
            Assert.True(statusDO.Message == UserMessages.InsertSuccess);
        }

        /*Request Id has value, should update data
           * into db against the id passed in request */

        #endregion

        #region private - get beerBrewery model
        private StatusDO? GetStatus(bool isValid)
        {
            BreweryDL breweryDL = new();
            BeerDL beerDL = new();

            int breweryId = isValid ? breweryDL.InsertUpdateBrewery(GetBreweryModel()) : 1;
            int beerId = isValid ? beerDL.InsertUpdateBeer(GetBeerModel()) : 1;

            BeerBreweryDtl beerBreweryLst = new()
            {
                RequestId = "IPF_IMApp",
                BeerBreweries = new List<BeerBreweryDO>{
                new BeerBreweryDO{
                    Id = 0,
                    BreweryId= breweryId,
                    BeerId=beerId,
                    IsActive=true,
                    CreatedBy = 1
                }
            }
            };
            return beerBrewery.InsertBeerBrewery(beerBreweryLst);
        }

        private static BeerDO GetBeerModel()
        {
            BeerDO beerDO = new()
            {
                Id = 0,
                CreatedBy = 1,
                Name = "IPF_IMApp_testing_" + DateTime.Now.Minute,
                PercentageAlcoholByVolume = 5.5,
                IsActive = true
            };
            return beerDO;
        }

        private static BreweryDO GetBreweryModel()
        {
            BreweryDO breweryDO = new()
            {
                Id = 0,
                CreatedBy = 1,
                Name = "IPF_IMApp_testing_" + DateTime.Now.Minute,
                IsActive = true
            };

            return breweryDO;
        }
        #endregion
    }
}