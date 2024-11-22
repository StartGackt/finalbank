using System;
using System.Linq;
using System.Windows.Forms;
using Projectfinal.Model;

namespace Projectfinal
{
    public partial class DividendPeople : Form
    {
        private readonly dbcontext _dbContext = new dbcontext();

        public DividendPeople()
        {
            InitializeComponent();
            this.WindowState = FormWindowState.Maximized;
            txtusername.TextChanged += txtusername_textChanged;
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
    }
}
