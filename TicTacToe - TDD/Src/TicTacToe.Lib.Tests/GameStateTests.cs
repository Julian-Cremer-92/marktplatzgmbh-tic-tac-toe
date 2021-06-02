using Microsoft.VisualStudio.TestTools.UnitTesting;
using TicTacToe.Console.GameStates;

namespace TicTacToeLib.Tests
{
    [TestClass]
    public class GameStateTests
    {
        private int[] movesPlayerOneWins = { 1, 2, 5, 6, 9 };//9 statt 7 :-, es kann aber auch ein zusätzlicher Schritt angehängt werden)
        private int[] movesRemis = { 1, 2, 4, 7, 8, 5, 9, 6, 3 };

       // private static int move = 0;

        [TestMethod]
        [TestCategory("GameState")]
        public void GameState_Running()
        {
            // Arrange           
            var userInput = new FakeUserInput()
            {                
                 FieldsPlayer   =  ( new int [] { 1 })
            };

            // Act
            var expectedState = typeof(GameStateRunning);
            var actualState = new GameStateRunning().Execute(new GameBoard(userInput), simulation: true);

            // Assert
            Assert.IsInstanceOfType(actualState, expectedState);
        }

        [TestMethod]
        [TestCategory("GameState")]
        public void GameState_Won()
        {
            // Arrange            
            var userInput = new FakeUserInput
            {                
                FieldsPlayer = movesPlayerOneWins
            };

            // Act
            var expectedState = typeof(GameStateWon);
            var actualState = this.SimulateGame(new GameBoard(userInput));

            // Assert
            Assert.IsInstanceOfType(actualState, expectedState);
        }

        [TestMethod]
        [TestCategory("GameState")]
        public void GameState_Remis()
        {
            // Arrange            
            var userInput = new FakeUserInput()
            {                
                FieldsPlayer = movesRemis
            };

            // Act
            var expectedState = typeof(GameStateRemis);
            var actualState = this.SimulateGame(new GameBoard(userInput));

            // Assert
            Assert.IsInstanceOfType(actualState, expectedState);
        }

        /// <summary>
        /// Simulates the gameloop.
        /// </summary>
        /// <param name="board">Board on which to play.</param>
        /// <returns>Returns last game state.</returns>
        private IGameState SimulateGame(GameBoard board)
        {
            IGameState currentState = new GameStateRunning();
            IGameState lastState = null;

            while (currentState != null)
            {
                currentState = currentState.Execute(board, simulation: true);

                if (currentState != null)
                    lastState = currentState.LastGameState;
            }

            return lastState;
        }
    }
}
