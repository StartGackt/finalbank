using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Projectfinal
{
    public partial class MainAdmin : Form
    {
        public MainAdmin()
        {
            InitializeComponent();
            this.WindowState = FormWindowState.Maximized;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            new MainReport().Show();
            this.Hide();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            new MainReprots().Show();
            this.Hide();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            new Main().Show();
            this.Hide();
        }

        private void MainAdmin_Load(object sender, EventArgs e)
        {

        }
    }
}
