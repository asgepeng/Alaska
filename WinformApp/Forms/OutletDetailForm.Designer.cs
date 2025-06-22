namespace WinformApp.Forms
{
    partial class OutletDetailForm
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
            nameTextBox = new TextBox();
            addressTextBox = new TextBox();
            label2 = new Label();
            label3 = new Label();
            waiterComboBox = new ComboBox();
            loginButton = new Button();
            SuspendLayout();
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new Point(12, 9);
            label1.Name = "label1";
            label1.Size = new Size(43, 17);
            label1.TabIndex = 0;
            label1.Text = "Nama";
            // 
            // textBox1
            // 
            nameTextBox.Location = new Point(12, 29);
            nameTextBox.Name = "textBox1";
            nameTextBox.Size = new Size(310, 25);
            nameTextBox.TabIndex = 1;
            // 
            // textBox2
            // 
            addressTextBox.Location = new Point(12, 77);
            addressTextBox.Multiline = true;
            addressTextBox.Name = "textBox2";
            addressTextBox.ScrollBars = ScrollBars.Both;
            addressTextBox.Size = new Size(310, 51);
            addressTextBox.TabIndex = 3;
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Location = new Point(12, 57);
            label2.Name = "label2";
            label2.Size = new Size(44, 17);
            label2.TabIndex = 2;
            label2.Text = "Lokasi";
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.Location = new Point(12, 131);
            label3.Name = "label3";
            label3.Size = new Size(43, 17);
            label3.TabIndex = 4;
            label3.Text = "Nama";
            // 
            // comboBox1
            // 
            waiterComboBox.DropDownStyle = ComboBoxStyle.DropDownList;
            waiterComboBox.FormattingEnabled = true;
            waiterComboBox.Location = new Point(12, 151);
            waiterComboBox.Name = "comboBox1";
            waiterComboBox.Size = new Size(310, 25);
            waiterComboBox.TabIndex = 5;
            // 
            // loginButton
            // 
            loginButton.BackColor = SystemColors.HotTrack;
            loginButton.FlatAppearance.BorderSize = 0;
            loginButton.FlatStyle = FlatStyle.Flat;
            loginButton.ForeColor = Color.White;
            loginButton.Location = new Point(10, 205);
            loginButton.Name = "loginButton";
            loginButton.Size = new Size(312, 36);
            loginButton.TabIndex = 9;
            loginButton.Text = "Simpan";
            loginButton.UseVisualStyleBackColor = false;
            loginButton.Click += HandleLoginButtonClick;
            // 
            // OutletDetailForm
            // 
            AutoScaleDimensions = new SizeF(7F, 17F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(334, 253);
            Controls.Add(loginButton);
            Controls.Add(waiterComboBox);
            Controls.Add(label3);
            Controls.Add(addressTextBox);
            Controls.Add(label2);
            Controls.Add(nameTextBox);
            Controls.Add(label1);
            Font = new Font("Segoe UI", 9.75F, FontStyle.Regular, GraphicsUnit.Point, 0);
            Name = "OutletDetailForm";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "OutletForm";
            Load += FormLoadHandler;
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Label label1;
        private TextBox nameTextBox;
        private TextBox addressTextBox;
        private Label label2;
        private Label label3;
        private ComboBox waiterComboBox;
        private Button loginButton;
    }
}