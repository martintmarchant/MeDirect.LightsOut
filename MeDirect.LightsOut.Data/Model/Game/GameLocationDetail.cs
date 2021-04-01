namespace MeDirect.LightsOut.Data.Model.Game
{
    /// <summary>
    /// Represent the content of a square 
    /// </summary>
    /// <remarks>
    /// (Extends location)
    /// </remarks>
    public class GameLocationDetail : GameLocation
    {
        /// <summary>
        /// identifies if the Square is lit
        /// </summary>
        public bool IsLit { get; set; } = false;
    }
}