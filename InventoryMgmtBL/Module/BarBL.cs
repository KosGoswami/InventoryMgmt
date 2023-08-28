using InventoryMgmt.Common;
using InventoryMgmtBL.IModule;
using InventoryMgmtDE.DataEntity;
using InventoryMgmtDL.IDataLayer;

namespace InventoryMgmtBL.Module
{
    public class BarBL : IBarBL
    {
        #region global variables
        public Utility utility = new();
        public StatusDO statusDO = new();
        #endregion

        #region constructor
        private readonly IBarDL _ibarDl;

        public BarBL(IBarDL barDL)
        {
            _ibarDl = barDL;
            
        }
        #endregion

        #region get bar
        public BarLst GetBar(string? requestId, int? id)
        {
            //Local Variable
            BarLst barLst = new BarLst();

            if (requestId == null)
            {
                statusDO = utility.GetStatus(Status.ReqIdError, UserMessages.ErrorReqId, id);
            }
            else
            {
                barLst.Bars = _ibarDl.GetBar(id);

                string userMessage = (barLst.Bars == null || barLst.Bars.Count == 0)
                  ? UserMessages.NoRecordFound : UserMessages.SuccessMessage;

                statusDO = utility.GetStatus(Status.Success, userMessage, id);
            }

            barLst.RequestId = requestId;
            barLst.StatusDO = statusDO;
            return barLst;
        }
        #endregion

        #region insert & update bar
        public StatusDO? InsertUpdateBar(BarLst barLst, ActionType actionType)
        {
            //Local Variable
            statusDO = utility.IsValidRequest(barLst, actionType, ModelName.Bars);

            if (statusDO != null && statusDO.Status == Status.Success
                && barLst!=null && barLst.Bars!=null)
            {
                int result = _ibarDl.InsertUpdateBar(barLst.Bars[0]);

                statusDO = utility.GetStatus(Status.Success,
                    Utility.SetUserMessage(barLst.Bars[0].Id > 0
                    ? ActionName.Update : ActionName.Insert, result), result);
            }
            return statusDO;
        }

        #endregion
    }
}
