using TicTacToeLib;

namespace TicTacToe.Console.GameStates
{
    public class GameStateExit : GameStateBase, IGameState
    {
        public IGameState Execute(GameBoard board, bool simulation = false)
        {
            return null;
        }
    }
}
