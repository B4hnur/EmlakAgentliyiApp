using System;
using System.Collections.Generic;

namespace DashinmazEmlakApp.Database.Models
{
    /// <summary>
    /// Daşınmaz əmlakın növünü təyin edən enum
    /// </summary>
    public enum PropertyTyp
    {
        Apartment,     // Mənzil
        House,         // Ev
        Villa,         // Villa
        Land,          // Torpaq
        Office,        // Ofis
        Store,         // Mağaza
        Warehouse,     // Anbar
        Other          // Digər
    }

    /// <summary>
    /// Satış və ya kirayə tipini təyin edən enum
    /// </summary>
    public enum ListingType
    {
        Sale,          // Satış
        Rent,          // Kirayə
        RentDaily      // Günlük kirayə
    }

    /// <summary>
    /// Əmlakın vəziyyətini təyin edən enum
    /// </summary>
    public enum PropertyStatus
    {
        Available,     // Mövcud
        Reserved,      // Rezerv olunub
        Sold,          // Satılıb
        Rented,        // Kirayə verilib
        Inactive       // Aktiv deyil
    }

    /// <summary>
    /// Daşınmaz əmlakı təmsil edən sinif
    /// </summary>
    public class Property
    {
        /// <summary>
        /// Əmlakın unikal identifikatoru
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// Əmlakın başlığı
        /// </summary>
        public string Title { get; set; }

        /// <summary>
        /// Əmlakın ətraflı təsviri
        /// </summary>
        public string Description { get; set; }

        /// <summary>
        /// Əmlakın növü
        /// </summary>
        public PropertyType Type { get; set; }

        /// <summary>
        /// Elanın tipi (satış və ya kirayə)
        /// </summary>
        public ListingType ListingType { get; set; }

        /// <summary>
        /// Əmlakın sahəsi (kvadrat metr ilə)
        /// </summary>
        public double Area { get; set; }

        /// <summary>
        /// Otaqların sayı
        /// </summary>
        public int Rooms { get; set; }

        /// <summary>
        /// Mərtəbə nömrəsi (əgər tətbiq olunursa)
        /// </summary>
        public int? Floor { get; set; }

        /// <summary>
        /// Binanın mərtəbə sayı (əgər tətbiq olunursa)
        /// </summary>
        public int? TotalFloors { get; set; }

        /// <summary>
        /// Əmlakın ünvanı
        /// </summary>
        public string Address { get; set; }

        /// <summary>
        /// Əmlakın şəhəri/rayonu
        /// </summary>
        public string City { get; set; }

        /// <summary>
        /// Əmlakın yerləşdiyi mikro rayon və ya qəsəbə
        /// </summary>
        public string District { get; set; }

        /// <summary>
        /// Əmlakın qiyməti
        /// </summary>
        public decimal Price { get; set; }

        /// <summary>
        /// Sahibkar ilə əlaqə nömrəsi
        /// </summary>
        public string ContactPhone { get; set; }

        /// <summary>
        /// Sahibkar ilə əlaqə e-poçtu
        /// </summary>
        public string ContactEmail { get; set; }

        /// <summary>
        /// Əmlak sahibinin identifikatoru
        /// </summary>
        public int? OwnerId { get; set; }

        /// <summary>
        /// Elanı əlavə edən işçinin identifikatoru
        /// </summary>
        public int? EmployeeId { get; set; }

        /// <summary>
        /// Əmlakın hal-hazırki statusu
        /// </summary>
        public PropertyStatus Status { get; set; }

        /// <summary>
        /// Əmlakın elan edildiyi tarix
        /// </summary>
        public DateTime ListingDate { get; set; }

        /// <summary>
        /// Əmlakın son yenilənmə tarixi
        /// </summary>
        public DateTime LastUpdated { get; set; }

        /// <summary>
        /// Əmlakın şəkillərinin yolu (vergüllə ayrılmış)
        /// </summary>
        public string ImagePaths { get; set; }

        /// <summary>
        /// Əmlakın mənbə saytının URL-i (əgər scraping ilə əlavə edilibsə)
        /// </summary>
        public string SourceUrl { get; set; }

        /// <summary>
        /// Əlavə xüsusiyyətlər (JSON formatında saxlanılır)
        /// </summary>
        public string Features { get; set; }

        /// <summary>
        /// İşçilərin daxili qeydləri
        /// </summary>
        public string Notes { get; set; }

        /// <summary>
        /// İzlənilən Alıcılar/Kirayəçilər siyahısı
        /// </summary>
        public List<Client> InterestedClients { get; set; }

        /// <summary>
        /// Default constructor
        /// </summary>
        public Property()
        {
            InterestedClients = new List<Client>();
            Status = PropertyStatus.Available;
            ListingDate = DateTime.Now;
            LastUpdated = DateTime.Now;
        }

        /// <summary>
        /// Şəkil yollarını massiv şəklində qaytaran metod
        /// </summary>
        /// <returns>Şəkil yolları massivi</returns>
        public string[] GetImagePathsArray()
        {
            if (string.IsNullOrEmpty(ImagePaths))
            {
                return new string[0];
            }

            return ImagePaths.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries);
        }

        /// <summary>
        /// Şəkil yollarını əlavə edən metod
        /// </summary>
        /// <param name="imagePath">Əlavə ediləcək şəkil yolu</param>
        public void AddImagePath(string imagePath)
        {
            if (string.IsNullOrEmpty(imagePath))
            {
                return;
            }

            if (string.IsNullOrEmpty(ImagePaths))
            {
                ImagePaths = imagePath;
            }
            else
            {
                ImagePaths += "," + imagePath;
            }
        }
    }
}