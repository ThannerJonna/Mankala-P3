using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Schema;

namespace Mankala
{
    public static class PlayerHandler
    {
        public static player NextPlayer(player now)
        {
            if (now == player.P1)
                return player.P2;
            else
                return player.P1;
        }
    }

    public enum player { P1 = 1, P2 }

    internal class Program
    {
        static int MaxPitCount = 50;
        static int MinPitCount = 2;

        static void Main(string[] args)
        {
            MankalaFamFact fBoard = new Mankala_F();

            string input = Console.ReadLine();
            int pitCount;

            while (Accepted(input, out pitCount))
            {
                input = Console.ReadLine();
            }

            Board playBoard = fBoard.CreateBoard(pitCount);
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
