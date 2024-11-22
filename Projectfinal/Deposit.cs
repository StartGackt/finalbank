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
    public partial class Deposit : Form
    {
        private readonly dbcontext _dbContext = new dbcontext();
        public Deposit()
        {
            InitializeComponent();
            this.WindowState = FormWindowState.Maximized;
            txtusername.TextChanged += TxtUsername_TextChanged;
            txtMoneyLast.TextChanged += txtMoneyLast_TextChanged;
        }

        // delete
        private void button2_Click(object sender, EventArgs e)
        {
            try
            {
                string username = txtusername.Text;
                if (string.IsNullOrEmpty(username))
                {
                    MessageBox.Show("กรุณากรอกชื่อผู้ใช้", "ข้อผิดพลาด", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                // ค้นหาผู้ใช้จากฐานข้อมูล
                var user = _dbContext.Users.FirstOrDefault(u => u.Username == username);
                if (user != null)
                {
                    // ค้นหาข้อมูลการทำธุรกรรมล่าสุดของผู้ใช้
                    var transaction = _dbContext.MoneyTranss
                        .Where(t => t.Username == username)
                        .OrderByDescending(t => t.TimeMoney)
                        .FirstOrDefault();

                    if (transaction != null)
                    {
                        // ยืนยันการลบข้อมูล
                        var confirmResult = MessageBox.Show("คุณต้องการลบข้อมูลการทำธุรกรรมนี้ใช่หรือไม่?", "ยืนยันการลบ", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                        if (confirmResult == DialogResult.Yes)
                        {
                            // ลบข้อมูลการทำธุรกรรม
                            _dbContext.MoneyTranss.Remove(transaction);
                            _dbContext.SaveChanges();

                            MessageBox.Show("ลบข้อมูลการทำธุรกรรมสำเร็จ", "สำเร็จ", MessageBoxButtons.OK, MessageBoxIcon.Information);

                            // ล้างค่าที่ป้อนในฟอร์มหลังจากลบสำเร็จ
                            ClearFields();
                        }
                    }
                    else
                    {
                        MessageBox.Show("ไม่พบข้อมูลการทำธุรกรรม", "ข้อผิดพลาด", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
                else
                {
                    MessageBox.Show("ไม่พบข้อมูลผู้ใช้", "ข้อผิดพลาด", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    ClearFields();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"เกิดข้อผิดพลาด: {ex.Message}\n{ex.StackTrace}", "ข้อผิดพลาด", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            new Main().Show();
        }

        private void Deposit_Load(object sender, EventArgs e)
        {

        }
        private void TxtUsername_TextChanged(object sender, EventArgs e)
        {
            try
            {
                string username = txtusername.Text;
                if (string.IsNullOrEmpty(username))
                {
                    MessageBox.Show("กรุณากรอกชื่อผู้ใช้", "ข้อผิดพลาด", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    ClearFields();
                    return;
                }

                var user = _dbContext.Users.FirstOrDefault(u => u.Username == username);
                if (user != null)
                {
                    txtFamily.Text = user.Family;
                    txtPhone.Text = user.Phone;
                    txtFullname.Text = user.Fullname;

                    // Fetch the latest transaction for this user, if available
                    var currentTransaction = _dbContext.MoneyTranss
                        .Where(t => t.Username == username)
                        .OrderByDescending(t => t.TimeMoney)
                        .FirstOrDefault();

                    if (currentTransaction != null)
                    {
                        txtMoneyOld.Text = currentTransaction.MoneyTotal.ToString("N2");
                        txtMoneyLast.Clear();
                        txtMoneyTotal.Clear();
                    }
                    else
                    {
                        txtMoneyOld.Text = "0.00";
                        txtMoneyLast.Clear();
                        txtMoneyTotal.Clear();
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
            txtPhone.Clear();
            txtFullname.Clear();
            txtMoneyOld.Text = "0.00";
            txtMoneyLast.Clear();
            txtMoneyTotal.Clear();
        }
        private void txtMoneyLast_TextChanged(object sender, EventArgs e)
        {
            try
            {
                // ตรวจสอบว่า txtMoneyOld และ txtMoneyLast มีค่าเป็นตัวเลขหรือไม่
                if (decimal.TryParse(txtMoneyOld.Text, out decimal moneyOld) &&
                    decimal.TryParse(txtMoneyLast.Text, out decimal moneyLast))
                {
                    // คำนวณยอดเงินรวม
                    decimal moneyTotal = moneyOld + moneyLast;
                    txtMoneyTotal.Text = moneyTotal.ToString("N2"); // แสดงผลเป็นตัวเลขที่มีทศนิยม 2 ตำแหน่ง
                }
                else
                {
                    // ถ้าค่าที่ป้อนไม่ใช่ตัวเลข ให้ตั้งค่าเริ่มต้น
                    txtMoneyOld.Text = "0.00";
                    txtMoneyTotal.Clear();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"เกิดข้อผิดพลาด: {ex.Message}\n{ex.StackTrace}", "ข้อผิดพลาด", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }



        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                // ตรวจสอบค่าที่ป้อนให้ถูกต้อง
                if (!decimal.TryParse(txtMoneyLast.Text, out decimal moneyLast))
                {
                    MessageBox.Show("กรุณากรอกจำนวนเงินฝากใหม่ให้ถูกต้อง", "ข้อผิดพลาด", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                if (!decimal.TryParse(txtMoneyOld.Text, out decimal moneyOld))
                {
                    MessageBox.Show("กรุณากรอกจำนวนเงินฝากเก่าให้ถูกต้อง", "ข้อผิดพลาด", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                // สร้างวัตถุ MoneyTrans เพื่อบันทึกข้อมูลลงฐานข้อมูล
                var moneytrans = new MoneyTrans()
                {
                    Username = txtusername.Text,
                    Family = txtFamily.Text,
                    Phone = txtPhone.Text,
                    Fullname = txtFullname.Text,
                    MoneyLast = moneyLast,
                    MoneyOld = moneyOld,
                    MoneyTotal = moneyOld + moneyLast,
                    TimeMoney = dateTimePicker2.Value // ใช้ค่า DateTime จาก dateTimePicker
                };

                // บันทึกข้อมูลลงฐานข้อมูล
                _dbContext.MoneyTranss.Add(moneytrans);
                _dbContext.SaveChanges();

                MessageBox.Show("บันทึกข้อมูลสำเร็จ", "สำเร็จ", MessageBoxButtons.OK, MessageBoxIcon.Information);

                // ล้างค่าที่ป้อนในฟอร์มหลังจากบันทึกสำเร็จ
                ClearFields();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"เกิดข้อผิดพลาด: {ex.Message}\n{ex.StackTrace}", "ข้อผิดพลาด", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}
