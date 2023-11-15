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
        static int MaxPitCount = 50;
        static int MinPitCount = 2;

        static void Main(string[] args)
        {
            MankalaFamFact fBoard = new Mankala_F();
            //Factory heeft nu alle shizzle erin zitten


            string input = Console.ReadLine();
            int pitCount;

            while (AcceptedPitCount(input, out pitCount))
            {
                input = Console.ReadLine();
            }

        }

        static bool AcceptedPitCount(string input, out int count)
        {
            if (AcceptedNumber(input, out count))
            {
                if (count > MaxPitCount)
                {
                    Console.WriteLine("This is bigger than we support.");
                }
                else if (count < MinPitCount)
                {
                    Console.WriteLine("This cannot make a proper board; it is too small.");
                }
                else
                {
                    return true;
                }
                return false;
            }
            return false;
        }

        public static bool AcceptedMove(string input, out int movePit, int pitCount)
        {
            if (AcceptedNumber(input, out movePit))
            {
                return true;
            }
            return false;
        }

        public static bool AcceptedNumber(string input, out int number)
        {
            if (int.TryParse(input, out number))
            {
                if (number < 0)
                {
                    Console.WriteLine("No negative numbers are accepted");
                    return false;
                }
                return true;
            }
            else
            {
                Console.WriteLine("This can't be turned this into a whole number.");
                return false;
            }
        }
    }
}
