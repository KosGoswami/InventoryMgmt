using InventoryMgmtDE.DataEntity;

namespace InventoryMgmtDL.IDataLayer
{
    public interface IBeerBarDL
    {
        public abstract List<BeerBarDO> GetBeerBar(int? barId,int? beerId);
        public abstract int InsertBeerBar(BeerBarDO beerBarDO);
    }
}
