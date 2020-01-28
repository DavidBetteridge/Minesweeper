using System;
using System.Collections.Generic;
using System.Linq;

namespace Minesweeper
{
    public class Game
    {
        public int NumberOfColumns { get; private set; }
        public int NumberOfRows { get; private set; }

        private const int UnknownCell = -1;
        private const int Mine = -2;
        private const int Empty = -3;

        private int[,] cells;

        public Game(char[,] initialState)
        {
            NumberOfColumns = initialState.GetUpperBound(0) + 1;
            NumberOfRows = initialState.GetUpperBound(1) + 1;

            cells = new int[NumberOfColumns, NumberOfRows];

            for (int column = 0; column < NumberOfColumns; column++)
            {
                for (int row = 0; row < NumberOfRows; row++)
                {
                    cells[column, row] = initialState[column, row] switch
                    {
                        '!' => Mine,
                        'x' => Empty,
                        ' ' => UnknownCell,
                        '0' => 0,
                        '1' => 1,
                        '2' => 2,
                        '3' => 3,
                        '4' => 4,
                        '5' => 5,
                        '6' => 6,
                        '7' => 7,
                        '8' => 8,
                        _ => throw new Exception($"Cell {column},{row} contains {initialState[column, row]} which isn't valid."),
                    };
                }
            }

        }

        public string CellContents(int column, int row)
        {
            return cells[column, row] switch
            {
                UnknownCell => "",
                Mine => "!",
                Empty => "x",
                _ => cells[column, row].ToString(),
            };
        }

        public Solution Solve()
        {
            var solution = Method1();

            if (solution is null)
                solution = Method2();

            return solution;
        }

        private Solution Method1()
        {
            for (int column = 0; column < NumberOfColumns; column++)
            {
                for (int row = 0; row < NumberOfRows; row++)
                {
                    if (TryGetMineCountForCell(column, row, out var mineCount))
                    {
                        var neighboursWithMines = NeighboursWithMines(column, row);
                        var neighboursWhichAreUnknown = NeighboursWhichAreUnknown(column, row);
                        var minesLeftToPlace = mineCount - (neighboursWithMines.Count());

                        if (minesLeftToPlace == 0 && neighboursWhichAreUnknown.Any())
                        {
                            foreach (var cell in neighboursWhichAreUnknown)
                            {
                                cells[cell.Column, cell.Row] = Empty;
                            }

                            return new Solution
                            {
                                CellOfInterest = new CellLocation(column, row),
                                SolvedCells = neighboursWhichAreUnknown,
                                Description = "No mines left to place.",
                            };
                        }
                    }
                }
            }

            return null;
        }


        private Solution Method2()
        {
            for (int column = 0; column < NumberOfColumns; column++)
            {
                for (int row = 0; row < NumberOfRows; row++)
                {
                    if (TryGetMineCountForCell(column, row, out var mineCount))
                    {
                        var neighboursWithMines = NeighboursWithMines(column, row);
                        var neighboursWhichAreUnknown = NeighboursWhichAreUnknown(column, row);
                        var minesLeftToPlace = mineCount - (neighboursWithMines.Count());

                        if (minesLeftToPlace > 0 && minesLeftToPlace == neighboursWhichAreUnknown.Count())
                        {
                            foreach (var cell in neighboursWhichAreUnknown)
                            {
                                cells[cell.Column, cell.Row] = Mine;
                            }

                            return new Solution
                            {
                                CellOfInterest = new CellLocation(column, row),
                                SolvedCells = neighboursWhichAreUnknown,
                                Description = "All unknown cells must contain mines.",
                            };
                        }
                    }
                }
            }

            return null;
        }

        internal CellLocation[] NeighboursWithMines(int column, int row)
        {
            var neighbours = GetNeighbours(column, row);
            return neighbours.Where(cell => cells[cell.Column, cell.Row] == Mine).ToArray();
        }

        internal CellLocation[] NeighboursWhichAreUnknown(int column, int row)
        {
            var neighbours = GetNeighbours(column, row);
            return neighbours.Where(cell => cells[cell.Column, cell.Row] == UnknownCell).ToArray();
        }

        private List<CellLocation> GetNeighbours(int column, int row)
        {
            var neighbours = new List<CellLocation>();

            /*
             *   123
             *   4 5
             *   678
             */

            if (column > 0 && row > 0) neighbours.Add(new CellLocation(column - 1, row - 1));
            if (row > 0) neighbours.Add(new CellLocation(column, row - 1));
            if (column < (NumberOfColumns - 1) && row > 0) neighbours.Add(new CellLocation(column + 1, row - 1));

            if (column > 0) neighbours.Add(new CellLocation(column - 1, row));
            if (column < (NumberOfColumns - 1)) neighbours.Add(new CellLocation(column + 1, row));

            if (column > 0 && row < (NumberOfRows - 1)) neighbours.Add(new CellLocation(column - 1, row + 1));
            if (row < (NumberOfRows - 1)) neighbours.Add(new CellLocation(column, row + 1));
            if (column < (NumberOfColumns - 1) && row < (NumberOfRows - 1)) neighbours.Add(new CellLocation(column + 1, row + 1));

            return neighbours;
        }

        internal bool TryGetMineCountForCell(int column, int row, out int mineCount)
        {
            if (cells[column, row] >= 0)
            {
                mineCount = cells[column, row];
                return true;
            }
            else
            {
                mineCount = int.MinValue;
                return false;
            }
        }
    }
}
