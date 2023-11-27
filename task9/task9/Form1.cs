using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Reflection.Emit;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using task9;

namespace task9
{
    public partial class Form1 : Form
    {
        private Mesh current_primitive
        {
            get
            {
                return sceneView1.Drawable;
            }
            set
            {
                sceneView1.Drawable = value;
                sceneView1.Refresh();
            }
        }

        //Camera variable
        private Camera camera;

        public Form1()
        {
            InitializeComponent();
            ProjectionBox.SelectedItem = ProjectionBox.Items[0];
            string str = "";
            if (listBox1.SelectedItem != null)
            {
                str = listBox1.SelectedItem.ToString();
                // ���������� �������� � ��������� ���������...
            }
            current_primitive = Graph.get_Graph(-0.8, 0.8, 0.1, -0.8, 0.8, 0.1, str);
            Matrix projection = Transformer.PerspectiveProjection(-0.1, 0.1, -0.1, 0.1, 0.1, 20);
            camera = new Camera(new Vector(1, 1, 1), Math.PI / 4, -Math.Atan(1 / Math.Sqrt(3)), projection);
            sceneView1.Camera = camera;
        }
        private void listBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            // �������� ��������� �������
            string selectedValue = listBox1.SelectedItem.ToString();

            // ����� �� ������ ������������ ��������� ������� �� ������ ����������
            // ��������, �������� ��������� ������� � ������� ��� ��������� ������ �������� ���������� �� �����

            Console.WriteLine("��������� �������: " + selectedValue);
            // ���
            label1.Text = selectedValue;
        }

        //Change current view to Plot
        private void ApplyProjection_Click(object sender, EventArgs e)
        {
            switch (ProjectionBox.SelectedItem.ToString())
            {
                case ("�������������"):
                    {
                        Matrix projection = Transformer.PerspectiveProjection(-0.1, 0.1, -0.1, 0.1, 0.1, 20);
                        camera = new Camera(new Vector(1, 1, 1), Math.PI / 4, -Math.Atan(1 / Math.Sqrt(3)), projection);
                        sceneView1.Camera = camera;
                        break;
                    }
                case ("��������������� XY"):
                    {
                        camera = new Camera(new Vector(0, 0, 0), 0, 0, Transformer.OrthogonalProjection());
                        sceneView1.Camera = camera;
                        break;
                    }
                case ("��������������� XZ"):
                    {
                        camera = new Camera(new Vector(0, 0, 0), 0, 0, Transformer.RotateX(Math.PI / 2) * Transformer.OrthogonalProjection());
                        sceneView1.Camera = camera;
                        break;
                    }
                case ("��������������� YZ"):
                    {
                        camera = new Camera(new Vector(0, 0, 0), 0, 0, Transformer.RotateY(-Math.PI / 2) * Transformer.OrthogonalProjection());
                        sceneView1.Camera = camera;
                        break;
                    }
                default:
                    {
                        Matrix projection = Transformer.PerspectiveProjection(-0.1, 0.1, -0.1, 0.1, 0.1, 20);
                        camera = new Camera(new Vector(1, 1, 1), Math.PI / 4, -Math.Atan(1 / Math.Sqrt(3)), projection);
                        sceneView1.Camera = camera;
                        break;
                    }
            }
            sceneView1.Refresh();
        }

        //Moves values in degrees to radians
        private static double DegreesToRadians(double degrees)
        {
            return degrees / 180 * Math.PI;
        }

        //Scale current primitive
        private void Scale()
        {
            double scalingX = (double)numericUpDown1.Value;
            double scalingY = (double)numericUpDown2.Value;
            double scalingZ = (double)numericUpDown3.Value;
            current_primitive.Apply(Transformer.Scale(scalingX, scalingY, scalingZ));
            sceneView1.Refresh();
        }

        //Rotate current primitive
        private void Rotate()
        {
            double rotatingX = DegreesToRadians((double)numericUpDown4.Value);
            double rotatingY = DegreesToRadians((double)numericUpDown5.Value);
            double rotatingZ = DegreesToRadians((double)numericUpDown6.Value);
            current_primitive.Apply(Transformer.RotateX(rotatingX)
                * Transformer.RotateY(rotatingY)
                * Transformer.RotateZ(rotatingZ));
            sceneView1.Refresh();
        }

        //Translate current primitive
        private void Translate()
        {
            double translatingX = (double)numericUpDown7.Value;
            double translatingY = (double)numericUpDown8.Value;
            double translatingZ = (double)numericUpDown9.Value;
            current_primitive.Apply(Transformer.Translate(translatingX, translatingY, translatingZ));
            sceneView1.Refresh();
        }

        //Overriding keyboard keys for camera moves
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
            sceneView1.Refresh();
            return base.ProcessCmdKey(ref msg, keyData);
        }

        private void ApplyAffin_Click(object sender, EventArgs e)
        {
            Scale();
            Rotate();
            Translate();
            sceneView1.Refresh();
        }

        private void SaveFile()
        {
            if (!(current_primitive is Mesh)) return;
            var mesh = current_primitive;
            SaveFileDialog saveDialog = new SaveFileDialog();
            saveDialog.Filter = "Object Files(*.obj)|*.obj|Text files (*.txt)|*.txt|All files (*.*)|*.*";
            if (saveDialog.ShowDialog() == DialogResult.OK)
            {
                try
                {
                    mesh.Save(saveDialog.FileName);
                }
                catch
                {
                    DialogResult rezult = MessageBox.Show("���������� ��������� ����",
                    "������", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void LoadFile()
        {
            OpenFileDialog openDialog = new OpenFileDialog();
            openDialog.Filter = "Object Files(*.obj)|*.obj|Text files (*.txt)|*.txt|All files (*.*)|*.*";
            if (openDialog.ShowDialog() != DialogResult.OK)
                return;
            try
            {
                current_primitive = new Mesh(openDialog.FileName);
            }
            catch
            {
                MessageBox.Show("������ ��� ������ �����",
                    "������", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void SaveButton_Click(object sender, EventArgs e)
        {
            SaveFile();
        }

        private void LoadButton_Click(object sender, EventArgs e)
        {
            LoadFile();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            current_primitive = Graph.get_Graph(-0.8, 0.8, 0.1, -0.8, 0.8, 0.1, label1.Text);
            Matrix projection = Transformer.PerspectiveProjection(-0.1, 0.1, -0.1, 0.1, 0.1, 20);
            camera = new Camera(new Vector(1, 1, 1), Math.PI / 4, -Math.Atan(1 / Math.Sqrt(3)), projection);
            sceneView1.Camera = camera;
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }
    }
}
