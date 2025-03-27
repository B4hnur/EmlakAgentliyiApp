using System;
using System.Collections.Generic;
using System.Windows.Forms;
using System.IO;
using System.Threading.Tasks;
using DashinmazEmlakApp.Database;
using DashinmazEmlakApp.Database.Models;
using DashinmazEmlakApp.Services;
using System.Data.SQLite;
using System.Drawing;
using DasinmazEmlakAgentligi.Database;
using DasinmazEmlakAgentligi.Models;
using RealEstateManager.Services;

namespace DashinmazEmlakApp.Forms
{
    public partial class PropertyListingForm : Form
    {
        private readonly DatabaseManager _dbManager;
        private readonly WebScraperService _webScraperService;
        private readonly ImageProcessingService _imageProcessingService;

        private List<PropertyListing> _currentListings = new List<PropertyListing>();
        private int _currentPage = 1;
        private bool _isSearching = false;

        public PropertyListingForm(DatabaseManager dbManager, WebScraperService webScraperService, ImageProcessingService imageProcessingService)
        {
            InitializeComponent();

            _dbManager = dbManager;
            _webScraperService = webScraperService;
            _imageProcessingService = imageProcessingService;

            // Set up event handlers
            this.Load += PropertyListingForm_Load;

            // Initialize controls
            InitializeComboBoxes();
        }

        private void PropertyListingForm_Load(object sender, EventArgs e)
        {
            // Set the form title
            this.Text = "Əmlak Elanları";

            // Load saved listings
            LoadSavedListings();
        }

        private void InitializeComboBoxes()
        {
            // Property Type
            cmbPropertyType.Items.Add(new { Text = "Bina ev mənzil", Value = PropertyType.Apartment });
            cmbPropertyType.Items.Add(new { Text = "Həyət evi / Villa", Value = PropertyType.House });
            cmbPropertyType.Items.Add(new { Text = "Qaraj", Value = PropertyType.Garage });
            cmbPropertyType.Items.Add(new { Text = "Ofis", Value = PropertyType.Office });
            cmbPropertyType.Items.Add(new { Text = "Torpaq sahəsi", Value = PropertyType.Land });
            cmbPropertyType.Items.Add(new { Text = "Obyekt", Value = PropertyType.Commercial });
            cmbPropertyType.DisplayMember = "Text";
            cmbPropertyType.ValueMember = "Value";
            cmbPropertyType.SelectedIndex = 0;

            // Purpose
            cmbPurpose.Items.Add(new { Text = "Satılır", Value = Purpose.Sale });
            cmbPurpose.Items.Add(new { Text = "Kirayə", Value = Purpose.Rent });
            cmbPurpose.Items.Add(new { Text = "Günlük kirayə", Value = Purpose.DailyRent });
            cmbPurpose.DisplayMember = "Text";
            cmbPurpose.ValueMember = "Value";
            cmbPurpose.SelectedIndex = 0;
        }

        private async void btnSearch_Click(object sender, EventArgs e)
        {
            try
            {
                _isSearching = true;
                _currentPage = 1;

                // Get search parameters
                PropertyType propertyType = (PropertyType)((dynamic)cmbPropertyType.SelectedItem).Value;
                Purpose purpose = (Purpose)((dynamic)cmbPurpose.SelectedItem).Value;
                string location = txtLocation.Text.Trim();

                double? minPrice = null;
                if (double.TryParse(txtMinPrice.Text, out double min))
                {
                    minPrice = min;
                }

                double? maxPrice = null;
                if (double.TryParse(txtMaxPrice.Text, out double max))
                {
                    maxPrice = max;
                }

                // Update UI
                lblStatus.Text = "Elanlar axtarılır...";
                btnSearch.Enabled = false;
                btnNextPage.Enabled = false;
                btnPrevPage.Enabled = false;
                Application.DoEvents();

                // Search for listings
                _currentListings = await _webScraperService.ScrapeKubAzListings(
                    propertyType,
                    purpose,
                    location,
                    minPrice,
                    maxPrice,
                    _currentPage);

                // Display results
                DisplayListings(_currentListings);

                // Update UI
                lblStatus.Text = $"{_currentListings.Count} elan tapıldı";
                btnSearch.Enabled = true;
                btnNextPage.Enabled = _currentListings.Count > 0;
                btnPrevPage.Enabled = _currentPage > 1;

                _isSearching = false;
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Axtarış zamanı xəta baş verdi: {ex.Message}", "Xəta", MessageBoxButtons.OK, MessageBoxIcon.Error);

                // Update UI
                lblStatus.Text = "Axtarış xətası";
                btnSearch.Enabled = true;

                _isSearching = false;
            }
        }

        private void DisplayListings(List<PropertyListing> listings)
        {
            lvwListings.Items.Clear();

            foreach (var listing in listings)
            {
                ListViewItem item = new ListViewItem(listing.Title);
                item.SubItems.Add(listing.Price.ToString("N0") + " ₼");
                item.SubItems.Add(listing.Address ?? "");
                item.SubItems.Add(listing.Rooms?.ToString() ?? "");
                item.SubItems.Add(listing.Area?.ToString() ?? "");
                item.SubItems.Add(listing.SourceWebsite);

                // Store the listing object for later use
                item.Tag = listing;

                lvwListings.Items.Add(item);
            }
        }

        private async void btnNextPage_Click(object sender, EventArgs e)
        {
            if (_isSearching) return;

            _currentPage++;
            await LoadPage();
        }

        private async void btnPrevPage_Click(object sender, EventArgs e)
        {
            if (_isSearching || _currentPage <= 1) return;

            _currentPage--;
            await LoadPage();
        }

        private async Task LoadPage()
        {
            try
            {
                _isSearching = true;

                // Get search parameters
                PropertyType propertyType = (PropertyType)((dynamic)cmbPropertyType.SelectedItem).Value;
                Purpose purpose = (Purpose)((dynamic)cmbPurpose.SelectedItem).Value;
                string location = txtLocation.Text.Trim();

                double? minPrice = null;
                if (double.TryParse(txtMinPrice.Text, out double min))
                {
                    minPrice = min;
                }

                double? maxPrice = null;
                if (double.TryParse(txtMaxPrice.Text, out double max))
                {
                    maxPrice = max;
                }

                // Update UI
                lblStatus.Text = $"Səhifə {_currentPage} yüklənir...";
                btnSearch.Enabled = false;
                btnNextPage.Enabled = false;
                btnPrevPage.Enabled = false;
                Application.DoEvents();

                // Search for listings
                _currentListings = await _webScraperService.ScrapeKubAzListings(
                    propertyType,
                    purpose,
                    location,
                    minPrice,
                    maxPrice,
                    _currentPage);

                // Display results
                DisplayListings(_currentListings);

                // Update UI
                lblStatus.Text = $"Səhifə {_currentPage} - {_currentListings.Count} elan tapıldı";
                btnSearch.Enabled = true;
                btnNextPage.Enabled = _currentListings.Count > 0;
                btnPrevPage.Enabled = _currentPage > 1;

                _isSearching = false;
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Səhifəni yükləyərkən xəta baş verdi: {ex.Message}", "Xəta", MessageBoxButtons.OK, MessageBoxIcon.Error);

                // Update UI
                lblStatus.Text = "Yükləmə xətası";
                btnSearch.Enabled = true;
                btnPrevPage.Enabled = _currentPage > 1;

                _isSearching = false;
            }
        }

        private async void lvwListings_DoubleClick(object sender, EventArgs e)
        {
            if (lvwListings.SelectedItems.Count == 0) return;

            try
            {
                // Get the selected listing
                PropertyListing listing = (PropertyListing)lvwListings.SelectedItems[0].Tag;

                // Open the listing in the browser
                System.Diagnostics.Process.Start(listing.SourceUrl);

                // Get details for the property
                PropertyDetail detail = await _webScraperService.GetPropertyDetails(listing.SourceUrl);

                // Show property detail form
                using (var form = new PropertyDetailForm(_dbManager, _webScraperService, _imageProcessingService, detail))
                {
                    form.ShowDialog();
                }

                // Refresh saved listings if needed
                if (tabControl.SelectedTab == tabSavedListings)
                {
                    LoadSavedListings();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Elanı açarkən xəta baş verdi: {ex.Message}", "Xəta", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void LoadSavedListings()
        {
            try
            {
                lvwSavedListings.Items.Clear();

                using (var connection = _dbManager.GetConnection())
                {
                    using (var command = new SQLiteCommand(
                        @"SELECT Id, Title, Price, Address, PropertyType, Purpose, Rooms, Area, SourceWebsite
                        FROM SavedListings
                        ORDER BY SavedDate DESC", connection))
                    {
                        using (var reader = command.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                int id = reader.GetInt32(0);
                                string title = reader.GetString(1);
                                double price = reader.GetDouble(2);
                                string address = reader.GetString(3);
                                PropertyType propertyType = (PropertyType)reader.GetInt32(4);
                                Purpose purpose = (Purpose)reader.GetInt32(5);
                                int? rooms = reader.IsDBNull(6) ? (int?)null : reader.GetInt32(6);
                                double? area = reader.IsDBNull(7) ? (double?)null : reader.GetDouble(7);
                                string sourceWebsite = reader.GetString(8);

                                ListViewItem item = new ListViewItem(title);
                                item.SubItems.Add(price.ToString("N0") + " ₼");
                                item.SubItems.Add(address);
                                item.SubItems.Add(rooms?.ToString() ?? "");
                                item.SubItems.Add(area?.ToString() ?? "");
                                item.SubItems.Add(GetPurposeText(purpose));
                                item.SubItems.Add(sourceWebsite);

                                // Store the ID for later use
                                item.Tag = id;

                                lvwSavedListings.Items.Add(item);
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Saxlanmış elanları yükləyərkən xəta baş verdi: {ex.Message}", "Xəta", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private string GetPurposeText(Purpose purpose)
        {
            switch (purpose)
            {
                case Purpose.Sale:
                    return "Satılır";
                case Purpose.Rent:
                    return "Kirayə";
                case Purpose.DailyRent:
                    return "Günlük kirayə";
                default:
                    return "";
            }
        }

        private async void lvwSavedListings_DoubleClick(object sender, EventArgs e)
        {
            if (lvwSavedListings.SelectedItems.Count == 0) return;

            try
            {
                // Get the selected listing ID
                int listingId = (int)lvwSavedListings.SelectedItems[0].Tag;

                // Get details for the property
                PropertyDetail detail = new PropertyDetail();

                using (var connection = _dbManager.GetConnection())
                {
                    // Get listing details
                    using (var command = new SQLiteCommand(
                        @"SELECT Title, Description, Address, Price, PropertyType, Purpose,
                            Rooms, Area, Floor, TotalFloors, BuildingType, SourceWebsite, SourceUrl, ContactInfo
                        FROM SavedListings
                        WHERE Id = @Id", connection))
                    {
                        command.Parameters.AddWithValue("@Id", listingId);

                        using (var reader = command.ExecuteReader())
                        {
                            if (reader.Read())
                            {
                                detail.Title = reader.GetString(0);
                                detail.Description = reader.IsDBNull(1) ? null : reader.GetString(1);
                                detail.Address = reader.GetString(2);
                                detail.Price = reader.GetDouble(3);
                                detail.PropertyType = (PropertyType)reader.GetInt32(4);
                                detail.Purpose = (Purpose)reader.GetInt32(5);
                                detail.Rooms = reader.IsDBNull(6) ? (int?)null : reader.GetInt32(6);
                                detail.Area = reader.IsDBNull(7) ? (double?)null : reader.GetDouble(7);
                                detail.Floor = reader.IsDBNull(8) ? (int?)null : reader.GetInt32(8);
                                detail.TotalFloors = reader.IsDBNull(9) ? (int?)null : reader.GetInt32(9);
                                detail.BuildingType = reader.IsDBNull(10) ? (BuildingType?)null : (BuildingType)reader.GetInt32(10);
                                detail.SourceWebsite = reader.GetString(11);
                                detail.SourceUrl = reader.GetString(12);
                                detail.ContactName = reader.IsDBNull(13) ? null : reader.GetString(13);
                            }
                        }
                    }

                    // Get listing images
                    using (var command = new SQLiteCommand(
                        @"SELECT ImagePath
                        FROM SavedListingImages
                        WHERE SavedListingId = @Id
                        ORDER BY IsMainImage DESC", connection))
                    {
                        command.Parameters.AddWithValue("@Id", listingId);

                        using (var reader = command.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                string imagePath = reader.GetString(0);
                                detail.ImageUrls.Add(imagePath);
                            }
                        }
                    }
                }

                // Show property detail form
                using (var form = new PropertyDetailForm(_dbManager, _webScraperService, _imageProcessingService, detail, true))
                {
                    form.ShowDialog();
                }

                // Refresh saved listings
                LoadSavedListings();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Elanı açarkən xəta baş verdi: {ex.Message}", "Xəta", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnDeleteSaved_Click(object sender, EventArgs e)
        {
            if (lvwSavedListings.SelectedItems.Count == 0) return;

            try
            {
                // Get the selected listing ID
                int listingId = (int)lvwSavedListings.SelectedItems[0].Tag;

                // Confirm deletion
                if (MessageBox.Show("Seçilmiş elanı silmək istədiyinizə əminsiniz?", "Təsdiq", MessageBoxButtons.YesNo, MessageBoxIcon.Question) != DialogResult.Yes)
                {
                    return;
                }

                using (var connection = _dbManager.GetConnection())
                {
                    // Start a transaction
                    using (var transaction = connection.BeginTransaction())
                    {
                        try
                        {
                            // Get the image paths to delete files
                            List<string> imagePaths = new List<string>();
                            using (var command = new SQLiteCommand(
                                "SELECT ImagePath FROM SavedListingImages WHERE SavedListingId = @Id", connection, transaction))
                            {
                                command.Parameters.AddWithValue("@Id", listingId);

                                using (var reader = command.ExecuteReader())
                                {
                                    while (reader.Read())
                                    {
                                        imagePaths.Add(reader.GetString(0));
                                    }
                                }
                            }

                            // Delete images
                            using (var command = new SQLiteCommand(
                                "DELETE FROM SavedListingImages WHERE SavedListingId = @Id", connection, transaction))
                            {
                                command.Parameters.AddWithValue("@Id", listingId);
                                command.ExecuteNonQuery();
                            }

                            // Delete listing
                            using (var command = new SQLiteCommand(
                                "DELETE FROM SavedListings WHERE Id = @Id", connection, transaction))
                            {
                                command.Parameters.AddWithValue("@Id", listingId);
                                command.ExecuteNonQuery();
                            }

                            // Commit the transaction
                            transaction.Commit();

                            // Delete image files
                            foreach (var imagePath in imagePaths)
                            {
                                try
                                {
                                    if (File.Exists(imagePath))
                                    {
                                        File.Delete(imagePath);
                                    }
                                }
                                catch (Exception ex)
                                {
                                    Console.WriteLine($"Error deleting image file: {ex.Message}");
                                }
                            }

                            // Refresh the list
                            LoadSavedListings();
                        }
                        catch (Exception ex)
                        {
                            // Rollback the transaction
                            transaction.Rollback();
                            throw;
                        }
                    }
                }

                MessageBox.Show("Elan uğurla silindi", "Məlumat", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Elanı silməyə çalışarkən xəta baş verdi: {ex.Message}", "Xəta", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void tabControl_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (tabControl.SelectedTab == tabSavedListings)
            {
                // Refresh saved listings
                LoadSavedListings();
            }
        }
    }
}
