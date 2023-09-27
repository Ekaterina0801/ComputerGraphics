using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Net.Mime.MediaTypeNames;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.Button;

namespace lab3
{
    public partial class RecA : Form
    {
        private Point start;
        private bool drawing = false;
        int left, right, up, down;
        Color c;
        OpenFileDialog open_dialog;
        private Graphics g;
        Pen needColor;
        Bitmap back;
        List<Tuple<Point, Point>> l = new List<Tuple<Point, Point>>();

        public RecA()
        {
            InitializeComponent();
            radioButton1.Checked = true;
            back = new Bitmap(pictureBox.Width, pictureBox.Height);


            //создаем фон
            Bitmap b = new Bitmap(pictureBox.Width, pictureBox.Height);
            pictureBox.Image = b;
            g = Graphics.FromImage(back);
            needColor = new Pen(Color.Red);
            //Clear(); // очищаем pictureBox
        }

        public Bitmap ResizeBitmap(Bitmap bmp, int width, int height)
        {
            Bitmap result = new Bitmap(width, height);
            using (Graphics g = Graphics.FromImage(result))
            {
                g.DrawImage(bmp, 0, 0, width, height);
            }

            return result;
        }
        //поиск границ
        private void findBorders(Point our_p, ref Point leftBound, ref Point rightBound, Bitmap b, Color c)
        {
            while (leftBound.X > 0 && equalColors(b.GetPixel(leftBound.X, leftBound.Y), c))
            {
                leftBound.X -= 1;
            }

            while (rightBound.X < b.Width && equalColors(b.GetPixel(rightBound.X, rightBound.Y), c))
                rightBound.X += 1;
        }
        //проверяем цвета на равенство
        private bool equalColors(Color c1, Color c2)
        {
            return c1.R == c2.R && c1.G == c2.G && c1.B == c2.B;
        }

        private void byFilling(Point p)
        {
            int back_av = back.Width / 2;
            int back_yav = back.Height / 2;

            int x_av = back_av - p.X;
            int y_av = back_yav - p.Y;

            //var g = Graphics.FromImage(pictureBox.Image);
            foreach (var t in l)
            {
                if (t.Item1.X < t.Item2.X)
                {
                    Rectangle r = new Rectangle(t.Item1.X + 1 + x_av, t.Item1.Y + y_av, t.Item2.X - t.Item1.X - 1, 1);
                    Bitmap line = back.Clone(r, back.PixelFormat); //копируем линию из заданного изображения

                    r = new Rectangle(t.Item1.X + 1, t.Item1.Y, t.Item2.X - t.Item1.X - 1, 1);
                    g.DrawImage(line, r);
                    pictureBox.Image = pictureBox.Image;
                }
            }
        }
        //заливка
        private void Fill(Point p)
        {
            Color formColor = back.GetPixel(p.X, p.Y);
            if (0 <= p.X && p.X < back.Width && 0 <= p.Y && p.Y < back.Height - 1 && !equalColors(formColor, Color.Black) &&
                !equalColors(formColor, Color.Red))
            {
                Point leftBound = new Point(p.X, p.Y);
                Point rightBound = new Point(p.X, p.Y);
                Color currentColor = formColor;
                while (0 < leftBound.X && !equalColors(currentColor, Color.Black))
                {
                    leftBound.X -= 1;
                    currentColor = back.GetPixel(leftBound.X, p.Y);
                }
                currentColor = formColor;
                while (rightBound.X < pictureBox1.Width - 1 && !equalColors(currentColor, Color.Black))
                {
                    rightBound.X += 1;
                    currentColor = back.GetPixel(rightBound.X, p.Y);
                }
                if (leftBound.X != 0)
                    leftBound.X += 1;
                rightBound.X -= 1;
                if (rightBound.X - leftBound.X == 0)
                    back.SetPixel(rightBound.X, rightBound.Y, Color.Red);
                g.DrawLine(needColor, leftBound, rightBound);
                pictureBox1.Image = back;
                for (int i = leftBound.X; i < rightBound.X + 1; ++i)
                    Fill(new Point(i, p.Y + 1));
                for (int i = leftBound.X; i < rightBound.X + 1; ++i)
                    if (p.Y > 0)
                        Fill(new Point(i, p.Y - 1));
            }
        }

        private void pictureBox_MouseDown(object sender, MouseEventArgs e)
        {
            start = new Point(e.X, e.Y);
            if (radioButton1.Checked) //рисуем
            {
         
                drawing = true;
            }
            else
            {
                left = e.Location.X;
                right = e.Location.X;
                up = e.Location.Y;
                down = e.Location.Y;

                Fill(start);

                //byFilling(start);
                l.Clear();
            }
        }
        private void pictureBox_MouseMove(object sender, MouseEventArgs e)
        {
            if (!drawing) return;
            var finish = new Point(e.X, e.Y);
            var pen = new Pen(Color.Black, 1f);

            var g = Graphics.FromImage(pictureBox.Image);
            g.DrawLine(pen, start, finish);
            pen.Dispose();
            g.Dispose();
            pictureBox.Image = pictureBox.Image;
            pictureBox1.Invalidate();
            start = finish;
        }

        private void pictureBox_MouseUp(object sender, MouseEventArgs e)
        {
            drawing = false;
        }

        private void Clear()
        {
            var g = Graphics.FromImage(pictureBox.Image);
            g.Clear(pictureBox.BackColor);
            pictureBox.Image = pictureBox.Image;
        }
        private void button1_Click(object sender, EventArgs e)
        {
            Clear();
        }

        private Bitmap multiplyImage(Bitmap im, int byWidth, int byHeight)
        {
            Bitmap res = new Bitmap(im);

            int width = res.Size.Width / byWidth;
            int height = res.Size.Height / byHeight;

            //Создаём фрагмент - изначальную картинку, но меньшей ширины и высоты
            Bitmap fragment = ResizeBitmap(res, width, height);
            Rectangle r;
            var g = Graphics.FromImage(res);
            for (int w = 0; w < byWidth; ++w)
            {
                for (int h = 0; h < byHeight; ++h)
                {
                    //Находим координаты для рисования
                    int x = width * w;
                    int y = height * h;

                    r = new Rectangle(x, y, width, height);
                    //Рисуем поверх исходного изображения
                    g.DrawImage(fragment, r);
                }
            }
            res = res;
            return res;
        }
        private void button2_Click(object sender, EventArgs e)
        {
            open_dialog = new OpenFileDialog(); //создание диалогового окна для выбора файла
            open_dialog.Filter = "Image Files(*.BMP;*.JPG;**.PNG)|*.BMP;*.JPG;**.PNG|All files (*.*)|*.*"; //формат загружаемого файла
            DialogResult dr = open_dialog.ShowDialog();
            if (dr == DialogResult.OK)
            {
                Bitmap b = new Bitmap(open_dialog.FileName);
                //Получаем увеличенную картинку
                back = new Bitmap(b, pictureBox.Size.Width * 3, pictureBox.Size.Height * 3);
                pictureBox1.Image = new Bitmap(b, pictureBox1.Size);

                //Получаем расклонированную картинку
                back = new Bitmap(multiplyImage(back, 3, 3));
            }
        }


        private void radioButton1_MouseClick(object sender, MouseEventArgs e)
        {
            if (radioButton2.Checked)
            {
                radioButton1.Checked = true;
                radioButton2.Checked = false;
            }
        }
        private void radioButton2_MouseClick(object sender, MouseEventArgs e)
        {
            if (radioButton1.Checked)
            {
                radioButton2.Checked = true;
                radioButton1.Checked = false;
            }
        }
    }

}