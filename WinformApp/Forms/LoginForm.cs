using Alaska.Data;
using Alaska;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WinformApp
{
    public partial class LoginForm : Form
    {
        public LoginForm()
        {
            InitializeComponent();
        }
        private void DisableControl()
        {
            this.usernameTextBox.Enabled = false;
            this.passwordTextBox.Enabled = false;
            this.loginButton.Enabled = false;
            this.Cursor = Cursors.WaitCursor;
        }
        private void EnableControl()
        {
            this.Cursor = Cursors.WaitCursor;
            this.usernameTextBox.Enabled = true;
            this.passwordTextBox.Enabled = true;
            this.loginButton.Enabled = true;
        }
        private async void TryLogin(object sender, EventArgs e)
        {
            if (this.usernameTextBox.Text.Trim() == "")
            {
                MessageBox.Show("Username tidak boleh kosong", "Username", MessageBoxButtons.OK, MessageBoxIcon.Information);
                usernameTextBox.Focus();
                return;
            }
            if (this.usernameTextBox.Focused)
            {
                this.passwordTextBox.Focus();
                return;
            }
            if (this.usernameTextBox.Text == "")
            {
                MessageBox.Show("Password tidak boleh kosong", "Password", MessageBoxButtons.OK, MessageBoxIcon.Information);
                passwordTextBox.Focus();
            }
            this.DisableControl();
            if (await HttpClientSingleton.SignInAsync(this.usernameTextBox.Text.Trim(), this.passwordTextBox.Text))
            {
                this.DialogResult = DialogResult.OK;
                this.Close();
            }
            else
            {
                MessageBox.Show("Username atau password yang diinput tidak valid", "Invalid Input", MessageBoxButtons.OK, MessageBoxIcon.Information);
                this.EnableControl();
            }
        }

        private void OnKeyPressed(object sender, KeyEventArgs e)
        {
            if (e.KeyData == Keys.Enter)
            {
                loginButton.PerformClick();
            }
        }
    }
}
