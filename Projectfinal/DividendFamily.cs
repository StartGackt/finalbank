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
            SetupDataGridView(); // เพิ่มการตั้งค่า DataGridView
        }

        private void DividendFamily_Load(object sender, EventArgs e)
        {
            SearchByFamily(""); // โหลดข้อมูลเริ่มต้น
        }

        private void SetupDataGridView()
        {
            // ตั้งค่าพื้นฐานของ DataGridView
            dataGridView1.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dataGridView1.RowTemplate.Height = 30; // ความสูงของแถว
            dataGridView1.AllowUserToAddRows = false; // ปิดการเพิ่มแถว
            dataGridView1.ReadOnly = true; // อ่านอย่างเดียว
            dataGridView1.SelectionMode = DataGridViewSelectionMode.FullRowSelect; // เลือกทั้งแถว

            // ตั้งค่าสไตล์ตาราง
            dataGridView1.EnableHeadersVisualStyles = false;
            dataGridView1.ColumnHeadersDefaultCellStyle.BackColor = Color.FromArgb(50, 50, 50); // หัวตารางสีเทาเข้ม
            dataGridView1.ColumnHeadersDefaultCellStyle.ForeColor = Color.White; // ตัวอักษรหัวตารางสีขาว
            dataGridView1.ColumnHeadersDefaultCellStyle.Font = new Font("Segoe UI", 10, FontStyle.Bold); // ฟอนต์หัวตาราง
            dataGridView1.DefaultCellStyle.Font = new Font("Segoe UI", 9); // ฟอนต์เนื้อหา

            // สีแถว
            dataGridView1.AlternatingRowsDefaultCellStyle.BackColor = Color.FromArgb(245, 245, 245); // แถวสลับสีเทาอ่อนมาก
            dataGridView1.DefaultCellStyle.BackColor = Color.White; // แถวปกติสีขาว
            dataGridView1.DefaultCellStyle.ForeColor = Color.Black; // ตัวอักษรสีดำ
            dataGridView1.DefaultCellStyle.SelectionBackColor = Color.FromArgb(100, 149, 237); // สีเมื่อเลือกแถว
            dataGridView1.DefaultCellStyle.SelectionForeColor = Color.White; // ตัวอักษรเมื่อเลือก

            // เส้นขอบ
            dataGridView1.GridColor = Color.FromArgb(200, 200, 200); // สีเส้นตาราง
            dataGridView1.BorderStyle = BorderStyle.FixedSingle; // ขอบตาราง
        }

        private void txtFamily_TextChanged(object sender, EventArgs e)
        {
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
                        d.Family,
                        d.Dividend
                    })
                    .ToList();

                // Bind results to DataGridView
                dataGridView1.DataSource = results;

                // ปรับแต่งคอลัมน์
                if (dataGridView1.Columns.Count > 0)
                {
                    dataGridView1.Columns["Username"].HeaderText = "ชื่อผู้ใช้";
                    dataGridView1.Columns["Fullname"].HeaderText = "ชื่อเต็ม";
                    dataGridView1.Columns["Family"].HeaderText = "ครอบครัว";
                    dataGridView1.Columns["Dividend"].HeaderText = "เงินปันผล";

                    // ความกว้างคอลัมน์
                    dataGridView1.Columns["Username"].Width = 150;
                    dataGridView1.Columns["Fullname"].Width = 200;
                    dataGridView1.Columns["Family"].Width = 150;
                    dataGridView1.Columns["Dividend"].Width = 120;

                    // จัดรูปแบบเงินปันผล
                    dataGridView1.Columns["Dividend"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
                    dataGridView1.Columns["Dividend"].DefaultCellStyle.Format = "N2"; // ทศนิยม 2 ตำแหน่ง
                }

                decimal totalDividend = results.Sum(r => r.Dividend);
                // ถ้าต้องการแสดงผลรวม สามารถเพิ่ม Label ได้ที่นี่
            }
            catch (Exception ex)
            {
                MessageBox.Show($"เกิดข้อผิดพลาด: {ex.Message}", "ข้อผิดพลาด",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
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