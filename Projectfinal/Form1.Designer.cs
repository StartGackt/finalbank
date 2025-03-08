namespace Projectfinal
{
    partial class Form1
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
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
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            label1 = new Label();
            label2 = new Label();
            dateTimePicker1 = new DateTimePicker();
            label3 = new Label();
            Username = new Label();
            Password = new Label();
            txtUsername = new TextBox();
            txtPassword = new TextBox();
            button1 = new Button();
            button2 = new Button();
            SuspendLayout();
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Font = new Font("Microsoft Sans Serif", 25.8F, FontStyle.Bold, GraphicsUnit.Point, 0);
            label1.ForeColor = Color.Gold;
            label1.Location = new Point(584, 187);
            label1.Name = "label1";
            label1.Size = new Size(991, 51);
            label1.TabIndex = 0;
            label1.Text = "ระบบบริหารจัดการกลุ่มออมทรัพย์เพื่อการผลิตบ้านท่ารวก ";
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Font = new Font("Microsoft Sans Serif", 25.8F, FontStyle.Bold, GraphicsUnit.Point, 0);
            label2.ForeColor = Color.Gold;
            label2.Location = new Point(620, 244);
            label2.Name = "label2";
            label2.Size = new Size(856, 51);
            label2.TabIndex = 1;
            label2.Text = "ตำบลหนองยายโต๊ะ อำเภอชัยบาดาล จังหวัดลพบุรี ";
            // 
            // dateTimePicker1
            // 
            dateTimePicker1.Location = new Point(1632, 187);
            dateTimePicker1.Name = "dateTimePicker1";
            dateTimePicker1.Size = new Size(250, 27);
            dateTimePicker1.TabIndex = 2;
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.Font = new Font("Microsoft Sans Serif", 25.8F, FontStyle.Bold, GraphicsUnit.Point, 0);
            label3.ForeColor = Color.Transparent;
            label3.Location = new Point(848, 343);
            label3.Name = "label3";
            label3.Size = new Size(274, 51);
            label3.TabIndex = 3;
            label3.Text = " การเข้าสู่ระบบ";
            // 
            // Username
            // 
            Username.AutoSize = true;
            Username.Font = new Font("Microsoft Sans Serif", 25.8F, FontStyle.Bold, GraphicsUnit.Point, 0);
            Username.ForeColor = Color.Transparent;
            Username.Location = new Point(319, 468);
            Username.Name = "Username";
            Username.Size = new Size(414, 51);
            Username.TabIndex = 4;
            Username.Text = " ชื่อผู้ใช้งาน (เจ้าหน้าที่)";
            // 
            // Password
            // 
            Password.AutoSize = true;
            Password.Font = new Font("Microsoft Sans Serif", 25.8F, FontStyle.Bold, GraphicsUnit.Point, 0);
            Password.ForeColor = Color.Transparent;
            Password.Location = new Point(428, 626);
            Password.Name = "Password";
            Password.Size = new Size(167, 51);
            Password.TabIndex = 5;
            Password.Text = "รหัสผ่าน";
            // 
            // txtUsername
            // 
            txtUsername.Font = new Font("Microsoft Sans Serif", 19.7999973F, FontStyle.Bold, GraphicsUnit.Point, 0);
            txtUsername.Location = new Point(793, 475);
            txtUsername.Name = "txtUsername";
            txtUsername.Size = new Size(462, 45);
            txtUsername.TabIndex = 6;
            // 
            // txtPassword
            // 
            txtPassword.Font = new Font("Microsoft Sans Serif", 19.7999973F, FontStyle.Bold, GraphicsUnit.Point, 0);
            txtPassword.Location = new Point(793, 633);
            txtPassword.Name = "txtPassword";
            txtPassword.Size = new Size(462, 45);
            txtPassword.TabIndex = 7;
            // 
            // button1
            // 
            button1.BackColor = Color.DarkGreen;
            button1.Font = new Font("Microsoft Sans Serif", 19.7999973F, FontStyle.Bold, GraphicsUnit.Point, 0);
            button1.ForeColor = Color.Transparent;
            button1.Location = new Point(704, 784);
            button1.Name = "button1";
            button1.Size = new Size(178, 56);
            button1.TabIndex = 8;
            button1.Text = "เข้าสู่ระบบ";
            button1.UseVisualStyleBackColor = false;
            button1.Click += button1_Click;
            // 
            // button2
            // 
            button2.BackColor = Color.DarkGreen;
            button2.Font = new Font("Microsoft Sans Serif", 19.7999973F, FontStyle.Bold, GraphicsUnit.Point, 0);
            button2.ForeColor = Color.Transparent;
            button2.Location = new Point(1027, 784);
            button2.Name = "button2";
            button2.Size = new Size(178, 56);
            button2.TabIndex = 9;
            button2.Text = "สมัครสมาชิก";
            button2.UseVisualStyleBackColor = false;
            button2.Click += button2_Click;
            // 
            // Form1
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.DarkGreen;
            ClientSize = new Size(1920, 1055);
            Controls.Add(button2);
            Controls.Add(button1);
            Controls.Add(txtPassword);
            Controls.Add(txtUsername);
            Controls.Add(Password);
            Controls.Add(Username);
            Controls.Add(label3);
            Controls.Add(dateTimePicker1);
            Controls.Add(label2);
            Controls.Add(label1);
            Name = "Form1";
            Text = "Form1";
            Load += Form1_Load;
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Label label1;
        private Label label2;
        private DateTimePicker dateTimePicker1;
        private Label label3;
        private Label Username;
        private Label Password;
        private TextBox txtUsername;
        private TextBox txtPassword;
        private Button button1;
        private Button button2;
    }
}
