using MeDirect.LightsOut.Data.Model.Game;
using MeDirect.LightsOut.Service.GamePlay;
using Microsoft.AspNetCore.Mvc;
using Serilog;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MeDirect.LightsOut.Api.Controllers
{
    /// <summary>
    /// Controller for managing scores
    /// </summary>
    [ApiController]
    [Route("[controller]")]
    public class HighScoreTableController : ControllerBase
    {
        #region Local Variables
        private readonly IScoreService _scoreService;
        #endregion

        #region Constructor
        /// <summary>
        /// Default Constructor
        /// </summary>
        public HighScoreTableController(IScoreService scoreService)
        {
            _scoreService = scoreService;
        }
        #endregion Constructor

        /// <summary>
        /// Gets a list of the top 20 previous Games matching the gameLayout
        /// </summary>
        /// <param name="columns"></param>
        /// <param name="rows"></param>
        /// <param name="startLightCount"></param>
        /// <returns>List of Hiscores</returns>
        [HttpGet]
        public Task<IEnumerable<GameScore>> GetListOfHiScores(int columns, int rows, int startLightCount)
        {
            var gameRequest = new GameRequest()
            {
                Columns = columns,
                Rows = rows,
                StartLightCount = startLightCount
            };
            Log.Information("HighScoreTableController.GetListOfHiScores");
            return _scoreService.GetHighScores(gameRequest);
        }

        /// <summary>
        /// Inserts Game Detail into the games repository archive
        /// </summary>
        /// <param name="gameScore"></param>
        /// <returns>Rows affected</returns>
        [HttpPost]
        public Task<int> InsertScore(GameScore gameScore)
        {
            Log.Information("HighScoreTableController.InsertScore");
            return _scoreService.InsertGameScore(gameScore);
        }

    }
}
