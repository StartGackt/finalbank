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
    public partial class MemberRegignation : Form
    {
        public MemberRegignation()
        {
            InitializeComponent();
            this.WindowState = FormWindowState.Maximized;
        }

        private void button3_Click(object sender, EventArgs e)
        {
            new Main().Show();
            this.Hide();
        }

        private void MemberRegignation_Load(object sender, EventArgs e)
        {

        }
    }
}
