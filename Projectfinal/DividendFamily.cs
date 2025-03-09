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
using PdfSharp.Pdf;
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

        private void print_Click(object sender, EventArgs e)
        {
            try
            {
                // สร้างโฟลเดอร์ปลายทาง
                string directoryPath = new PathConf().getPDFPath();
                if (!Directory.Exists(directoryPath))
                {
                    Directory.CreateDirectory(directoryPath);
                }

                // สร้างชื่อไฟล์ PDF ตามวันที่
                string fileName = $"DividendFamily_{DateTime.Now:yyyyMMddHHmmss}.pdf";
                string fullPath = Path.Combine(directoryPath, fileName);

                // สร้างเอกสาร PDF
                PdfDocument document = new PdfDocument();
                document.Info.Title = "รายงานเงินปันผลตามครอบครัว";
                PdfPage page = document.AddPage();
                XGraphics gfx = XGraphics.FromPdfPage(page);

                // กำหนดฟอนต์สำหรับภาษาไทย (ตรวจสอบว่ามีฟอนต์ในระบบ)
                // ถ้าใช้ฟอนต์ภาษาไทยไม่ได้ ให้ใช้ฟอนต์ที่รองรับ Unicode แทน
                XFont titleFont;
                XFont headerFont;
                XFont contentFont;

                try
                {
                    titleFont = new XFont("Kanit-Bold", 18);
                    headerFont = new XFont("Kanit-Bold", 12);
                    contentFont = new XFont("Kanit-Bold", 10);
                }
                catch
                {
                    // ใช้ฟอนต์สำรองถ้าไม่มี Kanit
                    titleFont = new XFont("Arial Unicode MS", 18);
                    headerFont = new XFont("Arial Unicode MS", 12);
                    contentFont = new XFont("Arial Unicode MS", 10);
                }

                XPen pen = new XPen(XColors.Black, 1);

                // วาดหัวเรื่อง
                gfx.DrawString("ระบบบริหารจัดการกลุ่มออมทรัพย์", titleFont, XBrushes.Black,
                    new XRect(0, 40, page.Width, 30), XStringFormats.Center);
                gfx.DrawString("ตำบลหนองยายโต๊ะ อำเภอชัยบาดาล จังหวัดลพบุรี", headerFont, XBrushes.Black,
                    new XRect(0, 70, page.Width, 20), XStringFormats.Center);
                gfx.DrawString("รายงานเงินปันผลตามครอบครัว", headerFont, XBrushes.Black,
                    new XRect(0, 100, page.Width, 20), XStringFormats.Center);

                // วาดเส้นใต้หัวข้อ
                gfx.DrawLine(pen, 50, 130, page.Width - 50, 130);

                // กำหนดตำแหน่งเริ่มต้นของข้อมูล
                double y = 150;
                double leftX = 50;
                // ลดจำนวนคอลัมน์ให้เหมาะกับข้อมูลที่แสดงจริง
                double columnWidth = (page.Width - 100) / 4; // แสดงเพียง 4 คอลัมน์ตามข้อมูลที่ query มา
                double rowHeight = 25; // เพิ่มความสูงของแถวเพื่อให้แสดงภาษาไทยได้ดีขึ้น

                // วาด Header ของตาราง
                string[] headers = { "ชื่อผู้ใช้", "ชื่อ-นามสกุล", "เงินเก่า", "เงินปันผล" };
                double currentX = leftX;

                // วาดพื้นหลังของส่วนหัวตาราง
                XRect headerRect = new XRect(leftX - 5, y - 15, (page.Width - 100) + 10, rowHeight);
                gfx.DrawRectangle(new XSolidBrush(XColor.FromArgb(230, 230, 230)), headerRect);

                foreach (var header in headers)
                {
                    gfx.DrawString(header, headerFont, XBrushes.Black, new XPoint(currentX, y));
                    currentX += columnWidth;
                }
                y += rowHeight;

                // วาดเส้นคั่นระหว่างส่วนหัวกับข้อมูล
                gfx.DrawLine(pen, leftX - 5, y - 10, page.Width - leftX + 5, y - 10);

                // คำนวณยอดรวม
                decimal totalMoneyOld = 0;
                decimal totalDividend = 0;

                // วาดข้อมูลจาก DataGridView
                bool isAlternateRow = false;
                foreach (DataGridViewRow row in dataGridView1.Rows)
                {
                    if (row.IsNewRow) continue; // ข้ามแถวว่าง

                    // สลับสีพื้นหลังของแถว
                    if (isAlternateRow)
                    {
                        XRect rowRect = new XRect(leftX - 5, y - 15, (page.Width - 100) + 10, rowHeight);
                        gfx.DrawRectangle(new XSolidBrush(XColor.FromArgb(245, 245, 245)), rowRect);
                    }
                    isAlternateRow = !isAlternateRow;

                    currentX = leftX;

                    // ใช้เฉพาะคอลัมน์ที่ต้องการ (Username, Fullname, MoneyOld, Dividend)
                    string username = row.Cells["Username"].Value?.ToString() ?? "";
                    string fullname = row.Cells["Fullname"].Value?.ToString() ?? "";
                    string moneyOld = row.Cells["MoneyOld"].Value?.ToString() ?? "0";
                    string dividend = row.Cells["Dividend"].Value?.ToString() ?? "0";

                    // คำนวณยอดรวม
                    decimal moneyOldValue = 0;
                    decimal dividendValue = 0;
                    decimal.TryParse(moneyOld, out moneyOldValue);
                    decimal.TryParse(dividend, out dividendValue);
                    totalMoneyOld += moneyOldValue;
                    totalDividend += dividendValue;

                    // แสดงข้อมูลในแต่ละคอลัมน์
                    gfx.DrawString(username, contentFont, XBrushes.Black, new XPoint(currentX, y));
                    currentX += columnWidth;
                    gfx.DrawString(fullname, contentFont, XBrushes.Black, new XPoint(currentX, y));
                    currentX += columnWidth;
                    gfx.DrawString(moneyOldValue.ToString("N2"), contentFont, XBrushes.Black, new XPoint(currentX, y));
                    currentX += columnWidth;
                    gfx.DrawString(dividendValue.ToString("N2"), contentFont, XBrushes.Black, new XPoint(currentX, y));

                    y += rowHeight;

                    // ตรวจสอบว่าต้องขึ้นหน้าใหม่หรือไม่
                    if (y > page.Height - 100) // เพิ่มพื้นที่ด้านล่างสำหรับยอดรวม
                    {
                        // สร้างเส้นด้านล่างของตาราง
                        gfx.DrawLine(pen, leftX - 5, y - 10, page.Width - leftX + 5, y - 10);

                        page = document.AddPage();
                        gfx = XGraphics.FromPdfPage(page);
                        y = 50;

                        // วาดส่วนหัวของตารางในหน้าใหม่
                        currentX = leftX;

                        // วาดพื้นหลังของส่วนหัวตาราง
                        headerRect = new XRect(leftX - 5, y - 15, (page.Width - 100) + 10, rowHeight);
                        gfx.DrawRectangle(new XSolidBrush(XColor.FromArgb(230, 230, 230)), headerRect);

                        foreach (var header in headers)
                        {
                            gfx.DrawString(header, headerFont, XBrushes.Black, new XPoint(currentX, y));
                            currentX += columnWidth;
                        }
                        y += rowHeight;

                        // วาดเส้นคั่นระหว่างส่วนหัวกับข้อมูล
                        gfx.DrawLine(pen, leftX - 5, y - 10, page.Width - leftX + 5, y - 10);
                    }
                }

                // สร้างเส้นด้านล่างของตาราง
                gfx.DrawLine(pen, leftX - 5, y - 10, page.Width - leftX + 5, y - 10);

                
                // แสดงวันที่พิมพ์รายงาน
                y += 40;
                gfx.DrawString("วันที่พิมพ์: " + DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss"), contentFont, XBrushes.Black,
                    new XRect(leftX, y, page.Width - 100, 20), XStringFormats.TopLeft);

                // บันทึกไฟล์ PDF
                document.Save(fullPath);

                // เปิดไฟล์ PDF หลังจากสร้างเสร็จ
                if (File.Exists(fullPath))
                {
                    MessageBox.Show($"สร้าง PDF สำเร็จ!\nบันทึกที่: {fullPath}",
                        "สำเร็จ", MessageBoxButtons.OK, MessageBoxIcon.Information);

                    try
                    {
                        Process.Start(new ProcessStartInfo
                        {
                            FileName = fullPath,
                            UseShellExecute = true
                        });
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show($"ไม่สามารถเปิดไฟล์ PDF ได้: {ex.Message}",
                            "คำเตือน", MessageBoxButtons.OK, MessageBoxIcon.Warning);
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
