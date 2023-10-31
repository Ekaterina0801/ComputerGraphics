using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using task7_1;

namespace task7_1
{
    public partial class Form2 : Form
    {
        private Graphics g;
        private Bitmap bmp;

        private Primitive cur_primitive;

        private int count_start_points_in_rotation_figure;

        private AffineTransformer get_projection_transform()
        {
            switch (projectionComboBox.SelectedItem.ToString())
            {
                
                case "Изометрическая":
                    {
                        return AffineTransformer.IsometricProjection();
                    }
                case "Перспективная":
                    {
                        return AffineTransformer.PerspectiveProjection();
                    }
                case "Ортографическая XY":
                    {
                        return AffineTransformer.OrthographicXYProjection();
                    }
                case "Ортографическая YZ":
                    {
                        return AffineTransformer.OrthographicYZProjection();
                    }
                case "Ортографическая XZ":
                    {
                        return AffineTransformer.OrthographicXZProjection();
                    }
                default:
                    {
                        return AffineTransformer.IsometricProjection();
                    }
            }
        }

        //Координатные оси
        private void DrawScene(Graphics g, AffineTransformer t, int width, int height)
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

            if (cur_primitive != null)
                p.Add(cur_primitive);

            foreach (Primitive x in p)
            {
                try
                {
                    x.Draw(g, t, width, height);
                }
                catch (ArgumentException ex)
                {
                    numericUpDown1.Value = (decimal)-cur_primitive.Center.X;
                    numericUpDown2.Value = (decimal)-cur_primitive.Center.Y;
                    numericUpDown3.Value = (decimal)-cur_primitive.Center.Z;
                    Translate();
                    numericUpDown1.Value = 0;
                    numericUpDown2.Value = 0;
                    numericUpDown3.Value = 0;
                }
            }
        }

        private void Clear()
        {
            bmp = new Bitmap(pictureBox1.Width, pictureBox1.Height);
            g = Graphics.FromImage(bmp);
            pictureBox1.Image = bmp;
        }

        public Form2()
        {
            InitializeComponent();
            bmp = new Bitmap(pictureBox1.Width, pictureBox1.Height);
            g = Graphics.FromImage(bmp);
            pictureBox1.Image = bmp;
            projectionComboBox.SelectedItem = projectionComboBox.Items[1];
            ReflectionComboBox.SelectedItem = ReflectionComboBox.Items[0];
            rotationAxisCB.SelectedItem = rotationAxisCB.Items[1];
            DrawScene(g, get_projection_transform(), pictureBox1.Width, pictureBox1.Height);
        }

        private void ApplyProjection_Click(object sender, EventArgs e)
        {
            bmp = new Bitmap(pictureBox1.Width, pictureBox1.Height);
            g = Graphics.FromImage(bmp);
            pictureBox1.Image = bmp;
            DrawScene(g, get_projection_transform(), pictureBox1.Width, pictureBox1.Height);
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

        //Отражение
        private void Reflect()
        {
            switch (ReflectionComboBox.SelectedItem.ToString())
            {
                case "Отражение по X":
                    {
                        cur_primitive.MultiplyWithTransformMatr(AffineTransformer.ReflectX());
                        break;
                    }
                case "Отражение по Y":
                    {
                        cur_primitive.MultiplyWithTransformMatr(AffineTransformer.ReflectY());
                        break;
                    }
                case "Отражение по Z":
                    {
                        cur_primitive.MultiplyWithTransformMatr(AffineTransformer.ReflectZ());
                        break;
                    }
                default:
                    {
                        cur_primitive.MultiplyWithTransformMatr(AffineTransformer.ReflectX());
                        break;
                    }
            }
        }

        //Масштабирование относительно центра
        private void ScaleCenter()
        {
            double C = (double)numericUpDown10.Value;
            cur_primitive.MultiplyWithTransformMatr(AffineTransformer.Scale(C, C, C));
        }


        private void RotateLine()
        {
            double X1 = (double)numericUpDown14.Value;
            double Y1 = (double)numericUpDown15.Value;
            double Z1 = (double)numericUpDown16.Value;

            double X2 = (double)numericUpDown17.Value;
            double Y2 = (double)numericUpDown18.Value;
            double Z2 = (double)numericUpDown19.Value;

            Line3D l = new Line3D(new Point3D(X1, Y1, Z1), new Point3D(X2, Y2, Z2));

            double ang = (double)numericUpDown20.Value / 180 * Math.PI;

            cur_primitive.MultiplyWithTransformMatr(AffineTransformer.RotateLine(l, ang));
        }

        private void ApplyAffin_Click(object sender, EventArgs e)
        {
            Clear();
            Translate();
            Rotate();
            ScaleCenter();
            DrawScene(g, get_projection_transform(), pictureBox1.Width, pictureBox1.Height);
        }

        private void ApplyReflection_Click(object sender, EventArgs e)
        {
            Clear();
            Reflect();
            DrawScene(g, get_projection_transform(), pictureBox1.Width, pictureBox1.Height);
        }


        private void ApplyLineRotation_Click(object sender, EventArgs e)
        {
            Clear();
            RotateLine();

            DrawScene(g, get_projection_transform(), pictureBox1.Width, pictureBox1.Height);
        }

        private void AddPointBtn_Click(object sender, EventArgs e)
        {
            double x = (double)numericUpDown21.Value;
            double y = (double)numericUpDown22.Value;
            double z = (double)numericUpDown23.Value;
            numericUpDown21.Value = 0;
            numericUpDown22.Value = 0;
            numericUpDown23.Value = 0;
            listBox1.Items.Add(new Point3D(x, y, z));
        }

        private void RemovePointBtn_Click(object sender, EventArgs e)
        {
            if (listBox1.SelectedIndex == -1)
                return;
            listBox1.Items.RemoveAt(listBox1.SelectedIndex);
        }

        private void DrawRotationFigure_Click(object sender, EventArgs e)
        {
            Clear();
            List<Point3D> points = new List<Point3D>();

            foreach (var p in listBox1.Items)
                points.Add((Point3D)p);
            int axis = 0;
            count_start_points_in_rotation_figure = points.Count;
            switch (rotationAxisCB.SelectedItem.ToString())
            {
                case "OX":
                    {
                        axis = 0;
                        break;
                    }
                case "OY":
                    {
                        axis = 1;
                        break;
                    }
                case "OZ":
                    {
                        axis = 2;
                        break;
                    }
                default:
                    {
                        axis = 0;
                        break;
                    }
            }
            var density = (int)densityCountNumUpDown.Value;
            cur_primitive = new RotationFigure(points, axis, density);
            DrawScene(g, get_projection_transform(), pictureBox1.Width, pictureBox1.Height);
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
                    DialogResult rezult = MessageBox.Show("Ошибка сохранения",
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
                    List<Face> faces = new List<Face>();

                    string str = System.IO.File.ReadAllText(loadDialog.FileName).Replace("\r\n", "!");
                    string[] info = str.Split('!');
                    string type_of_primitive = info[0];
                    int cur_string = 3;
                    while (cur_string < info.Length && info[cur_string] != "")
                    {
                        string[] coordinates = info[cur_string].Split(' ');
                        points.Add(new Point3D(double.Parse(coordinates[0]), double.Parse(coordinates[1]), double.Parse(coordinates[2])));
                        cur_string += 2;
                    }

                    cur_string++;
                    do
                    {
                        cur_string++;
                        if (info[cur_string] == "")
                            break;

                        List<Point3D> v = new List<Point3D>();
                        while (cur_string < info.Length - 1 && info[cur_string] != "" && info[cur_string][0] != '#')
                        {
                            string[] coordinates = info[cur_string].Split(' ');
                            v.Add(new Point3D(double.Parse(coordinates[0]), double.Parse(coordinates[1]), double.Parse(coordinates[2])));
                            cur_string++;
                        }

                        faces.Add(new Face(v));
                        cur_string++;
                    }
                    while (cur_string < info.Length - 1);

                    switch (type_of_primitive)
                    {
                        case "Rotation Figure":
                            {
                                cur_primitive = new RotationFigure(points, faces, count_start_points_in_rotation_figure);
                                break;
                            }
                        default:
                            {
                                cur_primitive = new RotationFigure(points, faces, count_start_points_in_rotation_figure);
                                break;
                            }
                    }

                    DrawScene(g, get_projection_transform(), pictureBox1.Width, pictureBox1.Height);
                }
                catch
                {
                    DialogResult rezult = MessageBox.Show("Невозможно открыть выбранный файл",
                    "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }

            }
        }
        private void Form2_Load(object sender, EventArgs e)
        {

        }
    }
}