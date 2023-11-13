using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mankala
{
    internal abstract class MankalaFamFact
    {
        public BoardCreator bCreator;
        public EndOfTurnRule endTurn;
        public MoveRule moveRule;
        public EndGameRule endGameRule;

        protected MankalaFamFact() { }

        public abstract BoardCreator BoardBuilder();

        public abstract EndOfTurnRule GameTurnRule();

        public abstract MoveRule MoveHandler();

        public abstract EndGameRule WhatIsGameRule();
    }

    internal class Mankala_F : MankalaFamFact
    {
        public Mankala_F()
        {
            this.bCreator = BoardBuilder();
            this.endTurn = GameTurnRule();
            this.moveRule = MoveHandler();
            this.endGameRule = WhatIsGameRule();
        }

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
        public Wari_F()
        {
            this.bCreator = BoardBuilder();
            this.endTurn = GameTurnRule();
            this.moveRule = MoveHandler();
            this.endGameRule = WhatIsGameRule();
        }

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

    //TODO
    internal class Splora_F : MankalaFamFact
    {
        public Splora_F()
        {
            this.bCreator = BoardBuilder();
            this.endTurn = GameTurnRule();
            this.moveRule = MoveHandler();
            this.endGameRule = WhatIsGameRule();
        }

        public override BoardCreator BoardBuilder()
        {
            throw new NotImplementedException();
        }

        public override EndOfTurnRule GameTurnRule()
        {
            throw new NotImplementedException();
        }

        public override MoveRule MoveHandler()
        {
            throw new NotImplementedException();
        }

        public override EndGameRule WhatIsGameRule()
        {
            throw new NotImplementedException();
        }
    }
}
