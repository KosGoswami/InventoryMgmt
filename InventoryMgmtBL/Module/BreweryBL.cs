using InventoryMgmt.Common;
using InventoryMgmtBL.IModule;
using InventoryMgmtDE.DataEntity;
using InventoryMgmtDL.IDataLayer;

namespace InventoryMgmtBL.Module
{
    public class BreweryBL : IBreweryBL
    {
        #region global variables
        public StatusDO statusDO = new();
        public Utility utility = new();
        #endregion

        #region constructor
        private readonly IBreweryDL _ibreweryDl;
        public BreweryBL(IBreweryDL breweryDL)
        {
            _ibreweryDl = breweryDL;
        }
        #endregion

        #region get brewery
        public BreweryLst GetBrewery(string? requestId, int? id)
        {
            //Local Variable
            BreweryLst breweryLst = new BreweryLst();

            if (requestId == null)
            {
                statusDO = utility.GetStatus(Status.ReqIdError, UserMessages.ErrorReqId, id);
            }
            else
            {
                breweryLst.Breweries = _ibreweryDl.GetBrewery(id);

                string userMessage = (breweryLst.Breweries == null || breweryLst.Breweries.Count == 0)
                ? UserMessages.NoRecordFound : UserMessages.SuccessMessage;

                statusDO = utility.GetStatus(Status.Success, userMessage, id);
            }

            breweryLst.RequestId = requestId;
            breweryLst.StatusDO = statusDO;
            return breweryLst;
        }
        #endregion

        #region insert & update brewery
        public StatusDO? InsertUpdateBrewery(BreweryLst breweryLst, ActionType actionType)
        {
            //Local Variable
            statusDO = utility.IsValidRequest(breweryLst, actionType, ModelName.Breweries);

            if (statusDO != null && statusDO.Status == Status.Success
                && breweryLst!=null && breweryLst.Breweries!=null)
            {
                int result = _ibreweryDl.InsertUpdateBrewery(breweryLst.Breweries[0]);

                statusDO = utility.GetStatus(Status.Success,
                    Utility.SetUserMessage(breweryLst.Breweries[0].Id > 0
                    ? ActionName.Update : ActionName.Insert, result), result);
            }
            return statusDO;
        }
        #endregion
    }
}
