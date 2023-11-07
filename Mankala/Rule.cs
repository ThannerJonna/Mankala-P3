using System.Collections.Generic;
using System.Diagnostics;
using System.Text;
using System;

namespace Mankala
{
    //                      Abstract Rules

    internal abstract class EndGameRule
    {
        public EndGameRule() { }

        public abstract bool GameIsEnded(Board b);
    }

    internal abstract class EndOfTurnRule
    {
        public EndOfTurnRule() { }

        public abstract bool PlayerContinues(Board b, int lastPlace);

        public abstract void EndOfMove(Board b, int lastPlace);
    }

    internal abstract class MoveRule
    {
        public MoveRule() { }
        public abstract int Move(Board b, int start);
    }


    //                              Implemented Rules

    //              TurnRules

    internal class StealRule : EndOfTurnRule
    {
        //check for homebase
        public override void EndOfMove(Board b, int endPlace)
        {
            // !!! needs to end in player's pit
            int endPitCount = b.pits[endPlace];
            if (b.IsNonScoringPit(endPlace) && endPitCount == 1)
            {
                int otherPit = OtherStealPit(b, endPlace);
                b.PointsTo(b.pits[otherPit] + endPitCount);
                b.pits[otherPit] = 0;
                b.pits[endPlace] = 0;
            }
        }

        //check for homebase
        protected int OtherStealPit(Board b, int firstPit)
        {
            return (firstPit + 1 / 2 * b.PitCount) % b.PitCount;
        }

        //after stealing, the player never continues
        public override bool PlayerContinues(Board b, int lastPlace)
        {
            return false;
        }
    }

    internal class ContiuneTurnRule : EndOfTurnRule //integrate to be mankala-rules
    {
        public override void EndOfMove(Board b, int lastPlace)
        {
            throw new System.NotImplementedException();
        }

        public override bool PlayerContinues(Board b, int lastPlace)
        {
            throw new System.NotImplementedException();
        }
    }

    //              MoveRules

    internal class MankalaMove : MoveRule
    {
        public override int Move(Board b, int start)
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
    }

    internal class WariMove : MoveRule
    {
        public override int Move(Board b, int start)
        {
            int place = start;
            int stones = b.pits[place];
            b.pits[place] = 0;
            while (stones > 0)
            {
                place++;
                b.pits[place % b.pits.Length]++;
                stones--;
            }
            return place;
        }
    }
}
