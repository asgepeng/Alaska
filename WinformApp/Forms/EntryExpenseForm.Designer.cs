namespace WinformApp.Forms
{
    partial class EntryExpenseForm
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
            DataGridViewCellStyle dataGridViewCellStyle1 = new DataGridViewCellStyle();
            grid = new DataGridView();
            button1 = new Button();
            SaveButton = new Button();
            button2 = new Button();
            label1 = new Label();
            label2 = new Label();
            colName = new DataGridViewComboBoxColumn();
            colAmount = new DataGridViewTextBoxColumn();
            ((System.ComponentModel.ISupportInitialize)grid).BeginInit();
            SuspendLayout();
            // 
            // grid
            // 
            grid.AllowUserToAddRows = false;
            grid.AllowUserToDeleteRows = false;
            grid.AllowUserToResizeRows = false;
            grid.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            grid.BackgroundColor = Color.White;
            grid.BorderStyle = BorderStyle.Fixed3D;
            grid.CellBorderStyle = DataGridViewCellBorderStyle.None;
            grid.ColumnHeadersBorderStyle = DataGridViewHeaderBorderStyle.Single;
            grid.ColumnHeadersHeight = 26;
            grid.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.DisableResizing;
            grid.Columns.AddRange(new DataGridViewColumn[] { colName, colAmount });
            grid.Location = new Point(12, 14);
            grid.Name = "grid";
            grid.RowHeadersVisible = false;
            grid.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            grid.Size = new Size(519, 183);
            grid.TabIndex = 6;
            grid.CellEndEdit += grid_CellEndEdit;
            grid.DataError += grid_DataError;
            // 
            // button1
            // 
            button1.BackColor = Color.Red;
            button1.FlatAppearance.BorderSize = 0;
            button1.FlatStyle = FlatStyle.Flat;
            button1.ForeColor = Color.White;
            button1.Location = new Point(54, 223);
            button1.Name = "button1";
            button1.Size = new Size(36, 32);
            button1.TabIndex = 9;
            button1.Text = "x";
            button1.UseVisualStyleBackColor = false;
            button1.Click += button1_Click;
            // 
            // SaveButton
            // 
            SaveButton.BackColor = SystemColors.HotTrack;
            SaveButton.FlatAppearance.BorderSize = 0;
            SaveButton.FlatStyle = FlatStyle.Flat;
            SaveButton.ForeColor = Color.White;
            SaveButton.Location = new Point(96, 223);
            SaveButton.Name = "SaveButton";
            SaveButton.Size = new Size(102, 32);
            SaveButton.TabIndex = 8;
            SaveButton.Text = "Simpan";
            SaveButton.UseVisualStyleBackColor = false;
            SaveButton.Click += SaveButton_Click;
            // 
            // button2
            // 
            button2.BackColor = SystemColors.HotTrack;
            button2.FlatAppearance.BorderSize = 0;
            button2.FlatStyle = FlatStyle.Flat;
            button2.ForeColor = Color.White;
            button2.Location = new Point(12, 223);
            button2.Name = "button2";
            button2.Size = new Size(36, 32);
            button2.TabIndex = 10;
            button2.Text = "+";
            button2.UseVisualStyleBackColor = false;
            button2.Click += button2_Click;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new Point(394, 206);
            label1.Name = "label1";
            label1.Size = new Size(36, 17);
            label1.TabIndex = 11;
            label1.Text = "Total";
            // 
            // label2
            // 
            label2.BackColor = Color.White;
            label2.BorderStyle = BorderStyle.FixedSingle;
            label2.Font = new Font("Segoe UI Semibold", 12F, FontStyle.Bold, GraphicsUnit.Point, 0);
            label2.Location = new Point(394, 223);
            label2.Name = "label2";
            label2.Size = new Size(137, 32);
            label2.TabIndex = 12;
            label2.Text = "0";
            label2.TextAlign = ContentAlignment.MiddleRight;
            // 
            // colName
            // 
            colName.DataPropertyName = "expenseId";
            colName.DisplayStyle = DataGridViewComboBoxDisplayStyle.Nothing;
            colName.FlatStyle = FlatStyle.Flat;
            colName.HeaderText = "Kategori";
            colName.Name = "colName";
            colName.Resizable = DataGridViewTriState.True;
            colName.SortMode = DataGridViewColumnSortMode.Automatic;
            colName.Width = 300;
            // 
            // colAmount
            // 
            colAmount.DataPropertyName = "amount";
            dataGridViewCellStyle1.Alignment = DataGridViewContentAlignment.MiddleRight;
            dataGridViewCellStyle1.Format = "N0";
            colAmount.DefaultCellStyle = dataGridViewCellStyle1;
            colAmount.HeaderText = "Nilai";
            colAmount.Name = "colAmount";
            colAmount.Width = 120;
            // 
            // EntryExpenseForm
            // 
            AutoScaleDimensions = new SizeF(7F, 17F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(543, 266);
            Controls.Add(label2);
            Controls.Add(label1);
            Controls.Add(button2);
            Controls.Add(button1);
            Controls.Add(SaveButton);
            Controls.Add(grid);
            Font = new Font("Segoe UI", 9.75F, FontStyle.Regular, GraphicsUnit.Point, 0);
            FormBorderStyle = FormBorderStyle.SizableToolWindow;
            Name = "EntryExpenseForm";
            StartPosition = FormStartPosition.CenterParent;
            Text = "Input Pengeluaran";
            Load += EntryExpenseForm_Load;
            ((System.ComponentModel.ISupportInitialize)grid).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private DataGridView grid;
        private Button button1;
        private Button SaveButton;
        private Button button2;
        private Label label1;
        private Label label2;
        private DataGridViewComboBoxColumn colName;
        private DataGridViewTextBoxColumn colAmount;
    }
}