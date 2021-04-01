using System.Collections.Generic;

namespace MeDirect.LightsOut.Data.Model.Game
{
    /// <summary>
    /// End of game details 
    /// </summary>
    public class GameCompleted : GameRequest
    {
        /// <summary>
        /// Name of Player
        /// </summary>
        public string PlayerName { get; set; }

        /// <summary>
        /// Starting layout of the game
        /// </summary>
        public List<GameLocationDetail> GameInitialLayout { get; set; }

        /// <summary>
        /// History of all changes in the game
        /// </summary>
        public List<GameLocation> GameMoves { get; set; }
    }
}