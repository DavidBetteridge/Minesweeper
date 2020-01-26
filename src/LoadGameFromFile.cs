using System.Threading.Tasks;

namespace Minesweeper
{
    public class LoadGameFromFile
    {
        public async Task<Game> Load(string filename)
        {
            var lines = await System.IO.File.ReadAllLinesAsync(filename).ConfigureAwait(false);
            var numberOfColumns = lines[0].Length;
            var numberOfRows = lines.Length;

            var initialState = new int?[numberOfColumns, numberOfRows];

            for (int column = 0; column < numberOfColumns; column++)
            {
                for (int row = 0; row < numberOfRows; row++)
                {
                    var cell = lines[row][column];
                    if (cell != ' ')
                    {
                        initialState[column, row] = int.Parse(cell.ToString());
                    }
                }
            }

            return new Game(initialState);
        }
    }
}
