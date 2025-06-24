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
            comboBox1 = new ComboBox();
            label4 = new Label();
            SuspendLayout();
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new Point(12, 19);
            label1.Name = "label1";
            label1.Size = new Size(43, 17);
            label1.TabIndex = 0;
            label1.Text = "Nama";
            // 
            // nameTextBox
            // 
            nameTextBox.Location = new Point(12, 39);
            nameTextBox.Name = "nameTextBox";
            nameTextBox.Size = new Size(310, 25);
            nameTextBox.TabIndex = 0;
            // 
            // addressTextBox
            // 
            addressTextBox.Location = new Point(12, 87);
            addressTextBox.Multiline = true;
            addressTextBox.Name = "addressTextBox";
            addressTextBox.ScrollBars = ScrollBars.Both;
            addressTextBox.Size = new Size(310, 51);
            addressTextBox.TabIndex = 1;
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Location = new Point(12, 67);
            label2.Name = "label2";
            label2.Size = new Size(44, 17);
            label2.TabIndex = 2;
            label2.Text = "Lokasi";
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.Location = new Point(12, 188);
            label3.Name = "label3";
            label3.Size = new Size(45, 17);
            label3.TabIndex = 4;
            label3.Text = "Waiter";
            // 
            // waiterComboBox
            // 
            waiterComboBox.DropDownStyle = ComboBoxStyle.DropDownList;
            waiterComboBox.FormattingEnabled = true;
            waiterComboBox.Location = new Point(12, 208);
            waiterComboBox.Name = "waiterComboBox";
            waiterComboBox.Size = new Size(310, 25);
            waiterComboBox.TabIndex = 3;
            // 
            // loginButton
            // 
            loginButton.BackColor = SystemColors.HotTrack;
            loginButton.FlatAppearance.BorderSize = 0;
            loginButton.FlatStyle = FlatStyle.Flat;
            loginButton.ForeColor = Color.White;
            loginButton.Location = new Point(12, 263);
            loginButton.Name = "loginButton";
            loginButton.Size = new Size(312, 36);
            loginButton.TabIndex = 4;
            loginButton.Text = "Simpan";
            loginButton.UseVisualStyleBackColor = false;
            loginButton.Click += HandleLoginButtonClick;
            // 
            // comboBox1
            // 
            comboBox1.DropDownStyle = ComboBoxStyle.DropDownList;
            comboBox1.FormattingEnabled = true;
            comboBox1.Items.AddRange(new object[] { "Internal", "Mitra" });
            comboBox1.Location = new Point(12, 161);
            comboBox1.Name = "comboBox1";
            comboBox1.Size = new Size(310, 25);
            comboBox1.TabIndex = 2;
            // 
            // label4
            // 
            label4.AutoSize = true;
            label4.Location = new Point(12, 141);
            label4.Name = "label4";
            label4.Size = new Size(35, 17);
            label4.TabIndex = 10;
            label4.Text = "Type";
            // 
            // OutletDetailForm
            // 
            AutoScaleDimensions = new SizeF(7F, 17F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(334, 311);
            Controls.Add(comboBox1);
            Controls.Add(label4);
            Controls.Add(loginButton);
            Controls.Add(waiterComboBox);
            Controls.Add(label3);
            Controls.Add(addressTextBox);
            Controls.Add(label2);
            Controls.Add(nameTextBox);
            Controls.Add(label1);
            Font = new Font("Segoe UI", 9.75F, FontStyle.Regular, GraphicsUnit.Point, 0);
            MaximizeBox = false;
            MinimizeBox = false;
            Name = "OutletDetailForm";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "Outlet";
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
        private ComboBox comboBox1;
        private Label label4;
    }
}