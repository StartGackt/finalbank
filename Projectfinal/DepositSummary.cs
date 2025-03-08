using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using PdfSharp.Drawing;
using PdfSharp.Fonts;
using PdfSharp.Pdf;
using Projectfinal.Model;
using static System.Runtime.InteropServices.JavaScript.JSType;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.StartPanel;

namespace Projectfinal
{
    public partial class DepositSummary : Form
    {
        private readonly dbcontext _dbContext = new dbcontext();
        public DepositSummary()
        {
            InitializeComponent();
            this.WindowState = FormWindowState.Maximized;
            // Required for PdfSharp to support UTF-8 encoding (for Thai characters)
            Encoding.RegisterProvider(CodePagesEncodingProvider.Instance);

            txtusername.TextChanged += TxtUsername_TextChanged;
        }

        private void button3_Click(object sender, EventArgs e)
        {

            new Main().Show();
            this.Hide();
        }


        private void DepositSummary_Load(object sender, EventArgs e)
        {

        }

        private void TxtUsername_TextChanged(object sender, EventArgs e)
        {
            try
            {
                string username = txtusername.Text;

                if (string.IsNullOrEmpty(username))
                {
                    // Clear the DataGridView and txtTotalMoneyLone when username is empty
                    dataGridView1.DataSource = null;
                    txtTotalMoneyLone.Text = "";
                    return;
                }

                // Search for user data
                var user = _dbContext.Users.FirstOrDefault(u => u.Username == username);
                if (user != null)
                {
                    txtFamily.Text = user.Family;
                    txtFullname.Text = user.Fullname;

                    // Get all transactions for this user and order by date descending
                    var transactions = _dbContext.MoneyTranss
                        .Where(t => t.Username == username)
                        .OrderByDescending(t => t.TimeMoney)  // Changed from TransactionDate to TimeMoney
                        .ToList();

                    // Display transactions in DataGridView
                    dataGridView1.DataSource = transactions;

                    // Get the latest deposit amount
                    var latestDeposit = transactions.FirstOrDefault();
                    if (latestDeposit != null)
                    {
                        txtTotalMoneyLone.Text = latestDeposit.MoneyTotal.ToString("N2"); // Changed from Amount to MoneyTotal
                    }
                    else
                    {
                        txtTotalMoneyLone.Text = "0.00";
                    }

                    // Optional: Format the DataGridView
                    FormatDataGridView();
                }
                else
                {
                    // Clear all fields if user not found
                    txtFamily.Text = "";
                    txtFullname.Text = "";
                    dataGridView1.DataSource = null;
                    txtTotalMoneyLone.Text = "";
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("An error occurred: " + ex.Message, "Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void FormatDataGridView()
        {
            dataGridView1.AutoResizeColumns();
            dataGridView1.AlternatingRowsDefaultCellStyle.BackColor = Color.LightGray;
            dataGridView1.EnableHeadersVisualStyles = false;
            dataGridView1.ColumnHeadersDefaultCellStyle.BackColor = Color.LightBlue;
            dataGridView1.ColumnHeadersDefaultCellStyle.ForeColor = Color.Black;

            // Set column headers in Thai or English as needed
            if (dataGridView1.Columns.Count > 0)
            {
                dataGridView1.Columns["Username"].HeaderText = "ชื่อผู้ใช้";
                dataGridView1.Columns["Family"].HeaderText = "ครอบครัว";
                dataGridView1.Columns["Phone"].HeaderText = "เบอร์โทร";
                dataGridView1.Columns["Fullname"].HeaderText = "ชื่อ-นามสกุล";
                dataGridView1.Columns["MoneyOld"].HeaderText = "เงินเก่า";
                dataGridView1.Columns["MoneyLast"].HeaderText = "เงินล่าสุด";
                dataGridView1.Columns["MoneyTotal"].HeaderText = "เงินรวม";
                dataGridView1.Columns["TimeMoney"].HeaderText = "วันที่ทำรายการ";
            }
        }



        private void button2_Click(object sender, EventArgs e)
        {

        }

        //print
        private void button1_Click(object sender, EventArgs e)
        {
       
        }
    }
}
