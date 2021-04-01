using MeDirect.LightsOut.Data.Model.Game;
using Serilog;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MeDirect.LightsOut.Service.GamePlay
{
    /// <summary>
    /// Game Move
    /// </summary>
    public class MoveService : IMoveService
    {
        #region Constructor

        /// <summary>
        /// Default Constructor
        /// </summary>
        public MoveService() { }

        #endregion Constructor

        /// <summary>
        /// Gets a list of effectd toggled cells (Game Locations) when gameLocationSelected is toggled
        /// </summary>
        /// <param name="gameLocation">location selected</param>
        /// <returns>cells to toggle</returns>
        public List<GameLocationDetail> GetToggleCellEffectedCells(
            List<GameLocationDetail> boardCells,
            GameLocation gameLocationSelected)
        {
            Log.Information("MoveService.GetToggleCellEffectedCells");
            ValidateMoveParameters(boardCells, gameLocationSelected);

            var result = new List<GameLocationDetail>();
            result.AddRange(
                boardCells.Where(cells =>
                        (cells.PositionColumn <= gameLocationSelected.PositionColumn + 1
                      && cells.PositionColumn >= gameLocationSelected.PositionColumn - 1
                      && cells.PositionRow == gameLocationSelected.PositionRow)
                      ||
                        (cells.PositionRow <= gameLocationSelected.PositionRow + 1
                      && cells.PositionRow >= gameLocationSelected.PositionRow - 1
                      && cells.PositionColumn == gameLocationSelected.PositionColumn))
                .Select(cell => new GameLocationDetail()
                {
                    PositionColumn = cell.PositionColumn,
                    PositionRow = cell.PositionRow,
                    IsLit = !cell.IsLit
                }));

            return result;
        }

        #region Validation

        /// <summary>
        /// Validates Parameters for a move operation
        /// </summary>
        /// <param name="boardCells"></param>
        /// <param name="gameLocationSelected"></param>
        private static void ValidateMoveParameters(
            List<GameLocationDetail> boardCells,
            GameLocation gameLocationSelected)
        {
            Log.Information("MoveService.ValidateMoveParameters");
            if (boardCells == null)
                throw new ArgumentNullException(nameof(boardCells), "Board nust be supplied");
            if (gameLocationSelected is null)
                throw new System.ArgumentNullException(nameof(gameLocationSelected));
        }

        #endregion Validation
    }
}