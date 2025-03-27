namespace DashinmazEmlakApp.Database.Models
{
    public class PropertyImage
    {
        public int Id { get; set; }
        public int PropertyId { get; set; }
        public string ImagePath { get; set; }
        public bool IsMainImage { get; set; }
        public string SourceUrl { get; set; }
    }
}
