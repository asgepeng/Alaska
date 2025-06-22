namespace WinformApp.Forms
{
    partial class RoleForm
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
            listView1 = new ListView();
            colRolename = new ColumnHeader();
            colCreator = new ColumnHeader();
            colCreatedDate = new ColumnHeader();
            button1 = new Button();
            deleteButton = new Button();
            SuspendLayout();
            // 
            // listView1
            // 
            listView1.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            listView1.Columns.AddRange(new ColumnHeader[] { colRolename, colCreator, colCreatedDate });
            listView1.FullRowSelect = true;
            listView1.Location = new Point(12, 12);
            listView1.MultiSelect = false;
            listView1.Name = "listView1";
            listView1.Size = new Size(660, 345);
            listView1.TabIndex = 104;
            listView1.UseCompatibleStateImageBehavior = false;
            listView1.View = View.Details;
            listView1.ItemActivate += HandleListViewItemActivated;
            listView1.SelectedIndexChanged += HandleListViewSelectedIndexChanged;
            // 
            // colRolename
            // 
            colRolename.Text = "Role";
            colRolename.Width = 150;
            // 
            // colCreator
            // 
            colCreator.Text = "Dibuat oleh";
            colCreator.Width = 150;
            // 
            // colCreatedDate
            // 
            colCreatedDate.Text = "Tgl. dibuat";
            colCreatedDate.Width = 120;
            // 
            // button1
            // 
            button1.Anchor = AnchorStyles.Bottom | AnchorStyles.Left;
            button1.ImageIndex = 3;
            button1.Location = new Point(12, 369);
            button1.Name = "button1";
            button1.Size = new Size(99, 30);
            button1.TabIndex = 105;
            button1.Text = "Baru";
            button1.TextImageRelation = TextImageRelation.ImageBeforeText;
            button1.UseVisualStyleBackColor = true;
            button1.Click += button1_Click;
            // 
            // deleteButton
            // 
            deleteButton.Anchor = AnchorStyles.Bottom | AnchorStyles.Left;
            deleteButton.Enabled = false;
            deleteButton.ImageIndex = 3;
            deleteButton.Location = new Point(117, 369);
            deleteButton.Name = "deleteButton";
            deleteButton.Size = new Size(99, 30);
            deleteButton.TabIndex = 106;
            deleteButton.Text = "Hapus";
            deleteButton.TextImageRelation = TextImageRelation.ImageBeforeText;
            deleteButton.UseVisualStyleBackColor = true;
            deleteButton.Click += HandleDeleteButtonClicked;
            // 
            // RoleForm
            // 
            AutoScaleDimensions = new SizeF(7F, 17F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(684, 411);
            Controls.Add(deleteButton);
            Controls.Add(button1);
            Controls.Add(listView1);
            Font = new Font("Segoe UI", 9.75F, FontStyle.Regular, GraphicsUnit.Point, 0);
            FormBorderStyle = FormBorderStyle.FixedDialog;
            MaximizeBox = false;
            MinimizeBox = false;
            Name = "RoleForm";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "Daftar Role";
            Load += HandleFormLoad;
            ResumeLayout(false);
        }

        #endregion

        private ListView listView1;
        private ColumnHeader colRolename;
        private ColumnHeader colCreator;
        private ColumnHeader colCreatedDate;
        private Button button1;
        private Button deleteButton;
    }
}