using System;
using System.Collections.Generic;
using System.Drawing;

namespace DashinmazEmlakApp.Utilities
{
    /// <summary>
    /// Bu sinif, tətbiqin müxtəlif hissələrində istifadə olunan sabitləri saxlayır.
    /// </summary>
    public static class Constants
    {
        #region Ümumi Sabitlər

        /// <summary>
        /// Tətbiqin versiyası
        /// </summary>
        public const string AppVersion = "1.0.0";

        /// <summary>
        /// Tətbiqin tam adı
        /// </summary>
        public const string AppName = "Daşınmaz Əmlak Agentliyi İdarəetmə Proqramı";

        /// <summary>
        /// Verilənlər bazası müştərilərinin maksimum sayı seçmək üçün
        /// </summary>
        public const int MaxDatabaseRecords = 1000;

        /// <summary>
        /// Şəkil faylları üçün standart genişlənmə
        /// </summary>
        public const string DefaultImageExtension = ".jpg";

        /// <summary>
        /// Əmlak şəkillərinin saxlanılacağı qovluğun adı
        /// </summary>
        public const string PropertyImagesFolder = "Images";

        /// <summary>
        /// Daşınmaz əmlak yüklənərkən maksimum şəkil limitini təyin edir
        /// </summary>
        public const int MaxImagesPerProperty = 10;

        #endregion

        #region Rənglər

        /// <summary>
        /// Əsas brend rəngi
        /// </summary>
        public static readonly Color PrimaryColor = Color.FromArgb(45, 52, 71);

        /// <summary>
        /// İkinci dərəcəli brend rəngi
        /// </summary>
        public static readonly Color SecondaryColor = Color.FromArgb(26, 188, 156);

        /// <summary>
        /// Diqqət/xəbərdarlıq rəngi
        /// </summary>
        public static readonly Color WarningColor = Color.FromArgb(243, 156, 18);

        /// <summary>
        /// Xəta rəngi
        /// </summary>
        public static readonly Color ErrorColor = Color.FromArgb(231, 76, 60);

        /// <summary>
        /// İnformasiya rəngi
        /// </summary>
        public static readonly Color InfoColor = Color.FromArgb(52, 152, 219);

        /// <summary>
        /// Uğurlu əməliyyat rəngi
        /// </summary>
        public static readonly Color SuccessColor = Color.FromArgb(46, 204, 113);

        #endregion

        #region Kateqoriyalar və Növlər

        /// <summary>
        /// Əmlak xərclərinin kateqoriyaları
        /// </summary>
        public static readonly List<string> ExpenseCategories = new List<string>
        {
            "Ofis xərcləri",
            "Maaşlar",
            "Kommunal xərclər",
            "Reklam",
            "Nəqliyyat",
            "Texniki xidmət",
            "Digər"
        };

        /// <summary>
        /// Əmlak vəziyyətlərinin növləri
        /// </summary>
        public static readonly List<string> PropertyConditionTypes = new List<string>
        {
            "Əla",
            "Yaxşı",
            "Orta",
            "Təmirə ehtiyacı var",
            "Tikinti vəziyyətində"
        };

        /// <summary>
        /// İstilik sistemlərinin növləri
        /// </summary>
        public static readonly List<string> HeatingTypes = new List<string>
        {
            "Mərkəzi",
            "Qaz",
            "Elektrik",
            "Kombi",
            "Yoxdur"
        };

        #endregion

        #region Web Scraping Sabitləri

        /// <summary>
        /// Web scraping əməliyyatlarını aparmaq üçün istifadəçi agenti
        /// </summary>
        public const string UserAgent = "Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/91.0.4472.124 Safari/537.36";

        /// <summary>
        /// Web scraping üçün bazalar arasında minimumu gözləmə müddəti (millisaniyələrlə)
        /// </summary>
        public const int ScrapingDelayMinMs = 1000;

        /// <summary>
        /// Web scraping üçün bazalar arasında maksimum gözləmə müddəti (millisaniyələrlə)
        /// </summary>
        public const int ScrapingDelayMaxMs = 3000;

        /// <summary>
        /// Daşınmaz əmlak saytlarının URL-ləri
        /// </summary>
        public static readonly Dictionary<string, string> RealEstateSites = new Dictionary<string, string>
        {
            { "Kub.az", "https://kub.az" },
            { "Bina.az", "https://bina.az" },
            { "Yeniemlak.az", "https://yeniemlak.az" },
            { "Emlak.az", "https://emlak.az" }
        };

        /// <summary>
        /// HTTP istəkləri üçün timeout (millisaniyələrlə)
        /// </summary>
        public const int HttpRequestTimeout = 30000; // 30 saniyə

        #endregion

        #region Format sabitləri

        /// <summary>
        /// Pul məbləğlərinin formatı
        /// </summary>
        public const string CurrencyFormat = "N0";

        /// <summary>
        /// Valyuta işarəsi
        /// </summary>
        public const string CurrencySymbol = "₼";

        /// <summary>
        /// Tarix formatı
        /// </summary>
        public const string DateFormat = "dd.MM.yyyy";

        /// <summary>
        /// Tarix və vaxt formatı
        /// </summary>
        public const string DateTimeFormat = "dd.MM.yyyy HH:mm:ss";

        /// <summary>
        /// Sahə ölçü vahidi
        /// </summary>
        public const string AreaUnit = "m²";

        #endregion

        #region İstifadəçi mesajları

        /// <summary>
        /// Ümumi xəta mesajları
        /// </summary>
        public static class ErrorMessages
        {
            public const string DatabaseConnectionError = "Verilənlər bazası ilə əlaqə yaradılarkən xəta baş verdi.";
            public const string DatabaseQueryError = "Verilənlər bazasında sorğu icra edilərkən xəta baş verdi.";
            public const string RequiredFieldEmpty = "Xahiş edirik bütün tələb olunan sahələri doldurun.";
            public const string InvalidNumberFormat = "Düzgün rəqəm formatı daxil edin.";
            public const string InvalidDateFormat = "Düzgün tarix formatı daxil edin.";
            public const string WebScrapingError = "Veb saytından məlumat çəkilərkən xəta baş verdi.";
            public const string ImageProcessingError = "Şəkil emal edilərkən xəta baş verdi.";
            public const string FileAccessError = "Fayl əməliyyatı zamanı xəta baş verdi.";
            public const string NetworkError = "Şəbəkə əlaqəsi xətası baş verdi. İnternet bağlantınızı yoxlayın.";
        }

        /// <summary>
        /// Müvəffəqiyyət mesajları
        /// </summary>
        public static class SuccessMessages
        {
            public const string RecordAdded = "Məlumat uğurla əlavə edildi.";
            public const string RecordUpdated = "Məlumat uğurla yeniləndi.";
            public const string RecordDeleted = "Məlumat uğurla silindi.";
            public const string OperationCompleted = "Əməliyyat uğurla tamamlandı.";
            public const string DataImported = "Məlumatlar uğurla idxal edildi.";
            public const string DataExported = "Məlumatlar uğurla ixrac edildi.";
            public const string ImagesSaved = "Şəkillər uğurla saxlanıldı.";
        }

        /// <summary>
        /// Təsdiq mesajları
        /// </summary>
        public static class ConfirmationMessages
        {
            public const string DeleteRecord = "Bu məlumatı silmək istədiyinizə əminsiniz?";
            public const string DeleteMultipleRecords = "Seçilmiş məlumatları silmək istədiyinizə əminsiniz?";
            public const string ExitApplication = "Proqramdan çıxmaq istədiyinizə əminsiniz?";
            public const string CancelOperation = "Əməliyyatı ləğv etmək istədiyinizə əminsiniz?";
            public const string ResetSettings = "Parametrləri sıfırlamaq istədiyinizə əminsiniz?";
        }

        #endregion
    }
}
