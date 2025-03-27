using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SQLite;
using DashinmazEmlakApp.Database;
using DashinmazEmlakApp.Database.Models;
using DashinmazEmlakApp.Services;
using DashinmazEmlakApp.Utilities;
using RealEstateManager.Services;
using DashinmazEmlakApp.Models;

namespace DashinmazEmlakApp.Forms
{
    public partial class PropertyDetailForm : Form
    {
        private readonly DatabaseManager _dbManager;
        private readonly WebScraperService _webScraperService;
        private readonly ImageProcessingService _imageProcessingService;
        private readonly PropertyDetail _propertyDetail;
        private readonly bool _isSavedProperty;

        private List<string> _imageUrls = new List<string>();
        private List<string> _processedImagePaths = new List<string>();
        private int _currentImageIndex = 0;

        public PropertyDetailForm(
            DatabaseManager dbManager,
            WebScraperService webScraperService,
            ImageProcessingService imageProcessingService,
            PropertyDetail propertyDetail,
            bool isSavedProperty = false)
        {
            InitializeComponent();

            _dbManager = dbManager;
            _webScraperService = webScraperService;
            _imageProcessingService = imageProcessingService;
            _propertyDetail = propertyDetail;
            _isSavedProperty = isSavedProperty;

            // Set up event handlers
            this.Load += PropertyDetailForm_Load;
        }

        private async void PropertyDetailForm_Load(object sender, EventArgs e)
        {
            // Set the form title
            this.Text = "Əmlak Detalları";

            // Display property details
            DisplayPropertyDetails();

            // Load images
            if (_isSavedProperty)
            {
                LoadSavedImages();
            }
            else
            {
                await LoadAndProcessImages();
            }
        }

        private void DisplayPropertyDetails()
        {
            try
            {
                // Display property title
                lblTitle.Text = _propertyDetail.Title;

                // Display price
                lblPrice.Text = _propertyDetail.Price.ToString("N0") + " ₼";

                // Display purpose
                string purposeText = "";
                switch (_propertyDetail.Purpose)
                {
                    case Purpose.Sale:
                        purposeText = "Satılır";
                        break;
                    case Purpose.Rent:
                        purposeText = "Kirayə";
                        break;
                    case Purpose.DailyRent:
                        purposeText = "Günlük kirayə";
                        break;
                }
                lblPurpose.Text = purposeText;

                // Display address
                lblAddress.Text = _propertyDetail.Address ?? "";

                // Display rooms
                lblRooms.Text = _propertyDetail.Rooms?.ToString() ?? "Məlumat yoxdur";

                // Display area
                lblArea.Text = _propertyDetail.Area.HasValue ? $"{_propertyDetail.Area.Value} m²" : "Məlumat yoxdur";

                // Display floor
                if (_propertyDetail.Floor.HasValue && _propertyDetail.TotalFloors.HasValue)
                {
                    lblFloor.Text = $"{_propertyDetail.Floor.Value}/{_propertyDetail.TotalFloors.Value}";
                }
                else
                {
                    lblFloor.Text = "Məlumat yoxdur";
                }

                // Display building type
                if (_propertyDetail.BuildingType.HasValue)
                {
                    lblBuildingType.Text = _propertyDetail.BuildingType.Value == BuildingType.New ? "Yeni tikili" : "Köhnə tikili";
                }
                else
                {
                    lblBuildingType.Text = "Məlumat yoxdur";
                }

                // Display description
                txtDescription.Text = _propertyDetail.Description ?? "Təsvir yoxdur";

                // Display source website
                lblSource.Text = _propertyDetail.SourceWebsite ?? "";

                // Display contact info
                lblContact.Text = string.IsNullOrEmpty(_propertyDetail.ContactName) ? "" : $"Əlaqə: {_propertyDetail.ContactName}";
                lblPhone.Text = string.IsNullOrEmpty(_propertyDetail.ContactPhone) ? "" : $"Telefon: {_propertyDetail.ContactPhone}";

                // Set image URLs
                _imageUrls = _propertyDetail.ImageUrls?.ToList() ?? new List<string>();

                // Update buttons state
                UpdateImageNavigationButtons();

                // Update UI based on saved state
                if (_isSavedProperty)
                {
                    btnSave.Visible = false;
                    btnBrowser.Location = new Point(btnSave.Location.X, btnSave.Location.Y);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Əmlak məlumatlarını göstərərkən xəta baş verdi: {ex.Message}", "Xəta", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private async Task LoadAndProcessImages()
        {
            try
            {
                // Show loading indicator
                picLoading.Visible = true;
                lblLoading.Visible = true;

                if (_imageUrls.Count == 0)
                {
                    // No images to load
                    picLoading.Visible = false;
                    lblLoading.Visible = false;
                    lblNoImages.Visible = true;
                    return;
                }

                // Load first image
                await LoadImageAtIndex(0);

                // Hide loading indicator
                picLoading.Visible = false;
                lblLoading.Visible = false;
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Şəkilləri yükləyərkən xəta baş verdi: {ex.Message}", "Xəta", MessageBoxButtons.OK, MessageBoxIcon.Error);

                // Hide loading indicator
                picLoading.Visible = false;
                lblLoading.Visible = false;
            }
        }

        private void LoadSavedImages()
        {
            try
            {
                // Show loading indicator
                picLoading.Visible = true;
                lblLoading.Visible = true;

                if (_imageUrls.Count == 0)
                {
                    // No images to load
                    picLoading.Visible = false;
                    lblLoading.Visible = false;
                    lblNoImages.Visible = true;
                    return;
                }

                // For saved properties, the image URLs are already file paths
                _processedImagePaths = _imageUrls.ToList();

                // Display first image
                DisplayImageAtIndex(0);

                // Hide loading indicator
                picLoading.Visible = false;
                lblLoading.Visible = false;
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Saxlanmış şəkilləri yükləyərkən xəta baş verdi: {ex.Message}", "Xəta", MessageBoxButtons.OK, MessageBoxIcon.Error);

                // Hide loading indicator
                picLoading.Visible = false;
                lblLoading.Visible = false;
            }
        }

        private async Task LoadImageAtIndex(int index)
        {
            try
            {
                if (index < 0 || index >= _imageUrls.Count)
                {
                    return;
                }

                _currentImageIndex = index;
                lblImageCount.Text = $"Şəkil {_currentImageIndex + 1} / {_imageUrls.Count}";

                // Check if image is already processed
                if (_processedImagePaths.Count > index && !string.IsNullOrEmpty(_processedImagePaths[index]))
                {
                    DisplayImageAtIndex(index);
                    return;
                }

                // Show loading indicator
                picImage.Image = null;
                picLoading.Visible = true;
                lblLoading.Visible = true;

                // Download the image
                byte[] imageData = await _webScraperService.DownloadImage(_imageUrls[index]);

                if (imageData == null || imageData.Length == 0)
                {
                    // Failed to download image
                    picLoading.Visible = false;
                    lblLoading.Visible = false;
                    return;
                }

                // Process the image to remove watermarks
                string imagePath = _imageProcessingService.ProcessAndSaveImage(imageData, _propertyDetail.SourceWebsite);

                // Ensure we have enough space in the list
                while (_processedImagePaths.Count <= index)
                {
                    _processedImagePaths.Add(null);
                }

                // Save the processed image path
                _processedImagePaths[index] = imagePath;

                // Display the image
                DisplayImageAtIndex(index);

                // Hide loading indicator
                picLoading.Visible = false;
                lblLoading.Visible = false;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error loading image: {ex.Message}");

                // Hide loading indicator
                picLoading.Visible = false;
                lblLoading.Visible = false;
            }
        }

        private void DisplayImageAtIndex(int index)
        {
            try
            {
                if (index < 0 || index >= _processedImagePaths.Count || string.IsNullOrEmpty(_processedImagePaths[index]))
                {
                    return;
                }

                _currentImageIndex = index;
                lblImageCount.Text = $"Şəkil {_currentImageIndex + 1} / {_imageUrls.Count}";

                // Dispose of current image
                if (picImage.Image != null)
                {
                    picImage.Image.Dispose();
                    picImage.Image = null;
                }

                // Load the image
                if (File.Exists(_processedImagePaths[index]))
                {
                    using (FileStream stream = new FileStream(_processedImagePaths[index], FileMode.Open, FileAccess.Read))
                    {
                        picImage.Image = Image.FromStream(stream);
                    }
                }

                // Update navigation buttons
                UpdateImageNavigationButtons();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error displaying image: {ex.Message}");
            }
        }

        private void UpdateImageNavigationButtons()
        {
            btnPrevImage.Enabled = _currentImageIndex > 0;
            btnNextImage.Enabled = _currentImageIndex < _imageUrls.Count - 1;
            btnDownloadImage.Enabled = _imageUrls.Count > 0 && _processedImagePaths.Count > _currentImageIndex && !string.IsNullOrEmpty(_processedImagePaths[_currentImageIndex]);
        }

        private async void btnNextImage_Click(object sender, EventArgs e)
        {
            if (_currentImageIndex < _imageUrls.Count - 1)
            {
                await LoadImageAtIndex(_currentImageIndex + 1);
            }
        }

        private async void btnPrevImage_Click(object sender, EventArgs e)
        {
            if (_currentImageIndex > 0)
            {
                await LoadImageAtIndex(_currentImageIndex - 1);
            }
        }

        private void btnDownloadImage_Click(object sender, EventArgs e)
        {
            try
            {
                if (_currentImageIndex < 0 || _currentImageIndex >= _processedImagePaths.Count || string.IsNullOrEmpty(_processedImagePaths[_currentImageIndex]))
                {
                    return;
                }

                using (SaveFileDialog dialog = new SaveFileDialog())
                {
                    dialog.Filter = "JPEG Image|*.jpg|All Files|*.*";
                    dialog.Title = "Şəkili saxla";
                    dialog.FileName = $"property_image_{DateTime.Now.Ticks}.jpg";

                    if (dialog.ShowDialog() == DialogResult.OK)
                    {
                        File.Copy(_processedImagePaths[_currentImageIndex], dialog.FileName, true);
                        MessageBox.Show("Şəkil uğurla saxlanıldı!", "Məlumat", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Şəkili saxlayarkən xəta baş verdi: {ex.Message}", "Xəta", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnBrowser_Click(object sender, EventArgs e)
        {
            try
            {
                if (!string.IsNullOrEmpty(_propertyDetail.SourceUrl))
                {
                    System.Diagnostics.Process.Start(_propertyDetail.SourceUrl);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Brauzeri açarkən xəta baş verdi: {ex.Message}", "Xəta", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            try
            {
                // First check if this listing is already saved
                bool alreadySaved = false;

                using (var connection = _dbManager.GetConnection())
                {
                    using (var command = new SQLiteCommand(
                        "SELECT COUNT(*) FROM SavedListings WHERE SourceUrl = @SourceUrl", connection))
                    {
                        command.Parameters.AddWithValue("@SourceUrl", _propertyDetail.SourceUrl);
                        int count = Convert.ToInt32(command.ExecuteScalar());
                        alreadySaved = count > 0;
                    }
                }

                if (alreadySaved)
                {
                    MessageBox.Show("Bu elan artıq saxlanılıb!", "Məlumat", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;
                }

                // Save the listing
                int savedListingId = -1;

                using (var connection = _dbManager.GetConnection())
                {
                    // Start a transaction
                    using (var transaction = connection.BeginTransaction())
                    {
                        try
                        {
                            // Insert listing
                            using (var command = new SQLiteCommand(
                                @"INSERT INTO SavedListings 
                                (Title, Description, Address, Price, PropertyType, Purpose, Rooms, Area, Floor, TotalFloors, BuildingType, 
                                SourceWebsite, SourceUrl, ContactInfo, SavedDate)
                                VALUES 
                                (@Title, @Description, @Address, @Price, @PropertyType, @Purpose, @Rooms, @Area, @Floor, @TotalFloors, @BuildingType, 
                                @SourceWebsite, @SourceUrl, @ContactInfo, @SavedDate);
                                SELECT last_insert_rowid();", connection, transaction))
                            {
                                command.Parameters.AddWithValue("@Title", _propertyDetail.Title);
                                command.Parameters.AddWithValue("@Description", _propertyDetail.Description ?? (object)DBNull.Value);
                                command.Parameters.AddWithValue("@Address", _propertyDetail.Address ?? "");
                                command.Parameters.AddWithValue("@Price", _propertyDetail.Price);
                                command.Parameters.AddWithValue("@PropertyType", (int)_propertyDetail.PropertyType);
                                command.Parameters.AddWithValue("@Purpose", (int)_propertyDetail.Purpose);
                                command.Parameters.AddWithValue("@Rooms", _propertyDetail.Rooms.HasValue ? (object)_propertyDetail.Rooms.Value : DBNull.Value);
                                command.Parameters.AddWithValue("@Area", _propertyDetail.Area.HasValue ? (object)_propertyDetail.Area.Value : DBNull.Value);
                                command.Parameters.AddWithValue("@Floor", _propertyDetail.Floor.HasValue ? (object)_propertyDetail.Floor.Value : DBNull.Value);
                                command.Parameters.AddWithValue("@TotalFloors", _propertyDetail.TotalFloors.HasValue ? (object)_propertyDetail.TotalFloors.Value : DBNull.Value);
                                command.Parameters.AddWithValue("@BuildingType", _propertyDetail.BuildingType.HasValue ? (object)(int)_propertyDetail.BuildingType.Value : DBNull.Value);
                                command.Parameters.AddWithValue("@SourceWebsite", _propertyDetail.SourceWebsite);
                                command.Parameters.AddWithValue("@SourceUrl", _propertyDetail.SourceUrl);

                                string contactInfo = "";
                                if (!string.IsNullOrEmpty(_propertyDetail.ContactName))
                                {
                                    contactInfo = _propertyDetail.ContactName;
                                }
                                if (!string.IsNullOrEmpty(_propertyDetail.ContactPhone))
                                {
                                    if (!string.IsNullOrEmpty(contactInfo))
                                    {
                                        contactInfo += ", ";
                                    }
                                    contactInfo += _propertyDetail.ContactPhone;
                                }

                                command.Parameters.AddWithValue("@ContactInfo", contactInfo);
                                command.Parameters.AddWithValue("@SavedDate", DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"));

                                savedListingId = Convert.ToInt32(command.ExecuteScalar());
                            }

                            // Insert images
                            for (int i = 0; i < _processedImagePaths.Count; i++)
                            {
                                if (string.IsNullOrEmpty(_processedImagePaths[i]))
                                {
                                    continue;
                                }

                                using (var command = new SQLiteCommand(
                                    @"INSERT INTO SavedListingImages 
                                    (SavedListingId, ImagePath, IsMainImage, SourceUrl)
                                    VALUES 
                                    (@SavedListingId, @ImagePath, @IsMainImage, @SourceUrl)", connection, transaction))
                                {
                                    command.Parameters.AddWithValue("@SavedListingId", savedListingId);
                                    command.Parameters.AddWithValue("@ImagePath", _processedImagePaths[i]);
                                    command.Parameters.AddWithValue("@IsMainImage", i == 0 ? 1 : 0);
                                    command.Parameters.AddWithValue("@SourceUrl", i < _imageUrls.Count ? _imageUrls[i] : (object)DBNull.Value);

                                    command.ExecuteNonQuery();
                                }
                            }

                            // Commit the transaction
                            transaction.Commit();
                        }
                        catch (Exception ex)
                        {
                            // Rollback the transaction
                            transaction.Rollback();
                            throw;
                        }
                    }
                }

                MessageBox.Show("Elan uğurla saxlanıldı!", "Məlumat", MessageBoxButtons.OK, MessageBoxIcon.Information);

                // Disable save button
                btnSave.Enabled = false;
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Elanı saxlayarkən xəta baş verdi: {ex.Message}", "Xəta", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}
