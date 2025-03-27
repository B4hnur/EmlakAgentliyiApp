using System;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Windows.Forms;

namespace DashinmazEmlakApp.Services
{
    public class ImageProcessingService
    {
        // Directory to save processed images
        private readonly string _imageDirectory;

        public ImageProcessingService()
        {
            // Create directory for images in AppData
            _imageDirectory = Path.Combine(
                Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData),
                "DashinmazEmlakApp",
                "Images");

            if (!Directory.Exists(_imageDirectory))
            {
                Directory.CreateDirectory(_imageDirectory);
            }
        }

        /// <summary>
        /// Processes an image to remove watermarks/filigrees
        /// </summary>
        /// <param name="imageData">Raw image data</param>
        /// <param name="sourceWebsite">Source website for specialized watermark removal</param>
        /// <returns>Path to the saved processed image</returns>
        public string ProcessAndSaveImage(byte[] imageData, string sourceWebsite)
        {
            try
            {
                if (imageData == null || imageData.Length == 0)
                {
                    return null;
                }

                using (MemoryStream ms = new MemoryStream(imageData))
                {
                    using (Bitmap originalImage = new Bitmap(ms))
                    {
                        // Create a copy of the original image to work with
                        Bitmap processedImage = new Bitmap(originalImage);

                        // Remove watermarks based on source website
                        RemoveWatermarks(processedImage, sourceWebsite);

                        // Create a unique filename based on timestamp
                        string fileName = $"property_{DateTime.Now.Ticks}.jpg";
                        string filePath = Path.Combine(_imageDirectory, fileName);

                        // Save the processed image
                        processedImage.Save(filePath, ImageFormat.Jpeg);

                        // Dispose of the processed image
                        processedImage.Dispose();

                        return filePath;
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Şəkili emal edərkən xəta baş verdi: {ex.Message}", "Xəta", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return null;
            }
        }

        /// <summary>
        /// Removes watermarks from the image
        /// </summary>
        /// <param name="image">Image to process</param>
        /// <param name="sourceWebsite">Source website for specialized watermark removal</param>
        private void RemoveWatermarks(Bitmap image, string sourceWebsite)
        {
            try
            {
                // Different watermark removal strategies based on source website
                if (sourceWebsite.Contains("kub.az"))
                {
                    RemoveKubAzWatermark(image);
                }
                else
                {
                    // Generic watermark removal for other sites
                    RemoveGenericWatermark(image);
                }

                // Apply some general image enhancement
                EnhanceImage(image);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error removing watermarks: {ex.Message}");
            }
        }

        /// <summary>
        /// Removes watermarks specific to kub.az
        /// </summary>
        /// <param name="image">Image to process</param>
        private void RemoveKubAzWatermark(Bitmap image)
        {
            // Common locations for kub.az watermarks (usually bottom-right corner)
            int watermarkWidth = image.Width / 4;
            int watermarkHeight = image.Height / 6;

            // Process bottom-right corner where watermark is typically found
            for (int x = image.Width - watermarkWidth; x < image.Width; x++)
            {
                for (int y = image.Height - watermarkHeight; y < image.Height; y++)
                {
                    // Check if pixel might be part of a watermark (semi-transparent white/gray text)
                    Color pixel = image.GetPixel(x, y);

                    // If it's a light, semi-transparent pixel (likely part of watermark)
                    if (IsLikelyWatermarkPixel(pixel))
                    {
                        // Sample surrounding area to get the background color
                        Color replacementColor = SampleSurroundingArea(image, x, y, watermarkWidth, watermarkHeight);

                        // Replace the watermark pixel with the sampled background color
                        image.SetPixel(x, y, replacementColor);
                    }
                }
            }
        }

        /// <summary>
        /// Generic watermark removal strategy for any site
        /// </summary>
        /// <param name="image">Image to process</param>
        private void RemoveGenericWatermark(Bitmap image)
        {
            // Attempt to detect and remove watermarks in common locations

            // Check corners for watermarks
            // Top-left
            ProcessCornerArea(image, 0, 0, image.Width / 4, image.Height / 4);

            // Top-right
            ProcessCornerArea(image, image.Width - image.Width / 4, 0, image.Width, image.Height / 4);

            // Bottom-left
            ProcessCornerArea(image, 0, image.Height - image.Height / 4, image.Width / 4, image.Height);

            // Bottom-right
            ProcessCornerArea(image, image.Width - image.Width / 4, image.Height - image.Height / 4, image.Width, image.Height);

            // Check center for watermarks
            ProcessCenterArea(image);
        }

        /// <summary>
        /// Process a corner area to detect and remove watermarks
        /// </summary>
        private void ProcessCornerArea(Bitmap image, int startX, int startY, int endX, int endY)
        {
            for (int x = startX; x < endX; x++)
            {
                for (int y = startY; y < endY; y++)
                {
                    Color pixel = image.GetPixel(x, y);

                    if (IsLikelyWatermarkPixel(pixel))
                    {
                        Color replacementColor = SampleSurroundingArea(image, x, y, endX - startX, endY - startY);
                        image.SetPixel(x, y, replacementColor);
                    }
                }
            }
        }

        /// <summary>
        /// Process the center area to detect and remove watermarks
        /// </summary>
        private void ProcessCenterArea(Bitmap image)
        {
            int centerX = image.Width / 2;
            int centerY = image.Height / 2;
            int radius = Math.Min(image.Width, image.Height) / 6;

            for (int x = centerX - radius; x < centerX + radius; x++)
            {
                for (int y = centerY - radius; y < centerY + radius; y++)
                {
                    if (x >= 0 && x < image.Width && y >= 0 && y < image.Height)
                    {
                        Color pixel = image.GetPixel(x, y);

                        if (IsLikelyWatermarkPixel(pixel))
                        {
                            Color replacementColor = SampleSurroundingArea(image, x, y, radius * 2, radius * 2);
                            image.SetPixel(x, y, replacementColor);
                        }
                    }
                }
            }
        }

        /// <summary>
        /// Determines if a pixel is likely part of a watermark
        /// </summary>
        private bool IsLikelyWatermarkPixel(Color pixel)
        {
            // Watermarks are typically semi-transparent, so they have alpha channel < 255
            bool isTransparent = pixel.A < 255 && pixel.A > 0;

            // Watermarks are typically light colored (white or light gray)
            bool isLight = (pixel.R > 200 && pixel.G > 200 && pixel.B > 200) ||
                           (Math.Abs(pixel.R - pixel.G) < 10 && Math.Abs(pixel.R - pixel.B) < 10 && pixel.R > 180);

            // Or they could be dark (black or dark gray)
            bool isDark = (pixel.R < 50 && pixel.G < 50 && pixel.B < 50) ||
                          (Math.Abs(pixel.R - pixel.G) < 10 && Math.Abs(pixel.R - pixel.B) < 10 && pixel.R < 50);

            return isTransparent || isLight || isDark;
        }

        /// <summary>
        /// Sample the surrounding area to get a replacement color for watermark pixels
        /// </summary>
        private Color SampleSurroundingArea(Bitmap image, int x, int y, int width, int height)
        {
            // Try to sample from an area outside the watermark region
            int sampleX = x - width / 2;
            int sampleY = y - height / 2;

            // Make sure the sample point is within the image bounds
            sampleX = Math.Max(0, Math.Min(sampleX, image.Width - 1));
            sampleY = Math.Max(0, Math.Min(sampleY, image.Height - 1));

            // Return the sampled color
            return image.GetPixel(sampleX, sampleY);
        }

        /// <summary>
        /// Enhances the image quality after watermark removal
        /// </summary>
        private void EnhanceImage(Bitmap image)
        {
            // Simple enhancement by slightly increasing contrast
            for (int x = 0; x < image.Width; x++)
            {
                for (int y = 0; y < image.Height; y++)
                {
                    Color pixel = image.GetPixel(x, y);

                    // Increase contrast slightly
                    int r = Math.Min(255, Math.Max(0, (int)(pixel.R * 1.05)));
                    int g = Math.Min(255, Math.Max(0, (int)(pixel.G * 1.05)));
                    int b = Math.Min(255, Math.Max(0, (int)(pixel.B * 1.05)));

                    image.SetPixel(x, y, Color.FromArgb(pixel.A, r, g, b));
                }
            }
        }
    }
}
