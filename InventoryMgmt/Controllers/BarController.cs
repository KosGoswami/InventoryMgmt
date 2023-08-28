using InventoryMgmt.Common;
using InventoryMgmtBL.IModule;
using InventoryMgmtDE.DataEntity;
using Microsoft.AspNetCore.Mvc;

namespace InventoryMgmt.Controllers
{
    /* This Controller is used for handling all
     * get/post/put webapi requests for bar */

    [ApiController]
    [Route("[controller]")]
    public class BarController : Controller
    {
        #region global variables
        public StatusDO? statusDO = new();
        public LogDO logDO = new();
        public Utility utility = new();
        #endregion

        #region constructor
        private readonly IBarBL _iBarBl;
        private readonly ILogBL _iLog;

        public BarController(IBarBL barBL, ILogBL iLog)
        {
            _iBarBl = barBL;
            _iLog = iLog;
        }
        #endregion

        #region get bar (GET)
        [HttpGet]
        [Route("/bar/{id?}")]
        public BarLst GetBar(string? requestId, int? id)
        {
            //Local Variable
            BarLst barLst = new();

            try
            {
                barLst = _iBarBl.GetBar(requestId, id);
            }
            catch (Exception ex)
            {
                barLst.StatusDO = utility.GetStatus(Status.Error,UserMessages.ErrorMessage, id);

                logDO = utility.GetErrorDetails(ex, requestId, ActionName.GetBar);
                _iLog.Logger(logDO);
            }

            return barLst;
        }
        #endregion

        #region insert bar (POST)
        [HttpPost]
        public StatusDO? InsertBar(BarLst barLst)
        {
            return InsertUpdateBar(barLst, ActionType.Insert);
        }
        #endregion

        #region update bar (PUT)
        [HttpPut]
        public StatusDO? UpdateBar(BarLst barLst)
        {
            return InsertUpdateBar(barLst, ActionType.Update);
        }
        #endregion

        #region private methods
        private StatusDO? InsertUpdateBar(BarLst barLst, ActionType actionType)
        {
            try
            {
                statusDO = !ModelState.IsValid ?
                    utility.GetStatus(Status.ModelError, UserMessages.ErrorModel, 0) :
                    _iBarBl.InsertUpdateBar(barLst, actionType);
            }
            catch (Exception ex)
            {
                statusDO = utility.GetStatus(Status.Error, UserMessages.ErrorMessage, 0);

                logDO = utility.GetErrorDetails(ex, barLst.RequestId,
                    actionType == ActionType.Insert ? ActionName.InsertBar : ActionName.UpdateBar);
                _iLog.Logger(logDO);
            }
            return statusDO;
        }
        #endregion
    }
}
