using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Net;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using DashinmazEmlakApp.Services;
using DasinmazEmlakAgentligi.Models;
using System.Windows.Forms;
using HtmlAgilityPack;
using RealEstateManager.Models;

namespace RealEstateManager.Services
{
    /// <summary>
    /// Service for scraping real estate listings from websites
    /// </summary>
    public class WebScraperService
    {
        // Dictionary to hold parsing methods for different sites
        private readonly Dictionary<string, Func<HtmlDocument, List<Property>>> _siteParsers;

        public WebScraperService()
        {
            _siteParsers = new Dictionary<string, Func<HtmlDocument, List<Property>>>
            {
                { "kub.az", ParseKubAzListing },
                { "bina.az", ParseBinaAzListing },
                { "yeniemlak.az", ParseYeniEmlakListing }
                // Add more sites as needed
            };
        }

        /// <summary>
        /// Scrapes real estate listings from a website
        /// </summary>
        public async Task<List<Property>> ScrapeListingsAsync(string url, PropertyType? propertyType = null,
            PropertyPurpose? purpose = null, string city = null, string district = null,
            double? minPrice = null, double? maxPrice = null)
        {
            try
            {
                // Parse the base URL to determine which site we're working with
                Uri uri = new Uri(url);
                string domain = uri.Host.Replace("www.", "");

                // Build the search URL with parameters
                var searchUrl = BuildSearchUrl(url, domain, propertyType, purpose, city, district, minPrice, maxPrice);

                // Load the HTML
                var htmlDoc = await LoadHtmlDocumentAsync(searchUrl);

                if (htmlDoc == null)
                {
                    return new List<Property>();
                }

                // Use the appropriate parser for this site
                if (_siteParsers.TryGetValue(domain, out var parser))
                {
                    return parser(htmlDoc);
                }

                return new List<Property>();
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"Error scraping listings: {ex.Message}");
                return new List<Property>();
            }
        }

        /// <summary>
        /// Loads an HTML document from a URL
        /// </summary>
        private async Task<HtmlDocument> LoadHtmlDocumentAsync(string url)
        {
            try
            {
                var web = new HtmlWeb();
                web.UserAgent = ConfigurationManager.AppSettings["UserAgent"];

                var htmlDoc = await Task.Run(() => web.Load(url));
                return htmlDoc;
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"Error loading HTML: {ex.Message}");
                return null;
            }
        }

        /// <summary>
        /// Builds a search URL for a specific site
        /// </summary>
        private string BuildSearchUrl(string baseUrl, string domain, PropertyType? propertyType,
            PropertyPurpose? purpose, string city, string district, double? minPrice, double? maxPrice)
        {
            switch (domain)
            {
                case "kub.az":
                    return BuildKubAzSearchUrl(baseUrl, propertyType, purpose, city, district, minPrice, maxPrice);
                case "bina.az":
                    return BuildBinaAzSearchUrl(baseUrl, propertyType, purpose, city, district, minPrice, maxPrice);
                case "yeniemlak.az":
                    return BuildYeniEmlakSearchUrl(baseUrl, propertyType, purpose, city, district, minPrice, maxPrice);
                default:
                    return baseUrl;
            }
        }

        #region Kub.az Scraping

        /// <summary>
        /// Builds a search URL for kub.az
        /// </summary>
        private string BuildKubAzSearchUrl(string baseUrl, PropertyType? propertyType,
            PropertyPurpose? purpose, string city, string district, double? minPrice, double? maxPrice)
        {
            var url = baseUrl;

            // Add search parameters
            var parameters = new List<string>();

            if (propertyType.HasValue)
            {
                parameters.Add($"entityType={(int)MapToKubAzPropertyType(propertyType.Value)}");
            }

            if (purpose.HasValue)
            {
                parameters.Add($"purpose={(int)MapToKubAzPurpose(purpose.Value)}");
            }

            if (!string.IsNullOrEmpty(city))
            {
                parameters.Add($"city={WebUtility.UrlEncode(city)}");
            }

            if (!string.IsNullOrEmpty(district))
            {
                parameters.Add($"district={WebUtility.UrlEncode(district)}");
            }

            if (minPrice.HasValue)
            {
                parameters.Add($"priceMin={minPrice.Value}");
            }

            if (maxPrice.HasValue)
            {
                parameters.Add($"priceMax={maxPrice.Value}");
            }

            if (parameters.Count > 0)
            {
                url += "/search?" + string.Join("&", parameters);
            }

            return url;
        }

        /// <summary>
        /// Maps our property type to kub.az property type
        /// </summary>
        private int MapToKubAzPropertyType(PropertyType propertyType)
        {
            switch (propertyType)
            {
                case PropertyType.Apartment:
                    return 0; // Bina ev mənzil
                case PropertyType.House:
                    return 1; // Həyət evi / Villa
                case PropertyType.Garage:
                    return 2; // Qaraj
                case PropertyType.Office:
                    return 3; // Ofis
                case PropertyType.Land:
                    return 4; // Torpaq sahəsi
                case PropertyType.Commercial:
                    return 5; // Obyekt
                default:
                    return 0;
            }
        }

        /// <summary>
        /// Maps our property purpose to kub.az purpose
        /// </summary>
        private int MapToKubAzPurpose(PropertyPurpose purpose)
        {
            switch (purpose)
            {
                case PropertyPurpose.Sale:
                    return 0; // Satılır
                case PropertyPurpose.Rent:
                    return 1; // Kirayə
                case PropertyPurpose.DailyRent:
                    return 2; // Günlük kirayə
                default:
                    return 0;
            }
        }

        /// <summary>
        /// Parses property listings from kub.az
        /// </summary>
        private List<Property> ParseKubAzListing(HtmlDocument htmlDoc)
        {
            var properties = new List<Property>();

            try
            {
                // Get all property items
                var propertyNodes = htmlDoc.DocumentNode.SelectNodes("//div[contains(@class, 'adsCardBox')]");

                if (propertyNodes == null)
                {
                    return properties;
                }

                foreach (var node in propertyNodes)
                {
                    try
                    {
                        var property = new Property();

                        // Get property title
                        var titleNode = node.SelectSingleNode(".//div[contains(@class, 'adsCardTitle')]/a");
                        if (titleNode != null)
                        {
                            property.Title = titleNode.InnerText.Trim();

                            // Get the source URL
                            property.SourceUrl = "https://kub.az" + titleNode.GetAttributeValue("href", "");
                        }

                        // Get property price
                        var priceNode = node.SelectSingleNode(".//div[contains(@class, 'adsCardPrice')]");
                        if (priceNode != null)
                        {
                            var priceText = priceNode.InnerText.Trim();

                            // Parse the price (remove currency symbol and spaces)
                            var priceMatch = Regex.Match(priceText, @"(\d+[.,\s]*\d*)");
                            if (priceMatch.Success)
                            {
                                var priceStr = priceMatch.Groups[1].Value.Replace(" ", "").Replace(",", ".");
                                double.TryParse(priceStr, out double price);
                                property.Price = price;
                            }
                        }

                        // Get property details (area, rooms, etc.)
                        var detailsNode = node.SelectSingleNode(".//div[contains(@class, 'adsCardDescription')]");
                        if (detailsNode != null)
                        {
                            var detailsText = detailsNode.InnerText.Trim();
                            property.Description = detailsText;

                            // Try to extract room count
                            var roomsMatch = Regex.Match(detailsText, @"(\d+)\s*otaq");
                            if (roomsMatch.Success)
                            {
                                int.TryParse(roomsMatch.Groups[1].Value, out int rooms);
                                property.Rooms = rooms;
                            }

                            // Try to extract area
                            var areaMatch = Regex.Match(detailsText, @"(\d+[.,]?\d*)\s*m²");
                            if (areaMatch.Success)
                            {
                                var areaStr = areaMatch.Groups[1].Value.Replace(",", ".");
                                double.TryParse(areaStr, out double area);
                                property.Area = area;
                            }

                            // Try to extract floor information
                            var floorMatch = Regex.Match(detailsText, @"(\d+)/(\d+)\s*mərtəbə");
                            if (floorMatch.Success)
                            {
                                int.TryParse(floorMatch.Groups[1].Value, out int floor);
                                int.TryParse(floorMatch.Groups[2].Value, out int totalFloors);
                                property.Floor = floor;
                                property.TotalFloors = totalFloors;
                            }
                        }

                        // Get property location
                        var locationNode = node.SelectSingleNode(".//div[contains(@class, 'adsCardLocation')]");
                        if (locationNode != null)
                        {
                            var location = locationNode.InnerText.Trim();
                            var locationParts = location.Split(',');

                            if (locationParts.Length >= 2)
                            {
                                property.City = locationParts[0].Trim();
                                property.District = locationParts[1].Trim();
                            }
                            else if (locationParts.Length == 1)
                            {
                                property.City = locationParts[0].Trim();
                            }
                        }

                        // Get property type and purpose
                        var categoryNode = node.SelectSingleNode(".//div[contains(@class, 'adsCardCategory')]");
                        if (categoryNode != null)
                        {
                            var category = categoryNode.InnerText.Trim().ToLower();

                            // Determine purpose
                            if (category.Contains("satılır"))
                            {
                                property.Purpose = PropertyPurpose.Sale;
                            }
                            else if (category.Contains("kirayə"))
                            {
                                property.Purpose = category.Contains("günlük")
                                    ? PropertyPurpose.DailyRent
                                    : PropertyPurpose.Rent;
                            }

                            // Determine property type
                            if (category.Contains("mənzil") || category.Contains("bina"))
                            {
                                property.PropertyType = PropertyType.Apartment;
                            }
                            else if (category.Contains("ev") || category.Contains("villa"))
                            {
                                property.PropertyType = PropertyType.House;
                            }
                            else if (category.Contains("qaraj"))
                            {
                                property.PropertyType = PropertyType.Garage;
                            }
                            else if (category.Contains("ofis"))
                            {
                                property.PropertyType = PropertyType.Office;
                            }
                            else if (category.Contains("torpaq"))
                            {
                                property.PropertyType = PropertyType.Land;
                            }
                            else if (category.Contains("obyekt"))
                            {
                                property.PropertyType = PropertyType.Commercial;
                            }
                            else
                            {
                                property.PropertyType = PropertyType.Apartment; // Default
                            }
                        }

                        // Get owner type
                        var ownerTypeNode = node.SelectSingleNode(".//div[contains(@class, 'adsCardOwnerType')]");
                        if (ownerTypeNode != null)
                        {
                            var ownerType = ownerTypeNode.InnerText.Trim().ToLower();
                            property.OwnerType = ownerType.Contains("mülkiyyətçi")
                                ? OwnerType.Owner
                                : OwnerType.Agent;
                        }

                        // Set source website
                        property.SourceWebsite = "kub.az";

                        properties.Add(property);
                    }
                    catch (Exception ex)
                    {
                        System.Diagnostics.Debug.WriteLine($"Error parsing property: {ex.Message}");
                        continue;
                    }
                }
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"Error parsing Kub.az listings: {ex.Message}");
            }

            return properties;
        }

        #endregion

        #region Bina.az Scraping

        /// <summary>
        /// Builds a search URL for bina.az
        /// </summary>
        private string BuildBinaAzSearchUrl(string baseUrl, PropertyType? propertyType,
            PropertyPurpose? purpose, string city, string district, double? minPrice, double? maxPrice)
        {
            // Implementation for Bina.az
            // Similar to Kub.az implementation but with Bina.az specific parameters
            return baseUrl;
        }

        /// <summary>
        /// Parses property listings from bina.az
        /// </summary>
        private List<Property> ParseBinaAzListing(HtmlDocument htmlDoc)
        {
            // Implementation for Bina.az
            // Similar to Kub.az implementation but with Bina.az specific HTML structure
            return new List<Property>();
        }

        #endregion

        #region YeniEmlak.az Scraping

        /// <summary>
        /// Builds a search URL for yeniemlak.az
        /// </summary>
        private string BuildYeniEmlakSearchUrl(string baseUrl, PropertyType? propertyType,
            PropertyPurpose? purpose, string city, string district, double? minPrice, double? maxPrice)
        {
            // Implementation for YeniEmlak.az
            // Similar to Kub.az implementation but with YeniEmlak.az specific parameters
            return baseUrl;
        }

        /// <summary>
        /// Parses property listings from yeniemlak.az
        /// </summary>
        private List<Property> ParseYeniEmlakListing(HtmlDocument htmlDoc)
        {
            // Implementation for YeniEmlak.az
            // Similar to Kub.az implementation but with YeniEmlak.az specific HTML structure
            return new List<Property>();
        }

        #endregion

        /// <summary>
        /// Downloads property images from a listing page
        /// </summary>
        public async Task<List<string>> DownloadPropertyImagesAsync(string url, string savePath)
        {
            var imageUrls = new List<string>();

            try
            {
                // Create directory if it doesn't exist
                if (!Directory.Exists(savePath))
                {
                    Directory.CreateDirectory(savePath);
                }

                // Load the HTML
                var htmlDoc = await LoadHtmlDocumentAsync(url);

                if (htmlDoc == null)
                {
                    return imageUrls;
                }

                // Parse the base URL to determine which site we're working with
                Uri uri = new Uri(url);
                string domain = uri.Host.Replace("www.", "");

                // Extract image URLs based on the site
                switch (domain)
                {
                    case "kub.az":
                        imageUrls = ExtractKubAzImages(htmlDoc);
                        break;
                    case "bina.az":
                        imageUrls = ExtractBinaAzImages(htmlDoc);
                        break;
                    case "yeniemlak.az":
                        imageUrls = ExtractYeniEmlakImages(htmlDoc);
                        break;
                }

                // Download and process images
                var downloadedPaths = new List<string>();
                var webClient = new WebClient();

                for (int i = 0; i < imageUrls.Count; i++)
                {
                    try
                    {
                        var imageUrl = imageUrls[i];
                        var fileName = $"image_{i + 1}.jpg";
                        var filePath = Path.Combine(savePath, fileName);

                        // Download the image
                        await webClient.DownloadFileTaskAsync(new Uri(imageUrl), filePath);

                        // Process the image to remove watermarks
                        var processedPath = ImageProcessingService.RemoveWatermark(filePath);

                        downloadedPaths.Add(processedPath);
                    }
                    catch (Exception ex)
                    {
                        System.Diagnostics.Debug.WriteLine($"Error downloading image: {ex.Message}");
                        continue;
                    }
                }

                return downloadedPaths;
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"Error downloading property images: {ex.Message}");
                return imageUrls;
            }
        }

        /// <summary>
        /// Extracts image URLs from kub.az property page
        /// </summary>
        private List<string> ExtractKubAzImages(HtmlDocument htmlDoc)
        {
            var imageUrls = new List<string>();

            try
            {
                // Get image nodes
                var imageNodes = htmlDoc.DocumentNode.SelectNodes("//div[contains(@class, 'detailsImagesWrapper')]//img");

                if (imageNodes != null)
                {
                    foreach (var imageNode in imageNodes)
                    {
                        var imageUrl = imageNode.GetAttributeValue("src", "");

                        if (!string.IsNullOrEmpty(imageUrl))
                        {
                            if (!imageUrl.StartsWith("http"))
                            {
                                imageUrl = "https://kub.az" + imageUrl;
                            }

                            imageUrls.Add(imageUrl);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"Error extracting Kub.az images: {ex.Message}");
            }

            return imageUrls;
        }

        /// <summary>
        /// Extracts image URLs from bina.az property page
        /// </summary>
        private List<string> ExtractBinaAzImages(HtmlDocument htmlDoc)
        {
            // Implementation for Bina.az
            return new List<string>();
        }

        /// <summary>
        /// Extracts image URLs from yeniemlak.az property page
        /// </summary>
        private List<string> ExtractYeniEmlakImages(HtmlDocument htmlDoc)
        {
            // Implementation for YeniEmlak.az
            return new List<string>();
        }
    }
}
