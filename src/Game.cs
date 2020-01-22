using System.Collections.Generic;

namespace Minesweeper
{
    public class Game
    {
        public string CellContents(int column, int row)
        {
            return row + column.ToString();
        }
    }
}
