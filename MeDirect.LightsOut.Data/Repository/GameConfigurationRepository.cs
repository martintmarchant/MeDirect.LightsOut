using Dapper;
using MeDirect.LightsOut.Data.Model.Game;
using MeDirect.LightsOut.Data.Model.Settings;
using MeDirect.LightsOut.Data.Settings;
using Serilog;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Threading.Tasks;

namespace MeDirect.LightsOut.Data.Repository
{
    /// <summary>
    /// Game Configuration Repository
    /// </summary>
    public class GameConfigurationRepository : IGameConfigurationRepository
    {
        #region Local Variables
        private readonly IApiConfiguration _apiConfiguration;
        #endregion Local Variables

        #region Constructor
        /// <summary>
        /// Default Constuctor
        /// </summary>
        public GameConfigurationRepository(IApiConfiguration apiConfiguration)
        {
            _apiConfiguration = apiConfiguration;
        }
        #endregion Constructor

        /// <summary>
        /// Gets the game configurable boundaries from the database
        /// </summary>
        /// <returns></returns>
        public async Task<GameConfiguration> GetGameConfiguration()
        {
            Log.Information("GameConfigurationRepository.GetGameConfiguration");
            var sql = @"SELECT 
                            MinBoardColumns, 
                            MinBoardRows, 
                            MaxBoardColumns, 
                            MaxBoardRows, 
                            MinNumberOfLightsAtStartUp
                        FROM GameConfiguration";

            using var dbConnection = new SqlConnection(_apiConfiguration.ConnectionString());
            return await dbConnection.QueryFirstAsync<GameConfiguration>(sql);
        }

        /// <summary>
        /// Gets a collection of game options
        /// </summary>
        /// <returns></returns>
        public async Task<IEnumerable<GamePlayMode>> GetGamesOptions()
        {
            Log.Information("GameConfigurationRepository.GetGamesEnumerable");
            var sql =
                @"SELECT[ConfigurationId]
                      ,[GameName]
                      ,[Columns]
                      ,[Rows]
                      ,[StartLightCount]
                  FROM[dbo].[GamePlayModes]
                  ORDER BY ConfigurationId";
            using var dbConnection = new SqlConnection(_apiConfiguration.ConnectionString());
            return await dbConnection.QueryAsync<GamePlayMode>(sql);
        }
    }
}