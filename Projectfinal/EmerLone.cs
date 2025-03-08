using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using PdfSharp.Drawing;
using PdfSharp.Pdf;
using Projectfinal.Model;
using static System.Runtime.InteropServices.JavaScript.JSType;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.StartPanel;


namespace Projectfinal
{
    public partial class EmerLone : Form
    {
        private readonly dbcontext _dbContext = new dbcontext();

        public string? Username { get; private set; }
        public string? Family { get; private set; }
        public string? Fullname { get; private set; }
        public string? NumberLone { get; private set; }
        public string? User1 { get; private set; }
        public string? User2 { get; private set; }
        public string? User3 { get; private set; }
        public string? FullName1 { get; private set; }
        public string? FullName2 { get; private set; }
        public string? FullName3 { get; private set; }
        public string? MoneyOld { get; private set; }
        public string? LoneMoney { get; private set; }
        public string? Interrate { get; private set; }
        public string? Total { get; private set; }

        public EmerLone()
        {
            InitializeComponent();
            this.WindowState = FormWindowState.Maximized;
            txtusername.TextChanged += txtusername_TextChanged;
            txtUsername1.TextChanged += TxtUsername1_TextChanged;
            txtUsername2.TextChanged += TxtUsername2_TextChanged;
            txtUsername3.TextChanged += TxtUsername3_TextChanged;
            txtLoneMoney.TextChanged += CalculateLoneMoney;
            txtInterrate.TextChanged += CalculateLoneMoney;
            GenerateLoneNumber();



        }
        private void txtusername_TextChanged(object sender, EventArgs e)
        {
            SearchUserAndFillFields(txtusername, txtFullname, txtFamily);
        }
        private void TxtUsername1_TextChanged(object sender, EventArgs e)
        {
            // Call the overload that takes three parameters
            SearchUserAndFillFields(txtUsername1, txtFullname1, txtPhone1);
        }

        private void TxtUsername2_TextChanged(object sender, EventArgs e)
        {
            SearchUserAndFillFields(txtUsername2, txtFullname2, txtPhone2);
        }

        private void TxtUsername3_TextChanged(object sender, EventArgs e)
        {
            SearchUserAndFillFields(txtUsername3, txtFullname3, txtPhone3);
        }


        private void SearchUserAndFillFields(TextBox usernameTextBox, TextBox fullNameTextBox, TextBox familyTextBox)
        {
            try
            {
                string username = usernameTextBox.Text;
                if (string.IsNullOrEmpty(username))
                {
                    ClearFields(fullNameTextBox, familyTextBox);
                    return;
                }

                // ค้นหาข้อมูลผู้ใช้
                var user = _dbContext.Users.FirstOrDefault(u => u.Username == username);
                if (user != null)
                {
                    // กรอกข้อมูลพื้นฐาน
                    fullNameTextBox.Text = user.Fullname;
                    familyTextBox.Text = user.Phone;

                    // ดึงข้อมูลเงินคงเหลือล่าสุด
                    var transaction = _dbContext.MoneyTranss
                        .Where(t => t.Username == username)
                        .OrderByDescending(t => t.TimeMoney)
                        .FirstOrDefault();

                    if (transaction != null)
                    {
                        txtMoneyOld.Text = transaction.MoneyTotal.ToString("N2");
                    }
                    else
                    {
                        txtMoneyOld.Text = "0.00";
                    }

                    try
                    {
                        // ดึงข้อมูลการกู้เงินฉุกเฉิน
                        var emerLoan = _dbContext.Emers
                            .Where(e => e.Username == username)
                            .OrderByDescending(e => e.TimeLone)
                            .FirstOrDefault();

                        if (emerLoan != null)
                        {
                            // กรอกข้อมูลการกู้
                            FillLoanDetails(emerLoan);
                        }
                    }
                    catch (Exception ex)
                    {
                        // จัดการกรณีไม่มีตาราง Emers
                        if (ex.Message.Contains("no such table"))
                        {
                            // สร้างตารางถ้ายังไม่มี
                            _dbContext.Database.EnsureCreated();
                        }
                        else
                        {
                            throw;
                        }
                    }
                }
                else
                {

                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"เกิดข้อผิดพลาด: {ex.Message}",
                    "ข้อผิดพลาด", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        // เมธอดสำหรับกรอกข้อมูลการกู้
        private void FillLoanDetails(Emer loan)
        {
            if (loan != null)
            {
                // ข้อมูลผู้กู้
                txtusername.Text = loan.Username;
                txtFamily.Text = loan.Family;
                txtFullname.Text = loan.Fullname;

                // ข้อมูลผู้ค้ำประกันคนที่ 1
                txtUsername1.Text = loan.Username1;
                txtFullname1.Text = loan.Fullname1;
                txtPhone1.Text = loan.Phone1;

                // ข้อมูลผู้ค้ำประกันคนที่ 2
                txtUsername2.Text = loan.Username2;
                txtFullname2.Text = loan.Fullname2;
                txtPhone2.Text = loan.Phone2;

                // ข้อมูลผู้ค้ำประกันคนที่ 3
                txtUsername3.Text = loan.Username3;
                txtFullname3.Text = loan.Fullname3;
                txtPhone3.Text = loan.Phone3;

                // ข้อมูลการกู้
                txtNumberLone.Text = loan.NumberLone;
                txtInterrate.Text = loan.Interrate;
                txtLoneMoney.Text = loan.LoneMoney.ToString("N2");
                txtTotalMoneyLone.Text = loan.TotalMoneyLone.ToString("N2");
            }
        }

        // เมธอดสำหรับล้างข้อมูลทั้งหมด
        private void ClearAllFields()
        {
            txtusername.Clear();
            txtFamily.Clear();
            txtFullname.Clear();
            txtMoneyOld.Clear();

            txtUsername1.Clear();
            txtFullname1.Clear();
            txtPhone1.Clear();

            txtUsername2.Clear();
            txtFullname2.Clear();
            txtPhone2.Clear();

            txtUsername3.Clear();
            txtFullname3.Clear();
            txtPhone3.Clear();

            txtNumberLone.Clear();
            txtInterrate.Clear();
            txtLoneMoney.Clear();
            txtTotalMoneyLone.Clear();
        }

        // เมธอดสำหรับล้างข้อมูลบางฟิลด์
        private void ClearFields(params TextBox[] textBoxes)
        {
            foreach (var textBox in textBoxes)
            {
                textBox.Clear();
            }
        }



        private void GenerateLoneNumber()
        {
            try
            {
                // ดึงข้อมูลล่าสุดจากฐานข้อมูล
                var lastLone = _dbContext.Emers
                    .OrderByDescending(l => l.Id)
                    .FirstOrDefault();

                int newNumber = 1;

                // ตรวจสอบว่ามีข้อมูลเก่าหรือไม่
                if (lastLone != null && !string.IsNullOrEmpty(lastLone.NumberLone))
                {
                    // แยกส่วนของหมายเลข
                    string[] parts = lastLone.NumberLone.Split('-');
                    if (parts.Length == 2 && int.TryParse(parts[1], out int lastSequence))
                    {
                        newNumber = lastSequence + 1;
                    }
                }

                // สร้างหมายเลขใหม่
                string newLoanNumber = $"EM-{newNumber:D3}";
                txtNumberLone.Text = newLoanNumber;
            }
            catch (Exception ex)
            {
                MessageBox.Show(
                    $"เกิดข้อผิดพลาดในการสร้างรหัสการกู้เงิน: {ex.Message}",
                    "ข้อผิดพลาด",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error
                );
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            new Main().Show();
            this.Hide();
        }


        private void EmerLone_Load(object sender, EventArgs e)
        {

        }
        private void CalculateLoneMoney(object sender, EventArgs e)
        {
            try
            {
                decimal loneAmount = 0;
                decimal loneAmount1 = 0;

                // คำนวณจาก txtLoneMoney
                if (decimal.TryParse(txtLoneMoney.Text.Trim(), out decimal amount1))
                {
                    loneAmount = amount1;
                }

                // คำนวณจาก txtLoneMoney1
                if (decimal.TryParse(txtInterrate.Text.Trim(), out decimal amount2))
                {
                    loneAmount1 = amount2;
                }

                // คำนวณยอดรวมและดอกเบี้ย
                decimal totalLone = loneAmount + loneAmount1;
                decimal interest = totalLone * 0.01m; // ดอกเบี้ย 1%
                decimal totalWithInterest = totalLone + interest;

                // แสดงผลลัพธ์
                txtTotalMoneyLone.Text = totalWithInterest.ToString("N2");
                txtInterrate.Text = interest.ToString("N2");
            }
            catch (Exception ex)
            {
                MessageBox.Show($"เกิดข้อผิดพลาดในการคำนวณ: {ex.Message}",
                    "ข้อผิดพลาด", MessageBoxButtons.OK, MessageBoxIcon.Error);

                // กรณีเกิดข้อผิดพลาด ล้างค่าในช่อง
                txtTotalMoneyLone.Clear();
                txtInterrate.Clear();
            }
        }
        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                // ตรวจสอบข้อมูลก่อนบันทึก
                if (!ValidateInputs())
                {
                    return;
                }

                using (var transaction = _dbContext.Database.BeginTransaction())
                {
                    try
                    {
                        // แปลงค่าตัวเลขโดยลบ comma ออกก่อน
                        decimal moneyOld = decimal.Parse(txtMoneyOld.Text.Replace(",", ""));
                        decimal loneMoney = decimal.Parse(txtLoneMoney.Text.Replace(",", ""));
                        decimal totalMoneyLone = decimal.Parse(txtTotalMoneyLone.Text.Replace(",", ""));

                        var emer = new Emer
                        {
                            Username = txtusername.Text.Trim(),
                            Family = txtFamily.Text.Trim(),
                            Fullname = txtFullname.Text.Trim(),
                            Username1 = txtUsername1.Text.Trim(),
                            Fullname1 = txtFullname1.Text.Trim(),
                            Phone1 = txtPhone1.Text.Trim(),
                            Username2 = txtUsername2.Text.Trim(),
                            Fullname2 = txtFullname2.Text.Trim(),
                            Phone2 = txtPhone2.Text.Trim(),
                            Username3 = txtUsername3.Text.Trim(),
                            Fullname3 = txtFullname3.Text.Trim(),
                            Phone3 = txtPhone3.Text.Trim(),
                            MoneyOld = (double)moneyOld,
                            LoneMoney = (double)loneMoney,
                            TotalMoneyLone = (double)totalMoneyLone,
                            NumberLone = txtNumberLone.Text.Trim(),
                            Interrate = txtInterrate.Text.Trim(),
                            TimeLone = DateTime.Now
                        };

                        // สร้างตารางถ้ายังไม่มี
                        _dbContext.Database.EnsureCreated();

                        _dbContext.Emers.Add(emer);
                        _dbContext.SaveChanges();
                        transaction.Commit();

                        MessageBox.Show("บันทึกข้อมูลสำเร็จ", "สำเร็จ",
                            MessageBoxButtons.OK, MessageBoxIcon.Information);
                        ClearFields();
                        GenerateLoneNumber();
                    }
                    catch (Exception ex)
                    {
                        transaction.Rollback();
                        throw new Exception("ไม่สามารถบันทึกข้อมูลได้: " + ex.Message, ex);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"เกิดข้อผิดพลาด: {ex.Message}\n\nรายละเอียด: {ex.InnerException?.Message}",
                    "ข้อผิดพลาด", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private bool ValidateInputs()
        {
            // ตรวจสอบข้อมูลที่จำเป็น
            if (string.IsNullOrWhiteSpace(txtusername.Text) ||
                string.IsNullOrWhiteSpace(txtFamily.Text) ||
                string.IsNullOrWhiteSpace(txtFullname.Text))
            {
                MessageBox.Show("กรุณากรอกข้อมูลให้ครบถ้วน", "ข้อผิดพลาด",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }

            // ตรวจสอบรูปแบบตัวเลข
            if (!decimal.TryParse(txtMoneyOld.Text.Replace(",", ""), out _) ||
                !decimal.TryParse(txtLoneMoney.Text.Replace(",", ""), out _) ||
                !decimal.TryParse(txtTotalMoneyLone.Text.Replace(",", ""), out _))
            {
                MessageBox.Show("กรุณากรอกจำนวนเงินให้ถูกต้อง", "ข้อผิดพลาด",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }

            return true;
        }

        // เพิ่มเมธอดสำหรับจัดการรูปแบบการป้อนตัวเลข
        private void NumericTextBox_KeyPress(object sender, KeyPressEventArgs e)
        {
            // อนุญาตให้ป้อนตัวเลข, จุดทศนิยม, backspace และ comma เท่านั้น
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar) &&
                e.KeyChar != '.' && e.KeyChar != ',')
            {
                e.Handled = true;
            }

            // ป้องกันการป้อนจุดทศนิยมซ้ำ
            TextBox textBox = (TextBox)sender;
            if (e.KeyChar == '.' && textBox.Text.Contains('.'))
            {
                e.Handled = true;
            }

        }
        private void ClearFields()
        {
            txtusername.Clear();
            txtFamily.Clear();
            txtFullname.Clear();
            txtUsername1.Clear();
            txtFullname1.Clear();
            txtPhone1.Clear();
            txtUsername2.Clear();
            txtFullname2.Clear();
            txtPhone2.Clear();
            txtUsername3.Clear();
            txtFullname3.Clear();
            txtPhone3.Clear();
            txtMoneyOld.Clear();
            txtLoneMoney.Clear();
            txtTotalMoneyLone.Clear();
            txtNumberLone.Clear();
            txtInterrate.Clear();
        }

        private void print_Click(object sender, EventArgs e)
        {
            try
            {
                // Create directory on desktop
                string directoryPath = new PathConf().getPDFPath();

                if (!Directory.Exists(directoryPath))
                {
                    Directory.CreateDirectory(directoryPath);
                }

                // Create PDF filename
                string fileName = $"EmerLone_{DateTime.Now:yyyyMMddHHmmss}.pdf";
                string fullPath = Path.Combine(directoryPath, fileName);

                // Create document
                PdfDocument document = new PdfDocument();
                document.Info.Title = "EmerLone Register Information";
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

                gfx.DrawString("ข้อมูลเกี่ยวกับสมาชิก : การลาออกสมาชิก", subtitleFont, XBrushes.Black,
                              new XRect(0, 100, page.Width, 30), XStringFormats.Center);

                // Get form data
                string username = txtusername.Text;
                if (string.IsNullOrEmpty(username) && !string.IsNullOrEmpty(Username))
                    username = Username;

                string family = txtFamily.Text;
                if (string.IsNullOrEmpty(family) && !string.IsNullOrEmpty(Family))
                    family = Family;

                string fullname = txtFullname.Text;
                if (string.IsNullOrEmpty(fullname) && !string.IsNullOrEmpty(Fullname))
                    fullname = Fullname;
                
                string numlone = txtNumberLone.Text;
                if (string.IsNullOrEmpty(numlone) && !string.IsNullOrEmpty(NumberLone))
                    numlone = NumberLone;
                
                
                string user1 = txtUsername1.Text;
                if (string.IsNullOrEmpty(user1) && !string.IsNullOrEmpty(User1))
                    user1 = User1;                
                
                string user2 = txtUsername1.Text;
                if (string.IsNullOrEmpty(user2) && !string.IsNullOrEmpty(User2))
                    user1 = User2;                
                
                string user3 = txtUsername1.Text;
                if (string.IsNullOrEmpty(user3) && !string.IsNullOrEmpty(User3))
                    user1 = User3;
                
                string fullname1 = txtFullname1.Text;
                if (string.IsNullOrEmpty(fullname1) && !string.IsNullOrEmpty(FullName1))
                    fullname1 = FullName1;                
                
                string fullname2 = txtFullname2.Text;
                if (string.IsNullOrEmpty(fullname2) && !string.IsNullOrEmpty(FullName2))
                    fullname2 = FullName2;                
                
                string fullname3 = txtFullname3.Text;
                if (string.IsNullOrEmpty(fullname3) && !string.IsNullOrEmpty(FullName3))
                    fullname3 = FullName3;
                
                string moneyOld = txtMoneyOld.Text;
                if (string.IsNullOrEmpty(moneyOld) && !string.IsNullOrEmpty(MoneyOld))
                    moneyOld = MoneyOld;
                
                string loneMoney = txtLoneMoney.Text;
                if (string.IsNullOrEmpty(loneMoney) && !string.IsNullOrEmpty(LoneMoney))
                    loneMoney = LoneMoney;
                
                string interrate = txtInterrate.Text;
                if (string.IsNullOrEmpty(interrate) && !string.IsNullOrEmpty(Interrate))
                    interrate = Interrate;
                
                string total = txtTotalMoneyLone.Text;
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

                //ประกาศวันไทย
                CultureInfo thaiCulture = new CultureInfo("th-TH");

                // Draw table rows
                string[,] rowData = new string[,] {
            {"เลขที่สมาชิก", username},
            {"รหัสครอบครัว", family},
            {"ชื่อ - สกุล", fullname},
            {"เลขที่สมาชิก ผู้ค้ำประกันคนที่ 1", user1},
            {"ชื่อผู้ค้ำประกันคนที่ 1", fullname1},            
            {"เลขที่สมาชิก ผู้ค้ำประกันคนที่ 2", user2},
            {"ชื่อผู้ค้ำประกันคนที่ 2", fullname2},            
            {"เลขที่สมาชิก ผู้ค้ำประกันคนที่ 3", user3},
            {"ชื่อผู้ค้ำประกันคนที่ 3", fullname3},
            {"เลขที่สัญญา", numlone},
            {"จำนวนเงินฝาก", moneyOld},
            {"จำนวนเงินกู้", loneMoney},
            {"ดอกเบี้ย", interrate},
            {"จำนวนเงินที่ต้องชำระเงินคืนรวมดอกเบี้ย", total},
            //{"วันทำรายการ", dateTimePicker2.Value.ToString("ddddที่ d MMMM พ.ศ. yyyy", thaiCulture)},

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
                string dateStr = DateTime.Now.ToString("ddddที่ d MMMM พ.ศ. yyyy", thaiCulture);
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
    }

}
