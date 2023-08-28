using System.ComponentModel.DataAnnotations;

namespace InventoryMgmtDE.DataEntity
{
    /* This Model is used for handling bar
     * details for all get/post/put webapi request made
     */

    #region bar list
    public class BarLst
    {
        public string? RequestId { get; set; }
        public List<BarDO>? Bars { get; set; }
        public StatusDO? StatusDO { get; set; }
    }
    #endregion

    #region bar model
    public class BarDO
    {
        public int Id { get; set; }

        [Required]
        [MaxLength(120)]
        public string? Name { get; set; }

        [Required]
        [MaxLength(400)]
        public string? Address { get; set; }

        [Required]
        public bool IsActive { get; set; }
        public int CreatedBy { get; set; }
        public DateTime CreatedDate { get; set; }
        public int? UpdatedBy { get; set; }
        public DateTime? UpdatedDate { get; set; }
    }
    #endregion
}