using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using HtmlAgilityPack;
using EmlakAgentliyiApp.Models;
using EmlakAgentliyiApp.Forms.EmlakAgentliyiApp.Models;
using System.Xml;

namespace EmlakAgentliyiApp.Utils
{
    public class HtmlParser
    {
        // HTML faylından məlumatları çıxarmaq üçün
        public static string ExtractTextFromHtml(string html)
        {
            if (string.IsNullOrEmpty(html))
            {
                return string.Empty;
            }

            var doc = new HtmlAgilityPack.HtmlDocument();
            doc.LoadHtml(html);

            // Script və style təqlərini silmək
            var nodes = doc.DocumentNode.SelectNodes("//script|//style");
            if (nodes != null)
            {
                foreach (var node in nodes)
                {
                    node.Remove();
                }
            }

            // Sadəcə mətn məzmununu qaytarmaq
            return WebUtility.HtmlDecode(doc.DocumentNode.InnerText)
                .Replace("\t", " ")
                .Replace("\r", " ")
                .Replace("\n", " ");
        }

        // Mətndən qiymət məlumatını təmizləmək
        public static decimal ExtractPrice(string priceText)
        {
            if (string.IsNullOrEmpty(priceText))
            {
                return 0;
            }

            // Rəqəm olmayan bütün simvolları silmək
            string numericText = Regex.Replace(priceText, "[^0-9]", "");

            if (decimal.TryParse(numericText, out decimal price))
            {
                return price;
            }

            return 0;
        }

        // Mətndən sahə məlumatını təmizləmək
        public static double ExtractArea(string areaText)
        {
            if (string.IsNullOrEmpty(areaText))
            {
                return 0;
            }

            // Sadəcə ədədləri və ondalıq nöqtələri saxlamaq
            string numericText = Regex.Replace(areaText, "[^0-9.,]", "");

            // Vergülləri nöqtə ilə əvəz etmək
            numericText = numericText.Replace(',', '.');

            if (double.TryParse(numericText, System.Globalization.NumberStyles.Any,
                System.Globalization.CultureInfo.InvariantCulture, out double area))
            {
                return area;
            }

            return 0;
        }

        // Mətndən otaq sayını çıxarmaq
        public static int ExtractRooms(string roomsText)
        {
            if (string.IsNullOrEmpty(roomsText))
            {
                return 0;
            }

            // Sadəcə ədədləri saxlamaq
            string numericText = Regex.Replace(roomsText, "[^0-9]", "");

            if (int.TryParse(numericText, out int rooms))
            {
                return rooms;
            }

            return 0;
        }

        // KUB.AZ saytından əmlak məlumatlarını çıxarmaq
        public static Property ExtractPropertyFromKubAz(HtmlNode propertyNode, string sourceUrl)
        {
            var property = new Property();

            try
            {
                // Elanın başlığını çıxarmaq
                var titleNode = propertyNode.SelectSingleNode(".//div[contains(@class, 'caption-subject')]");
                if (titleNode != null)
                {
                    property.Title = WebUtility.HtmlDecode(titleNode.InnerText.Trim());
                }

                // Qiyməti çıxarmaq
                var priceNode = propertyNode.SelectSingleNode(".//div[contains(@class, 'price')]");
                if (priceNode != null)
                {
                    property.Price = ExtractPrice(priceNode.InnerText);
                }

                // Məkanı çıxarmaq
                var locationNode = propertyNode.SelectSingleNode(".//div[contains(@class, 'location')]");
                if (locationNode != null)
                {
                    property.Location = WebUtility.HtmlDecode(locationNode.InnerText.Trim());
                }

                // Sahəni çıxarmaq
                var areaNode = propertyNode.SelectSingleNode(".//div[contains(@class, 'area')]");
                if (areaNode != null)
                {
                    property.Area = ExtractArea(areaNode.InnerText);
                }

                // Otaq sayını çıxarmaq
                var roomsNode = propertyNode.SelectSingleNode(".//div[contains(@class, 'rooms')]");
                if (roomsNode != null)
                {
                    property.Rooms = ExtractRooms(roomsNode.InnerText);
                }

                // Əmlak növünü müəyyənləşdirmək
                var typeNode = propertyNode.SelectSingleNode(".//div[contains(@class, 'type')]");
                if (typeNode != null)
                {
                    string typeText = typeNode.InnerText.Trim().ToLower();
                    if (typeText.Contains("bina") || typeText.Contains("mənzil"))
                    {
                        property.PropertyType = 0;
                    }
                    else if (typeText.Contains("həyət") || typeText.Contains("villa"))
                    {
                        property.PropertyType = 1;
                    }
                    else if (typeText.Contains("qaraj"))
                    {
                        property.PropertyType = 2;
                    }
                    else if (typeText.Contains("ofis"))
                    {
                        property.PropertyType = 3;
                    }
                    else if (typeText.Contains("torpaq"))
                    {
                        property.PropertyType = 4;
                    }
                    else if (typeText.Contains("obyekt"))
                    {
                        property.PropertyType = 5;
                    }
                }

                // Məqsəd növünü müəyyənləşdirmək
                var purposeNode = propertyNode.SelectSingleNode(".//div[contains(@class, 'purpose')]");
                if (purposeNode != null)
                {
                    string purposeText = purposeNode.InnerText.Trim().ToLower();
                    if (purposeText.Contains("satılır"))
                    {
                        property.PurposeType = 0;
                    }
                    else if (purposeText.Contains("kirayə") && !purposeText.Contains("günlük"))
                    {
                        property.PurposeType = 1;
                    }
                    else if (purposeText.Contains("günlük"))
                    {
                        property.PurposeType = 2;
                    }
                }

                // Təsviri çıxarmaq
                var descriptionNode = propertyNode.SelectSingleNode(".//div[contains(@class, 'description')]");
                if (descriptionNode != null)
                {
                    property.Description = WebUtility.HtmlDecode(descriptionNode.InnerText.Trim());
                }

                // Mənbə və URL
                property.Source = "kub.az";
                property.SourceUrl = sourceUrl;
                property.ListingDate = DateTime.Now;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Əmlak məlumatlarını çıxararkən xəta: {ex.Message}");
            }

            return property;
        }

        // Elanın detallı səhifəsindən şəkil URL-lərini çıxarmaq
        public static List<string> ExtractImageUrlsFromPropertyPage(string html)
        {
            var imageUrls = new List<string>();

            try
            {
                var doc = new HtmlAgilityPack.HtmlDocument();
                doc.LoadHtml(html);

                // Şəkil URL-lərini çıxarmaq
                var imageNodes = doc.DocumentNode.SelectNodes("//div[contains(@class, 'imageGallery')]//img");
                if (imageNodes != null)
                {
                    foreach (var imageNode in imageNodes)
                    {
                        string src = imageNode.GetAttributeValue("src", "");
                        if (!string.IsNullOrEmpty(src) && !imageUrls.Contains(src))
                        {
                            imageUrls.Add(src);
                        }
                    }
                }

                // Alternativ metod - imgShow class ilə şəkil axtarmaq
                if (imageUrls.Count == 0)
                {
                    var altImageNodes = doc.DocumentNode.SelectNodes("//img[contains(@class, 'imgShow')]");
                    if (altImageNodes != null)
                    {
                        foreach (var imageNode in altImageNodes)
                        {
                            string src = imageNode.GetAttributeValue("src", "");
                            if (!string.IsNullOrEmpty(src) && !imageUrls.Contains(src))
                            {
                                imageUrls.Add(src);
                            }
                        }
                    }
                }

                // data-src atributunda şəkil axtarmaq
                if (imageUrls.Count == 0)
                {
                    var dataSrcNodes = doc.DocumentNode.SelectNodes("//img[@data-src]");
                    if (dataSrcNodes != null)
                    {
                        foreach (var imageNode in dataSrcNodes)
                        {
                            string src = imageNode.GetAttributeValue("data-src", "");
                            if (!string.IsNullOrEmpty(src) && !imageUrls.Contains(src))
                            {
                                imageUrls.Add(src);
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Şəkil URL-lərini çıxararkən xəta: {ex.Message}");
            }

            return imageUrls;
        }
    }
}
