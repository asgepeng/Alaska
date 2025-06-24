namespace WinformApp.Forms
{
    partial class ListingForm
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
            grid = new DataGridView();
            panel1 = new Panel();
            tableLayoutPanel1 = new TableLayoutPanel();
            label8 = new Label();
            label7 = new Label();
            label6 = new Label();
            label5 = new Label();
            label4 = new Label();
            label3 = new Label();
            label2 = new Label();
            dateTimePicker2 = new DateTimePicker();
            dateTimePicker1 = new DateTimePicker();
            label1 = new Label();
            button1 = new Button();
            ((System.ComponentModel.ISupportInitialize)grid).BeginInit();
            panel1.SuspendLayout();
            tableLayoutPanel1.SuspendLayout();
            SuspendLayout();
            // 
            // grid
            // 
            grid.AllowUserToAddRows = false;
            grid.AllowUserToDeleteRows = false;
            grid.AllowUserToResizeRows = false;
            grid.BackgroundColor = Color.White;
            grid.BorderStyle = BorderStyle.None;
            grid.CellBorderStyle = DataGridViewCellBorderStyle.None;
            grid.ColumnHeadersBorderStyle = DataGridViewHeaderBorderStyle.Single;
            grid.ColumnHeadersHeight = 26;
            grid.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.DisableResizing;
            grid.Dock = DockStyle.Fill;
            grid.Location = new Point(0, 57);
            grid.Name = "grid";
            grid.ReadOnly = true;
            grid.RowHeadersVisible = false;
            grid.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            grid.Size = new Size(800, 453);
            grid.TabIndex = 4;
            grid.CellDoubleClick += HandleCellDoubleClick;
            grid.DataBindingComplete += HandleDataBindingComplete;
            // 
            // panel1
            // 
            panel1.BackColor = Color.SteelBlue;
            panel1.Controls.Add(button1);
            panel1.Controls.Add(tableLayoutPanel1);
            panel1.Controls.Add(label2);
            panel1.Controls.Add(dateTimePicker2);
            panel1.Controls.Add(dateTimePicker1);
            panel1.Controls.Add(label1);
            panel1.Dock = DockStyle.Top;
            panel1.Location = new Point(0, 0);
            panel1.Name = "panel1";
            panel1.Size = new Size(800, 57);
            panel1.TabIndex = 5;
            panel1.Visible = false;
            // 
            // tableLayoutPanel1
            // 
            tableLayoutPanel1.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            tableLayoutPanel1.ColumnCount = 3;
            tableLayoutPanel1.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 33.3333321F));
            tableLayoutPanel1.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 33.3333321F));
            tableLayoutPanel1.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 33.3333321F));
            tableLayoutPanel1.Controls.Add(label8, 0, 1);
            tableLayoutPanel1.Controls.Add(label7, 0, 1);
            tableLayoutPanel1.Controls.Add(label6, 0, 1);
            tableLayoutPanel1.Controls.Add(label5, 2, 0);
            tableLayoutPanel1.Controls.Add(label4, 1, 0);
            tableLayoutPanel1.Controls.Add(label3, 0, 0);
            tableLayoutPanel1.Location = new Point(449, 10);
            tableLayoutPanel1.Name = "tableLayoutPanel1";
            tableLayoutPanel1.RowCount = 2;
            tableLayoutPanel1.RowStyles.Add(new RowStyle(SizeType.Percent, 50F));
            tableLayoutPanel1.RowStyles.Add(new RowStyle(SizeType.Percent, 50F));
            tableLayoutPanel1.Size = new Size(348, 35);
            tableLayoutPanel1.TabIndex = 5;
            // 
            // label8
            // 
            label8.AutoSize = true;
            label8.Font = new Font("Segoe UI Semibold", 12F, FontStyle.Bold);
            label8.ForeColor = Color.White;
            label8.Location = new Point(3, 17);
            label8.Name = "label8";
            label8.Size = new Size(19, 18);
            label8.TabIndex = 9;
            label8.Text = "0";
            // 
            // label7
            // 
            label7.AutoSize = true;
            label7.Font = new Font("Segoe UI Semibold", 12F, FontStyle.Bold);
            label7.ForeColor = Color.White;
            label7.Location = new Point(119, 17);
            label7.Name = "label7";
            label7.Size = new Size(19, 18);
            label7.TabIndex = 8;
            label7.Text = "0";
            // 
            // label6
            // 
            label6.AutoSize = true;
            label6.Font = new Font("Segoe UI Semibold", 12F, FontStyle.Bold);
            label6.ForeColor = Color.White;
            label6.Location = new Point(235, 17);
            label6.Name = "label6";
            label6.Size = new Size(19, 18);
            label6.TabIndex = 7;
            label6.Text = "0";
            // 
            // label5
            // 
            label5.AutoSize = true;
            label5.ForeColor = SystemColors.GradientInactiveCaption;
            label5.Location = new Point(235, 0);
            label5.Name = "label5";
            label5.Size = new Size(44, 17);
            label5.TabIndex = 6;
            label5.Text = "Selisih";
            // 
            // label4
            // 
            label4.AutoSize = true;
            label4.ForeColor = SystemColors.GradientInactiveCaption;
            label4.Location = new Point(119, 0);
            label4.Name = "label4";
            label4.Size = new Size(80, 17);
            label4.TabIndex = 5;
            label4.Text = "Pengeluaran";
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.ForeColor = SystemColors.GradientInactiveCaption;
            label3.Location = new Point(3, 0);
            label3.Name = "label3";
            label3.Size = new Size(73, 17);
            label3.TabIndex = 4;
            label3.Text = "Pemasukan";
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.ForeColor = Color.FromArgb(244, 244, 244);
            label2.Location = new Point(99, 21);
            label2.Name = "label2";
            label2.Size = new Size(30, 17);
            label2.TabIndex = 3;
            label2.Text = "s/d.";
            // 
            // dateTimePicker2
            // 
            dateTimePicker2.CustomFormat = "dd/MM-yyyy";
            dateTimePicker2.Format = DateTimePickerFormat.Custom;
            dateTimePicker2.Location = new Point(135, 20);
            dateTimePicker2.Name = "dateTimePicker2";
            dateTimePicker2.Size = new Size(90, 25);
            dateTimePicker2.TabIndex = 2;
            // 
            // dateTimePicker1
            // 
            dateTimePicker1.CustomFormat = "dd/MM-yyyy";
            dateTimePicker1.Format = DateTimePickerFormat.Custom;
            dateTimePicker1.Location = new Point(3, 20);
            dateTimePicker1.Name = "dateTimePicker1";
            dateTimePicker1.Size = new Size(90, 25);
            dateTimePicker1.TabIndex = 1;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.ForeColor = Color.FromArgb(244, 244, 244);
            label1.Location = new Point(3, 0);
            label1.Name = "label1";
            label1.Size = new Size(53, 17);
            label1.TabIndex = 0;
            label1.Text = "Periode";
            // 
            // button1
            // 
            button1.Location = new Point(231, 20);
            button1.Name = "button1";
            button1.Size = new Size(109, 25);
            button1.TabIndex = 6;
            button1.Text = "Download";
            button1.UseVisualStyleBackColor = true;
            button1.Click += button1_Click;
            // 
            // ListingForm
            // 
            AutoScaleDimensions = new SizeF(7F, 17F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(800, 510);
            Controls.Add(grid);
            Controls.Add(panel1);
            Font = new Font("Segoe UI", 9.75F, FontStyle.Regular, GraphicsUnit.Point, 0);
            Name = "ListingForm";
            Text = "ListingForm";
            Load += HandleFormLoad;
            ((System.ComponentModel.ISupportInitialize)grid).EndInit();
            panel1.ResumeLayout(false);
            panel1.PerformLayout();
            tableLayoutPanel1.ResumeLayout(false);
            tableLayoutPanel1.PerformLayout();
            ResumeLayout(false);
        }

        #endregion

        private DataGridView grid;
        private Panel panel1;
        private Label label2;
        private DateTimePicker dateTimePicker2;
        private DateTimePicker dateTimePicker1;
        private Label label1;
        private Label label3;
        private TableLayoutPanel tableLayoutPanel1;
        private Label label8;
        private Label label7;
        private Label label6;
        private Label label5;
        private Label label4;
        private Button button1;
    }
}