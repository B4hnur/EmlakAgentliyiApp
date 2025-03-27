using System;

namespace DasinmazEmlakAgentligi.Models
{
    public enum TransactionType
    {
        Sale = 0,     // Satış
        Rent = 1      // Kirayə
    }

    public class Transaction
    {
        public int Id { get; set; }
        public int PropertyId { get; set; }
        public int ClientId { get; set; }
        public int EmployeeId { get; set; }
        public TransactionType TransactionType { get; set; }
        public decimal Amount { get; set; }
        public decimal CommissionAmount { get; set; }
        public string Currency { get; set; }
        public string Notes { get; set; }
        public DateTime TransactionDate { get; set; }

        // DB-də saxlanmayan, lakin bəzi əməliyyatlar zamanı istifadə olunan xüsusiyyətlər
        public string PropertyTitle { get; set; }
        public string ClientName { get; set; }
        public string EmployeeName { get; set; }

        public Transaction()
        {
            TransactionDate = DateTime.Now;
            Currency = "AZN";
        }

        public override string ToString()
        {
            return $"{TransactionDate.ToString("dd.MM.yyyy")} - {GetTransactionTypeString()} - {Amount} {Currency}";
        }

        public string GetTransactionTypeString()
        {
            switch (TransactionType)
            {
                case TransactionType.Sale:
                    return "Satış";
                case TransactionType.Rent:
                    return "Kirayə";
                default:
                    return "Bilinmir";
            }
        }
    }
}
