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
    public partial class MainReport : Form
    {
        public MainReport()
        {
            InitializeComponent();
            this.WindowState = FormWindowState.Maximized;
        }

        private void button3_Click(object sender, EventArgs e)
        {
            new Main().Show();
            this.Hide();
        }

        private void MainReport_Load(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            new DividendPeople().Show();
            this.Hide();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            new DividendFamily().Show();
            this.Hide();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            new AllDividend().Show();
            this.Hide();
        }
    }
}
