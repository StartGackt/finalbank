namespace Projectfinal
{
    partial class ReportEmerMonth
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ReportEmerMonth));
            dataGridView1 = new DataGridView();
            label3 = new Label();
            dateTimePicker1 = new DateTimePicker();
            label2 = new Label();
            label1 = new Label();
            comboBox2 = new ComboBox();
            label4 = new Label();
            button2 = new Button();
            button3 = new Button();
            ((System.ComponentModel.ISupportInitialize)dataGridView1).BeginInit();
            SuspendLayout();
            // 
            // dataGridView1
            // 
            dataGridView1.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dataGridView1.Location = new Point(302, 349);
            dataGridView1.Name = "dataGridView1";
            dataGridView1.RowHeadersWidth = 51;
            dataGridView1.Size = new Size(1246, 403);
            dataGridView1.TabIndex = 115;
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.Font = new Font("TH Sarabun New", 25.8F, FontStyle.Bold);
            label3.ForeColor = Color.Transparent;
            label3.Location = new Point(615, 183);
            label3.Name = "label3";
            label3.Size = new Size(559, 57);
            label3.TabIndex = 112;
            label3.Text = "รายงานการกู้เงินประจำเดือน :(เงินกู้ฉุกเฉิน) ";
            // 
            // dateTimePicker1
            // 
            dateTimePicker1.Location = new Point(1550, 67);
            dateTimePicker1.Name = "dateTimePicker1";
            dateTimePicker1.Size = new Size(250, 27);
            dateTimePicker1.TabIndex = 111;
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Font = new Font("TH Sarabun New", 25.8F, FontStyle.Bold);
            label2.ForeColor = Color.Gold;
            label2.Location = new Point(538, 124);
            label2.Name = "label2";
            label2.Size = new Size(635, 57);
            label2.TabIndex = 110;
            label2.Text = "ตำบลหนองยายโต๊ะ อำเภอชัยบาดาล จังหวัดลพบุรี ";
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Font = new Font("TH Sarabun New", 25.8F, FontStyle.Bold);
            label1.ForeColor = Color.Gold;
            label1.Location = new Point(502, 67);
            label1.Name = "label1";
            label1.Size = new Size(727, 57);
            label1.TabIndex = 109;
            label1.Text = "ระบบบริหารจัดการกลุ่มออมทรัพย์เพื่อการผลิตบ้านท่ารวก ";
            // 
            // comboBox2
            // 
            comboBox2.Font = new Font("TH Sarabun New", 17.9999981F, FontStyle.Bold);
            comboBox2.FormattingEnabled = true;
            comboBox2.Items.AddRange(new object[] { "ประธานกองทุน ", "เหรัญญิก ", "กรรมการ" });
            comboBox2.Location = new Point(676, 261);
            comboBox2.Name = "comboBox2";
            comboBox2.Size = new Size(558, 48);
            comboBox2.TabIndex = 117;
            comboBox2.SelectedIndexChanged += comboBox2_SelectedIndexChanged;
            // 
            // label4
            // 
            label4.AutoSize = true;
            label4.Font = new Font("TH Sarabun New", 22.1999989F, FontStyle.Bold);
            label4.ForeColor = Color.Transparent;
            label4.Location = new Point(538, 261);
            label4.Name = "label4";
            label4.Size = new Size(132, 49);
            label4.TabIndex = 116;
            label4.Text = "เลือกเดือน";
            // 
            // button2
            // 
            button2.BackColor = Color.DarkGreen;
            button2.Font = new Font("TH Sarabun New", 23.9999981F, FontStyle.Bold);
            button2.ForeColor = Color.Transparent;
            button2.Location = new Point(816, 800);
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
            button3.Location = new Point(1736, 903);
            button3.Name = "button3";
            button3.Size = new Size(64, 47);
            button3.TabIndex = 222;
            button3.UseVisualStyleBackColor = false;
            button3.Click += button3_Click;
            // 
            // ReportEmerMonth
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.DarkGreen;
            ClientSize = new Size(1902, 1033);
            Controls.Add(button3);
            Controls.Add(button2);
            Controls.Add(comboBox2);
            Controls.Add(label4);
            Controls.Add(dataGridView1);
            Controls.Add(label3);
            Controls.Add(dateTimePicker1);
            Controls.Add(label2);
            Controls.Add(label1);
            Name = "ReportEmerMonth";
            Text = "ReportEmerMonth";
            Load += ReportEmerMonth_Load;
            ((System.ComponentModel.ISupportInitialize)dataGridView1).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private DataGridView dataGridView1;
        private Label label3;
        private DateTimePicker dateTimePicker1;
        private Label label2;
        private Label label1;
        private ComboBox comboBox2;
        private Label label4;
        private Button button2;
        private Button button3;
    }
}