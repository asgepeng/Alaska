﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace AlaskaLib.Models
{
    public class DailySale
    {
        [JsonPropertyName("id")]
        public int Id { get; set; } = 0;

        [JsonPropertyName("date")]
        public DateTime Date { get; set; } = DateTime.Now;
        [JsonPropertyName("cashinId")] public int CashinId { get; set; } = 0;
        [JsonPropertyName("cashoutId")] public int CashoutId { get; set; } = 0;
        [JsonPropertyName("notes")]
        public string Notes { get; set; } = "";
        [JsonPropertyName("items")]
        public List<DailySalesItem> Items { get; set; } = new List<DailySalesItem>();
        [JsonPropertyName("expenses")]public List<DailyExpenseItem> Expenses { get; set; } = new List<DailyExpenseItem>();
        public (double totalIncome, double totalExpense) Calculate()
        {
            double totalIncome = 0, totalExpense = 0;
            foreach (var item in this.Items)
            {
                totalIncome += item.Income;
            }
            foreach (var item in this.Expenses)
            {
                totalExpense += item.Amount;
            }
            return (totalIncome, totalExpense);
        }
    }

    public class DailySalesItem
    {
        [JsonPropertyName("id")]
        public int Id { get; set; } = 0;

        [JsonPropertyName("outletId")]
        public int OutletId { get; set; } = 0;

        [JsonPropertyName("outletName")]
        public string OutletName { get; set; } = "";

        [JsonPropertyName("waiterId")]
        public int WaiterId { get; set; } = 0;

        [JsonPropertyName("waiterName")]
        public string WaiterName { get; set; } = "";
        [JsonPropertyName("outletType")]
        public string OutletType { get; set; } = "";

        [JsonPropertyName("income")]
        public double Income { get; set; } = 0;

        [JsonPropertyName("expense")]
        public double Expense { get; set; } = 0;

        [JsonPropertyName("balance")]
        public double Balance => Income - Expense;

        [JsonPropertyName("notes")]
        public string Notes { get; set; } = "";
    }

    public class DailyExpenseItem
    {
        [JsonPropertyName("id")] public int Id { get; set; } = 0;
        [JsonPropertyName("expenseId")] public int ExpenseId { get; set; } = 0;
        [JsonPropertyName("amount")] public double Amount { get; set; } = 0;
    }
}
