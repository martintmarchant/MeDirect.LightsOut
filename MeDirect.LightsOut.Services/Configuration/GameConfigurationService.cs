using MeDirect.LightsOut.Data.Model.Game;
using MeDirect.LightsOut.Data.Model.Settings;
using MeDirect.LightsOut.Data.Repository;
using MeDirect.LightsOut.Data.Settings;
using Microsoft.Extensions.Caching.Memory;
using Serilog;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MeDirect.LightsOut.Service.Configuration
{
    /// <summary>
    /// Get Game Configuration Limitations / Configurable Settings
    /// </summary>
    public class GameConfigurationService : IGameConfigurationService
    {
        #region Local Variables
        private readonly IGameConfigurationRepository _gameConfigurationRepository;
        private readonly IApiConfiguration _apiConfiguration;
        private readonly IMemoryCache _cache;
        #endregion Local Variables

        #region Constructor
        /// <summary>
        /// /Default Constructor
        /// </summary>
        /// <param name="gameConfigurationRepository"></param>
        /// <param name="cache"></param>
        public GameConfigurationService(IGameConfigurationRepository gameConfigurationRepository, IApiConfiguration apiConfiguration, IMemoryCache cache)
        {
            _gameConfigurationRepository = gameConfigurationRepository;
            _apiConfiguration = apiConfiguration;
            _cache = cache;
        }
        #endregion Constructor


        /// <summary>
        /// Gets the configurable limits for the Game
        /// </summary>
        public async Task<GameConfiguration> GetGameConfiguration()
        {
            Log.Information("ConfigurationService.GetGameConfigurationBoundaries");

            if (!_cache.TryGetValue("GameConfiguration", out GameConfiguration result))
            {
                result = await _gameConfigurationRepository.GetGameConfiguration();
                _cache.Set("gameConfiguration", result, TimeSpan.FromMinutes(_apiConfiguration.CacheTimeInterval()));
            }
            return result;
        }

        /// <summary>
        /// Gets a collection of popular board game options
        /// </summary>
        /// <returns></returns>
        public async Task<IEnumerable<GamePlayMode>> GetGameOptions()
        {
            Log.Information("ConfigurationService.GetGameConfigurationBoundaries");

            if (!_cache.TryGetValue("GamePlayModes", out IEnumerable<GamePlayMode> result))
            {
                result = await _gameConfigurationRepository.GetGamesOptions();
                _cache.Set("GamePlayModes", result, TimeSpan.FromMinutes(_apiConfiguration.CacheTimeInterval()));
            }
            return result;

        }
    }
}
