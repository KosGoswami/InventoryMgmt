using InventoryMgmt.Common;
using InventoryMgmtBL.IModule;
using InventoryMgmtDE.DataEntity;
using InventoryMgmtDL.DataLayer;
using InventoryMgmtDL.IDataLayer;

namespace InventoryMgmtBL.Module
{
    public class BeerBreweryBL : IBeerBreweryBL
    {
        #region global variables
        public LogDO logDO = new();
        public StatusDO statusDO = new();
        public Utility utility = new();
        #endregion

        #region constructor
        private readonly IBeerBreweryDL _ibeerBreweryDl;
        public BeerBreweryBL(IBeerBreweryDL beerBreweryDL)
        {
            _ibeerBreweryDl = beerBreweryDL;    
        }
        #endregion

        #region get beer brewery
        public BeerBreweryLst GetBeerBrewery(string? requestId, int? breweryId, int? beerId)
        {
            //Local Variable
            BeerBreweryLst beerBreweryLst = new();

            int? id = breweryId != null ? breweryId : beerId;

            if (requestId == null)
            {
                statusDO = utility.GetStatus(Status.ReqIdError, UserMessages.ErrorReqId, id);
            }
            else
            {
                List<BeerBreweryDO> beerBreweries = _ibeerBreweryDl.GetBeerBrewery(breweryId, beerId);
                beerBreweryLst.BeerBreweries = SetBeerBrewery(beerBreweries);
               
                string userMessage = (beerBreweryLst.BeerBreweries == null 
                    || beerBreweryLst.BeerBreweries.Count == 0)
                ? UserMessages.NoRecordFound : UserMessages.SuccessMessage;

                statusDO = utility.GetStatus(Status.Success, userMessage, id);
            }
            beerBreweryLst.RequestId = requestId;
            beerBreweryLst.StatusDO = statusDO;
            return beerBreweryLst;
        }
        #endregion

        #region insert beer brewery
        public StatusDO? InsertBeerBrewery(BeerBreweryDtl beerBreweryLst, ActionType actionType)
        {
            statusDO = utility.IsValidRequest(beerBreweryLst, actionType, ModelName.BeerBreweries);

            if (statusDO != null && statusDO.Status == Status.Success
                && beerBreweryLst!=null && beerBreweryLst.BeerBreweries!=null)
            {
                int result = _ibeerBreweryDl.InsertBeerBrewery(beerBreweryLst.BeerBreweries[0]);

                statusDO = utility.GetStatus(Status.Success,
                    Utility.SetUserMessage(ActionName.Insert, result), result);
            }
            return statusDO;
        }
        #endregion                                          s


        #region private
        private static List<BeerByBreweryDO> SetBeerBrewery(List<BeerBreweryDO> beerBeweryLst)
        {
            var beerByBrewery = beerBeweryLst.GroupBy(r => r.BreweryId).Select(g =>

            new BeerByBreweryDO()
            {
                BreweryId = g.Key,
                BreweryName = string.Empty,
                Beers = g.Select(cr =>
                new BeerDO
                {
                    Id = cr.BeerId,
                    Name = cr.BeerName,
                    IsActive = cr.IsActive
                }).ToList()
            }).ToList();

            return beerByBrewery;
        }
        #endregion
    }
}
