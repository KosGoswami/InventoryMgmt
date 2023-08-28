using InventoryMgmtDE.DataEntity;

namespace InventoryMgmtDL.IDataLayer
{
    public interface IBreweryDL
    {
        public abstract List<BreweryDO> GetBrewery(int? id);
        public abstract int InsertUpdateBrewery(BreweryDO breweryDO);
    }
}
