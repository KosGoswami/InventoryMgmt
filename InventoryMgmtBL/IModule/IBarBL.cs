using InventoryMgmt.Common;
using InventoryMgmtDE.DataEntity;

namespace InventoryMgmtBL.IModule
{
    public interface IBarBL
    {
        public abstract BarLst GetBar(string? requestId, int? id);
        public abstract StatusDO? InsertUpdateBar(BarLst barLst, ActionType actionType);
    }
}
