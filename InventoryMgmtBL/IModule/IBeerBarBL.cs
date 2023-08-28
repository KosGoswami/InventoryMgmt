using InventoryMgmt.Common;
using InventoryMgmtDE.DataEntity;

namespace InventoryMgmtBL.IModule
{
    public interface IBeerBarBL
    {
        public abstract BeerBarLst GetBeerBar(string? requestId, int? barId, int? beerId);
        public abstract StatusDO? InsertBeerBar(BeerBarDtl beerBarDtl, ActionType actionType);
    }
}
