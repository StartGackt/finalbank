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
    public partial class ReportDepostMonth : Form
    {
        private readonly dbcontext _dbContext = new dbcontext();

        public ReportDepostMonth()
        {
            InitializeComponent();
            this.WindowState = FormWindowState.Maximized;

            // Set ComboBox to DropDownList style to prevent free text entry
            comboBox1.DropDownStyle = ComboBoxStyle.DropDownList;

            // เพิ่ม: เติมข้อมูลเดือนลงใน ComboBox
            string[] months = new string[] {
                "มกราคม", "กุมภาพันธ์", "มีนาคม", "เมษายน", "พฤษภาคม", "มิถุนายน",
                "กรกฎาคม", "สิงหาคม", "กันยายน", "ตุลาคม", "พฤศจิกายน", "ธันวาคม"
            };
            comboBox1.Items.AddRange(months);

            // เพิ่ม: เชื่อม event handler
            comboBox1.SelectedIndexChanged += ComboBox1_SelectedIndexChanged;

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

        private void ReportDepostMonth_Load(object sender, EventArgs e)
        {
        }

        private void button3_Click(object sender, EventArgs e)
        {
            MainReprots mainReprots = new MainReprots();
            mainReprots.Show();
            this.Hide();
        }

        private void SearchByMonth()
        {
            if (comboBox1.SelectedIndex == -1)
            {
                MessageBox.Show("กรุณาเลือกเดือนที่ต้องการค้นหา", "แจ้งเตือน", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            try
            {
                // Get selected month (1-12)
                int selectedMonth = comboBox1.SelectedIndex + 1;

                // Query MoneyTrans data
                var transactions = _dbContext.MoneyTranss
                    .Where(mt => mt.TimeMoney.Month == selectedMonth)
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

                // Update DataGridView
                dataGridView1.DataSource = transactions;

                if (!transactions.Any())
                {
                    MessageBox.Show("ไม่พบข้อมูลในเดือนที่เลือก", "ผลการค้นหา", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"เกิดข้อผิดพลาด: {ex.Message}", "ข้อผิดพลาด", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        // เพิ่ม: event handler สำหรับ ComboBox
        private void ComboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            SearchByMonth();
        }

        private void comboBox1_SelectedIndexChanged_1(object sender, EventArgs e)
        {

        }
    }
}
