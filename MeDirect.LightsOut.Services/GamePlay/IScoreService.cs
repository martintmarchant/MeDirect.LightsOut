using MeDirect.LightsOut.Data.Model.Game;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MeDirect.LightsOut.Service.GamePlay
{
    public interface IScoreService
    {
        /// <summary>
        /// Gets a list of the top 20 previous Games matching the gameLayout
        /// </summary>
        /// <param name="gameRequest"></param>
        /// <returns>List of Hiscores</returns>
        Task<IEnumerable<GameScore>> GetHighScores(GameRequest gameRequest);

        /// <summary>
        /// Inserts Game Detail into the games repository archive
        /// </summary>
        /// <param name="gameScore"></param>
        /// <returns>Rows Affected</returns>
        Task<int> InsertGameScore(GameScore gamescore);
    }
}