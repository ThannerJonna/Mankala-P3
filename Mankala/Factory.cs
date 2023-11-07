using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mankala
{
    internal abstract class MankalaFamFact
    {

        protected MankalaFamFact() { }

        public abstract BoardCreator BoardBuilder();

        public abstract EndOfTurnRule GameTurnRule();

        public abstract MoveRule MoveHandler();

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
            return new MankalaTurn();
        }

        public override MoveRule MoveHandler()
        {
            return new MankalaMove();
        }

        public override EndGameRule WhatIsGameRule()
        {
            return new MankalaEndGame();
        }
    }

    internal class Wari_F : MankalaFamFact
    {
        public override BoardCreator BoardBuilder()
        {
            return new WariBCr();
        }

        public override EndOfTurnRule GameTurnRule()
        {
            return new WariTurn();
        }

        public override MoveRule MoveHandler()
        {
            return new WariMove();
        }

        public override EndGameRule WhatIsGameRule()
        {
            return new WariEndGame();
        }
    }
}
