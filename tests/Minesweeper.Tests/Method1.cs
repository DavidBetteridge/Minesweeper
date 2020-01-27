using Xunit;

namespace Minesweeper.Tests
{

    public class Method1
    {

        [Fact]
        public void SolveFirstCell()
        {
            var initialState = new int?[8, 2];
            initialState[0, 0] = 0;
            initialState[4, 0] = 1;
            initialState[6, 0] = 1;
            initialState[7, 0] = 1;
            initialState[5, 1] = 3;

            var game = new Game(initialState);
            var actualSolution = game.Solve();

            Assert.Equal("No mines left to place.", actualSolution.Description);
            Assert.Equal(0, actualSolution.CellOfInterest.Column);
            Assert.Equal(0, actualSolution.CellOfInterest.Row);

            Assert.Equal(3, actualSolution.SolvedCells.Length);
            Assert.Contains(actualSolution.SolvedCells, c => c.Column == 0 && c.Row == 1);
            Assert.Contains(actualSolution.SolvedCells, c => c.Column == 1 && c.Row == 0);
            Assert.Contains(actualSolution.SolvedCells, c => c.Column == 1 && c.Row == 1);

            Assert.Equal("x", game.CellContents(0, 1));
            Assert.Equal("x", game.CellContents(1, 0));
            Assert.Equal("x", game.CellContents(1, 1));
        }
    }
}
