namespace WinformApp.Forms
{
    partial class UserForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(UserForm));
            button1 = new Button();
            listView1 = new ListView();
            colUsername = new ColumnHeader();
            colRolename = new ColumnHeader();
            colCreator = new ColumnHeader();
            colCreatedDate = new ColumnHeader();
            il = new ImageList(components);
            deleteButton = new Button();
            SuspendLayout();
            // 
            // button1
            // 
            button1.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            button1.ImageIndex = 3;
            button1.Location = new Point(12, 369);
            button1.Name = "button1";
            button1.Size = new Size(100, 30);
            button1.TabIndex = 102;
            button1.Text = "Baru";
            button1.TextImageRelation = TextImageRelation.ImageBeforeText;
            button1.UseVisualStyleBackColor = true;
            button1.Click += HandleNewButtonClicked;
            // 
            // listView1
            // 
            listView1.Columns.AddRange(new ColumnHeader[] { colUsername, colRolename, colCreator, colCreatedDate });
            listView1.FullRowSelect = true;
            listView1.Location = new Point(12, 12);
            listView1.MultiSelect = false;
            listView1.Name = "listView1";
            listView1.Size = new Size(760, 351);
            listView1.SmallImageList = il;
            listView1.TabIndex = 103;
            listView1.UseCompatibleStateImageBehavior = false;
            listView1.View = View.Details;
            listView1.ItemActivate += listView1_ItemActivate;
            listView1.SelectedIndexChanged += HandleListViewSelectedIndexChanged;
            // 
            // colUsername
            // 
            colUsername.Text = "Nama Pengguna";
            colUsername.Width = 200;
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
            // il
            // 
            il.ColorDepth = ColorDepth.Depth32Bit;
            il.ImageStream = (ImageListStreamer)resources.GetObject("il.ImageStream");
            il.TransparentColor = Color.Transparent;
            il.Images.SetKeyName(0, "user.png");
            // 
            // deleteButton
            // 
            deleteButton.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            deleteButton.Enabled = false;
            deleteButton.ImageIndex = 3;
            deleteButton.Location = new Point(118, 369);
            deleteButton.Name = "deleteButton";
            deleteButton.Size = new Size(100, 30);
            deleteButton.TabIndex = 104;
            deleteButton.Text = "Hapus";
            deleteButton.TextImageRelation = TextImageRelation.ImageBeforeText;
            deleteButton.UseVisualStyleBackColor = true;
            deleteButton.Click += HandleDeletedButtonClicked;
            // 
            // UserForm
            // 
            AutoScaleDimensions = new SizeF(7F, 17F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(784, 411);
            Controls.Add(deleteButton);
            Controls.Add(listView1);
            Controls.Add(button1);
            Font = new Font("Segoe UI", 9.75F, FontStyle.Regular, GraphicsUnit.Point, 0);
            FormBorderStyle = FormBorderStyle.FixedDialog;
            MaximizeBox = false;
            MinimizeBox = false;
            Name = "UserForm";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "Daftar User";
            Load += HandleFormLoad;
            ResumeLayout(false);
        }

        #endregion

        private Button button1;
        private ListView listView1;
        private ColumnHeader colUsername;
        private ColumnHeader colRolename;
        private ColumnHeader colCreator;
        private ColumnHeader colCreatedDate;
        private ImageList il;
        private Button deleteButton;
    }
}