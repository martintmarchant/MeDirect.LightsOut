using MeDirect.LightsOut.Data.Model.Game;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MeDirect.LightsOut.Data.Repository
{
    /// <summary>
    /// Scores Repository
    /// Responsible setting and retrieving the high score table
    /// </summary>
    public interface IGameScoresRepository
    {
        /// <summary>
        /// Gets a list of the top 20 previous Games matching the gameLayout
        /// </summary>
        /// <param name="gameRequest"></param>
        /// <returns>List of Hiscores</returns>
        Task<IEnumerable<GameScore>> GetHighScores(GameRequest gameRequest);

        /// <summary>
        /// Inserts the game score into the GameScores Table
        /// </summary>
        /// <param name="gamescore"></param>
        /// <returns>Number of rows affected</returns>
        Task<int> InsertGameScore(GameScore gamescore);

    }
}