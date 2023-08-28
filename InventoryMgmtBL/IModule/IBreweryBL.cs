using InventoryMgmt.Common;
using InventoryMgmtDE.DataEntity;

namespace InventoryMgmtBL.IModule
{
    public interface IBreweryBL
    {
        public abstract BreweryLst GetBrewery(string? requestId, int? id);
        public abstract StatusDO? InsertUpdateBrewery(BreweryLst breweryLst, ActionType actionType);
    }
}
