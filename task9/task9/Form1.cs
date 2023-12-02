using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.DataFormats;

namespace task9
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Guro form = new Guro();
            form.Show();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Texturing form = new Texturing();
            form.Show();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            FloatingHorizon form = new FloatingHorizon();
            form.Show();
        }
    }
}
