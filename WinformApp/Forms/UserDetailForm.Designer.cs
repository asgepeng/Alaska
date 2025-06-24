namespace WinformApp.Forms
{
    partial class UserDetailForm
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
            label1 = new Label();
            textBox1 = new TextBox();
            textBox2 = new TextBox();
            label2 = new Label();
            comboBox1 = new ComboBox();
            label3 = new Label();
            textBox3 = new TextBox();
            label4 = new Label();
            SaveButton = new Button();
            button1 = new Button();
            pictureBox1 = new PictureBox();
            label5 = new Label();
            ((System.ComponentModel.ISupportInitialize)pictureBox1).BeginInit();
            SuspendLayout();
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new Point(22, 110);
            label1.Name = "label1";
            label1.Size = new Size(43, 17);
            label1.TabIndex = 0;
            label1.Text = "Nama";
            // 
            // textBox1
            // 
            textBox1.Location = new Point(22, 130);
            textBox1.Name = "textBox1";
            textBox1.Size = new Size(286, 25);
            textBox1.TabIndex = 0;
            // 
            // textBox2
            // 
            textBox2.Location = new Point(22, 226);
            textBox2.Name = "textBox2";
            textBox2.Size = new Size(286, 25);
            textBox2.TabIndex = 2;
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Location = new Point(22, 206);
            label2.Name = "label2";
            label2.Size = new Size(40, 17);
            label2.TabIndex = 2;
            label2.Text = "Login";
            // 
            // comboBox1
            // 
            comboBox1.DropDownStyle = ComboBoxStyle.DropDownList;
            comboBox1.FormattingEnabled = true;
            comboBox1.Location = new Point(22, 178);
            comboBox1.Name = "comboBox1";
            comboBox1.Size = new Size(286, 25);
            comboBox1.TabIndex = 1;
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.Location = new Point(22, 158);
            label3.Name = "label3";
            label3.Size = new Size(34, 17);
            label3.TabIndex = 5;
            label3.Text = "Role";
            // 
            // textBox3
            // 
            textBox3.Location = new Point(22, 274);
            textBox3.Name = "textBox3";
            textBox3.PasswordChar = '*';
            textBox3.Size = new Size(286, 25);
            textBox3.TabIndex = 3;
            textBox3.UseSystemPasswordChar = true;
            // 
            // label4
            // 
            label4.AutoSize = true;
            label4.Location = new Point(22, 254);
            label4.Name = "label4";
            label4.Size = new Size(64, 17);
            label4.TabIndex = 6;
            label4.Text = "Password";
            // 
            // SaveButton
            // 
            SaveButton.BackColor = SystemColors.HotTrack;
            SaveButton.FlatAppearance.BorderSize = 0;
            SaveButton.FlatStyle = FlatStyle.Flat;
            SaveButton.ForeColor = Color.White;
            SaveButton.Location = new Point(206, 352);
            SaveButton.Name = "SaveButton";
            SaveButton.Size = new Size(102, 36);
            SaveButton.TabIndex = 4;
            SaveButton.Text = "Simpan";
            SaveButton.UseVisualStyleBackColor = false;
            SaveButton.Click += loginButton_Click;
            // 
            // button1
            // 
            button1.BackColor = Color.Red;
            button1.FlatAppearance.BorderSize = 0;
            button1.FlatStyle = FlatStyle.Flat;
            button1.ForeColor = Color.White;
            button1.Location = new Point(22, 352);
            button1.Name = "button1";
            button1.Size = new Size(93, 36);
            button1.TabIndex = 5;
            button1.Text = "Batal";
            button1.UseVisualStyleBackColor = false;
            button1.Click += HandleCloseButtonClicked;
            // 
            // pictureBox1
            // 
            pictureBox1.Image = Properties.Resources.contact;
            pictureBox1.Location = new Point(22, 12);
            pictureBox1.Name = "pictureBox1";
            pictureBox1.Size = new Size(286, 81);
            pictureBox1.SizeMode = PictureBoxSizeMode.Zoom;
            pictureBox1.TabIndex = 10;
            pictureBox1.TabStop = false;
            // 
            // label5
            // 
            label5.AutoSize = true;
            label5.Font = new Font("Segoe UI", 9.75F, FontStyle.Italic, GraphicsUnit.Point, 0);
            label5.ForeColor = Color.Red;
            label5.Location = new Point(92, 254);
            label5.Name = "label5";
            label5.Size = new Size(192, 17);
            label5.TabIndex = 11;
            label5.Text = "isi jika ingin mengubah password";
            // 
            // UserDetailForm
            // 
            AutoScaleDimensions = new SizeF(7F, 17F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.White;
            ClientSize = new Size(334, 411);
            Controls.Add(label5);
            Controls.Add(pictureBox1);
            Controls.Add(button1);
            Controls.Add(SaveButton);
            Controls.Add(textBox3);
            Controls.Add(label4);
            Controls.Add(label3);
            Controls.Add(comboBox1);
            Controls.Add(textBox2);
            Controls.Add(label2);
            Controls.Add(textBox1);
            Controls.Add(label1);
            Font = new Font("Segoe UI", 9.75F, FontStyle.Regular, GraphicsUnit.Point, 0);
            FormBorderStyle = FormBorderStyle.FixedDialog;
            MaximizeBox = false;
            MinimizeBox = false;
            Name = "UserDetailForm";
            StartPosition = FormStartPosition.CenterParent;
            Text = "Edit Pengguna";
            Load += HandleFormLoad;
            ((System.ComponentModel.ISupportInitialize)pictureBox1).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Label label1;
        private TextBox textBox1;
        private TextBox textBox2;
        private Label label2;
        private ComboBox comboBox1;
        private Label label3;
        private TextBox textBox3;
        private Label label4;
        private Button SaveButton;
        private Button button1;
        private PictureBox pictureBox1;
        private Label label5;
    }
}