using System;
using System.Diagnostics;
using System.Linq;
using System.Windows.Forms;
using Microsoft.EntityFrameworkCore;
using PdfSharp.Drawing;
using PdfSharp.Pdf;
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

        private void print_Click(object sender, EventArgs e)
        {
            try
            {
                // 📌 สร้างโฟลเดอร์ปลายทางบน
                string directoryPath = new PathConf().getPDFPath();
                if (!Directory.Exists(directoryPath))
                {
                    Directory.CreateDirectory(directoryPath);
                }

                // 📌 สร้างชื่อไฟล์ PDF ตามวันที่
                string fileName = $"DepositOraMonthReport_{DateTime.Now:yyyyMMddHHmmss}.pdf";
                string fullPath = Path.Combine(directoryPath, fileName);

                // 📌 สร้างเอกสาร PDF
                PdfDocument document = new PdfDocument();
                document.Info.Title = "รายงานการฝากเงิน";
                PdfPage page = document.AddPage();
                XGraphics gfx = XGraphics.FromPdfPage(page);

                // 📌 กำหนดฟอนต์สำหรับภาษาไทย
                XFont titleFont = new XFont("Kanit-Bold", 18);
                XFont headerFont = new XFont("Kanit-Bold", 12);
                XFont contentFont = new XFont("Kanit-Bold", 10);
                XPen pen = new XPen(XColors.Black, 1);

                // 🔹 วาดหัวเรื่อง
                gfx.DrawString("ระบบบริหารจัดการกลุ่มออมทรัพย์", titleFont, XBrushes.Black,
                    new XRect(0, 40, page.Width, 30), XStringFormats.Center);
                gfx.DrawString("ตำบลหนองยายโต๊ะ อำเภอชัยบาดาล จังหวัดลพบุรี", headerFont, XBrushes.Black,
                    new XRect(0, 70, page.Width, 20), XStringFormats.Center);
                gfx.DrawString("รายงานการฝากเงิน", headerFont, XBrushes.Black,
                    new XRect(0, 100, page.Width, 20), XStringFormats.Center);

                // 🔹 วาดเส้นใต้หัวข้อ
                gfx.DrawLine(pen, 50, 130, page.Width - 50, 130);

                // 🔹 กำหนดตำแหน่งเริ่มต้นของข้อมูล
                double y = 150;
                double leftX = 50;
                double columnWidth = (page.Width - 100) / 5; // คำนวณให้แต่ละคอลัมน์กว้างเท่ากัน
                double rowHeight = 20;

                // 🔹 วาด Header ของตาราง
                string[] headers = { "Username", "Family", "Fullname", "Money Total", "Time" };
                double currentX = leftX;
                foreach (var header in headers)
                {
                    gfx.DrawString(header, headerFont, XBrushes.Black, new XPoint(currentX, y));
                    currentX += columnWidth;
                }
                y += rowHeight;

                // 🔹 วาดข้อมูลจาก DataGridView
                foreach (DataGridViewRow row in dataGridView1.Rows)
                {
                    if (row.IsNewRow) continue; // ข้ามแถวว่าง

                    currentX = leftX;
                    for (int i = 0; i < headers.Length; i++)
                    {
                        string value = row.Cells[i].Value?.ToString() ?? "";
                        gfx.DrawString(value, contentFont, XBrushes.Black, new XPoint(currentX, y));
                        currentX += columnWidth;
                    }
                    y += rowHeight;

                    // 🛑 ตรวจสอบว่าต้องขึ้นหน้าใหม่หรือไม่
                    if (y > page.Height - 50)
                    {
                        page = document.AddPage();
                        gfx = XGraphics.FromPdfPage(page);
                        y = 50;
                    }
                }

                // 📌 บันทึกไฟล์ PDF
                document.Save(fullPath);

                // 📌 เปิดไฟล์ PDF หลังจากสร้างเสร็จ
                if (File.Exists(fullPath))
                {
                    MessageBox.Show($"สร้าง PDF สำเร็จ!\nบันทึกที่: {fullPath}",
                        "สำเร็จ", MessageBoxButtons.OK, MessageBoxIcon.Information);

                    try
                    {
                        Process.Start("explorer.exe", fullPath);
                    }
                    catch
                    {
                        // ถ้าเปิดไฟล์ไม่สำเร็จ ให้ข้ามไป
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"เกิดข้อผิดพลาด: {ex.Message}\n{ex.StackTrace}",
                    "ข้อผิดพลาด", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}
