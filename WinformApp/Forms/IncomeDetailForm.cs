using Alaska;
using Alaska.Data;
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

namespace WinformApp.Forms
{
    public partial class IncomeDetailForm : Form
    {
        public IncomeDetailForm()
        {
            InitializeComponent();
        }

        private void HandleAmountKeyPress(object sender, KeyPressEventArgs e)
        {
            FormatHelpers.FilterOnlyNumber(e);
        }

        private async void SaveButton_Click(object sender, EventArgs e)
        {
            if (typeComboBox.SelectedIndex < 0)
            {
                MessageBox.Show("Arah Masuk / Keluar belum dipilih", "Opsi", MessageBoxButtons.OK, MessageBoxIcon.Information);
                typeComboBox.Focus();
                return;
            }
            double.TryParse(amountTextBox.Text.Trim(), out double amount);
            if (amount <= 0)
            {
                MessageBox.Show("Nilai tidak boleh kosong", "Nilai", MessageBoxButtons.OK, MessageBoxIcon.Information);
                amountTextBox.Focus();
                return;
            }
            if (categoryComboBox.SelectedItem is null)
            {
                MessageBox.Show("Kategori belum dipilih", "Nilai", MessageBoxButtons.OK, MessageBoxIcon.Information);
                amountTextBox.Focus();
                return;
            }
            var model = new CashFlowItem()
            {
                Direction = typeComboBox.SelectedIndex,
                Date = datePicker.Value,
                Amount = amount,
                Category = (int)((DropdownOption)categoryComboBox.SelectedItem).Id,
                Notes = noteTextBox.Text.Trim()
            };
            var json = await HttpClientSingleton.PostAsync("/reports/cashflows/create", JsonSerializer.Serialize(model, AppJsonSerializerContext.Default.CashFlowItem));
            var success = json.ToLower() == "true";
            if (success)
            {
                DialogResult = DialogResult.OK;
                Close();
            }
        }

        private void IncomeDetailForm_Load(object sender, EventArgs e)
        {

        }

        private async void typeComboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (typeComboBox.SelectedIndex < 0)
            {
                categoryComboBox.Items.Clear();
                return;
            }

            categoryComboBox.Items.Clear();
            var endpoint = typeComboBox.SelectedIndex == 0 ? "/trans/sales/income-categories" : "/trans/sales/expense-categories";
            using (var builder = new CostTableBuilder(await HttpClientSingleton.GetStreamAsync(endpoint)))
            {
                var categoryTable = builder.ToDataTable();
                foreach (DataRow row in categoryTable.Rows)
                {
                    var item = new DropdownOption()
                    {
                        Id = (int)row[0],
                        Text = Convert.ToString(row[1])
                    };
                    categoryComboBox.Items.Add(item);
                }
            }
        }
    }
}
