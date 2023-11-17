using System.Collections.Generic;
using System.Diagnostics;
using System.Text;
using System;
using System.Runtime.InteropServices.WindowsRuntime;

/*Problemen:
 * Errors gooien voor moves enzo
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

        //player number corresponds with player points
        public abstract int[] Points(Board b);
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

        public abstract bool AcceptableMove(Board b, int pit, player play);

        public abstract (int, int) MoveRange(Board b, player play);
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

        public override int[] Points(Board b)
        {
            return new int[] { -1, b.pits[0], b.pits[b.PitCount / 2] };
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

        public override int[] Points(Board b)
        {
            return new int[] { -1, b.pits[0], b.pits[b.PitCount / 2] };
        }
    }

    //              TurnRules

    internal class MankalaTurn : EndOfTurnRule
    {
        public override void EndOfMove(Board b, int lastPlace, player current)
        {
            int endPitCount = b.pits[lastPlace];
            int otherPit = OtherStealPit(b, lastPlace);
            int stealPitCount = b.pits[otherPit];
            if (Constants.Owns(current, lastPlace, b.PitCount) && !Constants.IsScoringPit(lastPlace, b.PitCount)
                && stealPitCount > 0 && endPitCount == 1)
            {
                this.PointsTo(b, current, stealPitCount + endPitCount);
                b.pits[otherPit] = 0;
                b.pits[lastPlace] = 0;
            }
        }

        protected int OtherStealPit(Board b, int firstPit)
        {
            return (b.PitCount - firstPit);
        }

        protected void PointsTo(Board b, player collector, int amount)
        {
            if (collector == player.P1)
                b.pits[0] = +amount;
            else if (collector == player.P2)
                b.pits[b.PitCount / 2] += amount;
            else
                throw new Exception("Player cannot be given stones");
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
            if (!Constants.Owns(current, lastPlace, b.PitCount))
            {
                int points = b.pits[lastPlace];
                if (points == 2 || points == 3)
                {
                    b.pits[lastPlace] = 0;
                    if (current == player.P1)
                        b.pits[0] += points;
                    else
                        b.pits[b.PitCount / 2] += points;
                }
            }
        }

        public override bool PlayerContinues(Board b, int lastPlace, player current)
        {
            return false;
        }
    }

    //              MoveRules

    internal class MankalaMove : MoveRule
    {
        public override bool AcceptableMove(Board b, int pit, player play)
        {
            (int, int) range = MoveRange(b, play);
            if (pit >= range.Item1 && pit <= range.Item2)
                return b.pits[pit] > 0;
            return false;
        }

        public override (int, int) MoveRange(Board b, player play)
        {
            if (play == player.P1)
                return (1, b.PitCount / 2 - 1);
            else
                return (b.PitCount / 2 + 1, b.PitCount - 1);
        }

        public override int Move(Board b, int start, player current)
        {
            int end = Distribute(b, start, current);

            //Applying the combo's
            while (!Constants.IsScoringPit(end, b.PitCount) && b.pits[end] > 1)
            {
                end = Distribute(b, end, current);
            }
            return end;
        }

        protected int Distribute(Board b, int start, player p)
        {
            if (start == 0 || start == b.PitCount / 2)
                throw new Exception("You cannot move from the collection pits");

            int place = start;
            int stones = b.pits[place];
            b.pits[place] = 0;
            int skipPit;
            if (p == player.P1)
                skipPit = b.PitCount / 2;
            else
                skipPit = 0;

            while (stones > 0)
            {
                place--;
                if (place < 0)
                    place += b.PitCount;
                if (place != skipPit)
                {
                    b.pits[place]++;
                    stones--;
                }
            }
            return place;
        }
    }

    internal class WariMove : MoveRule
    {
        public override bool AcceptableMove(Board b, int pit, player play)
        {
            (int, int) range = MoveRange(b, play);
            if (pit >= range.Item1 && pit <= range.Item2)
                return b.pits[pit] > 0;
            return false;
        }

        public override int Move(Board b, int start, player current)
        {
            if (start == 0 || start == b.PitCount / 2)
                throw new Exception("You cannot move from the collection pits");

            int place = start;
            int stones = b.pits[place];
            b.pits[place] = 0;
            while (stones > 0)
            {
                place--;
                if (place < 0) place += b.PitCount;
                if (place != 0 && place != b.PitCount / 2)//place isn't a collection pit
                {
                    b.pits[place]++;
                    stones--;
                }
            }
            return place;
        }

        public override (int, int) MoveRange(Board b, player play)
        {
            if (play == player.P1)
                return (1, b.PitCount / 2 - 1);
            else
                return (b.PitCount / 2 + 1, b.PitCount - 1);
        }
    }
}
