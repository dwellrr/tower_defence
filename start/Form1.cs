using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using library;

namespace start
{
    public partial class Form1 : Form
    {
        Timer graphicsTimer;
        GameLoop loop;
      
        public Form1()
        {
            InitializeComponent();

            Paint += Form1_Paint;

            graphicsTimer = new Timer();
            graphicsTimer.Interval = 1000 / 120;
            graphicsTimer.Tick += GraphicsTimer_Tick;


        }

        private void Form1_Load(object sender, EventArgs e)
        {
            GameCore game = new GameCore();
            loop = new GameLoop();
            loop.Load(game);
            loop.Start();
            Point p = Cursor.Position;
            loop.inp.p = p;

            graphicsTimer.Start();

        }

        private void Form1_Paint(object sender, PaintEventArgs e)
        {
            loop.Draw(e.Graphics);


        }

        private void GraphicsTimer_Tick(object sender, EventArgs e)
        {
            Invalidate();
            label1.Text = "Money: " + loop.money;
        }

        private void Form1_MouseClick(object sender, MouseEventArgs e)
        {
            Point p = Cursor.Position;
            loop.inp.isPressed = true;
            loop.inp.p = p;
        }


        private void label1_Click(object sender, EventArgs e)
        {

        }
    }

}
