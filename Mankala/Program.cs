using System;
using System.Collections.Generic;
using System.ComponentModel.Design;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Schema;

/*Problemen:
 * Print functie voor het printen van het bord naar de Console
 * Print inputPitCount-instructies
 * Lees selectie van het spel (Bool AcceptedSelection)
 * Zoek uit of bordgrootte ook dynamisch moet zijn
 */

namespace Mankala
{
    internal class Program
    {
        static void Main(string[] args)
        {
            //introduction
            Console.WriteLine("Welcome to the family of board games \"Mankala\"!!\n" +
                "We have a choice of the following games:\n");
            foreach (string name in InputHandler.madeGames.Keys)
            {
                Console.WriteLine(name);
            }

            //Game choice
            Console.WriteLine("Choose one to play:");
            string game = Console.ReadLine().ToLower();
            while (!InputHandler.madeGames.ContainsKey(game))
            {
                Console.WriteLine("Sorry, we can't seem to retrieve: " + game);
                game = Console.ReadLine().ToLower();
            }
            FamMankalaFact factory = InputHandler.madeGames[game];
            
            //Creating a board
            Board gameBoard;

            Console.WriteLine("Would you like to use a standard board or a custom board?");
            string boardChoice = Console.ReadLine().ToLower();
            while (!(boardChoice == "standard" || boardChoice == "custom"))
            {
                Console.WriteLine("Sorry, please choose \"standard\" or \"custom\"");
                boardChoice = Console.ReadLine().ToLower();
            }

            if (boardChoice == "standard")
            {//standard board choice
                gameBoard = factory.bCreator.StandardBoard();
            }
            else
            {//custom board choice
                //Choosing amount of pits
                Console.WriteLine("How big do you want the board?");
                string inputPitCount = Console.ReadLine();
                int pitCount;

                while (!InputHandler.AcceptedPitCount(inputPitCount, out pitCount))
                {
                    inputPitCount = Console.ReadLine();
                }

                //Choosing start amount in each pit
                Console.WriteLine("How big should the starting amount be in each pit?");
                string inputStartAm = Console.ReadLine();
                int startAmount;

                while(!InputHandler.AcceptedStartAmount(inputStartAm, out startAmount))
                {
                    inputStartAm = Console.ReadLine();
                }
                gameBoard = factory.bCreator.CreateBoard(pitCount, startAmount);
            }

            //Printing board for the first time
            Console.WriteLine("The board start like this:");
            Console.WriteLine(factory.bCreator.PrintBoard(gameBoard));
            Console.WriteLine("Choose who will be player 1 and player 2. " +
                "Player 1 has their pits at the top, Player 2 at the bottom.");

            player whoTurn = player.P1;

            while(!factory.endGameRule.GameIsEnded(gameBoard, whoTurn))
            {
                Console.WriteLine("It is " + PlayerHandler.PlayerString(whoTurn) + "'s turn!");


            }


            Console.ReadLine();
        }
    }
}
