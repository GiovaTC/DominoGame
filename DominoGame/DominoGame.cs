using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DominoGame
{
    public class DominoGame
    {
        public List<DominoTile> Deck { get; private set; }
        public List<DominoTile> Table { get; private set; }
        public List<DominoTile> PlayerHand { get; private set; }

        public DominoGame()
        {
            Deck = GenerateDeck();
            ShuffleDeck();
            PlayerHand = DrawTiles(7);
            Table = new List<DominoTile>();
        }

        private List<DominoTile> GenerateDeck()
        {
            var deck = new List<DominoTile>();
            for (int i = 0; i <= 6; i++)
            {
                for (int j = i; j <= 6; j++)
                {
                    deck.Add(new DominoTile(i, j));
                }
            }
            return deck;
        }

        private void ShuffleDeck()
        {
            var rnd = new Random();
            Deck = Deck.OrderBy(x => rnd.Next()).ToList();
        }

        private List<DominoTile> DrawTiles(int count)
        {
            var tiles = Deck.Take(count).ToList();
            Deck.RemoveRange(0, count);
            return tiles;
        }

        public bool CanPlay(DominoTile tile)
        {
            if (Table.Count == 0) return true;
            int leftEnd = Table.First().Left;
            int rightEnd = Table.Last().Right;
            return tile.Left == leftEnd || tile.Right == leftEnd || tile.Left == rightEnd || tile.Right == rightEnd;
        }

        public bool PlayTile(DominoTile tile)
        {
            if (Table.Count == 0)
            {
                Table.Add(tile);
                PlayerHand.Remove(tile);
                return true;
            }

            int leftEnd = Table.First().Left;
            int rightEnd = Table.Last().Right;

            if (tile.Right == leftEnd)
            {
                Table.Insert(0, tile);
            }
            else if (tile.Left == leftEnd)
            {
                Table.Insert(0, tile.Flip());
            }
            else if (tile.Left == rightEnd)
            {
                Table.Add(tile);
            }
            else if (tile.Right == rightEnd)
            {
                Table.Add(tile.Flip());
            }
            else
            {
                return false;
            }

            PlayerHand.Remove(tile);
            return true;
        }

        public DominoTile DrawTile()
        {
            if (Deck.Count == 0) return null;
            var tile = Deck.First();
            Deck.RemoveAt(0);
            PlayerHand.Add(tile);
            return tile;
        }
    }
}

