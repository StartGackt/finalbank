using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using PdfSharp.Drawing;
using PdfSharp.Fonts;
using PdfSharp.Pdf;
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
            // Required for PdfSharp to support UTF-8 encoding (for Thai characters)
            Encoding.RegisterProvider(CodePagesEncodingProvider.Instance);

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
                string fileName = $"AllDividend{DateTime.Now:yyyyMMddHHmmss}.pdf";
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
