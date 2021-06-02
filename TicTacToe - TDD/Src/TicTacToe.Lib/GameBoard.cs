using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TicTacToeLib.Models;
using TicTacToeLib.Models.Enums;

namespace TicTacToeLib
{
    public enum Player
    {
        Player1= 0,
        Player2 = 1
    };

    public class GameBoard
    {
        private int board = 0;

        private IUserInput userInput = null;

        public Models.Player CurrentPlayer
        {
            get;
            set;
        } = new Models.Player(0);

        // The methods necessary for capturing user input is injected as 
        // an interface so that it can be replaced by a stub for better
        // unit testing the GameState.
        public GameBoard(IUserInput userInput, int dimension, int playerCount)
        {
            this.Field = new PlayableField(dimension);
            this.userInput = userInput;
            this.PlayerCount = playerCount;
            this.Players = new List<Models.Player>();
            for (int i = 0; i < playerCount; i++)
            {
                this.Players.Add(new Models.Player(i));
            }

            this.WinCount = dimension;
        }

        public GameBoard(IUserInput userInput, int dimension, int playerCount, int winCount)
        {
            this.Field = new PlayableField(dimension);
            this.userInput = userInput;
            this.PlayerCount = playerCount;
            this.Players = new List<Models.Player>();
            for (int i = 0; i < playerCount; i++)
            {
                this.Players.Add(new Models.Player(i));
            }
            this.WinCount = winCount > dimension ? dimension : winCount;
        }

        public int PlayerCount { get; set; }
        public int WinCount { get; set; }
        
        private List<Models.Player> Players { get; set; }

        public PlayableField Field { get; set; }

        // Retrieves the input of a player, i.e. the field to set
        public int GetField()
        {
            return this.userInput.GetField(this.CurrentPlayer, this.Field.Dimension);
        }

        public void Set(int posX, int posY)
        {
            if (posX < 0 || posX > this.Field.Dimension || posY < 0 || posY > this.Field.Dimension) throw new ArgumentOutOfRangeException("The gameboard only consists of nine fields. Please set a new coin only on positions from zero to eight.");
            if (this.IsOccupied(posX, posY)) throw new ArgumentException("This field is already occupied");

            this.Field.SetEntry(new Entry(this.CurrentPlayer, posX, posY));
            
            this.SwitchPlayer();
        }

        public void Set(Position position)
        {
            this.Set(position.PosX, position.PosY);
        }
        
        /// <summary>
        /// Creates a textual representation of the board.
        /// </summary>
        /// <returns>String representation of the board.</returns>
        public override string ToString()
        {
            int dimensionArea = this.Field.Dimension * this.Field.Dimension;
            string result = string.Empty;

            // Convert the board (represented as a number) to a bit string.
            // The padding makes sure that it is 18 characters in size and gets leading zeros.
            var bits = new char[dimensionArea].ToString(); //Convert.ToString(board, 2).PadLeft(18, '0');
            var nextCharacter = string.Empty;
            var sb = new StringBuilder();

            for (int i = 0; i < dimensionArea; i++)
            {
                nextCharacter = "#"; // An empty field
                if (bits[i].Equals('1')) nextCharacter = "O";
                if (bits[i++].Equals('1')) nextCharacter = "X";

                sb.Append(nextCharacter);

                // Linebreak
                if ((i + 1) %  this.Field.Dimension == 0) sb.AppendLine();
            }

            return sb.ToString();
        }


        /// <summary>
        /// Switches the active player.
        /// </summary>
        private void SwitchPlayer()
        {
            this.CurrentPlayer = this.Players[this.CurrentPlayer.PlayerNumber++ % this.PlayerCount];
        }

        /// <summary>
        /// Check if field is occupied by using shift and logical operators.
        /// </summary>
        /// <param name="posX"></param>
        /// <param name="posY"></param>
        /// <returns></returns>
        public bool IsOccupied(int posX, int posY)
        {
            if (this.Field.Entries.Any())
            {
                var foundElement = this.Field.Entries.ToList();
                var x = foundElement.Where(entry => entry != null).FirstOrDefault(entry => entry.Position.PosX == posX && entry.Position.PosY == posY);
                return x != null;
            }

            return false;
        }
        
        /// <summary>
        /// Check if field is occupied by using shift and logical operators.
        /// </summary>
        /// <param name="position"></param>
        /// <returns></returns>
        public bool IsOccupied(Position position)
        {
            return IsOccupied(position.PosX, position.PosY);
        }

        private int steps = 0;        
        private const int fields = 9;
        private int gameCounter = 0;
        private int winP1Counter = 0;
        private int winP2Counter = 0;
        public enum GameState
        {
            GameStateRunning,
            GameStateRemis,
            GameStateWon,
            GameStateFieldOccupied,
            GameStateWrongField
        }
        public void ResetBoard()  { this.board = 0; steps = 0;}
        public int GameCounter => gameCounter;
        public int WinsPlayer1 => winP1Counter;

        public int WinsPlayer2 => winP2Counter;

        /// <summary>
        /// Checks if a Player has won by summing up when next to an entry another
        /// player related entry is found.
        ///
        /// Checks run for all three possible directions. Directions Being:
        /// <para>- To the Right</para>
        /// <para>- Diagonal / To the bottom right</para>
        /// <para>- To the Bottom</para>
        /// </summary>
        /// <returns></returns>
        public bool CheckWinner()
        {
            int sumRight = 0;
            int sumDiagonal = 0;
            int sumDown = 0;
            
            foreach (var entry in this.Field.Entries)
            {
                sumRight += Sum(Direction.Right, entry);
                sumDiagonal += Sum(Direction.Diagonal, entry);
                sumDown += Sum(Direction.Down, entry);
            }

            bool isRightWon = sumRight == this.WinCount;
            bool isDiagonalWon = sumDiagonal == this.WinCount;
            bool isDownWon = sumDown == this.WinCount;
            
            return isDiagonalWon || isDownWon || isRightWon;
        }

        private int Sum(Direction direction, Entry entry)
        {
            Entry nextEntry = null;
            switch (direction)
            {
                case Direction.Right:
                    nextEntry = this.Field.Entries.ToList().FirstOrDefault(entr => entr != null && entr.Position.PosY == entry.Position.PosY &&
                        entry.Position.PosX == entry.Position.PosX++ &&
                        entry.Player.PlayerNumber == this.CurrentPlayer.PlayerNumber);
                    break;
                case Direction.Diagonal:
                    nextEntry = this.Field.Entries.ToList().FirstOrDefault(entr => entr != null && entr.Position.PosY == entry.Position.PosY++ &&
                        entry.Position.PosX == entry.Position.PosX++ &&
                        entry.Player.PlayerNumber == this.CurrentPlayer.PlayerNumber);
                    break;
                case Direction.Down:
                    nextEntry = this.Field.Entries.ToList().FirstOrDefault(entr => entr != null && entr.Position.PosX == entry.Position.PosX &&
                        entry.Position.PosY == entry.Position.PosY++ &&
                        entry.Player.PlayerNumber == this.CurrentPlayer.PlayerNumber);
                    break;
                default:
                    throw new ArgumentOutOfRangeException(nameof(direction), direction, null);
            }


            return nextEntry != null ? 1 : 0;
        }
    }
}
