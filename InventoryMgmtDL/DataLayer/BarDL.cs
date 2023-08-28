using Dapper;
using InventoryMgmt.Common;
using InventoryMgmtDE.DataEntity;
using InventoryMgmtDL.IDataLayer;
using Microsoft.Data.SqlClient;
using System.Data;

namespace InventoryMgmtDL.DataLayer
{
    public class BarDL : IBarDL
    {
        #region global variables
        public SqlConnection _conn;
        #endregion

        #region constructor
        public BarDL()
        {
            _conn = new SqlConnection(DBConnString.connectionString);
        }
        #endregion

        #region get bars from DB
        public List<BarDO> GetBar(int? id)
        {
            //Setting stored proc parameters to be passed
            DynamicParameters dbParams = SetDBParametersGet(id);
           
            var obj = _conn.QueryMultiple(StoreProc.GetBars,
                dbParams, commandType: CommandType.StoredProcedure);
            return obj.Read<BarDO>().ToList();
        }
        #endregion

        #region insert - update bar in DB
        public int InsertUpdateBar(BarDO barDO)
        {
            //Setting stored proc parameters to be passed
            DynamicParameters dbParams = SetDBParametersInsUpd(barDO);

            return Convert.ToInt32(_conn.ExecuteScalar(StoreProc.InsertUpdateBar, dbParams,
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

        private static DynamicParameters SetDBParametersInsUpd(BarDO barDO)
        {
            DynamicParameters parameters = new();

            parameters.Add(DParameter.Id, barDO.Id);
            parameters.Add(DParameter.Name, barDO.Name);
            parameters.Add(DParameter.Address
                , barDO.Address);
            parameters.Add(DParameter.IsActive, barDO.IsActive);
            parameters.Add(DParameter.CreatedBy, barDO.CreatedBy);
            parameters.Add(DParameter.UpdatedBy, barDO.UpdatedBy);

            return parameters;
        }
        #endregion
    }
}
