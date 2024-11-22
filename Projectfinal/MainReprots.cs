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
    public partial class MainReprots : Form
    {
        public MainReprots()
        {
            InitializeComponent();
            this.WindowState = FormWindowState.Maximized;
        }

        private void button5_Click(object sender, EventArgs e)
        {
            new Main().Show();
        }

        private void MainReprots_Load(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            new ReportDepostMonth().Show();
            this.Hide();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            new ReportDepostYear().Show();
            this.Hide();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            new ReportEmerMonth().Show();
            this.Hide();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            new ReportEmerYear().Show();
            this.Hide();
        }

        private void button6_Click(object sender, EventArgs e)
        {
            new ReportOraMonth().Show();
            this.Hide();
        }

        private void button7_Click(object sender, EventArgs e)
        {
            new ReportOraYear().Show();
            this.Hide();
        }

        private void button8_Click(object sender, EventArgs e)
        {
            new ReportPaymentMonth().Show();
            this.Hide();
        }

        private void button9_Click(object sender, EventArgs e)
        {
            new ReportPaymentYear().Show();
            this.Hide();
        }
    }
}
