using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace task9
{
    public partial class FloatingHorizon : Form
    {
        Graph GraphicHorizon = null;
        List<functionType> Functions = new List<functionType>();
        Point capturePoint;
        bool isMouseCaptured;

        public FloatingHorizon()
        {
            InitializeComponent();
            Initialization();
        }

        private void Initialization()
        {
            List<string> Functions = new List<String>();
            Functions.Add("Y = Sin(x + z)");
            Functions.Add("Y = Sin(Cos(z) - Sin(x))");
            Functions.Add("Y = e^(Sin(Sqrt(x * x  + z * z )))");


            this.Functions.Add(task9.Functions.SinXplusZ);
            this.Functions.Add(task9.Functions.CosDelta);
            this.Functions.Add(task9.Functions.ExpSinR);

            cmbBoxFunctions.Items.AddRange(Functions.ToArray());
            cmbBoxFunctions.SelectedIndex = 0;



            txtBoxXBegin.Text = "-5";
            txtBoxXEnd.Text = "5";

            txtBoxZBegin.Text = "-5";
            txtBoxZEnd.Text = "5";
            txtBoxXStep.Text = "0,05";
            txtBoxZStep.Text = "0,2";



            btnMainColor.BackColor = Color.MediumVioletRed;
            btnBackColor.BackColor = Color.Black;



            GraphicHorizon = new Graph(picBox.Width, picBox.Height);
            StartHorizon();


            isMouseCaptured = false;
        }

        private void StartHorizon()
        {
            try
            {
                GraphicHorizon.SetBoundsOnX(Convert.ToDouble(txtBoxXBegin.Text), Convert.ToDouble(txtBoxXEnd.Text));
                GraphicHorizon.SetBoundsOnZ(Convert.ToDouble(txtBoxZBegin.Text), Convert.ToDouble(txtBoxZEnd.Text));
                GraphicHorizon.SetXZsteps(Convert.ToDouble(txtBoxXStep.Text), Convert.ToDouble(txtBoxZStep.Text));
                GraphicHorizon.SetAngleX(trackBarX.Value);
                GraphicHorizon.SetAngleY(trackBarY.Value);
                GraphicHorizon.SetAngleZ(trackBarZ.Value);
            }
            catch (System.Exception)
            {
                MessageBox.Show("Неверные входные данные");
            }
            GraphicHorizon.SetBackColor(btnBackColor.BackColor);
            GraphicHorizon.SetMainColor(btnMainColor.BackColor);
        }

        private void button1_Click(object sender, EventArgs e)
        {
            StartHorizon();
            ReDraw();
        }

        private void trackBarX_ValueChanged(object sender, EventArgs e)
        {
            GraphicHorizon.SetAngleX(trackBarX.Value);
            ReDraw();
        }

        private void trackBarY_ValueChanged(object sender, EventArgs e)
        {
            GraphicHorizon.SetAngleY(trackBarY.Value);
            ReDraw();
        }

        private void trackBarZ_ValueChanged(object sender, EventArgs e)
        {
            GraphicHorizon.SetAngleZ(trackBarZ.Value);
            ReDraw();
        }

        private void ReDraw()
        {
            GraphicHorizon.Draw(picBox.CreateGraphics(), Functions[cmbBoxFunctions.SelectedIndex]);
        }

        private void btnBackColor_Click(object sender, EventArgs e)
        {
            colorDlg.ShowDialog();
            GraphicHorizon.SetBackColor(colorDlg.Color);
            btnBackColor.BackColor = colorDlg.Color;
        }

        private void btnMainColor_Click(object sender, EventArgs e)
        {
            colorDlg.ShowDialog();
            GraphicHorizon.SetMainColor(colorDlg.Color);
            btnMainColor.BackColor = colorDlg.Color;
        }

        private void picBox_MouseDown(object sender, MouseEventArgs e)
        {
            capturePoint = e.Location;
            isMouseCaptured = true;
        }

        private void picBox_MouseMove(object sender, MouseEventArgs e)
        {

            if (isMouseCaptured)
            {
                double deltaAngle;
                if (Math.Abs(e.X - capturePoint.X) < Math.Abs(e.Y - capturePoint.Y))
                {
                    deltaAngle = e.Y > capturePoint.Y ?
                        360 * (e.Y - capturePoint.Y) / (picBox.Height - capturePoint.Y) :
                        360 * (1 - (e.Y - capturePoint.Y) / (capturePoint.Y - picBox.Height));
                    trackBarX.Value = Math.Abs((int)Math.Round(deltaAngle) % 361);
                }
                else
                {
                    deltaAngle = e.X > capturePoint.X ?
      360 * (e.X - capturePoint.X) / (picBox.Width - capturePoint.X) :
      360 * (1 - (e.X - capturePoint.X) / (capturePoint.X - picBox.Width));
                    trackBarY.Value = Math.Abs((int)Math.Round(deltaAngle) % 361);
                }


            }
        }

        private void picBox_MouseUp(object sender, MouseEventArgs e)
        {
            isMouseCaptured = false;
        }

        private void cmbBoxFunctions_SelectedIndexChanged(object sender, EventArgs e)
        {

        }
    }


    public static class Functions
    {

        public static double SinXplusZ(double x, double z)
        {
            return Math.Sin(x + z);
        }

        public static double CosDelta(double x, double z)
        {
            return Math.Cos(Math.Cos(z) - Math.Sin(x));
        }

        public static double ExpSinR(double x, double z)
        {
            return Math.Exp(Math.Sin(Math.Sqrt(x * x + z * z)));
        }
    }
}
