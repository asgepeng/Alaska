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
    public partial class WaiterDetailForm : Form
    {
        private readonly WaiterService waiterService;
        public WaiterDetailForm()
        {
            InitializeComponent();
            waiterService = new WaiterService();
        }
        private void SetEnableControl(bool enable)
        {
            foreach (Control control in this.Controls)
            {
                control.Enabled = enable;
            }
            if (enable) this.Cursor = Cursors.Default;
            else this.Cursor = Cursors.WaitCursor;
        }
        private async void loginButton_Click(object sender, EventArgs e)
        {
            SetEnableControl(false);
            if (this.textBox1.Text.Trim() == "")
            {
                MessageBox.Show("Nama tidak boleh kosong", "Nama waiter", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            var model = new Waiter()
            {
                Name = this.textBox1.Text.Trim(),
                StreetAddress = this.textBox2.Text.Trim(),
                Phone = this.textBox3.Text.Trim(),
                Email = this.textBox4.Text.Trim()
            };
            if (this.Tag is null)
            {
                var result = await waiterService.CreateAsync(model);
                var success = false;
                if (result != null)
                {
                    model = (Waiter)result;
                    if (model.Id > 0)
                    {
                        success = true;
                    }
                }
                if (!success)
                {
                    SetEnableControl(true);
                    return;
                }
            }
            else
            {
                model.Id = Tag is null ? 0 : (int)Tag;
                var result = await waiterService.UpdateAsync(model);
                if (!result.Success)
                {
                    SetEnableControl(true);
                    return;
                }
            }

            this.DialogResult = DialogResult.OK;
            this.Close();
        }

        private async void WaiterDetailForm_Load(object sender, EventArgs e)
        {
            if (this.Tag != null)
            {
                SetEnableControl(false);
                var id = (int)this.Tag;
                var result = await waiterService.GetByIdAsync(id);
                if (result != null)
                {
                    var waiter = (Waiter)result;
                    this.textBox1.Text = waiter.Name;
                    this.textBox2.Text = waiter.StreetAddress;
                    this.textBox3.Text = waiter.Phone;
                    this.textBox4.Text = waiter.Email;
                }
                SetEnableControl(true);
            }
        }
    }
}
