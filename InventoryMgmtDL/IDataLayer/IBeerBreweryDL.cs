using InventoryMgmtDE.DataEntity;

namespace InventoryMgmtDL.IDataLayer
{
    public interface IBeerBreweryDL
    {
        public abstract List<BeerBreweryDO> GetBeerBrewery(int? beerId, int? breweryId);
        public abstract int InsertBeerBrewery(BeerBreweryDO beerBreweryDO);
    }
}