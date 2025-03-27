using System;

namespace DashinmazEmlakApp.Utilities
{
    /// <summary>
    /// Proqramda istifadə olunan sabit dəyərləri saxlayan sinif
    /// </summary>
    public static class Constants
    {
        /// <summary>
        /// SQLite verilənlər bazası faylının adı
        /// </summary>
        public const string DatabaseFileName = "emlak_database.sqlite";

        /// <summary>
        /// Web Scraper API-nin baza URL-i
        /// </summary>
        public const string WebScraperBaseUrl = "http://localhost:5000";

        /// <summary>
        /// Daşınmaz əmlak saytlarının URL-ləri
        /// </summary>
        public static class RealEstateWebsites
        {
            public const string Kub = "https://kub.az";
            public const string Bina = "https://bina.az";
            public const string TurboAz = "https://turbo.az";
            public const string Tap = "https://tap.az";
        }

        /// <summary>
        /// Şəkil faylları üçün formatlar
        /// </summary>
        public static class ImageFormats
        {
            public static readonly string[] SupportedExtensions = { ".jpg", ".jpeg", ".png", ".gif", ".bmp" };
            public const string ImagesFolderName = "Images";
        }

        /// <summary>
        /// Hesabat formatları
        /// </summary>
        public static class ReportFormats
        {
            public const string PDF = "PDF";
            public const string Excel = "Excel";
            public const string Word = "Word";
            public const string CSV = "CSV";
        }

        /// <summary>
        /// Tarix formatları
        /// </summary>
        public static class DateFormats
        {
            public const string ShortDate = "dd.MM.yyyy";
            public const string LongDate = "dd MMMM yyyy";
            public const string TimeOnly = "HH:mm";
            public const string FullDateTime = "dd.MM.yyyy HH:mm:ss";
        }

        /// <summary>
        /// Loq faylı üçün parametrlər
        /// </summary>
        public static class LogSettings
        {
            public const string LogFolderName = "Logs";
            public const string LogFileNameFormat = "log_{0}.txt";
        }

        /// <summary>
        /// Valyuta simvolları
        /// </summary>
        public static class Currency
        {
            public const string AZN = "₼";
            public const string USD = "$";
            public const string EUR = "€";
        }
    }
}