using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace lab3
{
    public partial class MainForm : Form
    {
        public MainForm()
        {
            InitializeComponent();
        }
        private void button3_Click(object sender, EventArgs e)
        {
            ReccurC form1 = new ReccurC();
            form1.ShowDialog();
        }
        private void button4_Click(object sender, EventArgs e)
        {
            Bresenham form1 = new Bresenham();
            form1.ShowDialog();
        }
        private void button5_Click(object sender, EventArgs e)
        {
            RasterTriangle form1 = new RasterTriangle();
            form1.ShowDialog();
        }
        private void button1_Click(object sender, EventArgs e)
        {
            ReccurA form2 = new ReccurA();
            form2.ShowDialog();

        }

        private void button2_Click(object sender, EventArgs e)
        {
            _1b form3 = new _1b();
            form3.ShowDialog();
        }

    }
}
