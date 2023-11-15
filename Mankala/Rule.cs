using System.Collections.Generic;
using System.Diagnostics;
using System.Text;
using System;
using System.Runtime.InteropServices.WindowsRuntime;

/*Problemen:
 * Veel redundancy met "IsScoringPit" (got attention, to be completely fixed)
 * ManakalaTurn is nog niet klaar, mist nog een condition
 * Points to methode
 */

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
        public abstract int Move(Board b, int start, player current);
    }


    //                              Implemented Rules

    //              endGameRules

    internal class MankalaEndGame : EndGameRule
    {
        public override bool GameIsEnded(Board b, player upToGo)
        {
            int start;
            int end;

            if (upToGo == player.P1)
            {
                start = 1;
                end = b.PitCount * 1 / 2;
            }
            else
            {
                start = b.PitCount / 2 + 1;
                end = b.PitCount;
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
            int score2 = b.pits[b.PitCount / 2];

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
            int start;
            int end;

            if (upToGo == player.P1)
            {
                start = 1;
                end = b.PitCount * 1 / 2;
            }
            else
            {
                start = b.PitCount / 2 + 1;
                end = b.PitCount;
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
            int score2 = b.pits[b.PitCount / 2];

            if (score1 > score2)
                return 1;
            else if (score1 < score2)
                return 2;
            return 0;
        }
    }

    //              TurnRules

    internal class MankalaTurn : EndOfTurnRule
    {
        public override void EndOfMove(Board b, int lastPlace, player current)
        {
            int endPitCount = b.pits[lastPlace];
            if (Constants.Owns(current, lastPlace, b.PitCount) && !Constants.IsScoringPit(b.PitCount, lastPlace) && endPitCount == 1)
            {
                int otherPit = OtherStealPit(b, lastPlace);
                this.PointsTo(b, current, b.pits[otherPit] + endPitCount);
                b.pits[otherPit] = 0;
                b.pits[lastPlace] = 0;
            }
        }

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
            if (current == player.P1)
                return lastPlace == 0;
            if (current == player.P2)
                return lastPlace == b.PitCount / 2;
            throw new Exception("Player (type) unsupported");
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
        public override int Move(Board b, int start, player current)
        {
            int end = Distribute(b, start);

            while (!StaticMankala.IsScoringPit(b.PitCount, end) && StaticMankala.Owns(current, end, b.PitCount) && b.pits[end] > 0)
            {
                end = Distribute(b, end);
            }
            return end;
        }

        protected int Distribute(Board b, int start)
        {
            int place = start;
            int stones = b.pits[place];
            b.pits[place] = 0;
            while (stones > 0)
            {
                place--;
                if (!IsScoringPit(b.PitCount, place))
                {
                    b.pits[place % b.PitCount]++;
                    stones--;
                }
            }
            return place;
        }
    }

    internal class WariMove : MoveRule
    {
        public override int Move(Board b, int start, player current)
        {
            int place = start;
            int stones = b.pits[place];
            b.pits[place] = 0;
            while (stones > 0)
            {
                place--;
                if (place != 0 && place != b.PitCount / 2)
                {
                    b.pits[place % b.PitCount]++;
                    stones--;
                }
            }
            return place;
        }
    }
}
