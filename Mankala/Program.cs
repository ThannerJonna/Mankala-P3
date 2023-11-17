using System;
using System.Collections.Generic;
using System.ComponentModel.Design;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Schema;

/*Problemen:
 * Errors handlen in de loop
 * Doorgaan met UI bij de moveLoop  
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
            foreach (string gameName in InputHandler.madeGames.Keys)
            {
                Console.WriteLine(gameName);
            }

            //Game choice
            Console.WriteLine("Choose one to play:");
            string name = Console.ReadLine().ToLower();
            while (!InputHandler.madeGames.ContainsKey(name))
            {
                Console.WriteLine("Sorry, we can't seem to retrieve: " + name);
                name = Console.ReadLine().ToLower();
            }
            FamMankalaFact factory = InputHandler.madeGames[name];

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
                Console.WriteLine("How many pits should each player have?");
                string inputPitCount = Console.ReadLine();
                int pitCount;//for one player/one side of the board

                while (!InputHandler.AcceptedPitCount(inputPitCount, out pitCount))
                {
                    inputPitCount = Console.ReadLine();
                }

                //Choosing start amount in each pit
                Console.WriteLine("How big should the starting amount be in each pit?");
                string inputStartAm = Console.ReadLine();
                int startAmount;

                while (!InputHandler.AcceptedStartAmount(inputStartAm, out startAmount))
                {
                    inputStartAm = Console.ReadLine();
                }
                gameBoard = factory.bCreator.CreateBoard(pitCount * 2, startAmount);
            }

            //Printing board for the first time
            Console.WriteLine("The board start like this:");
            Console.WriteLine(factory.bCreator.PrintBoard(gameBoard));
            Console.WriteLine("Choose who will be player 1 and player 2. " +
                "Player 1 has their pits at the top, Player 2 at the bottom.");

            player whoTurn = player.P1;

            while (!factory.endGameRule.GameIsEnded(gameBoard, whoTurn))
            {
                Console.WriteLine("It is " + PlayerHandler.PlayerString(whoTurn) + "'s turn!");
                (int, int) range = factory.moveRule.MoveRange(gameBoard, whoTurn);
                Console.WriteLine("Numbered in anti-clockwise fashion starting left, you can choose pit: "
                    + range.Item1 + "-" + range.Item2 + " of the pits on your side");

                int lastPit;
                int chosenMove;
                string moveInput = Console.ReadLine();
                //any hindrance has happened in doing the next move
                bool unacceptable = true;

                while (unacceptable)
                {
                    if (InputHandler.AcceptedNumber(moveInput, out chosenMove))
                    {
                        if (factory.moveRule.AcceptableMove(gameBoard, chosenMove, whoTurn))
                        {
                            try
                            {
                                lastPit = factory.moveRule.Move(gameBoard, chosenMove, whoTurn);
                                Console.WriteLine("\n\nThe board before any special steps at the end of the turn:");
                                Console.WriteLine(factory.bCreator.PrintBoard(gameBoard));//show the in between step

                                //end the turn
                                factory.endTurn.EndOfMove(gameBoard, lastPit, whoTurn);

                                //deciding the next turn
                                if (factory.endTurn.PlayerContinues(gameBoard, lastPit, whoTurn))
                                    Console.WriteLine(PlayerHandler.PlayerString(whoTurn) + " gets another turn!");
                                else//player changes
                                    whoTurn = PlayerHandler.NextPlayer(whoTurn);

                                //Show players the Board
                                Console.WriteLine("The board after special rules:");
                                Console.WriteLine(factory.bCreator.PrintBoard(gameBoard));

                                unacceptable = false; //we can proceed
                            }
                            catch (Exception ex)
                            {
                                Console.WriteLine(ex.Message);
                                Console.WriteLine(ex.StackTrace);
                                Console.WriteLine("To retry, re-enter a move (this was a significant error/problem)");
                            }
                        }
                        else
                        {
                            Console.WriteLine("This move is not legal for this game");
                            moveInput = Console.ReadLine();//update move, try again
                        }
                    }
                    else
                    {//not a good number
                     //handler already gives feedback
                        moveInput = Console.ReadLine();//update move, try again
                    }
                }
            }
            int winnerNum = factory.endGameRule.Winner(gameBoard);
            int[] pointDistribution = factory.endGameRule.Points(gameBoard);
            if (winnerNum == 0)
            {
                Console.WriteLine("\n\n\nThe game ended in a tie");
            }
            else
            {
                Console.WriteLine("Player " + winnerNum + " has won the game with " + pointDistribution[winnerNum]
                    + " points!");
                string showDistr = "The points were: " + pointDistribution[1];
                for (int i = 2; i < pointDistribution.Length; i++)
                {
                    showDistr += " : " + pointDistribution[i];
                }
                Console.WriteLine(showDistr);
            }

            //keep the game running
            Console.ReadLine();
        }
    }
}
