using Alaska.Data;
using Alaska.Models;
using Microsoft.VisualBasic.ApplicationServices;
using System.Data;
using System.Diagnostics;
using System.Text.RegularExpressions;
using WinformApp.Data;
using WinformApp.Forms;

namespace WinformApp
{
    public partial class MainForm : Form
    {
        private UserService userService;

        public MainForm()
        {
            InitializeComponent();
            My.Application.ApiUrl = "http://localhost:5005";
            userService = new UserService();

            productButton.Tag = ListingType.Product;
            outletButton.Tag = ListingType.Outlet;
            waiterButton.Tag = ListingType.Waiter;
            saleButton.Tag = ListingType.Sales;
            cashflowButton.Tag = ListingType.CashFlow;
            costTypeButton.Tag = ListingType.CostType;

        }
        private void SetEnableControls(bool enable, bool usePeriod)
        {
            this.newToolStripMenuItem.Enabled = enable;
            this.refreshButton.Enabled = enable;
            this.addButton.Enabled = enable;
            this.deleteButton.Enabled = enable;
        }
        internal void SetBindingSource(BindingSource? bs)
        {
            this.navigator.BindingSource = bs;
        }
        private void OpenLoginDialog(object sender, EventArgs e)
        {
            var hostSettingPath = AppContext.BaseDirectory + "host.ini";
            var useHostIni = false;
            if (File.Exists(hostSettingPath))
            {
                useHostIni = true;
                My.Application.ApiUrl = File.ReadAllText(hostSettingPath).Trim();
            }
            LoginForm dialog = new LoginForm();
            if (dialog.ShowDialog() == DialogResult.OK)
            {
                string username = My.Application.User != null ? My.Application.User.Name : "-";
                this.userLoginName.Text = username;
                if (useHostIni) this.m_Hostname.Text = "Host: " + My.Application.ApiUrl;
                else this.m_Hostname.Text = "Host: " + NetworkHelper.GetIPV4Address() + ":5005";
                this.WindowState = FormWindowState.Maximized;
            }
            else
            {
                this.Close();
            }
        }
        private void OpenUserManagementForm(object sender, EventArgs e)
        {
            UserForm dlg = new UserForm();
            dlg.ShowDialog();
        }
        private void GoToHomePage(object sender, EventArgs e)
        {
            var url = "http://" + NetworkHelper.GetIPV4Address() + ":5005";
            System.Diagnostics.Process.Start(new ProcessStartInfo
            {
                FileName = "cmd",
                Arguments = $"/c start {url}",
                CreateNoWindow = true
            });

        }

        private async void Logout(object sender, EventArgs e)
        {
            var result = await HttpClientSingleton.PostAsync("/auth/logout");
            if (result == "true")
            {
                this.Close();
            }
        }

        private async void ForceLogout(object sender, FormClosingEventArgs e)
        {
            if (My.Application.ApiToken != "") await HttpClientSingleton.PostAsync("/auth/logout");
            HttpClientSingleton.Dispose();
        }

        private void PublishHomePage(object sender, EventArgs e)
        {
            Process.Start("cloudflared", "tunnel --url http://localhost:5005 --no-autoupdate");
        }

        private void HandleRoleButtonClicked(object sender, EventArgs e)
        {
            var dialog = new RoleForm();
            dialog.ShowDialog();
        }

        private void OpenOutletForm(object sender, EventArgs e)
        {
            var form = new OutletDetailForm();
            form.MdiParent = this;
            form.Show();
            form.WindowState = FormWindowState.Maximized;
        }

        private void OpenExpenseCategoryForm(object sender, EventArgs e)
        {

        }

        private void OpenListingForm(object sender, EventArgs e)
        {
            var button = (ToolStripMenuItem)sender;
            if (button.Tag is null) return;
            var tp = (ListingType)button.Tag;

            foreach (Form frm in this.MdiChildren)
            {
                if (frm is ListingForm lform)
                {
                    if (lform.Type == tp)
                    {
                        lform.Activate();
                        return;
                    }
                }
            }

            ListingForm form = new ListingForm(tp);
            form.MdiParent = this;
            form.Show();
            form.WindowState = FormWindowState.Maximized;
            form.FormClosing += (sender, e) =>
            {
                navigator.BindingSource = null;
            };
            this.SetEnableControls(true, form.UsePeriod());
        }

        private async void HandleMdiChildActivate(object sender, EventArgs e)
        {
            var activeForm = ActiveMdiChild;
            if (activeForm != null)
            {
                if (activeForm is ListingForm lform)
                {
                    await lform.LoadDataTableAsync();
                    this.navigator.BindingSource = lform.BindingSource;
                    this.childFormLabel.Text = lform.Text;
                    this.SetEnableControls(true, lform.UsePeriod());
                    return;
                }
            }
            this.childFormLabel.Text = "";
            this.SetEnableControls(false, false);
            navigator.BindingSource = null;
        }

        private async void RefreshListing(object sender, EventArgs e)
        {
            var activeForm = this.ActiveMdiChild;
            if (activeForm != null)
            {
                if (activeForm is ListingForm lform)
                {
                    await lform.LoadDataTableAsync();
                }
            }
        }

        private async void HandleAddNewItem(object sender, EventArgs e)
        {
            var activeForm = this.ActiveMdiChild;
            if (activeForm != null)
            {
                if (activeForm is ListingForm lform)
                {
                    await lform.OpenNewDialog();
                }
            }
        }

        private async void HandleDeleteRecord(object sender, EventArgs e)
        {
            var activeForm = this.ActiveMdiChild;
            if (activeForm != null)
            {
                if (activeForm is ListingForm lform)
                {
                    await lform.Delete();
                }
            }
        }

        private void HandleChangePasswordButtonClicked(object sender, EventArgs e)
        {
            ChangePasswordForm dialog = new ChangePasswordForm();
            dialog.ShowDialog();
        }

        private async void OpenCategoryProduct(object sender, EventArgs e)
        {
            if (this.MdiChildren.Length > 0)
            {
                MessageBox.Show("Tutup semua windows yang terbuka terlebih dahulu", "Windows", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            if (MessageBox.Show("Yakin mau menghapus semua data? Jika anda melanjutkan, data tidak bisa dipulihkan kembali", "Konfirmasi", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                var json = await HttpClientSingleton.GetAsync("/master-data/reset");
                if (json == "true")
                {
                    MessageBox.Show("Reset data berhasil", "Berhasil", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    Application.Restart();
                }
            }
        }

        private void aboutToolStripMenuItem_Click(object sender, EventArgs e)
        {
            AboutForm form = new AboutForm();
            form.ShowDialog();
        }
    }
}
