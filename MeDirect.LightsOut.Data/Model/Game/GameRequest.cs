namespace MeDirect.LightsOut.Data.Model.Game
{
    /// <summary>
    /// New Game Board Request
    /// </summary>
    public class GameRequest
    {
        /// <summary>
        /// Number of columns required for game board.
        /// </summary>
        public int Columns { get; set; } = 5;

        /// <summary>
        /// Number of rows required for game board.
        /// </summary>
        public int Rows { get; set; } = 5;

        /// <summary>
        /// Indicates number of Lights randomly placed at start of game.
        /// </summary>
        public int StartLightCount { get; set; } = 1;
    }
}