namespace WinformApp.Forms
{
    partial class CostCategoryForm
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
            textBox1 = new TextBox();
            label1 = new Label();
            comboBox1 = new ComboBox();
            label2 = new Label();
            button1 = new Button();
            SaveButton = new Button();
            SuspendLayout();
            // 
            // textBox1
            // 
            textBox1.Location = new Point(12, 55);
            textBox1.Name = "textBox1";
            textBox1.Size = new Size(310, 25);
            textBox1.TabIndex = 0;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new Point(12, 35);
            label1.Name = "label1";
            label1.Size = new Size(43, 17);
            label1.TabIndex = 1;
            label1.Text = "Nama";
            // 
            // comboBox1
            // 
            comboBox1.DropDownStyle = ComboBoxStyle.DropDownList;
            comboBox1.FormattingEnabled = true;
            comboBox1.Items.AddRange(new object[] { "Pemasukan", "Pengeluaran" });
            comboBox1.Location = new Point(12, 103);
            comboBox1.Name = "comboBox1";
            comboBox1.Size = new Size(310, 25);
            comboBox1.TabIndex = 2;
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Location = new Point(12, 83);
            label2.Name = "label2";
            label2.Size = new Size(33, 17);
            label2.TabIndex = 3;
            label2.Text = "Tipe";
            // 
            // button1
            // 
            button1.BackColor = Color.Red;
            button1.FlatAppearance.BorderSize = 0;
            button1.FlatStyle = FlatStyle.Flat;
            button1.ForeColor = Color.White;
            button1.Location = new Point(121, 163);
            button1.Name = "button1";
            button1.Size = new Size(93, 36);
            button1.TabIndex = 7;
            button1.Text = "Batal";
            button1.UseVisualStyleBackColor = false;
            button1.Click += button1_Click;
            // 
            // SaveButton
            // 
            SaveButton.BackColor = SystemColors.HotTrack;
            SaveButton.FlatAppearance.BorderSize = 0;
            SaveButton.FlatStyle = FlatStyle.Flat;
            SaveButton.ForeColor = Color.White;
            SaveButton.Location = new Point(220, 163);
            SaveButton.Name = "SaveButton";
            SaveButton.Size = new Size(102, 36);
            SaveButton.TabIndex = 6;
            SaveButton.Text = "Simpan";
            SaveButton.UseVisualStyleBackColor = false;
            SaveButton.Click += SaveButton_Click;
            // 
            // CostCategoryForm
            // 
            AutoScaleDimensions = new SizeF(7F, 17F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(334, 211);
            Controls.Add(button1);
            Controls.Add(SaveButton);
            Controls.Add(label2);
            Controls.Add(comboBox1);
            Controls.Add(label1);
            Controls.Add(textBox1);
            Font = new Font("Segoe UI", 9.75F, FontStyle.Regular, GraphicsUnit.Point, 0);
            FormBorderStyle = FormBorderStyle.FixedDialog;
            MaximizeBox = false;
            MinimizeBox = false;
            Name = "CostCategoryForm";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "Tipe Biaya";
            Load += CostCategoryForm_Load;
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private TextBox textBox1;
        private Label label1;
        private ComboBox comboBox1;
        private Label label2;
        private Button button1;
        private Button SaveButton;
    }
}