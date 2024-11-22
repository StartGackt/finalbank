namespace Projectfinal
{
    partial class MainUser
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MainUser));
            button1 = new Button();
            label3 = new Label();
            dateTimePicker1 = new DateTimePicker();
            label2 = new Label();
            label1 = new Label();
            button2 = new Button();
            button3 = new Button();
            button4 = new Button();
            button5 = new Button();
            button6 = new Button();
            SuspendLayout();
            // 
            // button1
            // 
            button1.BackColor = Color.DarkGreen;
            button1.Font = new Font("TH Sarabun New", 16.1999989F, FontStyle.Bold);
            button1.ForeColor = Color.Transparent;
            button1.Location = new Point(612, 343);
            button1.Name = "button1";
            button1.Size = new Size(645, 77);
            button1.TabIndex = 15;
            button1.Text = "การฝากเงินสัจจะสะสม ";
            button1.UseVisualStyleBackColor = false;
            button1.Click += button1_Click;
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.Font = new Font("TH Sarabun New", 25.8F, FontStyle.Bold);
            label3.ForeColor = Color.Transparent;
            label3.Location = new Point(847, 252);
            label3.Name = "label3";
            label3.Size = new Size(164, 57);
            label3.TabIndex = 14;
            label3.Text = "หน้าสมาชิก";
            // 
            // dateTimePicker1
            // 
            dateTimePicker1.Location = new Point(1603, 111);
            dateTimePicker1.Name = "dateTimePicker1";
            dateTimePicker1.Size = new Size(250, 27);
            dateTimePicker1.TabIndex = 13;
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Font = new Font("TH Sarabun New", 25.8F, FontStyle.Bold);
            label2.ForeColor = Color.Gold;
            label2.Location = new Point(591, 168);
            label2.Name = "label2";
            label2.Size = new Size(635, 57);
            label2.TabIndex = 12;
            label2.Text = "ตำบลหนองยายโต๊ะ อำเภอชัยบาดาล จังหวัดลพบุรี ";
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Font = new Font("TH Sarabun New", 25.8F, FontStyle.Bold);
            label1.ForeColor = Color.Gold;
            label1.Location = new Point(555, 111);
            label1.Name = "label1";
            label1.Size = new Size(727, 57);
            label1.TabIndex = 11;
            label1.Text = "ระบบบริหารจัดการกลุ่มออมทรัพย์เพื่อการผลิตบ้านท่ารวก ";
            // 
            // button2
            // 
            button2.BackColor = Color.DarkGreen;
            button2.Font = new Font("TH Sarabun New", 16.1999989F, FontStyle.Bold);
            button2.ForeColor = Color.Transparent;
            button2.Location = new Point(612, 445);
            button2.Name = "button2";
            button2.Size = new Size(645, 77);
            button2.TabIndex = 16;
            button2.Text = "การชำระเงินของสมาชิก";
            button2.UseVisualStyleBackColor = false;
            button2.Click += button2_Click;
            // 
            // button3
            // 
            button3.BackColor = Color.DarkGreen;
            button3.Font = new Font("TH Sarabun New", 16.1999989F, FontStyle.Bold);
            button3.ForeColor = Color.Transparent;
            button3.Location = new Point(612, 671);
            button3.Name = "button3";
            button3.Size = new Size(645, 77);
            button3.TabIndex = 17;
            button3.Text = "การสรุปเงินฝากสมาชิก ";
            button3.UseVisualStyleBackColor = false;
            button3.Click += button3_Click;
            // 
            // button4
            // 
            button4.BackColor = Color.DarkGreen;
            button4.Font = new Font("TH Sarabun New", 16.1999989F, FontStyle.Bold);
            button4.ForeColor = Color.Transparent;
            button4.Location = new Point(612, 557);
            button4.Name = "button4";
            button4.Size = new Size(645, 77);
            button4.TabIndex = 18;
            button4.Text = "การกู้เงิน ";
            button4.UseVisualStyleBackColor = false;
            button4.Click += button4_Click;
            // 
            // button5
            // 
            button5.BackColor = Color.DarkGreen;
            button5.Font = new Font("TH Sarabun New", 16.1999989F, FontStyle.Bold);
            button5.ForeColor = Color.Transparent;
            button5.Location = new Point(614, 797);
            button5.Name = "button5";
            button5.Size = new Size(645, 77);
            button5.TabIndex = 19;
            button5.Text = "การลาออกของสมาชิก  ";
            button5.UseVisualStyleBackColor = false;
            button5.Click += button5_Click;
            // 
            // button6
            // 
            button6.BackColor = Color.DarkGreen;
            button6.Font = new Font("TH Sarabun New", 23.9999981F, FontStyle.Bold);
            button6.ForeColor = Color.Transparent;
            button6.Image = (Image)resources.GetObject("button6.Image");
            button6.Location = new Point(1811, 958);
            button6.Name = "button6";
            button6.Size = new Size(64, 47);
            button6.TabIndex = 219;
            button6.UseVisualStyleBackColor = false;
            button6.Click += button6_Click;
            // 
            // MainUser
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.DarkGreen;
            ClientSize = new Size(1902, 1033);
            Controls.Add(button6);
            Controls.Add(button5);
            Controls.Add(button4);
            Controls.Add(button3);
            Controls.Add(button2);
            Controls.Add(button1);
            Controls.Add(label3);
            Controls.Add(dateTimePicker1);
            Controls.Add(label2);
            Controls.Add(label1);
            FormBorderStyle = FormBorderStyle.None;
            Name = "MainUser";
            Text = "MainUser";
            Load += MainUser_Load;
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Button button1;
        private Label label3;
        private DateTimePicker dateTimePicker1;
        private Label label2;
        private Label label1;
        private Button button2;
        private Button button3;
        private Button button4;
        private Button button5;
        private Button button6;
    }
}