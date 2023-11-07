﻿using System.Collections.Generic;
using System.Diagnostics;
using System.Text;
using System;
using System.Runtime.InteropServices.WindowsRuntime;

namespace Mankala
{
    //                      Abstract Rules

    internal abstract class EndGameRule
    {
        public EndGameRule() { }

        public abstract bool GameIsEnded(Board b, player upToGo);

        //returns player number of winner or returns 0 if a draw
        public abstract int Winner(Board b);
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
        public override bool GameIsEnded(Board b, player upToGo)
        {
            int start;
            int end;

            if (upToGo == player.P1)
            {
                start = 1;
                end = b.pits.Length * 1 / 2;
            }
            else
            {
                start = b.pits.Length / 2 + 1;
                end = b.pits.Length;
            }

            for (int i = start; i < end; i++)
            {
                if (b.pits[i] > 0)
                    return false;
            }
            return true;
        }

        public override int Winner(Board b)
        {
            int score1 = b.pits[0];
            int score2 = b.pits[b.pits.Length / 2];

            if (score1 > score2)
                return 1;
            else if (score1 < score2)
                return 2;
            return 0;
        }
    }

    internal class WariEndGame : EndGameRule
    {
        public override bool GameIsEnded(Board b, player upToGo)
        {
            throw new NotImplementedException();
        }

        public override int Winner(Board b)
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
            int place = start;
            int stones = b.pits[place];
            b.pits[place] = 0;
            while (stones > 0)
            {
                place--;
                if (!IsScoringPit(b.pits, place))
                {
                    b.pits[place % b.pits.Length]++;
                    stones--;
                }
            }
            return place;
        }

        protected bool IsScoringPit(int[] boardPits, int place)
        {
            return place == 0 || place == 1 / 2 * boardPits.Length;
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
                place--;
                b.pits[place % b.pits.Length]++;
                stones--;
            }
            return place;
        }
    }
}
