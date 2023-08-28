using System.ComponentModel.DataAnnotations;

namespace InventoryMgmtDE.DataEntity
{
    /* This Model is used for handling beer-brewery 
     * details for all get/post/put webapi request made
     */

    #region beer brewery 
    public class BeerBreweryDtl
    {
        public string? RequestId { get; set; }
        public List<BeerBreweryDO>? BeerBreweries { get; set; }
        public StatusDO? StatusDO { get; set; }
    }

    public class BeerBreweryLst
    {
        public string? RequestId { get; set; }
        public List<BeerByBreweryDO>? BeerBreweries { get; set; }
        public StatusDO? StatusDO { get; set; }
    }

    public class BeerByBreweryDO
    {
        public int BreweryId { get; set; }

        public string? BreweryName { get; set; }

        public List<BeerDO>? Beers { get; set; }
    }

    #endregion

    #region beerBrewery model
    public class BeerBreweryDO
    {
        public int Id { get; set; }

        [Required]
        public int BeerId { get; set; }

        [Required]
        public int BreweryId { get; set; }

        public string? BreweryName { get; set; }

        public string? BeerName { get; set; }

        public bool IsActive { get; set; }
        public int CreatedBy { get; set; }
        public DateTime CreatedDate { get; set; }
    }
    #endregion
}