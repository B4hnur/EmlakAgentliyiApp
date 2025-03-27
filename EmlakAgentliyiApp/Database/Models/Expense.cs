using System;
using System.Collections.Generic;
using System.Linq;

namespace DashinmazEmlakApp.Database.Models
{
    /// <summary>
    /// Xərc növləri
    /// </summary>
    public enum ExpenseType
    {
        /// <summary>
        /// Ofis xərcləri
        /// </summary>
        Office = 0,

        /// <summary>
        /// Marketinq xərcləri
        /// </summary>
        Marketing = 1,

        /// <summary>
        /// Əmək haqqı
        /// </summary>
        Salary = 2,

        /// <summary>
        /// Kommunal xərclər
        /// </summary>
        Utilities = 3,

        /// <summary>
        /// İcarə
        /// </summary>
        Rent = 4,

        /// <summary>
        /// Nəqliyyat
        /// </summary>
        Transportation = 5,

        /// <summary>
        /// Təmir
        /// </summary>
        Maintenance = 6,

        /// <summary>
        /// Texniki xidmət
        /// </summary>
        IT = 7,

        /// <summary>
        /// Əmlak xərcləri
        /// </summary>
        Property = 8,

        /// <summary>
        /// Digər
        /// </summary>
        Other = 9
    }

    /// <summary>
    /// Xərc statusları
    /// </summary>
    public enum ExpenseStatus
    {
        /// <summary>
        /// Gözləyir
        /// </summary>
        Pending = 0,

        /// <summary>
        /// Təsdiqlənib
        /// </summary>
        Approved = 1,

        /// <summary>
        /// Rədd edilib
        /// </summary>
        Rejected = 2,

        /// <summary>
        /// Ödənilib
        /// </summary>
        Paid = 3,

        /// <summary>
        /// Qismən ödənilib
        /// </summary>
        PartiallyPaid = 4
    }

    /// <summary>
    /// Xərc məlumatları üçün model sinifi
    /// </summary>
    public class Expense
    {
        /// <summary>
        /// Xərc ID-si
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// Xərcin adı
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Xərc növü
        /// </summary>
        public ExpenseType Type { get; set; }

        /// <summary>
        /// Məbləğ
        /// </summary>
        public decimal Amount { get; set; }

        /// <summary>
        /// Xərc tarixi
        /// </summary>
        public DateTime Date { get; set; }

        /// <summary>
        /// Əlaqəli işçi ID-si
        /// </summary>
        public int? EmployeeId { get; set; }

        /// <summary>
        /// Əlaqəli əmlak ID-si
        /// </summary>
        public int? PropertyId { get; set; }

        /// <summary>
        /// Xərcin təsviri
        /// </summary>
        public string Description { get; set; }

        /// <summary>
        /// Qəbz faylının yolu
        /// </summary>
        public string ReceiptPath { get; set; }

        /// <summary>
        /// Xərcin statusu
        /// </summary>
        public ExpenseStatus Status { get; set; }

        /// <summary>
        /// Ödənilibmi
        /// </summary>
        public bool IsPaid { get; set; }

        /// <summary>
        /// Ödəniş tarixi
        /// </summary>
        public DateTime? PaidDate { get; set; }

        /// <summary>
        /// Yaradılma tarixi
        /// </summary>
        public DateTime CreatedDate { get; set; }

        /// <summary>
        /// Son yenilənmə tarixi
        /// </summary>
        public DateTime LastUpdated { get; set; }

        /// <summary>
        /// Əlavə qeydlər
        /// </summary>
        public string Notes { get; set; }

        /// <summary>
        /// Konstruktor
        /// </summary>
        public Expense()
        {
            CreatedDate = DateTime.Now;
            LastUpdated = DateTime.Now;
            Date = DateTime.Now;
            Status = ExpenseStatus.Pending;
            IsPaid = false;
        }

        /// <summary>
        /// Xərcin qısa məlumatını qaytarır
        /// </summary>
        public override string ToString()
        {
            return $"{Type} - {Name} - {Amount:C2} - {Date:dd.MM.yyyy}";
        }

        /// <summary>
        /// Xərc ödənilmiş kimi qeyd edir
        /// </summary>
        public void MarkAsPaid()
        {
            IsPaid = true;
            PaidDate = DateTime.Now;
            Status = ExpenseStatus.Paid;
            LastUpdated = DateTime.Now;
        }

        /// <summary>
        /// Ödənilmiş statusunun aradan qaldırır
        /// </summary>
        public void MarkAsUnpaid()
        {
            IsPaid = false;
            PaidDate = null;
            Status = ExpenseStatus.Pending;
            LastUpdated = DateTime.Now;
        }
    }
}