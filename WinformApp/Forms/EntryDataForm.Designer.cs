namespace WinformApp.Forms
{
    partial class EntryDataForm
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
            components = new System.ComponentModel.Container();
            DataGridViewCellStyle dataGridViewCellStyle1 = new DataGridViewCellStyle();
            DataGridViewCellStyle dataGridViewCellStyle2 = new DataGridViewCellStyle();
            DataGridViewCellStyle dataGridViewCellStyle3 = new DataGridViewCellStyle();
            DataGridViewCellStyle dataGridViewCellStyle4 = new DataGridViewCellStyle();
            label1 = new Label();
            dataGridView1 = new DataGridView();
            outletIdDataGridViewTextBoxColumn = new DataGridViewTextBoxColumn();
            outletNameDataGridViewTextBoxColumn = new DataGridViewTextBoxColumn();
            OutletType = new DataGridViewTextBoxColumn();
            waiterNameDataGridViewTextBoxColumn = new DataGridViewTextBoxColumn();
            notesDataGridViewTextBoxColumn = new DataGridViewTextBoxColumn();
            incomeDataGridViewTextBoxColumn = new DataGridViewTextBoxColumn();
            dailySalesItemBindingSource = new BindingSource(components);
            incomeLabel = new Label();
            expenseLabel = new Label();
            balanceLabel = new Label();
            label5 = new Label();
            label6 = new Label();
            label7 = new Label();
            button1 = new Button();
            button2 = new Button();
            pictureBox1 = new PictureBox();
            notesTextBox = new TextBox();
            label2 = new Label();
            dateTimePicker1 = new DateTimePicker();
            button3 = new Button();
            ((System.ComponentModel.ISupportInitialize)dataGridView1).BeginInit();
            ((System.ComponentModel.ISupportInitialize)dailySalesItemBindingSource).BeginInit();
            ((System.ComponentModel.ISupportInitialize)pictureBox1).BeginInit();
            SuspendLayout();
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Font = new Font("Segoe UI", 9.75F, FontStyle.Bold, GraphicsUnit.Point, 0);
            label1.Location = new Point(12, 18);
            label1.Name = "label1";
            label1.Size = new Size(57, 17);
            label1.TabIndex = 0;
            label1.Text = "Tanggal";
            // 
            // dataGridView1
            // 
            dataGridView1.AllowUserToAddRows = false;
            dataGridView1.AllowUserToDeleteRows = false;
            dataGridView1.AllowUserToResizeRows = false;
            dataGridView1.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            dataGridView1.AutoGenerateColumns = false;
            dataGridView1.BackgroundColor = Color.White;
            dataGridView1.BorderStyle = BorderStyle.Fixed3D;
            dataGridView1.CellBorderStyle = DataGridViewCellBorderStyle.None;
            dataGridView1.ColumnHeadersBorderStyle = DataGridViewHeaderBorderStyle.Single;
            dataGridView1.ColumnHeadersHeight = 26;
            dataGridView1.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.DisableResizing;
            dataGridView1.Columns.AddRange(new DataGridViewColumn[] { outletIdDataGridViewTextBoxColumn, outletNameDataGridViewTextBoxColumn, OutletType, waiterNameDataGridViewTextBoxColumn, notesDataGridViewTextBoxColumn, incomeDataGridViewTextBoxColumn });
            dataGridView1.DataSource = dailySalesItemBindingSource;
            dataGridView1.Location = new Point(12, 95);
            dataGridView1.MultiSelect = false;
            dataGridView1.Name = "dataGridView1";
            dataGridView1.RowHeadersVisible = false;
            dataGridView1.RowTemplate.Height = 26;
            dataGridView1.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dataGridView1.Size = new Size(1160, 336);
            dataGridView1.TabIndex = 1;
            dataGridView1.CellEndEdit += HandleDataGridCellEndEdit;
            dataGridView1.DataBindingComplete += HandleDataBindingComplete;
            dataGridView1.DataError += dataGridView1_DataError;
            // 
            // outletIdDataGridViewTextBoxColumn
            // 
            outletIdDataGridViewTextBoxColumn.DataPropertyName = "OutletId";
            dataGridViewCellStyle1.Alignment = DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle1.BackColor = Color.FromArgb(255, 255, 192);
            dataGridViewCellStyle1.Format = "000000";
            outletIdDataGridViewTextBoxColumn.DefaultCellStyle = dataGridViewCellStyle1;
            outletIdDataGridViewTextBoxColumn.HeaderText = "Kode";
            outletIdDataGridViewTextBoxColumn.Name = "outletIdDataGridViewTextBoxColumn";
            outletIdDataGridViewTextBoxColumn.ReadOnly = true;
            outletIdDataGridViewTextBoxColumn.Width = 60;
            // 
            // outletNameDataGridViewTextBoxColumn
            // 
            outletNameDataGridViewTextBoxColumn.DataPropertyName = "OutletName";
            dataGridViewCellStyle2.BackColor = SystemColors.ButtonFace;
            outletNameDataGridViewTextBoxColumn.DefaultCellStyle = dataGridViewCellStyle2;
            outletNameDataGridViewTextBoxColumn.HeaderText = "Nama Outlet";
            outletNameDataGridViewTextBoxColumn.Name = "outletNameDataGridViewTextBoxColumn";
            outletNameDataGridViewTextBoxColumn.ReadOnly = true;
            outletNameDataGridViewTextBoxColumn.Width = 200;
            // 
            // OutletType
            // 
            OutletType.DataPropertyName = "OutletType";
            OutletType.HeaderText = "Tipe Outlet";
            OutletType.Name = "OutletType";
            OutletType.ReadOnly = true;
            // 
            // waiterNameDataGridViewTextBoxColumn
            // 
            waiterNameDataGridViewTextBoxColumn.DataPropertyName = "WaiterName";
            dataGridViewCellStyle3.BackColor = SystemColors.ButtonFace;
            waiterNameDataGridViewTextBoxColumn.DefaultCellStyle = dataGridViewCellStyle3;
            waiterNameDataGridViewTextBoxColumn.HeaderText = "Nama Waiter";
            waiterNameDataGridViewTextBoxColumn.Name = "waiterNameDataGridViewTextBoxColumn";
            waiterNameDataGridViewTextBoxColumn.ReadOnly = true;
            waiterNameDataGridViewTextBoxColumn.Width = 200;
            // 
            // notesDataGridViewTextBoxColumn
            // 
            notesDataGridViewTextBoxColumn.DataPropertyName = "Notes";
            notesDataGridViewTextBoxColumn.HeaderText = "Catatan";
            notesDataGridViewTextBoxColumn.Name = "notesDataGridViewTextBoxColumn";
            notesDataGridViewTextBoxColumn.Width = 200;
            // 
            // incomeDataGridViewTextBoxColumn
            // 
            incomeDataGridViewTextBoxColumn.DataPropertyName = "Income";
            dataGridViewCellStyle4.Alignment = DataGridViewContentAlignment.MiddleRight;
            dataGridViewCellStyle4.Format = "N0";
            incomeDataGridViewTextBoxColumn.DefaultCellStyle = dataGridViewCellStyle4;
            incomeDataGridViewTextBoxColumn.HeaderText = "Pemasukan";
            incomeDataGridViewTextBoxColumn.Name = "incomeDataGridViewTextBoxColumn";
            // 
            // dailySalesItemBindingSource
            // 
            dailySalesItemBindingSource.DataSource = typeof(AlaskaLib.Models.DailySalesItem);
            // 
            // incomeLabel
            // 
            incomeLabel.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;
            incomeLabel.BorderStyle = BorderStyle.FixedSingle;
            incomeLabel.Font = new Font("Segoe UI Semibold", 12F, FontStyle.Bold);
            incomeLabel.ForeColor = SystemColors.ControlText;
            incomeLabel.Location = new Point(972, 449);
            incomeLabel.Name = "incomeLabel";
            incomeLabel.Size = new Size(200, 32);
            incomeLabel.TabIndex = 2;
            incomeLabel.Text = "0";
            incomeLabel.TextAlign = ContentAlignment.MiddleRight;
            // 
            // expenseLabel
            // 
            expenseLabel.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;
            expenseLabel.BorderStyle = BorderStyle.FixedSingle;
            expenseLabel.Cursor = Cursors.Hand;
            expenseLabel.Font = new Font("Segoe UI Semibold", 12F, FontStyle.Bold);
            expenseLabel.ForeColor = SystemColors.ControlText;
            expenseLabel.Location = new Point(1005, 482);
            expenseLabel.Name = "expenseLabel";
            expenseLabel.Size = new Size(167, 32);
            expenseLabel.TabIndex = 3;
            expenseLabel.Text = "0";
            expenseLabel.TextAlign = ContentAlignment.MiddleRight;
            // 
            // balanceLabel
            // 
            balanceLabel.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;
            balanceLabel.BorderStyle = BorderStyle.FixedSingle;
            balanceLabel.Font = new Font("Segoe UI Semibold", 12F, FontStyle.Bold);
            balanceLabel.Location = new Point(972, 515);
            balanceLabel.Name = "balanceLabel";
            balanceLabel.Size = new Size(200, 32);
            balanceLabel.TabIndex = 4;
            balanceLabel.Text = "0";
            balanceLabel.TextAlign = ContentAlignment.MiddleRight;
            // 
            // label5
            // 
            label5.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;
            label5.AutoSize = true;
            label5.Location = new Point(893, 454);
            label5.Name = "label5";
            label5.Size = new Size(73, 17);
            label5.TabIndex = 5;
            label5.Text = "Pemasukan";
            // 
            // label6
            // 
            label6.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;
            label6.AutoSize = true;
            label6.Location = new Point(886, 490);
            label6.Name = "label6";
            label6.Size = new Size(80, 17);
            label6.TabIndex = 6;
            label6.Text = "Pengeluaran";
            // 
            // label7
            // 
            label7.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;
            label7.AutoSize = true;
            label7.Location = new Point(922, 523);
            label7.Name = "label7";
            label7.Size = new Size(44, 17);
            label7.TabIndex = 7;
            label7.Text = "Selisih";
            // 
            // button1
            // 
            button1.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;
            button1.BackColor = SystemColors.HotTrack;
            button1.FlatAppearance.BorderSize = 0;
            button1.FlatStyle = FlatStyle.Flat;
            button1.Font = new Font("Segoe UI", 9.75F, FontStyle.Bold, GraphicsUnit.Point, 0);
            button1.ForeColor = Color.White;
            button1.Location = new Point(1056, 567);
            button1.Name = "button1";
            button1.Size = new Size(116, 32);
            button1.TabIndex = 9;
            button1.Text = "Simpan";
            button1.UseVisualStyleBackColor = false;
            button1.Click += HandleSaveButtonClicked;
            // 
            // button2
            // 
            button2.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;
            button2.BackColor = Color.Red;
            button2.FlatAppearance.BorderSize = 0;
            button2.FlatStyle = FlatStyle.Flat;
            button2.Font = new Font("Segoe UI", 9.75F, FontStyle.Bold, GraphicsUnit.Point, 0);
            button2.ForeColor = Color.White;
            button2.Location = new Point(934, 567);
            button2.Name = "button2";
            button2.Size = new Size(116, 32);
            button2.TabIndex = 10;
            button2.Text = "Tutup";
            button2.UseVisualStyleBackColor = false;
            // 
            // pictureBox1
            // 
            pictureBox1.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            pictureBox1.Image = Properties.Resources.transparentlogo;
            pictureBox1.Location = new Point(976, 12);
            pictureBox1.Name = "pictureBox1";
            pictureBox1.Size = new Size(196, 77);
            pictureBox1.SizeMode = PictureBoxSizeMode.Zoom;
            pictureBox1.TabIndex = 11;
            pictureBox1.TabStop = false;
            // 
            // notesTextBox
            // 
            notesTextBox.Location = new Point(105, 43);
            notesTextBox.Multiline = true;
            notesTextBox.Name = "notesTextBox";
            notesTextBox.Size = new Size(300, 46);
            notesTextBox.TabIndex = 13;
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Font = new Font("Segoe UI", 9.75F, FontStyle.Bold, GraphicsUnit.Point, 0);
            label2.Location = new Point(12, 46);
            label2.Name = "label2";
            label2.Size = new Size(78, 17);
            label2.TabIndex = 12;
            label2.Text = "Keterangan";
            // 
            // dateTimePicker1
            // 
            dateTimePicker1.CustomFormat = "dd/MM/yyyy";
            dateTimePicker1.Format = DateTimePickerFormat.Custom;
            dateTimePicker1.Location = new Point(105, 12);
            dateTimePicker1.Name = "dateTimePicker1";
            dateTimePicker1.Size = new Size(90, 25);
            dateTimePicker1.TabIndex = 14;
            // 
            // button3
            // 
            button3.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;
            button3.BackColor = SystemColors.HotTrack;
            button3.FlatAppearance.BorderSize = 0;
            button3.FlatStyle = FlatStyle.Flat;
            button3.Font = new Font("Segoe UI", 9.75F, FontStyle.Bold, GraphicsUnit.Point, 0);
            button3.ForeColor = Color.White;
            button3.Location = new Point(972, 482);
            button3.Name = "button3";
            button3.Size = new Size(32, 32);
            button3.TabIndex = 15;
            button3.Text = "...";
            button3.UseVisualStyleBackColor = false;
            button3.Click += HandleButtonExpenseClicked;
            // 
            // EntryDataForm
            // 
            AutoScaleDimensions = new SizeF(7F, 17F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(1184, 611);
            Controls.Add(button3);
            Controls.Add(dateTimePicker1);
            Controls.Add(notesTextBox);
            Controls.Add(label2);
            Controls.Add(pictureBox1);
            Controls.Add(button2);
            Controls.Add(button1);
            Controls.Add(label7);
            Controls.Add(label6);
            Controls.Add(label5);
            Controls.Add(balanceLabel);
            Controls.Add(expenseLabel);
            Controls.Add(incomeLabel);
            Controls.Add(dataGridView1);
            Controls.Add(label1);
            Font = new Font("Segoe UI", 9.75F, FontStyle.Regular, GraphicsUnit.Point, 0);
            Name = "EntryDataForm";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "Input Penjualan Harian";
            Load += HandleFormLoad;
            ((System.ComponentModel.ISupportInitialize)dataGridView1).EndInit();
            ((System.ComponentModel.ISupportInitialize)dailySalesItemBindingSource).EndInit();
            ((System.ComponentModel.ISupportInitialize)pictureBox1).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Label label1;
        private DataGridView dataGridView1;
        private BindingSource dailySalesItemBindingSource;
        private Label incomeLabel;
        private Label expenseLabel;
        private Label balanceLabel;
        private Label label5;
        private Label label6;
        private Label label7;
        private Button button1;
        private Button button2;
        private PictureBox pictureBox1;
        private TextBox notesTextBox;
        private Label label2;
        private DateTimePicker dateTimePicker1;
        private Button button3;
        private DataGridViewTextBoxColumn outletIdDataGridViewTextBoxColumn;
        private DataGridViewTextBoxColumn outletNameDataGridViewTextBoxColumn;
        private DataGridViewTextBoxColumn OutletType;
        private DataGridViewTextBoxColumn waiterNameDataGridViewTextBoxColumn;
        private DataGridViewTextBoxColumn notesDataGridViewTextBoxColumn;
        private DataGridViewTextBoxColumn incomeDataGridViewTextBoxColumn;
    }
}