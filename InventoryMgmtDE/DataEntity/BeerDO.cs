using System.ComponentModel.DataAnnotations;

namespace InventoryMgmtDE.DataEntity
{
    /* This Model is used for handling beer 
     details for all get/post/put webapi request made
     */

    #region beer list
    public class BeerLst
    {
        public string? RequestId { get; set; }
        public List<BeerDO>? Beers { get; set; }
        public StatusDO? StatusDO { get; set; }
    }
    #endregion

    #region beer model
    public class BeerDO
    {
        public int Id { get; set; }

        [Required]
        [MaxLength(120)]
        public string? Name { get; set; }

        [Required]
        public double PercentageAlcoholByVolume { get; set; }

        [Required]
        public bool IsActive { get; set; }
        public int CreatedBy { get; set; }
        public DateTime CreatedDate { get; set; }
        public int? UpdatedBy { get; set; }
        public DateTime? UpdatedDate { get; set; }
    }
    #endregion
}