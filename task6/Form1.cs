using lab63D;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace lab63D
{
    public partial class Form1 : Form
    {
        private Graphics perspective_g;
        private Bitmap perspective_bmp;

        private Graphics orthographic_g;
        private Bitmap orthographic_bmp;

        private Primitive cur_primitive;


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

        private AffineTransformer get_orthographic_transform()
        {
            switch (OrthographicComboBox.SelectedItem.ToString())
            {
                case "Ортографическая XY":
                    {
                        return AffineTransformer.OrthographicXYProjection();
                    }
                case "Ортографическая XZ":
                    {
                        return AffineTransformer.OrthographicXZProjection();
                    }
                case "Ортографическая YZ":
                    {
                        return AffineTransformer.OrthographicYZProjection();
                    }
                default:
                    {
                        return AffineTransformer.OrthographicXYProjection();
                    }
            }
        }

        //Рисует координатные оси 
        private void DrawAxis(Graphics g, AffineTransformer t, int width, int height)
        {
            Point3D a = new Point3D(0, 0, 0);
            Point3D b = new Point3D(0.9, 0, 0);
            Point3D c = new Point3D(0, 0.9, 0);
            Point3D d = new Point3D(0, 0, 0.9);

            Point3D A0 = new Point3D((double)numericUpDown14.Value, (double)numericUpDown15.Value, (double)numericUpDown16.Value);
            Point3D B0 = new Point3D((double)numericUpDown17.Value, (double)numericUpDown18.Value, (double)numericUpDown19.Value);
            Line3D rotation_line = new Line3D(A0, B0);

            var p = new List<Primitive>
            {
            a,
            b,
            c,
            d,
            new Line3D(a, b),
            new Line3D(a, c),
            new Line3D(a, d),
            cur_primitive
            };

            foreach (Primitive x in p)
            {
                try
                {
                    x.Draw(g, t, width, height);
                }
                catch (ArgumentException ex)
                {
                    ErrorMBox.Text = "Координаты не допустимы для смещения в этом направлении";
                    numericUpDown1.Value = (decimal)-cur_primitive.Center.X;
                    numericUpDown2.Value = (decimal)-cur_primitive.Center.Y;
                    numericUpDown3.Value = (decimal)-cur_primitive.Center.Z;
                    Translate();
                    numericUpDown1.Value = 0;
                    numericUpDown2.Value = 0;
                    numericUpDown3.Value = 0;
                }
            }

            rotation_line.Draw(g, t, width, height, 2);
            label14.Text = cur_primitive.Center.X.ToString() + " " + cur_primitive.Center.Y.ToString() + " " + cur_primitive.Center.Z.ToString();
        }

        private void GetPrimitive()
        {
            switch (PrimitiveComboBox.SelectedItem.ToString())
            {
                case "Тетраэдр":
                    {
                        cur_primitive = new Tetrahedron(0.5);
                        break;
                    }
                case "Гексаэдр":
                    {
                        //cur_primitive = new Tetrahedron(0.5);
                        cur_primitive = new Hexahedron(0.5);
                        break;
                    }
                case "Октаэдр":
                    {
                        //cur_primitive = new Tetrahedron(0.5);
                        cur_primitive = new Octahedron(0.5);
                        break;
                    }
                case "Икосаэдр":
                    {
                        //cur_primitive = new Tetrahedron(0.5);
                        cur_primitive = new Isocahedron(0.5);
                        break;
                    }
                case "Додекаэдр":
                    {
                        //cur_primitive = new Tetrahedron(0.5);
                        cur_primitive = new Dodecahedron(0.5);
                        break;
                    }
                default:
                    {
                        cur_primitive = new Tetrahedron(0.5);
                        break;
                    }
            }
        }

        private void Clear()
        {
            perspective_bmp = new Bitmap(PerspectiveBox.Width, PerspectiveBox.Height);
            perspective_g = Graphics.FromImage(perspective_bmp);
            PerspectiveBox.Image = perspective_bmp;

            orthographic_bmp = new Bitmap(OrthographicBox.Width, OrthographicBox.Height);
            orthographic_g = Graphics.FromImage(orthographic_bmp);
            OrthographicBox.Image = orthographic_bmp;
        }
        public Form1()
        {
            InitializeComponent();

            perspective_bmp = new Bitmap(PerspectiveBox.Width, PerspectiveBox.Height);
            perspective_g = Graphics.FromImage(perspective_bmp);
            PerspectiveBox.Image = perspective_bmp;

            orthographic_bmp = new Bitmap(OrthographicBox.Width, OrthographicBox.Height);
            orthographic_g = Graphics.FromImage(orthographic_bmp);
            OrthographicBox.Image = orthographic_bmp;

            PerspectiveComboBox.SelectedItem = PerspectiveComboBox.Items[1];
            OrthographicComboBox.SelectedItem = OrthographicComboBox.Items[0];
            PrimitiveComboBox.SelectedItem = PrimitiveComboBox.Items[0];
            ReflectionComboBox.SelectedItem = ReflectionComboBox.Items[0];

            GetPrimitive();
            DrawAxis(perspective_g, get_perpective_transform(), PerspectiveBox.Width, PerspectiveBox.Height);
            DrawAxis(orthographic_g, get_orthographic_transform(), OrthographicBox.Width, OrthographicBox.Height);
        }

        private void ApplyPerspective_Click(object sender, EventArgs e)
        {
            perspective_bmp = new Bitmap(PerspectiveBox.Width, PerspectiveBox.Height);
            perspective_g = Graphics.FromImage(perspective_bmp);
            PerspectiveBox.Image = perspective_bmp;
            DrawAxis(perspective_g, get_perpective_transform(), PerspectiveBox.Width, PerspectiveBox.Height);
        }

        private void ApplyOrthographic_Click(object sender, EventArgs e)
        {
            orthographic_bmp = new Bitmap(OrthographicBox.Width, OrthographicBox.Height);
            orthographic_g = Graphics.FromImage(orthographic_bmp);
            OrthographicBox.Image = orthographic_bmp;
            DrawAxis(orthographic_g, get_orthographic_transform(), OrthographicBox.Width, OrthographicBox.Height);
        }

        private void ApplyPrimitive_Click(object sender, EventArgs e)
        {
            Clear();
            GetPrimitive();
            DrawAxis(perspective_g, get_perpective_transform(), PerspectiveBox.Width, PerspectiveBox.Height);
            DrawAxis(orthographic_g, get_orthographic_transform(), OrthographicBox.Width, OrthographicBox.Height);
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

        private void RotateCenter()
        {
            double X = (double)numericUpDown11.Value / 180 * Math.PI;
            double Y = (double)numericUpDown12.Value / 180 * Math.PI;
            double Z = (double)numericUpDown13.Value / 180 * Math.PI;
            cur_primitive.MultiplyWithTransformMatr(AffineTransformer.RotateX(X) * AffineTransformer.RotateY(Y) * AffineTransformer.RotateZ(Z));
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
            ErrorMBox.Text = "-";
            Clear();
            Translate();
            Rotate();
            Scale();
            DrawAxis(perspective_g, get_perpective_transform(), PerspectiveBox.Width, PerspectiveBox.Height);
            DrawAxis(orthographic_g, get_orthographic_transform(), OrthographicBox.Width, OrthographicBox.Height);
        }

        private void ApplyReflection_Click(object sender, EventArgs e)
        {
            Clear();
            Reflect();

            DrawAxis(perspective_g, get_perpective_transform(), PerspectiveBox.Width, PerspectiveBox.Height);
            DrawAxis(orthographic_g, get_orthographic_transform(), OrthographicBox.Width, OrthographicBox.Height);
        }

        private void ApplyScaleCenter_Click(object sender, EventArgs e)
        {
            Clear();
            ScaleCenter();

            DrawAxis(perspective_g, get_perpective_transform(), PerspectiveBox.Width, PerspectiveBox.Height);
            DrawAxis(orthographic_g, get_orthographic_transform(), OrthographicBox.Width, OrthographicBox.Height);
        }

        private void ApplyRotationCenter_Click(object sender, EventArgs e)
        {
            Clear();
            RotateCenter();

            DrawAxis(perspective_g, get_perpective_transform(), PerspectiveBox.Width, PerspectiveBox.Height);
            DrawAxis(orthographic_g, get_orthographic_transform(), OrthographicBox.Width, OrthographicBox.Height);
        }

        private void ApplyLineRotation_Click(object sender, EventArgs e)
        {
            if (numericUpDown14.Value == 0 && numericUpDown15.Value == 0 && numericUpDown16.Value == 0 && numericUpDown17.Value == 0 && numericUpDown18.Value == 0 && numericUpDown19.Value == 0)
            {
                return;
            }
            Clear();
            RotateLine();

            DrawAxis(perspective_g, get_perpective_transform(), PerspectiveBox.Width, PerspectiveBox.Height);
            DrawAxis(orthographic_g, get_orthographic_transform(), OrthographicBox.Width, OrthographicBox.Height);
        }

        private void OrthographicLabel_Click(object sender, EventArgs e)
        {

        }

        private void ClearButton_Click(object sender, EventArgs e)
        {
            Clear();
            cur_primitive = new Tetrahedron(0.5);

            DrawAxis(perspective_g, get_perpective_transform(), PerspectiveBox.Width, PerspectiveBox.Height);
            DrawAxis(orthographic_g, get_orthographic_transform(), OrthographicBox.Width, OrthographicBox.Height);
        }

        private void ToZero_Click(object sender, EventArgs e)
        {
            numericUpDown1.Value = 0;
            numericUpDown2.Value = 0;
            numericUpDown3.Value = 0;
            numericUpDown4.Value = 0;
            numericUpDown5.Value = 0;
            numericUpDown6.Value = 0;
            numericUpDown7.Value = 1;
            numericUpDown8.Value = 1;
            numericUpDown9.Value = 1;
        }
    }
}
