using System;
using TicTacToeLib;

namespace TicTacToe.Console.GameStates
{
    public class GameStateRunning : GameStateBase, IGameState
    {
        public static int moves = 0;

        public IGameState Execute(GameBoard board, bool simulation = false)
        {
            if (!simulation)
            {
                // Display the board
                System.Console.Clear();
                System.Console.WriteLine(board);
            }

            int field = board.GetField();

            try
            {
                // Inverse input for internal representation
                board.Set(9 - field);
                moves++;

                if (board.CheckWinner())
                {
                    return new GameStateWon() { LastGameState = this };
                }
                else if (moves == 9)
                {
                    // Only count as remis if not won with the last move
                    return new GameStateRemis() { LastGameState = this };
                }
            }
            catch (Exception ex)
            {
                if (!simulation)
                {
                    System.Console.WriteLine(ex.Message);
                    System.Console.WriteLine("Press any key to continue.");
                    System.Console.ReadLine();
                }
            }

            return this;
        }
    }
}
