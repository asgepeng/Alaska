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
using System.Windows.Forms.VisualStyles;
using WinformApp.Data;

namespace WinformApp.Forms
{
    public partial class OutletDetailForm : Form
    {
        private readonly OutletService outletService;
        public OutletDetailForm()
        {
            InitializeComponent();
            outletService = new OutletService();
        }
        private Outlet? Outlet { get; set; }
        private async void FormLoadHandler(object sender, EventArgs e)
        {
            var id = this.Tag is null ? 0 : (int)Tag;
            var result = await outletService.GetByIdAsync(id);
            if (result != null)
            {
                var ovm = (OutletViewModel)result;
                this.nameTextBox.Text = ovm.Outlet.Name;
                this.addressTextBox.Text = ovm.Outlet.Address;
                for (int i = 0; i < ovm.Waiters.Count; i++)
                {
                    this.waiterComboBox.Items.Add(ovm.Waiters[i]);
                    if (ovm.Outlet.Waiter == ovm.Waiters[i].Id) this.waiterComboBox.SelectedIndex = i;
                }
                this.Outlet = ovm.Outlet;
            }
        }

        private async void HandleLoginButtonClick(object sender, EventArgs e)
        {
            if (nameTextBox.Text.Trim() == "")
            {
                MessageBox.Show("Nama outlet tiak boleh kosong", "Nama outlet", MessageBoxButtons.OK, MessageBoxIcon.Information);
                this.nameTextBox.Focus();
                return;
            }

            if (Outlet is null) Outlet = new Outlet();
            
            Outlet.Name = this.nameTextBox.Text.Trim();
            Outlet.Address = this.addressTextBox.Text.Trim();
            Outlet.Waiter = this.waiterComboBox.SelectedItem is null ? 0 : ((DropdownOption)this.waiterComboBox.SelectedItem).Id;
            if (Outlet.Id > 0)
            {
                var result = await outletService.UpdateAsync(Outlet);
                if (result.Success)
                {
                    this.DialogResult = DialogResult.OK;
                    this.Close();
                }
            }
            else
            {
                var result = await outletService.CreateAsync(Outlet);
                if (result != null)
                {
                    var outlet = (Outlet)result;
                    if (outlet.Id > 0)
                    {
                        this.DialogResult = DialogResult.OK;
                        this.Close();
                    }
                }
            }
        }
    }
}
