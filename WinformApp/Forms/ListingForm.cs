using Alaska;
using Alaska.Data;
using Alaska.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Text.Json;
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
        Sales,
        CashFlow,
        CostType
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
            this.panel1.Visible = Type == ListingType.Sales || Type == ListingType.CashFlow;
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
                case ListingType.Sales:
                    this.Text = "Laporan Penjualan Harian";
                    break;
                case ListingType.CashFlow:
                    this.Text = "Laporan Arus Kas";
                    break;
                case ListingType.CostType:
                    this.Text = "Kategori Biaya";
                    break;
            }
            dateTimePicker1.Value = new DateTime(DateTime.Today.Year, DateTime.Today.Month, 1, 0, 0, 0);
            dateTimePicker2.Value = new DateTime(DateTime.Today.Year, DateTime.Today.Month, DateTime.Today.Day, 23, 59, 59);
            dateTimePicker1.CloseUp += new EventHandler(this.HandleDatePickerCloseUp);
            dateTimePicker2.CloseUp += new EventHandler(this.HandleDatePickerCloseUp);
            tableLayoutPanel1.Visible = Type == ListingType.Sales || Type == ListingType.CashFlow;
            if (Type == ListingType.CashFlow)
            {
                label3.Text = "Kas Masuk";
                label4.Text = "Kasu Keluar";
                label5.Text = "Saldo Akhir";
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
                case ListingType.Sales:
                    return new SaleService();
                case ListingType.CashFlow:
                    return new CashflowService();
                case ListingType.CostType:
                    return new CostTypeService();
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
                case ListingType.Sales:
                    return new EntryDataForm();
                case ListingType.CashFlow:
                    return new IncomeDetailForm();
                case ListingType.CostType:
                    return new CostCategoryForm((CostTypeService)GetService());
            }
            return null;
        }
        internal bool UsePeriod()
        {
            return Type == ListingType.Sales || Type == ListingType.CashFlow;
        }
        internal async Task LoadDataTableAsync()
        {
            this.Cursor = Cursors.WaitCursor;
            var service = GetService();
            service.Period = CreatePeriod();
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
                        new DataTableColumnInfo("Tipe", "type", 200),
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
                case ListingType.Sales:
                    GridHelpers.InitializeDataGridColumns(this.grid, new DataTableColumnInfo[]
                    {
                        new DataTableColumnInfo("Kode", "id", 60, DataGridViewContentAlignment.MiddleCenter, "000000"),
                        new DataTableColumnInfo("Tanggal", "date", 80, DataGridViewContentAlignment.MiddleCenter, "dd-MM-yyyy"),
                        new DataTableColumnInfo("Pemasukan", "income", 100, DataGridViewContentAlignment.MiddleRight, "N0"),
                        new DataTableColumnInfo("Pengeluaran", "expense", 100, DataGridViewContentAlignment.MiddleRight, "N0"),
                        new DataTableColumnInfo("Selisih", "balance", 100, DataGridViewContentAlignment.MiddleRight, "N0"),
                        new DataTableColumnInfo("Dibuat oleh", "creator", 150),
                        new DataTableColumnInfo("Tgl. dibuat", "createdDate", 120, DataGridViewContentAlignment.MiddleRight, "dd-MM-yyyy HH:mm")
                    }, this.BindingSource);
                    break;
                case ListingType.CashFlow:
                    GridHelpers.InitializeDataGridColumns(this.grid, new DataTableColumnInfo[]
                    {
                        new DataTableColumnInfo("Tanggal", "date", 120, DataGridViewContentAlignment.MiddleCenter, "dd-MM-yyyy HH:mm"),
                        new DataTableColumnInfo("Credit", "credit", 100, DataGridViewContentAlignment.MiddleRight, "N0"),
                        new DataTableColumnInfo("Debt", "debt", 100, DataGridViewContentAlignment.MiddleRight, "N0"),
                        new DataTableColumnInfo("Saldo", "balance", 100, DataGridViewContentAlignment.MiddleRight, "N0"),
                        new DataTableColumnInfo("Keterangan", "notes", 350),
                        new DataTableColumnInfo("Dibuat oleh", "creator", 150),
                    }, this.BindingSource);
                    foreach (DataGridViewColumn column in this.grid.Columns)
                    {
                        column.SortMode = DataGridViewColumnSortMode.NotSortable;
                    }
                    break;
                case ListingType.CostType:
                    GridHelpers.InitializeDataGridColumns(this.grid, new DataTableColumnInfo[]
                    {
                        new DataTableColumnInfo("Kode", "id", 60, DataGridViewContentAlignment.MiddleCenter, "000000"),
                        new DataTableColumnInfo("Nama", "name", 200),
                        new DataTableColumnInfo("Tipe", "type", 200),
                        new DataTableColumnInfo("Dibuat oleh", "creator", 150),
                        new DataTableColumnInfo("Tgl. dibuat", "createdDate", 120, DataGridViewContentAlignment.MiddleRight, "dd-MM-yyyy HH:mm")
                    }, this.BindingSource);
                    break;
            }
            if (grid.Columns.Count > 0)
            {
                grid.Columns[0].DefaultCellStyle.BackColor = Color.FromArgb(240, 240, 240);
            }
            this.grid.ColumnHeadersDefaultCellStyle.Font = new Font(this.Font, FontStyle.Bold);
            this.grid.ColumnHeadersDefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
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

        private void HandleFormLoad(object sender, EventArgs e)
        {

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
        private async void HandleDatePickerCloseUp(object? sender, EventArgs e)
        {
            if (UsePeriod())
            {
                await LoadDataTableAsync();
            }
        }

        private Period CreatePeriod()
        {
            return new Period()
            {
                From = dateTimePicker1.Value,
                To = dateTimePicker2.Value
            };
        }

        private async void HandleDataBindingComplete(object sender, DataGridViewBindingCompleteEventArgs e)
        {
            if (e.ListChangedType == ListChangedType.PropertyDescriptorChanged)
            {
                if (Type == ListingType.Sales)
                {
                    double income = 0, expenxe = 0, balance = 0;
                    await Task.Run(() =>
                    {
                        foreach (DataGridViewRow row in this.grid.Rows)
                        {
                            income += (double)row.Cells[2].Value;
                            expenxe += (double)row.Cells[3].Value;
                            balance += (double)row.Cells[4].Value;
                        }
                    });
                    label8.Text = "Rp" + income.ToString("N0");
                    label7.Text = "Rp" + expenxe.ToString("N0");
                    label6.Text = "Rp" + balance.ToString("N0");
                }

                if (Type == ListingType.CashFlow)
                {
                    double income = 0, expense = 0, balance = 0;
                    await Task.Run(() =>
                    {
                        foreach (DataGridViewRow row in this.grid.Rows)
                        {
                            income += (double)row.Cells[1].Value;
                            expense += (double)row.Cells[2].Value;
                            balance += (double)row.Cells[3].Value;
                        }
                    });
                    label8.Text = "Rp" + income.ToString("N0");
                    label7.Text = "Rp" + expense.ToString("N0");
                    label6.Text = "Rp" + balance.ToString("N0");
                }
            }
        }

        private async void button1_Click(object sender, EventArgs e)
        {
            var dialog = new SaveFileDialog();
            dialog.Filter = "Excel Workbook(*.xlsx)|*.xlsx";
            if (dialog.ShowDialog() == DialogResult.OK)
            {
                var json = JsonSerializer.Serialize(CreatePeriod(), AppJsonSerializerContext.Default.Period);
                using (var stream = await HttpClientSingleton.PostStreamAsync("/trans/sales/export", json))
                {
                    using (var fs = new FileStream(dialog.FileName, FileMode.OpenOrCreate, FileAccess.ReadWrite))
                    {
                        await stream.CopyToAsync(fs);
                    }
                }
                if (File.Exists(dialog.FileName))
                {
                    System.Diagnostics.Process.Start(new ProcessStartInfo
                    {
                        FileName = dialog.FileName,
                        UseShellExecute = true
                    });
                }
            }
        }
    }
}
