namespace Projectfinal
{
    partial class ReportPaymentYear
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ReportPaymentYear));
            comboBox1 = new ComboBox();
            label5 = new Label();
            comboBox2 = new ComboBox();
            label4 = new Label();
            dataGridView1 = new DataGridView();
            dateTimePicker1 = new DateTimePicker();
            label2 = new Label();
            label1 = new Label();
            button2 = new Button();
            button3 = new Button();
            label3 = new Label();
            ((System.ComponentModel.ISupportInitialize)dataGridView1).BeginInit();
            SuspendLayout();
            // 
            // comboBox1
            // 
            comboBox1.Font = new Font("TH Sarabun New", 17.9999981F, FontStyle.Bold);
            comboBox1.FormattingEnabled = true;
            comboBox1.Items.AddRange(new object[] { "ประธานกองทุน ", "เหรัญญิก ", "กรรมการ" });
            comboBox1.Location = new Point(285, 267);
            comboBox1.Name = "comboBox1";
            comboBox1.Size = new Size(558, 48);
            comboBox1.TabIndex = 142;
            // 
            // label5
            // 
            label5.AutoSize = true;
            label5.Font = new Font("TH Sarabun New", 22.1999989F, FontStyle.Bold);
            label5.ForeColor = Color.Transparent;
            label5.Location = new Point(148, 267);
            label5.Name = "label5";
            label5.Size = new Size(93, 49);
            label5.TabIndex = 141;
            label5.Text = "เลือกปี";
            // 
            // comboBox2
            // 
            comboBox2.Font = new Font("TH Sarabun New", 17.9999981F, FontStyle.Bold);
            comboBox2.FormattingEnabled = true;
            comboBox2.Items.AddRange(new object[] { "ประธานกองทุน ", "เหรัญญิก ", "กรรมการ" });
            comboBox2.Location = new Point(1047, 267);
            comboBox2.Name = "comboBox2";
            comboBox2.Size = new Size(558, 48);
            comboBox2.TabIndex = 140;
            // 
            // label4
            // 
            label4.AutoSize = true;
            label4.Font = new Font("TH Sarabun New", 22.1999989F, FontStyle.Bold);
            label4.ForeColor = Color.Transparent;
            label4.Location = new Point(891, 267);
            label4.Name = "label4";
            label4.Size = new Size(132, 49);
            label4.TabIndex = 139;
            label4.Text = "เลือกเดือน";
            // 
            // dataGridView1
            // 
            dataGridView1.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dataGridView1.Location = new Point(328, 357);
            dataGridView1.Name = "dataGridView1";
            dataGridView1.RowHeadersWidth = 51;
            dataGridView1.Size = new Size(1246, 403);
            dataGridView1.TabIndex = 138;
            // 
            // dateTimePicker1
            // 
            dateTimePicker1.Location = new Point(1576, 75);
            dateTimePicker1.Name = "dateTimePicker1";
            dateTimePicker1.Size = new Size(250, 27);
            dateTimePicker1.TabIndex = 136;
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Font = new Font("TH Sarabun New", 25.8F, FontStyle.Bold);
            label2.ForeColor = Color.Gold;
            label2.Location = new Point(564, 132);
            label2.Name = "label2";
            label2.Size = new Size(635, 57);
            label2.TabIndex = 135;
            label2.Text = "ตำบลหนองยายโต๊ะ อำเภอชัยบาดาล จังหวัดลพบุรี ";
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Font = new Font("TH Sarabun New", 25.8F, FontStyle.Bold);
            label1.ForeColor = Color.Gold;
            label1.Location = new Point(528, 75);
            label1.Name = "label1";
            label1.Size = new Size(727, 57);
            label1.TabIndex = 134;
            label1.Text = "ระบบบริหารจัดการกลุ่มออมทรัพย์เพื่อการผลิตบ้านท่ารวก ";
            // 
            // button2
            // 
            button2.BackColor = Color.DarkGreen;
            button2.Font = new Font("TH Sarabun New", 23.9999981F, FontStyle.Bold);
            button2.ForeColor = Color.Transparent;
            button2.Location = new Point(801, 797);
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
            button3.Location = new Point(1762, 864);
            button3.Name = "button3";
            button3.Size = new Size(64, 47);
            button3.TabIndex = 222;
            button3.UseVisualStyleBackColor = false;
            button3.Click += button3_Click;
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.Font = new Font("TH Sarabun New", 25.8F, FontStyle.Bold);
            label3.ForeColor = Color.Transparent;
            label3.Location = new Point(704, 189);
            label3.Name = "label3";
            label3.Size = new Size(372, 57);
            label3.TabIndex = 223;
            label3.Text = "รายงานการชำระเงินประจำปี";
            // 
            // ReportPaymentYear
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.DarkGreen;
            ClientSize = new Size(1884, 986);
            Controls.Add(label3);
            Controls.Add(button3);
            Controls.Add(button2);
            Controls.Add(comboBox1);
            Controls.Add(label5);
            Controls.Add(comboBox2);
            Controls.Add(label4);
            Controls.Add(dataGridView1);
            Controls.Add(dateTimePicker1);
            Controls.Add(label2);
            Controls.Add(label1);
            ForeColor = SystemColors.ButtonHighlight;
            Name = "ReportPaymentYear";
            Text = "ReportPaymentYear";
            Load += ReportPaymentYear_Load;
            ((System.ComponentModel.ISupportInitialize)dataGridView1).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private ComboBox comboBox1;
        private Label label5;
        private ComboBox comboBox2;
        private Label label4;
        private DataGridView dataGridView1;
        private DateTimePicker dateTimePicker1;
        private Label label2;
        private Label label1;
        private Button button2;
        private Button button3;
        private Label label3;
    }
}