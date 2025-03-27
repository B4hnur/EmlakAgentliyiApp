using System;
using System.Collections.Generic;
using System.Linq;
using DasinmazEmlakAgentligi.Models;
using DasinmazEmlakAgentligi.Database;
using DashinmazEmlakApp.Database;

namespace DasinmazEmlakAgentligi.Services
{
    public class FinancialService
    {
        private readonly DatabaseManager _databaseManager;

        public FinancialService()
        {
            _databaseManager = new DatabaseManager();
        }

        /// <summary>
        /// Satış əməliyyatı üçün komissiya hesablayır
        /// </summary>
        /// <param name="price">Satış qiyməti</param>
        /// <param name="commissionRate">Komissiya faizi</param>
        /// <returns>Komissiya məbləği</returns>
        public decimal CalculateSaleCommission(decimal price, decimal commissionRate)
        {
            return Math.Round(price * (commissionRate / 100), 2);
        }

        /// <summary>
        /// Kirayə əməliyyatı üçün komissiya hesablayır
        /// </summary>
        /// <param name="monthlyRent">Aylıq kirayə məbləği</param>
        /// <param name="commissionRate">Komissiya faizi</param>
        /// <returns>Komissiya məbləği</returns>
        public decimal CalculateRentalCommission(decimal monthlyRent, decimal commissionRate)
        {
            // Kirayə komissiyası adətən bir aylıq kirayənin müəyyən faizi olur
            return Math.Round(monthlyRent * (commissionRate / 100), 2);
        }

        /// <summary>
        /// İşçinin əmək haqqını hesablayır
        /// </summary>
        /// <param name="employee">İşçi</param>
        /// <param name="month">Ay</param>
        /// <param name="year">İl</param>
        /// <returns>Ümumi əmək haqqı (Əsas + Komissiya)</returns>
        public decimal CalculateEmployeeSalary(Employee employee, int month, int year)
        {
            // Əsas əmək haqqı
            decimal totalSalary = employee.BaseSalary;

            // İşçinin ay üzrə əməliyyatlarını alırıq
            List<Transaction> transactions = GetEmployeeTransactions(employee.Id, month, year);

            // Komissiya məbləğini hesablayırıq
            decimal totalCommission = transactions.Sum(t => t.CommissionAmount * (employee.CommissionRate / 100));

            // Toplam əmək haqqı
            return Math.Round(totalSalary + totalCommission, 2);
        }

        /// <summary>
        /// Aylıq gəlir hesabatı hazırlayır
        /// </summary>
        /// <param name="month">Ay</param>
        /// <param name="year">İl</param>
        /// <returns>Aylıq gəlir, xərc və xalis gəlir</returns>
        public (decimal Income, decimal Expenses, decimal NetIncome) GetMonthlyFinancialReport(int month, int year)
        {
            // Ay üzrə gəlir və xərcləri hesablayırıq
            decimal income = CalculateMonthlyIncome(month, year);
            decimal expenses = CalculateMonthlyExpenses(month, year);
            decimal netIncome = income - expenses;

            return (income, expenses, netIncome);
        }

        /// <summary>
        /// İllik gəlir hesabatı hazırlayır
        /// </summary>
        /// <param name="year">İl</param>
        /// <returns>İllik gəlir, xərc və xalis gəlir</returns>
        public (decimal Income, decimal Expenses, decimal NetIncome) GetYearlyFinancialReport(int year)
        {
            decimal totalIncome = 0;
            decimal totalExpenses = 0;

            // Hər ay üçün hesablama aparırıq
            for (int month = 1; month <= 12; month++)
            {
                var monthlyReport = GetMonthlyFinancialReport(month, year);
                totalIncome += monthlyReport.Income;
                totalExpenses += monthlyReport.Expenses;
            }

            decimal netIncome = totalIncome - totalExpenses;

            return (totalIncome, totalExpenses, netIncome);
        }

        /// <summary>
        /// Xərc kateqoriyaları üzrə hesabat hazırlayır
        /// </summary>
        /// <param name="month">Ay</param>
        /// <param name="year">İl</param>
        /// <returns>Kateqoriya-məbləğ cütləri</returns>
        public Dictionary<ExpenseType, decimal> GetExpensesByCategory(int month, int year)
        {
            Dictionary<ExpenseType, decimal> expensesByCategory = new Dictionary<ExpenseType, decimal>();

            // Bütün mümkün xərc kateqoriyalarını əlavə edirik
            foreach (ExpenseType expenseType in Enum.GetValues(typeof(ExpenseType)))
            {
                expensesByCategory[expenseType] = 0;
            }

            // Verilmiş ay və il üçün bütün xərcləri əldə edirik
            List<Expense> expenses = GetMonthlyExpenses(month, year);

            // Hər xərc üçün müvafiq kateqoriyada məbləği artırırıq
            foreach (Expense expense in expenses)
            {
                expensesByCategory[expense.ExpenseType] += expense.Amount;
            }

            return expensesByCategory;
        }

        /// <summary>
        /// İşçi üzrə gəlir hesabatı hazırlayır
        /// </summary>
        /// <param name="employeeId">İşçi ID-si</param>
        /// <param name="month">Ay</param>
        /// <param name="year">İl</param>
        /// <returns>İşçinin gətirdiyi ümumi gəlir</returns>
        public decimal GetEmployeeIncomeReport(int employeeId, int month, int year)
        {
            List<Transaction> transactions = GetEmployeeTransactions(employeeId, month, year);
            return transactions.Sum(t => t.CommissionAmount);
        }

        /// <summary>
        /// Verilmiş ay və il üçün bütün gəlirləri hesablayır
        /// </summary>
        /// <param name="month">Ay</param>
        /// <param name="year">İl</param>
        /// <returns>Aylıq ümumi gəlir</returns>
        private decimal CalculateMonthlyIncome(int month, int year)
        {
            List<Transaction> transactions = GetMonthlyTransactions(month, year);
            return transactions.Sum(t => t.CommissionAmount);
        }

        /// <summary>
        /// Verilmiş ay və il üçün bütün xərcləri hesablayır
        /// </summary>
        /// <param name="month">Ay</param>
        /// <param name="year">İl</param>
        /// <returns>Aylıq ümumi xərc</returns>
        private decimal CalculateMonthlyExpenses(int month, int year)
        {
            List<Expense> expenses = GetMonthlyExpenses(month, year);
            return expenses.Sum(e => e.Amount);
        }

        /// <summary>
        /// Verilmiş ay və il üçün bütün əməliyyatları əldə edir
        /// </summary>
        /// <param name="month">Ay</param>
        /// <param name="year">İl</param>
        /// <returns>Əməliyyatlar siyahısı</returns>
        private List<Transaction> GetMonthlyTransactions(int month, int year)
        {
            DateTime startDate = new DateTime(year, month, 1);
            DateTime endDate = startDate.AddMonths(1).AddDays(-1);

            List<Transaction> allTransactions = _databaseManager.GetAllTransactions();

            return allTransactions.Where(t =>
                t.TransactionDate >= startDate && t.TransactionDate <= endDate).ToList();
        }

        /// <summary>
        /// Verilmiş ay və il üçün bütün xərcləri əldə edir
        /// </summary>
        /// <param name="month">Ay</param>
        /// <param name="year">İl</param>
        /// <returns>Xərclər siyahısı</returns>
        private List<Expense> GetMonthlyExpenses(int month, int year)
        {
            DateTime startDate = new DateTime(year, month, 1);
            DateTime endDate = startDate.AddMonths(1).AddDays(-1);

            List<Expense> allExpenses = _databaseManager.GetAllExpenses();

            return allExpenses.Where(e =>
                e.ExpenseDate >= startDate && e.ExpenseDate <= endDate).ToList();
        }

        /// <summary>
        /// Verilmiş işçi üçün ay və il üzrə əməliyyatları əldə edir
        /// </summary>
        /// <param name="employeeId">İşçi ID-si</param>
        /// <param name="month">Ay</param>
        /// <param name="year">İl</param>
        /// <returns>İşçinin əməliyyatlar siyahısı</returns>
        private List<Transaction> GetEmployeeTransactions(int employeeId, int month, int year)
        {
            DateTime startDate = new DateTime(year, month, 1);
            DateTime endDate = startDate.AddMonths(1).AddDays(-1);

            List<Transaction> allTransactions = _databaseManager.GetAllTransactions();

            return allTransactions.Where(t =>
                t.EmployeeId == employeeId &&
                t.TransactionDate >= startDate &&
                t.TransactionDate <= endDate).ToList();
        }
    }
}
