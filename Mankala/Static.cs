using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mankala
{
    //Name is still very foggy
    internal static class Constants
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
}
