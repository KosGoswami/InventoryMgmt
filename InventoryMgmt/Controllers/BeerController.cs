using InventoryMgmt.Common;
using InventoryMgmtBL.IModule;
using InventoryMgmtDE.DataEntity;
using Microsoft.AspNetCore.Mvc;

namespace InventoryMgmt.Controllers
{
    /* This Controller is used for handling all
     * get/post/put webapi requests for beer */


    [ApiController]
    [Route("[controller]")]
    public class BeerController : Controller
    {
        #region global variables
        public StatusDO? statusDO = new();
        public LogDO logDO = new();
        public Utility utility = new();
        #endregion

        #region constructor
        private readonly IBeerBL _iBeerBl;
        private readonly ILogBL _iLog;

        public BeerController(IBeerBL beerBL, ILogBL iLog)
        {
            _iBeerBl = beerBL;
            _iLog = iLog;
        }
        #endregion

        #region get beer (GET)
        [HttpGet]
        [Route("/beer/{id?}")]
        public BeerLst GetBeer(string? requestId, int? id, double? ltAlcoholByVolume, double? gtAlcoholByVolume)
        {
            //Local Variable
            BeerLst beerLst = new();

            try
            {
                beerLst = _iBeerBl.GetBeer(requestId, id, ltAlcoholByVolume,gtAlcoholByVolume);
            }
            catch (Exception ex)
            {
                beerLst.StatusDO = utility.GetStatus(Status.Error, UserMessages.ErrorMessage, id);

                logDO = utility.GetErrorDetails(ex, requestId, ActionName.GetBeer);
                _iLog.Logger(logDO);
            }
            return beerLst;
        }
        #endregion

        #region insert beer (POST)
        [HttpPost]
        public StatusDO? InsertBeer(BeerLst beerLst)
        {
            return InsertUpdateBeer(beerLst, ActionType.Insert);
        }
        #endregion

        #region update beer (PUT)
        [HttpPut]
        public StatusDO? UpdateBeer(BeerLst beerLst)
        {
            return InsertUpdateBeer(beerLst, ActionType.Update);
        }
        #endregion

        #region private methods
        private StatusDO? InsertUpdateBeer(BeerLst beerLst, ActionType actionType)
        {
            try
            {
                statusDO = !ModelState.IsValid ?
                    utility.GetStatus(Status.ModelError, UserMessages.ErrorModel, 0) :
                    _iBeerBl.InsertUpdateBeer(beerLst, actionType);
            }
            catch (Exception ex)
            {
                statusDO = utility.GetStatus(Status.Error, UserMessages.ErrorMessage, 0);

                logDO = utility.GetErrorDetails(ex, beerLst.RequestId,
                    actionType== ActionType.Insert?ActionName.InsertBeer:ActionName.UpdateBeer);
                _iLog.Logger(logDO);
            }
            return statusDO;
        }
        #endregion
    }
}
