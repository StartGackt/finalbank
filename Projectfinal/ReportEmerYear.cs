using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Projectfinal.Model;

namespace Projectfinal
{
    public partial class ReportEmerYear : Form
    {
        dbcontext dbcontext = new dbcontext();
        public ReportEmerYear()
        {
            InitializeComponent();
            this.WindowState = FormWindowState.Maximized;
        }

        private void ReportEmerYear_Load(object sender, EventArgs e)
        {

        }

        private void button3_Click(object sender, EventArgs e)
        {
            MainReprots mainReprots = new MainReprots();
            mainReprots.Show();
            this.Hide();
        }
    }
}
