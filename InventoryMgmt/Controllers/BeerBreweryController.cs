using InventoryMgmt.Common;
using InventoryMgmtBL.IModule;
using InventoryMgmtDE.DataEntity;
using Microsoft.AspNetCore.Mvc;

namespace InventoryMgmt.Controllers
{
    /* This Controller is used for handling all
     * get/post/put webapi requests for beer brewery*/

    [ApiController]
    [Route("[controller]")]
    public class BeerBreweryController : Controller
    {
        #region global variables
        public StatusDO? statusDO = new();
        public LogDO logDO = new();
        public Utility utility = new();
        #endregion

        #region constructor
        private readonly IBeerBreweryBL _iBeerBreweryBl;
        private readonly ILogBL _iLog;
        public BeerBreweryController(IBeerBreweryBL beerBreweryBL, ILogBL iLog)
        {
            _iBeerBreweryBl = beerBreweryBL;
            _iLog = iLog;
        }
        #endregion

        #region get beer brewery (GET)
        [HttpGet]
        [Route("/brewery/beer/{breweryId?}/{beerId?}")]
        public BeerBreweryLst GetBeerBrewery(string? requestId,  int? breweryId, int? beerId)
        {
            //Local Variable
            BeerBreweryLst beerBreweryLst = new();

            try
            {
                beerBreweryLst = _iBeerBreweryBl.GetBeerBrewery(requestId, breweryId, beerId);
            }
            catch (Exception ex)
            {
                beerBreweryLst.StatusDO = utility.GetStatus(Status.Error,UserMessages.ErrorMessage, 0);

                logDO = utility.GetErrorDetails(ex, requestId, ActionName.GetBeerBrewery);
                _iLog.Logger(logDO);
            }

            return beerBreweryLst;
        }
        #endregion

        #region insert beer brewery (POST)
        [HttpPost]
        public StatusDO? InsertBeerBrewery(BeerBreweryDtl beerBreweryLst)
        {
            return InsertBeerBrewery(beerBreweryLst, ActionType.Insert, ActionName.InsertBeerBrewery);
        }
        #endregion

        #region private methods
        private StatusDO? InsertBeerBrewery(BeerBreweryDtl beerBreweryLst,
            ActionType actionType,string actionName)
        {
            try
            {
                statusDO = !ModelState.IsValid ?
                    utility.GetStatus(Status.ModelError, UserMessages.ErrorModel, 0) :
                    _iBeerBreweryBl.InsertBeerBrewery(beerBreweryLst,actionType);
            }
            catch (Exception ex)
            {
                statusDO = utility.GetStatus(Status.Error, UserMessages.ErrorMessage, 0);

                logDO = utility.GetErrorDetails(ex, beerBreweryLst.RequestId, actionName);
                _iLog.Logger(logDO);
            }
            return statusDO;
        }
        #endregion
    }
}
