namespace MeDirect.LightsOut.Data.Model.Settings
{
    /// <summary>
    /// Game Settings (Defaults)
    /// </summary>
    public class GameConfiguration
    {
        /// <summary>
        /// Minimum number of Columns in a game
        /// </summary>
        public int MinBoardColumns { get; set; } = 2;

        /// <summary>
        /// Minimum number of Rows in a game
        /// </summary>
        public int MinBoardRows { get; set; } = 2;

        /// <summary>
        /// Maximum number of Columns in a game
        /// </summary>
        public int MaxBoardColumns { get; set; } = 128;

        /// <summary>
        /// Maximum number of Rows in a game
        /// </summary>
        public int MaxBoardRows { get; set; } = 128;

        /// <summary>
        /// Minimum number of lights turned on when game starts
        /// </summary>
        public int MinNumberOfLightsAtStartUp { get; set; } = 1;
    }
}