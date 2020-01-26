namespace Minesweeper
{
    public class Game
    {
        public int NumberOfColumns { get; private set; }
        public int NumberOfRows { get; private set; }

        private const int UnknownCell = -1;

        private int[,] cells;

        public Game(int?[,] initialState)
        {
            NumberOfColumns = initialState.GetUpperBound(0) + 1;
            NumberOfRows = initialState.GetUpperBound(1) + 1;

            cells = new int[NumberOfColumns, NumberOfRows];

            for (int column = 0; column < NumberOfColumns; column++)
            {
                for (int row = 0; row < NumberOfRows; row++)
                {
                    cells[column, row] = initialState[column, row] ?? UnknownCell;
                }
            }

        }

        public string CellContents(int column, int row)
        {
            return cells[column, row] switch
            {
                UnknownCell => "",
                _ => cells[column, row].ToString(),
            };
        }
    }
}
