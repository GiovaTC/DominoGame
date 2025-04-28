using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;

namespace DominoGame
{
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

            int x = 60;
            int y = 80;

            // Dibujar mesa
            Label tableLabel = new Label();
            tableLabel.Text = "Mesa:";
            tableLabel.Location = new Point(x, y);
            this.Controls.Add(tableLabel);

            x += 130;
            foreach (var tile in game.Table)
            {
                Label tileLabel = new Label();
                tileLabel.Text = tile.ToString();
                tileLabel.BorderStyle = BorderStyle.FixedSingle;
                tileLabel.Location = new Point(x, y);
                tileLabel.Size = new Size(60, 30);
                this.Controls.Add(tileLabel);
                x += 45;
            }

            // Dibujar mano del jugador
            x = 60;
            y += 120;

            Label handLabel = new Label();
            handLabel.Text = "Tu mano:";
            handLabel.Location = new Point(x, y);
            this.Controls.Add(handLabel);

            x += 120;
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
            drawButton.Text = "Tomar ficha";
            drawButton.Size = new Size(100, 30);
            drawButton.Location = new Point(40, y + 80);
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
}

