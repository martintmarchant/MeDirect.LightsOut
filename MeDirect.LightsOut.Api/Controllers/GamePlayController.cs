using MeDirect.LightsOut.Data.Model.Game;
using MeDirect.LightsOut.Service.Configuration;
using MeDirect.LightsOut.Service.GamePlay;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Serilog;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MeDirect.LightsOut.Api.Controllers
{

    /// <summary>
    /// LightsOut Game Controller
    /// </summary>
    [ApiController]
    [Route("[controller]")]
    public class GamePlayController : ControllerBase
    {
        #region Local Variables
        private readonly IBoardService _boardService;
        private readonly IMoveService _moveService;
        private readonly IGameConfigurationService _gameConfigurationService;
        #endregion

        #region Constructor
        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="boardService">Service responsible for creation of board</param>
        /// <param name="moveService">Service responsible for cell changes</param>
        /// <param name="gameConfigurationService"></param>
        public GamePlayController(
            IBoardService boardService, 
            IMoveService moveService,
            IGameConfigurationService gameConfigurationService)
        {
            _boardService = boardService;
            _moveService = moveService;
            _gameConfigurationService = gameConfigurationService;
        }
        #endregion

        /// <summary>
        /// Gets a list of common board configurations
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public Task<IEnumerable<GamePlayMode>> GetCommonBoardConfigurations()
        {
            Log.Information("BoardController.GamePlayMode");
            return _gameConfigurationService.GetGameOptions();
        }

        /// <summary>
        /// Create a new game board
        /// </summary>
        /// <remarks>Create a new game board</remarks>
        /// <returns>All cells in the board</returns>
        [HttpPost]
        public List<GameLocationDetail> CreateNewGameBoard(GameRequest gameRequest)
        {
            Log.Information("BoardController.CreateNewGameBoard");
            return  _boardService.Create(gameRequest);
        }

        /// <summary>
        /// Board Cell Update. 
        /// </summary>
        /// <remarks>Toggles neighbours and returns list of cell affected</remarks>
        /// <returns>Cells Affected</returns>
        [HttpPut]
        public List<GameLocationDetail> UpdateCellsChange([FromBody] GameMovement gameMovement)
        {
            Log.Information("BoardController.SelectCell", gameMovement);

            if (gameMovement is null)
                throw new System.ArgumentNullException(
                    nameof(gameMovement),
                    "Parameter supplied was invalid when calling the GameMovement");

            return _moveService.GetToggleCellEffectedCells(gameMovement.Board, gameMovement.SelectedCell);
        }
    }
}