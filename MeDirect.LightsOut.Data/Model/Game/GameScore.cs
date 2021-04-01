namespace MeDirect.LightsOut.Data.Model.Game
{
    /// <summary>
    /// Game Score (required for high score table)
    /// </summary>
    public class GameScore : GameCompleted
    {
        /// <summary>
        /// Number of moves taken to complete Game
        /// </summary>
        public int NoOfMoves { get; set; }
    }
}
