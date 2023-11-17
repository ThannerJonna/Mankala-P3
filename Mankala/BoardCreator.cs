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
        public abstract Board CreateBoard(int pitAmount, int startAmount);//pit amount excludes the pits that cannot be moved into by a move

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

        public override string PrintBoard(Board board)
        {
            int midPits = board.PitCount / 2 - 1;

            string firstLine = "    "; // 4 spaces
            string secondLine = AdaptNum(board.pits[0]);
            string thirdLine = "    ";
            for (int i = 1; i <= midPits; i++)
            {
                firstLine += "|" + AdaptNum(board.pits[i]);
                secondLine += "|----";
                thirdLine += "|" + AdaptNum(board.pits[board.PitCount - i]);
            }
            firstLine += "|    ";
            secondLine += "|" + AdaptNum(board.pits[board.PitCount / 2]);
            thirdLine += "|    ";

            return firstLine + "\n" + secondLine + "\n" + thirdLine;
        }

        protected string AdaptNum(int num)
        {
            string str = "";
            string sNum = num.ToString();
            int diff = 4 - sNum.Length;

            str = new string(' ', (diff + 1) / 2) + sNum + new string(' ', diff / 2);
            return str;
        }
    }

    public class WariBCr : BoardCreator
    {
        public override Board CreateBoard(int pitAmount, int startAmount)
        {
            Board b = new Board(pitAmount + 2);//adding in the 2 collection pits
            SetAllPits(startAmount, b);
            return b;
        }

        public override Board StandardBoard()
        {
            return CreateBoard(2 * 6, 4);
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
            int midPits = board.PitCount / 2 - 1;

            string firstLine = "    "; // 4 spaces
            string secondLine = AdaptNum(board.pits[0]);
            string thirdLine = "    ";
            for (int i = 1; i <= midPits; i++)
            {
                firstLine += "|" + AdaptNum(board.pits[i]);
                secondLine += "|----";
                thirdLine += "|" + AdaptNum(board.pits[board.PitCount - i]);
            }
            firstLine += "|    ";
            secondLine += "|" + AdaptNum(board.pits[board.PitCount / 2]);
            thirdLine += "|    ";

            return firstLine + "\n" + secondLine + "\n" + thirdLine;
        }

        protected string AdaptNum(int num)
        {
            string str = "";
            string sNum = num.ToString();
            int diff = 4 - sNum.Length;

            str = new string(' ', (diff + 1) / 2) + sNum + new string(' ', diff / 2);
            return str;
        }
    }

    public class SploraBCr : WariBCr
    {
        public override Board StandardBoard()
        {
            return CreateBoard(7, 6);
        }
    }
}
