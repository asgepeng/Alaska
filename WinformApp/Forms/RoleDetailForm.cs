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
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WinformApp.Forms
{
    public partial class RoleDetailForm : Form
    {
        public RoleDetailForm()
        {
            InitializeComponent();
        }
        private async Task<bool> RoleNameExistsAsync(string role)
        {
            var json = await HttpClientSingleton.PostAsync("/role-manager/exists", role);
            return json == "true";
        }

        private async void HandleFormLoad(object sender, EventArgs e)
        {
            if (this.Tag != null)
            {
                var id = (int)Tag;
                var json = await HttpClientSingleton.GetAsync("/role-manager/" + id.ToString());
                var role = JsonSerializer.Deserialize(json, AppJsonSerializerContext.Default.Role);
                if (role != null) this.textBox1.Text = role.Name;
            }
        }

        private async void HandleSaveButtonClicked(object sender, EventArgs e)
        {
            if (this.textBox1.Text.Trim() == "")
            {
                MessageBox.Show("Nama role tidak boleh kosong", "Nama", MessageBoxButtons.OK, MessageBoxIcon.Information);
                this.textBox1.Focus();
                return;
            }
            var id = this.Tag is null ? 0 : (int)Tag;
            var role = new Role()
            {
                Id = id,
                Name = this.textBox1.Text.Trim()
            };
            var roleJson = JsonSerializer.Serialize(role, AppJsonSerializerContext.Default.Role);
            if (await RoleNameExistsAsync(roleJson))
            {
                MessageBox.Show("Nama role sudah dipakai, pakai nama lain", "Nama", MessageBoxButtons.OK, MessageBoxIcon.Information);
                this.textBox1.Focus();
                return;
            }
            var result = role.Id > 0 ? await HttpClientSingleton.PutAsync("/role-manager", roleJson) : await HttpClientSingleton.PostAsync("/role-manager", roleJson);
            var success = result == "true";
            if (success)
            {
                this.DialogResult = DialogResult.OK;
                this.Close();
            }
        }
    }
}
