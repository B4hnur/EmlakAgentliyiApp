using System;
using System.Collections.Generic;
using System.Linq;
using DashinmazEmlakApp.Database.Models;
using DashinmazEmlakApp.Utilities;

namespace DashinmazEmlakApp.Services
{
    /// <summary>
    /// Maliyyə hesablamaları üçün xidmət sinifi
    /// </summary>
    public class FinancialCalculationService
    {
        /// <summary>
        /// Satış üçün komissiya hesablayır
        /// </summary>
        /// <param name="propertyPrice">Əmlakın qiyməti</param>
        /// <param name="commissionRate">Komissiya faizi</param>
        /// <returns>Hesablanmış komissiya məbləği</returns>
        public decimal CalculateSaleCommission(decimal propertyPrice, decimal commissionRate)
        {
            if (propertyPrice <= 0 || commissionRate < 0)
            {
                LoggingHelper.LogWarning($"Komissiya hesablama üçün yanlış parametrlər: {propertyPrice}, {commissionRate}");
                return 0;
            }

            return Math.Round(propertyPrice * (commissionRate / 100), 2);
        }

        /// <summary>
        /// Kirayə üçün komissiya hesablayır (aylıq kirayə məbləğinə əsasən)
        /// </summary>
        /// <param name="monthlyRent">Aylıq kirayə məbləği</param>
        /// <param name="commissionRate">Komissiya faizi</param>
        /// <param name="months">Ay sayı (adətən 1)</param>
        /// <returns>Hesablanmış komissiya məbləği</returns>
        public decimal CalculateRentalCommission(decimal monthlyRent, decimal commissionRate, int months = 1)
        {
            if (monthlyRent <= 0 || commissionRate < 0 || months <= 0)
            {
                LoggingHelper.LogWarning($"Kirayə komissiyası hesablama üçün yanlış parametrlər: {monthlyRent}, {commissionRate}, {months}");
                return 0;
            }

            return Math.Round(monthlyRent * months * (commissionRate / 100), 2);
        }

        /// <summary>
        /// Müəyyən dövr üçün ümumi gəlirləri hesablayır
        /// </summary>
        /// <param name="transactions">Əməliyyatlar siyahısı</param>
        /// <param name="startDate">Başlanğıc tarix</param>
        /// <param name="endDate">Son tarix</param>
        /// <returns>Ümumi gəlir məbləği</returns>
        public decimal CalculateTotalIncome(List<Transaction> transactions, DateTime startDate, DateTime endDate)
        {
            if (transactions == null || !transactions.Any())
            {
                return 0;
            }

            var filteredTransactions = transactions
                .Where(t => t.Date >= startDate && t.Date <= endDate && t.IsIncome() && t.Status == TransactionStatus.Completed)
                .ToList();

            return filteredTransactions.Sum(t => t.Amount);
        }

        /// <summary>
        /// Müəyyən dövr üçün ümumi xərcləri hesablayır
        /// </summary>
        /// <param name="transactions">Əməliyyatlar siyahısı</param>
        /// <param name="expenses">Xərclər siyahısı</param>
        /// <param name="startDate">Başlanğıc tarix</param>
        /// <param name="endDate">Son tarix</param>
        /// <returns>Ümumi xərc məbləği</returns>
        public decimal CalculateTotalExpenses(List<Transaction> transactions, List<Expense> expenses, DateTime startDate, DateTime endDate)
        {
            decimal total = 0;

            // Xərc əməliyyatlarını hesabla
            if (transactions != null && transactions.Any())
            {
                var expenseTransactions = transactions
                    .Where(t => t.Date >= startDate && t.Date <= endDate && t.IsExpense() && t.Status == TransactionStatus.Completed)
                    .ToList();

                total += expenseTransactions.Sum(t => t.Amount);
            }

            // Xərc cədvəlindən xərcləri hesabla
            if (expenses != null && expenses.Any())
            {
                var filteredExpenses = expenses
                    .Where(e => e.Date >= startDate && e.Date <= endDate && e.IsPaid)
                    .ToList();

                total += filteredExpenses.Sum(e => e.Amount);
            }

            return total;
        }

        /// <summary>
        /// Müəyyən dövr üçün mənfəəti hesablayır (gəlir - xərc)
        /// </summary>
        /// <param name="transactions">Əməliyyatlar siyahısı</param>
        /// <param name="expenses">Xərclər siyahısı</param>
        /// <param name="startDate">Başlanğıc tarix</param>
        /// <param name="endDate">Son tarix</param>
        /// <returns>Hesablanmış mənfəət</returns>
        public decimal CalculateProfit(List<Transaction> transactions, List<Expense> expenses, DateTime startDate, DateTime endDate)
        {
            decimal income = CalculateTotalIncome(transactions, startDate, endDate);
            decimal expense = CalculateTotalExpenses(transactions, expenses, startDate, endDate);

            return income - expense;
        }

        /// <summary>
        /// İşçinin dövr üzrə qazancını hesablayır (maaş + komissiyalar)
        /// </summary>
        /// <param name="employee">İşçi</param>
        /// <param name="transactions">Əməliyyatlar</param>
        /// <param name="startDate">Başlanğıc tarix</param>
        /// <param name="endDate">Son tarix</param>
        /// <returns>İşçinin ümumi qazancı</returns>
        public decimal CalculateEmployeeEarnings(Employee employee, List<Transaction> transactions, DateTime startDate, DateTime endDate)
        {
            if (employee == null)
            {
                return 0;
            }

            decimal totalEarnings = 0;

            // Maaş və bonusları hesabla
            if (transactions != null && transactions.Any())
            {
                // İşçiyə ödənilmiş maaş və bonusları tap
                var salaryTransactions = transactions
                    .Where(t => (t.Type == TransactionType.Salary || t.Type == TransactionType.Bonus) &&
                               t.EmployeeId == employee.Id &&
                               t.Date >= startDate && t.Date <= endDate &&
                               t.Status == TransactionStatus.Completed)
                    .ToList();

                totalEarnings += salaryTransactions.Sum(t => t.Amount);

                // Komissiyaları hesabla
                var commissionTransactions = transactions
                    .Where(t => t.Type == TransactionType.Commission &&
                              t.EmployeeId == employee.Id &&
                              t.Date >= startDate && t.Date <= endDate &&
                              t.Status == TransactionStatus.Completed)
                    .ToList();

                totalEarnings += commissionTransactions.Sum(t => t.Amount);
            }
            else
            {
                // Əgər əməliyyat siyahısı yoxdursa, yalnız gündəlik maaşı hesabla
                // Ay ərzindəki iş günlərini təqribi 22 gün götürək
                int workingDays = 22;
                var monthsInPeriod = (endDate.Year - startDate.Year) * 12 + endDate.Month - startDate.Month + 1;

                // Dövrün əhatə etdiyi günləri nəzərə alırıq
                var daysInPeriod = (endDate - startDate).TotalDays + 1;
                var dailySalary = employee.Salary / workingDays;

                // Dövrün gün sayı bir aydan azdırsa
                if (daysInPeriod <= 31)
                {
                    // Sadəcə bu dövrdəki iş günlərini hesablayırıq (həftə sonları çıxarılmadan)
                    totalEarnings = (decimal)daysInPeriod * dailySalary;
                }
                else
                {
                    // Dövrün əhatə etdiyi aylarda tam maaş
                    totalEarnings = monthsInPeriod * employee.Salary;
                }
            }

            return totalEarnings;
        }

        /// <summary>
        /// İpoteka ödənişini hesablayır
        /// </summary>
        /// <param name="loanAmount">Kredit məbləği</param>
        /// <param name="annualInterestRate">İllik faiz dərəcəsi (% ilə, məsələn 7.5)</param>
        /// <param name="loanTermYears">Kreditin müddəti (illər)</param>
        /// <returns>Aylıq ödəniş</returns>
        public decimal CalculateMortgagePayment(decimal loanAmount, decimal annualInterestRate, int loanTermYears)
        {
            if (loanAmount <= 0 || annualInterestRate <= 0 || loanTermYears <= 0)
            {
                LoggingHelper.LogWarning($"İpoteka ödənişi hesablama üçün yanlış parametrlər: {loanAmount}, {annualInterestRate}, {loanTermYears}");
                return 0;
            }

            // Aylıq faiz dərəcəsi
            decimal monthlyInterestRate = annualInterestRate / 100 / 12;

            // Ödəniş sayı
            int numberOfPayments = loanTermYears * 12;

            // İpoteka ödənişi düsturu: P = L[c(1 + c)^n]/[(1 + c)^n - 1]
            // Burada L kredit məbləği, c aylıq faiz dərəcəsi, n ödəniş sayıdır

            // (1 + c)^n
            decimal temp = (decimal)Math.Pow((double)(1 + monthlyInterestRate), numberOfPayments);

            // Aylıq ödəniş
            decimal monthlyPayment = loanAmount * (monthlyInterestRate * temp) / (temp - 1);

            return Math.Round(monthlyPayment, 2);
        }

        /// <summary>
        /// Kirayə əmlakın rentabillik dərəcəsini hesablayır
        /// </summary>
        /// <param name="propertyPrice">Əmlakın dəyəri</param>
        /// <param name="monthlyRentalIncome">Aylıq kirayə gəliri</param>
        /// <param name="annualExpenses">İllik xərclər (vergilər, sığorta, təmir və s.)</param>
        /// <returns>İllik rentabillik faizi</returns>
        public decimal CalculateRentalYield(decimal propertyPrice, decimal monthlyRentalIncome, decimal annualExpenses)
        {
            if (propertyPrice <= 0 || monthlyRentalIncome < 0)
            {
                LoggingHelper.LogWarning($"Rentabillik hesablama üçün yanlış parametrlər: {propertyPrice}, {monthlyRentalIncome}, {annualExpenses}");
                return 0;
            }

            // İllik gəlir
            decimal annualIncome = monthlyRentalIncome * 12;

            // Xalis illik gəlir
            decimal netAnnualIncome = annualIncome - annualExpenses;

            // İllik rentabillik
            decimal rentalYield = (netAnnualIncome / propertyPrice) * 100;

            return Math.Round(rentalYield, 2);
        }
    }
}