using Alaska.Models;
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
    public enum ListingType
    {
        Product,
        Outlet,
        Waiter,
        Income,
        Expense
    }
    public partial class ListingForm : Form
    {
        public ListingType Type { get; set; } = ListingType.Product;
        internal BindingSource BindingSource { get; }
        public ListingForm(ListingType tp)
        {
            InitializeComponent();
            BindingSource = new BindingSource();
            this.Type = tp;
            this.grid.DataSource = BindingSource;
            this.InitializeColumns();
            switch (Type)
            {
                case ListingType.Product:
                    this.Text = "Daftar Produk";
                    break;
                case ListingType.Outlet:
                    this.Text = "Daftar Outlet";
                    break;
                case ListingType.Waiter:
                    this.Text = "Daftar Waiter";
                    break;
                case ListingType.Income:
                    this.Text = "Daftar Pemasukan";
                    break;
                case ListingType.Expense:
                    this.Text = "Daftar Pengeluaran";
                    break;
            }
        }
        private IService GetService()
        {
            switch (this.Type)
            {
                case ListingType.Product:
                    return new ProductService();
                case ListingType.Outlet:
                    return new OutletService();
                case ListingType.Waiter:
                    return new WaiterService();
                case ListingType.Income:
                    return new IncomeService();
                case ListingType.Expense:
                    return new ExpenseService();
            }
            return new ProductService();
        }
        private Form? GetFormByType()
        {
            switch (this.Type)
            {
                case ListingType.Product:
                    return new ProductDetailForm();
                case ListingType.Outlet:
                    return new OutletDetailForm();
                case ListingType.Waiter:
                    return new WaiterDetailForm();
                case ListingType.Income:
                    return new IncomeDetailForm();
                case ListingType.Expense:
                    return new ExpenseDetailForm();
            }
            return null;
        }
        internal async Task LoadDataTableAsync()
        {
            this.Cursor = Cursors.WaitCursor;
            var service = GetService();
            this.BindingSource.DataSource = await service.GetDataDataTableAsync();
            this.Cursor = Cursors.Default;
        }
        private void InitializeColumns()
        {
            switch (this.Type)
            {
                case ListingType.Product:
                    GridHelpers.InitializeDataGridColumns(this.grid, new DataTableColumnInfo[]
                    {
                        new DataTableColumnInfo("Kode", "id", 60, DataGridViewContentAlignment.MiddleCenter, "000000"),
                        new DataTableColumnInfo("Nama Produk", "name", 250),
                        new DataTableColumnInfo("SKU", "sku", 150),
                        new DataTableColumnInfo("Kategori", "category", 150),
                        new DataTableColumnInfo("Stok", "stock", 80, DataGridViewContentAlignment.MiddleRight),
                        new DataTableColumnInfo("Satuan", "unit", 150),
                        new DataTableColumnInfo("Harga", "price", 100, DataGridViewContentAlignment.MiddleRight, "N0"),
                        new DataTableColumnInfo("Dibuat oleh", "creator", 150),
                        new DataTableColumnInfo("Tgl. dibuat", "createdDate", 120, DataGridViewContentAlignment.MiddleRight, "dd-MM-yyyy HH:mm"),
                        new DataTableColumnInfo("Terakhir diubah", "lastModified", 120, DataGridViewContentAlignment.MiddleRight, "dd-MM-yyyy HH:mm")
                    }, this.BindingSource);
                    break;
                case ListingType.Outlet:
                    GridHelpers.InitializeDataGridColumns(this.grid, new DataTableColumnInfo[]
                    {
                        new DataTableColumnInfo("Kode", "id", 60, DataGridViewContentAlignment.MiddleCenter, "000000"),
                        new DataTableColumnInfo("Nama", "name", 200),
                        new DataTableColumnInfo("Lokasi", "location", 300),
                        new DataTableColumnInfo("Waiter", "waiter", 200),
                        new DataTableColumnInfo("Dibuat oleh", "createdBy", 150),
                        new DataTableColumnInfo("Tgl. dibuat", "createdDate", 120, DataGridViewContentAlignment.MiddleRight, "dd-MM-yyyy HH:mm")
                    }, this.BindingSource);
                    break;
                case ListingType.Waiter:
                    GridHelpers.InitializeDataGridColumns(this.grid, new DataTableColumnInfo[]
                    {
                        new DataTableColumnInfo("Kode", "id", 60, DataGridViewContentAlignment.MiddleCenter, "000000"),
                        new DataTableColumnInfo("Nama", "name", 200),
                        new DataTableColumnInfo("Alamat", "streetAddress", 300),
                        new DataTableColumnInfo("Telp", "phone", 120),
                        new DataTableColumnInfo("Dibuat oleh", "createdBy", 150),
                        new DataTableColumnInfo("Tgl. dibuat", "createdDate", 120, DataGridViewContentAlignment.MiddleRight, "dd-MM-yyyy HH:mm")
                    });
                    break;
            }
            if (grid.Columns.Count > 0)
            {
                grid.Columns[0].DefaultCellStyle.BackColor = Color.FromArgb(240, 240, 240);
            }
        }
        internal async Task OpenNewDialog()
        {
            var dialog = GetFormByType();
            if (dialog != null)
            {
                if (dialog.ShowDialog() == DialogResult.OK)
                {
                    await this.LoadDataTableAsync();
                }
            }
        }
        private void OpenDetailDialog()
        {

        }
        internal async Task Delete()
        {
            if (this.BindingSource.Current != null)
            {
                if (MessageBox.Show("Yakin ingin menghapus record secara permanen dari database?", "Hapus", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                {
                    var service = GetService();
                    var id = (int)((DataRowView)this.BindingSource.Current)[0];
                    var json = await service.DeleteAsync(id);
                    if (json.Success)
                    {
                        await this.LoadDataTableAsync();
                    }
                }
            }
        }

        private async void ListingForm_Load(object sender, EventArgs e)
        {
            await this.LoadDataTableAsync();
        }

        private async void HandleCellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                var form = GetFormByType();
                if (form is null) return;

                int id = this.BindingSource.Current != null ? (int)((DataRowView)this.BindingSource.Current)[0] : 0;
                if (id > 0) form.Tag = id;
                if (form.ShowDialog() == DialogResult.OK)
                {
                    await this.LoadDataTableAsync();
                }
            }
        }
    }
}
