using Dapper;
using InventoryMgmt.Common;
using InventoryMgmtDE.DataEntity;
using InventoryMgmtDL.IDataLayer;
using Microsoft.Data.SqlClient;
using System.Data;

namespace InventoryMgmtDL.DataLayer
{
    public class LogDL : ILogDL
    {
        #region global variable
        public SqlConnection _conn;
        #endregion

        #region constructor
        public LogDL()
        {
            _conn = new SqlConnection(DBConnString.connectionString);
        }
        #endregion

        #region insert log
        public int InsertLog(LogDO logDO)
        {
            DynamicParameters dbParams = SetDBParametersGet(logDO);
            
            return Convert.ToInt32(_conn.ExecuteScalar(StoreProc.InsertLog, dbParams,
                commandType: CommandType.StoredProcedure));
        }
        #endregion

        #region private - Set DB Parameters
        private static DynamicParameters SetDBParametersGet(LogDO logDO)
        {
            DynamicParameters parameters = new();

            parameters.Add(DParameter.RequestId, logDO.RequestId);
            parameters.Add(DParameter.MethodName, logDO.MethodName);
            parameters.Add(DParameter.ExLevel, logDO.ExLevel);
            parameters.Add(DParameter.ExMessage, logDO.ExMessage);
            parameters.Add(DParameter.Detail, logDO.Detail);

            return parameters;
        }
        #endregion
    }
}
