namespace Projectfinal
{
    partial class ReportDepostMonth
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ReportDepostMonth));
            label3 = new Label();
            dateTimePicker1 = new DateTimePicker();
            label2 = new Label();
            label1 = new Label();
            lblPassword = new Label();
            comboBox1 = new ComboBox();
            dataGridView1 = new DataGridView();
            button2 = new Button();
            button3 = new Button();
            ((System.ComponentModel.ISupportInitialize)dataGridView1).BeginInit();
            SuspendLayout();
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.Font = new Font("TH Sarabun New", 25.8F, FontStyle.Bold);
            label3.ForeColor = Color.Transparent;
            label3.Location = new Point(575, 203);
            label3.Name = "label3";
            label3.Size = new Size(602, 57);
            label3.TabIndex = 87;
            label3.Text = "รายงานการฝากเงินสัจจะออมทรัพย์ประจำเดือน ";
            // 
            // dateTimePicker1
            // 
            dateTimePicker1.Location = new Point(1569, 74);
            dateTimePicker1.Name = "dateTimePicker1";
            dateTimePicker1.Size = new Size(250, 27);
            dateTimePicker1.TabIndex = 86;
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Font = new Font("TH Sarabun New", 25.8F, FontStyle.Bold);
            label2.ForeColor = Color.Gold;
            label2.Location = new Point(557, 131);
            label2.Name = "label2";
            label2.Size = new Size(635, 57);
            label2.TabIndex = 85;
            label2.Text = "ตำบลหนองยายโต๊ะ อำเภอชัยบาดาล จังหวัดลพบุรี ";
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Font = new Font("TH Sarabun New", 25.8F, FontStyle.Bold);
            label1.ForeColor = Color.Gold;
            label1.Location = new Point(521, 74);
            label1.Name = "label1";
            label1.Size = new Size(727, 57);
            label1.TabIndex = 84;
            label1.Text = "ระบบบริหารจัดการกลุ่มออมทรัพย์เพื่อการผลิตบ้านท่ารวก ";
            // 
            // lblPassword
            // 
            lblPassword.AutoSize = true;
            lblPassword.Font = new Font("TH Sarabun New", 22.1999989F, FontStyle.Bold);
            lblPassword.ForeColor = Color.Transparent;
            lblPassword.Location = new Point(394, 277);
            lblPassword.Name = "lblPassword";
            lblPassword.Size = new Size(132, 49);
            lblPassword.TabIndex = 88;
            lblPassword.Text = "เลือกเดือน";
            // 
            // comboBox1
            // 
            comboBox1.Font = new Font("TH Sarabun New", 17.9999981F, FontStyle.Bold);
            comboBox1.FormattingEnabled = true;
            comboBox1.Location = new Point(595, 280);
            comboBox1.Name = "comboBox1";
            comboBox1.Size = new Size(558, 48);
            comboBox1.TabIndex = 89;
            comboBox1.SelectedIndexChanged += comboBox1_SelectedIndexChanged_1;
            // 
            // dataGridView1
            // 
            dataGridView1.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dataGridView1.Location = new Point(270, 355);
            dataGridView1.Name = "dataGridView1";
            dataGridView1.RowHeadersWidth = 51;
            dataGridView1.Size = new Size(1246, 403);
            dataGridView1.TabIndex = 90;
            // 
            // button2
            // 
            button2.BackColor = Color.DarkGreen;
            button2.Font = new Font("TH Sarabun New", 23.9999981F, FontStyle.Bold);
            button2.ForeColor = Color.Transparent;
            button2.Location = new Point(783, 809);
            button2.Name = "button2";
            button2.Size = new Size(195, 74);
            button2.TabIndex = 221;
            button2.Text = "พิมพ์";
            button2.UseVisualStyleBackColor = false;
            // 
            // button3
            // 
            button3.BackColor = Color.DarkGreen;
            button3.Font = new Font("TH Sarabun New", 23.9999981F, FontStyle.Bold);
            button3.ForeColor = Color.Transparent;
            button3.Image = (Image)resources.GetObject("button3.Image");
            button3.Location = new Point(1738, 885);
            button3.Name = "button3";
            button3.Size = new Size(64, 47);
            button3.TabIndex = 222;
            button3.UseVisualStyleBackColor = false;
            button3.Click += button3_Click;
            // 
            // ReportDepostMonth
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.DarkGreen;
            ClientSize = new Size(1902, 1033);
            Controls.Add(button3);
            Controls.Add(button2);
            Controls.Add(dataGridView1);
            Controls.Add(comboBox1);
            Controls.Add(lblPassword);
            Controls.Add(label3);
            Controls.Add(dateTimePicker1);
            Controls.Add(label2);
            Controls.Add(label1);
            FormBorderStyle = FormBorderStyle.None;
            Name = "ReportDepostMonth";
            Text = "ReportDepostMonth";
            Load += ReportDepostMonth_Load;
            ((System.ComponentModel.ISupportInitialize)dataGridView1).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Label label3;
        private DateTimePicker dateTimePicker1;
        private Label label2;
        private Label label1;
        private Label lblPassword;
        private ComboBox comboBox1;
        private DataGridView dataGridView1;
        private Button button2;
        private Button button3;
    }
}