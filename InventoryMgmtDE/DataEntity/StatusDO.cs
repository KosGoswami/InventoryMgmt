namespace InventoryMgmtDE.DataEntity
{
    /* This Model is used for returning status 
     for all webapi request made
     */

    #region status model
    public class StatusDO
    {
        public int? Id { get; set; }
        public int? StatusCode { get; set; }
        public string? Status { get; set; }
        public string? Message { get; set; }
    }
    #endregion
}
