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
    public partial class AllDividend : Form
    {
        private readonly dbcontext _dbContext = new dbcontext();
        public AllDividend()
        {
            InitializeComponent();
            this.WindowState = FormWindowState.Maximized;
        }

        private void button6_Click(object sender, EventArgs e)
        {
            MainReport mainReport = new MainReport();
            mainReport.Show();
            this.Hide();

        }


        private void AllDividend_Load(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            LoadAllDividends();
        }
        private void LoadAllDividends()
        {
            try
            {
                // Retrieve all records from DivPeoples
                var allDividends = _dbContext.DivPeoples
                    .Select(d => new
                    {
                        d.Username,
                        d.Family,
                        d.Fullname,
                        d.MoneyOld,
                        d.Dividend
                    })
                    .ToList();

                // Bind data to DataGridView
                dataGridView1.DataSource = allDividends;
            }
            catch (Exception ex)
            {
                MessageBox.Show($"เกิดข้อผิดพลาด: {ex.Message}", "ข้อผิดพลาด", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}
