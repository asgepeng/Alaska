namespace WinformApp.Forms
{
    partial class ChangePasswordForm
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
            label3 = new Label();
            label1 = new Label();
            saveButton = new Button();
            oldPasswrodTextBox = new TextBox();
            label2 = new Label();
            newPasswordTextBox = new TextBox();
            label4 = new Label();
            confirmPasswordTextBox = new TextBox();
            pictureBox1 = new PictureBox();
            ((System.ComponentModel.ISupportInitialize)pictureBox1).BeginInit();
            SuspendLayout();
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.Location = new Point(28, 148);
            label3.Name = "label3";
            label3.Size = new Size(110, 17);
            label3.TabIndex = 12;
            label3.Text = "Password Saat ini";
            label3.TextAlign = ContentAlignment.MiddleLeft;
            // 
            // label1
            // 
            label1.Location = new Point(12, 345);
            label1.Name = "label1";
            label1.Size = new Size(300, 57);
            label1.TabIndex = 11;
            label1.Text = "Copyright (c) 2025 Havas media";
            label1.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // saveButton
            // 
            saveButton.BackColor = SystemColors.HotTrack;
            saveButton.FlatAppearance.BorderSize = 0;
            saveButton.FlatStyle = FlatStyle.Flat;
            saveButton.ForeColor = Color.White;
            saveButton.Location = new Point(28, 306);
            saveButton.Name = "saveButton";
            saveButton.Size = new Size(276, 36);
            saveButton.TabIndex = 3;
            saveButton.Text = "Ubah Password";
            saveButton.UseVisualStyleBackColor = false;
            saveButton.Click += HandleButtonClicked;
            // 
            // oldPasswrodTextBox
            // 
            oldPasswrodTextBox.Location = new Point(28, 168);
            oldPasswrodTextBox.Name = "oldPasswrodTextBox";
            oldPasswrodTextBox.PasswordChar = '*';
            oldPasswrodTextBox.Size = new Size(276, 25);
            oldPasswrodTextBox.TabIndex = 0;
            oldPasswrodTextBox.UseSystemPasswordChar = true;
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Location = new Point(28, 196);
            label2.Name = "label2";
            label2.Size = new Size(94, 17);
            label2.TabIndex = 14;
            label2.Text = "Password Baru";
            label2.TextAlign = ContentAlignment.MiddleLeft;
            // 
            // newPasswordTextBox
            // 
            newPasswordTextBox.Location = new Point(28, 216);
            newPasswordTextBox.Name = "newPasswordTextBox";
            newPasswordTextBox.PasswordChar = '*';
            newPasswordTextBox.Size = new Size(276, 25);
            newPasswordTextBox.TabIndex = 1;
            newPasswordTextBox.UseSystemPasswordChar = true;
            // 
            // label4
            // 
            label4.AutoSize = true;
            label4.Location = new Point(28, 244);
            label4.Name = "label4";
            label4.Size = new Size(160, 17);
            label4.TabIndex = 16;
            label4.Text = "Konfirmasi Password Baru";
            label4.TextAlign = ContentAlignment.MiddleLeft;
            // 
            // confirmPasswordTextBox
            // 
            confirmPasswordTextBox.Location = new Point(28, 264);
            confirmPasswordTextBox.Name = "confirmPasswordTextBox";
            confirmPasswordTextBox.PasswordChar = '*';
            confirmPasswordTextBox.Size = new Size(276, 25);
            confirmPasswordTextBox.TabIndex = 2;
            confirmPasswordTextBox.UseSystemPasswordChar = true;
            // 
            // pictureBox1
            // 
            pictureBox1.Image = Properties.Resources.password;
            pictureBox1.Location = new Point(28, 25);
            pictureBox1.Name = "pictureBox1";
            pictureBox1.Size = new Size(276, 103);
            pictureBox1.SizeMode = PictureBoxSizeMode.CenterImage;
            pictureBox1.TabIndex = 17;
            pictureBox1.TabStop = false;
            // 
            // ChangePasswordForm
            // 
            AutoScaleDimensions = new SizeF(7F, 17F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(334, 411);
            Controls.Add(pictureBox1);
            Controls.Add(label4);
            Controls.Add(confirmPasswordTextBox);
            Controls.Add(label2);
            Controls.Add(newPasswordTextBox);
            Controls.Add(label3);
            Controls.Add(label1);
            Controls.Add(saveButton);
            Controls.Add(oldPasswrodTextBox);
            Font = new Font("Segoe UI", 9.75F, FontStyle.Regular, GraphicsUnit.Point, 0);
            FormBorderStyle = FormBorderStyle.FixedDialog;
            MaximizeBox = false;
            MinimizeBox = false;
            Name = "ChangePasswordForm";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "Ubah Password";
            ((System.ComponentModel.ISupportInitialize)pictureBox1).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Label label3;
        private Label label1;
        private Button saveButton;
        private TextBox oldPasswrodTextBox;
        private Label label2;
        private TextBox newPasswordTextBox;
        private Label label4;
        private TextBox confirmPasswordTextBox;
        private PictureBox pictureBox1;
    }
}