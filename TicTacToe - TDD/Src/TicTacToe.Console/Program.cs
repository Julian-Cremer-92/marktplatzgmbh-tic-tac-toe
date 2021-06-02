using System.Runtime.CompilerServices;
using TicTacToe.Console.GameStates;
using TicTacToeLib;
using TicTacToeLib.Models;

namespace TicTacToe.Console
{
    static class Program
    {
        private static int dimension;
        private static int playerCount; 
        static void Main()
        {
            SetUpNewGame();
            var board = new GameBoard(new UserInput(), dimension, playerCount);

            IGameState currentState = new GameStateRunning();

            while (currentState != null)
            {
                currentState = currentState.Execute(board);
            }
        }
        
        private static void SetUpNewGame()
        {
            string input = string.Empty;
            int number = 0;

            do
            {
                System.Console.WriteLine("Enter with how many player you want to play: ");
                input = System.Console.ReadLine();
            } while (!int.TryParse(input, out number) || number < 1);

            playerCount = number;
            number = 0;
            
            do
            {
                System.Console.WriteLine("Enter size of the game field (3 => 3x3; 4 => 4x4 etc.): ");
                input = System.Console.ReadLine();
            } while (!int.TryParse(input, out number) || number < 1);

            dimension = number;

        }
    }
}
