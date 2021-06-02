using TicTacToeLib;

namespace TicTacToe.Console.GameStates
{
    public interface IGameState
    {
        IGameState LastGameState { get; set; }

        IGameState Execute(GameBoard board, bool simulation = false);
    }
}
