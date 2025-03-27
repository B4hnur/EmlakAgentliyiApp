using System;

namespace EmlakAgentliyiApp.Models
{
    public class PropertyImage
    {
        public int Id { get; set; }
        public int PropertyId { get; set; }
        public string ImagePath { get; set; }
        public string OriginalUrl { get; set; }
        public bool IsMainImage { get; set; }
        public DateTime UploadDate { get; set; }
        public bool IsWatermarkRemoved { get; set; }

        // İlkin təyinatlara görə yeni şəkil yaratma
        public PropertyImage()
        {
            ImagePath = string.Empty;
            OriginalUrl = string.Empty;
            UploadDate = DateTime.Now;
            IsMainImage = false;
            IsWatermarkRemoved = false;
        }
    }
}
