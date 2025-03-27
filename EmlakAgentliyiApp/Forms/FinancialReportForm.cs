using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SQLite;
using DashinmazEmlakApp.Database;
using DashinmazEmlakApp.Database.Models;
using DashinmazEmlakApp.Services;
using DasinmazEmlakAgentligi.Database;
using DasinmazEmlakAgentligi.Models;

namespace DashinmazEmlakApp.Forms
{
    public partial class FinancialReportForm : Form
    {
        private readonly DatabaseManager _dbManager;
        private readonly ReportService _reportService;

        public FinancialReportForm(DatabaseManager dbManager, ReportService reportService)
        {
            InitializeComponent();

            _dbManager = dbManager;
            _reportService = reportService;

            // Set up event handlers
            this.Load += FinancialReportForm_Load;
            tabControl.SelectedIndexChanged += TabControl_SelectedIndexChanged;
        }

        private void FinancialReportForm_Load(object sender, EventArgs e)
        {
            // Set the form title
            this.Text = "Maliyyə Hesabatları";

            // Initialize date range
            dtpStartDate.Value = new DateTime(DateTime.Now.Year, DateTime.Now.Month, 1);
            dtpEndDate.Value = DateTime.Now;

            dtpTransStartDate.Value = new DateTime(DateTime.Now.Year, DateTime.Now.Month, 1);
            dtpTransEndDate.Value = DateTime.Now;

            dtpExpenseStartDate.Value = new DateTime(DateTime.Now.Year, DateTime.Now.Month, 1);
            dtpExpenseEndDate.Value = DateTime.Now;

            // Generate report
            GenerateFinancialSummary();
        }

        private void GenerateFinancialSummary()
        {
            try
            {
                DateTime startDate = dtpStartDate.Value.Date;
                DateTime endDate = dtpEndDate.Value.Date.AddHours(23).AddMinutes(59).AddSeconds(59);

                // Check if start date is not greater than end date
                if (startDate > endDate)
                {
                    MessageBox.Show("Başlanğıc tarixi bitmə tarixindən sonra ola bilməz!", "Xəta", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                // Set status
                lblStatus.Text = "Hesabat hazırlanır...";
                Application.DoEvents();

                // Generate report
                FinancialSummary summary = _reportService.GetFinancialSummary(startDate, endDate);

                // Display summary
                lblPeriod.Text = $"{startDate.ToString("dd.MM.yyyy")} - {endDate.ToString("dd.MM.yyyy")}";
                lblSalesRevenue.Text = summary.TotalSalesRevenue.ToString("N0") + " ₼";
                lblRentalRevenue.Text = summary.TotalRentalRevenue.ToString("N0") + " ₼";
                lblTotalCommission.Text = summary.TotalCommission.ToString("N0") + " ₼";
                lblTotalExpenses.Text = summary.TotalExpenses.ToString("N0") + " ₼";
                lblTotalSalaries.Text = summary.TotalSalaries.ToString("N0") + " ₼";
                lblNetProfit.Text = summary.TotalProfit.ToString("N0") + " ₼";

                // Set profit color
                lblNetProfit.ForeColor = summary.TotalProfit >= 0 ? Color.Green : Color.Red;

                // Display counts
                lblSalesCount.Text = $"{summary.SalesCount} satış əməliyyatı";
                lblRentalsCount.Text = $"{summary.RentalsCount} kirayə əməliyyatı";

                // Display top employees
                lvwTopEmployees.Items.Clear();

                foreach (var employee in summary.TopEmployees)
                {
                    ListViewItem item = new ListViewItem(employee.EmployeeName);
                    item.SubItems.Add(employee.TransactionCount.ToString());
                    item.SubItems.Add(employee.TotalCommission.ToString("N0") + " ₼");

                    lvwTopEmployees.Items.Add(item);
                }

                // Set status
                lblStatus.Text = "Hesabat hazırlandı";
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Hesabat hazırlanarkən xəta baş verdi: {ex.Message}", "Xəta", MessageBoxButtons.OK, MessageBoxIcon.Error);
                lblStatus.Text = "Xəta baş verdi";
            }
        }

        private void btnGenerateReport_Click(object sender, EventArgs e)
        {
            GenerateFinancialSummary();
        }

        private void LoadTransactions()
        {
            try
            {
                DateTime startDate = dtpTransStartDate.Value.Date;
                DateTime endDate = dtpTransEndDate.Value.Date.AddHours(23).AddMinutes(59).AddSeconds(59);

                // Check if start date is not greater than end date
                if (startDate > endDate)
                {
                    MessageBox.Show("Başlanğıc tarixi bitmə tarixindən sonra ola bilməz!", "Xəta", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                // Set status
                lblTransStatus.Text = "Əməliyyatlar yüklənir...";
                Application.DoEvents();

                // Get transactions
                List<TransactionReport> transactions = _reportService.GetTransactions(startDate, endDate);

                // Display transactions
                lvwTransactions.Items.Clear();

                foreach (var transaction in transactions)
                {
                    ListViewItem item = new ListViewItem(transaction.TransactionDate.ToString("dd.MM.yyyy"));
                    item.SubItems.Add(transaction.Type == TransactionType.Sale ? "Satış" : "Kirayə");
                    item.SubItems.Add(transaction.PropertyTitle);
                    item.SubItems.Add(transaction.PropertyAddress);
                    item.SubItems.Add(transaction.ClientName);
                    item.SubItems.Add(transaction.EmployeeName);
                    item.SubItems.Add(transaction.Amount.ToString("N0") + " ₼");
                    item.SubItems.Add(transaction.Commission.ToString("N0") + " ₼");

                    lvwTransactions.Items.Add(item);
                }

                // Set status
                lblTransStatus.Text = $"{transactions.Count} əməliyyat tapıldı";
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Əməliyyatları yükləyərkən xəta baş verdi: {ex.Message}", "Xəta", MessageBoxButtons.OK, MessageBoxIcon.Error);
                lblTransStatus.Text = "Xəta baş verdi";
            }
        }

        private void btnLoadTransactions_Click(object sender, EventArgs e)
        {
            LoadTransactions();
        }

        private void LoadExpenses()
        {
            try
            {
                DateTime startDate = dtpExpenseStartDate.Value.Date;
                DateTime endDate = dtpExpenseEndDate.Value.Date.AddHours(23).AddMinutes(59).AddSeconds(59);

                // Check if start date is not greater than end date
                if (startDate > endDate)
                {
                    MessageBox.Show("Başlanğıc tarixi bitmə tarixindən sonra ola bilməz!", "Xəta", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                // Set status
                lblExpensesStatus.Text = "Xərclər yüklənir...";
                Application.DoEvents();

                // Get expenses
                List<ExpenseReport> expenses = _reportService.GetExpenses(startDate, endDate);

                // Display expenses
                lvwExpenses.Items.Clear();

                foreach (var expense in expenses)
                {
                    ListViewItem item = new ListViewItem(expense.ExpenseDate.ToString("dd.MM.yyyy"));
                    item.SubItems.Add(expense.Category);
                    item.SubItems.Add(expense.Description);
                    item.SubItems.Add(expense.EmployeeName ?? "");
                    item.SubItems.Add(expense.Amount.ToString("N0") + " ₼");

                    lvwExpenses.Items.Add(item);
                }

                // Set status
                lblExpensesStatus.Text = $"{expenses.Count} xərc tapıldı";

                // Calculate total expenses
                double totalExpenses = expenses.Sum(e => e.Amount);
                lblTotalExpensesAmount.Text = totalExpenses.ToString("N0") + " ₼";

                // Group expenses by category
                var expensesByCategory = expenses
                    .GroupBy(e => e.Category)
                    .Select(g => new { Category = g.Key, Amount = g.Sum(e => e.Amount) })
                    .OrderByDescending(g => g.Amount);

                // Display expenses by category
                lvwExpensesByCategory.Items.Clear();

                foreach (var category in expensesByCategory)
                {
                    ListViewItem item = new ListViewItem(category.Category);
                    item.SubItems.Add(category.Amount.ToString("N0") + " ₼");

                    lvwExpensesByCategory.Items.Add(item);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Xərcləri yükləyərkən xəta baş verdi: {ex.Message}", "Xəta", MessageBoxButtons.OK, MessageBoxIcon.Error);
                lblExpensesStatus.Text = "Xəta baş verdi";
            }
        }

        private void btnLoadExpenses_Click(object sender, EventArgs e)
        {
            LoadExpenses();
        }

        private void TabControl_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (tabControl.SelectedTab == tabTransactions)
            {
                LoadTransactions();
            }
            else if (tabControl.SelectedTab == tabExpenses)
            {
                LoadExpenses();
            }
        }
    }
}
