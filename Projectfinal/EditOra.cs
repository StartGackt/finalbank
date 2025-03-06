using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Projectfinal.Migrations;
using Projectfinal.Model;

namespace Projectfinal
{
    public partial class EditOra : Form
    {
        private readonly dbcontext _dbContext = new dbcontext();
        public EditOra()
        {
            InitializeComponent();
            this.WindowState = FormWindowState.Maximized;
            txtusername.TextChanged += TxtUsername_TextChanged;
            txtUsername1.TextChanged += TxtUsername1_TextChanged;
            txtUsername2.TextChanged += TxtUsername2_TextChanged;
            txtUsername3.TextChanged += TxtUsername3_TextChanged;
        }

        private void textBox11_TextChanged(object sender, EventArgs e)
        {

        }

        private void button3_Click(object sender, EventArgs e)
        {
            new Main().Show();
            this.Hide();
        }

        private void TxtUsername_TextChanged(object sender, EventArgs e)
        {
            try
            {
                string username = txtusername.Text;
                if (string.IsNullOrEmpty(username))
                {
                    ClearFields();
                    return;
                }

                var ordLone = _dbContext.OrdLones.FirstOrDefault(u => u.Username == username);
                if (ordLone != null)
                {
                    PopulateOrdLoneFields(ordLone);
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

        private void PopulateOrdLoneFields(OrdLone ordLone)
        {
            txtFamily.Text = ordLone.Family;
            txtFullname.Text = ordLone.Fullname;
            txtUsername1.Text = ordLone.Username;
            txtUsername2.Text = ordLone.Username1;
            txtUsername3.Text = ordLone.Username2;
            txtFullname1.Text = ordLone.Fullname1;
            txtFullname2.Text = ordLone.Fullname2;
            txtFullname3.Text = ordLone.Fullname3;
            txtPhone1.Text = ordLone.Phone1;
            txtPhone2.Text = ordLone.Phone2;
            txtPhone3.Text = ordLone.Phone3;
            txtInterrate.Text = ordLone.Interrate.ToString();
            txtLoneMoney.Text = ordLone.LoneMoney.ToString();
            txtLoneMoney1.Text = ordLone.LoneMoney1.ToString();
            txtMoneyOld.Text = ordLone.MoneyOld.ToString();
            txtNumberLone.Text = ordLone.NumberLone;
            txtTotalMoneyLone.Text = ordLone.TotalMoneyLone.ToString();
        }

        private void ClearFields()
        {
            txtFamily.Text = string.Empty;
            txtFullname.Text = string.Empty;
            txtUsername1.Text = string.Empty;
            txtUsername2.Text = string.Empty;
            txtUsername3.Text = string.Empty;
            txtFullname1.Text = string.Empty;
            txtFullname2.Text = string.Empty;
            txtFullname3.Text = string.Empty;
            txtPhone1.Text = string.Empty;
            txtPhone2.Text = string.Empty;
            txtPhone3.Text = string.Empty;
            txtLoneMoney.Text = string.Empty;
            txtLoneMoney1.Text = string.Empty;
            txtMoneyOld.Text = string.Empty;
            txtNumberLone.Text = string.Empty;
            txtTotalMoneyLone.Text = string.Empty;
            txtEditUsername1.Text = string.Empty;
            txtEditUsername2.Text = string.Empty;
            txtEditUsername3.Text = string.Empty;
            txtInterrate.Text = string.Empty;
        }
        private void SearchUser(TextBox usernameTextBox, params TextBox[] fields)
        {
            try
            {
                string username = usernameTextBox.Text;
                if (string.IsNullOrEmpty(username))
                {
                    // Clear the fields if username is empty
                    foreach (var field in fields)
                    {
                        field.Text = string.Empty;
                    }
                    return;
                }

                var user = _dbContext.Users.FirstOrDefault(u => u.Username == username);
                if (user != null)
                {
                    if (fields.Length > 0) fields[0].Text = user.Fullname;
                    if (fields.Length > 1) fields[1].Text = user.Phone;
                }
                else
                {
                    // Clear the fields if user is not found
                    foreach (var field in fields)
                    {
                        field.Text = string.Empty;
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"เกิดข้อผิดพลาด: {ex.Message}\n{ex.StackTrace}", "ข้อผิดพลาด", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private void TxtUsername1_TextChanged(object sender, EventArgs e)
        {
            SearchUser(txtUsername1, txtFullname1, txtPhone1);
        }

        private void TxtUsername2_TextChanged(object sender, EventArgs e)
        {
            SearchUser(txtUsername2, txtFullname2, txtPhone2);
        }

        private void TxtUsername3_TextChanged(object sender, EventArgs e)
        {
            SearchUser(txtUsername3, txtFullname3, txtPhone3);
        }
        private void EditOra_Load(object sender, EventArgs e)
        {

        }
        // tedst
        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                using (var transaction = _dbContext.Database.BeginTransaction())
                {
                    try
                    {
                        // Validate username
                        if (string.IsNullOrEmpty(txtusername.Text))
                        {
                            MessageBox.Show("กรุณากรอก Username", "ข้อผิดพลาด", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                            return;
                        }

                        // Validate numeric inputs before processing
                        if (!ValidateNumericInputs())
                        {
                            return;
                        }

                        // Find OrdLone record
                        var ordLone = _dbContext.OrdLones
                            .FirstOrDefault(u => u.Username == txtusername.Text);

                        if (ordLone == null)
                        {
                            MessageBox.Show("ไม่พบข้อมูลในตาราง OrdLone ที่มี Username นี้", "ข้อผิดพลาด",
                                MessageBoxButtons.OK, MessageBoxIcon.Warning);
                            return;
                        }

                        // Update OrdLone with try-parse for numeric values
                        ordLone.Username1 = txtUsername1.Text;
                        ordLone.Fullname1 = txtFullname1.Text;
                        ordLone.Phone1 = txtPhone1.Text;
                        ordLone.Username2 = txtUsername2.Text;
                        ordLone.Fullname2 = txtFullname2.Text;
                        ordLone.Phone2 = txtPhone2.Text;
                        ordLone.Username3 = txtUsername3.Text;
                        ordLone.Fullname3 = txtFullname3.Text;
                        ordLone.Phone3 = txtPhone3.Text;
                        ordLone.NumberLone = txtNumberLone.Text;
                        ordLone.Interrate = txtInterrate.Text;

                        // Find or create EditOrdLone record
                        var editOrdLone = _dbContext.EditOrdLones
                            .FirstOrDefault(e => e.Username == txtusername.Text);

                        if (editOrdLone == null)
                        {
                            editOrdLone = new Model.EditOrdLone
                            {
                                Username = txtusername.Text,
                                TimeLone = DateTime.Now  // Set current date/time
                            };
                            _dbContext.EditOrdLones.Add(editOrdLone);
                        }

                        // Update EditOrdLone
                        UpdateEditOrdLone(editOrdLone);

                        // Save changes within transaction
                        _dbContext.SaveChanges();
                        transaction.Commit();

                        MessageBox.Show("อัปเดตข้อมูลเรียบร้อย", "สำเร็จ",
                            MessageBoxButtons.OK, MessageBoxIcon.Information);
                        ClearFields();
                    }
                    catch (Exception ex)
                    {
                        transaction.Rollback();
                        throw; // Re-throw to be caught by outer catch block
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"เกิดข้อผิดพลาด: {ex.Message}\nรายละเอียด: {ex.InnerException?.Message}",
                    "ข้อผิดพลาด", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void UpdateEditOrdLone(Model.EditOrdLone editOrdLone)
        {
            editOrdLone.Family = txtFamily.Text;
            editOrdLone.Fullname = txtFullname.Text;
            editOrdLone.Username1 = txtUsername1.Text;
            editOrdLone.Fullname1 = txtFullname1.Text;
            editOrdLone.Phone= txtPhone1.Text;
            editOrdLone.Phone1 = txtPhone1.Text;
            editOrdLone.EditUsername1 = txtEditUsername1.Text;
            editOrdLone.Username2 = txtUsername2.Text;
            editOrdLone.Fullname2 = txtFullname2.Text;
            editOrdLone.Phone2 = txtPhone2.Text;
            editOrdLone.EditUsername2 = txtEditUsername2.Text;
            editOrdLone.Username3 = txtUsername3.Text;
            editOrdLone.Fullname3 = txtFullname3.Text;
            editOrdLone.Phone3 = txtPhone3.Text;
            editOrdLone.EditUsername3 = txtEditUsername3.Text;
            editOrdLone.MoneyOld = decimal.Parse(txtMoneyOld.Text);
            editOrdLone.LoneMoney = decimal.Parse(txtLoneMoney.Text);
            editOrdLone.NumberLone = txtNumberLone.Text;
            editOrdLone.LoneMoney1 = int.Parse(txtLoneMoney1.Text);
            editOrdLone.TotalMoneyLone = decimal.Parse(txtTotalMoneyLone.Text);
            editOrdLone.Interrate = decimal.Parse(txtInterrate.Text);
        }

        private bool ValidateNumericInputs()
        {
            if (!decimal.TryParse(txtMoneyOld.Text, out _))
            {
                MessageBox.Show("กรุณากรอกจำนวนเงินเก่าให้ถูกต้อง", "ข้อผิดพลาด",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }
            if (!decimal.TryParse(txtLoneMoney.Text, out _))
            {
                MessageBox.Show("กรุณากรอกจำนวนเงินกู้ให้ถูกต้อง", "ข้อผิดพลาด",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }
            if (!int.TryParse(txtLoneMoney1.Text, out _))
            {
                MessageBox.Show("กรุณากรอกจำนวนเงินกู้ 1 ให้ถูกต้อง", "ข้อผิดพลาด",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }
            if (!decimal.TryParse(txtTotalMoneyLone.Text, out _))
            {
                MessageBox.Show("กรุณากรอกจำนวนเงินกู้รวมให้ถูกต้อง", "ข้อผิดพลาด",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }
            if (!decimal.TryParse(txtInterrate.Text, out _))
            {
                MessageBox.Show("กรุณากรอกอัตราดอกเบี้ยให้ถูกต้อง", "ข้อผิดพลาด",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }
            return true;
        }
    }
}
