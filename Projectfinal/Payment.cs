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
    public partial class Payment : Form
    {
        private readonly dbcontext _dbContext = new dbcontext();

        public Payment()
        {
            InitializeComponent();
            this.WindowState = FormWindowState.Maximized;
            txtusername.TextChanged += TxtUsername_TextChanged;
            txtNuneycetegory.TextChanged += txtNuneycetegory_TextChanged;
            txtuserpay.TextChanged += TxtUserPay_TextChanged; // Add this line
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
    }
}