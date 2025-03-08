using PdfSharp.Drawing;
using PdfSharp.Pdf;
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
using static System.Runtime.InteropServices.JavaScript.JSType;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.StartPanel;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;
using PdfSharp.Fonts;

namespace Projectfinal
{
    public partial class MemberRegignation : Form
    {
        public string? Username { get; private set; }
        public string? Idcard { get; private set; }
        public string? Phone { get; private set; }
        public string? Fullname { get; private set; }
        public string? Exit { get; private set; }
        public string? Family { get; private set; }

        public MemberRegignation()
        {
            InitializeComponent();
            this.WindowState = FormWindowState.Maximized;

            // Required for PdfSharp to support UTF-8 encoding (for Thai characters)
            Encoding.RegisterProvider(CodePagesEncodingProvider.Instance);
        }

        private void button3_Click(object sender, EventArgs e)
        {
            new Main().Show();
            this.Hide();
        }

        private void MemberRegignation_Load(object sender, EventArgs e)
        {

        }

        //ปริ้นข้อมูล 
        private void button1_Click(object sender, EventArgs e)
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
                string fileName = $"Member_Register_{DateTime.Now:yyyyMMddHHmmss}.pdf";
                string fullPath = Path.Combine(directoryPath, fileName);

                // Create document
                PdfDocument document = new PdfDocument();
                document.Info.Title = "Member Register Information";
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

                string idCard = txtIdcard.Text;
                if (string.IsNullOrEmpty(idCard) && !string.IsNullOrEmpty(Idcard))
                    idCard = Idcard;

                string phone = txtPhone.Text;
                if (string.IsNullOrEmpty(phone) && !string.IsNullOrEmpty(Phone))
                    phone = Phone;

                string fullname = txtFullname.Text;
                if (string.IsNullOrEmpty(fullname) && !string.IsNullOrEmpty(Fullname))
                    fullname = Fullname;

                string address = txtAdress.Text;
                if (string.IsNullOrEmpty(address) && !string.IsNullOrEmpty(Exit))
                    address = Exit;

                string exit = txtExit.Text;
                if (string.IsNullOrEmpty(exit) && !string.IsNullOrEmpty(Exit))
                    exit = Exit;

                string family = txtFamily.Text;
                if (string.IsNullOrEmpty(family) && !string.IsNullOrEmpty(Family))
                    family = Family;

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
            {"รหัสบัตรประชาชน", idCard},
            {"รหัสครอบครัว", family},
            {"เบอร์โทรศัพท์", phone},
            {"ชื่อ - สกุล", fullname},
            {"ที่อยู่", address},
            {"สาเหตุลาออก", exit},
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
    }
}
