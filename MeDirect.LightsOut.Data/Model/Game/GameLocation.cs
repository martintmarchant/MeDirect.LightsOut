namespace MeDirect.LightsOut.Data.Model.Game
{
    /// <summary>
    /// Represent a location of a square
    /// </summary>
    public class GameLocation
    {
        /// <summary>
        /// identifies the location (Column X)
        /// </summary>
        public int PositionColumn { get; set; }

        /// <summary>
        /// identifies the location (Row Y)
        /// </summary>
        public int PositionRow { get; set; }
    }
}