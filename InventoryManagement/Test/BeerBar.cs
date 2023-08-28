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
    public class BeerBar
    {
        #region global variable
        private BeerBarLst? beerBarLst = new();
        private StatusDO? statusDO = new();
        #endregion

        #region constructor
        private readonly BeerBarController beerBar;
        private readonly IBeerBarBL _iBeerBarBl;
        private readonly IBeerBarDL _iBeerBarDl;
        private readonly ILogBL _iLogBL;
        private readonly ILogDL _iLogDL;

        public BeerBar()
        {
            _iBeerBarDl = new BeerBarDL();
            _iLogDL = new LogDL();

            _iLogBL = new LogBL(_iLogDL);
            _iBeerBarBl = new BeerBarBL(_iBeerBarDl);

            beerBar = new BeerBarController(_iBeerBarBl, _iLogBL);
        }
        #endregion

        #region get tests
        [Fact]
        public void GetBeerBarWhenRequestIdNotExist_InValidReqId()
        {
            // Request Id is null or blank, should return validation error

            beerBarLst = beerBar.GetBeerBar(requestId: null, barId: null, beerId: null);

            Assert.NotNull(beerBarLst);
            Assert.Null(beerBarLst.RequestId);
            Assert.Null(beerBarLst.BeerBars);
            Assert.NotNull(beerBarLst.StatusDO);

            statusDO = beerBarLst.StatusDO;

            Assert.NotNull(statusDO.Status);                     
            Assert.NotEmpty(statusDO.Status);
            Assert.True(statusDO.Status == Status.ReqIdError);
            Assert.True(statusDO.StatusCode == StatusCode.Fail);
        }

        [Fact]
        public void GetBeerBarWhenRequestIdExistAndNotId_ReturnResult()
        {
            //Request Id has value, Id is blank, should return all records
            beerBarLst = beerBar.GetBeerBar("IPF_IMApp", barId: null, beerId: null);

            Assert.NotNull(beerBarLst);
            Assert.NotNull(beerBarLst.RequestId);
            Assert.NotNull(beerBarLst.BeerBars);
            Assert.NotNull(beerBarLst.StatusDO);

            statusDO = beerBarLst.StatusDO;
            Assert.NotNull(statusDO.Status);
            Assert.NotEmpty(statusDO.Status);
            Assert.NotNull(statusDO.StatusCode);

            Assert.True(statusDO.Status == Status.Success);
            Assert.True(statusDO.StatusCode == StatusCode.Success);
        }

        [Fact]
        public void GetBeerBarWhenRequestIdExistAndNotId_ReturnResultWithRecord()
        {
            //Id not passed, should return all records
            beerBarLst = beerBar.GetBeerBar("IPF_IMApp", barId: null, beerId: null);

            int? count = beerBarLst.BeerBars?.Count;

            Assert.True(count >= 0);
        }

        [Fact]
        public void GetBeerBarWhenRequestIdExistAndId_ReturnResultWithRecord()
        {
            //Id not passed, should return all records
            beerBarLst = beerBar.GetBeerBar("IPF_IMApp", barId: 1, beerId: null);

            int? count = beerBarLst.BeerBars?.Count;

            Assert.True(count >= 0);
            Assert.True(beerBarLst?.BeerBars?[0].Beers?.Count >= 1);
        }

        [Fact]
        public void GetBeerBarWhenRequestIdExistAndId_ReturnResultNoRecord()
        {
            //Id not passed and no records in database, should return no records
            beerBarLst = beerBar.GetBeerBar("IPF_IMApp", barId: 999, beerId: null);

            int? count = beerBarLst.BeerBars?.Count;

            Assert.True(count == 0);

            statusDO = beerBarLst.StatusDO;
            Assert.NotNull(statusDO?.Message);
            Assert.True(statusDO.Message == UserMessages.NoRecordFound);
        }

        #endregion

        #region insert/update
        /*Request Id has value, should insert data
            * into db & return respective id */
        [Fact]
        public void InsertBeerBarWhenModelAlreadyExist()
        {
            statusDO = GetStatus(false);

            Assert.NotNull(statusDO);
            Assert.True(statusDO.StatusCode == StatusCode.Success);
            Assert.True(statusDO.Status == Status.Success);
            Assert.True(statusDO.Message == UserMessages.BeerBarAlreadyExist);
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

        #region private - get beerBar model
     
        private StatusDO? GetStatus(bool isValid)
        {
            BarDL barDL = new();
            BeerDL beerDL = new();

            int barId = isValid ? barDL.InsertUpdateBar(GetBarModel()) : 1;
            int beerId = isValid ? beerDL.InsertUpdateBeer(GetBeerModel()) : 1;

            BeerBarDtl beerBarLst = new()
            {
                RequestId = "IPF_IMApp",
                BeerBars= new(){
                new BeerBarDO{
                    Id = 0,
                    BarId= barId,
                    BeerId=beerId,
                    IsActive=true,
                    CreatedBy = 1
                }
            }
            };
            return beerBar.InsertBeerBar(beerBarLst);
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

        private static BarDO GetBarModel()
        {
            BarDO barDO = new()
            {
                Id = 0,
                CreatedBy = 1,
                Name = "IPF_IMApp_testing_" + DateTime.Now.Minute,
                Address = "IPF_IMApp_test_address",
                IsActive = true
            };

            return barDO;
        }

        #endregion
    }
}