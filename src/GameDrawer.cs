using System.Drawing;

namespace Minesweeper
{
    class GameDrawer
    {
        private const int CellSize = 50;

        private int NumberOfColumns = 8;
        private int NumberOfRows = 10;

        private Font _symbolFont = new System.Drawing.Font("Segoe UI", 24F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
        private Game _game;

        public GameDrawer(Game game)
        {
            _game = game;
        }

        public void Draw(Graphics g)
        {
            g.TranslateTransform(100, 100);

            g.DrawRectangle(Pens.Black, 0, 0, NumberOfColumns * CellSize, NumberOfRows * CellSize);

            for (int column = 1; column < NumberOfColumns; column++)
            {
                g.DrawLine(Pens.Black, column * CellSize, 0, column * CellSize, NumberOfRows * CellSize);
            }

            for (int row = 1; row < NumberOfRows; row++)
            {
                g.DrawLine(Pens.Black, 0, row * CellSize, NumberOfColumns * CellSize, row * CellSize);
            }

            for (int column = 0; column < NumberOfColumns; column++)
            {
                for (int row = 0; row < NumberOfRows; row++)
                {
                    DrawSymbol(g, _game.CellContents(column, row), column, row);
                }
            }
        }

        private void DrawSymbol(Graphics g, string symbol, int column, int row)
        {
            var x = column * CellSize;
            var y = row * CellSize;

            var symbolSize = g.MeasureString(symbol, _symbolFont, new Point(x, y), StringFormat.GenericDefault);
            g.DrawString(symbol, _symbolFont, Brushes.Black, x + ((CellSize - symbolSize.Width) / 2), y + ((CellSize - symbolSize.Height) / 2));
        }
    }
}
