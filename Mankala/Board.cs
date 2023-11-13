using System;
using System.Diagnostics;
using System.Media;

/*Problemen:
 */

namespace Mankala
{
    public class Board
    {
        public int[] pits;
        public int PitCount { get { return pits.Length; } }

        protected player currentplayer = player.P1;

        public Board(int numPit)
        {
            if (numPit <= 0)
                throw new Exception("Can't create negative or empty board!");
            this.pits = new int[numPit];
        }
    }
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
    }

}