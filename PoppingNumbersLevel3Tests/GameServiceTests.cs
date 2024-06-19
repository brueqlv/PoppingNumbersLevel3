using PoppingNumbersLevel3.Models;
using PoppingNumbersLevel3.Services;

namespace PoppingNumbersLevel3Tests
{
    public class GameServiceTests
    {
        private GameService _gameService;
        private GameBoard _gameBoard;

        [SetUp]
        public void Setup()
        {
            _gameBoard = new GameBoard(5, 5);
            _gameService = new GameService(_gameBoard);
        }

        [Test]
        public void PrintBoard_ShouldNotThrowException()
        {
            //Act & Assert
            Assert.DoesNotThrow(() => _gameService.PrintBoard());
        }

        [Test]
        public void ComputerTurn_ShouldPlaceThreeRandomNumbers()
        {
            //Arrange & Act
            _gameService.ComputerTurn(1, 3, false);

            var nonZeroCount = 0;
            for (var i = 0; i < _gameBoard.Height; i++)
            {
                for (var j = 0; j < _gameBoard.Width; j++)
                {
                    if (_gameBoard.Board[i, j] != null)
                    {
                        nonZeroCount++;
                    }
                }
            }

            //Assert
            Assert.That(nonZeroCount, Is.EqualTo(3));
        }

        [Test]
        public void IsGameOver_ShouldReturnFalseForNonFullBoard()
        {
            //Act & Assert
            Assert.That(_gameService.IsGameOver(), Is.False);
        }

        [Test]
        public void IsGameOver_ShouldReturnTrueForFullBoard()
        {
            //Arrange
            for (var i = 0; i < _gameBoard.Height; i++)
            {
                for (var j = 0; j < _gameBoard.Width; j++)
                {
                    _gameBoard.Board[i, j] = "1";
                }
            }
            //Act & Assert
            Assert.That(_gameService.IsGameOver(), Is.True);
        }

        [Test]
        public void ClearConnectedNumbers_HorizontalTriplets_ShouldReturnPoints()
        {
            //arrange
            _gameBoard.Board[0, 0] = "1";
            _gameBoard.Board[0, 1] = "1";
            _gameBoard.Board[0, 2] = "1";

            //Act
            var points = _gameService.ClearConnectedNumbers();

            //Assert
            Assert.That(points, Is.GreaterThan(0));
        }

        [Test]
        public void ClearConnectedNumbers_EmptyField_ShouldReturnZero()
        {
            //Act
            var points = _gameService.ClearConnectedNumbers();

            //Assert
            Assert.That(points, Is.EqualTo(0));
        }

        [Test]
        public void ClearConnectedNumbers_ShouldClearHorizontalTriplets()
        {
            //arrange
            _gameBoard.Board[0, 0] = "1";
            _gameBoard.Board[0, 1] = "1";
            _gameBoard.Board[0, 2] = "1";

            //Act
            _gameService.ClearConnectedNumbers();

            //Assert
            Assert.Multiple(() =>
            {
                Assert.That(_gameBoard.Board[0, 0], Is.EqualTo(null));
                Assert.That(_gameBoard.Board[0, 1], Is.EqualTo(null));
                Assert.That(_gameBoard.Board[0, 2], Is.EqualTo(null));
            });
        }

        [Test]
        public void ClearConnectedNumbers_ShouldClearHorizontalQuadruplets()
        {
            //Arrange
            _gameBoard.Board[0, 0] = "1";
            _gameBoard.Board[0, 1] = "1";
            _gameBoard.Board[0, 2] = "1";
            _gameBoard.Board[0, 3] = "1";

            //Act
            _gameService.ClearConnectedNumbers();

            //Assert
            Assert.Multiple(() =>
            {
                Assert.That(_gameBoard.Board[0, 0], Is.EqualTo(null));
                Assert.That(_gameBoard.Board[0, 1], Is.EqualTo(null));
                Assert.That(_gameBoard.Board[0, 2], Is.EqualTo(null));
                Assert.That(_gameBoard.Board[0, 3], Is.EqualTo(null));
            });
        }

        [Test]
        public void ClearConnectedNumbers_ShouldClearHorizontalQuintuplets()
        {
            //Arrange
            _gameBoard.Board[0, 0] = "1";
            _gameBoard.Board[0, 1] = "1";
            _gameBoard.Board[0, 2] = "1";
            _gameBoard.Board[0, 3] = "1";
            _gameBoard.Board[0, 4] = "1";

            //Act
            _gameService.ClearConnectedNumbers();

            //Assert
            Assert.Multiple(() =>
            {
                Assert.That(_gameBoard.Board[0, 0], Is.EqualTo(null));
                Assert.That(_gameBoard.Board[0, 1], Is.EqualTo(null));
                Assert.That(_gameBoard.Board[0, 2], Is.EqualTo(null));
                Assert.That(_gameBoard.Board[0, 3], Is.EqualTo(null));
                Assert.That(_gameBoard.Board[0, 4], Is.EqualTo(null));
            });
        }

        [Test]
        public void ClearConnectedNumbers_ShouldReturn100ForHorizontalTriplets()
        {
            //Arrange
            _gameBoard.Board[0, 0] = "1";
            _gameBoard.Board[0, 1] = "1";
            _gameBoard.Board[0, 2] = "1";

            //Act
            var result = _gameService.ClearConnectedNumbers();

            //Assert
            Assert.That(result, Is.EqualTo(100));
        }

        [Test]
        public void ClearConnectedNumbers_ShouldReturn200ForHorizontalQuadruplets()
        {
            //Arrange
            _gameBoard.Board[0, 0] = "1";
            _gameBoard.Board[0, 1] = "1";
            _gameBoard.Board[0, 2] = "1";
            _gameBoard.Board[0, 3] = "1";

            //Act
            var result = _gameService.ClearConnectedNumbers();

            //Assert
            Assert.That(result, Is.EqualTo(200));
        }

        [Test]
        public void ClearConnectedNumbers_ShouldReturn500ForHorizontalQuintuplets()
        {
            //Arrange
            _gameBoard.Board[0, 0] = "1";
            _gameBoard.Board[0, 1] = "1";
            _gameBoard.Board[0, 2] = "1";
            _gameBoard.Board[0, 3] = "1";
            _gameBoard.Board[0, 4] = "1";

            //Act
            var result = _gameService.ClearConnectedNumbers();

            //Assert
            Assert.That(result, Is.EqualTo(500));
        }

        [Test]
        public void ClearConnectedNumbers_ShouldClearVerticalAndHorizontalTriplets()
        {
            //Arrange
            _gameBoard.Board[0, 0] = "1";
            _gameBoard.Board[1, 0] = "1";
            _gameBoard.Board[2, 0] = "1";
            _gameBoard.Board[0, 1] = "1";
            _gameBoard.Board[0, 2] = "1";

            //Act
            _gameService.ClearConnectedNumbers();

            //Assert
            Assert.Multiple(() =>
            {
                Assert.That(_gameBoard.Board[0, 0], Is.EqualTo(null));
                Assert.That(_gameBoard.Board[1, 0], Is.EqualTo(null));
                Assert.That(_gameBoard.Board[2, 0], Is.EqualTo(null));
                Assert.That(_gameBoard.Board[0, 1], Is.EqualTo(null));
                Assert.That(_gameBoard.Board[0, 2], Is.EqualTo(null));
            });
        }

        [Test]
        public void ClearConnectedNumbers_ShouldReturn200ForDiagonalAndHorizontalTriplets()
        {
            //Arrange
            _gameBoard.Board[0, 0] = "1";
            _gameBoard.Board[1, 0] = "1";
            _gameBoard.Board[2, 0] = "1";
            _gameBoard.Board[0, 1] = "1";
            _gameBoard.Board[0, 2] = "1";

            //Act
            var result = _gameService.ClearConnectedNumbers();

            //Assert
            Assert.That(result, Is.EqualTo(200));
        }

        [Test]
        public void ClearConnectedNumbers_ShouldClearVerticalTriplets()
        {
            //Arrange
            _gameBoard.Board[0, 0] = "1";
            _gameBoard.Board[1, 0] = "1";
            _gameBoard.Board[2, 0] = "1";

            //Act
            _gameService.ClearConnectedNumbers();

            //Assert
            Assert.Multiple(() =>
            {
                Assert.That(_gameBoard.Board[0, 0], Is.EqualTo(null));
                Assert.That(_gameBoard.Board[1, 0], Is.EqualTo(null));
                Assert.That(_gameBoard.Board[2, 0], Is.EqualTo(null));
            });
        }

        [Test]
        public void ClearConnectedNumbers_ShouldClearVerticalQuadruplets()
        {
            //Arrange
            _gameBoard.Board[0, 0] = "1";
            _gameBoard.Board[1, 0] = "1";
            _gameBoard.Board[2, 0] = "1";
            _gameBoard.Board[3, 0] = "1";

            //Act
            _gameService.ClearConnectedNumbers();

            //Assert
            Assert.Multiple(() =>
            {
                Assert.That(_gameBoard.Board[0, 0], Is.EqualTo(null));
                Assert.That(_gameBoard.Board[1, 0], Is.EqualTo(null));
                Assert.That(_gameBoard.Board[2, 0], Is.EqualTo(null));
                Assert.That(_gameBoard.Board[3, 0], Is.EqualTo(null));
            });
        }

        [Test]
        public void ClearConnectedNumbers_ShouldClearVerticalQuintuplets()
        {
            //Arrange
            _gameBoard.Board[0, 0] = "1";
            _gameBoard.Board[1, 0] = "1";
            _gameBoard.Board[2, 0] = "1";
            _gameBoard.Board[3, 0] = "1";
            _gameBoard.Board[4, 0] = "1";

            //Act
            _gameService.ClearConnectedNumbers();

            //Assert
            Assert.Multiple(() =>
            {
                Assert.That(_gameBoard.Board[0, 0], Is.EqualTo(null));
                Assert.That(_gameBoard.Board[1, 0], Is.EqualTo(null));
                Assert.That(_gameBoard.Board[2, 0], Is.EqualTo(null));
                Assert.That(_gameBoard.Board[3, 0], Is.EqualTo(null));
                Assert.That(_gameBoard.Board[4, 0], Is.EqualTo(null));
            });
        }

        [Test]
        public void ClearConnectedNumbers_ShouldReturn100ForVerticalTriplets()
        {
            //Arrange
            _gameBoard.Board[0, 0] = "1";
            _gameBoard.Board[1, 0] = "1";
            _gameBoard.Board[2, 0] = "1";

            //Act
            var result = _gameService.ClearConnectedNumbers();

            //Assert
            Assert.That(result, Is.EqualTo(100));
        }

        [Test]
        public void ClearConnectedNumbers_ShouldReturn200ForVerticalQuadruplets()
        {
            //Arrange
            _gameBoard.Board[0, 0] = "1";
            _gameBoard.Board[1, 0] = "1";
            _gameBoard.Board[2, 0] = "1";
            _gameBoard.Board[3, 0] = "1";

            //Act
            var result = _gameService.ClearConnectedNumbers();

            //Assert
            Assert.That(result, Is.EqualTo(200));
        }

        [Test]
        public void ClearConnectedNumbers_ShouldReturn500ForHVerticalQuintuplets()
        {
            //Arrange
            _gameBoard.Board[0, 0] = "1";
            _gameBoard.Board[1, 0] = "1";
            _gameBoard.Board[2, 0] = "1";
            _gameBoard.Board[3, 0] = "1";
            _gameBoard.Board[4, 0] = "1";

            //Act
            var result = _gameService.ClearConnectedNumbers();

            //Assert
            Assert.That(result, Is.EqualTo(500));
        }

        [Test]
        public void ClearConnectedNumbers_ShouldClearDiagonalTriplets()
        {
            //Arrange
            _gameBoard.Board[0, 0] = "1";
            _gameBoard.Board[1, 1] = "1";
            _gameBoard.Board[2, 2] = "1";

            //Act
            _gameService.ClearConnectedNumbers();

            //Assert
            Assert.Multiple(() =>
            {
                Assert.That(_gameBoard.Board[0, 0], Is.EqualTo(null));
                Assert.That(_gameBoard.Board[1, 1], Is.EqualTo(null));
                Assert.That(_gameBoard.Board[2, 2], Is.EqualTo(null));
            });
        }

        [Test]
        public void ClearConnectedNumbers_ShouldClearDiagonalQuadruplets()
        {
            //Arrange
            _gameBoard.Board[0, 0] = "1";
            _gameBoard.Board[1, 1] = "1";
            _gameBoard.Board[2, 2] = "1";
            _gameBoard.Board[3, 3] = "1";

            //Act
            _gameService.ClearConnectedNumbers();

            //Assert
            Assert.Multiple(() =>
            {
                Assert.That(_gameBoard.Board[0, 0], Is.EqualTo(null));
                Assert.That(_gameBoard.Board[1, 1], Is.EqualTo(null));
                Assert.That(_gameBoard.Board[2, 2], Is.EqualTo(null));
                Assert.That(_gameBoard.Board[3, 3], Is.EqualTo(null));
            });
        }

        [Test]
        public void ClearConnectedNumbers_ShouldClearDiagonalQuintuplets()
        {
            //Arrange
            _gameBoard.Board[0, 0] = "1";
            _gameBoard.Board[1, 1] = "1";
            _gameBoard.Board[2, 2] = "1";
            _gameBoard.Board[3, 3] = "1";
            _gameBoard.Board[4, 4] = "1";

            //Act
            _gameService.ClearConnectedNumbers();

            //Assert
            Assert.Multiple(() =>
            {
                Assert.That(_gameBoard.Board[0, 0], Is.EqualTo(null));
                Assert.That(_gameBoard.Board[1, 1], Is.EqualTo(null));
                Assert.That(_gameBoard.Board[2, 2], Is.EqualTo(null));
                Assert.That(_gameBoard.Board[3, 3], Is.EqualTo(null));
                Assert.That(_gameBoard.Board[4, 4], Is.EqualTo(null));
            });
        }

        [Test]
        public void ClearConnectedNumbers_ShouldReturn100ForDiagonalTriplets()
        {
            //Arrange
            _gameBoard.Board[0, 0] = "1";
            _gameBoard.Board[1, 1] = "1";
            _gameBoard.Board[2, 2] = "1";

            //Act
            var result = _gameService.ClearConnectedNumbers();

            //Assert
            Assert.That(result, Is.EqualTo(100));
        }

        [Test]
        public void ClearConnectedNumbers_ShouldReturn200ForDiagonalQuadruplets()
        {
            //Arrange
            _gameBoard.Board[0, 0] = "1";
            _gameBoard.Board[1, 1] = "1";
            _gameBoard.Board[2, 2] = "1";
            _gameBoard.Board[3, 3] = "1";

            //Act
            var result = _gameService.ClearConnectedNumbers();

            //Assert
            Assert.That(result, Is.EqualTo(200));
        }

        [Test]
        public void ClearConnectedNumbers_ShouldReturn500ForHDiagonalQuintuplets()
        {
            //Arrange
            _gameBoard.Board[0, 0] = "1";
            _gameBoard.Board[1, 1] = "1";
            _gameBoard.Board[2, 2] = "1";
            _gameBoard.Board[3, 3] = "1";
            _gameBoard.Board[4, 4] = "1";

            //Act
            var result = _gameService.ClearConnectedNumbers();

            //Assert
            Assert.That(result, Is.EqualTo(500));
        }

        [Test]
        public void ClearConnectedNumbers_ShouldClearDiagonalAndReverseDiagonalTriplets()
        {
            //Arrange
            _gameBoard.Board[0, 2] = "1";
            _gameBoard.Board[1, 1] = "1";
            _gameBoard.Board[2, 0] = "1";
            _gameBoard.Board[1, 3] = "1";
            _gameBoard.Board[2, 4] = "1";

            //Act
            _gameService.ClearConnectedNumbers();

            //Assert
            Assert.Multiple(() =>
            {
                Assert.That(_gameBoard.Board[0, 2], Is.EqualTo(null));
                Assert.That(_gameBoard.Board[1, 1], Is.EqualTo(null));
                Assert.That(_gameBoard.Board[2, 0], Is.EqualTo(null));
                Assert.That(_gameBoard.Board[1, 3], Is.EqualTo(null));
                Assert.That(_gameBoard.Board[2, 4], Is.EqualTo(null));
            });
        }

        [Test]
        public void ClearConnectedNumbers_ShouldClearReverseDiagonalTriplets()
        {
            //Arrange
            _gameBoard.Board[0, 2] = "1";
            _gameBoard.Board[1, 1] = "1";
            _gameBoard.Board[2, 0] = "1";

            //Act
            _gameService.ClearConnectedNumbers();

            //Assert
            Assert.Multiple(() =>
            {
                Assert.That(_gameBoard.Board[0, 2], Is.EqualTo(null));
                Assert.That(_gameBoard.Board[1, 1], Is.EqualTo(null));
                Assert.That(_gameBoard.Board[2, 0], Is.EqualTo(null));
            });
        }

        [Test]
        public void ClearConnectedNumbers_ShouldClearReverseDiagonalQuadruplets()
        {
            //Arrange
            _gameBoard.Board[0, 3] = "1";
            _gameBoard.Board[1, 2] = "1";
            _gameBoard.Board[2, 1] = "1";
            _gameBoard.Board[3, 0] = "1";

            //Act
            _gameService.ClearConnectedNumbers();

            //Assert
            Assert.Multiple(() =>
            {
                Assert.That(_gameBoard.Board[0, 3], Is.EqualTo(null));
                Assert.That(_gameBoard.Board[1, 2], Is.EqualTo(null));
                Assert.That(_gameBoard.Board[2, 1], Is.EqualTo(null));
                Assert.That(_gameBoard.Board[3, 0], Is.EqualTo(null));
            });
        }

        [Test]
        public void ClearConnectedNumbers_ShouldClearReverseDiagonalQuintuplets()
        {
            //Arrange
            _gameBoard.Board[0, 4] = "1";
            _gameBoard.Board[1, 3] = "1";
            _gameBoard.Board[2, 2] = "1";
            _gameBoard.Board[3, 1] = "1";
            _gameBoard.Board[4, 0] = "1";

            //Act
            _gameService.ClearConnectedNumbers();

            //Assert
            Assert.Multiple(() =>
            {
                Assert.That(_gameBoard.Board[0, 4], Is.EqualTo(null));
                Assert.That(_gameBoard.Board[1, 3], Is.EqualTo(null));
                Assert.That(_gameBoard.Board[2, 2], Is.EqualTo(null));
                Assert.That(_gameBoard.Board[3, 1], Is.EqualTo(null));
                Assert.That(_gameBoard.Board[4, 0], Is.EqualTo(null));
            });
        }

        [Test]
        public void ClearConnectedNumbers_ShouldReturn100ForReverseDiagonalTriplets()
        {
            //Arrange
            _gameBoard.Board[0, 2] = "1";
            _gameBoard.Board[1, 1] = "1";
            _gameBoard.Board[2, 0] = "1";

            //Act
            var result = _gameService.ClearConnectedNumbers();

            //Assert
            Assert.That(result, Is.EqualTo(100));
        }

        [Test]
        public void ClearConnectedNumbers_ShouldReturn200ForReverseDiagonalQuadruplets()
        {
            //Arrange
            _gameBoard.Board[0, 3] = "1";
            _gameBoard.Board[1, 2] = "1";
            _gameBoard.Board[2, 1] = "1";
            _gameBoard.Board[3, 0] = "1";

            //Act
            var result = _gameService.ClearConnectedNumbers();

            //Assert
            Assert.That(result, Is.EqualTo(200));
        }

        [Test]
        public void ClearConnectedNumbers_ShouldReturn500ForReverseDiagonalQuintuplets()
        {
            //Arrange
            _gameBoard.Board[0, 4] = "1";
            _gameBoard.Board[1, 3] = "1";
            _gameBoard.Board[2, 2] = "1";
            _gameBoard.Board[3, 1] = "1";
            _gameBoard.Board[4, 0] = "1";

            //Act
            var result = _gameService.ClearConnectedNumbers();

            //Assert
            Assert.That(result, Is.EqualTo(500));
        }
    }
}
