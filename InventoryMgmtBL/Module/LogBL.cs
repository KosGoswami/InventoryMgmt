using InventoryMgmtBL.IModule;
using InventoryMgmtDE.DataEntity;
using InventoryMgmtDL.IDataLayer;

namespace InventoryMgmtBL.Module
{
    public class LogBL : ILogBL
    {
        #region constructor
        private readonly ILogDL _iLogDL;

        public LogBL(ILogDL logDL)
        {
            _iLogDL = logDL;
        }
        #endregion

        #region insert log
        public void Logger(LogDO logDO)
        {
            try
            {
                _iLogDL.InsertLog(logDO);
            }
            catch (Exception)
            {
                //logger ex
            }
        }
        #endregion
    }
}
