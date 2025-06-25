using Alaska.Data;
using Alaska.Models;
using AlaskaLib.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Text;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using WinformApp.Data;

namespace WinformApp.Forms
{

    public partial class EntryExpenseForm : Form
    {
        private ExpenseTable table;
        private BindingSource bs;
        public EntryExpenseForm()
        {
            InitializeComponent();
            bs = new BindingSource();
            table = new ExpenseTable();
        }
        internal DailySale? Sale { get; set; }
        internal EntryDataForm? eForm { get; set; }
        private void button2_Click(object sender, EventArgs e)
        {
            table.Rows.Add(new object[] { 0, (double)0 });
            bs.MoveLast();
        }

        private async void EntryExpenseForm_Load(object sender, EventArgs e)
        {
            using (var builder = new CostTableBuilder(await HttpClientSingleton.GetStreamAsync("/trans/sales/expense-categories")))
            {
                var categoryTable = builder.ToDataTable();
                colName.DataSource = categoryTable;
                colName.DisplayMember = "name";
                colName.ValueMember = "id";
            }
            grid.AutoGenerateColumns = false;
            grid.DataSource = bs;
            if (this.Sale != null)
            {
                foreach (var item in Sale.Expenses)
                {
                    table.Rows.Add(item.ExpenseId, item.Amount);
                }
            }
            bs.DataSource = table;
            Calculate();
        }
        private void Calculate()
        {
            var total = (double)0;
            foreach (DataRow row in table.Rows)
            {
                total += (double)row[1];
            }
            this.label2.Text = total.ToString("N0");
        }
        private void button1_Click(object sender, EventArgs e)
        {
            if (this.bs.Current is null) return;
            bs.RemoveCurrent();
            Calculate();
        }

        private void SaveButton_Click(object sender, EventArgs e)
        {
            if (this.Sale is null) return;

            this.Sale.Expenses.Clear();
            foreach (DataRow row in table.Rows)
            {
                var expenseId = (int)row[0];
                var amount = (double)row[1];

                if (expenseId == 0)
                {
                    MessageBox.Show("Kategori pengeluaran tidak boleh kosong", "Data Kosong", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;
                }
                if (amount == 0)
                {
                    MessageBox.Show("Nilai pengeluaran tidak boleh kosong", "Nilai", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;
                }
                this.Sale.Expenses.Add(new DailyExpenseItem()
                {
                    ExpenseId = expenseId,
                    Amount = amount
                });

            }
            UpdateParent();
            this.DialogResult = DialogResult.OK;
            this.Close();
        }

        private void grid_CellEndEdit(object sender, DataGridViewCellEventArgs e)
        {
            Calculate();
        }
        private void UpdateParent()
        {
            if (eForm != null)
            {
                eForm.SetExpense(GetTotal());
            }
        }
        private double GetTotal()
        {
            double total = 0;
            foreach (DataRow row in table.Rows)
            {
                total += (double)row[1];
            }
            return total;
        }

        private void grid_CellLeave(object sender, DataGridViewCellEventArgs e)
        {
            UpdateParent();
        }

        private void grid_DataError(object sender, DataGridViewDataErrorEventArgs e)
        {
            e.Cancel = true;
        }
    }
    internal class ExpenseTable : DataTable
    {
        internal ExpenseTable()
        {
            Columns.Add("expenseId", typeof(int));
            Columns.Add("amount", typeof(double));
            Columns[1].AllowDBNull = false;
            Columns[1].DefaultValue = 0;
        }
    }

    public class CostTableBuilder : DataTableBuilder
    {
        public CostTableBuilder(Stream stream) : base(stream)
        {

        }
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
