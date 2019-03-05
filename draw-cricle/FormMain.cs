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

                DDA(centro, radio);
                //bresenham(initial, final);
                centro.X = centro.Y = -1;
                radio.X = radio.Y = -1;
            }
        }

        private void DDA(Point centro, Point radio)
        {
            double r = 0;
            int xc = 0;
            int yc = 0;
            int xf = 0;
            int yf = 0;
            int xk = 0;
            double yk = 0;
            int x = 0;
            int y = 0;
            int a = 0;
            int b = 0;


            xc = centro.X;
            yc = centro.Y;
            xf = radio.X;
            yf = radio.Y;

            a = xf - xc;
            b = yf - yc;


            r = Math.Sqrt(Math.Pow(a, 2) + Math.Pow(b, 2));
            r = Math.Round(r);
            r = Math.Abs(r);

            yk = Math.Sqrt(Math.Pow(r, 2) - Math.Pow(xk, 2));
            yk = Math.Round(yk);

            for (xk = (int)-r; xk <= r; xk++)
            {
                yk = Math.Sqrt(Math.Pow(r, 2) - Math.Pow(xk, 2));
                yk = Math.Round(yk);
                x = xk + xc;
                y = (int)yk + yc;
                int y2 = (int)-yk + yc;

                if ((y < bmp.Height && y > 0) && (y2 < bmp.Height && y2 > 0)) 
                {
                    if(x < bmp.Width && x > 0)
                    {
                        bmp.SetPixel(x, y, Color.Red); // Oct2
                        bmp.SetPixel(x, y2, Color.Red); // Oct2
                    }
                }
            }

            pictureBox1.Image = bmp;
        }
    }
}
