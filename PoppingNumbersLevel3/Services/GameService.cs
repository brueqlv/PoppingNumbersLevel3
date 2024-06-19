using PoppingNumbersLevel3.Models;

namespace PoppingNumbersLevel3.Services
{
    public class GameService(GameBoard gameBoard)
    {
        private readonly Random _gameRandom = new();

        private readonly Dictionary<int, ConsoleColor> _numberColors = new()
        {
            {0, ConsoleColor.Black},
            {1, ConsoleColor.Blue},
            {2, ConsoleColor.Cyan},
            {3, ConsoleColor.DarkBlue},
            {4, ConsoleColor.Magenta},
            {5, ConsoleColor.Gray},
            {6, ConsoleColor.White},
            {7, ConsoleColor.DarkRed},
            {8, ConsoleColor.DarkYellow},
            {9, ConsoleColor.Yellow},
        };

        public void PrintBoard()
        {
            for (var i = 0; i < gameBoard.Height; i++)
            {
                for (var j = 0; j < gameBoard.Width; j++)
                {
                    var number = gameBoard.Board[i, j];
                    if (number == 0)
                    {
                        Console.Write("   ");
                    }
                    else
                    {
                        Console.ForegroundColor = _numberColors[number];
                        Console.Write(" " + number + " ");
                        Console.ResetColor();
                    }

                    if (j < gameBoard.Width - 1)
                    {
                        Console.Write("|");
                    }
                }

                Console.WriteLine();

                if (i < gameBoard.Height - 1)
                {
                    for (var j = 0; j < gameBoard.Width; j++)
                    {
                        Console.Write("---");
                        if (j < gameBoard.Width - 1)
                        {
                            Console.Write("|");
                        }
                    }
                    Console.WriteLine();
                }
            }
        }


        public void PlayerTurn(int number, int row, int col)
        {
            while (true)
            {
                if (gameBoard.Board[row - 1, col - 1] == 0)
                {
                    gameBoard.Board[row - 1, col - 1] = number;
                    break;
                }

                Console.WriteLine("Field is already occupied, try again.");
            }
        }

        public void ComputerTurn(int minGameNumber, int maxGameNumber)
        {
            var numbersPlaced = 0;

            while (numbersPlaced < 3)
            {
                var row = _gameRandom.Next(gameBoard.Height);
                var col = _gameRandom.Next(gameBoard.Width);

                if (gameBoard.Board[row, col] == 0)
                {
                    gameBoard.Board[row, col] = _gameRandom.Next(minGameNumber, maxGameNumber + 1);
                    numbersPlaced++;
                }
            }
        }

        public bool IsGameOver()
        {
            for (var i = 0; i < gameBoard.Height; i++)
            {
                for (var j = 0; j < gameBoard.Width; j++)
                {
                    if (gameBoard.Board[i, j] == 0)
                    {
                        return false;
                    }
                }
            }

            Console.WriteLine("Game Over! No more spaces left.");
            return true;
        }

        public int ClearConnectedNumbers()
        {
            var toClear = new bool[gameBoard.Height, gameBoard.Width];

            CheckConnections(toClear, 0, 1);  // Horizontal
            CheckConnections(toClear, 1, 0);  // Vertical
            CheckConnections(toClear, 1, 1);  // Diagonal
            CheckConnections(toClear, 1, -1); // Reverse Diagonal

            return ClearMarkedCells(toClear);
        }

        private void CheckConnections(bool[,] toClear, int rowIncrement, int colIncrement)
        {
            var height = gameBoard.Height;
            var width = gameBoard.Width;

            for (var i = 0; i < height; i++)
            {
                for (var j = 0; j < width; j++)
                {
                    MarkConnectedCells(toClear, i, j, rowIncrement, colIncrement);
                }
            }
        }

        private void MarkConnectedCells(bool[,] toClear, int startRow, int startCol, int rowIncrement, int colIncrement)
        {
            var current = gameBoard.Board[startRow, startCol];
            if (current == 0) return;

            var count = 1;
            var row = startRow + rowIncrement;
            var col = startCol + colIncrement;

            while (IsValidPosition(row, col) && gameBoard.Board[row, col] == current)
            {
                count++;
                row += rowIncrement;
                col += colIncrement;
            }

            if (count >= 3)
            {
                for (var k = 0; k < count; k++)
                {
                    toClear[startRow + k * rowIncrement, startCol + k * colIncrement] = true;
                }
            }
        }

        private bool IsValidPosition(int row, int col)
        {
            return row >= 0 && row < gameBoard.Height && col >= 0 && col < gameBoard.Width;
        }

        private int ClearMarkedCells(bool[,] toClear)
        {
            var clearedCells = 0;
            for (var i = 0; i < gameBoard.Height; i++)
            {
                for (var j = 0; j < gameBoard.Width; j++)
                {
                    if (toClear[i, j])
                    {
                        gameBoard.Board[i, j] = 0;
                        clearedCells++;
                    }
                }
            }

            return clearedCells;
        }
    }
}
