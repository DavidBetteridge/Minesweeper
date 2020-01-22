using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Minesweeper
{
    public partial class Form1 : Form
    {
        private readonly Game _game;
        private readonly GameDrawer _gameDrawer;

        public Form1()
        {
            InitializeComponent();

            this.Text = "Minesweeper";
            this.Size = new Size(1000, 1000);

            _game = new Game();
            _gameDrawer = new GameDrawer(_game);
            this.Paint += Form1_Paint;
        }

        private void Form1_Paint(object sender, PaintEventArgs e)
        {
            _gameDrawer.Draw(e.Graphics);
        }
    }
}
