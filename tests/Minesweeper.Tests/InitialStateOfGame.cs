using System;
using Xunit;

namespace Minesweeper.Tests
{
    public class InitialStateOfGame
    {
        [Fact]
        public void CheckTheWidthAndHeightAreCorrectlyCalculated()
        {
            var initialState = new int?[3, 4];
            var game = new Game(initialState);

            Assert.Equal(3, game.NumberOfColumns);
            Assert.Equal(4, game.NumberOfRows);
        }

        [Fact]
        public void CheckTheMineCountIsCorrect()
        {
            var initialState = new int?[3, 4];
            initialState[1, 2] = 8;
            initialState[2, 3] = 1;

            var game = new Game(initialState);

            Assert.Equal("", game.CellContents(0, 0));
            Assert.Equal("8", game.CellContents(1, 2));
            Assert.Equal("1", game.CellContents(2, 3));
        }
    }
}
