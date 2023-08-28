namespace InventoryMgmt.Common
{
    /* This class is used for defining
     * all action/error-types/db proc & parameters
     */

    #region action names
    public static class ActionName
    {
        public const string Insert = "Insert";
        public const string Update = "Update";

        public const string GetBar = "GetBar";
        public const string InsertBar = "InsertBar";
        public const string UpdateBar = "UpdateBar";

        public const string GetBeer = "GetBeer";
        public const string InsertBeer = "InsertBeer";
        public const string UpdateBeer = "UpdateBeer";

        public const string GetBrewery = "GetBrewery";
        public const string InsertBrewery = "InsertBrewery";
        public const string UpdateBrewery = "UpdateBrewery";

        public const string GetBeerBar = "GetBeerBar";
        public const string InsertBeerBar = "InsertBeerBar";

        public const string GetBeerBrewery = "GetBeerBrewery";
        public const string InsertBeerBrewery = "InsertBeerBrewery";
    }
    #endregion

    #region action type
    public enum ActionType
    {
        Insert = 0,
        Update = 1
    }
    #endregion

    #region error level
    public enum ErrorLevel
    {
        Trace = 0,
        Debug = 1,
        Info = 2,
        Warning = 3,
        Error = 4,
        Critical = 5,
        None = 6
    }
    #endregion

     #region request status
    public static class Status
    {
        public const string Default = "Default";
        public const string Error = "Error";
        public const string Fail = "Fail";
        public const string InValidRequest = "InValidRequest";
        public const string ModelError = "ModelError";
        public const string RangeNotValid = "Range is not valid";
        public const string ReqIdError = "ReqIdError";
        public const string Success = "Success";
    }
    public static class StatusCode
    {
        public const int Success = 200;
        public const int Fail = 400;
    }
        #endregion

        #region data object/model names
        public static class ModelName
    {
        public const string Beers = "Beers";
        public const string Bars = "Bars";
        public const string Breweries = "Breweries";
        public const string BeerBars = "BeerBars";
        public const string BeerBreweries = "BeerBreweries";
    }
    #endregion

    #region database settings
    public static class DBConnString
    {
        public const string connectionString = "Data Source=localhost;Initial Catalog=InventroyManagement;User id=IMUser;Password=IMPwd;Connection Timeout=30;TrustServerCertificate=true";
    }

    public static class StoreProc
    {
        // GET
        public const string GetBeers = "GetBeer";
        public const string GetBars = "GetBar";
        public const string GetBrewerys = "GetBrewery";
        public const string GetBeerBar = "GetBeerBar";
        public const string GetBeerBrewery = "GetBeerBrewery";

        //INSERT_UPDATE
        public const string InsertUpdateBeer = "InsertUpdateBeer";
        public const string InsertUpdateBar = "InsertUpdateBar";
        public const string InsertUpdateBrewery = "InsertUpdateBrewery";
        public const string InsertBeerBar = "InsertBeerBar";
        public const string InsertBeerBrewery = "InsertBeerBrewery";
        public const string InsertLog = "InsertLog";
    }
    
    public static class DParameter
    {
        //Common
        public const string CreatedBy = "@CreatedBy";
        public const string Id = "@id";
        public const string IsActive = "@IsActive";
        public const string Name = "@Name";
        public const string UpdatedBy = "@UpdatedBy";

        //log
        public const string Detail = "@Detail";
        public const string ExLevel = "@ExLevel";
        public const string ExMessage = "@ExMessage";
        public const string MethodName = "@MethodName";
        public const string RequestId = "@RequestId";

        //Beer parameters
        public const string PercentageAlcoholByVolume = "@PercentageAlcoholByVolume";
        public const string LtAlcoholByVolume = "@LtAlcoholByVolume";
        public const string GtAlcoholByVolume = "@GtAlcoholByVolume";

        //Bar parameters
        public const string Address = "@Address";

        //BeerBar
        public const string BeerId = "@BeerId";
        public const string BarId = "@BarId";

        //BeerBrewery
        public const string BreweryId = "@BreweryId";
    }
    #endregion
}