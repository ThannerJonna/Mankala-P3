using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/*Problems:
 * name constants
 * splora game in dictionary
 */

namespace Mankala
{
    //Name is still very foggy
    internal static class SharedBoardHandler //Could be SharedBoard, HomePitHandler
    {
        public static bool Owns(player pl, int pit, int pitCount)
        {
            if (pl == player.P1)
            {
                return pit < pitCount / 2;
            }
            else if (pl == player.P2)
            {
                return pit >= pitCount / 2;
            }
            throw new Exception("Player unnaccounted for: " + pl.ToString());
        }

        public static bool IsScoringPit(int pit, int pitCount)
        {
            return pit == 0 || pit == pitCount / 2;
        }
    }

    //Player functions
    public enum player { P1 = 1, P2 }

    public static class PlayerHandler
    {
        public static player NextPlayer(player now)
        {
            if (now == player.P1)
                return player.P2;
            else
                return player.P1;
        }

        public static string PlayerString(player player)
        {
            return "Player " + ((int)player);
        }
    }

    public static class InputHandler
    {
        private static int MaxPitCount = 24;
        private static int MinPitCount = 2;
        internal static Dictionary<string, FamMankalaFact> madeGames = new Dictionary<string, FamMankalaFact>()
        {
            {"mankala", new Mankala_F() },
            {"wari", new Wari_F() }
            //,{"splora", new Splora_F() }
        };
        private static int MaxStartAmount = 41;
        private static int MinStartAmount = 1;

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
                Console.WriteLine("This can't be turned into a whole number.");
                return false;
            }
        }

        public static bool AcceptedPitCount(string input, out int count)
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

        public static bool AcceptedStartAmount(string input, out int count)
        {
            if (AcceptedNumber(input, out count))
            {
                if (count > MaxStartAmount)
                {
                    Console.WriteLine("This is more than we support.");
                }
                else if (count < MinStartAmount)
                {
                    Console.WriteLine("This cannot start a game; this amount is too small.");
                }
                else
                {
                    return true;
                }
                return false;
            }
            return false;
        }
    }
}
