
namespace PoppingNumbersLevel3.Helpers
{
    public class UserInputHelper
    {
        public static int GetValidUserInputNumber(string message, int max, int min = 0)
        {
            while (true)
            {
                Console.Write($"{message} ({min}-{max}): ");
                var numberInput = Console.ReadLine();

                if (int.TryParse(numberInput, out var result))
                {
                    if (result >= min && result <= max)
                    {
                        return result;
                    }
                }

                Console.WriteLine("Invalid input, try again.");
            }
        }

        public static object GetValidUserName()
        {
            while (true)
            {
                Console.WriteLine("Please enter your name.");
                var userName = Console.ReadLine();

                if (!string.IsNullOrWhiteSpace(userName))
                {
                    return userName;
                }
            }
        }

        public static bool GetValidUserInputBoolean(string message)
        {
            while (true)
            {
                Console.WriteLine($"{message} (y/n)");
                var userInput = Console.ReadLine().ToLower().Trim();

                switch (userInput)
                {
                    case "y":
                        return true;
                    case "n":
                        return false;
                    default:
                        Console.WriteLine("Invalid Input, try again.");
                        break;
                }
            }
        }
    }
}
