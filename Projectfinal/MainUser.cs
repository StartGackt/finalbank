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
    public partial class MainUser : Form
    {
        public MainUser()
        {
            InitializeComponent();
            this.WindowState = FormWindowState.Maximized;
        }

        private void button4_Click(object sender, EventArgs e)
        {
            new Mainborrowmoney().Show();
            this.Hide();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            new Deposit().Show();
            this.Hide();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            new Payment().Show();
            this.Hide();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            new DepositSummary().Show();
            this.Hide();
        }

        private void button5_Click(object sender, EventArgs e)
        {
            new MemberRegignation().Show();
            this.Hide();
        }

        private void button6_Click(object sender, EventArgs e)
        {
            new Main().Show();
            this.Hide();
        }

        private void MainUser_Load(object sender, EventArgs e)
        {

        }
    }
}
