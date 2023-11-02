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
        protected RuleSet ruleBook;

        public Board(int numPit, RuleSet rules)
        {
            if (numPit <= 0)
                throw new Exception("Can't create negative or empty board!");
            if (numPit % 2 == 1)
                throw new Exception("Unfair division between players!");

            this.pits = new int[numPit];
            this.ruleBook = rules;
        }

        //place indicates the space from which the stones will be taken
        public virtual void Move(int place)
        {
            int stones = pits[place];
            pits[place] = 0;
            while (stones > 0)
            {
                place++;
                pits[place % pits.Length]++;
                stones--;
            }
            this.EndOfTurnRules(place);
        }

        protected void EndOfTurnRules(int endPit)
        {
            if (this.ruleBook.PlayerContinues(this, endPit))
                currentplayer = PlayerHandler.NextPlayer(currentplayer);
            this.ruleBook.EndOfMove(this, endPit);
        }
    }

    public class HomeBoard : Board //this class needs to be removed!!!!
    {
        protected int _homePit1 = 0; //the place in the array that is a homebase
        protected int _homePit2; //the place in the array for the other homebase

        public HomeBoard(int numPit, RuleSet rules) : base(numPit, rules)
        {
            _homePit2 = numPit / 2;
        }

        public override void Move(int place)
        {
            int stones = pits[place];
            pits[place] = 0;
            while (stones > 0)
            {
                place++;
                if (IsNonScoringPit(place))
                {
                    pits[place % pits.Length]++;
                    stones--;
                }
            }
        }

        public override void PointsTo(int points)
        {
            switch (this.currentplayer)
            {
                case player.P1:
                    pits[_homePit1] += points;
                    break;
                case player.P2:
                    pits[_homePit2] += points;
                    break;
            }
        }
    }

    /* body for other implementation SetAll
     * for (int i = 0; i < pits.Length; i++)
            {
                pits[i] = amount;
            }
    */
    /*
     *int stones = pits[place];
            pits[place] = 0;
            while (stones > 0)
            {
                place++;
                pits[place % pits.Length]++;
                stones--;
            }
            this.ruleBook.EndOfMove(this, place); 
     */
}
