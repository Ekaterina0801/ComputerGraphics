using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using lab5.Color_Modifier;

using System.IO;
using System.Drawing.Drawing2D;

namespace lab5
{
    public partial class Form1 : Form
    {
        Graphics g;
        Pen pen;
        string axiom;
        double angle;
        string direction;
        int iterations;
        SortedDictionary<char, string> rules;
        Stack<Tuple<double, double, double, double, float, Color>> savedStates;

        public Form1()
        {
            InitializeComponent();
            pictureBox1.Image = new Bitmap(pictureBox1.Width, pictureBox1.Height);
            g = Graphics.FromImage(pictureBox1.Image);
            g.Clear(Color.White);
            g.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.HighQuality;
            AutoCompleteStringCollection source = new AutoCompleteStringCollection()
            {
                "Black",
                "Green",
                "Gray",
                "Yellow",
                "Purple",
                "Blue",
                "Red",
                "Pink"
            };
            col_box.AutoCompleteCustomSource = source;
            col_box.AutoCompleteMode = AutoCompleteMode.SuggestAppend;
            col_box.AutoCompleteSource = AutoCompleteSource.CustomSource;
            //this.Width = 1100;
            this.Height = 700;
            pen = new Pen(Color.Coral);

            rules = new SortedDictionary<char, string>();
            savedStates = new Stack<Tuple<double, double, double, double, float, Color>>();
        }

        private void openToolStripMenuItem_Click(object sender, EventArgs e)
        {
            OpenFileDialog openDialog = new OpenFileDialog();
            openDialog.Filter = "Text files|*.TXT";
            if (openDialog.ShowDialog() == DialogResult.OK)
            {
                try
                {
                    string fname = openDialog.FileName;
                    string[] flines = File.ReadAllLines(fname);
                    string[] parameters = flines[0].Split(' ');

                    axiom = parameters[0];
                    angle = Convert.ToDouble(parameters[1]);
                    direction = parameters[2];

                    rules.Clear();
                    inputBox.Text = "";
                    string[] rule;
                    inputBox.AppendText(flines[0] + "\r\n");
                    for (int i = 1; i < flines.Length; ++i)
                    {
                        inputBox.AppendText(flines[i] + "\r\n");
                        rule = flines[i].Split('>');
                        rules[Convert.ToChar(rule[0])] = rule[1];
                    }
                }
                catch
                {
                    DialogResult result = MessageBox.Show("Can't open file",
                        "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        string buildPath()
        {
            string prev = axiom;
            string next = axiom;
            int iter = 0;
            while (iter < iterations)
            {
                prev = next;
                next = "";
                for (int i = 0; i < prev.Length; ++i)
                {
                    if (rules.ContainsKey(prev[i]))
                        next += rules[prev[i]];
                    else
                        next += prev[i];
                }
                ++iter;
            }
            return next;
        }

        void drawLSystem(string path)
        {
            double min_x = Double.MaxValue;
            double max_x = Double.MinValue;
            double min_y = Double.MaxValue;
            double max_y = Double.MinValue;
            List<List<Tuple<double, double, float, Color>>> lSystPoints2 = new List<List<Tuple<double, double, float, Color>>>();
            lSystPoints2.Add(new List<Tuple<double, double, float, Color>>());
            double x = 0, y = 0, dx = 0, dy = 0;
            float w = 5; short to_decrease_width = 0;
            short fort = 0;
            Color_Modifier.Color_Modifier modifier = new Color_Modifier.Color_Modifier(0, 0, 0, 0);
            Color draw_color = Color.PeachPuff;
            double length = 0;
            if (len_box.Text == String.Empty)
            {
                length = Math.Min(pictureBox1.Width, pictureBox1.Height) / Math.Pow(10, iterations + 1);
            }
            else
            {
                length = Convert.ToDouble(len_box.Text);
            }

            if (width_box.Checked)
            {
                to_decrease_width = 1;
            }

            if (fort_box.Checked)
            {
                fort = 1;
            }

            if (col_change_box.Checked)
            {
                short k = 1;
                if (intensity_box.Text != String.Empty)
                {
                    k = Convert.ToInt16(intensity_box.Text);
                }
                modifier.Set(-9, Convert.ToInt16(2*k), Convert.ToInt16(-3 * k), Convert.ToInt16(4 * k));
            }

            if (col_box.Text != String.Empty)
            {
                switch (col_box.Text)
                {
                    case "Black":
                        draw_color = Color.Black;
                        break;
                    case "Blue":
                        draw_color = Color.Blue;
                        break;
                    case "Green":
                        draw_color = Color.Green;
                        break;
                    case "Yellow":
                        draw_color = Color.Yellow;
                        break;
                    case "Pink":
                        draw_color = Color.Pink;
                        break;
                    case "Purple":
                        draw_color = Color.Purple;
                        break;
                    case "Gray":
                        draw_color = Color.Gray;
                        break;
                    case "Red":
                        draw_color = Color.Red;
                        break;
                    default: break;
                }
                pen.Color = draw_color;
            }

            switch (direction)
            {
                case "LEFT":
                    x = pictureBox1.Width;
                    y = pictureBox1.Height / 2;
                    dx = -length;
                    break;

                case "RIGHT":
                    y = pictureBox1.Height / 2;
                    dx = length;
                    break;

                case "UP":
                    x = pictureBox1.Width / 2;
                    y = pictureBox1.Height;
                    dy = -length;
                    break;

                case "DOWN":
                    x = pictureBox1.Width / 2;
                    dy = length;
                    break;

                case "TREE":
                    x = pictureBox1.Width / 2;
                    y = pictureBox1.Height;
                    dy = -length;
                    break;

                default: break;
            }

            lSystPoints2.Last().Add(new Tuple<double, double, float, Color>(x, y, w, draw_color));

            setMinMax(x, y, ref min_x, ref min_y, ref max_x, ref max_y);

            double rx, ry;
            for (int i = 0; i < path.Length; ++i)
            {
                switch (path[i])
                {
                    case 'F':
                        x += dx;
                        y += dy;
                        w -= 0.5f * to_decrease_width;
                        draw_color = modifier.Change(draw_color);
                        lSystPoints2.Last().Add(new Tuple<double, double, float, Color>(x, y, w, draw_color));
                        setMinMax(x, y, ref min_x, ref min_y, ref max_x, ref max_y);
                        break;

                    case '+':
                        rx = dx;
                        ry = dy;
                        dx = rx * Math.Cos(angle * Math.PI / 180) + ry * Math.Sin(angle * Math.PI / 180);
                        dy = - rx * Math.Sin(angle * Math.PI / 180) + ry * Math.Cos(angle * Math.PI / 180);
                        break;

                    case '-':
                        rx = dx;
                        ry = dy;
                        dx = rx * Math.Cos(angle * Math.PI / 180) - ry * Math.Sin(angle * Math.PI / 180);
                        dy = rx * Math.Sin(angle * Math.PI / 180) + ry * Math.Cos(angle * Math.PI / 180);
                        break;

                    case '[':
                        savedStates.Push(new Tuple<double, double, double, double, float, Color>(x, y, dx, dy, w, draw_color));
                        break;

                    case ']':
                        Tuple<double, double, double, double, float, Color> coords = savedStates.Pop();
                        x = coords.Item1;
                        y = coords.Item2;
                        dx = coords.Item3;
                        dy = coords.Item4;
                        w = coords.Item5;
                        draw_color = coords.Item6;
                        lSystPoints2.Add(new List<Tuple<double, double, float, Color>>());
                        lSystPoints2.Last().Add(new Tuple<double, double, float, Color>(x, y, w, draw_color));
                        break;

                    case '@':
                        rx = dx;
                        ry = dy;
                        Random rnd = new Random();
                        int additional_angle = rnd.Next(-5, 5) * fort;
                        dx = rx * Math.Cos(additional_angle * Math.PI / 180) + ry * Math.Sin(additional_angle * Math.PI / 180);
                        dy = -rx * Math.Sin(additional_angle * Math.PI / 180) + ry * Math.Cos(additional_angle * Math.PI / 180);
                        break;

                    default: break;
                }
            }

            double scale = Math.Max(max_x - min_x, max_y - min_y);

            double k_w = 1 / scale * (pictureBox1.Width - 1);
            double k_h = 1 / scale * (pictureBox1.Height - 1);
            if (len_box.Text != String.Empty)
            {
                k_w = 1;
                k_h = 1;
            }
            
            foreach (var lst in lSystPoints2)
            {
                for (int i = 1; i < lst.Count; i++)
                {
                    pen.Width = lst[i - 1].Item3;
                    pen.Color = lst[i - 1].Item4;
                    g.DrawLine(pen,
                        (float)((lst[i - 1].Item1 - min_x) * k_w),
                        (float)((lst[i - 1].Item2 - min_y) * k_h),
                        (float)((lst[i].Item1 - min_x) * k_w),
                        (float)((lst[i].Item2 - min_y) * k_h));
                }
            }
        }

        private void createBtn_Click(object sender, EventArgs e)
        {
            g.Clear(Color.White);
            outputBox.Text = "";
            iterations = Convert.ToInt32(numericUpDown1.Value);
            string path = buildPath();
            drawLSystem(path);
            outputBox.Text = path;
            pictureBox1.Invalidate();
        }

        private void setMinMax(double x, double y, ref double min_x,
            ref double min_y, ref double max_x, ref double max_y)
        {
            if (x < min_x) min_x = x;
            if (x > max_x) max_x = x;
            if (y < min_y) min_y = y;
            if (y > max_y) max_y = y;
        }
    }
}

