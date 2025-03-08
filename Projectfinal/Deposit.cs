using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using PdfSharp.Drawing;
using PdfSharp.Fonts;
using Projectfinal.Model;
using PdfSharp.Fonts;
using PdfSharp.Pdf;
using PdfSharp.Drawing;
using IoPath = System.IO.Path;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.StartPanel;


namespace Projectfinal
{
    public partial class Deposit : Form
    {
        private readonly dbcontext _dbContext = new dbcontext();
        public Deposit()
        {
            InitializeComponent();
            this.WindowState = FormWindowState.Maximized;
            txtusername.TextChanged += TxtUsername_TextChanged;
            txtMoneyLast.TextChanged += txtMoneyLast_TextChanged;
        }

        // delete
        private void button2_Click(object sender, EventArgs e)
        {
            try
            {
                string username = txtusername.Text;
                if (string.IsNullOrEmpty(username))
                {
                    MessageBox.Show("กรุณากรอกชื่อผู้ใช้", "ข้อผิดพลาด", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                // ค้นหาผู้ใช้จากฐานข้อมูล
                var user = _dbContext.Users.FirstOrDefault(u => u.Username == username);
                if (user != null)
                {
                    // ค้นหาข้อมูลการทำธุรกรรมล่าสุดของผู้ใช้
                    var transaction = _dbContext.MoneyTranss
                        .Where(t => t.Username == username)
                        .OrderByDescending(t => t.TimeMoney)
                        .FirstOrDefault();

                    if (transaction != null)
                    {
                        // ยืนยันการลบข้อมูล
                        var confirmResult = MessageBox.Show("คุณต้องการลบข้อมูลการทำธุรกรรมนี้ใช่หรือไม่?", "ยืนยันการลบ", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                        if (confirmResult == DialogResult.Yes)
                        {
                            // ลบข้อมูลการทำธุรกรรม
                            _dbContext.MoneyTranss.Remove(transaction);
                            _dbContext.SaveChanges();

                            MessageBox.Show("ลบข้อมูลการทำธุรกรรมสำเร็จ", "สำเร็จ", MessageBoxButtons.OK, MessageBoxIcon.Information);

                            // ล้างค่าที่ป้อนในฟอร์มหลังจากลบสำเร็จ
                            ClearFields();
                        }
                    }
                    else
                    {
                        MessageBox.Show("ไม่พบข้อมูลการทำธุรกรรม", "ข้อผิดพลาด", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
                else
                {
                    MessageBox.Show("ไม่พบข้อมูลผู้ใช้", "ข้อผิดพลาด", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    ClearFields();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"เกิดข้อผิดพลาด: {ex.Message}\n{ex.StackTrace}", "ข้อผิดพลาด", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            new Main().Show();
            this.Hide();
        }

        private void Deposit_Load(object sender, EventArgs e)
        {

        }
        private void TxtUsername_TextChanged(object sender, EventArgs e)
        {
            try
            {
                string username = txtusername.Text;
                if (string.IsNullOrEmpty(username))
                {
                    MessageBox.Show("กรุณากรอกชื่อผู้ใช้", "ข้อผิดพลาด", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    ClearFields();
                    return;
                }

                var user = _dbContext.Users.FirstOrDefault(u => u.Username == username);
                if (user != null)
                {
                    txtFamily.Text = user.Family;
                    txtPhone.Text = user.Phone;
                    txtFullname.Text = user.Fullname;

                    // Fetch the latest transaction for this user, if available
                    var currentTransaction = _dbContext.MoneyTranss
                        .Where(t => t.Username == username)
                        .OrderByDescending(t => t.TimeMoney)
                        .FirstOrDefault();

                    if (currentTransaction != null)
                    {
                        txtMoneyOld.Text = currentTransaction.MoneyTotal.ToString("N2");
                        txtMoneyLast.Clear();
                        txtMoneyTotal.Clear();
                    }
                    else
                    {
                        txtMoneyOld.Text = "0.00";
                        txtMoneyLast.Clear();
                        txtMoneyTotal.Clear();
                    }
                }
                else
                {

                    ClearFields();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"เกิดข้อผิดพลาด: {ex.Message}\n{ex.StackTrace}", "ข้อผิดพลาด", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void ClearFields()
        {
            txtFamily.Clear();
            txtPhone.Clear();
            txtFullname.Clear();
            txtMoneyOld.Text = "0.00";
            txtMoneyLast.Clear();
            txtMoneyTotal.Clear();
        }
        private void txtMoneyLast_TextChanged(object sender, EventArgs e)
        {
            try
            {
                // ตรวจสอบว่า txtMoneyOld และ txtMoneyLast มีค่าเป็นตัวเลขหรือไม่
                if (decimal.TryParse(txtMoneyOld.Text, out decimal moneyOld) &&
                    decimal.TryParse(txtMoneyLast.Text, out decimal moneyLast))
                {
                    // คำนวณยอดเงินรวม
                    decimal moneyTotal = moneyOld + moneyLast;
                    txtMoneyTotal.Text = moneyTotal.ToString("N2"); // แสดงผลเป็นตัวเลขที่มีทศนิยม 2 ตำแหน่ง
                }
                else
                {
                    // ถ้าค่าที่ป้อนไม่ใช่ตัวเลข ให้ตั้งค่าเริ่มต้น
                    txtMoneyOld.Text = "0.00";
                    txtMoneyTotal.Clear();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"เกิดข้อผิดพลาด: {ex.Message}\n{ex.StackTrace}", "ข้อผิดพลาด", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }



        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                // ตรวจสอบค่าที่ป้อนให้ถูกต้อง
                if (!decimal.TryParse(txtMoneyLast.Text, out decimal moneyLast))
                {
                    MessageBox.Show("กรุณากรอกจำนวนเงินฝากใหม่ให้ถูกต้อง", "ข้อผิดพลาด", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                if (!decimal.TryParse(txtMoneyOld.Text, out decimal moneyOld))
                {
                    MessageBox.Show("กรุณากรอกจำนวนเงินฝากเก่าให้ถูกต้อง", "ข้อผิดพลาด", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                // สร้างวัตถุ MoneyTrans เพื่อบันทึกข้อมูลลงฐานข้อมูล
                var moneytrans = new MoneyTrans()
                {
                    Username = txtusername.Text,
                    Family = txtFamily.Text,
                    Phone = txtPhone.Text,
                    Fullname = txtFullname.Text,
                    MoneyLast = moneyLast,
                    MoneyOld = moneyOld,
                    MoneyTotal = moneyOld + moneyLast,
                    TimeMoney = dateTimePicker2.Value // ใช้ค่า DateTime จาก dateTimePicker
                };

                // บันทึกข้อมูลลงฐานข้อมูล
                _dbContext.MoneyTranss.Add(moneytrans);
                _dbContext.SaveChanges();

                MessageBox.Show("บันทึกข้อมูลสำเร็จ", "สำเร็จ", MessageBoxButtons.OK, MessageBoxIcon.Information);

                // ล้างค่าที่ป้อนในฟอร์มหลังจากบันทึกสำเร็จ
                ClearFields();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"เกิดข้อผิดพลาด: {ex.Message}\n{ex.StackTrace}", "ข้อผิดพลาด", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void print_Click(object sender, EventArgs e)
        {
            try
            {
                // Register custom font resolver for Thai fonts
                GlobalFontSettings.FontResolver = new CustomFontResolver();

                // Create directory on desktop
                string directoryPath = @"C:\Users\krisa\source\repos\finalbank\Projectfinal\Filepdf";

                if (!Directory.Exists(directoryPath))
                {
                    Directory.CreateDirectory(directoryPath);
                }

                // Create PDF filename
                string fileName = $"Deposit{DateTime.Now:yyyyMMddHHmmss}.pdf";
                string fullPath = Path.Combine(directoryPath, fileName);

                // Create document
                PdfDocument document = new PdfDocument();
                document.Info.Title = "ใบเสร็จการฝากเงิน";
                PdfPage page = document.AddPage();
                XGraphics gfx = XGraphics.FromPdfPage(page);

                // Define fonts with Unicode encoding for Thai support
                XPdfFontOptions options = new XPdfFontOptions(PdfFontEncoding.Unicode);
                XFont titleFont = new XFont("Kanit-Bold", 18);
                XFont subtitleFont = new XFont("Kanit-Bold", 14);
                XFont headerFont = new XFont("Kanit-Bold", 12);
                XFont contentFont = new XFont("Kanit-Bold", 11);

                // Draw header and title
                gfx.DrawString("ระบบบริหารจัดการกลุ่มออมทรัพย์เพื่อการผลิตบ้านท่ารวก", titleFont, XBrushes.Black,
                             new XRect(0, 40, page.Width, 30), XStringFormats.Center);

                gfx.DrawString("ตำบลหนองยายโต๊ะ อำเภอชัยบาดาล จังหวัดลพบุรี", subtitleFont, XBrushes.Black,
                              new XRect(0, 70, page.Width, 30), XStringFormats.Center);

                gfx.DrawString("รายงานการฝากเงิน", headerFont, XBrushes.Black,
                              new XRect(0, 100, page.Width, 30), XStringFormats.Center);

                // Draw line
                XPen pen = new XPen(XColors.Black, 1);
                gfx.DrawLine(pen, 50, 130, page.Width - 50, 130);

                // Get form data
                string username = txtusername.Text;
                string family = txtFamily.Text;
                string fullname = txtFullname.Text;
                string phone = txtPhone.Text;
                string moneyOld = txtMoneyOld.Text;
                string moneyLast = txtMoneyLast.Text;
                string moneyTotal = txtMoneyTotal.Text;
                string transactionDate = dateTimePicker2.Value.ToString("dd/MM/yyyy HH:mm:ss");

                // Set initial Y position for content
                double y = 150;
                double leftX = 100;
                double rightX = 250;
                double lineHeight = 25;

                // Draw transaction details
                gfx.DrawString("วันที่-เวลาทำรายการ:", headerFont, XBrushes.Black, leftX, y);
                gfx.DrawString(transactionDate, contentFont, XBrushes.Black, rightX, y);
                y += lineHeight;

                gfx.DrawString("ชื่อผู้ใช้:", headerFont, XBrushes.Black, leftX, y);
                gfx.DrawString(username, contentFont, XBrushes.Black, rightX, y);
                y += lineHeight;

                gfx.DrawString("ชื่อ-นามสกุล:", headerFont, XBrushes.Black, leftX, y);
                gfx.DrawString(fullname, contentFont, XBrushes.Black, rightX, y);
                y += lineHeight;

                gfx.DrawString("กลุ่ม:", headerFont, XBrushes.Black, leftX, y);
                gfx.DrawString(family, contentFont, XBrushes.Black, rightX, y);
                y += lineHeight;

                gfx.DrawString("เบอร์โทรศัพท์:", headerFont, XBrushes.Black, leftX, y);
                gfx.DrawString(phone, contentFont, XBrushes.Black, rightX, y);
                y += lineHeight + 10;

                // Draw line
                gfx.DrawLine(pen, 50, y, page.Width - 50, y);
                y += 20;

                // Draw financial information
                gfx.DrawString("รายละเอียดการฝากเงิน", headerFont, XBrushes.Black,
                             new XRect(0, y, page.Width, 30), XStringFormats.Center);
                y += lineHeight + 10;

                gfx.DrawString("ยอดเงินคงเหลือก่อนฝาก:", headerFont, XBrushes.Black, leftX, y);
                gfx.DrawString(moneyOld + " บาท", contentFont, XBrushes.Black, rightX, y);
                y += lineHeight;

                gfx.DrawString("จำนวนเงินที่ฝาก:", headerFont, XBrushes.Black, leftX, y);
                gfx.DrawString(moneyLast + " บาท", contentFont, XBrushes.Black, rightX, y);
                y += lineHeight;

                // Draw line
                gfx.DrawLine(pen, 50, y, page.Width - 50, y);
                y += lineHeight;

                // Draw total
                XFont totalFont = new XFont("Kanit-Bold", 14);
                gfx.DrawString("ยอดเงินคงเหลือหลังฝาก:", totalFont, XBrushes.Black, leftX, y);
                gfx.DrawString(moneyTotal + " บาท", totalFont, XBrushes.Black, rightX, y);
                y += lineHeight + 40;

                // Draw signature lines
                double sigLineX1 = 100;
                double sigLineX2 = page.Width - 100;
                double sigWidth = 150;

                gfx.DrawLine(pen, sigLineX1, y, sigLineX1 + sigWidth, y);
                gfx.DrawLine(pen, sigLineX2 - sigWidth, y, sigLineX2, y);
                y += 5;

                gfx.DrawString("ลายมือชื่อผู้ฝาก", contentFont, XBrushes.Black,
                             new XRect(sigLineX1, y, sigWidth, 20), XStringFormats.Center);
                gfx.DrawString("ลายมือชื่อผู้รับฝาก", contentFont, XBrushes.Black,
                             new XRect(sigLineX2 - sigWidth, y, sigWidth, 20), XStringFormats.Center);

                // Add footer
                y = page.Height - 50;
                gfx.DrawString("เอกสารฉบับนี้ออกโดยระบบบริหารจัดการกลุ่มออมทรัพย์เพื่อการผลิตบ้านท่ารวก",
                             new XFont("Kanit-Bold", 9), XBrushes.Black,
                             new XRect(0, y, page.Width, 20), XStringFormats.Center);

                gfx.DrawString("วันที่พิมพ์: " + DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss"),
                             new XFont("Kanit-Bold", 9), XBrushes.Black,
                             new XRect(0, y + 15, page.Width, 20), XStringFormats.Center);

                // Save document
                document.Save(fullPath);

                if (File.Exists(fullPath))
                {
                    MessageBox.Show($"สร้าง PDF สำเร็จ!\nบันทึกที่: {fullPath}",
                                   "สำเร็จ", MessageBoxButtons.OK, MessageBoxIcon.Information);

                    try
                    {
                        System.Diagnostics.Process.Start("explorer.exe", fullPath);
                    }
                    catch
                    {
                        // Ignore if file opening fails
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"เกิดข้อผิดพลาด: {ex.Message}\n{ex.StackTrace}", "ข้อผิดพลาด", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        // Custom font resolver class to handle Thai font
        public class CustomFontResolver : IFontResolver
        {
            public byte[] GetFont(string faceName)
            {
                if (faceName.StartsWith("Kanit-Bold", StringComparison.OrdinalIgnoreCase))
                {
                    string fontPath = @"C:\Users\krisa\source\repos\finalbank\Projectfinal\Fonts\Kanit-Bold.ttf";
                    return File.ReadAllBytes(fontPath);
                }
                return null;
            }

            public FontResolverInfo ResolveTypeface(string familyName, bool isBold, bool isItalic)
            {
                if (familyName.Equals("Kanit-Bold", StringComparison.OrdinalIgnoreCase))
                {
                    return new FontResolverInfo("Kanit-Bold");
                }
                // Fallback to standard fonts if Kanit is not available
                return PlatformFontResolver.ResolveTypeface("Arial", isBold, isItalic);
            }

            private static readonly IFontResolver PlatformFontResolver = GlobalFontSettings.FontResolver;

            public string DefaultFontName => "Kanit-Bold";
        }
    }
}
