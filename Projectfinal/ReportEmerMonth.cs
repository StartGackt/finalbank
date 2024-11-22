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
    public partial class ReportEmerMonth : Form
    {
        dbcontext dbcontext = new dbcontext();
        public ReportEmerMonth()
        {
            InitializeComponent();
            this.WindowState = FormWindowState.Maximized;
        }

        private void ReportEmerMonth_Load(object sender, EventArgs e)
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
