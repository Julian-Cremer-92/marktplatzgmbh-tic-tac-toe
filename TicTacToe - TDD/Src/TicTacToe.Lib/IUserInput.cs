namespace TicTacToeLib
{
    public interface IUserInput
    {
        int GetField(Player player);

        int GetField(Models.Player player, int dimension);
    }
}
