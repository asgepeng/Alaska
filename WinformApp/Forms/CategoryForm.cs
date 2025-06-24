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
    public partial class CategoryForm : Form
    {
        BindingSource bs;
        public CategoryForm()
        {
            InitializeComponent();
            bs = new BindingSource();
            this.grid.DataSource = bs;
        }
        private async Task LoadCategoriesAsync()
        {
            using (var builder = new CategoryTableBuilder(await HttpClientSingleton.GetStreamAsync("/master-data/categories")))
            {
                this.bs.DataSource = builder.ToDataTable();
            }
        }

        private async void button1_Click(object sender, EventArgs e)
        {
            var dialog = new CategoryDetailForm();
            if (dialog.ShowDialog() == DialogResult.OK)
            {
                await LoadCategoriesAsync();
            }
        }

        private async void button2_Click(object sender, EventArgs e)
        {
            if (bs.Current is null) return;
            var id = (int)((DataRowView)bs.Current)[0];
            var json = await (HttpClientSingleton.DeleteAsync("/master-data/categories/" + id.ToString()));
            var cr = json.Length > 0 ? JsonSerializer.Deserialize(json, AppJsonSerializerContext.Default.CommonResult) : null;
            if (cr != null)
            {
                if (!cr.Success)
                {
                    MessageBox.Show(cr.Message, "Gagal", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else
                {
                    await LoadCategoriesAsync();
                }
            }
        }

        private async void CategoryForm_Load(object sender, EventArgs e)
        {
            this.grid.AutoGenerateColumns = false;
            await LoadCategoriesAsync();
        }
    }

    public class CategoryTableBuilder : DataTableBuilder
    {
        public CategoryTableBuilder(Stream stream) : base(stream) { }
        public override DataTable ToDataTable()
        {
            AddColumn("id", typeof(int));
            AddColumn("name", typeof(string));

            while (Read())
            {
                var values = new object[]
                {
                    ReadInt32(), ReadString()
                };
                AddRow(values);
            }
            return base.ToDataTable();
        }
    }
}
