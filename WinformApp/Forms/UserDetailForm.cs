using Alaska.Models;
using AlaskaLib.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using WinformApp.Data;

namespace WinformApp.Forms
{
    public partial class UserDetailForm : Form
    {
        internal UserDetailForm(UserService userService)
        {
            InitializeComponent();
            this.Service = userService;
        }
        internal UserService Service { get; }
        internal User? User { get; set; }
        private async void loginButton_Click(object sender, EventArgs e)
        {
            if (textBox1.Text.Trim() == "")
            {
                MessageBox.Show("Nama pengguna tidak boleh kosong", "Nama pengguna", MessageBoxButtons.OK, MessageBoxIcon.Information);
                this.textBox1.Focus();
                return;
            }
            if (comboBox1.SelectedItem is null)
            {
                MessageBox.Show("Role masihbelum dipilih", "Role", MessageBoxButtons.OK, MessageBoxIcon.Information);
                comboBox1.Focus();
                return;
            }
            if (textBox2.Text.Trim() == "")
            {
                MessageBox.Show("Login tidak boleh kosong", "Login", MessageBoxButtons.OK, MessageBoxIcon.Information);
                textBox2.Focus();
                return;
            }
            if (textBox3.Text == "")
            {
                MessageBox.Show("Password tidak boleh kosong", "Password", MessageBoxButtons.OK, MessageBoxIcon.Information);
                textBox3.Focus();
                return;
            }
            if (User is null) User = new User();
            User.Name = textBox1.Text.Trim();
            User.RoleId = ((DropdownOption)this.comboBox1.SelectedItem).Id;
            User.Login = this.textBox2.Text.Trim();
            User.Password = this.textBox3.Text;

            if (User.Id > 0)
            {
                if (await this.Service.UpdateAsync(User))
                {
                    this.DialogResult = DialogResult.OK;
                    this.Close();
                }
            }
            else
            {
                if (await this.Service.CreateAsync(User))
                {
                    this.DialogResult = DialogResult.OK;
                    this.Close();
                }

            }
        }

        private async void HandleFormLoad(object sender, EventArgs e)
        {
            var id = Tag is null ? 0 : (int)Tag;
            var obj = await this.Service.GetByIdAsync(id);
            if (obj != null && obj is UserViewModel uvm)
            {
                this.textBox1.Text = uvm.User.Name;
                if (uvm.User.Id == 1)
                {
                    this.comboBox1.Items.Add(new DropdownOption() { Id = 0, Text = "Superuser" });
                    this.comboBox1.SelectedIndex = 0;
                }
                else
                {
                    for (int i = 0; i < uvm.Roles.Count; i++)
                    {
                        this.comboBox1.Items.Add(new DropdownOption()
                        {
                            Id = uvm.Roles[i].Id,
                            Text = uvm.Roles[i].Name
                        });
                        if (uvm.User.RoleId == uvm.Roles[i].Id)
                        {
                            comboBox1.SelectedIndex = i;
                        }
                    }
                }
                this.textBox2.Text = uvm.User.Login;
                this.textBox2.ReadOnly = uvm.User.Id == 1;
                this.SaveButton.Enabled = uvm.User.Id != 1;
            }
        }

        private void HandleCloseButtonClicked(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
            this.Close();
        }
    }
}
