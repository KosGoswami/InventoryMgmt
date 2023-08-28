using Dapper;
using InventoryMgmt.Common;
using InventoryMgmtDE.DataEntity;
using InventoryMgmtDL.IDataLayer;
using Microsoft.Data.SqlClient;

using System.Data;

namespace InventoryMgmtDL.DataLayer
{
    public class BeerDL : IBeerDL
    {
        #region global variables
        public SqlConnection _conn;
        #endregion

        #region constructor
        public BeerDL()
        {
            _conn = new SqlConnection(DBConnString.connectionString);
        }
        #endregion

        #region get beers from DB
        public List<BeerDO> GetBeer(int? id,double? ltAlcoholByVolume,double? gtAlcoholByVolume)
        {
            //Setting stored proc parameters to be passed
            DynamicParameters dbParams = SetDBParametersGet(id,ltAlcoholByVolume,gtAlcoholByVolume);

            var obj = _conn.QueryMultiple(StoreProc.GetBeers,
                dbParams, commandType: CommandType.StoredProcedure);
            return obj.Read<BeerDO>().ToList();
        }
        #endregion

        #region insert - update beers in DB
        public int InsertUpdateBeer(BeerDO beerDO)
        {
            //Setting stored proc parameters to be passed
            DynamicParameters dbParams = SetDBParametersInsUpd(beerDO);

            return Convert.ToInt32(_conn.ExecuteScalar(StoreProc.InsertUpdateBeer, dbParams,
                commandType: CommandType.StoredProcedure));
        }
        #endregion

        #region private - Set DB Parameters
        private static DynamicParameters SetDBParametersGet(int? id, double? ltAlcoholByVolume, double? gtAlcoholByVolume)
        {
            DynamicParameters parameters = new();
            parameters.Add(DParameter.Id, id);
            parameters.Add(DParameter.LtAlcoholByVolume, ltAlcoholByVolume);
            parameters.Add(DParameter.GtAlcoholByVolume, gtAlcoholByVolume);
            return parameters;
        }

        private static DynamicParameters SetDBParametersInsUpd(BeerDO beerDO)
        {
            DynamicParameters parameters = new();

            parameters.Add(DParameter.Id, beerDO.Id);
            parameters.Add(DParameter.Name, beerDO.Name);
            parameters.Add(DParameter.PercentageAlcoholByVolume
                , beerDO.PercentageAlcoholByVolume);
            parameters.Add(DParameter.IsActive, beerDO.IsActive);
            parameters.Add(DParameter.CreatedBy, beerDO.CreatedBy);
            parameters.Add(DParameter.UpdatedBy, beerDO.UpdatedBy);

            return parameters;
        }
        #endregion
    }
}