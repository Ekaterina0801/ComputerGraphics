using System;
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

namespace task9
{
    public partial class Guro : Form
    {
        private Mesh CurrentDrawable
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

        private Camera camera;

        public Guro()
        {
            InitializeComponent();
            ProjectionBox.SelectedItem = ProjectionBox.Items[0];
            FigureBox.SelectedItem = FigureBox.Items[1];
            CurrentDrawable = Figures.Cube(0.25);
            Matrix projection = Transformer.PerspectiveProjection(-0.1, 0.1, -0.1, 0.1, 0.1, 20);
            camera = new Camera(new Vector(1, 1, 1), Math.PI / 4, -Math.Atan(1 / Math.Sqrt(3)), projection);
            sceneView1.Camera = camera;
        }

        private static double DegreesToRadians(double degrees)
        {
            return degrees / 180 * Math.PI;
        }

        private void Scale()
        {
            CurrentDrawable.Apply(Transformer.Scale((double)numericUpDown1.Value, 
                (double)numericUpDown2.Value, (double)numericUpDown2.Value));
            sceneView1.Refresh();
        }

        private void Rotate()
        {
            CurrentDrawable.Apply(Transformer.RotateX(DegreesToRadians((double)numericUpDown4.Value))
                * Transformer.RotateY(DegreesToRadians((double)numericUpDown5.Value))
                * Transformer.RotateZ(DegreesToRadians((double)numericUpDown6.Value)));
            sceneView1.Refresh();
        }

        private void Translate()
        {
            CurrentDrawable.Apply(Transformer.Translate((double)numericUpDown7.Value, 
                (double)numericUpDown8.Value, (double)numericUpDown9.Value));
            sceneView1.Refresh();
        }

        private void SaveFile()
        {
            if (!(CurrentDrawable is Mesh)) return;
            var mesh = (Mesh)CurrentDrawable;
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
                    DialogResult rezult = MessageBox.Show("Невозможно сохранить файл",
                    "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
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
                CurrentDrawable = new Mesh(openDialog.FileName);
            }
            catch
            {
                MessageBox.Show("Ошибка при чтении файла",
                    "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
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
            sceneView1.Refresh();
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
                        sceneView1.Camera = camera;
                        break;
                    }
                case ("Ортографическая XY"):
                    {
                        camera = new Camera(new Vector(0, 0, 0), 0, 0, Transformer.OrthogonalProjection());
                        sceneView1.Camera = camera;
                        break;
                    }
                case ("Ортографическая XZ"):
                    {
                        camera = new Camera(new Vector(0, 0, 0), 0, 0, Transformer.RotateX(Math.PI / 2) * Transformer.OrthogonalProjection());
                        sceneView1.Camera = camera;
                        break;
                    }
                case ("Ортографическая YZ"):
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

        private void ApplyAffin_Click(object sender, EventArgs e)
        {
            Scale();
            Rotate();
            Translate();
            sceneView1.Refresh();
        }

        private void SaveButton_Click(object sender, EventArgs e)
        {
            SaveFile();
        }

        private void LoadButton_Click(object sender, EventArgs e)
        {
            LoadFile();
        }

        private void Guro_Load(object sender, EventArgs e)
        {

        }
        private void ApplyFigure_Click(object sender, EventArgs e)
        {
            switch (FigureBox.SelectedItem.ToString())
            {
                case ("Сфера"):
                    {
                        CurrentDrawable = Figures.Sphere(0.5, 20, 20);
                        break;
                    }
                case ("Октаэдр"):
                    {
                        CurrentDrawable = Figures.Octahedron(0.25);
                        break;
                    }
                case ("Тетраэдр"):
                    {
                        CurrentDrawable = Figures.Tetrahedron(0.25);
                        break;
                    }
                case ("Куб"):
                    {
                        CurrentDrawable = Figures.Cube(0.25);
                        break;
                    }
                default:
                    {
                        CurrentDrawable = Figures.Cube(0.25);
                        break;
                    }
            }
            sceneView1.Refresh();
        }
    }
}