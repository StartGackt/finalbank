namespace Projectfinal
{
    partial class MainAdmin
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MainAdmin));
            label3 = new Label();
            dateTimePicker1 = new DateTimePicker();
            label2 = new Label();
            label1 = new Label();
            button2 = new Button();
            button1 = new Button();
            button3 = new Button();
            SuspendLayout();
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.Font = new Font("TH Sarabun New", 25.8F, FontStyle.Bold);
            label3.ForeColor = Color.Transparent;
            label3.Location = new Point(694, 260);
            label3.Name = "label3";
            label3.Size = new Size(410, 57);
            label3.TabIndex = 22;
            label3.Text = "ข้อมูลเกี่ยวกับเจ้าหน้าที่กองทุน  ";
            // 
            // dateTimePicker1
            // 
            dateTimePicker1.Location = new Point(1567, 107);
            dateTimePicker1.Name = "dateTimePicker1";
            dateTimePicker1.Size = new Size(250, 27);
            dateTimePicker1.TabIndex = 21;
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Font = new Font("TH Sarabun New", 25.8F, FontStyle.Bold);
            label2.ForeColor = Color.Gold;
            label2.Location = new Point(565, 164);
            label2.Name = "label2";
            label2.Size = new Size(635, 57);
            label2.TabIndex = 20;
            label2.Text = "ตำบลหนองยายโต๊ะ อำเภอชัยบาดาล จังหวัดลพบุรี ";
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Font = new Font("TH Sarabun New", 25.8F, FontStyle.Bold);
            label1.ForeColor = Color.Gold;
            label1.Location = new Point(519, 107);
            label1.Name = "label1";
            label1.Size = new Size(727, 57);
            label1.TabIndex = 19;
            label1.Text = "ระบบบริหารจัดการกลุ่มออมทรัพย์เพื่อการผลิตบ้านท่ารวก ";
            // 
            // button2
            // 
            button2.BackColor = Color.DarkGreen;
            button2.Font = new Font("TH Sarabun New", 19.7999973F, FontStyle.Bold);
            button2.ForeColor = Color.Transparent;
            button2.Location = new Point(555, 551);
            button2.Name = "button2";
            button2.Size = new Size(645, 105);
            button2.TabIndex = 24;
            button2.Text = "พิมพ์รายงาน";
            button2.UseVisualStyleBackColor = false;
            button2.Click += button2_Click;
            // 
            // button1
            // 
            button1.BackColor = Color.DarkGreen;
            button1.Font = new Font("TH Sarabun New", 19.7999973F, FontStyle.Bold);
            button1.ForeColor = Color.Transparent;
            button1.Location = new Point(555, 377);
            button1.Name = "button1";
            button1.Size = new Size(645, 105);
            button1.TabIndex = 23;
            button1.Text = "การปันผลสมาชิก";
            button1.UseVisualStyleBackColor = false;
            button1.Click += button1_Click;
            // 
            // button3
            // 
            button3.BackColor = Color.DarkGreen;
            button3.Font = new Font("TH Sarabun New", 23.9999981F, FontStyle.Bold);
            button3.ForeColor = Color.Transparent;
            button3.Image = (Image)resources.GetObject("button3.Image");
            button3.Location = new Point(1772, 908);
            button3.Name = "button3";
            button3.Size = new Size(64, 47);
            button3.TabIndex = 219;
            button3.UseVisualStyleBackColor = false;
            button3.Click += button3_Click;
            // 
            // MainAdmin
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.DarkGreen;
            ClientSize = new Size(1884, 986);
            Controls.Add(button3);
            Controls.Add(button2);
            Controls.Add(button1);
            Controls.Add(label3);
            Controls.Add(dateTimePicker1);
            Controls.Add(label2);
            Controls.Add(label1);
            Cursor = Cursors.Default;
            FormBorderStyle = FormBorderStyle.None;
            Name = "MainAdmin";
            Text = "MainAdmin";
            Load += MainAdmin_Load;
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion
        private Label label3;
        private DateTimePicker dateTimePicker1;
        private Label label2;
        private Label label1;
        private Button button2;
        private Button button1;
        private Button button3;
    }
}