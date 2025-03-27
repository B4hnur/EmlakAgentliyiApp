using System;
using System.Collections.Generic;

namespace DashinmazEmlakApp.Models
{
    /// <summary>
    /// Əmlakın ətraflı məlumatlarını təmsil edən sinif
    /// </summary>
    public class PropertyDetail
    {
        /// <summary>
        /// Əmlak ID-si
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// Əmlak başlığı
        /// </summary>
        public string Title { get; set; }

        /// <summary>
        /// Ətraflı təsvir
        /// </summary>
        public string Description { get; set; }

        /// <summary>
        /// Qiymət
        /// </summary>
        public decimal Price { get; set; }

        /// <summary>
        /// Əmlak tipi (mənzil, ev, ofis, və s.)
        /// </summary>
        public int PropertyType { get; set; }

        /// <summary>
        /// Bina tipi (köhnə tikili, yeni tikili, və s.)
        /// </summary>
        public int BuildingType { get; set; }

        /// <summary>
        /// Elan tipi (satış, kirayə, və s.)
        /// </summary>
        public int ListingType { get; set; }

        /// <summary>
        /// Sahə (kv.m)
        /// </summary>
        public double Area { get; set; }

        /// <summary>
        /// Otaq sayı
        /// </summary>
        public int Rooms { get; set; }

        /// <summary>
        /// Mərtəbə
        /// </summary>
        public int Floor { get; set; }

        /// <summary>
        /// Bina mərtəbə sayı
        /// </summary>
        public int TotalFloors { get; set; }

        /// <summary>
        /// Şəhər
        /// </summary>
        public string City { get; set; }

        /// <summary>
        /// Rayon
        /// </summary>
        public string District { get; set; }

        /// <summary>
        /// Ünvan
        /// </summary>
        public string Address { get; set; }

        /// <summary>
        /// Əlaqə nömrəsi
        /// </summary>
        public string ContactPhone { get; set; }

        /// <summary>
        /// Əlaqə e-poçtu
        /// </summary>
        public string ContactEmail { get; set; }

        /// <summary>
        /// Əmlak sahibinin ID-si
        /// </summary>
        public int? OwnerId { get; set; }

        /// <summary>
        /// Məsul əməkdaşın ID-si
        /// </summary>
        public int? EmployeeId { get; set; }

        /// <summary>
        /// Əmlak statusu
        /// </summary>
        public int Status { get; set; }

        /// <summary>
        /// Elanın tarixi
        /// </summary>
        public DateTime ListingDate { get; set; }

        /// <summary>
        /// Son yeniləmə tarixi
        /// </summary>
        public DateTime LastUpdated { get; set; }

        /// <summary>
        /// Şəkil yolları
        /// </summary>
        public List<string> ImagePaths { get; set; }

        /// <summary>
        /// Elanın mənbə URL-i (kub.az, bina.az və s.)
        /// </summary>
        public string SourceUrl { get; set; }

        /// <summary>
        /// Əmlak xüsusiyyətləri (lift, kondisioner, mebel və s.)
        /// </summary>
        public Dictionary<string, bool> Features { get; set; }

        /// <summary>
        /// Əlavə qeydlər
        /// </summary>
        public string Notes { get; set; }

        /// <summary>
        /// Əlaqəli əməliyyatların ID-ləri
        /// </summary>
        public List<int> TransactionIds { get; set; }

        /// <summary>
        /// Əlaqəli xərclərin ID-ləri
        /// </summary>
        public List<int> ExpenseIds { get; set; }

        /// <summary>
        /// Əmlakla maraqlanan müştəri ID-ləri
        /// </summary>
        public List<int> InterestedClientIds { get; set; }

        /// <summary>
        /// Konstruktor: əlavə kolleksiyaları initialize edir
        /// </summary>
        public PropertyDetail()
        {
            ImagePaths = new List<string>();
            Features = new Dictionary<string, bool>();
            TransactionIds = new List<int>();
            ExpenseIds = new List<int>();
            InterestedClientIds = new List<int>();
        }

        /// <summary>
        /// Əmlak tiplərini sadalayan enum
        /// </summary>
        public enum PropertyTypes
        {
            Apartment = 0,    // Mənzil
            House = 1,        // Ev / Villa
            Office = 2,       // Ofis
            Commercial = 3,   // Kommersiya obyekti
            Land = 4,         // Torpaq sahəsi
            Garage = 5,       // Qaraj
            Other = 6         // Digər
        }

        /// <summary>
        /// Bina tiplərini sadalayan enum
        /// </summary>
        public enum BuildingTypes
        {
            NewBuilding = 0,      // Yeni tikili
            OldBuilding = 1,      // Köhnə tikili
            House = 2,            // Həyət evi
            Villa = 3,            // Villa
            Garden = 4,           // Bağ evi
            OfficeBuilding = 5,   // Ofis binası
            Industrial = 6,       // Sənaye binası
            Other = 7             // Digər
        }

        /// <summary>
        /// Elan tiplərini sadalayan enum
        /// </summary>
        public enum ListingTypes
        {
            Sale = 0,         // Satış
            Rent = 1,         // Kirayə
            DailyRent = 2,    // Günlük kirayə
            Exchange = 3,     // Mübadilə
            Other = 4         // Digər
        }

        /// <summary>
        /// Əmlak statuslarını sadalayan enum
        /// </summary>
        public enum PropertyStatus
        {
            Available = 0,    // Mövcud
            Sold = 1,         // Satılıb
            Rented = 2,       // Kirayə verilib
            Reserved = 3,     // Rezerv edilib
            NotAvailable = 4, // Mövcud deyil
            UnderRenovation = 5,  // Təmir edilir
            UnderConstruction = 6 // Tikinti mərhələsində
        }

        /// <summary>
        /// Standart əmlak xüsusiyyətlərini qaytaran metod
        /// </summary>
        /// <returns>Xüsusiyyət adlarının siyahısı</returns>
        public static List<string> GetStandardFeatures()
        {
            return new List<string>
            {
                "Kondisioner",
                "Mərkəzi istilik",
                "Qaz",
                "Su",
                "İşıq",
                "Kanalizasiya",
                "Telefon",
                "İnternet",
                "Kabel TV",
                "Lift",
                "Yeraltı qaraj",
                "Yerüstü qaraj",
                "Mebel",
                "Mətbəx mebelləri",
                "Mühafizə",
                "Domofon",
                "Uşaq meydançası",
                "Qapalı həyət",
                "Eyvan",
                "Hovuz",
                "Sauna"
            };
        }
    }
}