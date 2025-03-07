using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Projectfinal.Model;


namespace Projectfinal
{
    public partial class EmerLone : Form
    {
        private readonly dbcontext _dbContext = new dbcontext();
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
    }

}
