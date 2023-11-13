using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mankala
{
    public abstract class BoardCreator
    {
        public abstract Board CreateBoard(int pitAmount, int startAmount);

        public abstract Board StandardBoard();

        protected abstract void SetAllPits(int amount, Board b);
    }

    public class MankalaBCr : BoardCreator
    {
        public override Board CreateBoard(int pitAmount, int startAmount)
        {
            Board b = new Board(pitAmount);
            SetAllPits(startAmount, b);
            return b;
        }

        public override Board StandardBoard()
        {
            return CreateBoard(14, 4);
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

    public class WariBCr : BoardCreator
    {
        public override Board CreateBoard(int pitAmount, int startAmount)
        {
            Board b = new Board(pitAmount);
            SetAllPits(startAmount, b);
            return b;
        }

        public override Board StandardBoard()
        {
            return CreateBoard(14, 4);
        }

        protected override void SetAllPits(int amount, Board b)
        {
            for (int i = 0; i < b.pits.Length; i++)
            {
                if (i != 0 && i != b.pits.Length * 1 / 2)
                    b.pits[i] = amount;
            }
        }
    }
}
