namespace WinformApp.Forms
{
    partial class CategoryDetailForm
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
            button1 = new Button();
            SuspendLayout();
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new Point(12, 47);
            label1.Name = "label1";
            label1.Size = new Size(43, 17);
            label1.TabIndex = 0;
            label1.Text = "Nama";
            // 
            // textBox1
            // 
            nameTextBox.Location = new Point(12, 67);
            nameTextBox.Name = "textBox1";
            nameTextBox.Size = new Size(310, 25);
            nameTextBox.TabIndex = 1;
            // 
            // button1
            // 
            button1.Location = new Point(222, 123);
            button1.Name = "button1";
            button1.Size = new Size(100, 30);
            button1.TabIndex = 7;
            button1.Text = "Simpan";
            button1.UseVisualStyleBackColor = true;
            button1.Click += HandleSaveButtonClicked;
            // 
            // CategoryDetailForm
            // 
            AutoScaleDimensions = new SizeF(7F, 17F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(334, 191);
            Controls.Add(button1);
            Controls.Add(nameTextBox);
            Controls.Add(label1);
            Font = new Font("Segoe UI", 9.75F, FontStyle.Regular, GraphicsUnit.Point, 0);
            FormBorderStyle = FormBorderStyle.FixedToolWindow;
            MaximizeBox = false;
            MinimizeBox = false;
            Name = "CategoryDetailForm";
            StartPosition = FormStartPosition.CenterParent;
            Text = "Kategori Produk";
            Load += HandleFormLoad;
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Label label1;
        private TextBox nameTextBox;
        private Button button1;
    }
}