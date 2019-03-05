using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
// Add for use Timer
using System.Diagnostics;

namespace draw_cricle
{
    public partial class FormMain : Form
    {
        const int width = 500;
        const int height = 500;
        Point centro, radio;
        Bitmap bmp;
        Stopwatch sw;

        public FormMain()
        {
            centro = new Point(-1, -1);
            radio = new Point(-1, -1);
            bmp = new Bitmap(width, height);
            sw = new Stopwatch();
            InitializeComponent();
        }

        private void buttonClear_Click(object sender, EventArgs e)
        {
            centro.X = centro.Y = -1;
            radio.X = radio.Y = -1;

            Graphics g = Graphics.FromImage(bmp);
            g.FillRectangle(Brushes.White, 0, 0, width, height);
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

                sw.Restart();
                DDA(centro, radio);
                labelAdd.Text = "ADD: " + String.Format("{0}", sw.Elapsed.TotalMilliseconds) + " s";

                sw.Restart();
                bresenham(centro, radio);
                labelBresenham.Text = "Bresenham: " + String.Format("{0}", sw.Elapsed.TotalMilliseconds) + " s";

                centro.X = centro.Y = -1;
                radio.X = radio.Y = -1;
            }
        }

        /*
         * Funcion de dibujo de pixeles simetricos
         * en los 8 octantes del circulo.
         * Con verificación de existencia
         */
        private void setPixelCircle(int xc, int yc, int x, int y, Color color)
        {
            if((xc + x) < pictureBox1.Width && (xc + x) >= 0)
            {
                if((yc + y) < pictureBox1.Height && (yc + y) >= 0)
                {
                    bmp.SetPixel(xc + x, yc + y, color);
                }
            }
            if ((xc - x) < pictureBox1.Width && (xc - x) >= 0)
            {
                if ((yc + y) < pictureBox1.Height && (yc + y) >= 0)
                {
                    bmp.SetPixel(xc - x, yc + y, color);
                }
            }
            if ((xc + x) < pictureBox1.Width && (xc + x) >= 0)
            {
                if ((yc - y) < pictureBox1.Height && (yc - y) >= 0)
                {
                    bmp.SetPixel(xc + x, yc - y, color);
                }
            }
            if ((xc - x) < pictureBox1.Width && (xc - x) >= 0)
            {
                if ((yc - y) < pictureBox1.Height && (yc - y) >= 0)
                {
                    bmp.SetPixel(xc - x, yc - y, color);
                }
            }
            if ((xc + y) < pictureBox1.Width && (xc + y) >= 0)
            {
                if ((yc + x) < pictureBox1.Height && (yc + x) >= 0)
                {
                    bmp.SetPixel(xc + y, yc + x, color);
                }
            }
            if ((xc - y) < pictureBox1.Width && (xc - y) >= 0)
            {
                if ((yc + x) < pictureBox1.Height && (yc + x) >= 0)
                {
                    bmp.SetPixel(xc - y, yc + x, color);
                }
            }
            if ((xc + y) < pictureBox1.Width && (xc + y) >= 0)
            {
                if ((yc - x) < pictureBox1.Height && (yc - x) >= 0)
                {
                    bmp.SetPixel(xc + y, yc - x, color);
                }
            }
            if ((xc - y) < pictureBox1.Width && (xc - y) >= 0)
            {
                if ((yc - x) < pictureBox1.Height && (yc - x) >= 0)
                {
                    bmp.SetPixel(xc - y, yc - x, color);
                }
            }
        }

        /*
         * Algoritmo de trasado de circunferencia con DDA
         * yk = Raiz( r^2 - xk^2)
         * 
         */
        private void DDA(Point centro, Point radio)
        {
            Stopwatch sw = new Stopwatch();
            double r = 0;
            double yk = 0;
            int xk = 0;
            int xc = centro.X;
            int yc = centro.Y;
            int xf = radio.X;
            int yf = radio.Y;
            int a = xf - xc;
            int b = yf - yc;

            r = Math.Sqrt(Math.Pow(a, 2) + Math.Pow(b, 2));
            r = Math.Round(r);
            r = Math.Abs(r);

            yk = Math.Sqrt(Math.Pow(r, 2) - Math.Pow(xk, 2));
            yk = Math.Round(yk);

            for (xk = 0; xk <= yk; xk++)
            {
                yk = Math.Sqrt(Math.Pow(r, 2) - Math.Pow(xk, 2));
                yk = Math.Round(yk);
                
                setPixelCircle(xc, yc, xk, (int)yk, Color.Red);
            }

            pictureBox1.Image = bmp;
        }

        /*
         * Algoritmo de trasado de circunferencia con Bresenham o MidPoint
         */
        private void bresenham(Point centro, Point radio)
        {
            Stopwatch sw = new Stopwatch();
            double r = 0;
            int xc = centro.X;
            int yc = centro.Y;
            int xf = radio.X;
            int yf = radio.Y;
            int a = xf - xc;
            int b = yf - yc;
            int x = 0, y = 0, e = 0;

            
            r = Math.Sqrt(Math.Pow(a, 2) + Math.Pow(b, 2));
            r = Math.Round(r);
            r = Math.Abs(r);

            x = (int)r;
            while(y <= x)
            {
                setPixelCircle(xc, yc, x, y, Color.Blue);
                e = e + (2 * y) + 1;
                y = y + 1;
                if((2 * e) > ((2 * x) - 1))
                {
                    x = x - 1;
                    e = e - (2 * x) + 1;
                }
            }

            pictureBox1.Image = bmp;
        }
    }
}
