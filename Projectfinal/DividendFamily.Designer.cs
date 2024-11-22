namespace Projectfinal
{
    partial class DividendFamily
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(DividendFamily));
            txtfamily = new TextBox();
            lblPassword = new Label();
            label3 = new Label();
            dateTimePicker1 = new DateTimePicker();
            label2 = new Label();
            label1 = new Label();
            dataGridView1 = new DataGridView();
            button1 = new Button();
            button6 = new Button();
            ((System.ComponentModel.ISupportInitialize)dataGridView1).BeginInit();
            SuspendLayout();
            // 
            // txtfamily
            // 
            txtfamily.Font = new Font("TH Sarabun New", 17.9999981F, FontStyle.Bold);
            txtfamily.Location = new Point(742, 342);
            txtfamily.Name = "txtfamily";
            txtfamily.Size = new Size(462, 47);
            txtfamily.TabIndex = 78;
            // 
            // lblPassword
            // 
            lblPassword.AutoSize = true;
            lblPassword.Font = new Font("TH Sarabun New", 22.1999989F, FontStyle.Bold);
            lblPassword.ForeColor = Color.Transparent;
            lblPassword.Location = new Point(511, 342);
            lblPassword.Name = "lblPassword";
            lblPassword.Size = new Size(170, 49);
            lblPassword.TabIndex = 76;
            lblPassword.Text = "รหัสครอบครัว";
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.Font = new Font("TH Sarabun New", 25.8F, FontStyle.Bold);
            label3.ForeColor = Color.Transparent;
            label3.Location = new Point(475, 233);
            label3.Name = "label3";
            label3.Size = new Size(796, 57);
            label3.TabIndex = 73;
            label3.Text = "ข้อมูลเกี่ยวกับเจ้าหน้าที่กองทุน : การปันผลสมาชิกรายครอบครัว ";
            // 
            // dateTimePicker1
            // 
            dateTimePicker1.Location = new Point(1538, 92);
            dateTimePicker1.Name = "dateTimePicker1";
            dateTimePicker1.Size = new Size(250, 27);
            dateTimePicker1.TabIndex = 72;
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Font = new Font("TH Sarabun New", 25.8F, FontStyle.Bold);
            label2.ForeColor = Color.Gold;
            label2.Location = new Point(526, 149);
            label2.Name = "label2";
            label2.Size = new Size(635, 57);
            label2.TabIndex = 71;
            label2.Text = "ตำบลหนองยายโต๊ะ อำเภอชัยบาดาล จังหวัดลพบุรี ";
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Font = new Font("TH Sarabun New", 25.8F, FontStyle.Bold);
            label1.ForeColor = Color.Gold;
            label1.Location = new Point(490, 92);
            label1.Name = "label1";
            label1.Size = new Size(727, 57);
            label1.TabIndex = 70;
            label1.Text = "ระบบบริหารจัดการกลุ่มออมทรัพย์เพื่อการผลิตบ้านท่ารวก ";
            // 
            // dataGridView1
            // 
            dataGridView1.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dataGridView1.Location = new Point(273, 424);
            dataGridView1.Name = "dataGridView1";
            dataGridView1.RowHeadersWidth = 51;
            dataGridView1.Size = new Size(1309, 397);
            dataGridView1.TabIndex = 79;
            // 
            // button1
            // 
            button1.BackColor = Color.DarkGreen;
            button1.Font = new Font("TH Sarabun New", 23.9999981F, FontStyle.Bold);
            button1.ForeColor = Color.Transparent;
            button1.Location = new Point(802, 859);
            button1.Name = "button1";
            button1.Size = new Size(195, 74);
            button1.TabIndex = 80;
            button1.Text = "พิมพ์";
            button1.UseVisualStyleBackColor = false;
            // 
            // button6
            // 
            button6.BackColor = Color.DarkGreen;
            button6.Font = new Font("TH Sarabun New", 23.9999981F, FontStyle.Bold);
            button6.ForeColor = Color.Transparent;
            button6.Image = (Image)resources.GetObject("button6.Image");
            button6.Location = new Point(1707, 905);
            button6.Name = "button6";
            button6.Size = new Size(64, 47);
            button6.TabIndex = 220;
            button6.UseVisualStyleBackColor = false;
            button6.Click += button6_Click;
            // 
            // DividendFamily
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.DarkGreen;
            ClientSize = new Size(1851, 1006);
            Controls.Add(button6);
            Controls.Add(button1);
            Controls.Add(dataGridView1);
            Controls.Add(txtfamily);
            Controls.Add(lblPassword);
            Controls.Add(label3);
            Controls.Add(dateTimePicker1);
            Controls.Add(label2);
            Controls.Add(label1);
            FormBorderStyle = FormBorderStyle.None;
            Name = "DividendFamily";
            Text = "DividendFamily";
            Load += DividendFamily_Load;
            ((System.ComponentModel.ISupportInitialize)dataGridView1).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private TextBox txtfamily;
        private Label lblPassword;
        private Label label3;
        private DateTimePicker dateTimePicker1;
        private Label label2;
        private Label label1;
        private DataGridView dataGridView1;
        private Button button1;
        private Button button6;
    }
}