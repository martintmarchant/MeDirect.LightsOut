using MeDirect.LightsOut.Data.Model.Settings;
using MeDirect.LightsOut.Service.Configuration;
using Microsoft.AspNetCore.Mvc;
using Serilog;
using System.Threading.Tasks;

namespace MeDirect.LightsOut.Api.Controllers
{

    /// <summary>
    /// Configuration Controller
    /// </summary>
    [ApiController]
    [Route("[controller]")]
    public class GameConfigurationController
    {
        #region Local Variables
        private readonly IGameConfigurationService _gameConfigurationService;
        #endregion Local Variables

        #region Constructor
        /// <summary>
        /// Default Constructor
        /// </summary>
        /// <param name="gameConfigurationService"></param>
        public GameConfigurationController(IGameConfigurationService gameConfigurationService)
        {
            _gameConfigurationService = gameConfigurationService;
        }
        #endregion

        /// <summary>
        /// Get the enforced configuration settings
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public Task<GameConfiguration> GetConfiguration()
        {
            Log.Information("ConfigurationController.GetConfiguration");
            return _gameConfigurationService.GetGameConfiguration();
        }
    }
}
