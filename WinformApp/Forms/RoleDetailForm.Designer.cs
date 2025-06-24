namespace WinformApp.Forms
{
    partial class RoleDetailForm
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
            saveButton = new Button();
            SuspendLayout();
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new Point(25, 36);
            label1.Name = "label1";
            label1.Size = new Size(43, 17);
            label1.TabIndex = 0;
            label1.Text = "Nama";
            // 
            // textBox1
            // 
            textBox1.Location = new Point(25, 56);
            textBox1.Name = "textBox1";
            textBox1.Size = new Size(280, 25);
            textBox1.TabIndex = 1;
            // 
            // saveButton
            // 
            saveButton.Location = new Point(205, 127);
            saveButton.Name = "saveButton";
            saveButton.Size = new Size(100, 30);
            saveButton.TabIndex = 2;
            saveButton.Text = "Simpan";
            saveButton.UseVisualStyleBackColor = true;
            saveButton.Click += HandleSaveButtonClicked;
            // 
            // RoleDetailForm
            // 
            AutoScaleDimensions = new SizeF(7F, 17F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(334, 181);
            Controls.Add(saveButton);
            Controls.Add(textBox1);
            Controls.Add(label1);
            Font = new Font("Segoe UI", 9.75F, FontStyle.Regular, GraphicsUnit.Point, 0);
            FormBorderStyle = FormBorderStyle.FixedDialog;
            MaximizeBox = false;
            MinimizeBox = false;
            Name = "RoleDetailForm";
            StartPosition = FormStartPosition.CenterParent;
            Text = "Role Detail";
            Load += HandleFormLoad;
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Label label1;
        private TextBox textBox1;
        private Button saveButton;
    }
}