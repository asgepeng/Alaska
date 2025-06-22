using Alaska;
using Alaska.Data;
using Alaska.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WinformApp.Forms
{
    public partial class ChangePasswordForm : Form
    {
        public ChangePasswordForm()
        {
            InitializeComponent();
        }

        private async void HandleButtonClicked(object sender, EventArgs e)
        {
            if (this.newPasswordTextBox.Text == "")
            {
                MessageBox.Show("Password lama tidak boleh kosong", "Password lama", MessageBoxButtons.OK, MessageBoxIcon.Information);
                this.oldPasswrodTextBox.Focus();
                return;
            }
            if (this.newPasswordTextBox.Text == "")
            {
                MessageBox.Show("Password baru tidak boleh kosong", "Password baru", MessageBoxButtons.OK, MessageBoxIcon.Information);
                newPasswordTextBox.Focus();
                return;
            }
            if (this.confirmPasswordTextBox.Text == "")
            {
                MessageBox.Show("Konfirmasi password baru tidak boleh kosong", "Konfirmasi password", MessageBoxButtons.OK, MessageBoxIcon.Information);
                confirmPasswordTextBox.Focus();
                return;
            }
            if (this.newPasswordTextBox.Text != this.confirmPasswordTextBox.Text)
            {
                MessageBox.Show("Konfirmasi password tidak match", "Konfirmasi password", MessageBoxButtons.OK, MessageBoxIcon.Information);
                confirmPasswordTextBox.Focus();
                return;
            }
            var changePasswordModel = new ResetPasswordModel()
            {
                OldPassword = this.oldPasswrodTextBox.Text,
                NewPassword = this.confirmPasswordTextBox.Text
            };
            var model = JsonSerializer.Serialize(changePasswordModel, AppJsonSerializerContext.Default.ResetPasswordModel);
            var json = await HttpClientSingleton.PostAsync("/user-manager/reset-password", model);
            var cr = json.Length > 0 ? JsonSerializer.Deserialize(json, AppJsonSerializerContext.Default.CommonResult) : null;
            if (cr != null)
            {
                if (cr.Success)
                {
                    MessageBox.Show(cr.Message, "Berhasil", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    this.DialogResult = DialogResult.OK;
                    this.Close();
                }
                else
                {
                    MessageBox.Show(cr.Message, "Gagal", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
        }
    }
}
