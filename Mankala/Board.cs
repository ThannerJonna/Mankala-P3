using System;
using System.Diagnostics;
using System.Media;

/*Problemen:
 */

namespace Mankala
{
    public class Board
    {
        //pit number 0 is usually owned by Player 1
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
}