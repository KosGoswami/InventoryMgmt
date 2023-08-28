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
    public class Bar
    {
        #region global variable
        private BarLst? barLst = new();
        private StatusDO? statusDO = new();
        #endregion

        #region constructor
        private readonly BarController bar;
        private readonly IBarBL _iBarBl;
        private readonly IBarDL _iBarDl;
        private readonly ILogBL _iLogBL;
        private readonly ILogDL _iLogDL;

        public Bar()
        {
            _iBarDl = new BarDL();
            _iLogDL = new LogDL();

            _iLogBL = new LogBL(_iLogDL);
            _iBarBl = new BarBL(_iBarDl);

            bar = new BarController(_iBarBl, _iLogBL);
        }
        #endregion

        #region get tests
        [Fact]
        public void GetBarWhenRequestIdNotExist_InValidReqId()
        {
            // Request Id is null or blank, should return validation error

            barLst = bar.GetBar(requestId: null, id: null);

            Assert.NotNull(barLst);
            Assert.Null(barLst.RequestId);
            Assert.Null(barLst.Bars);
            Assert.NotNull(barLst.StatusDO);

            statusDO = barLst.StatusDO;

            Assert.NotNull(statusDO.Status);
            Assert.NotEmpty(statusDO.Status);
            Assert.True(statusDO.Status == Status.ReqIdError);
            Assert.True(statusDO.StatusCode == StatusCode.Fail);
        }

        [Fact]
        public void GetBarWhenRequestIdExistAndNotId_ReturnResult()
        {
            //Request Id has value, Id is blank, should return all records
            barLst = bar.GetBar("IPF_IMApp", id: null);

            Assert.NotNull(barLst);
            Assert.NotNull(barLst.RequestId);
            Assert.NotNull(barLst.Bars);
            Assert.NotNull(barLst.StatusDO);

            statusDO = barLst.StatusDO;
            Assert.NotNull(statusDO.Status);
            Assert.NotEmpty(statusDO.Status);
            Assert.NotNull(statusDO.StatusCode);

            Assert.True(statusDO.Status == Status.Success);
            Assert.True(statusDO.StatusCode == StatusCode.Success);
        }

        [Fact]
        public void GetBarWhenRequestIdExistAndNotId_ReturnResultWithRecord()
        {
            //Id not passed, should return all records
            barLst = bar.GetBar("IPF_IMApp", id: null);

            int? count = barLst.Bars?.Count;

            Assert.True(count >= 0);
        }

        [Fact]
        public void GetBarWhenRequestIdExistAndId_ReturnResultWithRecord()
        {
            //Id not passed, should return all records
            barLst = bar.GetBar("IPF_IMApp", id: 1);

            int? count = barLst.Bars?.Count;

            Assert.True(count == 1);
        }

        [Fact]
        public void GetBarWhenRequestIdExistAndId_ReturnResultNoRecord()
        {
            //Id not passed and no records in database, should return no records
            barLst = bar.GetBar("IPF_IMApp", id: 999);

            int? count = barLst.Bars?.Count;

            Assert.True(count == 0);

            statusDO = barLst.StatusDO;
            Assert.NotNull(statusDO?.Message);
            Assert.True(statusDO.Message == UserMessages.NoRecordFound);
        }

        #endregion

        #region insert/update
        /*Request Id has value, should insert data
            * into db & return respective id */
        [Fact]
        public void InsertBarWhenModelisValid()
        {
            barLst = GetBarModel(ActionType.Insert);

            statusDO = bar.InsertBar(barLst);

            Assert.NotNull(statusDO);
            Assert.True(statusDO.StatusCode == StatusCode.Success);
            Assert.True(statusDO.Status == Status.Success);
            Assert.True(statusDO.Message == UserMessages.InsertSuccess);
            Assert.True(statusDO.Id > 0);
        }

        /*Request Id has value, should update data
           * into db against the id passed in request */
        [Fact]
        public void UpdateBarWhenModelisValid()
        {
            barLst = GetBarModel(ActionType.Update);

            statusDO = bar.UpdateBar(barLst);

            Assert.NotNull(statusDO);
            Assert.True(statusDO.StatusCode == StatusCode.Success);
            Assert.True(statusDO.Status == Status.Success);
            Assert.True(statusDO.Message == UserMessages.UpdateSuccess);
            Assert.True(statusDO.Id > 0);
        }
        #endregion

        #region private - get bar model
        private static BarLst GetBarModel(ActionType actionType)
        {
            BarLst barLst = new BarLst
            {
                RequestId = "IPF_IMApp",
                Bars = new List<BarDO>{
                new BarDO{
                    Name="IPF_IMApp_testing_" + DateTime.Now.Minute,
                    Address = "IPF_IMApp_test_address",
                    IsActive=true
                }
            }
            };

            if (actionType == ActionType.Insert)
            {
                barLst.Bars[0].Id = 0;
                barLst.Bars[0].CreatedBy = 1;
            }
            else
            {
                barLst.Bars[0].Id = 1;
                barLst.Bars[0].UpdatedBy = 1;
            }

            return barLst;
        }
        #endregion
    }
}