using TicTacToeLib;

namespace TicTacToe.Console.GameStates
{
    public class GameStateRemis : GameStateBase, IGameState
    {
        public IGameState Execute(GameBoard board, bool simulation = false)
        {
            // Display the board one last time
            if (!simulation)
            {
                System.Console.Clear();
                System.Console.WriteLine(board);

                System.Console.WriteLine("Remis!");
            }

            return new GameStateExit() { LastGameState = this };
        }
    }
}
