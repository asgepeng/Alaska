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
    public partial class CostCategoryForm : Form
    {
        internal CostCategoryForm(CostTypeService service_)
        {
            InitializeComponent();
            this.service = service_;
        }
        private readonly CostTypeService service;
        private CostCategory? CostCategory { get; set; }
        private async void SaveButton_Click(object sender, EventArgs e)
        {
            if (this.Text.Trim() == "")
            {
                MessageBox.Show("Nama tidak boleh kosong", "Nama", MessageBoxButtons.OK, MessageBoxIcon.Information);
                this.textBox1.Focus();
                return;
            }
            if (this.CostCategory != null && this.CostCategory.Id == 0 && this.comboBox1.SelectedIndex < 0)
            {
                MessageBox.Show("Tipe biaya belum dipilih", "Tipe", MessageBoxButtons.OK, MessageBoxIcon.Information);
                comboBox1.Focus();
                return;
            }
            if (CostCategory is null) return;

            CostCategory.Name = this.textBox1.Text.Trim();
            if (CostCategory.Id == 0) this.CostCategory.Type = this.comboBox1.SelectedIndex;
            if (CostCategory.Id > 0)
            {
                var cr = await service.UpdateAsync(CostCategory);
                if (cr.Success)
                {
                    this.DialogResult = DialogResult.OK;
                    this.Close();
                }
            }
            else
            {
                var result = await service.CreateAsync(CostCategory);
                if (result != null)
                {
                    var cr = (CommonResult)result;
                    if (cr.Success)
                    {
                        this.DialogResult = DialogResult.OK;
                        this.Close();
                    }
                }
            }
        }

        private async void CostCategoryForm_Load(object sender, EventArgs e)
        {
            var id = this.Tag is null ? 0 : (int)this.Tag;

            if (id > 0)
            {
                var model = await service.GetByIdAsync(id);
                if (model != null)
                {
                    this.CostCategory = (CostCategory)model;
                }
            }
            if (this.CostCategory is null) this.CostCategory = new CostCategory();
            this.textBox1.Text = this.CostCategory.Name;
            this.comboBox1.SelectedIndex = this.CostCategory.Type;
            this.comboBox1.Enabled = this.CostCategory.Id == 0;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
            this.Close();
        }
    }
}
