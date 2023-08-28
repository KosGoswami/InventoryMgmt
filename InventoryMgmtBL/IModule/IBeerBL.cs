using InventoryMgmt.Common;
using InventoryMgmtDE.DataEntity;

namespace InventoryMgmtBL.IModule
{
    public interface IBeerBL
    {
        public abstract BeerLst GetBeer(string? requestId, int? id, double? ltAlcoholByVolume, double? gtAlcoholByVolume);
        public abstract StatusDO? InsertUpdateBeer(BeerLst beerLst, ActionType actionType);
    }
}
