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
        const int width = 500;
        const int height = 500;

        public FormMain()
        {
            centro = new Point(-1, -1);
            radio = new Point(-1, -1);
            bmp = new Bitmap(width, height);
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

                DDA(centro, radio);
                //bresenham(initial, final);
                centro.X = centro.Y = -1;
                radio.X = radio.Y = -1;
            }
        }

        // function to draw all other 7 pixels 
        // present at symmetric position 
        private void setPixelCircle(int xc, int yc, int x, int y, Color color)
        {
            if((xc + x) <= pictureBox1.Width && (xc + x) >= 0)
            {
                if((yc + y) <= pictureBox1.Height && (yc + y) >= 0)
                {
                    bmp.SetPixel(xc + x, yc + y, color);
                }
            }
            if ((xc - x) <= pictureBox1.Width && (xc - x) >= 0)
            {
                if ((yc + y) <= pictureBox1.Height && (yc + y) >= 0)
                {
                    bmp.SetPixel(xc - x, yc + y, color);
                }
            }
            if ((xc + x) <= pictureBox1.Width && (xc + x) >= 0)
            {
                if ((yc - y) <= pictureBox1.Height && (yc - y) >= 0)
                {
                    bmp.SetPixel(xc + x, yc - y, color);
                }
            }
            if ((xc - x) <= pictureBox1.Width && (xc - x) >= 0)
            {
                if ((yc - y) <= pictureBox1.Height && (yc - y) >= 0)
                {
                    bmp.SetPixel(xc - x, yc - y, color);
                }
            }
            if ((xc + y) <= pictureBox1.Width && (xc + y) >= 0)
            {
                if ((yc + x) <= pictureBox1.Height && (yc + x) >= 0)
                {
                    bmp.SetPixel(xc + y, yc + x, color);
                }
            }
            if ((xc - y) <= pictureBox1.Width && (xc - y) >= 0)
            {
                if ((yc + x) <= pictureBox1.Height && (yc + x) >= 0)
                {
                    bmp.SetPixel(xc - y, yc + x, color);
                }
            }
            if ((xc + y) <= pictureBox1.Width && (xc + y) >= 0)
            {
                if ((yc - x) <= pictureBox1.Height && (yc - x) >= 0)
                {
                    bmp.SetPixel(xc + y, yc - x, color);
                }
            }
            if ((xc - y) <= pictureBox1.Width && (xc - y) >= 0)
            {
                if ((yc - x) <= pictureBox1.Height && (yc - x) >= 0)
                {
                    bmp.SetPixel(xc - y, yc - x, color);
                }
            }
            //bmp.SetPixel(xc + x, yc + y, color);
            //bmp.SetPixel(xc - x, yc + y, color);
            //bmp.SetPixel(xc + x, yc - y, color);
            //bmp.SetPixel(xc - x, yc - y, color);
            //bmp.SetPixel(xc + y, yc + x, color);
            //bmp.SetPixel(xc - y, yc + x, color);
            //bmp.SetPixel(xc + y, yc - x, color);
            //bmp.SetPixel(xc - y, yc - x, color);
        }

        private void DDA(Point centro, Point radio)
        {
            double r = 0;
            double yk = 0;
            int xk = 0;
            int xc = 0;
            int yc = 0;
            int xf = 0;
            int yf = 0;
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

            for (xk = 0; xk <= yk; xk++)
            {
                yk = Math.Sqrt(Math.Pow(r, 2) - Math.Pow(xk, 2));
                yk = Math.Round(yk);
                
                setPixelCircle(xc, yc, xk, (int)yk, Color.Red);
            }

            pictureBox1.Image = bmp;
        }

        private void bresenham(Point centro, Point radio)
        {

        }
    }
}
