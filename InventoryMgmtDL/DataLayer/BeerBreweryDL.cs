using Dapper;
using InventoryMgmt.Common;
using InventoryMgmtDE.DataEntity;
using InventoryMgmtDL.IDataLayer;
using Microsoft.Data.SqlClient;
using System.Data;

namespace InventoryMgmtDL.DataLayer
{
    public class BeerBreweryDL : IBeerBreweryDL
    {
        #region global variables
        public SqlConnection _conn;
        #endregion

        #region constructor
        public BeerBreweryDL()
        {
            _conn = new SqlConnection(DBConnString.connectionString);
        }
        #endregion

        #region get beer breweries from DB
        public List<BeerBreweryDO> GetBeerBrewery(int? beerId, int? breweryId)
        {
            //Setting stored proc parameters to be passed
            DynamicParameters dbParams = SetDBParametersGet(breweryId, beerId);

            var obj = _conn.QueryMultiple(StoreProc.GetBeerBrewery,
                dbParams, commandType: CommandType.StoredProcedure);
            return obj.Read<BeerBreweryDO>().ToList();
        }                                                              
        #endregion                                                                        

        #region insert beer breweries in DB
        public int InsertBeerBrewery(BeerBreweryDO beerBreweryDO)
        {
            //Setting stored proc parameters to be passed
            DynamicParameters dbParams = SetDBParametersInsUpd(beerBreweryDO);

            return Convert.ToInt32(_conn.ExecuteScalar(StoreProc.InsertBeerBrewery, dbParams,
                commandType: CommandType.StoredProcedure));
        }
        #endregion

        #region private - Set DB Parameters
        private static DynamicParameters SetDBParametersGet(int? breweryId, int? beerId)
        {
            DynamicParameters parameters = new();
            parameters.Add(DParameter.BreweryId, breweryId);
            parameters.Add(DParameter.BeerId, beerId);
            return parameters;
        }

        private static DynamicParameters SetDBParametersInsUpd(BeerBreweryDO beerBreweryDO)
        {
            DynamicParameters parameters = new();

            parameters.Add(DParameter.BeerId, beerBreweryDO.BeerId);
            parameters.Add(DParameter.BreweryId, beerBreweryDO.BreweryId);
            parameters.Add(DParameter.IsActive, beerBreweryDO.IsActive);
            parameters.Add(DParameter.CreatedBy, beerBreweryDO.CreatedBy);

            return parameters;
        }

        #endregion
    }
}
