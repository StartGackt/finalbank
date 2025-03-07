using Projectfinal.Model;
using Microsoft.EntityFrameworkCore;
using System.IO;
using IoPath = System.IO.Path;
using PdfSharp.Pdf;
using PdfSharp.Drawing;
using System.Text;
using PdfSharp.Fonts;

namespace Projectfinal
{
    public partial class AdminRegisterForm : Form
    {
        public AdminRegisterForm()
        {
            InitializeComponent();
            this.WindowState = FormWindowState.Maximized;

            // Required for PdfSharp to support UTF-8 encoding (for Thai characters)
            Encoding.RegisterProvider(CodePagesEncodingProvider.Instance);
            
            // Register custom font resolver for Thai font support
            GlobalFontSettings.FontResolver = new CustomFontResolver();
        }

        dbcontext dbcontext = new dbcontext();

        public int Id { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
        public string Idcard { get; set; }
        public string Phone { get; set; }
        public string Fullname { get; set; }
        public string Address { get; set; }
        public string Time { get; set; }
        public string Position { get; set; }

        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                // Validate input fields
                if (string.IsNullOrEmpty(txtusername.Text))
                {
                    MessageBox.Show("กรุณากรอกชื่อผู้ใช้งาน", "ผิดพลาด", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    txtusername.Focus();
                    return;
                }

                if (string.IsNullOrEmpty(txtPassword.Text))
                {
                    MessageBox.Show("กรุณากรอกรหัสผ่าน", "ผิดพลาด", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    txtPassword.Focus();
                    return;
                }

                if (string.IsNullOrEmpty(txtIdcard.Text))
                {
                    MessageBox.Show("กรุณากรอกบัตรประชาชน", "ผิดพลาด", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    txtIdcard.Focus();
                    return;
                }

                if (string.IsNullOrEmpty(txtPhone.Text))
                {
                    MessageBox.Show("กรุณากรอกเบอร์โทรศัพท์", "ผิดพลาด", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    txtPhone.Focus();
                    return;
                }

                if (string.IsNullOrEmpty(txtFullname.Text))
                {
                    MessageBox.Show("กรุณากรอกชื่อ-นามสกุล", "ผิดพลาด", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    txtFullname.Focus();
                    return;
                }

                if (string.IsNullOrEmpty(txtAddress.Text))
                {
                    MessageBox.Show("กรุณากรอกที่อยู่", "ผิดพลาด", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    txtAddress.Focus();
                    return;
                }

                if (string.IsNullOrEmpty(comboBox1.Text))
                {
                    MessageBox.Show("กรุณากรอกตำแหน่ง", "ผิดพลาด", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    comboBox1.Focus();
                    return;
                }

                // Create a new AdminRegister object
                var adminRegister = new AdminRegisterModel
                {
                    Username = txtusername.Text,
                    Password = txtPassword.Text,
                    Idcard = txtIdcard.Text,
                    Phone = txtPhone.Text,
                    Fullname = txtFullname.Text,
                    Address = txtAddress.Text,
                    Time = DateTime.Now.ToString("hh:mm:ss tt"),
                    Position = comboBox1.Text
                };

                // Add data and save
                dbcontext.AdminRegisters.Add(adminRegister);
                dbcontext.SaveChanges();

                MessageBox.Show("เพิ่มข้อมูลสำเร็จ", "สำเร็จ", MessageBoxButtons.OK, MessageBoxIcon.Information);

                // Store the form values in class properties for PDF generation
                Username = txtusername.Text;
                Password = txtPassword.Text;
                Idcard = txtIdcard.Text;
                Phone = txtPhone.Text;
                Fullname = txtFullname.Text;
                Address = txtAddress.Text;
                Position = comboBox1.Text;
                Time = DateTime.Now.ToString("hh:mm:ss tt");

                // Print PDF immediately after adding data
                print_Click(sender, e);
            }
            catch (Exception dbEx)
            {
                var innerException = dbEx.InnerException?.InnerException;
                MessageBox.Show($"ข้อผิดพลาดในการอัปเดตฐานข้อมูล: {innerException?.Message ?? dbEx.Message}", "ข้อผิดพลาด", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            var mainForm = new Main();
            mainForm.Show();
            this.Hide();
        }

        private void AdminRegister_Load(object sender, EventArgs e)
        {
        }

        private void print_Click(object sender, EventArgs e)
        {
            try
            {
                // Create directory on desktop
                string directoryPath = @"C:\Users\krisa\source\repos\finalbank\Projectfinal\Filepdf";

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

                gfx.DrawString("ข้อมูลเกี่ยวกับเจ้าหน้าที่กองทุน : การปันผลครอบครัว", subtitleFont, XBrushes.Black,
                              new XRect(0, 100, page.Width, 30), XStringFormats.Center);

                // Get form data
                string username = txtusername.Text;
                if (string.IsNullOrEmpty(username) && !string.IsNullOrEmpty(Username))
                    username = Username;

                string password = txtPassword.Text;
                if (string.IsNullOrEmpty(password) && !string.IsNullOrEmpty(Password))
                    password = Password;

                string idCard = txtIdcard.Text;
                if (string.IsNullOrEmpty(idCard) && !string.IsNullOrEmpty(Idcard))
                    idCard = Idcard;

                string phone = txtPhone.Text;
                if (string.IsNullOrEmpty(phone) && !string.IsNullOrEmpty(Phone))
                    phone = Phone;

                string fullname = txtFullname.Text;
                if (string.IsNullOrEmpty(fullname) && !string.IsNullOrEmpty(Fullname))
                    fullname = Fullname;

                string address = txtAddress.Text;
                if (string.IsNullOrEmpty(address) && !string.IsNullOrEmpty(Address))
                    address = Address;

                string position = comboBox1.Text;
                if (string.IsNullOrEmpty(position) && !string.IsNullOrEmpty(Position))
                    position = Position;

                // Draw table header
                double yPosition = 150;
                double leftMargin = 50;

                // Draw content rows
                DrawRow(gfx, "ชื่อผู้ใช้งาน:", username, headerFont, contentFont, leftMargin, yPosition);
                yPosition += 25;

                DrawRow(gfx, "รหัสผ่าน:", password, headerFont, contentFont, leftMargin, yPosition);
                yPosition += 25;

                DrawRow(gfx, "รหัสบัตรประชาชน:", idCard, headerFont, contentFont, leftMargin, yPosition);
                yPosition += 25;

                DrawRow(gfx, "เบอร์โทรศัพท์:", phone, headerFont, contentFont, leftMargin, yPosition);
                yPosition += 25;

                DrawRow(gfx, "ชื่อ - สกุล:", fullname, headerFont, contentFont, leftMargin, yPosition);
                yPosition += 25;

                DrawRow(gfx, "ที่อยู่:", address, headerFont, contentFont, leftMargin, yPosition);
                yPosition += 25;

                DrawRow(gfx, "ตำแหน่ง:", position, headerFont, contentFont, leftMargin, yPosition);
                yPosition += 25;

                DrawRow(gfx, "เวลาที่บันทึก:", DateTime.Now.ToString("dd/MM/yyyy hh:mm:ss tt"),
                       headerFont, contentFont, leftMargin, yPosition);

                // Draw a border around the content
                double boxWidth = 500;
                double boxHeight = yPosition + 25 - 140;
                gfx.DrawRectangle(new XPen(XColors.Black, 1), leftMargin - 10, 140, boxWidth, boxHeight);

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

        // Helper method to draw a row with label and value
        private void DrawRow(XGraphics gfx, string label, string value, XFont labelFont, XFont valueFont,
                            double x, double y)
        {
            gfx.DrawString(label, labelFont, XBrushes.Black, new XPoint(x, y));
            gfx.DrawString(value, valueFont, XBrushes.Black, new XPoint(x + 150, y));
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