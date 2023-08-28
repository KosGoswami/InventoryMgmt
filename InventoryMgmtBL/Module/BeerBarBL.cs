using InventoryMgmt.Common;
using InventoryMgmtBL.IModule;
using InventoryMgmtDE.DataEntity;
using InventoryMgmtDL.IDataLayer;

namespace InventoryMgmtBL.Module
{
    public class BeerBarBL : IBeerBarBL
    {
        #region global variables
        public StatusDO statusDO = new();
        public Utility utility = new();
        #endregion

        #region constructor
        private readonly IBeerBarDL _ibeerBarDl;
        public BeerBarBL(IBeerBarDL beerBarDL)
        {
            _ibeerBarDl = beerBarDL;
        }
        #endregion

        #region get beer bar
        public BeerBarLst GetBeerBar(string? requestId, int? barId, int? beerId)
        {
            //Local Variable
            BeerBarLst beerBarLst = new();

            int? id = barId != null ? barId : beerId;

            if (requestId == null)
            {
                statusDO = utility.GetStatus(Status.ReqIdError, UserMessages.ErrorReqId, id);
            }
            else
            {
                List<BeerBarDO> beerBars = _ibeerBarDl.GetBeerBar(barId, beerId);
                beerBarLst.BeerBars = SetBeerBar(beerBars);

                string userMessage = (beerBarLst.BeerBars == null || beerBarLst.BeerBars.Count == 0)
                ? UserMessages.NoRecordFound : UserMessages.SuccessMessage;

                statusDO = utility.GetStatus(Status.Success, userMessage, id);
            }

            beerBarLst.RequestId = requestId;
            beerBarLst.StatusDO = statusDO;
            return beerBarLst;
        }
        #endregion

        #region insert beer bar
        public StatusDO? InsertBeerBar(BeerBarDtl beerBarDtl, ActionType actionType)
        {
            statusDO = utility.IsValidRequest(beerBarDtl, actionType, ModelName.BeerBars);

            if (statusDO != null && statusDO.Status == Status.Success
                && beerBarDtl != null && beerBarDtl.BeerBars != null)
            {
                int result = _ibeerBarDl.InsertBeerBar(beerBarDtl.BeerBars[0]);

                statusDO = utility.GetStatus(Status.Success,
                    Utility.SetUserMessage(ActionName.Insert, result), result);
            }
            return statusDO;
        }
        #endregion

        #region private
        private static List<BeerByBarDO> SetBeerBar(List<BeerBarDO> beerBarLst)
        {
            var beerByBar = beerBarLst.GroupBy(r => r.BarId).Select(g =>

            new BeerByBarDO()
            {
                BarId = g.Key,
                BarName = string.Empty,
                Beers = g.Select(cr =>
                new BeerDO
                {
                    Id = cr.BeerId,
                    Name = cr.BeerName,
                    IsActive = cr.IsActive
                }).ToList()
            }).ToList();

            return beerByBar;
        }
        #endregion
    }
}
