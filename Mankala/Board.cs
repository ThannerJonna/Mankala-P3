using System;
using System.Diagnostics;
using System.Media;

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

}