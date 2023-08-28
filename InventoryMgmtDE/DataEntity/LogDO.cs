namespace InventoryMgmtDE.DataEntity
{
    /* This Model is used for logging error/info 
     * details for all get/post/put webapi request made
     */

    #region log model
    public class LogDO
    {
        public int Id { get; set; }
        public string? RequestId { get; set; }
        public DateTime LoggedDate { get; set; }
        public string? ExLevel { get; set; }
        public string? ExMessage { get; set; }
        public string? MethodName { get; set; }
        public string? Detail { get; set; }
    }
    #endregion
}