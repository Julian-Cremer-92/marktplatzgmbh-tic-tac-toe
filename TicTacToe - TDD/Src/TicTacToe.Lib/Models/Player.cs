namespace TicTacToeLib.Models
{
    public class Player
    {
        public int PlayerNumber { get; set; }
        public string PlayerSymbol { get; set; }

        public Player(int playerNumber)
        {
            this.PlayerNumber = playerNumber;
        }
    }
}
