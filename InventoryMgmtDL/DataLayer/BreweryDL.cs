using Dapper;
using InventoryMgmt.Common;
using InventoryMgmtDE.DataEntity;
using InventoryMgmtDL.IDataLayer;
using Microsoft.Data.SqlClient;
using System.Data;

namespace InventoryMgmtDL.DataLayer
{
    public class BreweryDL : IBreweryDL
    {
        #region global variables
        public SqlConnection _conn;
        #endregion

        #region constructor
        public BreweryDL()
        {
            _conn = new SqlConnection(DBConnString.connectionString);
        }
        #endregion

        #region get brewery from DB
        public List<BreweryDO> GetBrewery(int? id)
        {
            //Setting stored proc parameters to be passed
            DynamicParameters dbParams = SetDBParametersGet(id);

            var obj = _conn.QueryMultiple(StoreProc.GetBars,
                dbParams, commandType: CommandType.StoredProcedure);
            return obj.Read<BreweryDO>().ToList();
        }
        #endregion

        #region insert - update brewery in DB
        public int InsertUpdateBrewery(BreweryDO breweryDO)
        {
            //Setting stored proc parameters to be passed
            DynamicParameters dbParams = SetDBParametersInsUpd(breweryDO);

            return Convert.ToInt32(_conn.ExecuteScalar(StoreProc.InsertUpdateBrewery, dbParams,
                commandType: CommandType.StoredProcedure));
        }
        #endregion

        #region private - Set DB Parameters
        private static DynamicParameters SetDBParametersGet(int? id)
        {
            DynamicParameters parameters = new();
            parameters.Add(DParameter.Id, id);
            return parameters;
        }

        private static DynamicParameters SetDBParametersInsUpd(BreweryDO breweryDO)
        {
            DynamicParameters parameters = new();

            parameters.Add(DParameter.Id, breweryDO.Id);
            parameters.Add(DParameter.Name, breweryDO.Name);
            parameters.Add(DParameter.IsActive, breweryDO.IsActive);
            parameters.Add(DParameter.CreatedBy, breweryDO.CreatedBy);
            parameters.Add(DParameter.UpdatedBy, breweryDO.UpdatedBy);

            return parameters;
        }
        #endregion
    }
}
