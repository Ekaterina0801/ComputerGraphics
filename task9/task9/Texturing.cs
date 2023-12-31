﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using task9;
using task9.Properties;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace task9
{
    public partial class Texturing : Form
    {
        private Mesh cur_obj;

        private Camera camera;

        public string SelectedTexture = "Текстура1";

        public Texturing()
        {
            InitializeComponent();
            ProjectionBox.SelectedItem = ProjectionBox.Items[0];
            comboBox1.SelectedItem = comboBox1.Items[0];
            switch (comboBox1.SelectedItem.ToString())
            {
                case ("Текстура1"):
                    {
                        var t = Resources.backgroundImage;
                        break;
                    }
                case ("Текстура2"):
                    {
                        var t = Resources.Texture11;
                        break;
                    }
                case ("Текстура3"):
                    {
                        var t = Resources.Texture2;
                        break;
                    }
                case ("Текстура4"):
                    {
                        var t = Resources.Texture3;
                        break;
                    }
                default:
                    {
                        var t = Resources.backgroundImage;
                        break;
                    }
            }

            cur_obj = Figures.CubeTexture(0.5, SelectedTexture);
            Matrix projection = Transformer.PerspectiveProjection(-0.1, 0.1, -0.1, 0.1, 0.1, 20);
            camera = new Camera(new Vector(1, 1, 1), Math.PI / 4, -Math.Atan(1 / Math.Sqrt(3)), projection);
        }

        private static double DegreesToRadians(double degrees)
        {
            return degrees / 180 * Math.PI;
        }

        private void Scale()
        {
            double scalingX = (double)numericUpDown1.Value;
            double scalingY = (double)numericUpDown2.Value;
            double scalingZ = (double)numericUpDown3.Value;
            cur_obj.Apply(Transformer.Scale(scalingX, scalingY, scalingZ));
            pictureBox1.Invalidate();
        }

        private void Rotate()
        {
            double rotatingX = DegreesToRadians((double)numericUpDown4.Value);
            double rotatingY = DegreesToRadians((double)numericUpDown5.Value);
            double rotatingZ = DegreesToRadians((double)numericUpDown6.Value);
            cur_obj.Apply(Transformer.RotateX(rotatingX)
                * Transformer.RotateY(rotatingY)
                * Transformer.RotateZ(rotatingZ));
            pictureBox1.Invalidate();
        }

        private void Translate()
        {
            double translatingX = (double)numericUpDown7.Value;
            double translatingY = (double)numericUpDown8.Value;
            double translatingZ = (double)numericUpDown9.Value;
            cur_obj.Apply(Transformer.Translate(translatingX, translatingY, translatingZ));
            pictureBox1.Invalidate();
        }

        protected override bool ProcessCmdKey(ref Message msg, Keys keyData)
        {
            double delta = 0.3;
            switch (keyData)
            {
                case Keys.W: camera.Position *= Transformer.Translate(0.1 * camera.Forward); break;
                case Keys.A: camera.Position *= Transformer.Translate(0.1 * camera.Left); break;
                case Keys.S: camera.Position *= Transformer.Translate(0.1 * camera.Backward); break;
                case Keys.D: camera.Position *= Transformer.Translate(0.1 * camera.Right); break;
                case Keys.Left: camera.AngleY += delta; break;
                case Keys.Right: camera.AngleY -= delta; break;
                case Keys.Up: camera.AngleX += delta; break;
                case Keys.Down: camera.AngleX -= delta; break;
            }
            pictureBox1.Invalidate();
            return base.ProcessCmdKey(ref msg, keyData);
        }

        private void ApplyProjection_Click(object sender, EventArgs e)
        {
            switch (ProjectionBox.SelectedItem.ToString())
            {
                case ("Перспективная"):
                    {
                        Matrix projection = Transformer.PerspectiveProjection(-0.1, 0.1, -0.1, 0.1, 0.1, 20);
                        camera = new Camera(new Vector(1, 1, 1), Math.PI / 4, -Math.Atan(1 / Math.Sqrt(3)), projection);
                        break;
                    }
                case ("Ортографическая XY"):
                    {
                        camera = new Camera(new Vector(0, 0, 0), 0, 0, Transformer.OrthogonalProjection());
                        break;
                    }
                case ("Ортографическая XZ"):
                    {
                        camera = new Camera(new Vector(0, 0, 0), 0, 0, Transformer.RotateX(Math.PI / 2) * Transformer.OrthogonalProjection());
                        break;
                    }
                case ("Ортографическая YZ"):
                    {
                        camera = new Camera(new Vector(0, 0, 0), 0, 0, Transformer.RotateY(-Math.PI / 2) * Transformer.OrthogonalProjection());
                        break;
                    }
                default:
                    {
                        Matrix projection = Transformer.PerspectiveProjection(-0.1, 0.1, -0.1, 0.1, 0.1, 20);
                        camera = new Camera(new Vector(1, 1, 1), Math.PI / 4, -Math.Atan(1 / Math.Sqrt(3)), projection);
                        break;
                    }
            }
            pictureBox1.Invalidate();
        }

        private void ApplyAffin_Click(object sender, EventArgs e)
        {
            Scale();
            Rotate();
            Translate();
            pictureBox1.Invalidate();
        }

        private void PictureBox1_Paint(object sender, PaintEventArgs e)
        {
            //e.Graphics.FillRectangle(Brushes.Black, 0, 0, pictureBox1.Width, pictureBox1.Height);
            var graphics3D = new View3D(e.Graphics, camera.ViewProjection, pictureBox1.Width, pictureBox1.Height, cur_obj.Center, camera.Position);
            var zero = new Vector(0, 0, 0);
            var x = new Vector(0.8, 0, 0);
            var y = new Vector(0, 0.8, 0);
            var z = new Vector(0, 0, 0.8);
            graphics3D.DrawLine(
                new Vertex(zero, Color.Red),
                new Vertex(x, Color.Red));
            graphics3D.DrawPoint(new Vertex(x, Color.Red));
            graphics3D.DrawLine(
                new Vertex(zero, Color.Green),
                new Vertex(y, Color.Green));
            graphics3D.DrawPoint(new Vertex(y, Color.Green));
            graphics3D.DrawLine(
                new Vertex(zero, Color.Blue),
                new Vertex(z, Color.Blue));
            graphics3D.DrawPoint(new Vertex(z, Color.Blue));
            cur_obj.Draw(graphics3D);
            e.Graphics.DrawImage(graphics3D.colorBuffer, 0, 0);
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            switch (comboBox1.SelectedItem.ToString())
            {
                case ("Текстура1"):
                    {
                        SelectedTexture = "Текстура1";
                        break;
                    }
                case ("Текстура2"):
                    {
                        SelectedTexture = "Текстура2";
                        break;
                    }
                case ("Текстура3"):
                    {
                        SelectedTexture = "Текстура3";
                        break;
                    }
                case ("Текстура4"):
                    {
                        SelectedTexture = "Текстура4";
                        break;
                    }
                default:
                    {
                        SelectedTexture = "Текстура1";
                        break;
                    }
            }
            cur_obj = Figures.CubeTexture(0.5, SelectedTexture);
            pictureBox1.Invalidate();
        }

        private void Texturing_Load(object sender, EventArgs e)
        {

        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {
        }

        private void label7_Click(object sender, EventArgs e)
        {
        }

        private void label6_Click(object sender, EventArgs e)
        {
        }

        private void label5_Click(object sender, EventArgs e)
        {
        }

        private void label4_Click(object sender, EventArgs e)
        {
        }

        private void label3_Click(object sender, EventArgs e)
        {
        }

        private void label2_Click(object sender, EventArgs e)
        {
        }

        private void label1_Click(object sender, EventArgs e)
        {
        }

        private void ProjectionBox_SelectedIndexChanged(object sender, EventArgs e)
        {
        }

        private void numericUpDown9_ValueChanged(object sender, EventArgs e)
        {
        }

        private void numericUpDown8_ValueChanged(object sender, EventArgs e)
        {
        }

        private void numericUpDown7_ValueChanged(object sender, EventArgs e)
        {
        }

        private void numericUpDown6_ValueChanged(object sender, EventArgs e)
        {
        }

        private void numericUpDown5_ValueChanged(object sender, EventArgs e)
        {
        }

        private void numericUpDown4_ValueChanged(object sender, EventArgs e)
        {
        }

        private void numericUpDown3_ValueChanged(object sender, EventArgs e)
        {
        }

        private void numericUpDown2_ValueChanged(object sender, EventArgs e)
        {
        }

        private void numericUpDown1_ValueChanged(object sender, EventArgs e)
        {
        }
    }
}