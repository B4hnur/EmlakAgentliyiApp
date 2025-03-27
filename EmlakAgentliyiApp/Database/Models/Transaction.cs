using System;
using System.Collections.Generic;

namespace DashinmazEmlakApp.Database.Models
{
    /// <summary>
    /// Əməliyyat tipini təyin edən enum
    /// </summary>
    public enum TransactionTyp
    {
        Sale,              // Satış
        Rental,            // Kirayə
        RentalExtension,   // Kirayə müddətinin uzadılması
        Cancellation,      // Ləğv etmə
        Other              // Digər
    }

    /// <summary>
    /// Əməliyyat statusunu təyin edən enum
    /// </summary>
    public enum TransactionStatus
    {
        Pending,           // Gözləmədə
        InProgress,        // Davam edir
        Completed,         // Tamamlanıb
        Cancelled,         // Ləğv edilib
        Failed             // Uğursuz olub
    }

    /// <summary>
    /// Əməliyyat məlumatlarını təmsil edən sinif
    /// </summary>
    public class Transaction
    {
        /// <summary>
        /// Əməliyyatın unikal identifikatoru
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// Əməliyyat tipi
        /// </summary>
        public TransactionType Type { get; set; }

        /// <summary>
        /// Əməliyyat statusu
        /// </summary>
        public TransactionStatus Status { get; set; }

        /// <summary>
        /// Əmlakın identifikatoru
        /// </summary>
        public int PropertyId { get; set; }

        /// <summary>
        /// Alıcı/icarəçi müştərinin identifikatoru
        /// </summary>
        public int BuyerClientId { get; set; }

        /// <summary>
        /// Satıcı/mülk sahibi müştərinin identifikatoru
        /// </summary>
        public int SellerClientId { get; set; }

        /// <summary>
        /// Əməliyyatı həyata keçirən agentin identifikatoru
        /// </summary>
        public int AgentId { get; set; }

        /// <summary>
        /// Əməliyyatın başlama tarixi
        /// </summary>
        public DateTime StartDate { get; set; }

        /// <summary>
        /// Əməliyyatın tamamlanma tarixi
        /// </summary>
        public DateTime? CompletionDate { get; set; }

        /// <summary>
        /// Ümumi məbləğ
        /// </summary>
        public decimal TotalAmount { get; set; }

        /// <summary>
        /// Agentlik komissiyası məbləği
        /// </summary>
        public decimal CommissionAmount { get; set; }

        /// <summary>
        /// Agentin komissiyası
        /// </summary>
        public decimal AgentCommission { get; set; }

        /// <summary>
        /// Kirayə müddəti (ay ilə, kirayə əməliyyatları üçün)
        /// </summary>
        public int? RentalPeriod { get; set; }

        /// <summary>
        /// Sənədin adı (və ya nömrəsi)
        /// </summary>
        public string DocumentNumber { get; set; }

        /// <summary>
        /// Sənəd şəklinin yolu
        /// </summary>
        public string DocumentImagePath { get; set; }

        /// <summary>
        /// Əməliyyat haqqında qeydlər
        /// </summary>
        public string Notes { get; set; }

        /// <summary>
        /// Əməliyyatla bağlı xərclər
        /// </summary>
        public List<Expense> Expenses { get; set; }

        /// <summary>
        /// Default constructor
        /// </summary>
        public Transaction()
        {
            Expenses = new List<Expense>();
            StartDate = DateTime.Now;
            Status = TransactionStatus.Pending;
        }

        /// <summary>
        /// Əməliyyat tipinin adını qaytarır
        /// </summary>
        /// <returns>Tip adı</returns>
       

        /// <summary>
        /// Əməliyyat statusunun adını qaytarır
        /// </summary>
        /// <returns>Status adı</returns>
        public string GetStatusName()
        {
            switch (Status)
            {
                case TransactionStatus.Pending:
                    return "Gözləmədə";
                case TransactionStatus.InProgress:
                    return "Davam edir";
                case TransactionStatus.Completed:
                    return "Tamamlanıb";
                case TransactionStatus.Cancelled:
                    return "Ləğv edilib";
                case TransactionStatus.Failed:
                    return "Uğursuz olub";
                default:
                    return "Naməlum";
            }
        }

        /// <summary>
        /// Əməliyyatın xalis gəlirini hesablayır
        /// </summary>
        /// <returns>Xalis gəlir</returns>
        public decimal CalculateNetIncome()
        {
            decimal totalExpenses = 0;

            foreach (var expense in Expenses)
            {
                if (expense.Status == ExpenseStatus.Paid)
                {
                    totalExpenses += expense.Amount;
                }
            }

            return CommissionAmount - totalExpenses;
        }
    }
}