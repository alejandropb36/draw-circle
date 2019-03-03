using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace draw_cricle
{
    public partial class FormMain : Form
    {
        Point centro, radio;
        Bitmap bmp;

        public FormMain()
        {
            centro = new Point(-1, -1);
            radio = new Point(-1, -1);
            bmp = new Bitmap(500, 500);
            InitializeComponent();
        }

        private void buttonClear_Click(object sender, EventArgs e)
        {
            centro.X = centro.Y = -1;
            radio.X = radio.Y = -1;

            Graphics g = Graphics.FromImage(bmp);
            g.FillRectangle(Brushes.White, 0, 0, 500, 500);
            pictureBox1.Image = bmp;

            labelAdd.Text = "ADD: 0 s";
            labelBresenham.Text = "Bresenham: 0 s";
        }

        private void pictureBox1_MouseClick(object sender, MouseEventArgs e)
        {
            Pen pen = new Pen(Color.Orange, 3);

            if (centro.X == -1)
            {
                centro.X = e.X;
                centro.Y = e.Y;
                pictureBox1.CreateGraphics().DrawEllipse(pen, centro.X, centro.Y, 3, 3);
            }
            else
            {
                radio.X = e.X;
                radio.Y = e.Y;
                pictureBox1.CreateGraphics().DrawEllipse(pen, radio.X, radio.Y, 3, 3);

                Console.WriteLine("------------- Puntos ------------------");
                Console.WriteLine("punto inicial: (" + centro.X + ", " + centro.Y + ")");
                Console.WriteLine("punto final: (" + radio.X + ", " + radio.Y + ")");

                //DDA(initial, final);
                //bresenham(initial, final);
                centro.X = centro.Y = -1;
                radio.X = radio.Y = -1;
            }
        }
    }
}
