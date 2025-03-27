using System;
using System.Collections.Generic;
using System.Linq;

namespace DashinmazEmlakApp.Database.Models
{
    /// <summary>
    /// Müştəri tipini təyin edən enum
    /// </summary>
    public enum ClientType
    {
        Buyer,         // Alıcı
        Seller,        // Satıcı
        Tenant,        // Kirayəçi
        Landlord,      // Mülk sahibi
        Both           // Həm alıcı/kirayəçi, həm də satıcı/mülk sahibi
    }

    /// <summary>
    /// Müştəri statusunu təyin edən enum
    /// </summary>
    public enum ClientStatus
    {
        Active,        // Aktiv
        Inactive,      // Qeyri-aktiv
        Potential,     // Potensial
        Former         // Keçmiş
    }

    /// <summary>
    /// Müştəri məlumatlarını təmsil edən sinif
    /// </summary>
    public class Client
    {
        /// <summary>
        /// Müştərinin unikal identifikatoru
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// Müştərinin adı
        /// </summary>
        public string FirstName { get; set; }

        /// <summary>
        /// Müştərinin soyadı
        /// </summary>
        public string LastName { get; set; }

        /// <summary>
        /// Müştərinin telefon nömrəsi
        /// </summary>
        public string Phone { get; set; }

        /// <summary>
        /// Müştərinin alternativ telefon nömrəsi
        /// </summary>
        public string AlternativePhone { get; set; }

        /// <summary>
        /// Müştərinin e-poçt ünvanı
        /// </summary>
        public string Email { get; set; }

        /// <summary>
        /// Müştərinin ünvanı
        /// </summary>
        public string Address { get; set; }

        /// <summary>
        /// Müştərinin tipi
        /// </summary>
        public ClientType Type { get; set; }

        /// <summary>
        /// Müştərinin statusu
        /// </summary>
        public ClientStatus Status { get; set; }

        /// <summary>
        /// Müştərinin qeydiyyat tarixi
        /// </summary>
        public DateTime RegistrationDate { get; set; }

        /// <summary>
        /// Müştərinin son əlaqə tarixi
        /// </summary>
        public DateTime? LastContactDate { get; set; }

        /// <summary>
        /// Müştəri haqqında əlavə qeydlər
        /// </summary>
        public string Notes { get; set; }

        /// <summary>
        /// Müştəriylə işləyən işçinin identifikatoru
        /// </summary>
        public int? AssignedEmployeeId { get; set; }

        /// <summary>
        /// Büdcə limiti (alıcı/kirayəçi üçün)
        /// </summary>
        public decimal? BudgetLimit { get; set; }

        /// <summary>
        /// İstədiyi əmlak növləri (vergüllə ayrılmış)
        /// </summary>
        public string DesiredPropertyTypes { get; set; }

        /// <summary>
        /// İstədiyi rayonlar (vergüllə ayrılmış)
        /// </summary>
        public string DesiredDistricts { get; set; }

        /// <summary>
        /// İstədiyi minimum otaq sayı
        /// </summary>
        public int? MinRooms { get; set; }

        /// <summary>
        /// İstədiyi minimum sahə (kvadrat metr)
        /// </summary>
        public double? MinArea { get; set; }

        /// <summary>
        /// Müştərinin malik olduğu və ya maraqlandığı əmlaklar
        /// </summary>
        public List<Property> Properties { get; set; }

        /// <summary>
        /// Müştərinin tamamlanmış əməliyyatları
        /// </summary>
        public List<Transaction> Transactions { get; set; }

        /// <summary>
        /// Default constructor
        /// </summary>
        public Client()
        {
            Properties = new List<Property>();
            Transactions = new List<Transaction>();
            Status = ClientStatus.Active;
            RegistrationDate = DateTime.Now;
        }

        /// <summary>
        /// Müştərinin tam adını qaytarır
        /// </summary>
        /// <returns>Müştərinin tam adı</returns>
        public string GetFullName()
        {
            return $"{FirstName} {LastName}";
        }

        /// <summary>
        /// İstəklər siyahısını massiv şəklində qaytaran metod
        /// </summary>
        /// <returns>İstəklər massivi</returns>
        public string[] GetDesiredPropertyTypes()
        {
            if (string.IsNullOrEmpty(DesiredPropertyTypes))
            {
                return new string[0];
            }

            return DesiredPropertyTypes.Split(new char[] { ',' }).Where(s => !string.IsNullOrEmpty(s)).ToArray();
        }

        /// <summary>
        /// İstədiyi rayonları massiv şəklində qaytaran metod
        /// </summary>
        /// <returns>Rayonlar massivi</returns>
        public string[] GetDesiredDistricts()
        {
            if (string.IsNullOrEmpty(DesiredDistricts))
            {
                return new string[0];
            }

            return DesiredDistricts.Split(new char[] { ',' }).Where(s => !string.IsNullOrEmpty(s)).ToArray();
        }
    }
}