using Alaska;
using Alaska.Data;
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

namespace WinformApp.Forms
{
    public partial class EntryDataForm : Form
    {
        public EntryDataForm()
        {
            InitializeComponent();
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
                    this.dateLabel.Text = this.DailySale.Date.ToString("dd-MM-yyyy");
                    this.dailySalesItemBindingSource.DataSource = DailySale.Items;
                    this.notesTextBox.Text = DailySale.Notes;
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
    }
}
