using MeDirect.LightsOut.Data.Model.Game;
using MeDirect.LightsOut.Data.Model.Settings;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MeDirect.LightsOut.Service.Configuration
{
    public interface IGameConfigurationService
    {
        /// <summary>
        /// Gets the configurable limits for the Game
        /// </summary>
        Task<GameConfiguration> GetGameConfiguration();

        /// <summary>
        /// Gets a collection of popular board game options
        /// </summary>
        /// <returns></returns>
        Task<IEnumerable<GamePlayMode>> GetGameOptions();
    }
}