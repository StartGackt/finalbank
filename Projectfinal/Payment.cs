using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using PdfSharp.Drawing;
using PdfSharp.Fonts;
using PdfSharp.Pdf;
using Projectfinal.Model;
using static System.Runtime.InteropServices.JavaScript.JSType;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.StartPanel;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;
using iText.Kernel.Pdf.Canvas.Parser.ClipperLib;

namespace Projectfinal
{
    public partial class Payment : Form
    {
        private readonly dbcontext _dbContext = new dbcontext();

        public string? Username { get; private set; }
        public string? Nuneycetegory { get; private set; }
        public string Userpay { get; private set; }
        public string? Fullname { get; private set; }
        public string? MoneyFirst { get; private set; }
        public string Family { get; private set; }
        public string? NumberLone { get; private set; }
        public string? Interest { get; private set; }
        public string? Total { get; private set; }

        public Payment()
        {
            InitializeComponent();
            this.WindowState = FormWindowState.Maximized;
            txtusername.TextChanged += TxtUsername_TextChanged;
            txtNuneycetegory.TextChanged += txtNuneycetegory_TextChanged;
            txtuserpay.TextChanged += TxtUserPay_TextChanged; // Add this line

            // Required for PdfSharp to support UTF-8 encoding (for Thai characters)
            Encoding.RegisterProvider(CodePagesEncodingProvider.Instance);

            // Register custom font resolver for Thai font support
        }

        private void button3_Click(object sender, EventArgs e)
        {
            new Main().Show();
            this.Hide();
        }

        private void Payment_Load(object sender, EventArgs e)
        {

        }

        private void TxtUsername_TextChanged(object sender, EventArgs e)
        {
            try
            {
                string username = txtusername.Text;

                if (string.IsNullOrEmpty(username))
                {
                    ClearUserFields();
                    return;
                }

                // Search for user data
                var user = _dbContext.Users.FirstOrDefault(u => u.Username == username);
                if (user != null)
                {
                    txtFamily.Text = user.Family;
                    txtFullname.Text = user.Fullname;

                    // Clear loan information when a new username is entered
                    ClearLoanFields();
                }
                else
                {
                    ClearUserFields();
                }
            }
            catch (Exception ex)
            {
                ShowError(ex);
            }
        }

        private void txtNuneycetegory_TextChanged(object sender, EventArgs e)
        {
            try
            {
                string username = txtusername.Text;
                string Nuneycetegory = txtNuneycetegory.Text.Trim();

                if (string.IsNullOrEmpty(username))
                {
                    MessageBox.Show("กรุณากรอกชื่อผู้ใช้ก่อน", "ข้อมูลไม่ครบ", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    ClearLoanFields();
                    return;
                }

                bool userHasLoanData = _dbContext.OrdLones.Any(o => o.Username == username) || _dbContext.Emers.Any(e => e.Username == username);
                if (!userHasLoanData)
                {
                    ClearLoanFields();
                    MessageBox.Show("ไม่พบข้อมูลการกู้สำหรับชื่อผู้ใช้นี้", "ข้อมูลไม่ครบ", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                if (string.IsNullOrEmpty(Nuneycetegory))
                {
                    MessageBox.Show("กรุณากรอกประเภทการกู้", "ข้อมูลไม่ครบ", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    ClearLoanFields();
                    return;
                }

                decimal loanAmount = 0m;
                decimal interestRate = 0m;
                decimal interestAmount = 0m;

                if (Nuneycetegory == "กู้สามัญ")
                {
                    var ordLone = _dbContext.OrdLones.FirstOrDefault(o => o.Username == username);
                    if (ordLone != null)
                    {
                        txtNumberLone.Text = ordLone.NumberLone ?? "ไม่พบข้อมูล";
                        txtMoneyFirst.Text = ordLone.LoneMoney.ToString("F2");

                        loanAmount = Convert.ToDecimal(ordLone.LoneMoney); // Convert double to decimal
                        interestRate = (decimal)0.08; // 8% ดอกเบี้ย
                    }
                    else
                    {
                        ClearLoanFields();
                        MessageBox.Show("ไม่พบข้อมูลการกู้สามัญ", "ข้อมูลไม่ครบ", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        txtNumberLone.Text = "ไม่พบข้อมูล";
                        txtMoneyFirst.Text = "ไม่พบข้อมูล";
                        return;
                    }
                }
                else if (Nuneycetegory == "กู้ฉุกเฉิน")
                {
                    var emerLone = _dbContext.Emers.FirstOrDefault(e => e.Username == username);
                    if (emerLone != null)
                    {
                        txtNumberLone.Text = emerLone.NumberLone ?? "ไม่พบข้อมูล";
                        txtMoneyFirst.Text = emerLone.LoneMoney.ToString("F2");

                        loanAmount = Convert.ToDecimal(emerLone.LoneMoney); // Convert double to decimal
                        interestRate = (decimal)0.01; // 1% ดอกเบี้ย
                    }
                    else
                    {
                        ClearLoanFields();
                        MessageBox.Show("ไม่พบข้อมูลการกู้ฉุกเฉิน", "ข้อมูลไม่ครบ", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        txtNumberLone.Text = "ไม่พบข้อมูล";
                        txtMoneyFirst.Text = "ไม่พบข้อมูล";
                        return;
                    }
                }

                // คำนวณดอกเบี้ย
                interestAmount = loanAmount * interestRate;
                decimal totalWithInterest = loanAmount + interestAmount;

                // แสดงผลลัพธ์
                txtMoneyFirst.Text = loanAmount.ToString("F2");   // เงินหลักที่แยกไว้
                txtInterest.Text = interestAmount.ToString("F2"); // ดอกเบี้ย
            }
            catch (Exception ex)
            {
                ShowError(ex);
            }
        }

        private void TxtUserPay_TextChanged(object sender, EventArgs e)
        {
            try
            {
                // Parse the values from the text boxes
                if (decimal.TryParse(txtMoneyFirst.Text, out decimal moneyFirst) &&
                    decimal.TryParse(txtInterest.Text, out decimal interest) &&
                    decimal.TryParse(txtuserpay.Text, out decimal userPay))
                {
                    // Calculate the total amount with interest
                    decimal totalWithInterest = moneyFirst + interest;

                    // Subtract the user payment from the total
                    decimal moneyLoneTotal = totalWithInterest - userPay;

                    // Display the result
                    txtMoneyLoneTotal.Text = moneyLoneTotal.ToString("F2");
                }
                else
                {
                    // Display an error message if parsing fails
                    txtMoneyLoneTotal.Text = "ไม่ถูกต้อง";
                }
            }
            catch (Exception ex)
            {
                // Show error message if an exception occurs
                ShowError(ex);
            }
        }

        // Clear user fields
        private void ClearUserFields()
        {
            txtFamily.Clear();
            txtFullname.Clear();
            ClearLoanFields(); // Also clear loan information
        }

        // Clear loan fields
        private void ClearLoanFields()
        {
            txtNumberLone.Clear();
            txtMoneyFirst.Clear();
            txtMoneyLoneTotal.Clear(); // Clear the total field as well
        }

        // Display error message
        private void ShowError(Exception ex)
        {
            MessageBox.Show($"เกิดข้อผิดพลาด: {ex.Message}\n{ex.StackTrace}", "ข้อผิดพลาด", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }

        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                // Validate input fields
                if (string.IsNullOrEmpty(txtusername.Text.Trim()) ||
                    string.IsNullOrEmpty(txtFamily.Text.Trim()) ||
                    string.IsNullOrEmpty(txtFullname.Text.Trim()) ||
                    string.IsNullOrEmpty(txtNumberLone.Text.Trim()) ||
                    string.IsNullOrEmpty(txtNuneycetegory.Text.Trim()) ||
                    string.IsNullOrEmpty(txtuserpay.Text.Trim()) ||
                    string.IsNullOrEmpty(txtMoneyFirst.Text.Trim()) ||
                    string.IsNullOrEmpty(txtMoneyLoneTotal.Text.Trim()))
                {
                    MessageBox.Show("กรุณากรอกข้อมูลให้ครบถ้วน", "ข้อผิดพลาด", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                // Parse decimal values
                decimal paymentAmount = decimal.Parse(txtuserpay.Text.Trim());
                decimal originalAmount = decimal.Parse(txtMoneyFirst.Text.Trim());
                decimal interest = decimal.Parse(txtInterest.Text.Trim());
                decimal remainingBalance = decimal.Parse(txtMoneyLoneTotal.Text.Trim());

                // Create new payment record
                var newPayment = new UserPayment
                {
                    Username = txtusername.Text.Trim(),
                    Family = txtFamily.Text.Trim(),
                    Fullname = txtFullname.Text.Trim(),
                    NumberLone = txtNumberLone.Text.Trim(),
                    LoneCategory = txtNuneycetegory.Text.Trim(),
                    PaymentAmount = paymentAmount,
                    OriginalAmount = originalAmount,
                    Interest = interest,
                    RemainingBalance = remainingBalance,
                    PaymentDate = DateTime.Now
                };

                _dbContext.UserPayments.Add(newPayment);
                _dbContext.SaveChanges();

                // Update loan balance
                if (txtNuneycetegory.Text.Trim() == "กู้ฉุกเฉิน")
                {
                    var emerLoan = _dbContext.Emers.FirstOrDefault(e => e.Username == txtusername.Text.Trim());
                    if (emerLoan != null)
                    {
                        emerLoan.TotalMoneyLone = Convert.ToDouble(remainingBalance);
                        emerLoan.LoneMoney = Convert.ToDouble(remainingBalance);
                        _dbContext.SaveChanges();
                    }
                }
                else if (txtNuneycetegory.Text.Trim() == "กู้สามัญ")
                {
                    var ordLoan = _dbContext.OrdLones.FirstOrDefault(o => o.Username == txtusername.Text.Trim());
                    if (ordLoan != null)
                    {
                        ordLoan.LoneMoney = (decimal)Convert.ToDouble(remainingBalance);
                        _dbContext.SaveChanges();
                    }
                }

                MessageBox.Show("บันทึกการชำระเงินเรียบร้อยแล้ว", "สำเร็จ", MessageBoxButtons.OK, MessageBoxIcon.Information);
                ClearAllFields();
            }
            catch (FormatException)
            {
                MessageBox.Show("กรุณากรอกข้อมูลตัวเลขให้ถูกต้อง", "ข้อผิดพลาด", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            catch (Exception ex)
            {
                ShowError(ex);
            }



        }
        private void ClearAllFields()
        {
            txtusername.Clear();
            txtFamily.Clear();
            txtFullname.Clear();
            txtNumberLone.Clear();
            txtNuneycetegory.Clear();
            txtuserpay.Clear();
            txtMoneyFirst.Clear();
            txtInterest.Clear();
            txtMoneyLoneTotal.Clear();
        }

        //print PDF
        private void button2_Click(object sender, EventArgs e)
        {
            try
            {
                // Create directory on desktop
                string directoryPath = new PathConf().getPDFPath();

                //string directoryPath = @"E:\dotNet_Project\jame\Projectfinal\Filepdf";

                if (!Directory.Exists(directoryPath))
                {
                    Directory.CreateDirectory(directoryPath);
                }

                // Create PDF filename
                string fileName = $"ADMIN_Register_{DateTime.Now:yyyyMMddHHmmss}.pdf";
                string fullPath = Path.Combine(directoryPath, fileName);

                // Create document
                PdfDocument document = new PdfDocument();
                document.Info.Title = "Admin Register Information";
                PdfPage page = document.AddPage();
                XGraphics gfx = XGraphics.FromPdfPage(page);

                // Define fonts with Unicode encoding for Thai support
                XPdfFontOptions options = new XPdfFontOptions(PdfFontEncoding.Unicode);
                XFont titleFont = new XFont("Kanit-Bold", 18);
                XFont subtitleFont = new XFont("Kanit-Bold", 14);
                XFont headerFont = new XFont("Kanit-Bold", 11);
                XFont contentFont = new XFont("Kanit-Bold", 11);

                // Draw title
                gfx.DrawString("ระบบบริหารจัดการกลุ่มออมทรัพย์เพื่อการผลิตบ้านท่ารวก", titleFont, XBrushes.Black,
                              new XRect(0, 40, page.Width, 30), XStringFormats.Center);

                gfx.DrawString("ตำบลหนองยายโต๊ะ อำเภอชัยบาดาล จังหวัดลพบุรี", subtitleFont, XBrushes.Black,
                              new XRect(0, 70, page.Width, 30), XStringFormats.Center);

                gfx.DrawString("ข้อมูลเกี่ยวกับสมาชิก : การชำระเงินกู้ของสมาชิก ", subtitleFont, XBrushes.Black,
                              new XRect(0, 100, page.Width, 30), XStringFormats.Center);

                // Get form data
                string username = txtusername.Text;
                if (string.IsNullOrEmpty(username) && !string.IsNullOrEmpty(Username))
                    username = Username;

                string nuneycetegory = txtNuneycetegory.Text;
                if (string.IsNullOrEmpty(nuneycetegory) && !string.IsNullOrEmpty(Nuneycetegory))
                    nuneycetegory = Nuneycetegory;

                string userpay = txtuserpay.Text;
                if (string.IsNullOrEmpty(userpay) && !string.IsNullOrEmpty(Userpay))
                    userpay = Userpay;

                string fullname = txtFullname.Text;
                if (string.IsNullOrEmpty(fullname) && !string.IsNullOrEmpty(Fullname))
                    fullname = Fullname;

                string moneyFirst = txtMoneyFirst.Text;
                if (string.IsNullOrEmpty(moneyFirst) && !string.IsNullOrEmpty(MoneyFirst))
                    moneyFirst = MoneyFirst;

                string family = txtFamily.Text;
                if (string.IsNullOrEmpty(family) && !string.IsNullOrEmpty(Family))
                    family = Family;


                string lone = txtNumberLone.Text;
                if (string.IsNullOrEmpty(lone) && !string.IsNullOrEmpty(NumberLone))
                    lone = NumberLone;


                string interest = txtInterest.Text;
                if (string.IsNullOrEmpty(interest) && !string.IsNullOrEmpty(Interest))
                    interest = Interest;


                string total = txtMoneyLoneTotal.Text;
                if (string.IsNullOrEmpty(total) && !string.IsNullOrEmpty(Total))
                    total = Total;


                // Define table properties
                double tableStartY = 150;
                double tableWidth = 500;
                double leftMargin = 50;
                double rowHeight = 30;
                double col1Width = 150;
                double col2Width = tableWidth - col1Width;

                // Draw table header row with background
                XRect headerRect = new XRect(leftMargin, tableStartY, tableWidth, rowHeight);
                gfx.DrawRectangle(new XSolidBrush(XColor.FromArgb(220, 220, 220)), headerRect);
                gfx.DrawRectangle(new XPen(XColors.Black, 1), headerRect);

                // Draw header text
                XRect labelHeaderRect = new XRect(leftMargin, tableStartY, col1Width, rowHeight);
                XRect valueHeaderRect = new XRect(leftMargin + col1Width, tableStartY, col2Width, rowHeight);

                gfx.DrawString("หัวข้อ", headerFont, XBrushes.Black, labelHeaderRect, XStringFormats.Center);
                gfx.DrawString("ข้อมูล", headerFont, XBrushes.Black, valueHeaderRect, XStringFormats.Center);

                // Draw vertical divider in header
                gfx.DrawLine(new XPen(XColors.Black, 1),
                    new XPoint(leftMargin + col1Width, tableStartY),
                    new XPoint(leftMargin + col1Width, tableStartY + rowHeight));

                double currentY = tableStartY + rowHeight;

                // Draw table rows
                string[,] rowData = new string[,] {
            {"เลขที่สมาชิก", username},
            {"ชื่อ - นามสกุล", fullname},
            {"รหัสครอบครัว", family},
            {"สัญญาเลขที่", lone},
            {"วันที่ฝากชำระ", dateTimePicker2.Text },
            {"ประเภทเงินกู้ ", nuneycetegory},
            {"จำนวนเงินที่สมาชิกชำระ ", userpay},
            {"เป็นเงินต้น", moneyFirst },
            {"ดอกเบี้ย", interest },
            {"จำนวนเงินที่กู้คงเหลือ", total },
            {"เวลาที่บันทึก", DateTime.Now.ToString("dd/MM/yyyy hh:mm:ss tt")}
        };

                for (int i = 0; i < rowData.GetLength(0); i++)
                {
                    // Draw row background with alternating colors
                    bool isEvenRow = i % 2 == 0;
                    XColor rowColor = isEvenRow ? XColor.FromArgb(240, 240, 240) : XColor.FromArgb(255, 255, 255);
                    XRect rowRect = new XRect(leftMargin, currentY, tableWidth, rowHeight);
                    gfx.DrawRectangle(new XSolidBrush(rowColor), rowRect);

                    // Draw row border
                    gfx.DrawRectangle(new XPen(XColors.Black, 1), rowRect);

                    // Draw label column
                    XRect labelRect = new XRect(leftMargin + 5, currentY, col1Width - 5, rowHeight);
                    gfx.DrawString(rowData[i, 0] + ":", headerFont, XBrushes.Black, labelRect,
                        new XStringFormat { LineAlignment = XLineAlignment.Center, Alignment = XStringAlignment.Near });

                    // Draw value column
                    XRect valueRect = new XRect(leftMargin + col1Width + 5, currentY, col2Width - 5, rowHeight);
                    gfx.DrawString(rowData[i, 1], contentFont, XBrushes.Black, valueRect,
                        new XStringFormat { LineAlignment = XLineAlignment.Center, Alignment = XStringAlignment.Near });

                    // Draw vertical divider
                    gfx.DrawLine(new XPen(XColors.Black, 1),
                        new XPoint(leftMargin + col1Width, currentY),
                        new XPoint(leftMargin + col1Width, currentY + rowHeight));

                    currentY += rowHeight;
                }

                // Add footer with date and signature lines
                currentY += 30;
                string dateStr = "วันที่ " + DateTime.Now.ToString("dd MMMM yyyy");
                gfx.DrawString(dateStr, contentFont, XBrushes.Black,
                    new XPoint(page.Width - leftMargin - 200, currentY));

                currentY += 50;

                // Add signature line
                double sigLineX = page.Width - leftMargin - 200;
                double sigLineWidth = 150;

                gfx.DrawLine(new XPen(XColors.Black, 1),
                    new XPoint(sigLineX, currentY),
                    new XPoint(sigLineX + sigLineWidth, currentY));

                currentY += 10;

                // Add signature text
                gfx.DrawString("(ลายเซ็นเจ้าหน้าที่)", contentFont, XBrushes.Black,
                    new XRect(sigLineX - 25, currentY, sigLineWidth + 50, 20), XStringFormats.Center);

                // Save and close document
                document.Save(fullPath);

                if (File.Exists(fullPath))
                {
                    MessageBox.Show($"PDF created successfully!\nSaved at: {fullPath}",
                                   "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);

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
                MessageBox.Show($"PDF creation failed: {ex.Message}\n\nStack trace: {ex.StackTrace}",
                               "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void label8_Click(object sender, EventArgs e)
        {

        }
    }
}