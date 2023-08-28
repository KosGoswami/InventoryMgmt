using InventoryMgmt.Common;
using InventoryMgmtBL.IModule;
using InventoryMgmtDE.DataEntity;
using InventoryMgmtDL.IDataLayer;

namespace InventoryMgmtBL.Module
{
    public class BeerBL : IBeerBL
    {
        #region global variables

        public StatusDO statusDO = new();
        public Utility utility = new();

        #endregion

        #region constructor
        private readonly IBeerDL _ibeerDl;

        public BeerBL(IBeerDL beerDL)
        {
            _ibeerDl = beerDL;
        }
        #endregion

        #region get beer
        public BeerLst GetBeer(string? requestId, int? id, double? ltAlcoholByVolume, double? gtAlcoholByVolume)
        {
            //Local Variable
            BeerLst beerLst = new();

            if (requestId == null)
            {
                statusDO = utility.GetStatus(Status.ReqIdError, UserMessages.ErrorReqId, id);
            }
            else if (ltAlcoholByVolume>gtAlcoholByVolume)
            {
                statusDO = utility.GetStatus(Status.RangeNotValid, UserMessages.RangeNotValid, id);
            }
            else
            {
                beerLst.Beers = _ibeerDl.GetBeer(id, ltAlcoholByVolume, gtAlcoholByVolume);

                string userMessage = (beerLst.Beers == null || beerLst.Beers.Count == 0)
                    ? UserMessages.NoRecordFound : UserMessages.SuccessMessage;

                statusDO = utility.GetStatus(Status.Success, userMessage, id);
            }
           
            beerLst.RequestId = requestId;
            beerLst.StatusDO = statusDO;
            return beerLst;
        }

        #endregion

        #region insert & update beer
        public StatusDO? InsertUpdateBeer(BeerLst beerLst, ActionType actionType)
        {
            statusDO = utility.IsValidRequest(beerLst, actionType, ModelName.Beers);

            if (statusDO != null && statusDO.Status == Status.Success 
                && beerLst!=null && beerLst.Beers!=null)
            {
                int result = _ibeerDl.InsertUpdateBeer(beerLst.Beers[0]);
                
                statusDO = utility.GetStatus(Status.Success,
                    Utility.SetUserMessage(beerLst.Beers[0].Id > 0
                    ? ActionName.Update : ActionName.Insert, result), result);
            }
            return statusDO;
        }
        #endregion
    }
}