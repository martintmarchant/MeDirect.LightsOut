using System.Collections.Generic;

namespace MeDirect.LightsOut.Data.Model.Game
{
    /// <summary>
    /// Represents a board with a selected cell.
    /// </summary>
    public class GameMovement
    {
        /// <summary>
        /// Represents the board
        /// </summary>
        public List<GameLocationDetail> Board { get; set; }

        /// <summary>
        /// The Cell due to be toggled
        /// </summary>
        public GameLocation SelectedCell { get; set; }
    }
}