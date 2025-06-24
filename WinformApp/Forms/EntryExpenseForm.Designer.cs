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
            colName = new DataGridViewComboBoxColumn();
            colAmount = new DataGridViewTextBoxColumn();
            button1 = new Button();
            SaveButton = new Button();
            button2 = new Button();
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
            grid.Location = new Point(12, 12);
            grid.Name = "grid";
            grid.RowHeadersVisible = false;
            grid.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            grid.Size = new Size(519, 171);
            grid.TabIndex = 6;
            grid.CellEndEdit += grid_CellEndEdit;
            grid.DataError += grid_DataError;
            // 
            // colName
            // 
            colName.DataPropertyName = "expenseId";
            colName.DisplayStyle = DataGridViewComboBoxDisplayStyle.Nothing;
            colName.FlatStyle = FlatStyle.Flat;
            colName.HeaderText = "Nama";
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
            // button1
            // 
            button1.BackColor = Color.Red;
            button1.FlatAppearance.BorderSize = 0;
            button1.FlatStyle = FlatStyle.Flat;
            button1.ForeColor = Color.White;
            button1.Location = new Point(54, 189);
            button1.Name = "button1";
            button1.Size = new Size(36, 36);
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
            SaveButton.Location = new Point(429, 189);
            SaveButton.Name = "SaveButton";
            SaveButton.Size = new Size(102, 36);
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
            button2.Location = new Point(12, 189);
            button2.Name = "button2";
            button2.Size = new Size(36, 36);
            button2.TabIndex = 10;
            button2.Text = "+";
            button2.UseVisualStyleBackColor = false;
            button2.Click += button2_Click;
            // 
            // EntryExpenseForm
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(543, 235);
            Controls.Add(button2);
            Controls.Add(button1);
            Controls.Add(SaveButton);
            Controls.Add(grid);
            FormBorderStyle = FormBorderStyle.SizableToolWindow;
            Name = "EntryExpenseForm";
            StartPosition = FormStartPosition.CenterParent;
            Text = "Input Pengeluaran";
            Load += EntryExpenseForm_Load;
            ((System.ComponentModel.ISupportInitialize)grid).EndInit();
            ResumeLayout(false);
        }

        #endregion

        private DataGridView grid;
        private Button button1;
        private Button SaveButton;
        private Button button2;
        private DataGridViewComboBoxColumn colName;
        private DataGridViewTextBoxColumn colAmount;
    }
}