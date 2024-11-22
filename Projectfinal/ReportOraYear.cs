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
    public partial class ReportOraYear : Form
    {
        dbcontext dbcontext = new dbcontext();
        public ReportOraYear()
        {
            InitializeComponent();
            this.WindowState = FormWindowState.Maximized;
        }

        private void ReportOraYear_Load(object sender, EventArgs e)
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
