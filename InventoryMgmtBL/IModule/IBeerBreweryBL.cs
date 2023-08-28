using InventoryMgmt.Common;
using InventoryMgmtDE.DataEntity;

namespace InventoryMgmtBL.IModule
{
    public interface IBeerBreweryBL
    {
        public abstract BeerBreweryLst GetBeerBrewery(string? requestId, int? breweryId, int? beerId);
        public abstract StatusDO? InsertBeerBrewery(BeerBreweryDtl beerBreweryLst, ActionType actionType);
    }
}
