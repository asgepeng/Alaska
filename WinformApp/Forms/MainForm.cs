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
        }

        private void OpenLoginDialog(object sender, EventArgs e)
        {
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
            if (My.Application.ApiUrl != "") await HttpClientSingleton.PostAsync("/auth/logout");
        }

        private void PublishHomePage(object sender, EventArgs e)
        {
            Process.Start("cloudflared", "tunnel --url http://localhost:5005 --no-autoupdate");
        }

        private void OpenRoleManagementForm(object sender, EventArgs e)
        {

        }

        private void OpenOutletForm(object sender, EventArgs e)
        {
            var form = new OutletForm();
            form.MdiParent = this;
            form.Show();
            form.WindowState = FormWindowState.Maximized;
        }

        private void OpenWaiterForm(object sender, EventArgs e)
        {
            var form = new WaiterForm();
            form.MdiParent = this;
            form.Show();
            form.WindowState = FormWindowState.Maximized;
        }

        private void OpenExpenseCategoryForm(object sender, EventArgs e)
        {

        }

        private void OpenProductForm(object sender, EventArgs e)
        {

        }
    }
}
