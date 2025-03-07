using System;
using System.Linq;
using System.Windows.Forms;
using Projectfinal.Model;
using PdfSharp.Pdf;
using PdfSharp.Drawing;
using PdfSharp.Fonts;
using System.Diagnostics;

namespace Projectfinal
{
    public partial class OraLone : Form
    {
        private readonly dbcontext _dbContext = new dbcontext();

        public decimal MoneyOld { get; private set; }
        public decimal LoneMoney { get; private set; }
        public int LoneMoney1 { get; private set; }
        public decimal TotalMoneyLone { get; private set; }

        public OraLone()
        {
            InitializeComponent();
            this.WindowState = FormWindowState.Maximized;
            txtusername.TextChanged += TxtUsername_TextChanged;
            txtUsername1.TextChanged += TxtUsername1_TextChanged;
            txtUsername2.TextChanged += TxtUsername2_TextChanged;
            txtUsername3.TextChanged += TxtUsername3_TextChanged;
            txtLoneMoney.TextChanged += (sender, e) => CalculateTotalMoney();
            txtLoneMoney1.TextChanged += (sender, e) => CalculateTotalMoney();
            GenerateLoneNumber();
        }

        private void OraLone_Load(object sender, EventArgs e)
        {
        }

        private void button3_Click(object sender, EventArgs e)
        {
            new Main().Show();
            this.Hide();
        }

        private void TxtUsername_TextChanged(object sender, EventArgs e)
        {
            // Call the overload that takes six parameters
            SearchUserAndFillFields(txtusername, txtFamily, txtFullname, txtMoneyOld);

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

        // Overload with three parameters for basic fields
        private void SearchUserAndFillFields(TextBox usernameTextBox, TextBox fullNameTextBox, TextBox phoneTextBox)
        {
            try // เริ่มต้นบล็อก try เพื่อตรวจจับข้อผิดพลาด
            {
                string username = usernameTextBox.Text; // รับค่าชื่อผู้ใช้จาก TextBox
                if (string.IsNullOrEmpty(username)) // ตรวจสอบว่าชื่อผู้ใช้ว่างหรือไม่
                {
                    ClearFields(fullNameTextBox, phoneTextBox); // ล้างฟิลด์ถ้าชื่อผู้ใช้ว่าง
                    return; // ออกจากฟังก์ชัน
                }

                var user = _dbContext.Users.FirstOrDefault(u => u.Username == username); // ค้นหาผู้ใช้ในฐานข้อมูล
                if (user != null) // ถ้าพบผู้ใช้
                {
                    fullNameTextBox.Text = user.Fullname; // กรอกชื่อเต็มในฟิลด์
                    phoneTextBox.Text = user.Phone; // กรอกหมายเลขโทรศัพท์ในฟิลด์
                }
                else // ถ้าไม่พบผู้ใช้
                {
                    ClearFields(fullNameTextBox, phoneTextBox); // ล้างฟิลด์
                }
            }
            catch (Exception ex) // จับข้อผิดพลาด
            {
                MessageBox.Show($"เกิดข้อผิดพลาด: {ex.Message}\n{ex.StackTrace}", "ข้อผิดพลาด", MessageBoxButtons.OK, MessageBoxIcon.Error); // แสดงข้อความข้อผิดพลาด
            }
        }

        // Overload with six parameters for additional fields
        private void SearchUserAndFillFields(TextBox usernameTextBox, TextBox familyTextBox, TextBox fullNameTextBox, TextBox moneyOldTextBox)
        {
            try
            {
                string username = usernameTextBox.Text;
                if (string.IsNullOrEmpty(username))
                {
                    ClearFields(familyTextBox, fullNameTextBox, moneyOldTextBox);
                    return;
                }

                var user = _dbContext.Users.FirstOrDefault(u => u.Username == username);
                if (user != null)
                {
                    familyTextBox.Text = user.Family;
                    fullNameTextBox.Text = user.Fullname;

                    // 1. ดึงข้อมูลจาก MoneyTranss ก่อน
                    var transaction = _dbContext.MoneyTranss
                        .Where(t => t.Username == username)
                        .OrderByDescending(t => t.TimeMoney)
                        .FirstOrDefault();

                    decimal moneyOldValue = 0;
                    if (transaction != null)
                    {
                        moneyOldValue = transaction.MoneyTotal;
                    }

                    // 2. ดึงข้อมูลจาก OrdLones
                    var loan = _dbContext.OrdLones
                        .Where(o => o.Username == username)
                        .OrderByDescending(o => o.TimeLone)  // เรียงตามเวลาล่าสุด
                        .FirstOrDefault();

                    // 3. ถ้ามีข้อมูลการกู้ก่อนหน้า ให้ใช้ค่า MoneyOld จาก OrdLones
                    if (loan != null)
                    {
                        moneyOldValue = loan.MoneyOld;

                        // กรอกข้อมูลอื่นๆ จาก OrdLones
                        txtusername.Text = loan.Username;
                        txtFamily.Text = loan.Family;
                        txtFullname.Text = loan.Fullname;
                        txtUsername1.Text = loan.Username1;
                        txtFullname1.Text = loan.Fullname1;
                        txtPhone1.Text = loan.Phone1;
                        txtUsername2.Text = loan.Username2;
                        txtFullname2.Text = loan.Fullname2;
                        txtPhone2.Text = loan.Phone2;
                        txtUsername3.Text = loan.Username3;
                        txtFullname3.Text = loan.Fullname3;
                        txtPhone3.Text = loan.Phone3;
                        txtNumberLone.Text = loan.NumberLone;
                        txtInterrate.Text = loan.Interrate;
                        txtLoneMoney.Text = loan.LoneMoney.ToString("N2");
                        txtLoneMoney1.Text = loan.LoneMoney1.ToString("N2");
                        txtTotalMoneyLone.Text = loan.TotalMoneyLone.ToString("N2");
                    }

                    // 4. แสดงค่า MoneyOld
                    moneyOldTextBox.Text = moneyOldValue.ToString("N2");
                }
                else
                {
                    ClearFields(familyTextBox, fullNameTextBox, moneyOldTextBox);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"เกิดข้อผิดพลาด: {ex.Message}\n{ex.StackTrace}", "ข้อผิดพลาด", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        // เพิ่มเมธอดสำหรับล้างข้อมูลทั้งหมด
        private void ClearAllFields()
        {
            txtLoneMoney.Clear();
            txtLoneMoney1.Clear();
            txtTotalMoneyLone.Clear();
            txtNumberLone.Clear();
            if (txtInterrate != null) txtInterrate.Clear();

            // ล้างข้อมูลผู้ค้ำประกัน
            txtUsername1.Clear();
            txtFullname1.Clear();
            txtPhone1.Clear();
            txtUsername2.Clear();
            txtFullname2.Clear();
            txtPhone2.Clear();
            txtUsername3.Clear();
            txtFullname3.Clear();
            txtPhone3.Clear();
        }

        // Clear fields helper function
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
                var lastLoan = _dbContext.OrdLones
                    .OrderByDescending(l => l.Id)
                    .FirstOrDefault();

                int newNumber = 1;

                if (lastLoan != null && lastLoan.NumberLone != null)
                {
                    var parts = lastLoan.NumberLone.ToString().Split('-');
                    if (parts.Length == 2 && int.TryParse(parts[1], out int lastSequence))
                    {
                        newNumber = lastSequence + 1;
                    }
                }

                string newLoanNumber = $"ORD-{newNumber:D3}";
                txtNumberLone.Text = newLoanNumber;
            }
            catch (Exception ex)
            {
                MessageBox.Show($"เกิดข้อผิดพลาดในการสร้างรหัสการกู้เงิน: {ex.Message}", "ข้อผิดพลาด", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void CalculateTotalMoney()
        {
            try
            {
                if (string.IsNullOrEmpty(txtLoneMoney.Text) || string.IsNullOrEmpty(txtLoneMoney1.Text))
                {
                    txtTotalMoneyLone.Text = "0.00";
                    return;
                }

                // แปลงค่าจาก TextBox เป็นตัวเลข
                if (!decimal.TryParse(txtLoneMoney.Text.Trim(), out decimal loanAmount))
                {
                    MessageBox.Show("กรุณากรอกจำนวนเงินกู้ให้ถูกต้อง");
                    return;
                }

                if (!int.TryParse(txtLoneMoney1.Text.Trim(), out int months) || months <= 0)
                {

                    return;
                }

                // คำนวณดอกเบี้ย (8%)
                decimal interestRate = 0.08m;
                decimal interest = loanAmount * interestRate;

                // แสดงค่าดอกเบี้ย
                txtInterrate.Text = interest.ToString("N2");

                // คำนวณยอดรวมทั้งหมด
                decimal totalLoan = loanAmount + interest;

                // คำนวณค่างวดต่อเดือน
                decimal monthlyPayment = totalLoan / months;

                // แสดงผลลัพธ์
                txtTotalMoneyLone.Text = monthlyPayment.ToString("N2");

                // เก็บค่าไว้ในตัวแปร property
                TotalMoneyLone = monthlyPayment;
            }
            catch (Exception ex)
            {
                MessageBox.Show($"เกิดข้อผิดพลาดในการคำนวณ: {ex.Message}");
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                // ตรวจสอบข้อมูลที่จำเป็น
                if (string.IsNullOrEmpty(txtusername.Text) ||
                    string.IsNullOrEmpty(txtLoneMoney.Text) ||
                    string.IsNullOrEmpty(txtLoneMoney1.Text))
                {
                    MessageBox.Show("กรุณากรอกข้อมูลให้ครบถ้วน");
                    return;
                }

                // แปลงค่าตัวเลขและตรวจสอบความถูกต้อง
                if (!decimal.TryParse(txtLoneMoney.Text, out decimal loneMoney))
                {
                    MessageBox.Show("จำนวนเงินกู้ไม่ถูกต้อง");
                    return;
                }

                if (!int.TryParse(txtLoneMoney1.Text, out int loanPeriod))
                {
                    MessageBox.Show("จำนวนงวดไม่ถูกต้อง");
                    return;
                }

                if (!decimal.TryParse(txtTotalMoneyLone.Text, out decimal totalMoneyLone))
                {
                    MessageBox.Show("ยอดชำระต่องวดไม่ถูกต้อง");
                    return;
                }

                // ตรวจสอบว่ามีข้อมูลเดิมหรือไม่
                var existingLoan = _dbContext.OrdLones
                    .FirstOrDefault(o => o.Username == txtusername.Text && o.NumberLone == txtNumberLone.Text);

                if (existingLoan != null)
                {
                    MessageBox.Show("มีข้อมูลการกู้นี้อยู่แล้วในระบบ");
                    return;
                }

                // สร้างข้อมูลการกู้
                var newLoan = new OrdLone
                {
                    Username = txtusername.Text?.Trim() ?? "",
                    Family = txtFamily.Text?.Trim() ?? "",
                    Fullname = txtFullname.Text?.Trim() ?? "",
                    Phone = "", // เพิ่มค่าว่างสำหรับฟิลด์ Phone ที่จำเป็น
                    Username1 = txtUsername1.Text?.Trim() ?? "",
                    Fullname1 = txtFullname1.Text?.Trim() ?? "",
                    Phone1 = txtPhone1.Text?.Trim() ?? "",
                    Username2 = txtUsername2.Text?.Trim() ?? "",
                    Fullname2 = txtFullname2.Text?.Trim() ?? "",
                    Phone2 = txtPhone2.Text?.Trim() ?? "",
                    Username3 = txtUsername3.Text?.Trim() ?? "",
                    Fullname3 = txtFullname3.Text?.Trim() ?? "",
                    Phone3 = txtPhone3.Text?.Trim() ?? "",
                    MoneyOld = decimal.TryParse(txtMoneyOld.Text, out decimal moneyOld) ? moneyOld : 0m,
                    LoneMoney = loneMoney,
                    LoneMoney1 = loanPeriod,
                    Interrate = txtInterrate.Text,
                    TotalMoneyLone = totalMoneyLone,
                    NumberLone = txtNumberLone.Text?.Trim() ?? "",
                    TimeLone = DateTime.Now
                };

                // บันทึกข้อมูล
                using (var transaction = _dbContext.Database.BeginTransaction())
                {
                    try
                    {
                        _dbContext.OrdLones.Add(newLoan);
                        _dbContext.SaveChanges();
                        transaction.Commit();

                        MessageBox.Show("บันทึกข้อมูลเรียบร้อยแล้ว");

                        // ล้างฟอร์มและสร้างเลขที่กู้ใหม่
                        ClearAllFields();
                        GenerateLoneNumber();
                    }
                    catch (Exception ex)
                    {
                        transaction.Rollback();
                        throw; // โยน exception ไปให้ catch ด้านนอกจัดการ
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"เกิดข้อผิดพลาด: {ex.Message}\nรายละเอียด: {ex.InnerException?.Message ?? "ไม่มีรายละเอียดเพิ่มเติม"}");
            }
        }

        private void print_pdf_Click(object sender, EventArgs e)
        {
            try
            {
                GlobalFontSettings.FontResolver = new CustomFontResolver();

                string directoryPath = @"C:\Users\krisa\source\repos\finalbank\Projectfinal\Filepdf";

                if (!Directory.Exists(directoryPath))
                    Directory.CreateDirectory(directoryPath);

                string fileName = $"LoanContract_{txtNumberLone.Text}_{DateTime.Now:yyyyMMddHHmmss}.pdf";
                string fullPath = Path.Combine(directoryPath, fileName);

                PdfDocument document = new PdfDocument();
                document.Info.Title = "สัญญาเงินกู้";
                PdfPage page = document.AddPage();
                page.Size = PdfSharp.PageSize.A4;

                XGraphics gfx = XGraphics.FromPdfPage(page);
                XFont titleFont = new XFont("Kanit-Bold", 16);
                XFont headerFont = new XFont("Kanit-Bold", 12);
                XFont contentFont = new XFont("Kanit-Bold", 11);

                int yPos = 40;
                int margin = 60;
                int rowHeight = 25;

                gfx.DrawString("ระบบบริหารจัดการกลุ่มออมทรัพย์เพื่อการผลิตบ้านท่ารวก", titleFont, XBrushes.Black, new XRect(0, yPos, page.Width, 25), XStringFormats.Center);
                yPos += 25;

                gfx.DrawString("ตำบลหนองยายโต๊ะ อำเภอชัยบาดาล จังหวัดลพบุรี", contentFont, XBrushes.Black, new XRect(0, yPos, page.Width, 25), XStringFormats.Center);
                yPos += 25;

                gfx.DrawString("ข้อมูลเกี่ยวกับสมาชิก : การกู้เงินสามัญ", headerFont, XBrushes.Black, new XRect(0, yPos, page.Width, 20), XStringFormats.Center);
                yPos += 40;

                // ข้อมูลสมาชิก
                gfx.DrawString($"เลขที่สมาชิก: {txtusername.Text}", contentFont, XBrushes.Black, margin, yPos);
                gfx.DrawString($"รหัสครอบครัว: {txtFamily.Text}", contentFont, XBrushes.Black, margin + 300, yPos);
                yPos += 25;

                gfx.DrawString($"ชื่อ - นามสกุล: {txtFullname.Text}", contentFont, XBrushes.Black, margin, yPos);
                yPos += 30;

                // ข้อมูลผู้ค้ำประกัน
                for (int i = 1; i <= 3; i++)
                {
                    var username = Controls[$"txtUsername{i}"].Text;
                    var fullname = Controls[$"txtFullname{i}"].Text;
                    var phone = Controls[$"txtPhone{i}"]?.Text;

                    if (!string.IsNullOrEmpty(username))
                    {
                        gfx.DrawString($"เลขที่สมาชิก ผู้ค้ำประกันคนที่ {i}: {username}", contentFont, XBrushes.Black, margin, yPos);
                        gfx.DrawString($"ชื่อ-นามสกุล: {fullname}", contentFont, XBrushes.Black, margin + 220, yPos);
                        gfx.DrawString($"เบอร์โทรศัพท์: {phone}", contentFont, XBrushes.Black, margin + 380, yPos);
                        yPos += 30;
                    }
                }

                yPos += 20;

                // ข้อมูลการกู้เงิน
                gfx.DrawString($"เลขที่สัญญา: {txtNumberLone.Text}", contentFont, XBrushes.Black, margin, yPos);
                gfx.DrawString($"จำนวนเงินฝาก: {txtMoneyOld.Text} บาท", contentFont, XBrushes.Black, 300, yPos);
                yPos += 25;

                gfx.DrawString($"จำนวนเงินกู้: {txtLoneMoney.Text} บาท", contentFont, XBrushes.Black, margin, yPos);
                gfx.DrawString($"จำนวนงวดที่ส่ง: {txtLoneMoney1.Text} งวด", contentFont, XBrushes.Black, 300, yPos);
                yPos += 30;

                gfx.DrawString($"ดอกเบี้ย: {txtInterrate.Text} %", contentFont, XBrushes.Black, margin, yPos);
                gfx.DrawString($"ส่งงวดละ: {txtTotalMoneyLone.Text} บาท", contentFont, XBrushes.Black, 300, yPos);
                yPos += 50;

                gfx.DrawString($"วันที่ {DateTime.Now:dd MMMM yyyy}", contentFont, XBrushes.Black, new XRect(0, yPos, page.Width - margin, 25), XStringFormats.BottomRight);

                yPos += 40;
                gfx.DrawString("(ลายเซ็นเจ้าหน้าที่)", contentFont, XBrushes.Black, new XRect(0, yPos, page.Width, 25), XStringFormats.Center);

                document.Save(fullPath);

                Process.Start(new ProcessStartInfo { FileName = fullPath, UseShellExecute = true });

                MessageBox.Show($"สร้างไฟล์ PDF เรียบร้อยแล้ว\nบันทึกไฟล์ที่: {fullPath}", "สำเร็จ", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"เกิดข้อผิดพลาดในการสร้างไฟล์ PDF: {ex.Message}\n{ex.StackTrace}", "ข้อผิดพลาด", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

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
