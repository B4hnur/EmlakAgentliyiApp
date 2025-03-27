using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Text.RegularExpressions;
using System.Windows.Forms;

namespace DashinmazEmlakApp.Utilities
{
    /// <summary>
    /// Köməkçi funksiyalar sinifi
    /// </summary>
    public static class Helpers
    {
        #region String Köməkçiləri

        /// <summary>
        /// Mətnin boş olub olmadığını yoxlayır
        /// </summary>
        public static bool IsNullOrEmpty(this string text)
        {
            return string.IsNullOrEmpty(text) || string.IsNullOrWhiteSpace(text);
        }

        /// <summary>
        /// Mətni müəyyən bir uzunluqda kəsir və nöqtə əlavə edir
        /// </summary>
        public static string Truncate(this string text, int maxLength)
        {
            if (text == null) return null;
            return text.Length <= maxLength ? text : text.Substring(0, maxLength) + "...";
        }

        /// <summary>
        /// Telefon nömrəsinin düzgün formatda olmasını yoxlayır
        /// </summary>
        public static bool IsValidPhoneNumber(this string phone)
        {
            if (phone.IsNullOrEmpty()) return false;
            return Regex.IsMatch(phone, @"^(?:\+994|0)[1-9][0-9]{8}$");
        }

        /// <summary>
        /// E-poçt ünvanının düzgün formatda olmasını yoxlayır
        /// </summary>
        public static bool IsValidEmail(this string email)
        {
            if (email.IsNullOrEmpty()) return false;
            return Regex.IsMatch(email, @"^[^@\s]+@[^@\s]+\.[^@\s]+$");
        }

        /// <summary>
        /// Pul məbləğini formatlaşdırır
        /// </summary>
        public static string FormatCurrency(this decimal amount)
        {
            return $"{amount.ToString(Constants.CurrencyFormat)} {Constants.CurrencySymbol}";
        }

        /// <summary>
        /// Pul məbləğini formatlaşdırır
        /// </summary>
        public static string FormatCurrency(this double amount)
        {
            return $"{amount.ToString(Constants.CurrencyFormat)} {Constants.CurrencySymbol}";
        }

        /// <summary>
        /// Sahə dəyərini formatlaşdırır
        /// </summary>
        public static string FormatArea(this double area)
        {
            return $"{area:0.##} {Constants.AreaUnit}";
        }

        #endregion

        #region Tarix Köməkçiləri

        /// <summary>
        /// Tarixi formatlaşdırır
        /// </summary>
        public static string FormatDate(this DateTime date)
        {
            return date.ToString(Constants.DateFormat);
        }

        /// <summary>
        /// Tarix və saatı formatlaşdırır
        /// </summary>
        public static string FormatDateTime(this DateTime dateTime)
        {
            return dateTime.ToString(Constants.DateTimeFormat);
        }

        /// <summary>
        /// SQLite üçün tarixin string formatını qaytarır
        /// </summary>
        public static string ToSQLiteDate(this DateTime date)
        {
            return date.ToString("yyyy-MM-dd");
        }

        /// <summary>
        /// SQLite üçün tarix və saatın string formatını qaytarır
        /// </summary>
        public static string ToSQLiteDateTime(this DateTime dateTime)
        {
            return dateTime.ToString("yyyy-MM-dd HH:mm:ss");
        }

        /// <summary>
        /// Bu həftənin başlanğıc tarixini qaytarır (Bazar ertəsi)
        /// </summary>
        public static DateTime GetStartOfWeek(this DateTime date)
        {
            int diff = (7 + (date.DayOfWeek - DayOfWeek.Monday)) % 7;
            return date.AddDays(-1 * diff).Date;
        }

        /// <summary>
        /// Bu ayın başlanğıc tarixini qaytarır
        /// </summary>
        public static DateTime GetStartOfMonth(this DateTime date)
        {
            return new DateTime(date.Year, date.Month, 1);
        }

        /// <summary>
        /// Bu ayın son tarixini qaytarır
        /// </summary>
        public static DateTime GetEndOfMonth(this DateTime date)
        {
            return new DateTime(date.Year, date.Month, DateTime.DaysInMonth(date.Year, date.Month));
        }

        /// <summary>
        /// Bu ilin başlanğıc tarixini qaytarır
        /// </summary>
        public static DateTime GetStartOfYear(this DateTime date)
        {
            return new DateTime(date.Year, 1, 1);
        }

        #endregion

        #region UI Köməkçiləri

        /// <summary>
        /// Form elementin mərkəzində yerləşdirir
        /// </summary>
        public static void CenterInForm(this Control control, Form form)
        {
            control.Left = (form.ClientSize.Width - control.Width) / 2;
            control.Top = (form.ClientSize.Height - control.Height) / 2;
        }

        /// <summary>
        /// ListViev'nun sütun başlıqlarını avtomatik tənzimləyir
        /// </summary>
        public static void ResizeColumnHeaders(this ListView listView)
        {
            foreach (ColumnHeader column in listView.Columns)
            {
                column.Width = -2; // Auto-size by column content
            }
        }

        /// <summary>
        /// ComboBox üçün avtomatik genişlik tənzimləyici
        /// </summary>
        public static void SetDropDownWidth(this ComboBox comboBox)
        {
            int maxWidth = 0;

            foreach (var item in comboBox.Items)
            {
                int itemWidth = TextRenderer.MeasureText(comboBox.GetItemText(item), comboBox.Font).Width;
                maxWidth = Math.Max(maxWidth, itemWidth);
            }

            // Add a buffer for better appearance
            maxWidth += 20;

            // Set the dropdown width to the max item width or the control width if it's greater
            comboBox.DropDownWidth = Math.Max(maxWidth, comboBox.Width);
        }

        /// <summary>
        /// DataGridView'un sütunlarını avtomatik ölçüləndirir
        /// </summary>
        public static void AutoResizeColumns(this DataGridView dataGridView, bool headerAutoSize = false)
        {
            // Freeze the grid to prevent flicker during resizing
            dataGridView.SuspendLayout();

            foreach (DataGridViewColumn column in dataGridView.Columns)
            {
                if (column.Visible)
                {
                    if (headerAutoSize)
                    {
                        column.AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;
                    }
                    else
                    {
                        column.AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;
                    }
                }
            }

            // Resume layout
            dataGridView.ResumeLayout();
        }

        /// <summary>
        /// Formun bütün TextBox, ComboBox və DateTimePicker elementlərini təmizləyir
        /// </summary>
        public static void ClearFormControls(this Control container)
        {
            foreach (Control control in container.Controls)
            {
                if (control is TextBox textBox)
                {
                    textBox.Clear();
                }
                else if (control is ComboBox comboBox)
                {
                    if (comboBox.Items.Count > 0)
                    {
                        comboBox.SelectedIndex = 0;
                    }
                }
                else if (control is DateTimePicker dateTimePicker)
                {
                    dateTimePicker.Value = DateTime.Now;
                }
                else if (control is CheckBox checkBox)
                {
                    checkBox.Checked = false;
                }
                else if (control is NumericUpDown numericUpDown)
                {
                    numericUpDown.Value = numericUpDown.Minimum;
                }
                else if (control.HasChildren)
                {
                    ClearFormControls(control); // Recurse into child controls
                }
            }
        }

        /// <summary>
        /// Məlumat mesajı göstərir
        /// </summary>
        public static void ShowInfoMessage(string message, string title = "Məlumat")
        {
            MessageBox.Show(message, title, MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        /// <summary>
        /// Xəta mesajı göstərir
        /// </summary>
        public static void ShowErrorMessage(string message, string title = "Xəta")
        {
            MessageBox.Show(message, title, MessageBoxButtons.OK, MessageBoxIcon.Error);
        }

        /// <summary>
        /// Xəbərdarlıq mesajı göstərir
        /// </summary>
        public static void ShowWarningMessage(string message, string title = "Xəbərdarlıq")
        {
            MessageBox.Show(message, title, MessageBoxButtons.OK, MessageBoxIcon.Warning);
        }

        /// <summary>
        /// Sual mesajı göstərir
        /// </summary>
        public static DialogResult ShowQuestionMessage(string message, string title = "Sual")
        {
            return MessageBox.Show(message, title, MessageBoxButtons.YesNo, MessageBoxIcon.Question);
        }

        #endregion

        #region Şəkil Köməkçiləri

        /// <summary>
        /// Şəkili ölçüsünü dəyişir, nisbətləri qoruyaraq ölçüsünü kiçildir
        /// </summary>
        public static Image ResizeImage(this Image image, int maxWidth, int maxHeight)
        {
            if (image == null)
                return null;

            var ratioX = (double)maxWidth / image.Width;
            var ratioY = (double)maxHeight / image.Height;
            var ratio = Math.Min(ratioX, ratioY);

            var newWidth = (int)(image.Width * ratio);
            var newHeight = (int)(image.Height * ratio);

            var newImage = new Bitmap(newWidth, newHeight);

            using (var graphics = Graphics.FromImage(newImage))
            {
                graphics.InterpolationMode = InterpolationMode.HighQualityBicubic;
                graphics.DrawImage(image, 0, 0, newWidth, newHeight);
            }

            return newImage;
        }

        /// <summary>
        /// Filigranı (watermark) silir
        /// </summary>
        public static Image RemoveWatermark(this Image image, Rectangle watermarkArea)
        {
            if (image == null)
                return null;

            // Make a copy of the image
            Bitmap result = new Bitmap(image);

            using (Graphics g = Graphics.FromImage(result))
            {
                // Find average color around watermark
                Color averageColor = GetAverageColorAround(image, watermarkArea);

                // Fill watermark area with average color
                using (SolidBrush brush = new SolidBrush(averageColor))
                {
                    g.FillRectangle(brush, watermarkArea);
                }
            }

            return result;
        }

        /// <summary>
        /// Şəkil ətrafındakı orta rəngi tapır
        /// </summary>
        private static Color GetAverageColorAround(Image image, Rectangle area)
        {
            Bitmap bitmap = new Bitmap(image);

            int totalPixels = 0;
            int redSum = 0, greenSum = 0, blueSum = 0;

            // Sample pixels around the area
            int sampleSize = 20; // Pixels to sample on each side

            // Top edge
            for (int x = Math.Max(0, area.Left - sampleSize); x < Math.Min(bitmap.Width, area.Right + sampleSize); x++)
            {
                for (int y = Math.Max(0, area.Top - sampleSize); y < area.Top; y++)
                {
                    Color pixel = bitmap.GetPixel(x, y);
                    redSum += pixel.R;
                    greenSum += pixel.G;
                    blueSum += pixel.B;
                    totalPixels++;
                }
            }

            // Bottom edge
            for (int x = Math.Max(0, area.Left - sampleSize); x < Math.Min(bitmap.Width, area.Right + sampleSize); x++)
            {
                for (int y = area.Bottom; y < Math.Min(bitmap.Height, area.Bottom + sampleSize); y++)
                {
                    Color pixel = bitmap.GetPixel(x, y);
                    redSum += pixel.R;
                    greenSum += pixel.G;
                    blueSum += pixel.B;
                    totalPixels++;
                }
            }

            // Left edge
            for (int y = Math.Max(0, area.Top - sampleSize); y < Math.Min(bitmap.Height, area.Bottom + sampleSize); y++)
            {
                for (int x = Math.Max(0, area.Left - sampleSize); x < area.Left; x++)
                {
                    Color pixel = bitmap.GetPixel(x, y);
                    redSum += pixel.R;
                    greenSum += pixel.G;
                    blueSum += pixel.B;
                    totalPixels++;
                }
            }

            // Right edge
            for (int y = Math.Max(0, area.Top - sampleSize); y < Math.Min(bitmap.Height, area.Bottom + sampleSize); y++)
            {
                for (int x = area.Right; x < Math.Min(bitmap.Width, area.Right + sampleSize); x++)
                {
                    Color pixel = bitmap.GetPixel(x, y);
                    redSum += pixel.R;
                    greenSum += pixel.G;
                    blueSum += pixel.B;
                    totalPixels++;
                }
            }

            if (totalPixels == 0) return Color.White; // Default if no pixels were sampled

            int avgRed = redSum / totalPixels;
            int avgGreen = greenSum / totalPixels;
            int avgBlue = blueSum / totalPixels;

            return Color.FromArgb(avgRed, avgGreen, avgBlue);
        }

        /// <summary>
        /// Şəkili JPEG olaraq yaddaşa alır
        /// </summary>
        public static bool SaveAsJpeg(this Image image, string fileName, long quality = 90L)
        {
            try
            {
                if (image == null) return false;

                // Create an Encoder object for the Quality parameter
                EncoderParameters encoderParameters = new EncoderParameters(1);
                EncoderParameter encoderParameter = new EncoderParameter(Encoder.Quality, quality);
                encoderParameters.Param[0] = encoderParameter;

                // Get the JPEG codec
                ImageCodecInfo jpegCodec = GetEncoderInfo("image/jpeg");

                if (jpegCodec == null) return false;

                // Save the image with the specified quality
                image.Save(fileName, jpegCodec, encoderParameters);

                return true;
            }
            catch
            {
                return false;
            }
        }

        /// <summary>
        /// MIME tipindən codec məlumatını alır
        /// </summary>
        private static ImageCodecInfo GetEncoderInfo(string mimeType)
        {
            ImageCodecInfo[] codecs = ImageCodecInfo.GetImageEncoders();

            foreach (var codec in codecs)
            {
                if (codec.MimeType == mimeType)
                {
                    return codec;
                }
            }

            return null;
        }

        /// <summary>
        /// Şəkil faylını Image obyektinə yükləyir
        /// </summary>
        public static Image LoadImageFromFile(string fileName)
        {
            try
            {
                if (!File.Exists(fileName)) return null;

                // Create a memory stream to avoid file locks
                using (var stream = new MemoryStream(File.ReadAllBytes(fileName)))
                {
                    return Image.FromStream(stream);
                }
            }
            catch
            {
                return null;
            }
        }

        #endregion

        #region Fayl Köməkçiləri

        /// <summary>
        /// Fayl adını təmizləyərək etibarlı edir
        /// </summary>
        public static string SanitizeFileName(string fileName)
        {
            if (string.IsNullOrEmpty(fileName)) return fileName;

            // Replace invalid file name characters with underscore
            string invalidChars = new string(Path.GetInvalidFileNameChars());
            string invalidReStr = $"[{Regex.Escape(invalidChars)}]";

            return Regex.Replace(fileName, invalidReStr, "_");
        }

        /// <summary>
        /// Məlumatları CSV faylına yazır
        /// </summary>
        public static bool ExportToCsv<T>(IEnumerable<T> data, string filePath, char delimiter = ',')
        {
            try
            {
                if (data == null) return false;

                using (var writer = new StreamWriter(filePath, false, Encoding.UTF8))
                {
                    // Write header
                    var properties = typeof(T).GetProperties();
                    writer.WriteLine(string.Join(delimiter.ToString(), properties.Select(p => p.Name)));

                    // Write data rows
                    foreach (var item in data)
                    {
                        string line = string.Join(delimiter.ToString(),
                            properties.Select(p =>
                            {
                                var value = p.GetValue(item);
                                return value != null ? FormatValueForCsv(value.ToString(), delimiter) : string.Empty;
                            }));

                        writer.WriteLine(line);
                    }
                }

                return true;
            }
            catch
            {
                return false;
            }
        }

        /// <summary>
        /// CSV məlumatı üçün dəyərləri formatlaşdırır
        /// </summary>
        private static string FormatValueForCsv(string value, char delimiter)
        {
            // If value contains delimiter, newline, or quotes, enclose in quotes
            if (value.Contains(delimiter.ToString()) || value.Contains("\n") || value.Contains("\""))
            {
                // Double up any embedded quotes
                return $"\"{value.Replace("\"", "\"\"")}\"";
            }

            return value;
        }

        /// <summary>
        /// Faylın şəkil olub olmadığını yoxlayır
        /// </summary>
        public static bool IsImageFile(string filePath)
        {
            if (!File.Exists(filePath)) return false;

            string extension = Path.GetExtension(filePath).ToLower();
            string[] imageExtensions = { ".jpg", ".jpeg", ".png", ".gif", ".bmp", ".tiff" };

            return imageExtensions.Contains(extension);
        }

        /// <summary>
        /// Unikal fayl adı yaradır
        /// </summary>
        public static string GetUniqueFileName(string directory, string fileName)
        {
            string fileNameOnly = Path.GetFileNameWithoutExtension(fileName);
            string extension = Path.GetExtension(fileName);
            string path = Path.Combine(directory, fileName);
            int count = 1;

            while (File.Exists(path))
            {
                string newFileName = $"{fileNameOnly}_{count++}{extension}";
                path = Path.Combine(directory, newFileName);
            }

            return path;
        }

        #endregion

        #region Şifrələmə Köməkçiləri

        /// <summary>
        /// Mətin üçün MD5 heş yaradır
        /// </summary>
        public static string GetMd5Hash(string input)
        {
            using (MD5 md5Hash = MD5.Create())
            {
                byte[] data = md5Hash.ComputeHash(Encoding.UTF8.GetBytes(input));

                StringBuilder sBuilder = new StringBuilder();

                for (int i = 0; i < data.Length; i++)
                {
                    sBuilder.Append(data[i].ToString("x2"));
                }

                return sBuilder.ToString();
            }
        }

        /// <summary>
        /// Şifrə təhlükəsizlik səviyyəsini yoxlayır
        /// </summary>
        public static PasswordStrength CheckPasswordStrength(string password)
        {
            if (password.Length < 6)
                return PasswordStrength.VeryWeak;

            bool hasLowercase = password.Any(char.IsLower);
            bool hasUppercase = password.Any(char.IsUpper);
            bool hasDigit = password.Any(char.IsDigit);
            bool hasSpecialChar = password.Any(c => !char.IsLetterOrDigit(c));

            if (hasLowercase && hasUppercase && hasDigit && hasSpecialChar && password.Length >= 10)
                return PasswordStrength.VeryStrong;

            if ((hasLowercase || hasUppercase) && hasDigit && hasSpecialChar && password.Length >= 8)
                return PasswordStrength.Strong;

            if ((hasLowercase || hasUppercase) && hasDigit && password.Length >= 8)
                return PasswordStrength.Medium;

            if ((hasLowercase || hasUppercase) && password.Length >= 6)
                return PasswordStrength.Weak;

            return PasswordStrength.VeryWeak;
        }

        /// <summary>
        /// Şifrə təhlükəsizlik səviyyələri
        /// </summary>
        public enum PasswordStrength
        {
            VeryWeak,
            Weak,
            Medium,
            Strong,
            VeryStrong
        }

        #endregion

        #region Digər Köməkçilər

        /// <summary>
        /// Təsadüfi string yaradır
        /// </summary>
        public static string GenerateRandomString(int length)
        {
            Random random = new Random();
            const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789";

            return new string(Enumerable.Repeat(chars, length)
                .Select(s => s[random.Next(s.Length)]).ToArray());
        }

        /// <summary>
        /// İki tarix arasındakı fərqi insanlar üçün oxunaqlı formada göstərir
        /// </summary>
        public static string GetHumanReadableTimeSpan(DateTime start, DateTime end)
        {
            TimeSpan span = end - start;

            if (span.TotalDays > 365)
            {
                int years = (int)(span.TotalDays / 365);
                return $"{years} il{(years == 1 ? "" : "lər")} əvvəl";
            }

            if (span.TotalDays > 30)
            {
                int months = (int)(span.TotalDays / 30);
                return $"{months} ay{(months == 1 ? "" : "lar")} əvvəl";
            }

            if (span.TotalDays > 1)
            {
                int days = (int)span.TotalDays;
                return $"{days} gün əvvəl";
            }

            if (span.TotalHours > 1)
            {
                int hours = (int)span.TotalHours;
                return $"{hours} saat əvvəl";
            }

            if (span.TotalMinutes > 1)
            {
                int minutes = (int)span.TotalMinutes;
                return $"{minutes} dəqiqə əvvəl";
            }

            return "İndicə";
        }

        /// <summary>
        /// Web URL-nin doğruluğunu yoxlayır
        /// </summary>
        public static bool IsValidUrl(string url)
        {
            if (string.IsNullOrEmpty(url)) return false;

            return Uri.TryCreate(url, UriKind.Absolute, out Uri uriResult)
                && (uriResult.Scheme == Uri.UriSchemeHttp || uriResult.Scheme == Uri.UriSchemeHttps);
        }

        #endregion
    }
}
