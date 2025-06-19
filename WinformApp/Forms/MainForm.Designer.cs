namespace WinformApp
{
    partial class MainForm
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
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
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MainForm));
            menuStrip1 = new MenuStrip();
            fileToolStripMenuItem = new ToolStripMenuItem();
            newToolStripMenuItem = new ToolStripMenuItem();
            openToolStripMenuItem = new ToolStripMenuItem();
            toolStripSeparator = new ToolStripSeparator();
            saveToolStripMenuItem = new ToolStripMenuItem();
            toolStripSeparator2 = new ToolStripSeparator();
            exitToolStripMenuItem = new ToolStripMenuItem();
            editToolStripMenuItem = new ToolStripMenuItem();
            undoToolStripMenuItem = new ToolStripMenuItem();
            redoToolStripMenuItem = new ToolStripMenuItem();
            copyToolStripMenuItem = new ToolStripMenuItem();
            cutToolStripMenuItem = new ToolStripMenuItem();
            toolStripSeparator4 = new ToolStripSeparator();
            toolsToolStripMenuItem = new ToolStripMenuItem();
            customizeToolStripMenuItem = new ToolStripMenuItem();
            optionsToolStripMenuItem = new ToolStripMenuItem();
            toolsToolStripMenuItem1 = new ToolStripMenuItem();
            goToHomepageToolStripMenuItem = new ToolStripMenuItem();
            publishCloudflaredToolStripMenuItem = new ToolStripMenuItem();
            helpToolStripMenuItem = new ToolStripMenuItem();
            aboutToolStripMenuItem = new ToolStripMenuItem();
            m_StatusStrip = new StatusStrip();
            userLoginName = new ToolStripStatusLabel();
            toolStripStatusLabel1 = new ToolStripStatusLabel();
            m_Hostname = new ToolStripStatusLabel();
            laporanToolStripMenuItem = new ToolStripMenuItem();
            laporanArusKasToolStripMenuItem = new ToolStripMenuItem();
            menuStrip1.SuspendLayout();
            m_StatusStrip.SuspendLayout();
            SuspendLayout();
            // 
            // menuStrip1
            // 
            menuStrip1.Font = new Font("Segoe UI", 9.75F, FontStyle.Regular, GraphicsUnit.Point, 0);
            menuStrip1.Items.AddRange(new ToolStripItem[] { fileToolStripMenuItem, editToolStripMenuItem, toolsToolStripMenuItem, laporanToolStripMenuItem, toolsToolStripMenuItem1, helpToolStripMenuItem });
            menuStrip1.Location = new Point(0, 0);
            menuStrip1.Name = "menuStrip1";
            menuStrip1.Size = new Size(1057, 25);
            menuStrip1.TabIndex = 1;
            menuStrip1.Text = "menuStrip1";
            // 
            // fileToolStripMenuItem
            // 
            fileToolStripMenuItem.DropDownItems.AddRange(new ToolStripItem[] { newToolStripMenuItem, toolStripSeparator, openToolStripMenuItem, saveToolStripMenuItem, toolStripSeparator2, exitToolStripMenuItem });
            fileToolStripMenuItem.Name = "fileToolStripMenuItem";
            fileToolStripMenuItem.Size = new Size(39, 21);
            fileToolStripMenuItem.Text = "&File";
            // 
            // newToolStripMenuItem
            // 
            newToolStripMenuItem.Image = (Image)resources.GetObject("newToolStripMenuItem.Image");
            newToolStripMenuItem.ImageTransparentColor = Color.Magenta;
            newToolStripMenuItem.Name = "newToolStripMenuItem";
            newToolStripMenuItem.ShortcutKeys = Keys.Control | Keys.N;
            newToolStripMenuItem.Size = new Size(222, 22);
            newToolStripMenuItem.Text = "&New";
            // 
            // openToolStripMenuItem
            // 
            openToolStripMenuItem.Image = (Image)resources.GetObject("openToolStripMenuItem.Image");
            openToolStripMenuItem.ImageTransparentColor = Color.Magenta;
            openToolStripMenuItem.Name = "openToolStripMenuItem";
            openToolStripMenuItem.ShortcutKeys = Keys.Control | Keys.O;
            openToolStripMenuItem.Size = new Size(222, 22);
            openToolStripMenuItem.Text = "&Manajemen User";
            openToolStripMenuItem.Click += OpenUserManagementForm;
            // 
            // toolStripSeparator
            // 
            toolStripSeparator.Name = "toolStripSeparator";
            toolStripSeparator.Size = new Size(152, 6);
            // 
            // saveToolStripMenuItem
            // 
            saveToolStripMenuItem.Image = (Image)resources.GetObject("saveToolStripMenuItem.Image");
            saveToolStripMenuItem.ImageTransparentColor = Color.Magenta;
            saveToolStripMenuItem.Name = "saveToolStripMenuItem";
            saveToolStripMenuItem.ShortcutKeys = Keys.Control | Keys.S;
            saveToolStripMenuItem.Size = new Size(222, 22);
            saveToolStripMenuItem.Text = "Manajemen &Role";
            saveToolStripMenuItem.Click += OpenRoleManagementForm;
            // 
            // toolStripSeparator2
            // 
            toolStripSeparator2.Name = "toolStripSeparator2";
            toolStripSeparator2.Size = new Size(152, 6);
            // 
            // exitToolStripMenuItem
            // 
            exitToolStripMenuItem.Name = "exitToolStripMenuItem";
            exitToolStripMenuItem.Size = new Size(155, 22);
            exitToolStripMenuItem.Text = "L&ogout";
            exitToolStripMenuItem.Click += Logout;
            // 
            // editToolStripMenuItem
            // 
            editToolStripMenuItem.DropDownItems.AddRange(new ToolStripItem[] { undoToolStripMenuItem, redoToolStripMenuItem, copyToolStripMenuItem, cutToolStripMenuItem, toolStripSeparator4 });
            editToolStripMenuItem.Name = "editToolStripMenuItem";
            editToolStripMenuItem.Size = new Size(47, 21);
            editToolStripMenuItem.Text = "&Data";
            // 
            // undoToolStripMenuItem
            // 
            undoToolStripMenuItem.Name = "undoToolStripMenuItem";
            undoToolStripMenuItem.ShortcutKeys = Keys.Control | Keys.Z;
            undoToolStripMenuItem.Size = new Size(247, 22);
            undoToolStripMenuItem.Text = "Data Produk";
            undoToolStripMenuItem.Click += OpenProductForm;
            // 
            // redoToolStripMenuItem
            // 
            redoToolStripMenuItem.Name = "redoToolStripMenuItem";
            redoToolStripMenuItem.ShortcutKeys = Keys.Control | Keys.Y;
            redoToolStripMenuItem.Size = new Size(247, 22);
            redoToolStripMenuItem.Text = "Data Outlet";
            redoToolStripMenuItem.Click += OpenOutletForm;
            // 
            // copyToolStripMenuItem
            // 
            copyToolStripMenuItem.Image = (Image)resources.GetObject("copyToolStripMenuItem.Image");
            copyToolStripMenuItem.ImageTransparentColor = Color.Magenta;
            copyToolStripMenuItem.Name = "copyToolStripMenuItem";
            copyToolStripMenuItem.ShortcutKeys = Keys.Control | Keys.C;
            copyToolStripMenuItem.Size = new Size(247, 22);
            copyToolStripMenuItem.Text = "Kategori Pengeluaran";
            copyToolStripMenuItem.Click += OpenExpenseCategoryForm;
            // 
            // cutToolStripMenuItem
            // 
            cutToolStripMenuItem.Image = (Image)resources.GetObject("cutToolStripMenuItem.Image");
            cutToolStripMenuItem.ImageTransparentColor = Color.Magenta;
            cutToolStripMenuItem.Name = "cutToolStripMenuItem";
            cutToolStripMenuItem.ShortcutKeys = Keys.Control | Keys.X;
            cutToolStripMenuItem.Size = new Size(247, 22);
            cutToolStripMenuItem.Text = "Data Waiter";
            cutToolStripMenuItem.Click += OpenWaiterForm;
            // 
            // toolStripSeparator4
            // 
            toolStripSeparator4.Name = "toolStripSeparator4";
            toolStripSeparator4.Size = new Size(244, 6);
            // 
            // toolsToolStripMenuItem
            // 
            toolsToolStripMenuItem.DropDownItems.AddRange(new ToolStripItem[] { customizeToolStripMenuItem, optionsToolStripMenuItem });
            toolsToolStripMenuItem.Name = "toolsToolStripMenuItem";
            toolsToolStripMenuItem.Size = new Size(73, 21);
            toolsToolStripMenuItem.Text = "&Transaksi";
            // 
            // customizeToolStripMenuItem
            // 
            customizeToolStripMenuItem.Name = "customizeToolStripMenuItem";
            customizeToolStripMenuItem.Size = new Size(148, 22);
            customizeToolStripMenuItem.Text = "&Pemasukan";
            // 
            // optionsToolStripMenuItem
            // 
            optionsToolStripMenuItem.Name = "optionsToolStripMenuItem";
            optionsToolStripMenuItem.Size = new Size(148, 22);
            optionsToolStripMenuItem.Text = "Penge&luaran";
            // 
            // toolsToolStripMenuItem1
            // 
            toolsToolStripMenuItem1.DropDownItems.AddRange(new ToolStripItem[] { goToHomepageToolStripMenuItem, publishCloudflaredToolStripMenuItem });
            toolsToolStripMenuItem1.Name = "toolsToolStripMenuItem1";
            toolsToolStripMenuItem1.Size = new Size(66, 21);
            toolsToolStripMenuItem1.Text = "Website";
            // 
            // goToHomepageToolStripMenuItem
            // 
            goToHomepageToolStripMenuItem.Name = "goToHomepageToolStripMenuItem";
            goToHomepageToolStripMenuItem.Size = new Size(197, 22);
            goToHomepageToolStripMenuItem.Text = "Go to Homepage";
            goToHomepageToolStripMenuItem.Click += GoToHomePage;
            // 
            // publishCloudflaredToolStripMenuItem
            // 
            publishCloudflaredToolStripMenuItem.Name = "publishCloudflaredToolStripMenuItem";
            publishCloudflaredToolStripMenuItem.Size = new Size(197, 22);
            publishCloudflaredToolStripMenuItem.Text = "Publish (Cloudflared)";
            publishCloudflaredToolStripMenuItem.Click += PublishHomePage;
            // 
            // helpToolStripMenuItem
            // 
            helpToolStripMenuItem.DropDownItems.AddRange(new ToolStripItem[] { aboutToolStripMenuItem });
            helpToolStripMenuItem.Name = "helpToolStripMenuItem";
            helpToolStripMenuItem.Size = new Size(66, 21);
            helpToolStripMenuItem.Text = "&Bantuan";
            // 
            // aboutToolStripMenuItem
            // 
            aboutToolStripMenuItem.Name = "aboutToolStripMenuItem";
            aboutToolStripMenuItem.Size = new Size(180, 22);
            aboutToolStripMenuItem.Text = "&About...";
            // 
            // m_StatusStrip
            // 
            m_StatusStrip.Font = new Font("Segoe UI", 9.75F, FontStyle.Regular, GraphicsUnit.Point, 0);
            m_StatusStrip.Items.AddRange(new ToolStripItem[] { userLoginName, toolStripStatusLabel1, m_Hostname });
            m_StatusStrip.Location = new Point(0, 428);
            m_StatusStrip.Name = "m_StatusStrip";
            m_StatusStrip.Size = new Size(1057, 22);
            m_StatusStrip.TabIndex = 3;
            m_StatusStrip.Text = "menuStrip2";
            // 
            // userLoginName
            // 
            userLoginName.Image = Properties.Resources.icustomer;
            userLoginName.Name = "userLoginName";
            userLoginName.Size = new Size(147, 17);
            userLoginName.Text = "toolStripStatusLabel1";
            // 
            // toolStripStatusLabel1
            // 
            toolStripStatusLabel1.Name = "toolStripStatusLabel1";
            toolStripStatusLabel1.Size = new Size(857, 17);
            toolStripStatusLabel1.Spring = true;
            toolStripStatusLabel1.Text = "Alaska Application";
            // 
            // m_Hostname
            // 
            m_Hostname.Name = "m_Hostname";
            m_Hostname.Size = new Size(38, 17);
            m_Hostname.Text = "Host:";
            // 
            // laporanToolStripMenuItem
            // 
            laporanToolStripMenuItem.DropDownItems.AddRange(new ToolStripItem[] { laporanArusKasToolStripMenuItem });
            laporanToolStripMenuItem.Name = "laporanToolStripMenuItem";
            laporanToolStripMenuItem.Size = new Size(68, 21);
            laporanToolStripMenuItem.Text = "Laporan";
            // 
            // laporanArusKasToolStripMenuItem
            // 
            laporanArusKasToolStripMenuItem.Name = "laporanArusKasToolStripMenuItem";
            laporanArusKasToolStripMenuItem.Size = new Size(180, 22);
            laporanArusKasToolStripMenuItem.Text = "Laporan Arus Kas";
            // 
            // MainForm
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(1057, 450);
            Controls.Add(m_StatusStrip);
            Controls.Add(menuStrip1);
            IsMdiContainer = true;
            MainMenuStrip = menuStrip1;
            Name = "MainForm";
            Text = "Form1";
            FormClosing += ForceLogout;
            Load += OpenLoginDialog;
            menuStrip1.ResumeLayout(false);
            menuStrip1.PerformLayout();
            m_StatusStrip.ResumeLayout(false);
            m_StatusStrip.PerformLayout();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion
        private MenuStrip menuStrip1;
        private ToolStripMenuItem fileToolStripMenuItem;
        private ToolStripMenuItem newToolStripMenuItem;
        private ToolStripMenuItem openToolStripMenuItem;
        private ToolStripSeparator toolStripSeparator;
        private ToolStripMenuItem saveToolStripMenuItem;
        private ToolStripSeparator toolStripSeparator2;
        private ToolStripMenuItem exitToolStripMenuItem;
        private ToolStripMenuItem editToolStripMenuItem;
        private ToolStripMenuItem undoToolStripMenuItem;
        private ToolStripMenuItem redoToolStripMenuItem;
        private ToolStripMenuItem copyToolStripMenuItem;
        private ToolStripMenuItem cutToolStripMenuItem;
        private ToolStripSeparator toolStripSeparator4;
        private ToolStripMenuItem toolsToolStripMenuItem;
        private ToolStripMenuItem customizeToolStripMenuItem;
        private ToolStripMenuItem optionsToolStripMenuItem;
        private ToolStripMenuItem helpToolStripMenuItem;
        private ToolStripMenuItem aboutToolStripMenuItem;
        private ToolStripMenuItem toolsToolStripMenuItem1;
        private ToolStripMenuItem goToHomepageToolStripMenuItem;
        private StatusStrip m_StatusStrip;
        private ToolStripStatusLabel userLoginName;
        private ToolStripStatusLabel toolStripStatusLabel1;
        private ToolStripStatusLabel m_Hostname;
        private ToolStripMenuItem publishCloudflaredToolStripMenuItem;
        private ToolStripMenuItem laporanToolStripMenuItem;
        private ToolStripMenuItem laporanArusKasToolStripMenuItem;
    }
}
