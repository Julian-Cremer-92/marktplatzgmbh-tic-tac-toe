using TicTacToeLib;

namespace TicTacToe.Console.GameStates
{
    public class GameStateWon : GameStateBase, IGameState
    {
        public IGameState Execute(GameBoard board, bool simulation = false)
        {
            if (!simulation)
            {
                // Display the board one last time
                System.Console.Clear();
                System.Console.WriteLine(board);

                System.Console.WriteLine("Won!");
            }
            return new GameStateExit() { LastGameState = this };
        }
    }
}
