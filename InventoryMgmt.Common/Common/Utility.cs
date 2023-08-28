using InventoryMgmtDE.DataEntity;

namespace InventoryMgmt.Common
{
    public class Utility
    {
        #region global variables
        public LogDO logDO = new();
        public StatusDO statusDO = new();
        #endregion

        #region public methods
        public LogDO GetErrorDetails(Exception ex, string? requestId, string actionName)
        {
            string expMsg = string.Empty;

            if (ex.InnerException != null)
            {
                expMsg = (ex.InnerException == null)
                    ? string.Empty : ex.InnerException.Message.ToString();
            }

            logDO = GetLogDetail(requestId, actionName,
               ErrorLevel.Error.ToString(), expMsg, ex.Message);

            return logDO;
        }

        public StatusDO GetStatus(string status, string userMessage, int? id)
        {
            statusDO.Id = id;
            statusDO.Message = userMessage;

            statusDO.Status = (status == Status.Default) ? status = Status.Success : status;
            statusDO.StatusCode = (status == Status.Success || status == Status.Default) 
                ? StatusCode.Success : StatusCode.Fail;

            return statusDO;
        }

        public static string SetUserMessage(string actionName, int result)
        {
            string userMessage;

            if (result > 0)
            {
                userMessage = actionName == ActionName.Insert ? UserMessages.InsertSuccess :
                    UserMessages.UpdateSuccess;
            }
            else if (result == -1)
            {
                userMessage = UserMessages.BeerNotExist;
            }
            else if (result == -2)
            {
                userMessage = UserMessages.BarNotExist;
            }
            else if (result == -3)
            {
                userMessage = UserMessages.BreweryNotExist;
            }
            else if (result == -4)
            {
                userMessage = UserMessages.BeerBarNotExist;
            }
            else if (result == -5)
            {
                userMessage = UserMessages.BeerBreweryNotExist;
            }
            else if (result == -6)
            {
                userMessage = UserMessages.BeerBarAlreadyExist;
            }
            else if (result == -7)
            {
                userMessage = UserMessages.BeerBreweryAlreadyExist;
            }
            else
            {
                userMessage = actionName == ActionName.Insert ? UserMessages.InsertFailed :
                    UserMessages.UpdateFailed;
            }
            return userMessage;
        }

        public StatusDO IsValidRequest<T>(T obj, ActionType actionType, string propName) where T : class
        {
            dynamic? list = (dynamic)(object)obj;

            dynamic? childList = list?.GetType().GetProperty(propName).GetValue(list, null);

            int id = 0;
            string status;
            string userMessage;

            if (list == null || childList == null)
            {
                status = Status.InValidRequest;
                userMessage = UserMessages.InValidRequest;
            }
            else if(childList?.Count != 1)
            {
                status = Status.InValidRequest;
                userMessage = UserMessages.InValidRequest;
            }
            else if (list?.RequestId == null)
            {
                status = Status.ReqIdError;
                userMessage = UserMessages.ErrorReqId;
                id = childList[0].Id;
            }
            else if ((actionType == ActionType.Insert && childList[0].Id != 0)
                   || (actionType == ActionType.Update && childList[0].Id == 0)
                   || (actionType == ActionType.Insert && childList[0].CreatedBy == 0)
                   || (actionType == ActionType.Update && childList[0].UpdatedBy == 0))
            {
                status = Status.InValidRequest;
                userMessage = UserMessages.InValidRequest;
                id = childList[0].Id;
            }
            else
            {
                status = Status.Success;
                userMessage = UserMessages.ValidRequest;
            }

            return GetStatus(status, userMessage, id);
        }
        #endregion

        #region private methods
        private LogDO GetLogDetail(string? requestId, string methodName
            , string exLevel, string? detail, string exMessage)
        {
            logDO.RequestId = requestId;
            logDO.MethodName = methodName;
            logDO.ExLevel = exLevel;
            logDO.Detail = detail;
            logDO.ExMessage = exMessage;

            return logDO;
        }
        #endregion
    }
}
