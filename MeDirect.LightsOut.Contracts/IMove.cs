using System.Collections.Generic;

namespace MeDirect.LightsOut.Contracts
{
    /// <summary>
    /// Game Movements
    /// </summary>
    public interface IMove
    {
        /// <summary>
        /// Gets a list of effected cells (Game Locations) when gameLocationSelected is toggled
        /// </summary>
        /// <param name="gameLocation">location selected</param>
        /// <returns>cells to toggle</returns>
        List<GameLocationDetail> GetToggleCellEffectedCells(List<GameLocationDetail> boardCells, GameLocation gameLocationSelected);
    }
}