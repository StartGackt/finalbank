using System;
using System.Linq;
using System.Windows.Forms;
using Microsoft.EntityFrameworkCore;
using Projectfinal.Model;

namespace Projectfinal
{
    public partial class ReportOraMonth : Form
    {
        private readonly dbcontext dbcontext = new dbcontext();

        public ReportOraMonth()
        {
            InitializeComponent();
            this.WindowState = FormWindowState.Maximized;

            comboBox1.DropDownStyle = ComboBoxStyle.DropDownList;
            comboBox1.SelectedIndexChanged += ComboBox1_SelectedIndexChanged;

            dataGridView1.AutoGenerateColumns = false;
            SetupDataGridViewColumns();
        }

        private void SetupDataGridViewColumns()
        {
            dataGridView1.Columns.Clear();
            dataGridView1.Columns.AddRange(
                new DataGridViewTextBoxColumn { Name = "Id", HeaderText = "ID", DataPropertyName = "Id" },
                new DataGridViewTextBoxColumn { Name = "Username", HeaderText = "Username", DataPropertyName = "Username" },
                new DataGridViewTextBoxColumn { Name = "Family", HeaderText = "Family", DataPropertyName = "Family" },
                new DataGridViewTextBoxColumn { Name = "Phone", HeaderText = "Phone", DataPropertyName = "Phone" },
                new DataGridViewTextBoxColumn { Name = "Fullname", HeaderText = "Fullname", DataPropertyName = "Fullname" },
                new DataGridViewTextBoxColumn { Name = "LoneMoney", HeaderText = "Loan Amount", DataPropertyName = "LoneMoney", DefaultCellStyle = { Format = "N2" } },
                new DataGridViewTextBoxColumn { Name = "NumberLone", HeaderText = "Loan Number", DataPropertyName = "NumberLone" },
                new DataGridViewTextBoxColumn { Name = "Interrate", HeaderText = "Interest Rate", DataPropertyName = "Interrate" },
                new DataGridViewTextBoxColumn { Name = "TimeLone", HeaderText = "Time", DataPropertyName = "TimeLone" },
                new DataGridViewTextBoxColumn { Name = "TotalMoneyLone", HeaderText = "Total Amount", DataPropertyName = "TotalMoneyLone", DefaultCellStyle = { Format = "N2" } }
            );
        }

        private void SearchByMonth()
        {
            try
            {
                if (comboBox1.SelectedIndex == -1)
                {
                    MessageBox.Show("กรุณาเลือกเดือน", "แจ้งเตือน", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                int selectedMonth = comboBox1.SelectedIndex + 1;
                int currentYear = DateTime.Now.Year;

                var query = dbcontext.OrdLones
                    .Where(loan => loan.TimeLone.Year == currentYear &&
                                   loan.TimeLone.Month == selectedMonth)
                    .ToList();

                if (!query.Any())
                {
                    MessageBox.Show($"ไม่พบข้อมูลในเดือน {comboBox1.Text}", "ผลการค้นหา", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    dataGridView1.DataSource = null;
                    return;
                }

                dataGridView1.DataSource = query;
                MessageBox.Show($"พบข้อมูลทั้งหมด {query.Count} รายการ");
            }
            catch (Exception ex)
            {
                MessageBox.Show($"เกิดข้อผิดพลาด: {ex.Message}", "ข้อผิดพลาด", MessageBoxButtons.OK, MessageBoxIcon.Error);
                dataGridView1.DataSource = null;
            }
        }

        private void ReportOraMonth_Load(object sender, EventArgs e)
        {
            comboBox1.Items.AddRange(new string[] {
                "มกราคม", "กุมภาพันธ์", "มีนาคม", "เมษายน", "พฤษภาคม", "มิถุนายน",
                "กรกฎาคม", "สิงหาคม", "กันยายน", "ตุลาคม", "พฤศจิกายน", "ธันวาคม"
            });
        }

        private void buttonSearch_Click(object sender, EventArgs e)
        {
            SearchByMonth();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            MainReprots mainReprots = new MainReprots();
            mainReprots.Show();
            this.Hide();
        }

        private void ComboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (comboBox1.SelectedIndex != -1)
            {
                SearchByMonth();
            }
        }
    }
}
