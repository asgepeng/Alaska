using Alaska;
using Alaska.Data;
using Alaska.Models;
using AlaskaLib.Models;
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
using static System.Windows.Forms.VisualStyles.VisualStyleElement.Window;

namespace WinformApp.Forms
{
    public partial class EntryDataForm : Form
    {
        public EntryDataForm()
        {
            InitializeComponent();
            this.dateTimePicker1.Value = DateTime.Today;
        }
        private async Task LoadDataEntryAsync()
        {
            var id = this.Tag is null ? 0 : (int)Tag;
            var json = await HttpClientSingleton.GetAsync("/trans/sales/" + id.ToString());
            if (json.Length > 0)
            {
                this.DailySale = JsonSerializer.Deserialize(json, AppJsonSerializerContext.Default.DailySale);
                if (DailySale != null)
                {
                    this.dateTimePicker1.Value = this.DailySale.Date;
                    this.dailySalesItemBindingSource.DataSource = DailySale.Items;
                    this.notesTextBox.Text = DailySale.Notes;
                    this.dateTimePicker1.Enabled = DailySale.Id == 0;
                }
            }
        }
        private DailySale? DailySale { get; set; }
        private void Calculate()
        {
            if (this.DailySale is null) return;

            double balance = 0;
            var (totalIncome, totalExpense) = this.DailySale.Calculate();
            balance = totalIncome - totalExpense;
            this.incomeLabel.Text = totalIncome.ToString("N0");
            this.expenseLabel.Text = totalExpense.ToString("N0");
            this.balanceLabel.Text = balance.ToString("N0");
        }
        private async void HandleFormLoad(object sender, EventArgs e)
        {
            this.Cursor = Cursors.WaitCursor;
            await LoadDataEntryAsync();
            this.Cursor = Cursors.Default;
        }

        private void HandleDataGridCellEndEdit(object sender, DataGridViewCellEventArgs e)
        {
            this.Calculate();
        }

        private async void HandleSaveButtonClicked(object sender, EventArgs e)
        {
            if (this.DailySale is null) return;

            this.DailySale.Notes = this.notesTextBox.Text.Trim();
            if (this.DailySale.Id == 0)
            {
                this.DailySale.Date = this.dateTimePicker1.Value;
                var period = JsonSerializer.Serialize(new Period() { From = new DateTime(DailySale.Date.Year, DailySale.Date.Month, DailySale.Date.Day, 0, 0, 0) }, AppJsonSerializerContext.Default.Period);
                var json = await HttpClientSingleton.PostAsync("/trans/sales-check", period);
                int.TryParse(json, out int saleID);
                if (saleID > 0)
                {
                    MessageBox.Show($"Sudah ada entry data pada tanggal {dateTimePicker1.Value.ToString("dd/MM/yyyy")}, silakan pilih tanggal lain", "Tanggal tidak tersedia", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    this.dateTimePicker1.Focus();
                    return;
                }
            }


            var dailySale = JsonSerializer.Serialize(DailySale, AppJsonSerializerContext.Default.DailySale);
            var result = await HttpClientSingleton.PostAsync("/trans/sales-submit", dailySale);
            var success = result == "true";
            if (success)
            {
                this.DialogResult = DialogResult.OK;
                this.Close();
            }
        }

        private void HandleDataBindingComplete(object sender, DataGridViewBindingCompleteEventArgs e)
        {
            if (e.ListChangedType == ListChangedType.PropertyDescriptorChanged)
            {
                Calculate();
            }
        }

        internal void SetExpense(double value)
        {
            Calculate();
        }

        private void dataGridView1_DataError(object sender, DataGridViewDataErrorEventArgs e)
        {
            e.ThrowException = false;
        }

        private void HandleButtonExpenseClicked(object sender, EventArgs e)
        {
            if (this.DailySale is null) return;

            var Form = new EntryExpenseForm();
            Form.Sale = this.DailySale;
            Form.eForm = this;
            Form.ShowDialog();
        }
    }
}
