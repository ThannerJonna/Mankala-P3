using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Schema;

/*Problemen:
 * Print functie voor het printen van het bord naar de Console
 * Print input-instructies
 * Lees selectie van het spel (Bool AcceptedSelection)
 * Zoek uit of bordgrootte ook dynamisch moet zijn
 */

namespace Mankala
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Welcome to the family of board games \"Mankala\"!!\n" +
                "We have a choice of the following games:\n");
            foreach (string name in InputHandler.madeGames.Keys)
            {
                Console.WriteLine(name);
            }
            Console.WriteLine("Choose one to play:");
            string game = Console.ReadLine();
            while (!InputHandler.madeGames.ContainsKey(game))
            {
                Console.WriteLine("Sorry, we can't seem to retrieve: " + game);
                game = Console.ReadLine();
            }

            FamMankalaFact fBoard = InputHandler.madeGames[game];

            Console.WriteLine("How big do you want the board?");
            string input = Console.ReadLine();
            int pitCount;

            while (!InputHandler.AcceptedPitCount(input, out pitCount))
            {
                input = Console.ReadLine();
            }
        }

        public static void PrintBoard(Board b)
        {

        }
    }
}
