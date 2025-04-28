Juego de Dominó en Visual Studio 2022 con Windows Forms

![image](https://github.com/user-attachments/assets/486ceb2c-e8ce-446c-a058-c04fe90dda01)

Vamos a hacer una versión más completa del juego de dominó en Visual Studio 2022 usando Windows Forms:

✔️ Fichas en la mano

✔️ Validación de jugadas (número debe coincidir)

✔️ Robar fichas si no puedes jugar

✔️ Turnos de jugador (puede ser expandido luego a jugador vs jugador o vs IA)

🎯 Plan para el Juego de Dominó
Modelo de ficha (DominoTile)

Modelo de juego (DominoGame)

Mesa (fichas jugadas)

Pozo (fichas restantes para robar)

Manos de jugadores

Interfaz gráfica (Form1)

Dibujar la mano del jugador

Mostrar la mesa

Controlar turnos y acciones (jugar/robar)

🔥 Código Base Completo
1. Clase DominoTile.cs

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
                return $"{Left}|{Right}";
             }
           }
2. Clase DominoGame.cs

        using System;
        using System.Collections.Generic;
        using System.Linq;

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
3. Formulario Principal Form1.cs

        using System;
        using System.Collections.Generic;
        using System.Drawing;
        using System.Linq;
        using System.Windows.Forms;
        
        public partial class Form1 : Form
        {
            private DominoGame game;

            public Form1()
            {
                InitializeComponent();
                game = new DominoGame();
                DrawGame();
            }
        
            private void DrawGame()
            {
                this.Controls.Clear();
        
                int x = 10;
                int y = 10;
        
                // Dibujar mesa
                Label tableLabel = new Label();
                tableLabel.Text = "Mesa:";
                tableLabel.Location = new Point(x, y);
                this.Controls.Add(tableLabel);
        
                x += 50;
                foreach (var tile in game.Table)
                {
                    Label tileLabel = new Label();
                    tileLabel.Text = tile.ToString();
                    tileLabel.BorderStyle = BorderStyle.FixedSingle;
                    tileLabel.Location = new Point(x, y);
                    tileLabel.Size = new Size(40, 30);
                    this.Controls.Add(tileLabel);
                    x += 45;
                }
        
                // Dibujar mano del jugador
                x = 10;
                y += 60;
        
                Label handLabel = new Label();
                handLabel.Text = "Tu mano:";
                handLabel.Location = new Point(x, y);
                this.Controls.Add(handLabel);
        
                x += 50;
                foreach (var tile in game.PlayerHand)
                {
                    Button tileButton = new Button();
                    tileButton.Text = tile.ToString();
                    tileButton.Size = new Size(60, 30);
                    tileButton.Location = new Point(x, y);
                    tileButton.Click += (s, e) => PlayTile(tile);
                    this.Controls.Add(tileButton);
                    x += 70;
                }
        
                // Botón para robar ficha
                Button drawButton = new Button();
                drawButton.Text = "Robar ficha";
                drawButton.Size = new Size(100, 30);
                drawButton.Location = new Point(10, y + 50);
                drawButton.Click += (s, e) => DrawTile();
                this.Controls.Add(drawButton);
            }

            private void PlayTile(DominoTile tile)
            {
                if (game.CanPlay(tile))
                {
                    game.PlayTile(tile);
                }
                else
                {
                    MessageBox.Show("No puedes jugar esa ficha.", "Movimiento inválido", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
                DrawGame();
            }
        
            private void DrawTile()
            {
                var drawn = game.DrawTile();
                if (drawn == null)
                {
                    MessageBox.Show("No hay más fichas para robar.", "Sin fichas", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                DrawGame();
            }
        }

📋 ¿Qué hace este proyecto?
Al iniciar, se reparten 7 fichas al jugador.

Puedes hacer clic en una ficha para jugarla si coincide con los extremos de la mesa.

Si no puedes jugar, puedes robar fichas del mazo.

Se valida si la ficha es jugable antes de colocarla.

