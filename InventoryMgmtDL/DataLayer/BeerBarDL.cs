using Dapper;
using InventoryMgmt.Common;
using InventoryMgmtDE.DataEntity;
using InventoryMgmtDL.IDataLayer;
using Microsoft.Data.SqlClient;
using System.Data;

namespace InventoryMgmtDL.DataLayer
{
    public class BeerBarDL : IBeerBarDL
    {
        #region global variables
        public SqlConnection _conn;
        #endregion

        #region constructor
        public BeerBarDL()
        {
            _conn = new SqlConnection(DBConnString.connectionString);
        }
        #endregion

        #region get beer bars from DB
        public List<BeerBarDO> GetBeerBar(int? barId, int? beerId)
        {
            //Setting stored proc parameters to be passed  
            DynamicParameters dbParams = SetDBParametersGet(barId,beerId);
           
            var obj = _conn.QueryMultiple(StoreProc.GetBeerBar,
                dbParams, commandType: CommandType.StoredProcedure);
            return obj.Read<BeerBarDO>().ToList();
        }
        #endregion

        #region insert beer bars in DB
        public int InsertBeerBar(BeerBarDO beerBarDO)
        {
            //Setting stored proc parameters to be passed
            DynamicParameters dbParams = SetDBParametersInsUpd(beerBarDO);

            return Convert.ToInt32(_conn.ExecuteScalar(StoreProc.InsertBeerBar, dbParams,
                commandType: CommandType.StoredProcedure));
        }
        #endregion

        #region private - Set DB Parameters
        private static DynamicParameters SetDBParametersGet(int? barId,int? beerId)
        {
            DynamicParameters parameters = new();
            parameters.Add(DParameter.BarId, barId);
            parameters.Add(DParameter.BeerId, beerId);
            return parameters;
        }

        private static DynamicParameters SetDBParametersInsUpd(BeerBarDO beerBarDO)
        {
            DynamicParameters parameters = new();

            parameters.Add(DParameter.BeerId, beerBarDO.BeerId);
            parameters.Add(DParameter.BarId, beerBarDO.BarId);
            parameters.Add(DParameter.IsActive, beerBarDO.IsActive);
            parameters.Add(DParameter.CreatedBy, beerBarDO.CreatedBy);

            return parameters;
        }
        #endregion
    }
}