namespace PoppingNumbersLevel3.Models
{
    public class GameNumbers(int from, int to)
    {
        public int From { get; private set; } = from;
        public int To { get; private set; } = to;
    }
}
