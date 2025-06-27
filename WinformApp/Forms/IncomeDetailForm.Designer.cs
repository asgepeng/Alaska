namespace WinformApp.Forms
{
    partial class IncomeDetailForm
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
            amountTextBox = new TextBox();
            typeComboBox = new ComboBox();
            label2 = new Label();
            noteTextBox = new TextBox();
            label3 = new Label();
            label4 = new Label();
            categoryComboBox = new ComboBox();
            button1 = new Button();
            SaveButton = new Button();
            label5 = new Label();
            datePicker = new DateTimePicker();
            SuspendLayout();
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new Point(12, 105);
            label1.Name = "label1";
            label1.Size = new Size(34, 17);
            label1.TabIndex = 0;
            label1.Text = "Nilai";
            // 
            // amountTextBox
            // 
            amountTextBox.Location = new Point(12, 125);
            amountTextBox.Name = "amountTextBox";
            amountTextBox.Size = new Size(310, 25);
            amountTextBox.TabIndex = 2;
            amountTextBox.KeyPress += HandleAmountKeyPress;
            // 
            // typeComboBox
            // 
            typeComboBox.DropDownStyle = ComboBoxStyle.DropDownList;
            typeComboBox.FormattingEnabled = true;
            typeComboBox.Items.AddRange(new object[] { "Masuk", "Keluar" });
            typeComboBox.Location = new Point(12, 77);
            typeComboBox.Name = "typeComboBox";
            typeComboBox.Size = new Size(310, 25);
            typeComboBox.TabIndex = 1;
            typeComboBox.SelectedIndexChanged += typeComboBox_SelectedIndexChanged;
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Location = new Point(12, 57);
            label2.Name = "label2";
            label2.Size = new Size(35, 17);
            label2.TabIndex = 3;
            label2.Text = "Arah";
            // 
            // noteTextBox
            // 
            noteTextBox.Location = new Point(12, 221);
            noteTextBox.Multiline = true;
            noteTextBox.Name = "noteTextBox";
            noteTextBox.ScrollBars = ScrollBars.Both;
            noteTextBox.Size = new Size(310, 50);
            noteTextBox.TabIndex = 4;
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.Location = new Point(12, 201);
            label3.Name = "label3";
            label3.Size = new Size(75, 17);
            label3.TabIndex = 4;
            label3.Text = "Keterangan";
            // 
            // label4
            // 
            label4.AutoSize = true;
            label4.Location = new Point(12, 153);
            label4.Name = "label4";
            label4.Size = new Size(58, 17);
            label4.TabIndex = 7;
            label4.Text = "Kategori";
            // 
            // categoryComboBox
            // 
            categoryComboBox.DropDownStyle = ComboBoxStyle.DropDownList;
            categoryComboBox.FormattingEnabled = true;
            categoryComboBox.Location = new Point(12, 173);
            categoryComboBox.Name = "categoryComboBox";
            categoryComboBox.Size = new Size(310, 25);
            categoryComboBox.TabIndex = 3;
            // 
            // button1
            // 
            button1.BackColor = Color.Red;
            button1.FlatAppearance.BorderSize = 0;
            button1.FlatStyle = FlatStyle.Flat;
            button1.Font = new Font("Segoe UI", 9.75F, FontStyle.Bold);
            button1.ForeColor = Color.White;
            button1.Location = new Point(114, 287);
            button1.Name = "button1";
            button1.Size = new Size(100, 32);
            button1.TabIndex = 11;
            button1.Text = "Tutup";
            button1.UseVisualStyleBackColor = false;
            // 
            // SaveButton
            // 
            SaveButton.BackColor = SystemColors.HotTrack;
            SaveButton.FlatAppearance.BorderSize = 0;
            SaveButton.FlatStyle = FlatStyle.Flat;
            SaveButton.Font = new Font("Segoe UI", 9.75F, FontStyle.Bold);
            SaveButton.ForeColor = Color.White;
            SaveButton.Location = new Point(220, 287);
            SaveButton.Name = "SaveButton";
            SaveButton.Size = new Size(102, 32);
            SaveButton.TabIndex = 10;
            SaveButton.Text = "Simpan";
            SaveButton.UseVisualStyleBackColor = false;
            SaveButton.Click += SaveButton_Click;
            // 
            // label5
            // 
            label5.AutoSize = true;
            label5.Location = new Point(12, 9);
            label5.Name = "label5";
            label5.Size = new Size(54, 17);
            label5.TabIndex = 12;
            label5.Text = "Tanggal";
            // 
            // datePicker
            // 
            datePicker.CustomFormat = "dd/MM/yyyy HH:mm:ss";
            datePicker.Format = DateTimePickerFormat.Custom;
            datePicker.Location = new Point(12, 29);
            datePicker.Name = "datePicker";
            datePicker.Size = new Size(310, 25);
            datePicker.TabIndex = 0;
            // 
            // IncomeDetailForm
            // 
            AutoScaleDimensions = new SizeF(7F, 17F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(334, 331);
            Controls.Add(datePicker);
            Controls.Add(label5);
            Controls.Add(button1);
            Controls.Add(SaveButton);
            Controls.Add(label4);
            Controls.Add(categoryComboBox);
            Controls.Add(noteTextBox);
            Controls.Add(label3);
            Controls.Add(label2);
            Controls.Add(typeComboBox);
            Controls.Add(amountTextBox);
            Controls.Add(label1);
            Font = new Font("Segoe UI", 9.75F, FontStyle.Regular, GraphicsUnit.Point, 0);
            FormBorderStyle = FormBorderStyle.FixedDialog;
            MaximizeBox = false;
            MinimizeBox = false;
            Name = "IncomeDetailForm";
            StartPosition = FormStartPosition.CenterParent;
            Text = "Input Kas";
            Load += IncomeDetailForm_Load;
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Label label1;
        private TextBox amountTextBox;
        private ComboBox typeComboBox;
        private Label label2;
        private TextBox noteTextBox;
        private Label label3;
        private Label label4;
        private ComboBox categoryComboBox;
        private Button button1;
        private Button SaveButton;
        private Label label5;
        private DateTimePicker datePicker;
    }
}