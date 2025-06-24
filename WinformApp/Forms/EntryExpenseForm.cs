using Alaska.Data;
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
        internal EntryDataForm? eForm { get; set; }
        private void button2_Click(object sender, EventArgs e)
        {
            table.Rows.Add(new object[] { 0, (double)0 });
        }

        private async void EntryExpenseForm_Load(object sender, EventArgs e)
        {
            using (var builder = new CostTypeTableBuilder(await HttpClientSingleton.GetStreamAsync("/master-data/expense-categories")))
            {
                var categoryTable = builder.ToDataTable();
                colName.DataSource = categoryTable;
                colName.DisplayMember = "name";
                colName.ValueMember = "id";
            }
            grid.AutoGenerateColumns = false;
            grid.DataSource = bs;
            bs.DataSource = table;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (this.bs.Current is null) return;
            bs.RemoveCurrent();
            UpdateParent();
        }

        private void SaveButton_Click(object sender, EventArgs e)
        {

        }

        private void grid_CellEndEdit(object sender, DataGridViewCellEventArgs e)
        {
            UpdateParent();
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
}
