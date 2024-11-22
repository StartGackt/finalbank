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
    public partial class ReportDepostYear : Form
    {
        private readonly dbcontext _dbContext = new dbcontext();

        public ReportDepostYear()
        {
            InitializeComponent();
            this.WindowState = FormWindowState.Maximized;

            // Set ComboBox style to prevent free text entry
            comboBox1.DropDownStyle = ComboBoxStyle.DropDownList;

            // Remove comboBox2 from the form
            if (comboBox2 != null)
            {
                this.Controls.Remove(comboBox2);
                comboBox2.Dispose();
            }

            // Add years to ComboBox1
            for (int year = DateTime.Now.Year - 5; year <= DateTime.Now.Year; year++)
            {
                comboBox1.Items.Add(year);
            }

            // Add event handler
            comboBox1.SelectedIndexChanged += ComboBox_SelectedIndexChanged;

            // Configure DataGridView
            dataGridView1.AutoGenerateColumns = false;
            SetupDataGridViewColumns();
        }

        private void SetupDataGridViewColumns()
        {
            dataGridView1.Columns.Clear();
            dataGridView1.Columns.AddRange(
                new DataGridViewTextBoxColumn { Name = "Username", HeaderText = "Username", DataPropertyName = "Username" },
                new DataGridViewTextBoxColumn { Name = "Family", HeaderText = "Family", DataPropertyName = "Family" },
                new DataGridViewTextBoxColumn { Name = "Fullname", HeaderText = "Fullname", DataPropertyName = "Fullname" },
                new DataGridViewTextBoxColumn { Name = "MoneyTotal", HeaderText = "Money Total", DataPropertyName = "MoneyTotal", DefaultCellStyle = { Format = "N2" } },
                new DataGridViewTextBoxColumn { Name = "TimeMoney", HeaderText = "Time", DataPropertyName = "TimeMoney", DefaultCellStyle = { Format = "dd/MM/yyyy" } }
            );
        }

        private void SearchByYear()
        {
            if (comboBox1.SelectedIndex == -1)
            {
                MessageBox.Show("กรุณาเลือกปีที่ต้องการค้นหา", "แจ้งเตือน", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            try
            {
                int selectedYear = Convert.ToInt32(comboBox1.SelectedItem);

                var transactions = _dbContext.MoneyTranss
                    .Where(mt => mt.TimeMoney.Year == selectedYear)
                    .Select(mt => new
                    {
                        mt.Username,
                        mt.Family,
                        mt.Fullname,
                        mt.MoneyTotal,
                        mt.TimeMoney
                    })
                    .OrderBy(mt => mt.TimeMoney)
                    .ToList();

                dataGridView1.DataSource = transactions;

                if (!transactions.Any())
                {
                    MessageBox.Show("ไม่พบข้อมูลในปีที่เลือก", "ผลการค้นหา", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"เกิดข้อผิดพลาด: {ex.Message}", "ข้อผิดพลาด", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void ComboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            SearchByYear();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            MainReprots mainReprots = new MainReprots();
            mainReprots.Show();
            this.Hide();
        }

        private void ReportDepostYear_Load(object sender, EventArgs e)
        {

        }
    }
}

