using System;
using System.Collections.Generic;

namespace DasinmazEmlakAgentligi.Models
{
    public enum PropertyType
    {
        Apartment = 0,        // Bina ev mənzil
        House = 1,            // Həyət evi / Villa, Bağ evi
        Garage = 2,           // Qaraj
        Office = 3,           // Ofis
        Land = 4,             // Torpaq sahəsi
        Commercial = 5        // Obyekt
    }

    public enum PropertyPurpose
    {
        Sale = 0,             // Satılır
        Rent = 1,             // Kirayə
        DailyRent = 2         // Günlük kirayə
    }

    public enum PropertyStatus
    {
        Active = 0,           // Aktiv
        Sold = 1,             // Satılıb
        Rented = 2,           // Kirayələnilib
        Inactive = 3,         // Deaktiv
        Reserved = 4          // Rezerv olunub
    }

    public class Property
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public decimal Price { get; set; }
        public string Currency { get; set; }
        public PropertyType PropertyType { get; set; }
        public PropertyPurpose Purpose { get; set; }
        public string Address { get; set; }
        public string City { get; set; }
        public string District { get; set; }
        public decimal Area { get; set; }
        public int Rooms { get; set; }
        public int Floor { get; set; }
        public int TotalFloors { get; set; }
        public string OwnerName { get; set; }
        public string OwnerPhone { get; set; }
        public string OwnerEmail { get; set; }
        public bool IsAgencyProperty { get; set; }
        public PropertyStatus Status { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime ModifiedDate { get; set; }
        public string OriginalListingUrl { get; set; }
        public string OriginalListingSite { get; set; }

        // DB-də saxlanmayan, lakin bəzi əməliyyatlar zamanı istifadə olunan xüsusiyyətlər
        public List<string> Images { get; set; }

        public Property()
        {
            CreatedDate = DateTime.Now;
            ModifiedDate = DateTime.Now;
            Status = PropertyStatus.Active;
            Currency = "AZN";
            Images = new List<string>();
        }

        public override string ToString()
        {
            return $"{Title} - {Price} {Currency}";
        }

        public string GetPropertyTypeString()
        {
            switch (PropertyType)
            {
                case PropertyType.Apartment:
                    return "Bina ev mənzil";
                case PropertyType.House:
                    return "Həyət evi / Villa, Bağ evi";
                case PropertyType.Garage:
                    return "Qaraj";
                case PropertyType.Office:
                    return "Ofis";
                case PropertyType.Land:
                    return "Torpaq sahəsi";
                case PropertyType.Commercial:
                    return "Obyekt";
                default:
                    return "Bilinmir";
            }
        }

        public string GetPropertyPurposeString()
        {
            switch (Purpose)
            {
                case PropertyPurpose.Sale:
                    return "Satılır";
                case PropertyPurpose.Rent:
                    return "Kirayə";
                case PropertyPurpose.DailyRent:
                    return "Günlük kirayə";
                default:
                    return "Bilinmir";
            }
        }

        public string GetPropertyStatusString()
        {
            switch (Status)
            {
                case PropertyStatus.Active:
                    return "Aktiv";
                case PropertyStatus.Sold:
                    return "Satılıb";
                case PropertyStatus.Rented:
                    return "Kirayələnilib";
                case PropertyStatus.Inactive:
                    return "Deaktiv";
                case PropertyStatus.Reserved:
                    return "Rezerv olunub";
                default:
                    return "Bilinmir";
            }
        }
    }
}
