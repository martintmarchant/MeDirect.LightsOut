using Dapper;
using MeDirect.LightsOut.Data.Model.Game;
using MeDirect.LightsOut.Data.Settings;
using Serilog;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Threading.Tasks;

namespace MeDirect.LightsOut.Data.Repository
{
    /// <summary>
    /// Scores Repository
    /// Responsible setting and retrieving the high score table
    /// </summary>
    public class GameScoresRepository : IGameScoresRepository
    {
        #region Local Variables
        private readonly IApiConfiguration _apiConfiguration;
        #endregion Local Variables

        #region Constructor
        /// <summary>
        /// Default Constuctor
        /// </summary>
        public GameScoresRepository(IApiConfiguration apiConfiguration)
        {
            _apiConfiguration = apiConfiguration;
        }
        #endregion Constructor

        /// <summary>
        /// gets the previous scores 
        /// </summary>
        /// <returns></returns>
        public async Task<IEnumerable<GameScore>> GetHighScores(GameRequest gameRequest)
        {
            Log.Information("GameConfigurationRepository.GetGameConfiguration");
            var sql = @"SELECT TOP 20
                               [Columns]
                              ,[Rows]
                              ,[StartLightCount]
                              ,[PlayerName]
                              ,[NoOfMoves]
                        FROM [LightsOut].[dbo].[GameScores]
                        WHERE Columns=@columns 
                          and Rows=@Rows 
                          and StartLightCount=@StartLightCount
                        ORDER BY NoOfMoves DESC";

            using var dbConnection = new SqlConnection(_apiConfiguration.ConnectionString());
            return await dbConnection.QueryAsync<GameScore>(sql, gameRequest);
        }

        /// <summary>
        /// Inserts the game score into the GameScores Table
        /// </summary>
        /// <param name="gamescore"></param>
        /// <returns>Number of rows affected</returns>
        public async Task<int> InsertGameScore(GameScore gamescore)
        {
            Log.Information("GameConfigurationRepository.GetGameConfiguration");
            var sql = @"INSERT INTO [dbo].[GameScores]
                               ([Columns]
                               ,[Rows]
                               ,[StartLightCount]
                               ,[PlayerName]
                               ,[NoOfMoves])
                         VALUES
                               (@Columns
                               ,@Rows
                               ,@StartLightCount
                               ,@PlayerName
                               ,@NoOfMoves)";

            using var dbConnection = new SqlConnection(_apiConfiguration.ConnectionString());
            return await dbConnection.ExecuteAsync(sql, gamescore);

        }
    }
}
