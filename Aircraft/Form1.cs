using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Aircraft
{
    public partial class Form1 : Form
    {
        GameController controller;
        public Form1()
        {
            InitializeComponent();

            //plContainer.BackgroundImage = new Bitmap("resource/bg.png");
            //plContainer.BackgroundImageLayout = ImageLayout.Stretch;
            plContainer.Paint += PlContainer_Paint;

            controller = new Aircraft.GameController(plContainer);

            controller.GenerateSelf();
            controller.GenerateEnemy();
            controller.Begin();

            this.KeyDown += Form1_KeyDown;
            this.KeyUp += Form1_KeyUp;
        }

        private void Form1_KeyUp(object sender, KeyEventArgs e)
        {
            controller.Listening(e.KeyCode, "UP");
        }

        private void Form1_KeyDown(object sender, KeyEventArgs e)
        {
            controller.Listening(e.KeyCode, "DOWN");
        }

        private void PlContainer_Paint(object sender, PaintEventArgs e)
        {
            PaintContainer(e.Graphics);
            plContainer.Invalidate();
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void PaintContainer(Graphics g)
        {
            controller.Draw(g);
        }
        
    }
}
