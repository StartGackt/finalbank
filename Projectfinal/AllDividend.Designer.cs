namespace Projectfinal
{
    partial class AllDividend
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(AllDividend));
            dataGridView1 = new DataGridView();
            label3 = new Label();
            dateTimePicker1 = new DateTimePicker();
            label2 = new Label();
            label1 = new Label();
            button1 = new Button();
            button2 = new Button();
            button6 = new Button();
            ((System.ComponentModel.ISupportInitialize)dataGridView1).BeginInit();
            SuspendLayout();
            // 
            // dataGridView1
            // 
            dataGridView1.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dataGridView1.Location = new Point(258, 395);
            dataGridView1.Name = "dataGridView1";
            dataGridView1.RowHeadersWidth = 51;
            dataGridView1.Size = new Size(1309, 397);
            dataGridView1.TabIndex = 86;
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.Font = new Font("Microsoft Sans Serif", 25.8F, FontStyle.Bold);
            label3.ForeColor = Color.Transparent;
            label3.Location = new Point(460, 204);
            label3.Name = "label3";
            label3.Size = new Size(1066, 51);
            label3.TabIndex = 83;
            label3.Text = " ข้อมูลเกี่ยวกับเจ้าหน้าที่กองทุน : สรุปการปันผลสมาชิกทั้งหมด ";
            // 
            // dateTimePicker1
            // 
            dateTimePicker1.Location = new Point(1523, 63);
            dateTimePicker1.Name = "dateTimePicker1";
            dateTimePicker1.Size = new Size(250, 27);
            dateTimePicker1.TabIndex = 82;
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Font = new Font("Microsoft Sans Serif", 25.8F, FontStyle.Bold);
            label2.ForeColor = Color.Gold;
            label2.Location = new Point(511, 120);
            label2.Name = "label2";
            label2.Size = new Size(856, 51);
            label2.TabIndex = 81;
            label2.Text = "ตำบลหนองยายโต๊ะ อำเภอชัยบาดาล จังหวัดลพบุรี ";
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Font = new Font("Microsoft Sans Serif", 25.8F, FontStyle.Bold);
            label1.ForeColor = Color.Gold;
            label1.Location = new Point(475, 63);
            label1.Name = "label1";
            label1.Size = new Size(991, 51);
            label1.TabIndex = 80;
            label1.Text = "ระบบบริหารจัดการกลุ่มออมทรัพย์เพื่อการผลิตบ้านท่ารวก ";
            // 
            // button1
            // 
            button1.BackColor = Color.DarkGreen;
            button1.Font = new Font("Microsoft Sans Serif", 23.9999981F, FontStyle.Bold);
            button1.ForeColor = Color.Transparent;
            button1.Location = new Point(799, 292);
            button1.Name = "button1";
            button1.Size = new Size(195, 74);
            button1.TabIndex = 87;
            button1.Text = "ค้นหา";
            button1.UseVisualStyleBackColor = false;
            button1.Click += button1_Click;
            // 
            // button2
            // 
            button2.BackColor = Color.DarkGreen;
            button2.Font = new Font("Microsoft Sans Serif", 23.9999981F, FontStyle.Bold);
            button2.ForeColor = Color.Transparent;
            button2.Location = new Point(799, 840);
            button2.Name = "button2";
            button2.Size = new Size(195, 74);
            button2.TabIndex = 88;
            button2.Text = "พิมพ์";
            button2.UseVisualStyleBackColor = false;
            button2.Click += button2_Click;
            // 
            // button6
            // 
            button6.BackColor = Color.DarkGreen;
            button6.Font = new Font("Microsoft Sans Serif", 23.9999981F, FontStyle.Bold);
            button6.ForeColor = Color.Transparent;
            button6.Image = (Image)resources.GetObject("button6.Image");
            button6.Location = new Point(1700, 884);
            button6.Name = "button6";
            button6.Size = new Size(64, 47);
            button6.TabIndex = 220;
            button6.UseVisualStyleBackColor = false;
            button6.Click += button6_Click;
            // 
            // AllDividend
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.DarkGreen;
            ClientSize = new Size(1884, 986);
            Controls.Add(button6);
            Controls.Add(button2);
            Controls.Add(button1);
            Controls.Add(dataGridView1);
            Controls.Add(label3);
            Controls.Add(dateTimePicker1);
            Controls.Add(label2);
            Controls.Add(label1);
            Name = "AllDividend";
            Text = "AllDividend";
            Load += AllDividend_Load;
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
        private Button button1;
        private Button button2;
        private Button button6;
    }
}