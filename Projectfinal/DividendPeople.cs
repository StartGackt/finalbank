using System;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using iText.Layout.Element;
using PdfSharp.Drawing;
using PdfSharp.Fonts;
using PdfSharp.Pdf;
using Projectfinal.Model;
using static System.Runtime.InteropServices.JavaScript.JSType;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.StartPanel;

namespace Projectfinal
{
    public partial class DividendPeople : Form
    {
        private readonly dbcontext _dbContext = new dbcontext();

        public string? Username { get; private set; }
        public string? Fullname { get; private set; }
        public string? MoneyOld { get; private set; }
        public string? Divi { get; private set; }
        public string? Family { get; private set; }

        public DividendPeople()
        {
            InitializeComponent();
            this.WindowState = FormWindowState.Maximized;
            txtusername.TextChanged += txtusername_textChanged;

            // Required for PdfSharp to support UTF-8 encoding (for Thai characters)
            Encoding.RegisterProvider(CodePagesEncodingProvider.Instance);

 
        }

        private void txtusername_textChanged(object sender, EventArgs e)
        {
            try
            {
                string username = txtusername.Text;
                if (string.IsNullOrEmpty(username))
                {
                    ClearFields();
                    return;
                }

                // Fetch the user and the latest transaction data
                var user = _dbContext.Users.FirstOrDefault(u => u.Username == username);
                if (user != null)
                {
                    txtFamily.Text = user.Family;
                    txtFullname.Text = user.Fullname;

                    // Get the latest MoneyTotal value from MoneyTranss
                    var latestTransaction = _dbContext.MoneyTranss
                        .Where(t => t.Username == username)
                        .OrderByDescending(t => t.TimeMoney)
                        .FirstOrDefault();

                    if (latestTransaction != null)
                    {
                        txtMoneyOld.Text = latestTransaction.MoneyTotal.ToString("N2");

                        // Calculate dividend based on MoneyTotal (example: 4% dividend)
                        decimal moneyTotal = latestTransaction.MoneyTotal;
                        decimal dividend = moneyTotal * 0.04m; // 4% dividend
                        txtDiv.Text = dividend.ToString("N2");
                    }
                    else
                    {
                        txtMoneyOld.Text = "0.00";
                        txtDiv.Clear();
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
            txtFullname.Clear();
            txtMoneyOld.Text = "0.00";
            txtDiv.Clear();
        }

        private void DividendPeople_Load(object sender, EventArgs e)
        {
        }

        private void button6_Click(object sender, EventArgs e)
        {
            MainReport mainReport = new MainReport();
            mainReport.Show();
            this.Hide();


        }

        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                // Ensure a username is entered
                string username = txtusername.Text;
                if (string.IsNullOrEmpty(username))
                {
                    MessageBox.Show("กรุณากรอกชื่อผู้ใช้", "ข้อผิดพลาด", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                // Retrieve or create a DivPeople record
                var divPeopleRecord = _dbContext.DivPeoples.FirstOrDefault(d => d.Username == username);
                if (divPeopleRecord == null)
                {
                    divPeopleRecord = new DivPeople { Username = username };
                    _dbContext.DivPeoples.Add(divPeopleRecord);
                }

                // Update fields with current values from the form
                divPeopleRecord.Family = txtFamily.Text;
                divPeopleRecord.Fullname = txtFullname.Text;
                divPeopleRecord.MoneyOld = decimal.Parse(txtMoneyOld.Text);
                divPeopleRecord.Dividend = decimal.Parse(txtDiv.Text);

                // Save changes to the database
                _dbContext.SaveChanges();
                MessageBox.Show("บันทึกข้อมูลสำเร็จ", "ข้อมูล", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                ClearFields();
                MessageBox.Show($"เกิดข้อผิดพลาด: {ex.Message}", "ข้อผิดพลาด", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        //print
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

                gfx.DrawString("ข้อมูลเกี่ยวกับเจ้าหน้าที่กองทุน : การปันผลสมาชิกรายบุคคล", subtitleFont, XBrushes.Black,
                              new XRect(0, 100, page.Width, 30), XStringFormats.Center);

                // Get form data
                string username = txtusername.Text;
                if (string.IsNullOrEmpty(username) && !string.IsNullOrEmpty(Username))
                    username = Username;

                string fullname = txtFullname.Text;
                if (string.IsNullOrEmpty(fullname) && !string.IsNullOrEmpty(Fullname))
                    fullname = Fullname;

                string Money_old = txtMoneyOld.Text;
                if (string.IsNullOrEmpty(Money_old) && !string.IsNullOrEmpty(MoneyOld))
                    Money_old = MoneyOld;

                string div = txtDiv.Text;
                if (string.IsNullOrEmpty(div) && !string.IsNullOrEmpty(Divi))
                    div = Divi;

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
            {"ชื่อ - นามสกุล", fullname},
            {"รหัสครอบครัว", family},
            {"เงินที่ฝากในรอบ 1 ปี ", Money_old},
            {"จำนวนเงินปันผล", div},
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
