using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace lab5
{
    public partial class Form2 : Form
    {
        private List<PointF> points;
        private Random random;
        private float roughness;
        private float displacement;
        private bool generate;

        public Form2()
        {
            InitializeComponent();
            points = new List<PointF>();
            random = new Random();
            roughness = 1;
            displacement = 1;
            generate = false;
            this.Width = 1000;
            this.Height = 600;
        }

        private async void btnGenerate_Click(object sender, EventArgs e)
        {
            int height = pbCanvas.Height;
            int width = pbCanvas.Width;

            PointF p1 = new PointF(0, height / 2);
            PointF p2 = new PointF(width, height / 2);

            points.Clear();
            points.Add(p1);
            points.Add(p2);

            btnGenerate.Enabled = false;
            generate = true;

            await midpointDisplacementAsync(p1, p2, height / 2, roughness);

            pbCanvas.Invalidate();
            btnGenerate.Enabled = true;
            generate = false;
        }

        private async Task midpointDisplacementAsync(PointF p1, PointF p2, float variance, float roughness)
        {
            if (p2.X - p1.X < 1 || !generate)
                return;

            float midX = (p1.X + p2.X) / 2;
            float midY = (p1.Y + p2.Y) / 2;

            midY += random.Next(-1, 2) * variance * roughness;

            PointF midPoint = new PointF(midX, midY);
            points.Insert(points.IndexOf(p2), midPoint);

            pbCanvas.Invalidate();

            await Task.Delay(50);

            await midpointDisplacementAsync(p1, midPoint, variance / 2, roughness);
            await midpointDisplacementAsync(midPoint, p2, variance / 2, roughness);
        }

        private void pbCanvas_Paint(object sender, PaintEventArgs e)
        {
            Graphics g = e.Graphics;
            Pen pen = new Pen(Color.Black);

            for (int i = 1; i < points.Count; i++)
            {
                g.DrawLine(pen, points[i - 1], points[i]);
            }
        }

        private void trackBarRoughness_Scroll(object sender, EventArgs e)
        {
            roughness = trackBarRoughness.Value / 10.0f;
            lblRoughness.Text = $"Roughness: {roughness}";
        }

        private void trackBarDisplacement_Scroll(object sender, EventArgs e)
        {
            displacement = trackBarDisplacement.Value;
            lblDisplacement.Text = $"Displacement: {displacement}";
        }

    }
}
