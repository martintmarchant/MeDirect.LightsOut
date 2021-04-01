using MeDirect.LightsOut.Data.Model.Game;
using MeDirect.LightsOut.Data.Model.Settings;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MeDirect.LightsOut.Data.Repository
{
    /// <summary>
    /// Game Configuration Repository
    /// </summary>
    public interface IGameConfigurationRepository
    {
        /// <summary>
        /// Gets the Game configurable settings
        /// </summary>
        /// <returns></returns>
        Task<GameConfiguration> GetGameConfiguration();

        /// <summary>
        /// Gets a collection of popular board game options
        /// </summary>
        /// <returns></returns>
        Task<IEnumerable<GamePlayMode>> GetGamesOptions();
    }
}