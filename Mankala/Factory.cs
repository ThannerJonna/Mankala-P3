using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mankala
{
    internal abstract class MankalaFamFact //abstract factory for 
    {
        
        protected MankalaFamFact() { }

        public abstract Board CreateBoard(int pitAmount, int startAmount);

        public abstract EndOfTurnRule GameTurnRule();

        public abstract EndGameRule WhatIsGameRule();

        public abstract void SetAllPits(int amount, Board b);
    }

    internal class Mankala_F : MankalaFamFact
    {
        public Mankala_F() { }

        public override Board CreateBoard(int pitAmount = 7, int startAmount = 4)
        {

            Board b = new HomeBoard(pitAmount, RuleSet.MankalaRules());
            SetAllPits(startAmount, b);
            return b;
        }

        protected override void SetAllPits(int amount, Board b)
        {
            int[] pits = b.pits;
            for (int i = 0; i < pits.Length; i++)
            {
                if (i != 0 && i != pits.Length * 1 / 2)
                    pits[i] = amount;
            }
        }


    }
}
