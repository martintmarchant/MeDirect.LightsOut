namespace MeDirect.LightsOut.Data.Model.Game
{
    /// <summary>
    /// Games available to play
    /// </summary>
    public class GamePlayMode : GameRequest
    {
        /// <summary>
        /// Name (or difficulty) of the game (e.g. Easy, Medium, Hard)
        /// </summary>
        /// <remarks>
        /// represents a board configuration at startup
        /// </remarks>
        public string GameName { get; set; }
    }
}
