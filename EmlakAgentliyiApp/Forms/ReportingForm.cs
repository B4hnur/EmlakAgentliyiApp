using System;
using System.Collections.Generic;
using System.Drawing;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Windows.Forms.DataVisualization.Charting;
using EmlakAgentliyiApp.Models;
using EmlakAgentliyiApp.Services;
using EmlakAgentliyiApp.Utils;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace EmlakAgentliyiApp.Forms
{
    public partial class ReportingForm : Form
    {
        private readonly DatabaseService _databaseService;
        private readonly FinancialCalculationService _financialService;
        private List<Transaction> _transactions;
        private List<Expense> _expenses;
        private List<Employee> _employees;
        private List<Client> _clients;
        private List<Property> _properties;

        public ReportingForm()
        {
            InitializeComponent();

            _databaseService = new DatabaseService();
            _financialService = new FinancialCalculationService();
            _transactions = new List<Transaction>();
            _expenses = new List<Expense>();
            _employees = new List<Employee>();
            _clients = new List<Client>();
            _properties = new List<Property>();
        }

        private async void ReportingForm_Load(object sender, EventArgs e)
        {
            // Set initial reporting period (current month by default)
            dtpStartDate.Value = new DateTime(DateTime.Now.Year, DateTime.Now.Month, 1);
            dtpEndDate.Value = DateTime.Now;

            cmbReportType.Items.Clear();
            cmbReportType.Items.AddRange(new string[] {
                "Ümumi maliyyə hesabatı",
                "Gəlir hesabatı",
                "Xərc hesabatı",
                "İşçi fəaliyyəti hesabatı",
                "Əmlak satış hesabatı",
                "Müştəri fəaliyyəti hesabatı"
            });
            cmbReportType.SelectedIndex = 0;

            // Initialize charts
            InitializeCharts();

            // Load data 
            await LoadAllData();

            // Generate report
            await GenerateReport();
        }

        private void InitializeCharts()
        {
            // Initialize Revenue vs Expenses chart
            chartRevenueExpenses.Series.Clear();

            var revenueSeries = new Series("Gəlir")
            {
                ChartType = SeriesChartType.Column,
                Color = Color.FromArgb(46, 204, 113),
                IsValueShownAsLabel = true
            };

            var expensesSeries = new Series("Xərclər")
            {
                ChartType = SeriesChartType.Column,
                Color = Color.FromArgb(231, 76, 60),
                IsValueShownAsLabel = true
            };

            chartRevenueExpenses.Series.Add(revenueSeries);
            chartRevenueExpenses.Series.Add(expensesSeries);

            chartRevenueExpenses.ChartAreas[0].AxisX.MajorGrid.LineColor = Color.LightGray;
            chartRevenueExpenses.ChartAreas[0].AxisY.MajorGrid.LineColor = Color.LightGray;
            chartRevenueExpenses.ChartAreas[0].AxisX.LabelStyle.Font = new Font("Segoe UI", 8);
            chartRevenueExpenses.ChartAreas[0].AxisY.LabelStyle.Font = new Font("Segoe UI", 8);
            chartRevenueExpenses.ChartAreas[0].AxisY.Title = "Məbləğ (₼)";
            chartRevenueExpenses.ChartAreas[0].AxisX.Title = "Dövr";

            // Initialize Property Types Pie Chart
            chartPropertyTypes.Series.Clear();

            var propertySeries = new Series("Əmlak növləri")
            {
                ChartType = SeriesChartType.Pie,
                IsValueShownAsLabel = true
            };

            chartPropertyTypes.Series.Add(propertySeries);
            chartPropertyTypes.Legends[0].Docking = Docking.Bottom;
            chartPropertyTypes.Legends[0].Alignment = StringAlignment.Center;
        }

        private async Task LoadAllData()
        {
            try
            {
                lblStatus.Text = "Məlumatlar yüklənir...";
                progressBar.Visible = true;

                // Load all data concurrently
                var dataLoadingTasks = new List<Task>
                {
                    Task.Run(() =>
                    {
                        _transactions = _databaseService.GetAllTransactions();
                    }),
                    Task.Run(() =>
                    {
                        _expenses = _databaseService.GetAllExpenses();
                    }),
                    Task.Run(() =>
                    {
                        _employees = _databaseService.GetAllEmployees();
                    }),
                    Task.Run(() =>
                    {
                        _clients = _databaseService.GetAllClients();
                    }),
                    Task.Run(() =>
                    {
                        _properties = _databaseService.GetAllProperties();
                    })
                };

                await Task.WhenAll(dataLoadingTasks);

                lblStatus.Text = "Məlumatlar hazır";
                progressBar.Visible = false;
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Məlumatları yükləyərkən xəta baş verdi: {ex.Message}", "Xəta",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
                lblStatus.Text = "Xəta baş verdi!";
                progressBar.Visible = false;
            }
        }

        private async void btnGenerateReport_Click(object sender, EventArgs e)
        {
            await GenerateReport();
        }

        private async Task GenerateReport()
        {
            try
            {
                lblStatus.Text = "Hesabat yaradılır...";
                progressBar.Visible = true;

                // Get date range
                DateTime startDate = dtpStartDate.Value.Date;
                DateTime endDate = dtpEndDate.Value.Date.AddDays(1).AddSeconds(-1); // End of the day

                if (startDate > endDate)
                {
                    MessageBox.Show("Başlanğıc tarixi bitmə tarixindən sonra ola bilməz!", "Xəbərdarlıq",
                        MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                // Generate based on report type
                int reportType = cmbReportType.SelectedIndex;

                switch (reportType)
                {
                    case 0: // Ümumi maliyyə hesabatı
                        await GenerateFinancialReport(startDate, endDate);
                        break;
                    case 1: // Gəlir hesabatı
                        await GenerateRevenueReport(startDate, endDate);
                        break;
                    case 2: // Xərc hesabatı
                        await GenerateExpenseReport(startDate, endDate);
                        break;
                    case 3: // İşçi fəaliyyəti hesabatı
                        await GenerateEmployeeReport(startDate, endDate);
                        break;
                    case 4: // Əmlak satış hesabatı
                        await GeneratePropertySalesReport(startDate, endDate);
                        break;
                    case 5: // Müştəri fəaliyyəti hesabatı
                        await GenerateClientReport(startDate, endDate);
                        break;
                    default:
                        break;
                }

                lblStatus.Text = "Hesabat yaradıldı";
                progressBar.Visible = false;
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Hesabatı yaradarkən xəta baş verdi: {ex.Message}", "Xəta",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
                lblStatus.Text = "Xəta baş verdi!";
                progressBar.Visible = false;
            }
        }

        private async Task GenerateFinancialReport(DateTime startDate, DateTime endDate)
        {
            // Show all panels and charts for financial report
            panelRevenueExpenses.Visible = true;
            panelPropertyTypes.Visible = true;
            panelDetails.Visible = true;

            await Task.Run(() =>
            {
            // Get financial data within the date range
            var filteredTransactions = _transactions.Where(t =>
                t.TransactionDate >= startDate &&
                t.TransactionDate <= endDate &&
                t.IsSuccessful).ToList();

            var filteredExpenses = _expenses.Where(e =>
                e.ExpenseDate >= startDate &&
                e.ExpenseDate <= endDate &&
                e.IsPaid).ToList();

            // Calculate summary data
            decimal totalRevenue = filteredTransactions.Sum(t => t.Amount);
            decimal totalExpenses = filteredExpenses.Sum(e => e.Amount);
            decimal totalCommission = filteredTransactions.Sum(t => t.CommissionAmount);
            decimal profit = totalRevenue - totalExpenses;

            // Monthly breakdown (for charts)
            var monthlyData = new Dictionary<string, Tuple<decimal, decimal>>();

            // Group by month
            var revenueByMonth = filteredTransactions
                .GroupBy(t => new { Month = t.TransactionDate.Month, Year = t.TransactionDate.Year })
                .OrderBy(g => g.Key.Year)
                .ThenBy(g => g.Key.Month)
                .Select(g => new
                {
                    MonthName = $"{g.Key.Year}-{g.Key.Month:D2}",
                    Revenue = g.Sum(t => t.Amount)
                });

            var expensesByMonth = filteredExpenses
                .GroupBy(e => new { Month = e.ExpenseDate.Month, Year = e.ExpenseDate.Year })
                .OrderBy(g => g.Key.Year)
                .ThenBy(g => g.Key.Month)
                .Select(g => new
                {
                    MonthName = $"{g.Key.Year}-{g.Key.Month:D2}",
                    Expenses = g.Sum(e => e.Amount)
                });

            // Combine revenue and expenses by month
            foreach (var revenue in revenueByMonth)
            {
                monthlyData[revenue.MonthName] = new Tuple<decimal, decimal>(revenue.Revenue, 0);
            }

            foreach (var expense in expensesByMonth)
            {
                if (monthlyData.ContainsKey(expense.MonthName))
                {
                    var currentData = monthlyData[expense.MonthName];
                    monthlyData[expense.MonthName] = new Tuple<decimal, decimal>(currentData.Item1, expense.Expenses);
                }
                else
                {
                    monthlyData[expense.MonthName] = new Tuple<decimal, decimal>(0, expense.Expenses);
                }
            }

            // Get property sales data by type
            var propertySalesByType = filteredTransactions
                .Where(t => t.PropertyId > 0 && t.TransactionType.Contains("Satış"))
                .GroupBy(t => GetPropertyType(t.PropertyId))
                .Select(g => new
                {
                    PropertyType = g.Key,
                    Count = g.Count(),
                    TotalAmount = g.Sum(t => t.Amount)
                })
                .OrderByDescending(g => g.TotalAmount)
                .ToList();

            // Update UI on the UI thread
            this.Invoke(new Action(() =>
            {
            // Update summary labels
            lblTotalRevenue.Text = totalRevenue.ToString("N2") + " ₼";
            lblTotalExpenses.Text = totalExpenses.ToString("N2") + " ₼";
            lblTotalCommission.Text = totalCommission.ToString("N2") + " ₼";
            lblProfit.Text = profit.ToString("N2") + " ₼";
            lblProfit.ForeColor = profit >= 0 ? Color.Green : Color.Red;

            // Update Revenue vs Expenses Chart
            chartRevenueExpenses.Series[0].Points.Clear();
            chartRevenueExpenses.Series[1].Points.Clear();

            foreach (var monthData in monthlyData.OrderBy(kvp => kvp.Key))
            {
                string monthDisplay = GetMonthDisplayName(monthData.Key);
                chartRevenueExpenses.Series[0].Points.AddXY(monthDisplay, (double)monthData.Value.Item1);
                chartRevenueExpenses.Series[1].Points.AddXY(monthDisplay, (double)monthData.Value.Item2);
            }

            // Update Property Types Chart
            chartPropertyTypes.Series[0].Points.Clear();

            foreach (var propertyType in propertySalesByType)
            {
                int pointIndex = chartPropertyTypes.Series[0].Points.AddXY(propertyType.PropertyType, (double)propertyType.TotalAmount);
                chartPropertyTypes.Series[0].Points[pointIndex].Label = $"{propertyType.PropertyType}: {propertyType.TotalAmount:N0} ₼";
            }

            // Update Report Title
            lblReportTitle.Text = $"Ümumi Maliyyə Hesabatı ({startDate:dd.MM.yyyy} - {endDate:dd.MM.yyyy})";

            // Update details in rich text box
            rtbDetails.Clear();
            Windows.Forms;
            using System.Threading;
            using System.Globalization;

namespace RealEstateManager
    {
        static class Program
        {
            /// <summary>
            /// The main entry point for the application.
            /// </summary>
            [STAThread]
            static void Main()
            {
                // Set Azerbaijani culture for the application
                Thread.CurrentThread.CurrentUICulture = new CultureInfo("az-Latn-AZ");
                Thread.CurrentThread.CurrentCulture = new CultureInfo("az-Latn-AZ");

                Application.EnableVisualStyles();
                Application.SetCompatibleTextRenderingDefault(false);

                // Initialize the database
                DatabaseManager.InitializeDatabase();

                // Start the application with the main form
                Application.Run(new MainForm());
            }
        }
    }
