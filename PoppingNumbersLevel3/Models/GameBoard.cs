namespace PoppingNumbersLevel3.Models
{
    public class GameBoard
    {
        public int Width { get; set; }
        public int Height { get; set; }
        public string[,] Board { get; set; }

        public GameBoard(int width, int height)
        {
            Width = width;
            Height = height;
            Board = new string[Height, Width];
        }
    }
}
