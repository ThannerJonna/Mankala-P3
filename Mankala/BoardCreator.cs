using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/*Problemen:
 */

namespace Mankala
{
    public abstract class BoardCreator
    {
        //pit amount excludes the home pits
        public abstract Board CreateBoard(int pitAmount, int startAmount);

        public abstract Board StandardBoard();

        protected abstract void SetAllPits(int amount, Board b);

        public abstract string PrintBoard(Board board);
    }

    public class MankalaBCr : BoardCreator
    {
        public override Board CreateBoard(int pitAmount, int startAmount)
        {
            Board b = new Board(pitAmount);
            SetAllPits(startAmount, b);
            return b;
        }

        public override string PrintBoard(Board board)
        {
            throw new NotImplementedException();
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

        public override string PrintBoard(Board board)
        {
            string firstLine = "/__";

            return firstLine;
        }
    }

    public class SploraBCr : BoardCreator
    {
        public override Board CreateBoard(int pitAmount, int startAmount)
        {
            throw new NotImplementedException();
        }

        public override string PrintBoard(Board board)
        {
            throw new NotImplementedException();
        }

        public override Board StandardBoard()
        {
            throw new NotImplementedException();
        }

        protected override void SetAllPits(int amount, Board b)
        {
            throw new NotImplementedException();
        }
    }
}
