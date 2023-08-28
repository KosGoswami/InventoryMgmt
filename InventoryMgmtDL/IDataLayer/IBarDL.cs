using InventoryMgmtDE.DataEntity;

namespace InventoryMgmtDL.IDataLayer
{
    public interface IBarDL
    {
        public abstract List<BarDO> GetBar(int? id);
        public abstract int InsertUpdateBar(BarDO barDO);
    }
}
