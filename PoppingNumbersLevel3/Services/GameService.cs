using PoppingNumbersLevel3.Helpers;
using PoppingNumbersLevel3.Models;
using System;

namespace PoppingNumbersLevel3.Services
{
    public class GameService(GameBoard gameBoard)
    {
        private readonly Random _gameRandom = new();
        private readonly Dictionary<string, ConsoleColor> _numberColors = new()
        {
            {"*", ConsoleColor.DarkGray},
            {"0", ConsoleColor.DarkMagenta},
            {"1", ConsoleColor.Blue},
            {"2", ConsoleColor.Cyan},
            {"3", ConsoleColor.DarkBlue},
            {"4", ConsoleColor.Magenta},
            {"5", ConsoleColor.Gray},
            {"6", ConsoleColor.White},
            {"7", ConsoleColor.DarkRed},
            {"8", ConsoleColor.DarkYellow},
            {"9", ConsoleColor.Yellow},
        };
        private readonly Dictionary<int, int> _pointsPerRowLength = new()
        {
            { 3, 100 },
            { 4, 200 },
            { 5, 500 }
        };

        public void PrintBoard()
        {
            for (var i = 0; i < gameBoard.Height; i++)
            {
                for (var j = 0; j < gameBoard.Width; j++)
                {
                    var number = gameBoard.Board[i, j];

                    if (number == null)
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

        public void PlayerTurn(int minGameNumber, int maxGameNumber)
        {
            while (true)
            {
                var number = UserInputHelper.GetValidUserInputNumber("Enter a number", maxGameNumber, minGameNumber).ToString();
                var row = UserInputHelper.GetValidUserInputNumber("Enter row", gameBoard.Height);
                var col = UserInputHelper.GetValidUserInputNumber("Enter col", gameBoard.Width);

                if (gameBoard.Board[row - 1, col - 1] == null || gameBoard.Board[row - 1, col - 1] == "*")
                {
                    gameBoard.Board[row - 1, col - 1] = number;
                    break;
                }

                Console.WriteLine("Field is already occupied, try again.");
            }
        }

        public void ComputerTurn(int minGameNumber, int maxGameNumber, bool useAppearancePredictor)
        {
            var numbersPlaced = 0;

            while (numbersPlaced < 3 || IsGameOver())
            {
                var row = _gameRandom.Next(gameBoard.Height);
                var col = _gameRandom.Next(gameBoard.Width);

                if (gameBoard.Board[row, col] != null) continue;

                var placeTaker = "*";

                if (!useAppearancePredictor)
                {
                    placeTaker = _gameRandom.Next(minGameNumber, maxGameNumber + 1).ToString();
                }

                gameBoard.Board[row, col] = placeTaker;
                numbersPlaced++;
            }
        }

        public void ReplaceStarsWithRandomNumbers(int minGameNumber, int maxGameNumber)
        {
            for (var i = 0; i < gameBoard.Height; i++)
            {
                for (var j = 0; j < gameBoard.Width; j++)
                {
                    if (gameBoard.Board[i, j] == "*")
                    {
                        gameBoard.Board[i, j] = _gameRandom.Next(minGameNumber, maxGameNumber + 1).ToString();
                    }
                }
            }
        }

        public bool IsGameOver()
        {
            for (var i = 0; i < gameBoard.Height; i++)
            {
                for (var j = 0; j < gameBoard.Width; j++)
                {
                    if (gameBoard.Board[i, j] == null)
                    {
                        return false;
                    }
                }
            }
            return true;
        }

        public int ClearConnectedNumbers()
        {
            var toClear = new bool[gameBoard.Height, gameBoard.Width];

            var points = 0;

            points += CheckConnections(toClear, 0, 1);  // Horizontal
            points += CheckConnections(toClear, 1, 0);  // Vertical
            points += CheckConnections(toClear, 1, 1);  // Diagonal
            points += CheckConnections(toClear, 1, -1); // Reverse Diagonal
            ClearMarkedCells(toClear);

            return points;
        }

        private int CheckConnections(bool[,] toClear, int rowIncrement, int colIncrement)
        {
            var height = gameBoard.Height;
            var width = gameBoard.Width;

            var timesMarkedCells = 0;

            for (var i = 0; i < height; i++)
            {
                for (var j = 0; j < width; j++)
                {
                    if (MarkConnectedCells(toClear, i, j, rowIncrement, colIncrement))
                    {
                        timesMarkedCells++;
                    }
                }
            }

            return timesMarkedCells == 0 ? timesMarkedCells : _pointsPerRowLength[timesMarkedCells + 2];
        }

        private bool MarkConnectedCells(bool[,] toClear, int startRow, int startCol, int rowIncrement, int colIncrement)
        {
            var current = gameBoard.Board[startRow, startCol];
            if (current == null) return false;

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

                return true;
            }

            return false;
        }

        private bool IsValidPosition(int row, int col)
        {
            return row >= 0 && row < gameBoard.Height && col >= 0 && col < gameBoard.Width;
        }

        private void ClearMarkedCells(bool[,] toClear)
        {
            for (var i = 0; i < gameBoard.Height; i++)
            {
                for (var j = 0; j < gameBoard.Width; j++)
                {
                    if (toClear[i, j])
                    {
                        gameBoard.Board[i, j] = null;
                    }
                }
            }
        }
    }
}
