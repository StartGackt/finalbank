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
    public partial class DividendFamily : Form
    {
        private readonly dbcontext _dbContext = new dbcontext();
        public DividendFamily()
        {
            InitializeComponent();
            this.WindowState = FormWindowState.Maximized;
            txtfamily.TextChanged += txtFamily_TextChanged;
        }


        private void DividendFamily_Load(object sender, EventArgs e)
        {

        }
        private void txtFamily_TextChanged(object sender, EventArgs e)
        {
            // Perform search and update DataGridView when txtFamily text changes
            SearchByFamily(txtfamily.Text);
        }

        private void SearchByFamily(string family)
        {
            try
            {
                // Query DivPeoples table for matching Family
                var results = _dbContext.DivPeoples
                    .Where(d => d.Family.Contains(family))
                    .Select(d => new
                    {
                        d.Username,
                        d.Fullname,
                        d.MoneyOld,
                        d.Dividend
                    })
                    .ToList();

                // Bind results to DataGridView
                dataGridView1.DataSource = results;
            }
            catch (Exception ex)
            {
                MessageBox.Show($"เกิดข้อผิดพลาด: {ex.Message}", "ข้อผิดพลาด", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void button6_Click(object sender, EventArgs e)
        {
            MainReport mainReport = new MainReport();
            mainReport.Show();
            this.Hide();

        }
    }
}
