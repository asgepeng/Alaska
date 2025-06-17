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
            this.button1.Enabled = false;
        }
        private void EnableControl()
        {
            this.usernameTextBox.Enabled = true;
            this.passwordTextBox.Enabled = true;
            this.button1.Enabled = true;
        }
        private async void TryLogin(object sender, EventArgs e)
        {
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
    }
}
