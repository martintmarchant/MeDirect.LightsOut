using System.Collections.Generic;

namespace MeDirect.LightsOut.Contracts
{
    /// <summary>
    /// Board Actions
    /// </summary>
    public interface IBoard
    {

        /// <summary>
        /// Gets a new game board
        /// </summary>
        /// <param name="gameRequest"></param>
        /// <returns></returns>
        List<GameLocationDetail> Create(GameRequest gameRequest);
    }
}