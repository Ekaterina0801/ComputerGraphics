﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace task7_1
{
    public partial class Form4 : Form
    {
        private Graphics perspective_g;
        private Bitmap perspective_bmp;

        private Primitive cur_primitive;

        private int count_start_points_in_rotation_figure;


        private AffineTransformer get_perpective_transform()
        {
            switch (PerspectiveComboBox.SelectedItem.ToString())
            {
                case "Перспективная":
                    {
                        return AffineTransformer.PerspectiveProjection();
                    }
                case "Изометрическая":
                    {
                        return AffineTransformer.IsometricProjection();
                    }
                default:
                    {
                        return AffineTransformer.PerspectiveProjection();
                    }
            }
        }

        //Рисует координатные оси 
        private void DrawAxis(Graphics g, AffineTransformer t, int width, int height)
        {
            List<Primitive> p = new List<Primitive>();
            Point3D a = new Point3D(0, 0, 0);
            Point3D b = new Point3D(0.8, 0, 0);
            Point3D c = new Point3D(0, 0.8, 0);
            Point3D d = new Point3D(0, 0, 0.8);

            p.Add(a);
            p.Add(b);
            p.Add(c);
            p.Add(d);

            p.Add(new Line3D(a, b));
            p.Add(new Line3D(a, c));
            p.Add(new Line3D(a, d));

            p.Add(cur_primitive);

            foreach (Primitive x in p)
            {
                x.Draw(g, t, width, height);
            }
        }

        private void GetPrimitive()
        {
            Tuple<double, double, double, double> span = Tuple.Create((double)numericUpDown15.Value,
                (double)numericUpDown16.Value, (double)numericUpDown17.Value, (double)numericUpDown18.Value);

            switch (PrimitiveComboBox.Text)
            {
                case "(x * x * y) / ((x * x * x * x + y * y) - 0.01)":
                    {
                        cur_primitive = new Plot(PrimitiveComboBox.Text, span, (double)numericUpDown14.Value);
                        break;
                    }
                case "(x * x) + (y * y)":
                    {
                        cur_primitive = new Plot(PrimitiveComboBox.Text, span, (double)numericUpDown14.Value);
                        break;
                    }
                case "x + y":
                    {
                        cur_primitive = new Plot(PrimitiveComboBox.Text, span, (double)numericUpDown14.Value);
                        break;
                    }
                default:
                    {
                        cur_primitive = new Plot(PrimitiveComboBox.Text, span, (double)numericUpDown14.Value);
                        break;
                    }
            }
        }

        private void Clear()
        {
            perspective_bmp = new Bitmap(PerspectiveBox.Width, PerspectiveBox.Height);
            perspective_g = Graphics.FromImage(perspective_bmp);
            PerspectiveBox.Image = perspective_bmp;
        }

        public Form4()
        {
            InitializeComponent();

            //Создаем Bitmap и Graphics для двух PictureBox
            perspective_bmp = new Bitmap(PerspectiveBox.Width, PerspectiveBox.Height);
            perspective_g = Graphics.FromImage(perspective_bmp);
            PerspectiveBox.Image = perspective_bmp;

            //Инициализируем ComboBox для отображения проекций
            PerspectiveComboBox.SelectedItem = PerspectiveComboBox.Items[1];
            PrimitiveComboBox.SelectedItem = PrimitiveComboBox.Items[0];

            GetPrimitive();
            DrawAxis(perspective_g, get_perpective_transform(), PerspectiveBox.Width, PerspectiveBox.Height);
        }

        private void ApplyPerspective_Click(object sender, EventArgs e)
        {
            perspective_bmp = new Bitmap(PerspectiveBox.Width, PerspectiveBox.Height);
            perspective_g = Graphics.FromImage(perspective_bmp);
            PerspectiveBox.Image = perspective_bmp;
            DrawAxis(perspective_g, get_perpective_transform(), PerspectiveBox.Width, PerspectiveBox.Height);
        }

        private void ApplyPrimitive_Click(object sender, EventArgs e)
        {
            Clear();
            GetPrimitive();
            DrawAxis(perspective_g, get_perpective_transform(), PerspectiveBox.Width, PerspectiveBox.Height);
        }

        //Смещение
        private void Translate()
        {
            double X = (double)numericUpDown1.Value;
            double Y = (double)numericUpDown2.Value;
            double Z = (double)numericUpDown3.Value;
            cur_primitive.MultiplyWithTransformMatr(AffineTransformer.Translate(X, Y, Z));
        }

        //Поворот
        private void Rotate()
        {
            double X = (double)numericUpDown4.Value / 180 * Math.PI;
            double Y = (double)numericUpDown5.Value / 180 * Math.PI;
            double Z = (double)numericUpDown6.Value / 180 * Math.PI;
            cur_primitive.MultiplyWithTransformMatr(AffineTransformer.RotateX(X) * AffineTransformer.RotateY(Y) * AffineTransformer.RotateZ(Z));
        }

        //Масштаб
        private void Scale()
        {
            double X = (double)numericUpDown7.Value;
            double Y = (double)numericUpDown8.Value;
            double Z = (double)numericUpDown9.Value;
            cur_primitive.MultiplyWithTransformMatr(AffineTransformer.Scale(X, Y, Z));

        }

        private void ApplyAffin_Click(object sender, EventArgs e)
        {
            Clear();
            Translate();
            Rotate();
            Scale();

            DrawAxis(perspective_g, get_perpective_transform(), PerspectiveBox.Width, PerspectiveBox.Height);
        }

        private void SaveButton_Click(object sender, EventArgs e)
        {
            SaveFileDialog saveDialog = new SaveFileDialog();
            saveDialog.Filter = "Object Files(*.obj)| *.obj | Text files(*.txt) | *.txt | All files(*.*) | *.* ";
            if (saveDialog.ShowDialog() == DialogResult.OK)
            {
                try
                {
                    string info = "";

                    info += cur_primitive.ToString() + "\r\n" + "\r\n";

                    int num = 1;
                    foreach (Point3D point in cur_primitive.Points)
                    {
                        info += "Point #" + num;
                        info += "\r\n";
                        info += point.X + " ";
                        info += point.Y + " ";
                        info += point.Z;
                        info += "\r\n";
                        ++num;
                    }
                    info += "# " + cur_primitive.Points.Count + " points\r\n";
                    info += "\r\n";

                    num = 1;
                    foreach (Face v in cur_primitive.Faces)
                    {
                        info += v.ToString() + " #" + num;

                        info += "\r\n";
                        for (int i = 0; i < v.Points.Count; ++i)
                        {
                            info += v.Points[i].X + " " + v.Points[i].Y + " " + v.Points[i].Z;
                            info += "\r\n";
                        }
                        if (num != cur_primitive.Faces.Count)
                            info += "\r\n";
                        ++num;
                    }
                    info += "# " + cur_primitive.Faces.Count + " Faces\r\n";

                    System.IO.File.WriteAllText(saveDialog.FileName, info);
                }
                catch
                {
                    DialogResult rezult = MessageBox.Show("Невозможно сохранить файл",
                    "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void LoadButton_Click(object sender, EventArgs e)
        {
            OpenFileDialog loadDialog = new OpenFileDialog();
            loadDialog.Filter = "Object Files(*.obj)|*.obj|Text files (*.txt)|*.txt|All files (*.*)|*.*";
            if (loadDialog.ShowDialog() == DialogResult.OK)
            {
                try
                {
                    Clear();
                    List<Point3D> points = new List<Point3D>();
                    List<Face> Faces = new List<Face>();

                    string str = System.IO.File.ReadAllText(loadDialog.FileName).Replace("\r\n", "!");
                    string[] info = str.Split('!');

                    string type_of_primitive = info[0];
                    if (type_of_primitive.Contains("Rotation Figure"))
                    {
                        string[] tmp = type_of_primitive.Split(' ');
                        type_of_primitive = tmp[0] + " " + tmp[1];
                        count_start_points_in_rotation_figure = int.Parse(tmp[2]);
                    }

                    int cur_string = 3;
                    while (cur_string < info.Length && info[cur_string] != "")
                    {
                        string[] coordinates = info[cur_string].Split(' ');

                        double x = double.Parse(coordinates[0]);
                        double y = double.Parse(coordinates[1]);
                        double z = double.Parse(coordinates[2]);
                        points.Add(new Point3D(x, y, z));
                        cur_string += 2;
                    }

                    cur_string++;
                    do
                    {
                        cur_string++;
                        if (info[cur_string] == "")
                            break;

                        List<Point3D> vertices = new List<Point3D>();
                        while (cur_string < info.Length - 1 && info[cur_string] != "" && info[cur_string][0] != '#')
                        {
                            string[] coordinates = info[cur_string].Split(' ');

                            double x = double.Parse(coordinates[0]);
                            double y = double.Parse(coordinates[1]);
                            double z = double.Parse(coordinates[2]);
                            vertices.Add(new Point3D(x, y, z));
                            cur_string++;
                        }

                        Faces.Add(new Face(vertices));
                        cur_string++;
                    }
                    while (cur_string < info.Length - 1);

                    switch (type_of_primitive)
                    {
                        case "Plot":
                            {
                                cur_primitive = new Plot(points);
                                break;
                            }
                        default:
                            {
                                cur_primitive = new Plot(points);
                                break;
                            }
                    }

                    DrawAxis(perspective_g, get_perpective_transform(), PerspectiveBox.Width, PerspectiveBox.Height);
                }
                catch
                {
                    DialogResult rezult = MessageBox.Show("Невозможно открыть выбранный файл",
                    "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }

            }
        }
    }
}