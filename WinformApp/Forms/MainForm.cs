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
        }
        private void SetEnableControls(bool enable)
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
            if (File.Exists(hostSettingPath))
            {
                My.Application.ApiUrl = File.ReadAllText(hostSettingPath).Trim();
            }
            LoginForm dialog = new LoginForm();
            if (dialog.ShowDialog() == DialogResult.OK)
            {
                string username = My.Application.User != null ? My.Application.User.Name : "-";
                this.userLoginName.Text = username;
                this.m_Hostname.Text = "Host: " + NetworkHelper.GetIPV4Address() + ":5005";
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
            this.SetEnableControls(true);
        }

        private void HandleMdiChildActivate(object sender, EventArgs e)
        {
            var activeForm = ActiveMdiChild;
            if (activeForm != null)
            {
                if (activeForm is ListingForm lform)
                {
                    this.navigator.BindingSource = lform.BindingSource;
                    this.childFormLabel.Text = lform.Text;
                    return;
                }
            }
            this.childFormLabel.Text = "";
            this.SetEnableControls(false);
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
    }
}
