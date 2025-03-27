using System;

namespace DashinmazEmlakApp.Database.Models
{
    /// <summary>
    /// Əməliyyat növləri
    /// </summary>
    public enum TransactionType
    {
        Sale = 0,
        Rent = 1
    }

    /// <summary>
    /// Müştəri növləri
    /// </summary>
    public enum ClientTyp
    {
        Owner = 0,
        Buyer = 1,
        Both = 2
    }

    /// <summary>
    /// Əmlak tipləri
    /// </summary>
    public enum PropertyType
    {
        Apartment = 0,
        House = 1,
        Villa = 2,
        Land = 3,
        Commercial = 4,
        Office = 5,
        Other = 6
    }

    /// <summary>
    /// Təyinat növləri
    /// </summary>
    public enum Purpose
    {
        ForSale = 0,
        ForRent = 1,
        Both = 2
    }

    /// <summary>
    /// Bina tipləri
    /// </summary>
    public enum BuildingType
    {
        NewBuilding = 0,
        OldBuilding = 1,
        PrivateHouse = 2,
        Villa = 3,
        Other = 4
    }
}