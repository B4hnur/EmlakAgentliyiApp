using System;
using System.Collections.Generic;
using System.Linq;

namespace DashinmazEmlakApp.Database.Models
{
    /// <summary>
    /// İşçi vəzifələri
    /// </summary>
    public enum EmployeeRole
    {
        /// <summary>
        /// Satış meneceri
        /// </summary>
        SalesAgent = 0,

        /// <summary>
        /// Kirayə meneceri
        /// </summary>
        RentalAgent = 1,

        /// <summary>
        /// Müştəri məsləhətçisi
        /// </summary>
        CustomerAdvisor = 2,

        /// <summary>
        /// Marketinq əməkdaşı
        /// </summary>
        MarketingSpecialist = 3,

        /// <summary>
        /// Ofis meneceri
        /// </summary>
        OfficeManager = 4,

        /// <summary>
        /// Maliyyə meneceri
        /// </summary>
        FinanceManager = 5,

        /// <summary>
        /// Direktor
        /// </summary>
        Director = 6,

        /// <summary>
        /// İnzibatçı
        /// </summary>
        Administrator = 7,

        /// <summary>
        /// Digər
        /// </summary>
        Other = 8
    }

    /// <summary>
    /// İşçi statusları
    /// </summary>
    public enum EmployeeStatus
    {
        /// <summary>
        /// Aktiv
        /// </summary>
        Active = 0,

        /// <summary>
        /// Məzuniyyətdə
        /// </summary>
        OnLeave = 1,

        /// <summary>
        /// İşdən çıxarılıb
        /// </summary>
        Terminated = 2,

        /// <summary>
        /// Sınaq müddətində
        /// </summary>
        Probation = 3,

        /// <summary>
        /// Yarım ştat
        /// </summary>
        PartTime = 4
    }

    /// <summary>
    /// İşçi məlumatları üçün model sinifi
    /// </summary>
    public class Employee
    {
        /// <summary>
        /// İşçi ID-si
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// Ad
        /// </summary>
        public string FirstName { get; set; }

        /// <summary>
        /// Soyad
        /// </summary>
        public string LastName { get; set; }

        /// <summary>
        /// Tam ad
        /// </summary>
        public string FullName => $"{FirstName} {LastName}";

        /// <summary>
        /// Vəzifə
        /// </summary>
        public EmployeeRole Role { get; set; }

        /// <summary>
        /// Status
        /// </summary>
        public EmployeeStatus Status { get; set; }

        /// <summary>
        /// Telefon
        /// </summary>
        public string Phone { get; set; }

        /// <summary>
        /// E-poçt
        /// </summary>
        public string Email { get; set; }

        /// <summary>
        /// Ünvan
        /// </summary>
        public string Address { get; set; }

        /// <summary>
        /// İşə qəbul tarixi
        /// </summary>
        public DateTime HireDate { get; set; }

        /// <summary>
        /// İşdən çıxma tarixi
        /// </summary>
        public DateTime? TerminationDate { get; set; }

        /// <summary>
        /// Əmək haqqı
        /// </summary>
        public decimal Salary { get; set; }

        /// <summary>
        /// Komissiya faizi
        /// </summary>
        public decimal CommissionRate { get; set; }

        /// <summary>
        /// Əlavə qeydlər
        /// </summary>
        public string Notes { get; set; }

        /// <summary>
        /// Şəxsiyyət vəsiqəsi nömrəsi
        /// </summary>
        public string IdNumber { get; set; }

        /// <summary>
        /// İstifadəçi adı
        /// </summary>
        public string Username { get; set; }

        /// <summary>
        /// Şifrənin heş kodu
        /// </summary>
        public string PasswordHash { get; set; }

        /// <summary>
        /// Sistem rolu
        /// </summary>
        public string SystemRole { get; set; }

        /// <summary>
        /// Yaradılma tarixi
        /// </summary>
        public DateTime CreatedDate { get; set; }

        /// <summary>
        /// Son yenilənmə tarixi
        /// </summary>
        public DateTime LastUpdated { get; set; }

        /// <summary>
        /// Konstruktor
        /// </summary>
        public Employee()
        {
            CreatedDate = DateTime.Now;
            LastUpdated = DateTime.Now;
            HireDate = DateTime.Now;
            Status = EmployeeStatus.Active;
            CommissionRate = 0.0m;
        }

        /// <summary>
        /// İşçi məlumatlarının qısa versiyanı qaytarır
        /// </summary>
        public override string ToString()
        {
            return $"{FullName} - {Role}";
        }

        /// <summary>
        /// İşçinin aktivliyini yoxlayır
        /// </summary>
        public bool IsActive()
        {
            return Status == EmployeeStatus.Active || Status == EmployeeStatus.Probation || Status == EmployeeStatus.PartTime;
        }

        /// <summary>
        /// İşçinin işdə olduğu müddəti qaytarır
        /// </summary>
        public TimeSpan GetWorkDuration()
        {
            DateTime endDate = TerminationDate ?? DateTime.Now;
            return endDate - HireDate;
        }

        /// <summary>
        /// İşçinin işdə olduğu müddəti il olaraq qaytarır
        /// </summary>
        public double GetYearsOfService()
        {
            var duration = GetWorkDuration();
            return Math.Round(duration.TotalDays / 365.25, 1);
        }

        /// <summary>
        /// İşçi üçün illik ödəniş hesablayır
        /// </summary>
        public decimal CalculateAnnualSalary()
        {
            return Salary * 12;
        }
    }
}