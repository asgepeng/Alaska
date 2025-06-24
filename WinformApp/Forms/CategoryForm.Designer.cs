namespace WinformApp.Forms
{
    partial class CategoryForm
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
            button1 = new Button();
            button2 = new Button();
            colName = new DataGridViewTextBoxColumn();
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
            grid.Columns.AddRange(new DataGridViewColumn[] { colName });
            grid.Location = new Point(12, 12);
            grid.Name = "grid";
            grid.ReadOnly = true;
            grid.RowHeadersVisible = false;
            grid.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            grid.Size = new Size(310, 446);
            grid.TabIndex = 5;
            // 
            // button1
            // 
            button1.Location = new Point(12, 469);
            button1.Name = "button1";
            button1.Size = new Size(30, 30);
            button1.TabIndex = 6;
            button1.Text = "+";
            button1.UseVisualStyleBackColor = true;
            button1.Click += button1_Click;
            // 
            // button2
            // 
            button2.Location = new Point(48, 469);
            button2.Name = "button2";
            button2.Size = new Size(30, 30);
            button2.TabIndex = 7;
            button2.Text = "x";
            button2.UseVisualStyleBackColor = true;
            button2.Click += button2_Click;
            // 
            // colName
            // 
            colName.DataPropertyName = "name";
            colName.HeaderText = "Nama";
            colName.Name = "colName";
            colName.ReadOnly = true;
            colName.Width = 200;
            // 
            // CategoryForm
            // 
            AutoScaleDimensions = new SizeF(7F, 17F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(334, 511);
            Controls.Add(button2);
            Controls.Add(button1);
            Controls.Add(grid);
            Font = new Font("Segoe UI", 9.75F, FontStyle.Regular, GraphicsUnit.Point, 0);
            FormBorderStyle = FormBorderStyle.FixedToolWindow;
            MaximizeBox = false;
            MinimizeBox = false;
            Name = "CategoryForm";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "Kategori Produk";
            Load += CategoryForm_Load;
            ((System.ComponentModel.ISupportInitialize)grid).EndInit();
            ResumeLayout(false);
        }

        #endregion

        private DataGridView grid;
        private Button button1;
        private Button button2;
        private DataGridViewTextBoxColumn colName;
    }
}