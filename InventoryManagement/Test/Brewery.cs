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
    public class Brewery
    {
        #region global variable
        private BreweryLst? BreweryLst = new();
        private StatusDO? statusDO = new();
        #endregion

        #region constructor
        private readonly BreweryController brewery;
        private readonly IBreweryBL _iBreweryBl;
        private readonly IBreweryDL _iBreweryDl;
        private readonly ILogBL _iLogBL;
        private readonly ILogDL _iLogDL;

        public Brewery()
        {
            _iBreweryDl = new BreweryDL();
            _iLogDL = new LogDL();

            _iLogBL = new LogBL(_iLogDL);
            _iBreweryBl = new BreweryBL(_iBreweryDl);

            brewery = new BreweryController(_iBreweryBl, _iLogBL);
        }
        #endregion

        #region get tests
        [Fact]
        public void GetBreweryWhenRequestIdNotExist_InValidReqId()
        {
            // Request Id is null or blank, should return validation error

            BreweryLst = brewery.GetBrewery(requestId: null, id: null);

            Assert.NotNull(BreweryLst);
            Assert.Null(BreweryLst.RequestId);
            Assert.Null(BreweryLst.Breweries);
            Assert.NotNull(BreweryLst.StatusDO);

            statusDO = BreweryLst.StatusDO;

            Assert.NotNull(statusDO.Status);
            Assert.NotEmpty(statusDO.Status);
            Assert.True(statusDO.Status == Status.ReqIdError);
            Assert.True(statusDO.StatusCode == StatusCode.Fail);
        }

        [Fact]
        public void GetBreweryWhenRequestIdExistAndNotId_ReturnResult()
        {
            //Request Id has value, Id is blank, should return all records
            BreweryLst = brewery.GetBrewery("IPF_IMApp", id: null);

            Assert.NotNull(BreweryLst);
            Assert.NotNull(BreweryLst.RequestId);
            Assert.NotNull(BreweryLst.Breweries);
            Assert.NotNull(BreweryLst.StatusDO);

            statusDO = BreweryLst.StatusDO;
            Assert.NotNull(statusDO.Status);
            Assert.NotEmpty(statusDO.Status);
            Assert.NotNull(statusDO.StatusCode);

            Assert.True(statusDO.Status == Status.Success);
            Assert.True(statusDO.StatusCode == StatusCode.Success);
        }

        [Fact]
        public void GetBreweryWhenRequestIdExistAndNotId_ReturnResultWithRecord()
        {
            //Id not passed, should return all records
            BreweryLst = brewery.GetBrewery("IPF_IMApp", id: null);

            int? count = BreweryLst.Breweries?.Count;

            Assert.True(count >= 0);
        }

        [Fact]
        public void GetBreweryWhenRequestIdExistAndId_ReturnResultWithRecord()
        {
            //Id not passed, should return all records
            BreweryLst = brewery.GetBrewery("IPF_IMApp", id: 1);

            int? count = BreweryLst.Breweries?.Count;

            Assert.True(count == 1);
        }

        [Fact]
        public void GetBreweryWhenRequestIdExistAndId_ReturnResultNoRecord()
        {
            //Id not passed and no records in database, should return no records
            BreweryLst = brewery.GetBrewery("IPF_IMApp", id: 999);

            int? count = BreweryLst.Breweries?.Count;

            Assert.True(count == 0);

            statusDO = BreweryLst.StatusDO;
            Assert.NotNull(statusDO?.Message);
            Assert.True(statusDO.Message == UserMessages.NoRecordFound);
        }

        #endregion

        #region insert/update
        /*Request Id has value, should insert data
            * into db & return respective id */
        [Fact]
        public void InsertBreweryWhenModelisValid()
        {
            BreweryLst = GetBreweryModel(ActionType.Insert);

            statusDO = brewery.InsertBrewery(BreweryLst);

            Assert.NotNull(statusDO);
            Assert.True(statusDO.StatusCode == StatusCode.Success);
            Assert.True(statusDO.Status == Status.Success);
            Assert.True(statusDO.Message == UserMessages.InsertSuccess);
            Assert.True(statusDO.Id > 0);
        }

        /*Request Id has value, should update data
           * into db against the id passed in request */
        [Fact]
        public void UpdateBreweryWhenModelisValid()
        {
            BreweryLst = GetBreweryModel(ActionType.Update);

            statusDO = brewery.UpdateBrewery(BreweryLst);

            Assert.NotNull(statusDO);
            Assert.True(statusDO.StatusCode == StatusCode.Success);
            Assert.True(statusDO.Status == Status.Success);
            Assert.True(statusDO.Message == UserMessages.UpdateSuccess);
            Assert.True(statusDO.Id > 0);
        }
        #endregion

        #region private - get brewery model
        private static BreweryLst GetBreweryModel(ActionType actionType)
        {
            BreweryLst BreweryLst = new BreweryLst
            {
                RequestId = "IPF_IMApp",
                Breweries = new List<BreweryDO>{
                new BreweryDO{
                    Name="IPF_IMApp_Bre_testing_" + DateTime.Now.Minute,
                    IsActive=true
                }
            }
            };

            if (actionType == ActionType.Insert)
            {
                BreweryLst.Breweries[0].Id = 0;
                BreweryLst.Breweries[0].CreatedBy = 1;
            }
            else
            {
                BreweryLst.Breweries[0].Id = 1;
                BreweryLst.Breweries[0].UpdatedBy = 1;
            }

            return BreweryLst;
        }
        #endregion
    }
}