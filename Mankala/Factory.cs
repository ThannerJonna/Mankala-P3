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

        public abstract BoardCreator BoardBuilder();

        public abstract EndOfTurnRule GameTurnRule();

        public abstract EndGameRule WhatIsGameRule();
    }

    internal class Mankala_F : MankalaFamFact
    {
        public Mankala_F() { }

        public override BoardCreator BoardBuilder()
        {
            return new MankalaBCr();
        }

        public override EndOfTurnRule GameTurnRule()
        {
            throw new NotImplementedException();
        }

        public override EndGameRule WhatIsGameRule()
        {
            throw new NotImplementedException();
        }
    }
}
