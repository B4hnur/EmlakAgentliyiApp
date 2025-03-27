using System;

namespace DasinmazEmlakAgentligi.Models
{
    public enum ClientType
    {
        Buyer = 0,        // Alıcı
        Seller = 1,       // Satıcı
        Renter = 2,       // Kirayəçi
        Landlord = 3      // Ev sahibi
    }

    public class Client
    {
        public int Id { get; set; }
        public string FullName { get; set; }
        public string Phone { get; set; }
        public string Email { get; set; }
        public string Address { get; set; }
        public string Details { get; set; }
        public ClientType ClientType { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime ModifiedDate { get; set; }

        public Client()
        {
            CreatedDate = DateTime.Now;
            ModifiedDate = DateTime.Now;
        }

        public override string ToString()
        {
            return $"{FullName} ({GetClientTypeString()})";
        }

        public string GetClientTypeString()
        {
            switch (ClientType)
            {
                case ClientType.Buyer:
                    return "Alıcı";
                case ClientType.Seller:
                    return "Satıcı";
                case ClientType.Renter:
                    return "Kirayəçi";
                case ClientType.Landlord:
                    return "Ev sahibi";
                default:
                    return "Bilinmir";
            }
        }
    }
}
