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
    public partial class CategoryDetailForm : Form
    {
        public CategoryDetailForm()
        {
            InitializeComponent();
        }
        public ProductCategory? Category { get; set; }

        private void HandleFormLoad(object sender, EventArgs e)
        {
            
            if (Category != null) this.nameTextBox.Text = Category.Name;
        }

        private async void HandleSaveButtonClicked(object sender, EventArgs e)
        {
            if (this.nameTextBox.Text.Trim()== "")
            {
                MessageBox.Show("Nama kategori tidak boleh kosong", "Nama produk", MessageBoxButtons.OK, MessageBoxIcon.Information);
                this.nameTextBox.Focus();
                return;
            }
            if (this.Category is null) Category = new ProductCategory();
            Category.Name = this.nameTextBox.Text.Trim();

            var json = "";
            var jsonCategory = JsonSerializer.Serialize(Category, AppJsonSerializerContext.Default.ProductCategory);
            if (Category.Id > 0)
            {
                json = await HttpClientSingleton.PutAsync("/master-data/categories", jsonCategory);
            }
            else
            {
                json = await HttpClientSingleton.PostAsync("/master-data/categories", jsonCategory);
            }

            var cr = json.Length > 0 ? JsonSerializer.Deserialize(json, AppJsonSerializerContext.Default.CommonResult) : null;
            if (cr != null)
            {
                if (!cr.Success)
                {
                    MessageBox.Show(cr.Message, "Gagal", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else
                {
                    this.DialogResult = DialogResult.OK;
                    this.Close();
                }
            }
        }
    }
}
