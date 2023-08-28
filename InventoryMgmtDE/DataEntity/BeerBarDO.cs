using System.ComponentModel.DataAnnotations;

namespace InventoryMgmtDE.DataEntity
{
    /* This Model is used for handling beer-bar 
     * details for all get/post/put webapi request made
     */

    #region beerBar list
    public class BeerBarDtl
    {
        public string? RequestId { get; set; }
        public List<BeerBarDO>? BeerBars { get; set; }
        public StatusDO? StatusDO { get; set; }
    }
    #endregion

    public class BeerBarLst
    {
        public string? RequestId { get; set; }
        public List<BeerByBarDO>? BeerBars { get; set; }
        public StatusDO? StatusDO { get; set; }
    }

    public class BeerByBarDO
    {
        public int BarId { get; set; }

        public string? BarName { get; set; }

        public List<BeerDO>? Beers { get; set; }
    }

    #region beerBar model
    public class BeerBarDO
    {
        public int Id { get; set; }

        [Required]
        public int BeerId { get; set; }

        [Required]
        public int BarId { get; set; }

        [Required]
        public string? BeerName { get; set; }

        [Required]
        public string? BarName { get; set; }

        [Required]
        public bool IsActive { get; set; }
        public int CreatedBy { get; set; }
        public DateTime CreatedDate { get; set; }
    }
    #endregion
}