using MeDirect.Common;
using MeDirect.LightsOut.Data.Model.Game;
using MeDirect.LightsOut.Data.Model.Settings;
using MeDirect.LightsOut.Service.Configuration;
using Serilog;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MeDirect.LightsOut.Service.GamePlay
{
    /// <summary>
    /// Service responsible for Board Actions
    /// </summary>
    /// <remarks>
    /// Obviously these actions could have been done in the frontend 
    /// however, i am applying for a backend role.  and have therefore
    /// thought it would be more fitting to place the logic in the backend.
    /// </remarks>
    public class BoardService : IBoardService
    {
        #region Local Variables

        /// <summary>
        /// Game defaults
        /// </summary>
        private readonly GameConfiguration _gameConfiguration;

        #endregion Local Variables

        #region Constructor

        /// <summary>
        /// Default Constructor
        /// </summary>
        /// <param name="gameConfiguration"></param>
        public BoardService(IGameConfigurationService gameConfigurationService)
        {
            _gameConfiguration = gameConfigurationService.GetGameConfiguration().Result;
        }

        #endregion Constructor

        /// <summary>
        /// Gets a new game board
        /// </summary>
        /// <param name="gameRequest"></param>
        /// <returns></returns>
        public List<GameLocationDetail> Create(GameRequest gameRequest)
        {
            Log.Information("GameConfiguration.Create");
            ValidateGameRequest(gameRequest);

            var board = BoardDrawWithLightsOff(gameRequest);
            return LiteUpRandomCells(board, gameRequest.StartLightCount);
        }

        #region Private Methods

        /// <summary>
        /// Creates a default board
        /// </summary>
        /// <param name="gameRequest"></param>
        /// <returns></returns>
        private static List<GameLocationDetail> BoardDrawWithLightsOff(GameRequest gameRequest)
        {
            Log.Information("GameConfiguration.BoardDrawWithLightsOff");
            var newBoard = new List<GameLocationDetail>();
            for (int row = 1; row < gameRequest.Rows + 1; row++)
            {
                newBoard.AddRange(
                    Enumerable.Range(1, gameRequest.Columns)
                       .Select(column =>
                            new GameLocationDetail()
                            {
                                PositionColumn = column,
                                PositionRow = row,
                                IsLit = false
                            }));
            }
            return newBoard;
        }

        /// <summary>
        /// Sets a number of unlit random cells on given board to lit.
        /// </summary>
        /// <param name="board">the board to be affected</param>
        /// <param name="lightRequiredCount">number of squares to be affected</param>
        /// <returns>the adjusted board</returns>
        private static List<GameLocationDetail> LiteUpRandomCells(
                    List<GameLocationDetail> board, int lightRequiredCount)
        {
            Log.Information("GameConfiguration.LiteUpRandomCells");
            ValidateLights(board, lightRequiredCount);

            int boardTotalCells = board.Count;
            for (int lightCounter = 0; lightCounter < lightRequiredCount; lightCounter++)
            {
                // identifies random square from the board, not already Lit
                int counter = 0;
                int randomNumber = boardTotalCells.Randomize();
                while (board[randomNumber - 1].IsLit)
                {
                    randomNumber = boardTotalCells.Randomize();
                    counter += 1;

                    if(counter> boardTotalCells)
                    {
                        board.Where(b => !b.IsLit).First().IsLit = true;
                        break;
                    }
                }
                board[randomNumber - 1].IsLit = true;
            }

            return board;
        }

        #region Validation

        /// <summary>
        /// Validates Game Requested parameters.
        /// </summary>
        /// <param name="gameRequest"></param>
        private void ValidateGameRequest(GameRequest gameRequest)
        {
            Log.Information("GameConfiguration.ValidateGameRequest");
            if (gameRequest is null)
                throw new ArgumentNullException(nameof(gameRequest), "A game request is required to build a game board.");

            if (gameRequest.Columns < _gameConfiguration.MinBoardColumns || gameRequest.Rows < _gameConfiguration.MinBoardRows
             || gameRequest.Columns > _gameConfiguration.MaxBoardColumns || gameRequest.Rows > _gameConfiguration.MaxBoardRows)
                throw new Exception($"Board size must contain [{_gameConfiguration.MinBoardColumns}-{_gameConfiguration.MaxBoardColumns}] " +
                                    $"columns and [{_gameConfiguration.MinBoardRows}-{_gameConfiguration.MaxBoardRows}] rows");
        }

        /// <summary>
        /// Validate Board Lights required.
        /// </summary>
        /// <param name="board"></param>
        /// <param name="lightRequiredCount"></param>
        private static void ValidateLights(List<GameLocationDetail> board, int lightRequiredCount)
        {
            Log.Information("GameConfiguration.ValidateLights");
            if (board == null)
                throw new ArgumentNullException(nameof(board), "Board nust be supplied");

            if (board.Count(cell => cell.IsLit == false) < lightRequiredCount)
                throw new Exception("Number of lights requested exceeds number of unlit board squares");
        }

        #endregion Validation

        #endregion Private Methods
    }
}