using System.ComponentModel.DataAnnotations;

namespace InventoryMgmtDE.DataEntity
{
    /* This Model is used for handling brewery 
     * details for all get/post/put webapi request made
     */

    #region brewery list
    public class BreweryLst
    {
        public string? RequestId { get; set; }
        public List<BreweryDO>? Breweries { get; set; }
        public StatusDO? StatusDO { get; set; }
    }
    #endregion

    #region brewery model
    public class BreweryDO
    {
        public int Id { get; set; }

        [Required]
        [MaxLength(120)]
        public string? Name { get; set; }

        [Required]
        public bool IsActive { get; set; }
        public int CreatedBy { get; set; }
        public DateTime CreatedDate { get; set; }
        public int? UpdatedBy { get; set; }
        public DateTime? UpdatedDate { get; set; }
    }
    #endregion
}