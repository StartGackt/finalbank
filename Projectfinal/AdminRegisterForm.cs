using Projectfinal.Model;
using Microsoft.EntityFrameworkCore;
using iText.Kernel.Pdf;
using iText.Layout;
using iText.Layout.Element;
using iText.Layout.Properties;
using iText.IO.Font;
using iText.Kernel.Font;
using iText.IO.Font.Constants;
using iText.Kernel.Geom;
using iText.Kernel.Colors;
using iText.IO.Image;
using System.IO;
using IoPath = System.IO.Path;

namespace Projectfinal
{
    public partial class AdminRegisterForm : Form
    {
        public AdminRegisterForm()
        {
            InitializeComponent();
            this.WindowState = FormWindowState.Maximized;
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
                string directoryPath = @"C:\Users\Thest\source\repos\bankingcs\Filepdf";
                if (!Directory.Exists(directoryPath))
                {
                    try
                    {
                        Directory.CreateDirectory(directoryPath);
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show($"ไม่สามารถสร้างโฟลเดอร์ได้: {ex.Message}",
                                        "Error",
                                        MessageBoxButtons.OK,
                                        MessageBoxIcon.Error);
                        return;
                    }
                }

                // Create PDF filename
                string fileName = $"ADMIN_Register_{DateTime.Now:yyyyMMddHHmmss}.pdf";
                string fullPath = IoPath.Combine(directoryPath, fileName);

                string fontPath = @"C:\Users\Thest\source\repos\Projectfinal\Projectfinal\Fonts\Kanit-Bold.ttf";
                if (!File.Exists(fontPath))
                {
                    throw new FileNotFoundException($"Font file not found at: {fontPath}");
                }

                using (var writer = new PdfWriter(fullPath))
                using (var pdf = new PdfDocument(writer))
                using (var document = new iText.Layout.Document(pdf))
                {
                    PdfFont font = PdfFontFactory.CreateFont(fontPath, PdfEncodings.IDENTITY_H, PdfFontFactory.EmbeddingStrategy.FORCE_EMBEDDED);
                    document.SetFont(font);

                    // Add title
                    document.Add(new Paragraph("ระบบบริหารจัดการกลุ่มออมทรัพย์เพื่อการผลิตบ้านท่ารวก")
                        .SetFontSize(18)
                        .SetBold()
                        .SetTextAlignment(TextAlignment.CENTER));

                    document.Add(new Paragraph("ตำบลหนองยายโต๊ะ อำเภอชัยบาดาล จังหวัดลพบุรี")
                        .SetFontSize(16)
                        .SetTextAlignment(TextAlignment.CENTER));

                    document.Add(new Paragraph("ข้อมูลเกี่ยวกับเจ้าหน้าที่กองทุน : การปันผลครอบครัว")
                        .SetFontSize(16)
                        .SetTextAlignment(TextAlignment.CENTER));

                    // Create and add table to the document
                    Table table = new Table(6); // Set number of columns
                    table.SetWidth(UnitValue.CreatePercentValue(100)); // Set table width to 100% of the page

                    // Add table headers
                    table.AddCell(new Cell().Add(new Paragraph("ชื่อผู้ใช้งาน")));
                    table.AddCell(new Cell().Add(new Paragraph("รหัสผ่าน")));
                    table.AddCell(new Cell().Add(new Paragraph("รหัสบัตรประชาชน")));
                    table.AddCell(new Cell().Add(new Paragraph("เบอร์โทรศัพท์")));
                    table.AddCell(new Cell().Add(new Paragraph("ชื่อ - สกุล")));
                    table.AddCell(new Cell().Add(new Paragraph("ที่อยู่")));

                    // Add data rows
                    table.AddCell(new Cell().Add(new Paragraph(Username)));
                    table.AddCell(new Cell().Add(new Paragraph(Password)));
                    table.AddCell(new Cell().Add(new Paragraph(Idcard)));
                    table.AddCell(new Cell().Add(new Paragraph(Phone)));
                    table.AddCell(new Cell().Add(new Paragraph(Fullname)));
                    table.AddCell(new Cell().Add(new Paragraph(Address)));

                    document.Add(table);
                }

                if (File.Exists(fullPath))
                {
                    MessageBox.Show($"PDF created successfully!\nSaved at: {fullPath}", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else
                {
                    throw new IOException("PDF file was not created after the operation");
                }
            }
            catch (UnauthorizedAccessException uaEx)
            {
                MessageBox.Show($"No permission to write file: {uaEx.Message}", "Access Rights Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            catch (FileNotFoundException fnfEx)
            {
                MessageBox.Show($"File not found: {fnfEx.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            catch (iText.Kernel.Exceptions.PdfException pdfEx)
            {
                MessageBox.Show($"PDF creation failed: {pdfEx.Message}\n\nDetails", "PDF Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"An unexpected error occurred: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}
