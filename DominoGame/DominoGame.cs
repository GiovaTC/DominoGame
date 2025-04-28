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



    }
}
