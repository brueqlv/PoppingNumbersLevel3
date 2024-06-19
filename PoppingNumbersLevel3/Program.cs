using PoppingNumbersLevel3.Helpers;
using PoppingNumbersLevel3.Models;
using PoppingNumbersLevel3.Services;

namespace PoppingNumbersLevel3
{
    internal class Program
    {
        static void Main(string[] args)
        {
            const int minDeskSize = 5;
            const int maxDeskSize = 25;
            const int minGameNumber = 0;
            const int maxGameNumber = 9;
            var score = 0;

            var userName = UserInputHelper.GetValidUserName();

            var deskWidth = UserInputHelper.GetValidUserInputNumber("Enter board desk width", maxDeskSize, minDeskSize);
            var deskHeight = UserInputHelper.GetValidUserInputNumber("Enter board desk height", maxDeskSize, minDeskSize);

            var gameBoard = new GameBoard(deskWidth, deskHeight);
            var gameService = new GameService(gameBoard);

            var gameNumbersFrom = UserInputHelper.GetValidUserInputNumber("Enter min game number", maxGameNumber, minGameNumber);
            var gameNumbersTo = UserInputHelper.GetValidUserInputNumber("Enter max game number", maxGameNumber, minGameNumber);

            var appearancePredictor = UserInputHelper.GetValidUserInputBoolean("Do you want to use number appearance predictor?");

            while (true)
            {
                Console.Clear();
                Console.WriteLine($"{userName} score - {score}");

                if (appearancePredictor)
                {
                    gameService.ComputerTurn(gameNumbersFrom, gameNumbersTo, appearancePredictor);
                }

                gameService.PrintBoard();

                gameService.PlayerTurn(gameNumbersFrom, gameNumbersTo);

                if (gameService.IsGameOver())
                {
                    break;
                }

                if (appearancePredictor)
                {
                    gameService.ReplaceStarsWithRandomNumbers(gameNumbersFrom, gameNumbersTo);
                }
                else
                {
                    gameService.ComputerTurn(gameNumbersFrom, gameNumbersTo, appearancePredictor);
                }

                score += gameService.ClearConnectedNumbers();

                if (gameService.IsGameOver())
                {
                    break;
                }
            }

            Console.WriteLine("Game Over! No more spaces left.");
            Console.ReadLine();
        }
    }
}
