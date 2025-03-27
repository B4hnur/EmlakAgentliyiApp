using System;
using System.Collections.Generic;
using System.Data.SQLite;
using System.Linq;
using DashinmazEmlakApp.Database;
using DashinmazEmlakApp.Database.Models;

namespace DashinmazEmlakApp.Services
{
    public class ReportService
    {
        private readonly DatabaseManager _dbManager;

        public ReportService(DatabaseManager dbManager)
        {
            _dbManager = dbManager;
        }

        /// <summary>
        /// Gets financial summary for a specific date range
        /// </summary>
        public FinancialSummary GetFinancialSummary(DateTime startDate, DateTime endDate)
        {
            var summary = new FinancialSummary
            {
                StartDate = startDate,
                EndDate = endDate
            };

            using (var connection = _dbManager.GetConnection())
            {
                // Get total sales revenue
                using (var command = new SQLiteCommand(
                    @"SELECT SUM(Amount) FROM Transactions 
                    WHERE TransactionType = 0 
                    AND TransactionDate BETWEEN @StartDate AND @EndDate", connection))
                {
                    command.Parameters.AddWithValue("@StartDate", startDate.ToString("yyyy-MM-dd"));
                    command.Parameters.AddWithValue("@EndDate", endDate.ToString("yyyy-MM-dd"));
                    var result = command.ExecuteScalar();
                    summary.TotalSalesRevenue = (result != DBNull.Value) ? Convert.ToDouble(result) : 0;
                }

                // Get total rental revenue
                using (var command = new SQLiteCommand(
                    @"SELECT SUM(Amount) FROM Transactions 
                    WHERE TransactionType = 1 
                    AND TransactionDate BETWEEN @StartDate AND @EndDate", connection))
                {
                    command.Parameters.AddWithValue("@StartDate", startDate.ToString("yyyy-MM-dd"));
                    command.Parameters.AddWithValue("@EndDate", endDate.ToString("yyyy-MM-dd"));
                    var result = command.ExecuteScalar();
                    summary.TotalRentalRevenue = (result != DBNull.Value) ? Convert.ToDouble(result) : 0;
                }

                // Get total commission
                using (var command = new SQLiteCommand(
                    @"SELECT SUM(Commission) FROM Transactions 
                    WHERE TransactionDate BETWEEN @StartDate AND @EndDate", connection))
                {
                    command.Parameters.AddWithValue("@StartDate", startDate.ToString("yyyy-MM-dd"));
                    command.Parameters.AddWithValue("@EndDate", endDate.ToString("yyyy-MM-dd"));
                    var result = command.ExecuteScalar();
                    summary.TotalCommission = (result != DBNull.Value) ? Convert.ToDouble(result) : 0;
                }

                // Get total expenses
                using (var command = new SQLiteCommand(
                    @"SELECT SUM(Amount) FROM Expenses 
                    WHERE ExpenseDate BETWEEN @StartDate AND @EndDate", connection))
                {
                    command.Parameters.AddWithValue("@StartDate", startDate.ToString("yyyy-MM-dd"));
                    command.Parameters.AddWithValue("@EndDate", endDate.ToString("yyyy-MM-dd"));
                    var result = command.ExecuteScalar();
                    summary.TotalExpenses = (result != DBNull.Value) ? Convert.ToDouble(result) : 0;
                }

                // Get employee salaries total
                using (var command = new SQLiteCommand(
                    @"SELECT SUM(Salary) FROM Employees 
                    WHERE IsActive = 1", connection))
                {
                    var result = command.ExecuteScalar();
                    summary.TotalSalaries = (result != DBNull.Value) ? Convert.ToDouble(result) : 0;
                }

                // Calculate profit
                summary.TotalProfit = summary.TotalCommission - (summary.TotalExpenses + summary.TotalSalaries);

                // Get transaction count by type
                using (var command = new SQLiteCommand(
                    @"SELECT TransactionType, COUNT(*) as Count 
                    FROM Transactions 
                    WHERE TransactionDate BETWEEN @StartDate AND @EndDate 
                    GROUP BY TransactionType", connection))
                {
                    command.Parameters.AddWithValue("@StartDate", startDate.ToString("yyyy-MM-dd"));
                    command.Parameters.AddWithValue("@EndDate", endDate.ToString("yyyy-MM-dd"));

                    using (var reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            int type = reader.GetInt32(0);
                            int count = reader.GetInt32(1);

                            if (type == 0) // Sale
                            {
                                summary.SalesCount = count;
                            }
                            else if (type == 1) // Rent
                            {
                                summary.RentalsCount = count;
                            }
                        }
                    }
                }

                // Get top performing employees
                using (var command = new SQLiteCommand(
                    @"SELECT e.Id, e.FullName, COUNT(t.Id) as TransactionCount, SUM(t.Commission) as TotalCommission 
                    FROM Employees e 
                    JOIN Transactions t ON e.Id = t.EmployeeId 
                    WHERE t.TransactionDate BETWEEN @StartDate AND @EndDate 
                    GROUP BY e.Id, e.FullName 
                    ORDER BY TotalCommission DESC 
                    LIMIT 5", connection))
                {
                    command.Parameters.AddWithValue("@StartDate", startDate.ToString("yyyy-MM-dd"));
                    command.Parameters.AddWithValue("@EndDate", endDate.ToString("yyyy-MM-dd"));

                    using (var reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            summary.TopEmployees.Add(new EmployeePerformance
                            {
                                EmployeeId = reader.GetInt32(0),
                                EmployeeName = reader.GetString(1),
                                TransactionCount = reader.GetInt32(2),
                                TotalCommission = reader.GetDouble(3)
                            });
                        }
                    }
                }
            }

            return summary;
        }

        /// <summary>
        /// Gets transactions for a specific date range
        /// </summary>
        public List<TransactionReport> GetTransactions(DateTime startDate, DateTime endDate)
        {
            var transactions = new List<TransactionReport>();

            using (var connection = _dbManager.GetConnection())
            {
                using (var command = new SQLiteCommand(
                    @"SELECT t.Id, t.TransactionDate, t.Amount, t.Commission, t.TransactionType,
                            p.Title as PropertyTitle, p.Address as PropertyAddress,
                            c.FullName as ClientName, e.FullName as EmployeeName
                      FROM Transactions t
                      JOIN Properties p ON t.PropertyId = p.Id
                      JOIN Clients c ON t.ClientId = c.Id
                      JOIN Employees e ON t.EmployeeId = e.Id
                      WHERE t.TransactionDate BETWEEN @StartDate AND @EndDate
                      ORDER BY t.TransactionDate DESC", connection))
                {
                    command.Parameters.AddWithValue("@StartDate", startDate.ToString("yyyy-MM-dd"));
                    command.Parameters.AddWithValue("@EndDate", endDate.ToString("yyyy-MM-dd"));

                    using (var reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            transactions.Add(new TransactionReport
                            {
                                Id = reader.GetInt32(0),
                                TransactionDate = DateTime.Parse(reader.GetString(1)),
                                Amount = reader.GetDouble(2),
                                Commission = reader.GetDouble(3),
                                Type = (TransactionType)reader.GetInt32(4),
                                PropertyTitle = reader.GetString(5),
                                PropertyAddress = reader.GetString(6),
                                ClientName = reader.GetString(7),
                                EmployeeName = reader.GetString(8)
                            });
                        }
                    }
                }
            }

            return transactions;
        }

        /// <summary>
        /// Gets expenses for a specific date range
        /// </summary>
        public List<ExpenseReport> GetExpenses(DateTime startDate, DateTime endDate)
        {
            var expenses = new List<ExpenseReport>();

            using (var connection = _dbManager.GetConnection())
            {
                using (var command = new SQLiteCommand(
                    @"SELECT e.Id, e.Category, e.Description, e.Amount, e.ExpenseDate,
                            emp.FullName as EmployeeName
                      FROM Expenses e
                      LEFT JOIN Employees emp ON e.EmployeeId = emp.Id
                      WHERE e.ExpenseDate BETWEEN @StartDate AND @EndDate
                      ORDER BY e.ExpenseDate DESC", connection))
                {
                    command.Parameters.AddWithValue("@StartDate", startDate.ToString("yyyy-MM-dd"));
                    command.Parameters.AddWithValue("@EndDate", endDate.ToString("yyyy-MM-dd"));

                    using (var reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            expenses.Add(new ExpenseReport
                            {
                                Id = reader.GetInt32(0),
                                Category = reader.GetString(1),
                                Description = reader.GetString(2),
                                Amount = reader.GetDouble(3),
                                ExpenseDate = DateTime.Parse(reader.GetString(4)),
                                EmployeeName = reader.IsDBNull(5) ? null : reader.GetString(5)
                            });
                        }
                    }
                }
            }

            return expenses;
        }
    }

    public class FinancialSummary
    {
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public double TotalSalesRevenue { get; set; }
        public double TotalRentalRevenue { get; set; }
        public double TotalCommission { get; set; }
        public double TotalExpenses { get; set; }
        public double TotalSalaries { get; set; }
        public double TotalProfit { get; set; }
        public int SalesCount { get; set; }
        public int RentalsCount { get; set; }
        public List<EmployeePerformance> TopEmployees { get; set; } = new List<EmployeePerformance>();
    }

    public class EmployeePerformance
    {
        public int EmployeeId { get; set; }
        public string EmployeeName { get; set; }
        public int TransactionCount { get; set; }
        public double TotalCommission { get; set; }
    }

    public class TransactionReport
    {
        public int Id { get; set; }
        public DateTime TransactionDate { get; set; }
        public double Amount { get; set; }
        public double Commission { get; set; }
        public TransactionType Type { get; set; }
        public string PropertyTitle { get; set; }
        public string PropertyAddress { get; set; }
        public string ClientName { get; set; }
        public string EmployeeName { get; set; }
    }

    public class ExpenseReport
    {
        public int Id { get; set; }
        public string Category { get; set; }
        public string Description { get; set; }
        public double Amount { get; set; }
        public DateTime ExpenseDate { get; set; }
        public string EmployeeName { get; set; }
    }
}
