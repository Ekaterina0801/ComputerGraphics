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

namespace lab3
{
    public partial class ReccurC : Form
    {
        private Bitmap image;
        private List<Point> borderPoints;
        public Point start = new Point(-1,-1);
        Color borderColor;
        public ReccurC()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {

            // Загрузка изображения
            image = Properties.Resources.Border1;
            for (int y = 0; y < image.Height; y++)
            {
                for (int x = 0; x<image.Width;x++)
                {
                    Color pixel = image.GetPixel(x, y);
                    int red = pixel.R;
                    int green = pixel.G;
                    int blue = pixel.B;
                    if (red == 0 & green == 0 && blue == 0)
                    {
                        start = new Point(x, y);
                        borderColor = pixel;
                        break;
                    }
                }
            }
            pictureBox.Image = image;
            // Создание списка для точек границы
            borderPoints = new List<Point>();
            // Занесение точек границы в список в порядке обхода
            borderPoints.Add(start);
            TraverseBorder(start);
            // Перерисовка pictureBox с обновленным изображением
            pictureBox.Image = DrawBorder(image, borderPoints);

        }


        private void TraverseBorder(Point start)
        {
            // Массивы для проверки соседних пикселей
            int[] dx = { 1, 0, -1, 0 };
            int[] dy = { 0, 1, 0, -1 };

            int width = image.Width;
            int height = image.Height;

            // Начальное направление движения
            int direction = 0;

            Point current = start;

            do
            {
                // Добавление текущей точки в список границы
                borderPoints.Add(current);

                // Поиск следующей точки границы
                for (int i = 0; i < 4; i++)
                {
                    int newX = current.X + dx[direction];
                    int newY = current.Y + dy[direction];

                    // Если следующая точка не выходит за границы изображения и имеет цвет границы
                    if (newX >= 0 && newX < width && newY >= 0 && newY < height)
                    {
                        Color pixel = image.GetPixel(newX, newY);
                        int red = pixel.R;
                        int green = pixel.G;
                        int blue = pixel.B;
                        if (red == 0 & green == 0 && blue == 0)
                        {
                            current = new Point(newX, newY);
                            direction = (direction + 3) % 4; // Изменение направления движения налево (поворот против часовой стрелки)
                            break;
                        }
                        
                    }

                    direction = (direction + 1) % 4; // Изменение направления движения направо (поворот по часовой стрелке)
                }
            } while (current != start);
        }

        private Bitmap DrawBorder(Bitmap image, List<Point> borderPoints)
        {
            Bitmap result = new Bitmap(image);

            foreach (Point p in borderPoints)
            {
                result.SetPixel(p.X, p.Y, Color.Red);
            }

            return result;
        }
    }
}
