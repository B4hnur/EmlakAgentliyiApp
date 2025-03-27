using System;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Drawing.Drawing2D;

namespace EmlakAgentliyiApp.Utils
{
    public class WatermarkRemover
    {
        // Şəkildəki filigranı təyin etmək (renglər əsasında)
        public static Bitmap RemoveWatermark(Bitmap originalImage)
        {
            // Yeni şəkil yaratmaq
            Bitmap processedImage = new Bitmap(originalImage.Width, originalImage.Height);

            // Filigran təyin etmək üçün yayğın sərhədləri
            int cornerSize = 100; // Filigranların adətən küncdə olduğunu təxmin edirik

            // Filigranları aşağı və sağ künclərdə axtarmaq
            bool hasWatermark = DetectWatermarkInRegion(originalImage,
                new Rectangle(originalImage.Width - cornerSize, originalImage.Height - cornerSize, cornerSize, cornerSize));

            // Şəkili emal etmək
            using (Graphics g = Graphics.FromImage(processedImage))
            {
                // Orijinal şəkili çəkmək
                g.DrawImage(originalImage, 0, 0, originalImage.Width, originalImage.Height);

                // Filigran aşkar edildiyi halda onu aradan qaldırmaq
                if (hasWatermark)
                {
                    // Öncə təyin edilməli olan regionlar (adətən künclər) 
                    Rectangle bottomRightCorner = new Rectangle(
                        originalImage.Width - cornerSize, originalImage.Height - cornerSize,
                        cornerSize, cornerSize);

                    // Bu regionu yaxın rənglərlə doldurmaq
                    InpaintRegion(originalImage, processedImage, bottomRightCorner);

                    // Aşağı sol künc üçün də yoxlamaq
                    Rectangle bottomLeftCorner = new Rectangle(
                        0, originalImage.Height - cornerSize,
                        cornerSize, cornerSize);

                    if (DetectWatermarkInRegion(originalImage, bottomLeftCorner))
                    {
                        InpaintRegion(originalImage, processedImage, bottomLeftCorner);
                    }

                    // Yuxarı sağ künc üçün də yoxlamaq
                    Rectangle topRightCorner = new Rectangle(
                        originalImage.Width - cornerSize, 0,
                        cornerSize, cornerSize);

                    if (DetectWatermarkInRegion(originalImage, topRightCorner))
                    {
                        InpaintRegion(originalImage, processedImage, topRightCorner);
                    }
                }
            }

            return processedImage;
        }

        // Regionda filigran olub-olmadığını təyin etmək
        private static bool DetectWatermarkInRegion(Bitmap image, Rectangle region)
        {
            // Bəzi saytların filigranları üçün xarakterik rənglər
            Color[] watermarkColors = new Color[]
            {
                Color.FromArgb(255, 255, 255), // Ağ
                Color.FromArgb(0, 0, 0),       // Qara
                Color.FromArgb(255, 0, 0),     // Qırmızı
                Color.FromArgb(0, 0, 255)      // Mavi
            };

            int watermarkPixelCount = 0;
            int totalPixels = region.Width * region.Height;

            // Region daxilində nümunəvi piksellər götürüb yoxlamaq
            for (int x = region.X; x < region.X + region.Width; x += 2)
            {
                for (int y = region.Y; y < region.Y + region.Height; y += 2)
                {
                    if (x < image.Width && y < image.Height)
                    {
                        Color pixelColor = image.GetPixel(x, y);

                        // Filigran rənglərinə oxşarlığı yoxlamaq
                        foreach (Color watermarkColor in watermarkColors)
                        {
                            if (IsColorSimilar(pixelColor, watermarkColor, 50))
                            {
                                watermarkPixelCount++;
                                break;
                            }
                        }
                    }
                }
            }

            // Regionun müəyyən faizində filigran rəngi varsa, filigran var demək olar
            double watermarkPercentage = (double)watermarkPixelCount / (totalPixels / 4) * 100;
            return watermarkPercentage > 15; // 15% və daha çox uyğunluq
        }

        // İki rəngin oxşarlığını müəyyən etmək
        private static bool IsColorSimilar(Color c1, Color c2, int threshold)
        {
            int rDiff = Math.Abs(c1.R - c2.R);
            int gDiff = Math.Abs(c1.G - c2.G);
            int bDiff = Math.Abs(c1.B - c2.B);

            // Rəng fərqinin ümumi dəyəri təyin edilmiş həddi keçmirsə, rənglər oxşardır
            return (rDiff + gDiff + bDiff) <= threshold;
        }

        // Filigran regionunda inpainting (bərpaetmə) tətbiq etmək
        private static void InpaintRegion(Bitmap sourceImage, Bitmap targetImage, Rectangle region)
        {
            // Regionun kənarından ortalama rəng almaq
            Color averageColor = GetAverageColorAroundRegion(sourceImage, region);

            // Qradiyent doldurma üçün rəng
            using (Graphics g = Graphics.FromImage(targetImage))
            {
                using (LinearGradientBrush brush = new LinearGradientBrush(
                    region,
                    Color.FromArgb(150, averageColor), // Yarı şəffaf rəng
                    Color.FromArgb(200, averageColor), // Daha az şəffaf rəng
                    LinearGradientMode.ForwardDiagonal))
                {
                    g.FillRectangle(brush, region);
                }
            }
        }

        // Regionun ətrafındakı rənglərin ortalamasını hesablamaq
        private static Color GetAverageColorAroundRegion(Bitmap image, Rectangle region)
        {
            int totalR = 0, totalG = 0, totalB = 0;
            int sampleCount = 0;

            // Regionun kənarında nümunələr götürmək
            int sampleDistance = 5;

            // Yuxarı kənar
            for (int x = region.X; x < region.X + region.Width; x += sampleDistance)
            {
                if (x >= 0 && x < image.Width && region.Y - 1 >= 0)
                {
                    Color c = image.GetPixel(x, region.Y - 1);
                    totalR += c.R;
                    totalG += c.G;
                    totalB += c.B;
                    sampleCount++;
                }
            }

            // Aşağı kənar
            for (int x = region.X; x < region.X + region.Width; x += sampleDistance)
            {
                if (x >= 0 && x < image.Width && region.Y + region.Height < image.Height)
                {
                    Color c = image.GetPixel(x, region.Y + region.Height);
                    totalR += c.R;
                    totalG += c.G;
                    totalB += c.B;
                    sampleCount++;
                }
            }

            // Sol kənar
            for (int y = region.Y; y < region.Y + region.Height; y += sampleDistance)
            {
                if (region.X - 1 >= 0 && y >= 0 && y < image.Height)
                {
                    Color c = image.GetPixel(region.X - 1, y);
                    totalR += c.R;
                    totalG += c.G;
                    totalB += c.B;
                    sampleCount++;
                }
            }

            // Sağ kənar
            for (int y = region.Y; y < region.Y + region.Height; y += sampleDistance)
            {
                if (region.X + region.Width < image.Width && y >= 0 && y < image.Height)
                {
                    Color c = image.GetPixel(region.X + region.Width, y);
                    totalR += c.R;
                    totalG += c.G;
                    totalB += c.B;
                    sampleCount++;
                }
            }

            // Ortalama rəngi qaytarmaq
            if (sampleCount > 0)
            {
                return Color.FromArgb(
                    totalR / sampleCount,
                    totalG / sampleCount,
                    totalB / sampleCount);
            }

            // Əgər nümunə alına bilməsə, ağ rəng qaytarmaq
            return Color.White;
        }
    }
}
