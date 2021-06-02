using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace TicTacToeLib.Tests
{
    [TestClass]
    public class GameBoardTests
    {
        private GameBoard board = null;

        [TestInitialize]
        public void TestInitialize()
        {
            this.board = new GameBoard(null);
        }

        [TestMethod]
        [TestCategory("GameBoard")]
        [ExpectedException(typeof(ArgumentOutOfRangeException))]
        public void Fail_If_Greater_Than_Max()
        {
            board.Set(9);
        }

        [TestMethod]
        [TestCategory("GameBoard")]
        [ExpectedException(typeof(ArgumentOutOfRangeException))]
        public void Fail_If_Less_Than_Min()
        {
            board.Set(-1);
        }

        [TestMethod]
        [TestCategory("GameBoard")]
        [ExpectedException(typeof(ArgumentException))]
        public void Set_Fails_If_Occupied()
        {
            board.Set(4);
            board.Set(4);
        }

        [TestMethod]
        [TestCategory("GameBoard")]
        public void Player_Switch_After_Move()
        {
            // It is assumed that Player 1 has the first move
            var activePlayerBeforeMove  = board.ActivePlayer;

            // After making a move, it is assumed that the game automatically switched the active player to Player 2
            board.Set(4);

            var activePlayerAfterMove = board.ActivePlayer;

            // However represented, the active players should differ by now :)
            Assert.AreNotEqual(activePlayerBeforeMove, activePlayerAfterMove);
        }

        [TestMethod]
        [TestCategory("GameBoard")]
        public void Is_Occupied()
        {
            board.Set(4);

            var actual = board.IsOccupied(4);

            Assert.IsTrue(actual);
        }

        [TestMethod]
        [TestCategory("GameBoard")]
        public void Check_Winner_Vertical()
        {
            // Gameboard representation
            // 8 7 6 
            // 5 4 3
            // 2 1 0

            board.Set(8);
            board.Set(7);
            board.Set(5);
            board.Set(4);
            board.Set(2);
            board.Set(0);

            // X O #
            // X O #
            // X # O

            bool actual = board.CheckWinner();

            Assert.IsTrue(actual);
        }

        [TestMethod]
        [TestCategory("GameBoard")]
        public void Check_Winner_Horizontal()
        {
            // Gameboard representation
            // 8 7 6 
            // 5 4 3
            // 2 1 0

            board.Set(8);
            board.Set(5);
            board.Set(7);
            board.Set(4);
            board.Set(6);
            board.Set(0);

            // X X X
            // O O #
            // # # O

            bool actual = board.CheckWinner();

            Assert.IsTrue(actual);
        }

        [TestMethod]
        [TestCategory("GameBoard")]
        public void Check_Winner_Diagonal()
        {
            // Gameboard representation
            // 8 7 6 
            // 5 4 3
            // 2 1 0

            board.Set(8);
            board.Set(5);
            board.Set(4);
            board.Set(2);
            board.Set(0);
            board.Set(1);

            // X # #
            // O X #
            // O O X

            bool actual = board.CheckWinner();

            Assert.IsTrue(actual);
        }

        [TestMethod]
        [TestCategory("GameBoard")]
        public void Display_Board()
        {
            // Gameboard representation
            // 8 7 6 
            // 5 4 3
            // 2 1 0

            board.Set(8);
            board.Set(5);
            board.Set(4);
            board.Set(2);
            board.Set(0);
            board.Set(1);

            // X # #
            // O X #
            // O O X

            var actual = board.ToString();
            var expected = "X##\r\nOX#\r\nOOX\r\n";

            Assert.AreEqual(actual, expected);
        }
    }
}
