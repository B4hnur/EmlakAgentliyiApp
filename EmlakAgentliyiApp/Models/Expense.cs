using System;

namespace DasinmazEmlakAgentligi.Models
{
    public enum ExpenseType
    {
        Rent = 0,            // Ofis icarəsi
        Utilities = 1,       // Kommunal xərclər
        Salary = 2,          // Əmək haqqı
        Advertising = 3,     // Reklam xərcləri
        Transportation = 4,  // Nəqliyyat xərcləri
        Office = 5,          // Ofis ləvazimatları
        Maintenance = 6,     // Texniki xidmət
        Legal = 7,           // Hüquqi xərclər
        Tax = 8,             // Vergilər
        Other = 9            // Digər xərclər
    }

    public class Expense
    {
        public int Id { get; set; }
        public ExpenseType ExpenseType { get; set; }
        public decimal Amount { get; set; }
        public string Currency { get; set; }
        public string Description { get; set; }
        public DateTime ExpenseDate { get; set; }

        public Expense()
        {
            ExpenseDate = DateTime.Now;
            Currency = "AZN";
        }

        public override string ToString()
        {
            return $"{ExpenseDate.ToString("dd.MM.yyyy")} - {GetExpenseTypeString()} - {Amount} {Currency}";
        }

        public string GetExpenseTypeString()
        {
            switch (ExpenseType)
            {
                case ExpenseType.Rent:
                    return "Ofis icarəsi";
                case ExpenseType.Utilities:
                    return "Kommunal xərclər";
                case ExpenseType.Salary:
                    return "Əmək haqqı";
                case ExpenseType.Advertising:
                    return "Reklam xərcləri";
                case ExpenseType.Transportation:
                    return "Nəqliyyat xərcləri";
                case ExpenseType.Office:
                    return "Ofis ləvazimatları";
                case ExpenseType.Maintenance:
                    return "Texniki xidmət";
                case ExpenseType.Legal:
                    return "Hüquqi xərclər";
                case ExpenseType.Tax:
                    return "Vergilər";
                case ExpenseType.Other:
                    return "Digər xərclər";
                default:
                    return "Bilinmir";
            }
        }
    }
}
