namespace InventoryMgmt.Common
{
    /* This class is used for maintaining all
     * types of user friendly messages
     */

    #region user friendly message
    public static class UserMessages
    {
        //Insert
        public const string InsertFailed = "Inserted failed";
        public const string InsertSuccess = "Inserted successfully";

        //Update
        public const string UpdateFailed = "Updated failed";
        public const string UpdateSuccess = "Updated successfully";

        //Error
        public const string ErrorMessage = "There is an error";
        public const string ErrorReqId = "Requestid not provided";
        public const string ErrorModel = "Validation failed";
        public const string InValidRequest = "Invalid Request";
        public const string ValidRequest = "Valid Request";
        public const string RangeNotValid = "Alcohol By Percentage - lower boundry cannot be greater than higher boundry";

        //Commom
        public const string NoRecordFound = "No record found";
        public const string SuccessMessage = "Request executed successfully";

        //Not Exist
        public const string BarNotExist = "Provided Bar id does not exist";
        public const string BeerNotExist = "Provided Beer id does not exist";
        public const string BreweryNotExist = "Provided Brewery id does not exist";
        public const string BeerBarNotExist = "Provided Beer id and Bar id do not exist";
        public const string BeerBreweryNotExist = "Provided Beer id and Brewery id do not exist";
        public const string BeerBreweryAlreadyExist = "Provided Beer id and Brewery id already exist";
        public const string BeerBarAlreadyExist = "Provided Beer id and Bar id already exist";
    }
    #endregion
}