using TicTacToe.Console.GameStates;
using TicTacToeLib;

namespace TicTacToe.Console
{
    static class Program
    {
        static void Main()
        {
            var board = new GameBoard(new UserInput());

            IGameState currentState = new GameStateRunning();

            while (currentState != null)
            {
                currentState = currentState.Execute(board);
            }
        }
    }
}
