using MeDirect.LightsOut.Data.Model.Game;
using MeDirect.LightsOut.Data.Repository;
using Serilog;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MeDirect.LightsOut.Service.GamePlay
{
    /// <summary>
    /// Score Service
    /// Responsible for keeping track of scores
    /// </summary>
    public class ScoreService : IScoreService
    {
        #region Local Variables
        private IGameScoresRepository _gameScoreRepository;
        #endregion Local Variables

        #region Constructor
        /// <summary>
        /// Default Constructor
        /// </summary>
        /// <param name="gameScoreRepository"></param>
        public ScoreService(IGameScoresRepository gameScoreRepository)
        {
            _gameScoreRepository = gameScoreRepository;
        }
        #endregion Construtor


        /// <summary>
        /// Gets a list of the top 20 previous Games matching the gameLayout
        /// </summary>
        /// <param name="gameRequest"></param>
        /// <returns>List of Hiscores</returns>
        public Task<IEnumerable<GameScore>> GetHighScores(GameRequest gameRequest)
        {
            Log.Information("ScoreService.GetHighScores");
            return _gameScoreRepository.GetHighScores(gameRequest);
        }


        /// <summary>
        /// Inserts Game Detail into the games repository archive
        /// </summary>
        /// <param name="gameScore"></param>
        /// <returns>True on success</returns>
        public Task<int> InsertGameScore(GameScore gamescore)
        {
            Log.Information("ScoreService.InsertGameScore");
            return _gameScoreRepository.InsertGameScore(gamescore);
        }

    }
}
