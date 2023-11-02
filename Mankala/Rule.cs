using System.Collections.Generic;

namespace Mankala
{
    public class RuleSet
    {
        internal List<EndGameRule> endConditions;
        internal List<EndOfTurnRule> endTurnRules;

        internal RuleSet(List<EndGameRule> gameEnders, List<EndOfTurnRule> turnRules)
        {
            this.endConditions = gameEnders;
            this.endTurnRules = turnRules;
        }

        public bool GameIsEnded(Board b)
        {
            foreach (EndGameRule c in endConditions)
            {
                if (c.GameIsEnded(b))
                    return true;
            }
            return false;
        }

        public void EndOfMove(Board b, int lastPlace)
        {
            foreach (EndOfTurnRule rule in endTurnRules)
                rule.EndOfMove(b, lastPlace);
        }

        //insert the correct parameters
        public bool PlayerContinues(Board b, int lastPlace)
        {
            foreach (EndOfTurnRule rule in endTurnRules)
            {
                if (rule.PlayerContinues(b, lastPlace))
                    return true;
            }
            return false;
        }

        public static RuleSet MankalaRules()
        {
            List<EndGameRule> endGame = new List<EndGameRule>();
            //endGame.Add();

            List<EndOfTurnRule> turnRules = new List<EndOfTurnRule>();
            turnRules.Add(new StealRule());

            RuleSet theRules = new RuleSet(endGame, turnRules);
            return theRules;
        }
    }

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

    internal class ContiuneTurnRule : EndOfTurnRule
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
}
