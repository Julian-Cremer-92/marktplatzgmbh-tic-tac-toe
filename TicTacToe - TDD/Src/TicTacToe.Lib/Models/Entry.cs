using TicTacToeLib.Models.Enums;

namespace TicTacToeLib.Models
{
    public class Entry
    {
        public Player Player { get; private set; }
        public Position Position { get; set; }
        
        public int SumRight { get; set; }
        public int SumDiagonal { get; set; }
        public int SumDown { get; set; }

        public Entry(Player player, int posX, int posY)
        {
            this.Player = player;
            this.Position = new Position()
            {
                PosX = posX,
                PosY = posY
            };
        }

        /// <summary>
        /// Returns if Entry has an neighbour, according to its given direction that belongs to the player
        /// </summary>
        /// <returns>bool</returns>
        public bool HasPlayersNeighbour(Direction direction)
        {
            return true;
        }
        
        
    }
}
