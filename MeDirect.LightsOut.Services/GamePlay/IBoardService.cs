using MeDirect.LightsOut.Data.Model.Game;
using System.Collections.Generic;

namespace MeDirect.LightsOut.Service.GamePlay
{
    /// <summary>
    /// Board Actions
    /// </summary>
    public interface IBoardService
    {
        /// <summary>
        /// Gets a new game board
        /// </summary>
        /// <param name="gameRequest"></param>
        /// <returns></returns>
        List<GameLocationDetail> Create(GameRequest gameRequest);
    }
}