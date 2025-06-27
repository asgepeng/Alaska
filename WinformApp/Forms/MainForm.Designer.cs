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
            components = new System.ComponentModel.Container();
            menuStrip1 = new MenuStrip();
            fileToolStripMenuItem = new ToolStripMenuItem();
            newToolStripMenuItem = new ToolStripMenuItem();
            toolStripSeparator = new ToolStripSeparator();
            openToolStripMenuItem = new ToolStripMenuItem();
            roleButton = new ToolStripMenuItem();
            toolStripSeparator2 = new ToolStripSeparator();
            ubahPasswordToolStripMenuItem = new ToolStripMenuItem();
            exitToolStripMenuItem = new ToolStripMenuItem();
            editToolStripMenuItem = new ToolStripMenuItem();
            productButton = new ToolStripMenuItem();
            waiterButton = new ToolStripMenuItem();
            outletButton = new ToolStripMenuItem();
            costTypeButton = new ToolStripMenuItem();
            toolStripSeparator4 = new ToolStripSeparator();
            expenseButton = new ToolStripMenuItem();
            toolsToolStripMenuItem = new ToolStripMenuItem();
            saleButton = new ToolStripMenuItem();
            cashflowButton = new ToolStripMenuItem();
            toolsToolStripMenuItem1 = new ToolStripMenuItem();
            goToHomepageToolStripMenuItem = new ToolStripMenuItem();
            publishCloudflaredToolStripMenuItem = new ToolStripMenuItem();
            helpToolStripMenuItem = new ToolStripMenuItem();
            aboutToolStripMenuItem = new ToolStripMenuItem();
            m_StatusStrip = new StatusStrip();
            userLoginName = new ToolStripStatusLabel();
            toolStripStatusLabel1 = new ToolStripStatusLabel();
            m_Hostname = new ToolStripStatusLabel();
            navigator = new BindingNavigator(components);
            countLabel = new ToolStripLabel();
            refreshButton = new ToolStripButton();
            sparator0 = new ToolStripSeparator();
            addButton = new ToolStripButton();
            deleteButton = new ToolStripButton();
            sparator1 = new ToolStripSeparator();
            firstRowButton = new ToolStripButton();
            previousRowButton = new ToolStripButton();
            positionTextBox = new ToolStripTextBox();
            sparator2 = new ToolStripSeparator();
            nextButton = new ToolStripButton();
            lastRowButton = new ToolStripButton();
            sparator3 = new ToolStripSeparator();
            downloadButton = new ToolStripButton();
            childFormLabel = new ToolStripLabel();
            menuStrip1.SuspendLayout();
            m_StatusStrip.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)navigator).BeginInit();
            navigator.SuspendLayout();
            SuspendLayout();
            // 
            // menuStrip1
            // 
            menuStrip1.Font = new Font("Segoe UI", 9.75F, FontStyle.Regular, GraphicsUnit.Point, 0);
            menuStrip1.Items.AddRange(new ToolStripItem[] { fileToolStripMenuItem, editToolStripMenuItem, toolsToolStripMenuItem, toolsToolStripMenuItem1, helpToolStripMenuItem });
            menuStrip1.Location = new Point(0, 0);
            menuStrip1.Name = "menuStrip1";
            menuStrip1.Padding = new Padding(6, 4, 0, 4);
            menuStrip1.Size = new Size(1057, 29);
            menuStrip1.TabIndex = 1;
            menuStrip1.Text = "menuStrip1";
            // 
            // fileToolStripMenuItem
            // 
            fileToolStripMenuItem.DropDownItems.AddRange(new ToolStripItem[] { newToolStripMenuItem, toolStripSeparator, openToolStripMenuItem, roleButton, toolStripSeparator2, ubahPasswordToolStripMenuItem, exitToolStripMenuItem });
            fileToolStripMenuItem.Name = "fileToolStripMenuItem";
            fileToolStripMenuItem.Size = new Size(39, 21);
            fileToolStripMenuItem.Text = "&File";
            // 
            // newToolStripMenuItem
            // 
            newToolStripMenuItem.Enabled = false;
            newToolStripMenuItem.Image = Properties.Resources.plusblack;
            newToolStripMenuItem.ImageTransparentColor = Color.Magenta;
            newToolStripMenuItem.Name = "newToolStripMenuItem";
            newToolStripMenuItem.ShortcutKeys = Keys.Control | Keys.N;
            newToolStripMenuItem.Size = new Size(221, 22);
            newToolStripMenuItem.Text = "&New";
            newToolStripMenuItem.Click += HandleAddNewItem;
            // 
            // toolStripSeparator
            // 
            toolStripSeparator.Name = "toolStripSeparator";
            toolStripSeparator.Size = new Size(218, 6);
            // 
            // openToolStripMenuItem
            // 
            openToolStripMenuItem.Image = Properties.Resources.usermanager;
            openToolStripMenuItem.ImageTransparentColor = Color.Magenta;
            openToolStripMenuItem.Name = "openToolStripMenuItem";
            openToolStripMenuItem.ShortcutKeys = Keys.Control | Keys.U;
            openToolStripMenuItem.Size = new Size(221, 22);
            openToolStripMenuItem.Text = "&Manajemen User";
            openToolStripMenuItem.Click += OpenUserManagementForm;
            // 
            // roleButton
            // 
            roleButton.ImageTransparentColor = Color.Magenta;
            roleButton.Name = "roleButton";
            roleButton.ShortcutKeys = Keys.Control | Keys.L;
            roleButton.Size = new Size(221, 22);
            roleButton.Text = "Manajemen &Role";
            roleButton.Click += HandleRoleButtonClicked;
            // 
            // toolStripSeparator2
            // 
            toolStripSeparator2.Name = "toolStripSeparator2";
            toolStripSeparator2.Size = new Size(218, 6);
            // 
            // ubahPasswordToolStripMenuItem
            // 
            ubahPasswordToolStripMenuItem.Image = Properties.Resources.changepwd;
            ubahPasswordToolStripMenuItem.Name = "ubahPasswordToolStripMenuItem";
            ubahPasswordToolStripMenuItem.Size = new Size(221, 22);
            ubahPasswordToolStripMenuItem.Text = "Ubah Password";
            ubahPasswordToolStripMenuItem.Click += HandleChangePasswordButtonClicked;
            // 
            // exitToolStripMenuItem
            // 
            exitToolStripMenuItem.Name = "exitToolStripMenuItem";
            exitToolStripMenuItem.Size = new Size(221, 22);
            exitToolStripMenuItem.Text = "L&ogout";
            exitToolStripMenuItem.Click += Logout;
            // 
            // editToolStripMenuItem
            // 
            editToolStripMenuItem.DropDownItems.AddRange(new ToolStripItem[] { productButton, waiterButton, outletButton, costTypeButton, toolStripSeparator4, expenseButton });
            editToolStripMenuItem.Name = "editToolStripMenuItem";
            editToolStripMenuItem.Size = new Size(47, 21);
            editToolStripMenuItem.Text = "&Data";
            // 
            // productButton
            // 
            productButton.Image = Properties.Resources.icons8_product_24;
            productButton.Name = "productButton";
            productButton.ShortcutKeys = Keys.Control | Keys.Z;
            productButton.Size = new Size(192, 22);
            productButton.Text = "Data Produk";
            productButton.Click += OpenListingForm;
            // 
            // waiterButton
            // 
            waiterButton.Image = Properties.Resources.usermanager;
            waiterButton.ImageTransparentColor = Color.Magenta;
            waiterButton.Name = "waiterButton";
            waiterButton.ShortcutKeys = Keys.Control | Keys.X;
            waiterButton.Size = new Size(192, 22);
            waiterButton.Text = "Data Waiter";
            waiterButton.Click += OpenListingForm;
            // 
            // outletButton
            // 
            outletButton.Name = "outletButton";
            outletButton.ShortcutKeys = Keys.Control | Keys.Y;
            outletButton.Size = new Size(192, 22);
            outletButton.Text = "Data Outlet";
            outletButton.Click += OpenListingForm;
            // 
            // costTypeButton
            // 
            costTypeButton.Name = "costTypeButton";
            costTypeButton.Size = new Size(192, 22);
            costTypeButton.Text = "Kategori Biaya";
            costTypeButton.Click += OpenListingForm;
            // 
            // toolStripSeparator4
            // 
            toolStripSeparator4.Name = "toolStripSeparator4";
            toolStripSeparator4.Size = new Size(189, 6);
            // 
            // expenseButton
            // 
            expenseButton.ImageTransparentColor = Color.Magenta;
            expenseButton.Name = "expenseButton";
            expenseButton.Size = new Size(192, 22);
            expenseButton.Text = "Reset Data";
            expenseButton.Click += OpenCategoryProduct;
            // 
            // toolsToolStripMenuItem
            // 
            toolsToolStripMenuItem.DropDownItems.AddRange(new ToolStripItem[] { saleButton, cashflowButton });
            toolsToolStripMenuItem.Name = "toolsToolStripMenuItem";
            toolsToolStripMenuItem.Size = new Size(73, 21);
            toolsToolStripMenuItem.Text = "&Transaksi";
            // 
            // saleButton
            // 
            saleButton.Image = Properties.Resources.iincome;
            saleButton.Name = "saleButton";
            saleButton.Size = new Size(231, 22);
            saleButton.Text = "&Penjualan Tea && Chocolate";
            saleButton.Click += OpenListingForm;
            // 
            // cashflowButton
            // 
            cashflowButton.Image = Properties.Resources.isale;
            cashflowButton.Name = "cashflowButton";
            cashflowButton.Size = new Size(231, 22);
            cashflowButton.Text = "Kas Keluar && Masuk";
            cashflowButton.Click += OpenListingForm;
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
            aboutToolStripMenuItem.Size = new Size(120, 22);
            aboutToolStripMenuItem.Text = "&About...";
            aboutToolStripMenuItem.Click += aboutToolStripMenuItem_Click;
            // 
            // m_StatusStrip
            // 
            m_StatusStrip.BackColor = SystemColors.ControlDarkDark;
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
            userLoginName.BackColor = Color.DimGray;
            userLoginName.ForeColor = Color.White;
            userLoginName.Image = Properties.Resources.usr;
            userLoginName.Name = "userLoginName";
            userLoginName.Size = new Size(147, 17);
            userLoginName.Text = "toolStripStatusLabel1";
            // 
            // toolStripStatusLabel1
            // 
            toolStripStatusLabel1.ForeColor = Color.White;
            toolStripStatusLabel1.Name = "toolStripStatusLabel1";
            toolStripStatusLabel1.Size = new Size(857, 17);
            toolStripStatusLabel1.Spring = true;
            toolStripStatusLabel1.Text = "Alaska Application";
            // 
            // m_Hostname
            // 
            m_Hostname.ForeColor = Color.White;
            m_Hostname.Name = "m_Hostname";
            m_Hostname.Size = new Size(38, 17);
            m_Hostname.Text = "Host:";
            // 
            // navigator
            // 
            navigator.AddNewItem = null;
            navigator.CountItem = countLabel;
            navigator.DeleteItem = null;
            navigator.Font = new Font("Segoe UI", 9.75F, FontStyle.Regular, GraphicsUnit.Point, 0);
            navigator.GripStyle = ToolStripGripStyle.Hidden;
            navigator.ImageScalingSize = new Size(24, 24);
            navigator.Items.AddRange(new ToolStripItem[] { refreshButton, sparator0, addButton, deleteButton, sparator1, firstRowButton, previousRowButton, positionTextBox, countLabel, sparator2, nextButton, lastRowButton, sparator3, downloadButton, childFormLabel });
            navigator.Location = new Point(0, 29);
            navigator.MoveFirstItem = firstRowButton;
            navigator.MoveLastItem = lastRowButton;
            navigator.MoveNextItem = nextButton;
            navigator.MovePreviousItem = previousRowButton;
            navigator.Name = "navigator";
            navigator.Padding = new Padding(6);
            navigator.PositionItem = positionTextBox;
            navigator.Size = new Size(1057, 43);
            navigator.TabIndex = 5;
            navigator.Text = "toolStrip1";
            // 
            // countLabel
            // 
            countLabel.Name = "countLabel";
            countLabel.Size = new Size(39, 28);
            countLabel.Text = "of {0}";
            // 
            // refreshButton
            // 
            refreshButton.Enabled = false;
            refreshButton.Image = Properties.Resources.reload;
            refreshButton.ImageTransparentColor = Color.Magenta;
            refreshButton.Name = "refreshButton";
            refreshButton.Size = new Size(80, 28);
            refreshButton.Text = "Refresh";
            refreshButton.Click += RefreshListing;
            // 
            // sparator0
            // 
            sparator0.Name = "sparator0";
            sparator0.Size = new Size(6, 31);
            // 
            // addButton
            // 
            addButton.DisplayStyle = ToolStripItemDisplayStyle.Image;
            addButton.Enabled = false;
            addButton.Image = Properties.Resources.plusblack;
            addButton.ImageTransparentColor = Color.Magenta;
            addButton.Name = "addButton";
            addButton.Size = new Size(28, 28);
            addButton.Text = "&New";
            addButton.Click += HandleAddNewItem;
            // 
            // deleteButton
            // 
            deleteButton.DisplayStyle = ToolStripItemDisplayStyle.Image;
            deleteButton.Enabled = false;
            deleteButton.Image = Properties.Resources.deleteblack;
            deleteButton.ImageTransparentColor = Color.Magenta;
            deleteButton.Name = "deleteButton";
            deleteButton.Size = new Size(28, 28);
            deleteButton.Text = "Delete";
            deleteButton.Click += HandleDeleteRecord;
            // 
            // sparator1
            // 
            sparator1.Name = "sparator1";
            sparator1.Size = new Size(6, 31);
            // 
            // firstRowButton
            // 
            firstRowButton.DisplayStyle = ToolStripItemDisplayStyle.Image;
            firstRowButton.Image = Properties.Resources.first_page;
            firstRowButton.ImageTransparentColor = Color.Magenta;
            firstRowButton.Name = "firstRowButton";
            firstRowButton.Size = new Size(28, 28);
            firstRowButton.Text = "Move First";
            // 
            // previousRowButton
            // 
            previousRowButton.DisplayStyle = ToolStripItemDisplayStyle.Image;
            previousRowButton.Image = Properties.Resources.back__2_;
            previousRowButton.ImageTransparentColor = Color.Magenta;
            previousRowButton.Name = "previousRowButton";
            previousRowButton.Size = new Size(28, 28);
            previousRowButton.Text = "Move Previous";
            // 
            // positionTextBox
            // 
            positionTextBox.BackColor = Color.FromArgb(255, 255, 192);
            positionTextBox.Name = "positionTextBox";
            positionTextBox.Size = new Size(60, 31);
            positionTextBox.Text = "0";
            // 
            // sparator2
            // 
            sparator2.Name = "sparator2";
            sparator2.Size = new Size(6, 31);
            // 
            // nextButton
            // 
            nextButton.DisplayStyle = ToolStripItemDisplayStyle.Image;
            nextButton.Image = Properties.Resources.right_arrow;
            nextButton.ImageTransparentColor = Color.Magenta;
            nextButton.Name = "nextButton";
            nextButton.Size = new Size(28, 28);
            nextButton.Text = "Move Next";
            // 
            // lastRowButton
            // 
            lastRowButton.DisplayStyle = ToolStripItemDisplayStyle.Image;
            lastRowButton.Image = Properties.Resources.last_page;
            lastRowButton.ImageTransparentColor = Color.Magenta;
            lastRowButton.Name = "lastRowButton";
            lastRowButton.Size = new Size(28, 28);
            lastRowButton.Text = "Move Last";
            // 
            // sparator3
            // 
            sparator3.Name = "sparator3";
            sparator3.Size = new Size(6, 31);
            // 
            // downloadButton
            // 
            downloadButton.DisplayStyle = ToolStripItemDisplayStyle.Image;
            downloadButton.Image = Properties.Resources.download;
            downloadButton.ImageTransparentColor = Color.Magenta;
            downloadButton.Name = "downloadButton";
            downloadButton.Size = new Size(28, 28);
            downloadButton.Text = "Download";
            // 
            // childFormLabel
            // 
            childFormLabel.Alignment = ToolStripItemAlignment.Right;
            childFormLabel.Font = new Font("Tahoma", 12F, FontStyle.Bold, GraphicsUnit.Point, 0);
            childFormLabel.ForeColor = SystemColors.HotTrack;
            childFormLabel.Margin = new Padding(0, 1, 6, 2);
            childFormLabel.Name = "childFormLabel";
            childFormLabel.Size = new Size(0, 28);
            // 
            // MainForm
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(1057, 450);
            Controls.Add(navigator);
            Controls.Add(m_StatusStrip);
            Controls.Add(menuStrip1);
            IsMdiContainer = true;
            MainMenuStrip = menuStrip1;
            Name = "MainForm";
            Text = "Alaska Application";
            FormClosing += ForceLogout;
            Load += OpenLoginDialog;
            MdiChildActivate += HandleMdiChildActivate;
            menuStrip1.ResumeLayout(false);
            menuStrip1.PerformLayout();
            m_StatusStrip.ResumeLayout(false);
            m_StatusStrip.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)navigator).EndInit();
            navigator.ResumeLayout(false);
            navigator.PerformLayout();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion
        private MenuStrip menuStrip1;
        private ToolStripMenuItem fileToolStripMenuItem;
        private ToolStripMenuItem newToolStripMenuItem;
        private ToolStripMenuItem openToolStripMenuItem;
        private ToolStripSeparator toolStripSeparator;
        private ToolStripMenuItem roleButton;
        private ToolStripSeparator toolStripSeparator2;
        private ToolStripMenuItem exitToolStripMenuItem;
        private ToolStripMenuItem editToolStripMenuItem;
        private ToolStripMenuItem productButton;
        private ToolStripMenuItem outletButton;
        private ToolStripMenuItem expenseButton;
        private ToolStripMenuItem waiterButton;
        private ToolStripSeparator toolStripSeparator4;
        private ToolStripMenuItem toolsToolStripMenuItem;
        private ToolStripMenuItem saleButton;
        private ToolStripMenuItem helpToolStripMenuItem;
        private ToolStripMenuItem aboutToolStripMenuItem;
        private ToolStripMenuItem toolsToolStripMenuItem1;
        private ToolStripMenuItem goToHomepageToolStripMenuItem;
        private StatusStrip m_StatusStrip;
        private ToolStripStatusLabel userLoginName;
        private ToolStripStatusLabel toolStripStatusLabel1;
        private ToolStripStatusLabel m_Hostname;
        private ToolStripMenuItem publishCloudflaredToolStripMenuItem;
        private BindingNavigator navigator;
        private ToolStripButton addButton;
        private ToolStripButton deleteButton;
        private ToolStripButton firstRowButton;
        private ToolStripButton previousRowButton;
        private ToolStripSeparator sparator2;
        private ToolStripButton nextButton;
        private ToolStripButton lastRowButton;
        private ToolStripSeparator sparator3;
        private ToolStripButton downloadButton;
        private ToolStripLabel childFormLabel;
        private ToolStripTextBox positionTextBox;
        private ToolStripLabel countLabel;
        private ToolStripButton refreshButton;
        private ToolStripSeparator sparator1;
        private ToolStripMenuItem ubahPasswordToolStripMenuItem;
        private ToolStripSeparator sparator0;
        private ToolStripMenuItem costTypeButton;
        private ToolStripMenuItem cashflowButton;
    }
}
