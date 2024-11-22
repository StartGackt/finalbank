namespace Projectfinal
{
    partial class Mainborrowmoney
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Mainborrowmoney));
            button2 = new Button();
            button1 = new Button();
            label3 = new Label();
            dateTimePicker1 = new DateTimePicker();
            label2 = new Label();
            label1 = new Label();
            button3 = new Button();
            button4 = new Button();
            SuspendLayout();
            // 
            // button2
            // 
            button2.BackColor = Color.DarkGreen;
            button2.Font = new Font("TH Sarabun New", 19.7999973F, FontStyle.Bold, GraphicsUnit.Point);
            button2.ForeColor = Color.Transparent;
            button2.Location = new Point(528, 673);
            button2.Name = "button2";
            button2.Size = new Size(645, 105);
            button2.TabIndex = 30;
            button2.Text = "การกู้เงินฉุกเฉิน  ";
            button2.UseVisualStyleBackColor = false;
            button2.Click += button2_Click;
            // 
            // button1
            // 
            button1.BackColor = Color.DarkGreen;
            button1.Font = new Font("TH Sarabun New", 19.7999973F, FontStyle.Bold, GraphicsUnit.Point);
            button1.ForeColor = Color.Transparent;
            button1.Location = new Point(528, 381);
            button1.Name = "button1";
            button1.Size = new Size(645, 105);
            button1.TabIndex = 29;
            button1.Text = "การกู้เงินสามัญ";
            button1.UseVisualStyleBackColor = false;
            button1.Click += button1_Click;
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.Font = new Font("TH Sarabun New", 25.8F, FontStyle.Bold, GraphicsUnit.Point);
            label3.ForeColor = Color.Transparent;
            label3.Location = new Point(705, 267);
            label3.Name = "label3";
            label3.Size = new Size(298, 57);
            label3.TabIndex = 28;
            label3.Text = "ข้อมูลเกี่ยวกับการกู้เงิน";
            // 
            // dateTimePicker1
            // 
            dateTimePicker1.Location = new Point(1540, 111);
            dateTimePicker1.Name = "dateTimePicker1";
            dateTimePicker1.Size = new Size(250, 27);
            dateTimePicker1.TabIndex = 27;
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Font = new Font("TH Sarabun New", 25.8F, FontStyle.Bold, GraphicsUnit.Point);
            label2.ForeColor = Color.Gold;
            label2.Location = new Point(538, 168);
            label2.Name = "label2";
            label2.Size = new Size(635, 57);
            label2.TabIndex = 26;
            label2.Text = "ตำบลหนองยายโต๊ะ อำเภอชัยบาดาล จังหวัดลพบุรี ";
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Font = new Font("TH Sarabun New", 25.8F, FontStyle.Bold, GraphicsUnit.Point);
            label1.ForeColor = Color.Gold;
            label1.Location = new Point(492, 111);
            label1.Name = "label1";
            label1.Size = new Size(727, 57);
            label1.TabIndex = 25;
            label1.Text = "ระบบบริหารจัดการกลุ่มออมทรัพย์เพื่อการผลิตบ้านท่ารวก ";
            // 
            // button3
            // 
            button3.BackColor = Color.DarkGreen;
            button3.Font = new Font("TH Sarabun New", 19.7999973F, FontStyle.Bold, GraphicsUnit.Point);
            button3.ForeColor = Color.Transparent;
            button3.Location = new Point(528, 524);
            button3.Name = "button3";
            button3.Size = new Size(645, 105);
            button3.TabIndex = 31;
            button3.Text = "การกู้เงินสามัญ (แก้ไขข้อมูลผู้ค้ำประกัน)";
            button3.UseVisualStyleBackColor = false;
            button3.Click += button3_Click;
            // 
            // button4
            // 
            button4.BackColor = Color.DarkGreen;
            button4.Font = new Font("TH Sarabun New", 23.9999981F, FontStyle.Bold, GraphicsUnit.Point);
            button4.ForeColor = Color.Transparent;
            button4.Image = (Image)resources.GetObject("button4.Image");
            button4.Location = new Point(1811, 951);
            button4.Name = "button4";
            button4.Size = new Size(64, 47);
            button4.TabIndex = 219;
            button4.UseVisualStyleBackColor = false;
            button4.Click += button4_Click;
            // 
            // Mainborrowmoney
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.DarkGreen;
            ClientSize = new Size(1902, 1033);
            Controls.Add(button4);
            Controls.Add(button3);
            Controls.Add(button2);
            Controls.Add(button1);
            Controls.Add(label3);
            Controls.Add(dateTimePicker1);
            Controls.Add(label2);
            Controls.Add(label1);
            FormBorderStyle = FormBorderStyle.None;
            Name = "Mainborrowmoney";
            Text = "Mainborrowmoney";
            Load += Mainborrowmoney_Load;
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Button button2;
        private Button button1;
        private Label label3;
        private DateTimePicker dateTimePicker1;
        private Label label2;
        private Label label1;
        private Button button3;
        private Button button4;
    }
}