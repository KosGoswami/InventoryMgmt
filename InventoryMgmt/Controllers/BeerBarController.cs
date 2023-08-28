using InventoryMgmt.Common;
using InventoryMgmtBL.IModule;
using InventoryMgmtDE.DataEntity;
using Microsoft.AspNetCore.Mvc;

namespace InventoryMgmt.Controllers
{
    /* This Controller is used for handling all
     * get/post/put webapi requests for beer bar */

    [ApiController]
    [Route("[controller]")]
    public class BeerBarController : Controller
    {
        #region global variables
        public StatusDO? statusDO = new();
        public LogDO logDO = new();
        public Utility utility = new();
        #endregion

        #region constructor
        private readonly IBeerBarBL _iBeerBarBl;
        private readonly ILogBL _iLog;
        public BeerBarController(IBeerBarBL beerBarBL, ILogBL iLog)
        {
            _iBeerBarBl = beerBarBL;
            _iLog = iLog;
        }
        #endregion

        #region get beer bar (GET)
        [HttpGet]
        [Route("/bar/beer/{barId?}/{beerId?}")]
        public BeerBarLst GetBeerBar(string? requestId, int? barId, int? beerId)
        {
            //Local Variable
            BeerBarLst beerBarLst = new();

            try
            {
                beerBarLst = _iBeerBarBl.GetBeerBar(requestId, barId, beerId);
            }
            catch (Exception ex)
            {
                beerBarLst.StatusDO = utility.GetStatus(Status.Error,UserMessages.ErrorMessage, 0);

                logDO = utility.GetErrorDetails(ex, requestId, ActionName.GetBeerBar);
                _iLog.Logger(logDO);
            }

            return beerBarLst;
        }
        #endregion

        #region insert beer bar (POST)
        [HttpPost]
        public StatusDO? InsertBeerBar(BeerBarDtl beerBarDtl)
        {
            return InsertBeerBar(beerBarDtl, ActionType.Insert, ActionName.InsertBeerBar);
        }
        #endregion

        #region private methods
        private StatusDO? InsertBeerBar(BeerBarDtl beerBarDtl,
            ActionType actionType, string actionName)
        {
            try
            {
                statusDO = !ModelState.IsValid ?
                    utility.GetStatus(Status.ModelError, UserMessages.ErrorModel, 0) :
                    _iBeerBarBl.InsertBeerBar(beerBarDtl, actionType);
            }
            catch (Exception ex)
            {
                statusDO = utility.GetStatus(Status.Error, UserMessages.ErrorMessage, 0);

                logDO = utility.GetErrorDetails(ex, beerBarDtl.RequestId, actionName);
                _iLog.Logger(logDO);
            }
            return statusDO;
        }
        #endregion
    }
}
