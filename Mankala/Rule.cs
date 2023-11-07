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

        public abstract player Winner(Board b);
    }

    internal abstract class EndOfTurnRule
    {
        public EndOfTurnRule() { }

        public abstract bool PlayerContinues(Board b, int lastPlace, player current);

        public abstract void EndOfMove(Board b, int lastPlace, player current);
    }

    internal abstract class MoveRule
    {
        public MoveRule() { }

        //clockwise through the array is ++ and anti-clockwise is --
        public abstract int Move(Board b, int start);
    }


    //                              Implemented Rules

    //              TurnRules

    internal class MankalaEndGame : EndGameRule
    {
        public override bool GameIsEnded(Board b)
        {
            throw new NotImplementedException();
        }

        public override player Winner(Board b)
        {
            throw new NotImplementedException();
        }
    }

    internal class WariEndGame : EndGameRule
    {
        public override bool GameIsEnded(Board b)
        {
            throw new NotImplementedException();
        }

        public override player Winner(Board b)
        {
            throw new NotImplementedException();
        }
    }

    //              TurnRules

    internal class MankalaTurn : EndOfTurnRule
    {
        //check for homebase
        public override void EndOfMove(Board b, int lastPlace, player current)
        {
            // !!! needs to end in player's pit
            int endPitCount = b.pits[lastPlace];
            if (b.IsNonScoringPit(lastPlace) && endPitCount == 1)
            {
                int otherPit = OtherStealPit(b, lastPlace);
                this.PointsTo(b, current, b.pits[otherPit] + endPitCount);
                b.pits[otherPit] = 0;
                b.pits[lastPlace] = 0;
            }
        }

        //check for homebase
        protected int OtherStealPit(Board b, int firstPit)
        {
            return (firstPit + 1 / 2 * b.PitCount) % b.PitCount;
        }

        protected void PointsTo(Board b, player collector, int amount)//TODO
        {
            return;
        }

        public override bool PlayerContinues(Board b, int lastPlace, player current)
        {
            throw new NotImplementedException();
        }
    }

    internal class WariTurn : EndOfTurnRule
    {
        public override void EndOfMove(Board b, int lastPlace, player current)
        {
            throw new NotImplementedException();
        }

        public override bool PlayerContinues(Board b, int lastPlace, player current)
        {
            throw new NotImplementedException();
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
