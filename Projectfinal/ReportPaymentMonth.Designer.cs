namespace Projectfinal
{
    partial class ReportPaymentMonth
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ReportPaymentMonth));
            comboBox2 = new ComboBox();
            label4 = new Label();
            dataGridView1 = new DataGridView();
            label3 = new Label();
            dateTimePicker1 = new DateTimePicker();
            label2 = new Label();
            label1 = new Label();
            print = new Button();
            button3 = new Button();
            ((System.ComponentModel.ISupportInitialize)dataGridView1).BeginInit();
            SuspendLayout();
            // 
            // comboBox2
            // 
            comboBox2.Font = new Font("Microsoft Sans Serif", 17.9999981F, FontStyle.Bold);
            comboBox2.FormattingEnabled = true;
            comboBox2.Items.AddRange(new object[] { "ประธานกองทุน ", "เหรัญญิก ", "กรรมการ" });
            comboBox2.Location = new Point(707, 268);
            comboBox2.Name = "comboBox2";
            comboBox2.Size = new Size(558, 44);
            comboBox2.TabIndex = 140;
            // 
            // label4
            // 
            label4.AutoSize = true;
            label4.Font = new Font("Microsoft Sans Serif", 22.1999989F, FontStyle.Bold);
            label4.ForeColor = Color.Transparent;
            label4.Location = new Point(569, 268);
            label4.Name = "label4";
            label4.Size = new Size(168, 42);
            label4.TabIndex = 139;
            label4.Text = "เลือกเดือน";
            // 
            // dataGridView1
            // 
            dataGridView1.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dataGridView1.Location = new Point(257, 352);
            dataGridView1.Name = "dataGridView1";
            dataGridView1.RowHeadersWidth = 51;
            dataGridView1.Size = new Size(1246, 403);
            dataGridView1.TabIndex = 138;
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.Font = new Font("Microsoft Sans Serif", 25.8F, FontStyle.Bold);
            label3.ForeColor = Color.Transparent;
            label3.Location = new Point(704, 191);
            label3.Name = "label3";
            label3.Size = new Size(562, 51);
            label3.TabIndex = 137;
            label3.Text = "รายงานการชำระเงินประจำเดือน";
            // 
            // dateTimePicker1
            // 
            dateTimePicker1.Location = new Point(1594, 77);
            dateTimePicker1.Name = "dateTimePicker1";
            dateTimePicker1.Size = new Size(250, 27);
            dateTimePicker1.TabIndex = 136;
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Font = new Font("Microsoft Sans Serif", 25.8F, FontStyle.Bold);
            label2.ForeColor = Color.Gold;
            label2.Location = new Point(582, 134);
            label2.Name = "label2";
            label2.Size = new Size(856, 51);
            label2.TabIndex = 135;
            label2.Text = "ตำบลหนองยายโต๊ะ อำเภอชัยบาดาล จังหวัดลพบุรี ";
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Font = new Font("Microsoft Sans Serif", 25.8F, FontStyle.Bold);
            label1.ForeColor = Color.Gold;
            label1.Location = new Point(546, 77);
            label1.Name = "label1";
            label1.Size = new Size(991, 51);
            label1.TabIndex = 134;
            label1.Text = "ระบบบริหารจัดการกลุ่มออมทรัพย์เพื่อการผลิตบ้านท่ารวก ";
            // 
            // print
            // 
            print.BackColor = Color.DarkGreen;
            print.Font = new Font("Microsoft Sans Serif", 23.9999981F, FontStyle.Bold);
            print.ForeColor = Color.Transparent;
            print.Location = new Point(819, 780);
            print.Name = "print";
            print.Size = new Size(195, 74);
            print.TabIndex = 221;
            print.Text = "พิมพ์";
            print.UseVisualStyleBackColor = false;
            print.Click += print_Click;
            // 
            // button3
            // 
            button3.BackColor = Color.DarkGreen;
            button3.Font = new Font("Microsoft Sans Serif", 23.9999981F, FontStyle.Bold);
            button3.ForeColor = Color.Transparent;
            button3.Image = (Image)resources.GetObject("button3.Image");
            button3.Location = new Point(1737, 824);
            button3.Name = "button3";
            button3.Size = new Size(64, 47);
            button3.TabIndex = 222;
            button3.UseVisualStyleBackColor = false;
            button3.Click += button3_Click;
            // 
            // ReportPaymentMonth
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.DarkGreen;
            ClientSize = new Size(1866, 939);
            Controls.Add(button3);
            Controls.Add(print);
            Controls.Add(comboBox2);
            Controls.Add(label4);
            Controls.Add(dataGridView1);
            Controls.Add(label3);
            Controls.Add(dateTimePicker1);
            Controls.Add(label2);
            Controls.Add(label1);
            Name = "ReportPaymentMonth";
            Text = "ReportPaymentMonth";
            Load += ReportPaymentMonth_Load;
            ((System.ComponentModel.ISupportInitialize)dataGridView1).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private ComboBox comboBox2;
        private Label label4;
        private DataGridView dataGridView1;
        private Label label3;
        private DateTimePicker dateTimePicker1;
        private Label label2;
        private Label label1;
        private Button print;
        private Button button3;
    }
}