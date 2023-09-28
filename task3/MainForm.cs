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

        private void button1_Click(object sender, EventArgs e)
        {
            ReccurA newForm = new ReccurA();
            newForm.Show();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            _1b newForm = new _1b();
            newForm.Show();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            ReccurC newForm = new ReccurC();
            newForm.Show();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            Bresenham newForm = new Bresenham();
            newForm.Show();
        }

        private void button5_Click(object sender, EventArgs e)
        {
            RasterTriangle newForm = new RasterTriangle();
            newForm.Show();
        }
    }
}