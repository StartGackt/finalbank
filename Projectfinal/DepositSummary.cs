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
using static System.Runtime.InteropServices.JavaScript.JSType;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.StartPanel;

namespace Projectfinal
{
    public partial class DepositSummary : Form
    {
        private readonly dbcontext _dbContext = new dbcontext();
        public DepositSummary()
        {
            InitializeComponent();
            this.WindowState = FormWindowState.Maximized;
            // Required for PdfSharp to support UTF-8 encoding (for Thai characters)
            Encoding.RegisterProvider(CodePagesEncodingProvider.Instance);

            txtusername.TextChanged += TxtUsername_TextChanged;
        }

        private void button3_Click(object sender, EventArgs e)
        {

            new Main().Show();
            this.Hide();
        }


        private void DepositSummary_Load(object sender, EventArgs e)
        {

        }

        private void TxtUsername_TextChanged(object sender, EventArgs e)
        {
            try
            {
                string username = txtusername.Text;

                if (string.IsNullOrEmpty(username))
                {
                    // Clear the DataGridView and txtTotalMoneyLone when username is empty
                    dataGridView1.DataSource = null;
                    txtTotalMoneyLone.Text = "";
                    return;
                }

                // Search for user data
                var user = _dbContext.Users.FirstOrDefault(u => u.Username == username);
                if (user != null)
                {
                    txtFamily.Text = user.Family;
                    txtFullname.Text = user.Fullname;

                    // Get all transactions for this user and order by date descending
                    var transactions = _dbContext.MoneyTranss
                        .Where(t => t.Username == username)
                        .OrderByDescending(t => t.TimeMoney)  // Changed from TransactionDate to TimeMoney
                        .ToList();

                    // Display transactions in DataGridView
                    dataGridView1.DataSource = transactions;

                    // Get the latest deposit amount
                    var latestDeposit = transactions.FirstOrDefault();
                    if (latestDeposit != null)
                    {
                        txtTotalMoneyLone.Text = latestDeposit.MoneyTotal.ToString("N2"); // Changed from Amount to MoneyTotal
                    }
                    else
                    {
                        txtTotalMoneyLone.Text = "0.00";
                    }

                    // Optional: Format the DataGridView
                    FormatDataGridView();
                }
                else
                {
                    // Clear all fields if user not found
                    txtFamily.Text = "";
                    txtFullname.Text = "";
                    dataGridView1.DataSource = null;
                    txtTotalMoneyLone.Text = "";
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("An error occurred: " + ex.Message, "Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void FormatDataGridView()
        {
            dataGridView1.AutoResizeColumns();
            dataGridView1.AlternatingRowsDefaultCellStyle.BackColor = Color.LightGray;
            dataGridView1.EnableHeadersVisualStyles = false;
            dataGridView1.ColumnHeadersDefaultCellStyle.BackColor = Color.LightBlue;
            dataGridView1.ColumnHeadersDefaultCellStyle.ForeColor = Color.Black;

            // Set column headers in Thai or English as needed
            if (dataGridView1.Columns.Count > 0)
            {
                dataGridView1.Columns["Username"].HeaderText = "ชื่อผู้ใช้";
                dataGridView1.Columns["Family"].HeaderText = "ครอบครัว";
                dataGridView1.Columns["Phone"].HeaderText = "เบอร์โทร";
                dataGridView1.Columns["Fullname"].HeaderText = "ชื่อ-นามสกุล";
                dataGridView1.Columns["MoneyOld"].HeaderText = "เงินเก่า";
                dataGridView1.Columns["MoneyLast"].HeaderText = "เงินล่าสุด";
                dataGridView1.Columns["MoneyTotal"].HeaderText = "เงินรวม";
                dataGridView1.Columns["TimeMoney"].HeaderText = "วันที่ทำรายการ";
            }
        }

        private void print_Click(object sender, EventArgs e)
        {
            try
            {
                // 📌 สร้างโฟลเดอร์ปลายทาง
                string directoryPath = new PathConf().getPDFPath();
                if (!Directory.Exists(directoryPath))
                {
                    Directory.CreateDirectory(directoryPath);
                }

                // 📌 สร้างชื่อไฟล์ PDF ตามวันที่
                string fileName = $"DepositYearReport_{DateTime.Now:yyyyMMddHHmmss}.pdf";
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
                double columnWidth = (page.Width - 100) / 9; // คำนวณให้แต่ละคอลัมน์กว้างเท่ากัน
                double rowHeight = 20;

                // 🔹 วาด Header ของตาราง
                string[] headers = { "ID", "ชื่อผู้ใช้", "ครอบครัว", "เบอร์โทร", "ชื่อ-นามสกุล", "เงินเก่า", "เงินล่าสุด", "เงินรวม", "วันที่ทำรายการ" };
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

                gfx.DrawLine(pen, 50, 130, page.Width - 50, 130);

                //gfx.DrawString("รวมเป็นเงิน " + txtTotalMoneyLone.Text, headerFont, XBrushes.Black,
                //    new XRect(0, 240, page.Width, 20), XStringFormats.Center);

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
