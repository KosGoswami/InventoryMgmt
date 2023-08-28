using InventoryMgmtDE.DataEntity;

namespace InventoryMgmtDL.IDataLayer
{
    public interface IBeerDL
    {
        public abstract List<BeerDO> GetBeer(int? id, double? ltAlcoholByVolume, double? gtAlcoholByVolume);
        public abstract int InsertUpdateBeer(BeerDO beerDO);
    }
}
