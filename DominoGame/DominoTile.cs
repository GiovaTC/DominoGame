using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DominoGame
{
    public class DominoTile
    {
        public int Left { get; set; }
        public int Right { get; set; }

        public DominoTile(int left, int right)
        {
            Left = left;
            Right = right;
        }

        public DominoTile Flip()
        {
            return new DominoTile(Right, Left);
        }

        public override string ToString()
        {
            return $"{Left} | {Right}";
        }
    }
}
