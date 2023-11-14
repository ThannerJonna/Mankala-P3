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

            while (Accepted(input, out pitCount))
            {
                input = Console.ReadLine();
            }

        }

        static bool Accepted(string input, out int count)
        {
            if (int.TryParse(input, out count))
            {
                if (count > MaxPitCount)
                {
                    Console.WriteLine("This is bigger than we support.");
                }
                else if (count < MinPitCount)
                {
                    Console.WriteLine("This is too small.");
                }
                else
                {
                    return true;
                }
                return false;
            }
            else
            {
                Console.WriteLine("We can't turn this into a whole number.");
                return false;
            }

        }
    }
}
