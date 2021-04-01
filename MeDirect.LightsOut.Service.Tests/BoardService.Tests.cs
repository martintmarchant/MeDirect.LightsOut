using MeDirect.LightsOut.Data.Model.Settings;
using MeDirect.LightsOut.Service.Configuration;
using MeDirect.LightsOut.Service.GamePlay;
using Moq;
using System.Linq;
using System.Threading.Tasks;
using Xunit;

namespace MeDirect.LightsOut.Service.Tests
{
    /// <summary>
    /// Test Board Service
    /// </summary>
    public class BoardServiceTest
    {
        GameConfiguration _defaultGameConfiguration = new GameConfiguration()
        {
            MaxBoardColumns = 5,
            MinBoardColumns = 5,
            MaxBoardRows = 2,
            MinBoardRows = 2,
            MinNumberOfLightsAtStartUp = 1
        };


        /// <summary>
        /// Moq dependancies
        /// </summary>
        private IBoardService GetBoardService(GameConfiguration gameConfiguration)
        {
            // Mock GameConfiguration
            var _gameConfigurationService = new Mock<IGameConfigurationService>();
            _gameConfigurationService
                .Setup(o => o.GetGameConfiguration())
                .Returns(new Task<GameConfiguration>(() =>
                    new GameConfiguration()
                    {
                        MaxBoardColumns = gameConfiguration.MaxBoardColumns,
                        MinBoardColumns = gameConfiguration.MinBoardColumns,
                        MaxBoardRows = gameConfiguration.MaxBoardRows,
                        MinBoardRows = gameConfiguration.MinBoardRows,
                        MinNumberOfLightsAtStartUp = gameConfiguration.MinNumberOfLightsAtStartUp
                    }));

            return new BoardService(_gameConfigurationService.Object);
        }


        /// <summary>
        /// Count the number of cells on the board
        /// </summary>
        [Fact]
        public void Correct_Number_of_board_cells_returned()
        {
            // Arrange
            var boardService = GetBoardService(_defaultGameConfiguration);                

            // Act
            var board = boardService.Create(new Data.Model.Game.GameRequest() { Columns = 5, Rows = 5, StartLightCount = 1 });
            
            // Assert - Count the number of cells on the board
            Assert.Equal(25, board.Count());
        }


        /// <summary>
        /// Count the number of Lite cells (1 light)
        /// </summary>
        [Fact]
        public void Correct_Number_of_Board_cells_With_LightsOn_Min()
        {
            // Arrange
            var boardService = GetBoardService(_defaultGameConfiguration);

            // Act
            var board = boardService.Create(
                new Data.Model.Game.GameRequest() { Columns = 5, Rows = 1, StartLightCount = 1 });

            // Assert - Count the number of cells on the board
            Assert.Equal(1, board.Count(c => c.IsLit));
        }


        /// <summary>
        /// Count the number of Lite cells (10 lights)
        /// </summary>
        [Fact]
        public void Correct_Number_of_Board_cells_With_LightsOn_Mid()
        {
            // Arrange
            var boardService = GetBoardService(_defaultGameConfiguration);

            // Act
            var board = boardService.Create(
                new Data.Model.Game.GameRequest() { Columns = 5, Rows = 1, StartLightCount = 10 });

            // Assert - Count the number of cells on the board
            Assert.Equal(10, board.Count(c => c.IsLit));
        }

        /// <summary>
        /// error: number of Lite cells exceeds board cells available
        /// </summary>
        [Fact]
        public void Lit_cells_Error_overflow()
        {
            // Arrange
            var boardService = GetBoardService(_defaultGameConfiguration);

             // Assert - Count the number of cells on the board
            Assert.Throws<System.Exception>(() =>
                boardService.Create(
                    new Data.Model.Game.GameRequest() { Columns = 5, Rows = 5, StartLightCount = 100 }));
        }


        /// <summary>
        /// error: Number of rows below configuration value
        /// </summary>
        [Fact]
        public void Correct_Number_of_rows_below_min()
        {
            // Arrange
            var boardService = GetBoardService(_defaultGameConfiguration);

            // Assert - Count the number of cells on the board
            Assert.Throws<System.Exception>(() =>
                boardService.Create(
                    new Data.Model.Game.GameRequest() { Columns = 5, Rows = 1, StartLightCount = 1 }));
        }

        /// <summary>
        /// error: Number of columns below configuration value
        /// </summary>
        [Fact]
        public void Correct_Number_of_cols_below_min()
        {
            // Arrange
            var boardService = GetBoardService(_defaultGameConfiguration);

            // Assert - Count the number of cells on the board
            Assert.Throws<System.Exception>(() =>
                boardService.Create(
                    new Data.Model.Game.GameRequest() { Columns = 1, Rows = 5, StartLightCount = 1 }));
        }

    }
}