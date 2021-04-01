using MeDirect.LightsOut.Data.Model.Game;
using System.Collections.Generic;

namespace MeDirect.LightsOut.Service.GamePlay
{
    /// <summary>
    /// Game Movements
    /// </summary>
    public interface IMoveService
    {
        /// <summary>
        /// Gets a list of effected cells (Game Locations) when gameLocationSelected is toggled
        /// </summary>
        /// <param name="gameLocation">location selected</param>
        /// <returns>cells to toggle</returns>
        List<GameLocationDetail> GetToggleCellEffectedCells(List<GameLocationDetail> boardCells, GameLocation gameLocationSelected);
    }
}