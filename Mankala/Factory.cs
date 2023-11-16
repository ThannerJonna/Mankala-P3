using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/*Problemen:
 * Invullen Splora factory
 * Checken of meteen invullen fields een probleem is
 */

namespace Mankala
{
    internal abstract class FamMankalaFact
    {
        public BoardCreator bCreator;
        public EndOfTurnRule endTurn;
        public MoveRule moveRule;
        public EndGameRule endGameRule;

        protected FamMankalaFact()
        { //Standard filling of the fields, obviously doesn't change with the concrete factory (type)
            this.bCreator = BoardBuilder();
            this.endTurn = GameTurnRule();
            this.moveRule = MoveHandler();
            this.endGameRule = WhatIsGameRule();
        }

        public abstract BoardCreator BoardBuilder();

        public abstract EndOfTurnRule GameTurnRule();

        public abstract MoveRule MoveHandler();

        public abstract EndGameRule WhatIsGameRule();
    }

    internal class Mankala_F : FamMankalaFact
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

    internal class Wari_F : FamMankalaFact
    {
        public Wari_F() { }

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
    internal class Splora_F : FamMankalaFact
    {
        public Splora_F() { }

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
