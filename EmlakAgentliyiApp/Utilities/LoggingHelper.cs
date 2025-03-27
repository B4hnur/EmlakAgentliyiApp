using System;
using System.IO;

namespace DashinmazEmlakApp.Utilities
{
    /// <summary>
    /// Loqları idarə etmək üçün köməkçi sinif
    /// </summary>
    public static class LoggingHelper
    {
        private static readonly string LogDirectory = Constants.LogSettings.LogFolderName;
        private static readonly string LogFile = Path.Combine(LogDirectory, string.Format(Constants.LogSettings.LogFileNameFormat, DateTime.Now.ToString("yyyy-MM-dd")));

        /// <summary>
        /// Loq sistemi üçün lazımi qovluqları yaradır
        /// </summary>
        static LoggingHelper()
        {
            try
            {
                if (!Directory.Exists(LogDirectory))
                {
                    Directory.CreateDirectory(LogDirectory);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Loq qovluğunu yaradarkən xəta: {ex.Message}");
            }
        }

        /// <summary>
        /// İnformasiya tipli loq mesajını qeyd edir
        /// </summary>
        /// <param name="message">Loq mesajı</param>
        public static void LogInfo(string message)
        {
            LogMessage("INFO", message);
        }

        /// <summary>
        /// Xəbərdarlıq tipli loq mesajını qeyd edir
        /// </summary>
        /// <param name="message">Loq mesajı</param>
        public static void LogWarning(string message)
        {
            LogMessage("WARNING", message);
        }

        /// <summary>
        /// Xəta tipli loq mesajını qeyd edir
        /// </summary>
        /// <param name="message">Loq mesajı</param>
        public static void LogError(string message)
        {
            LogMessage("ERROR", message);
        }

        /// <summary>
        /// Kritik xəta tipli loq mesajını qeyd edir
        /// </summary>
        /// <param name="message">Loq mesajı</param>
        public static void LogCritical(string message)
        {
            LogMessage("CRITICAL", message);
        }

        /// <summary>
        /// Debug tipli loq mesajını qeyd edir
        /// </summary>
        /// <param name="message">Loq mesajı</param>
        public static void LogDebug(string message)
        {
            LogMessage("DEBUG", message);
        }

        /// <summary>
        /// Exception-ı loqa qeyd edir
        /// </summary>
        /// <param name="ex">Xəta obyekti</param>
        /// <param name="additionalInfo">Əlavə məlumat</param>
        public static void LogException(Exception ex, string additionalInfo = "")
        {
            string message = $"{ex.Message}\n{ex.StackTrace}";
            if (!string.IsNullOrEmpty(additionalInfo))
            {
                message = $"{additionalInfo}\n{message}";
            }

            LogError(message);

            if (ex.InnerException != null)
            {
                LogError($"Inner Exception: {ex.InnerException.Message}\n{ex.InnerException.StackTrace}");
            }
        }

        /// <summary>
        /// Loq faylına mesaj yazmaq üçün əsas metod
        /// </summary>
        private static void LogMessage(string level, string message)
        {
            try
            {
                string logMessage = $"{DateTime.Now:yyyy-MM-dd HH:mm:ss} [{level}] {message}";

                File.AppendAllText(LogFile, logMessage + Environment.NewLine);

                // Həm də konsola çıxar (debug zamanı faydalıdır)
                Console.WriteLine(logMessage);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Loq qeyd edərkən xəta: {ex.Message}");
            }
        }
    }
}