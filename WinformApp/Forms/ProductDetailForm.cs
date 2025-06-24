using Alaska;
using Alaska.Data;
using Alaska.Models;
using AlaskaLib.Models;
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
using WinformApp.Data;

namespace WinformApp.Forms
{
    public partial class ProductDetailForm : Form
    {
        private readonly ProductService productService;
        private int imageIndex = 0;
        public List<string> ProductImages { get; } = new List<string>();
        public ProductDetailForm()
        {
            InitializeComponent();
            productService = new ProductService();
        }
        private void SetControlsEnable(bool enable)
        {
            foreach (Control control in this.Controls)
            {
                control.Enabled = enable;
            }
            if (enable) Cursor = Cursors.Default;
            else
            {
                Cursor = Cursors.WaitCursor;
            }
        }
        private void NextImage()
        {
            if (imageIndex < this.ProductImages.Count - 1)
            {
                imageIndex++;
                ShowImage();
            }
        }
        private void PreviousImage()
        {
            if (imageIndex > 0)
            {
                imageIndex--;
                ShowImage();
            }
        }
        private void ShowImage()
        {
            this.productImage.LoadAsync(string.Concat(My.Application.ApiUrl, "/images/", this.ProductImages[this.imageIndex]));
        }

        private async void HandleSaveButtonClicked(object sender, EventArgs e)
        {
            if (this.skuTextBox.Text.Trim() == "")
            {
                MessageBox.Show("SKU tidak boleh kosong", "SKU", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            if (this.nameTextBox.Text.Trim() == "")
            {
                MessageBox.Show("Nama produk tidak boleh kosong", "Nama", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            if (this.categoryComboBox.SelectedIndex == -1)
            {
                MessageBox.Show("Kategori produk belum dipilih", "Kategori", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            if (this.unitComboBox.Text.Trim() == "")
            {
                MessageBox.Show("Satuan barang tidak boleh kosong", "Satuan barang", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            Product product = Tag is null ? new Product() : (Product)Tag;
            bool isUpdate = product.Id > 0;

            product.SKU = this.skuTextBox.Text.Trim();
            product.Name = this.nameTextBox.Text.Trim();
            product.Category = this.categoryComboBox.SelectedItem is null ? 0 : ((ProductCategory)this.categoryComboBox.SelectedItem).Id;
            product.Unit = this.unitComboBox.Text;
            product.Description = this.descriptionTextBox.Text.Trim();

            double.TryParse(this.priceTextBox.Text.Replace(".", "").Replace(",", ""), out double productPrice);
            double.TryParse(this.basicpriceTextBox.Text.Replace(".", "").Replace(",", ""), out double costAverage);

            int.TryParse(this.stockTextBox.Text.Trim(), out int stock);
            int.TryParse(this.minstockTextBox.Text, out int productMinStock);
            int.TryParse(this.maxstockTextBox.Text, out int productMaxStock);

            product.Price = productPrice;
            product.BasicPrice = costAverage;
            product.Stock = stock;
            product.MinStock = productMinStock;
            product.MaxStock = productMaxStock;
            product.IsActive = this.isactiveCheckBox.Checked;
            product.Images.Clear();

            if (this.ProductImages.Count > 0)
            {
                product.Images.AddRange(this.ProductImages.ToArray());
            }
            if (isUpdate)
            {
                var commonResult = await productService.UpdateAsync(product);
                if (commonResult != null && commonResult.Success)
                {
                    this.DialogResult = DialogResult.OK;
                    this.Close();
                }
            }
            else
            {
                var result = (await productService.CreateAsync(product));
                if (result != null && ((Product)result).Id > 0)
                {
                    this.DialogResult = DialogResult.OK;
                    this.Close();
                }
            }
        }

        private async void HandleFormLoad(object sender, EventArgs e)
        {
            this.SetControlsEnable(false);
            this.productImage.Image = null;
            this.ProductImages.Clear();
            this.imageIndex = -1;

            var id = Tag is null ? 0 : (int)Tag;
            int categoryIndex = -1, categoryId = -1;
            var obj = await this.productService.GetByIdAsync(id);
            if (obj is null) return;

            ProductViewModel pvm = (ProductViewModel)obj;
            if (pvm.Product is null) pvm.Product = new Product();

            this.skuTextBox.Text = pvm.Product.SKU;
            this.nameTextBox.Text = pvm.Product.Name;
            this.descriptionTextBox.Text = pvm.Product.Description;
            this.basicpriceTextBox.Text = pvm.Product.BasicPrice.ToString("N0");
            this.priceTextBox.Text = pvm.Product.Price.ToString("N0");
            this.minstockTextBox.Text = pvm.Product.MinStock.ToString();
            this.maxstockTextBox.Text = pvm.Product.MaxStock.ToString();
            this.isactiveCheckBox.Checked = pvm.Product.IsActive;
            if (pvm.Product.Images.Count > 0)
            {
                this.ProductImages.AddRange(pvm.Product.Images.ToArray());
                try
                {
                    this.imageIndex = 0;
                    this.ShowImage();
                }
                catch (Exception) { }
            }
            categoryId = pvm.Product.Category;
            this.categoryComboBox.Items.Clear();
            if (pvm.Categories != null)
            {
                for (int i = 0; i < pvm.Categories.Count; i++)
                {
                    if (pvm.Categories[i].Id == categoryId)
                    {
                        categoryIndex = i;
                    }
                    this.categoryComboBox.Items.Add(pvm.Categories[i]);
                }
            }
            this.categoryComboBox.SelectedIndex = categoryIndex;
            this.unitComboBox.Items.Clear();
            if (pvm.Units != null)
            {
                for (int i = 0; i < pvm.Units.Count; i++)
                {
                    this.unitComboBox.Items.AddRange(pvm.Units.ToArray());
                }
            }
            this.stockTextBox.Text = pvm.Product.Stock.ToString();
            this.unitComboBox.Text = pvm.Product != null ? pvm.Product.Unit : "";
            this.Tag = pvm.Product;
            this.SetControlsEnable(true);
        }
        private void HandleSaveImageButtonClicked(object sender, EventArgs e)
        {
            if (this.productImage == null) return;
            OpenFileDialog dlg = new OpenFileDialog();
            if (dlg.ShowDialog() == DialogResult.OK)
            {
                this.productImage.Image.Save(dlg.FileName);
            }
        }

        private void HandleCloseButonClicked(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
            this.Close();
        }

        private void HandleKeyPressed(object sender, KeyPressEventArgs e)
        {
            FormatHelpers.FilterOnlyNumber(e);
        }

        private async void HandleCategoryButtonClicked(object sender, EventArgs e)
        {
            CategoryForm form = new CategoryForm();
            form.ShowDialog();

            var id = this.categoryComboBox.SelectedItem is null ? 0 : (int)((ProductCategory)this.categoryComboBox.SelectedItem).Id;
            using (var builder = new CategoryTableBuilder(await HttpClientSingleton.GetStreamAsync("/master-data/categories")))
            {
                var table = builder.ToDataTable();
                this.categoryComboBox.Items.Clear();
                for (int i = 0; i < table.Rows.Count; i++)
                {
                    var category = new ProductCategory()
                    {
                        Id = (int)table.Rows[i][0],
                        Name = table.Rows[i][1] as string ?? ""
                    };
                    this.categoryComboBox.Items.Add(category);
                    if (category.Id == id) this.categoryComboBox.SelectedIndex = i;
                }
            }
        }

        private async void HandleButtonImageClicked(object sender, EventArgs e)
        {
            using var openFileDialog = new OpenFileDialog
            {
                Filter = "Image Files (*.jpg; *.jpeg; *.png; *.gif; *.bmp)|*.jpg;*.jpeg;*.png;*.gif;*.bmp",
                Title = "Select an Image to Upload"
            };
            if (openFileDialog.ShowDialog() == DialogResult.OK)
            {
                string filePath = openFileDialog.FileName;
                try
                {
                    CommonResult? result = await UploadImageAsync(filePath);
                    if (result != null && result.Success)
                    {
                        this.productImage.LoadAsync(result.Message);
                        int pos = result.Message.LastIndexOf("/");
                        string filename = pos >= 0 ? result.Message.Substring(pos + 1) : string.Empty;
                        if (!string.IsNullOrEmpty(filename))
                        {
                            this.imageIndex = this.ProductImages.Count - 1;
                            this.ProductImages.Add(filename);
                        }
                    }
                }
                catch (Exception ex)
                {
                    Clipboard.SetText(ex.Message);
                    MessageBox.Show($"An error occurred: {ex.ToString()}", "Upload Failed", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private async Task<CommonResult?> UploadImageAsync(string filePath)
        {
            using var multipartContent = new MultipartFormDataContent();
            using HttpClient client = new HttpClient();
            // Add the file to the request
            var fileContent = new StreamContent(File.OpenRead(filePath));
            fileContent.Headers.ContentType = new System.Net.Http.Headers.MediaTypeHeaderValue("application/octet-stream");
            multipartContent.Add(fileContent, "file", Path.GetFileName(filePath));

            // Send the POST request
            string uploadUrl = string.Concat(My.Application.ApiUrl, "/documents/images");
            var response = await client.PostAsync(uploadUrl, multipartContent);

            // Ensure the response is successful
            response.EnsureSuccessStatusCode();

            // Read and return the response message
            string jsonResult = await response.Content.ReadAsStringAsync();
            CommonResult? result = JsonSerializer.Deserialize(jsonResult, AppJsonSerializerContext.Default.CommonResult);
            return result;
        }
    }
}
