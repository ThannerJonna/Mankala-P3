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

        protected abstract void SetAllPits(int amount, Board b);
    }

    public class MankalaBCr : BoardCreator
    {
        public override Board CreateBoard(int pitAmount = 7, int startAmount = 4)
        {

            Board b = new Board(pitAmount);
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

    public class WariBCr : BoardCreator
    {
        public override Board CreateBoard(int pitAmount, int startAmount)
        {
            throw new NotImplementedException();
        }

        protected override void SetAllPits(int amount, Board b)
        {
            for (int i = 0; i < b.pits.Length; i++)
            {
                b.pits[i] = amount;
            }
        }
    }
}
