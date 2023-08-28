using InventoryMgmt.Common;
using InventoryMgmtBL.IModule;
using InventoryMgmtDE.DataEntity;
using Microsoft.AspNetCore.Mvc;

namespace InventoryMgmt.Controllers
{
    /* This Controller is used for handling all
     * get/post/put webapi requests for brewery */

    [ApiController]
    [Route("[controller]")]
    public class BreweryController : Controller
    {
        #region global variables
        public StatusDO? statusDO = new();
        public LogDO logDO = new();
        public Utility utility = new();
        #endregion

        #region constructor
        private readonly IBreweryBL _iBreweryBl;
        private readonly ILogBL _iLog;
        public BreweryController(IBreweryBL breweryBL, ILogBL iLog)
        {
            _iBreweryBl = breweryBL;
            _iLog = iLog;
        }
        #endregion

        #region get brewery (GET)
        [HttpGet]
        [Route("/brewery/{id?}")]
        public BreweryLst GetBrewery(string? requestId, int? id)
        {
            //Local Variable
            BreweryLst breweryLst = new();

            try
            {
                breweryLst = _iBreweryBl.GetBrewery(requestId, id);
            }
            catch (Exception ex)
            {
                breweryLst.StatusDO = utility.GetStatus(Status.Error, UserMessages.ErrorMessage, id);

                logDO = utility.GetErrorDetails(ex, requestId, ActionName.GetBrewery);
                _iLog.Logger(logDO);
            }

            return breweryLst;
        }
        #endregion

        #region insert brewery (POST)
        [HttpPost]
        public StatusDO? InsertBrewery(BreweryLst breweryLst)
        {
            return InsertUpdateBrewery(breweryLst, ActionType.Insert);
        }
        #endregion

        #region update brewery (PUT)
        [HttpPut]
        public StatusDO? UpdateBrewery(BreweryLst breweryLst)
        {
            return InsertUpdateBrewery(breweryLst, ActionType.Update);
        }
        #endregion

        #region private methods
        private StatusDO? InsertUpdateBrewery(BreweryLst breweryLst, ActionType actionType)
        {
            try
            {
                statusDO = !ModelState.IsValid ?
                    utility.GetStatus(Status.ModelError, UserMessages.ErrorModel, 0) :
                    _iBreweryBl.InsertUpdateBrewery(breweryLst, actionType);
            }
            catch (Exception ex)
            {
                statusDO = utility.GetStatus(Status.Error, UserMessages.ErrorMessage, 0);

                logDO = utility.GetErrorDetails(ex, breweryLst.RequestId,
                    actionType == ActionType.Insert ? ActionName.InsertBrewery : ActionName.UpdateBrewery);
                _iLog.Logger(logDO);
            }
            return statusDO;
        }
        #endregion
    }
}
