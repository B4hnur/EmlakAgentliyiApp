using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SQLite;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using DashinmazEmlakApp.Utilities;
using EmlakAgentliyiApp.Models;
using EmlakAgentliyiApp.Utils;

namespace EmlakAgentliyiApp.Services
{
    public class DatabaseService
    {
        private readonly string _connectionString;

        public DatabaseService()
        {
            string dbPath = Constants.DatabasePath;
            _connectionString = $"Data Source={dbPath};Version=3;";
        }

        // Verilənlər bazasını ilkin qurmaq və cədvəlləri yaratmaq
        public static void InitializeDatabase()
        {
            try
            {
                string dbPath = Constants.DatabasePath;
                bool firstRun = !File.Exists(dbPath);

                if (firstRun)
                {
                    SQLiteConnection.CreateFile(dbPath);
                }

                using (var connection = new SQLiteConnection($"Data Source={dbPath};Version=3;"))
                {
                    connection.Open();

                    // Cədvəlləri yaratmaq
                    using (var command = new SQLiteCommand(connection))
                    {
                        if (firstRun)
                        {
                            // Employees cədvəli
                            command.CommandText = @"CREATE TABLE IF NOT EXISTS Employees (
                                Id INTEGER PRIMARY KEY AUTOINCREMENT,
                                FullName TEXT NOT NULL,
                                Username TEXT NOT NULL UNIQUE,
                                PasswordHash TEXT NOT NULL,
                                Position TEXT,
                                Phone TEXT,
                                Email TEXT,
                                Salary REAL,
                                CommissionRate REAL,
                                HireDate TEXT,
                                IsActive INTEGER,
                                IsAdmin INTEGER,
                                LastLogin TEXT
                            )";
                            command.ExecuteNonQuery();

                            // Properties cədvəli
                            command.CommandText = @"CREATE TABLE IF NOT EXISTS Properties (
                                Id INTEGER PRIMARY KEY AUTOINCREMENT,
                                Title TEXT NOT NULL,
                                Description TEXT,
                                Price REAL,
                                Location TEXT,
                                Area REAL,
                                Rooms INTEGER,
                                Floor INTEGER,
                                BuildingFloors INTEGER,
                                PropertyType INTEGER,
                                BuildingType INTEGER,
                                PurposeType INTEGER,
                                OwnerType INTEGER,
                                Source TEXT,
                                SourceUrl TEXT,
                                ListingDate TEXT,
                                IsSaved INTEGER,
                                IsSold INTEGER,
                                CommissionAmount REAL,
                                AssignedEmployeeId INTEGER,
                                LastUpdated TEXT
                            )";
                            command.ExecuteNonQuery();

                            // Clients cədvəli
                            command.CommandText = @"CREATE TABLE IF NOT EXISTS Clients (
                                Id INTEGER PRIMARY KEY AUTOINCREMENT,
                                FullName TEXT NOT NULL,
                                Phone TEXT,
                                Email TEXT,
                                Address TEXT,
                                Notes TEXT,
                                ClientType INTEGER,
                                RegistrationDate TEXT,
                                AssignedEmployeeId INTEGER,
                                IsActive INTEGER,
                                LastContactDate TEXT
                            )";
                            command.ExecuteNonQuery();

                            // ClientProperties cədvəli (əlaqə cədvəli)
                            command.CommandText = @"CREATE TABLE IF NOT EXISTS ClientProperties (
                                Id INTEGER PRIMARY KEY AUTOINCREMENT,
                                ClientId INTEGER,
                                PropertyId INTEGER,
                                RelationType INTEGER,
                                CreatedDate TEXT,
                                FOREIGN KEY(ClientId) REFERENCES Clients(Id),
                                FOREIGN KEY(PropertyId) REFERENCES Properties(Id)
                            )";
                            command.ExecuteNonQuery();

                            // Transactions cədvəli
                            command.CommandText = @"CREATE TABLE IF NOT EXISTS Transactions (
                                Id INTEGER PRIMARY KEY AUTOINCREMENT,
                                PropertyId INTEGER,
                                ClientId INTEGER,
                                EmployeeId INTEGER,
                                Amount REAL,
                                CommissionAmount REAL,
                                TransactionType TEXT,
                                TransactionDate TEXT,
                                Description TEXT,
                                IsSuccessful INTEGER,
                                PaymentMethod TEXT,
                                ReferenceNumber TEXT,
                                FOREIGN KEY(PropertyId) REFERENCES Properties(Id),
                                FOREIGN KEY(ClientId) REFERENCES Clients(Id),
                                FOREIGN KEY(EmployeeId) REFERENCES Employees(Id)
                            )";
                            command.ExecuteNonQuery();

                            // Expenses cədvəli
                            command.CommandText = @"CREATE TABLE IF NOT EXISTS Expenses (
                                Id INTEGER PRIMARY KEY AUTOINCREMENT,
                                Title TEXT NOT NULL,
                                Category TEXT,
                                Amount REAL,
                                ExpenseDate TEXT,
                                Description TEXT,
                                EmployeeId INTEGER,
                                IsPaid INTEGER,
                                PaymentDate TEXT,
                                PaymentMethod TEXT,
                                ReferenceNumber TEXT,
                                ReceiptImagePath TEXT,
                                FOREIGN KEY(EmployeeId) REFERENCES Employees(Id)
                            )";
                            command.ExecuteNonQuery();

                            // PropertyImages cədvəli
                            command.CommandText = @"CREATE TABLE IF NOT EXISTS PropertyImages (
                                Id INTEGER PRIMARY KEY AUTOINCREMENT,
                                PropertyId INTEGER,
                                ImagePath TEXT,
                                OriginalUrl TEXT,
                                IsMainImage INTEGER,
                                UploadDate TEXT,
                                IsWatermarkRemoved INTEGER,
                                FOREIGN KEY(PropertyId) REFERENCES Properties(Id)
                            )";
                            command.ExecuteNonQuery();

                            // Demo admin istifadəçisi yaratmaq
                            command.CommandText = @"INSERT INTO Employees (
                                FullName, Username, PasswordHash, Position, IsActive, IsAdmin, HireDate, LastLogin
                            ) VALUES (
                                'Administrator', 'admin', 'admin123', 'Administrator', 1, 1, datetime('now'), datetime('now')
                            )";
                            command.ExecuteNonQuery();
                        }
                    }

                    connection.Close();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Verilənlər bazasını qurmaq mümkün olmadı: {ex.Message}", "Xəta",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        // İstifadəçinin doğruluğunu yoxlamaq
        public bool ValidateUser(string username, string password)
        {
            try
            {
                using (var connection = new SQLiteConnection(_connectionString))
                {
                    connection.Open();

                    using (var command = new SQLiteCommand(connection))
                    {
                        command.CommandText = "SELECT Id FROM Employees WHERE Username = @Username AND PasswordHash = @PasswordHash AND IsActive = 1";
                        command.Parameters.AddWithValue("@Username", username);
                        command.Parameters.AddWithValue("@PasswordHash", password);

                        using (var reader = command.ExecuteReader())
                        {
                            if (reader.Read())
                            {
                                // Giriş vaxtını yeniləmək
                                int userId = Convert.ToInt32(reader["Id"]);
                                reader.Close();

                                command.CommandText = "UPDATE Employees SET LastLogin = datetime('now') WHERE Id = @Id";
                                command.Parameters.Clear();
                                command.Parameters.AddWithValue("@Id", userId);
                                command.ExecuteNonQuery();

                                return true;
                            }
                        }
                    }

                    connection.Close();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"İstifadəçi doğrulaması zamanı xəta: {ex.Message}", "Xəta",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

            return false;
        }

        // Əmlak əlavə etmək və ya yeniləmək
        public int SaveProperty(Property property)
        {
            try
            {
                using (var connection = new SQLiteConnection(_connectionString))
                {
                    connection.Open();

                    using (var command = new SQLiteCommand(connection))
                    {
                        // ID-yə görə mövcud olub-olmadığını yoxlamaq
                        if (property.Id > 0)
                        {
                            command.CommandText = "SELECT Id FROM Properties WHERE Id = @Id";
                            command.Parameters.AddWithValue("@Id", property.Id);

                            using (var reader = command.ExecuteReader())
                            {
                                if (reader.Read())
                                {
                                    // Mövcud əmlakı yeniləmək
                                    reader.Close();
                                    command.CommandText = @"UPDATE Properties SET
                                        Title = @Title,
                                        Description = @Description,
                                        Price = @Price,
                                        Location = @Location,
                                        Area = @Area,
                                        Rooms = @Rooms,
                                        Floor = @Floor,
                                        BuildingFloors = @BuildingFloors,
                                        PropertyType = @PropertyType,
                                        BuildingType = @BuildingType,
                                        PurposeType = @PurposeType,
                                        OwnerType = @OwnerType,
                                        Source = @Source,
                                        SourceUrl = @SourceUrl,
                                        IsSaved = @IsSaved,
                                        IsSold = @IsSold,
                                        CommissionAmount = @CommissionAmount,
                                        AssignedEmployeeId = @AssignedEmployeeId,
                                        LastUpdated = datetime('now')
                                        WHERE Id = @Id";
                                }
                                else
                                {
                                    // Yeni əmlak əlavə etmək
                                    reader.Close();
                                    command.CommandText = @"INSERT INTO Properties (
                                        Title, Description, Price, Location, Area, Rooms, Floor, BuildingFloors,
                                        PropertyType, BuildingType, PurposeType, OwnerType, Source, SourceUrl,
                                        ListingDate, IsSaved, IsSold, CommissionAmount, AssignedEmployeeId, LastUpdated
                                    ) VALUES (
                                        @Title, @Description, @Price, @Location, @Area, @Rooms, @Floor, @BuildingFloors,
                                        @PropertyType, @BuildingType, @PurposeType, @OwnerType, @Source, @SourceUrl,
                                        datetime('now'), @IsSaved, @IsSold, @CommissionAmount, @AssignedEmployeeId, datetime('now')
                                    )";
                                }
                            }
                        }
                        else
                        {
                            // Yeni əmlak əlavə etmək
                            command.CommandText = @"INSERT INTO Properties (
                                Title, Description, Price, Location, Area, Rooms, Floor, BuildingFloors,
                                PropertyType, BuildingType, PurposeType, OwnerType, Source, SourceUrl,
                                ListingDate, IsSaved, IsSold, CommissionAmount, AssignedEmployeeId, LastUpdated
                            ) VALUES (
                                @Title, @Description, @Price, @Location, @Area, @Rooms, @Floor, @BuildingFloors,
                                @PropertyType, @BuildingType, @PurposeType, @OwnerType, @Source, @SourceUrl,
                                datetime('now'), @IsSaved, @IsSold, @CommissionAmount, @AssignedEmployeeId, datetime('now')
                            )";
                        }

                        // Parametrləri təyin etmək
                        command.Parameters.Clear();

                        if (property.Id > 0)
                        {
                            command.Parameters.AddWithValue("@Id", property.Id);
                        }

                        command.Parameters.AddWithValue("@Title", property.Title);
                        command.Parameters.AddWithValue("@Description", property.Description);
                        command.Parameters.AddWithValue("@Price", property.Price);
                        command.Parameters.AddWithValue("@Location", property.Location);
                        command.Parameters.AddWithValue("@Area", property.Area);
                        command.Parameters.AddWithValue("@Rooms", property.Rooms);
                        command.Parameters.AddWithValue("@Floor", property.Floor);
                        command.Parameters.AddWithValue("@BuildingFloors", property.BuildingFloors);
                        command.Parameters.AddWithValue("@PropertyType", property.PropertyType);
                        command.Parameters.AddWithValue("@BuildingType", property.BuildingType);
                        command.Parameters.AddWithValue("@PurposeType", property.PurposeType);
                        command.Parameters.AddWithValue("@OwnerType", property.OwnerType);
                        command.Parameters.AddWithValue("@Source", property.Source);
                        command.Parameters.AddWithValue("@SourceUrl", property.SourceUrl);
                        command.Parameters.AddWithValue("@IsSaved", property.IsSaved ? 1 : 0);
                        command.Parameters.AddWithValue("@IsSold", property.IsSold ? 1 : 0);
                        command.Parameters.AddWithValue("@CommissionAmount", property.CommissionAmount);
                        command.Parameters.AddWithValue("@AssignedEmployeeId", property.AssignedEmployeeId);

                        command.ExecuteNonQuery();

                        if (property.Id <= 0)
                        {
                            // Yeni əlavə edilmiş əmlakın ID-sini almaq
                            command.CommandText = "SELECT last_insert_rowid()";
                            property.Id = Convert.ToInt32(command.ExecuteScalar());
                        }
                    }

                    connection.Close();
                }

                return property.Id;
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Əmlakı yadda saxlayarkən xəta: {ex.Message}", "Xəta",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
                return 0;
            }
        }

        // Əmlakı yeniləmək
        public bool UpdateProperty(Property property)
        {
            try
            {
                using (var connection = new SQLiteConnection(_connectionString))
                {
                    connection.Open();

                    using (var command = new SQLiteCommand(connection))
                    {
                        command.CommandText = @"UPDATE Properties SET
                            Title = @Title,
                            Description = @Description,
                            Price = @Price,
                            Location = @Location,
                            Area = @Area,
                            Rooms = @Rooms,
                            Floor = @Floor,
                            BuildingFloors = @BuildingFloors,
                            PropertyType = @PropertyType,
                            BuildingType = @BuildingType,
                            PurposeType = @PurposeType,
                            OwnerType = @OwnerType,
                            Source = @Source,
                            SourceUrl = @SourceUrl,
                            IsSaved = @IsSaved,
                            IsSold = @IsSold,
                            CommissionAmount = @CommissionAmount,
                            AssignedEmployeeId = @AssignedEmployeeId,
                            LastUpdated = datetime('now')
                            WHERE Id = @Id";

                        command.Parameters.AddWithValue("@Id", property.Id);
                        command.Parameters.AddWithValue("@Title", property.Title);
                        command.Parameters.AddWithValue("@Description", property.Description);
                        command.Parameters.AddWithValue("@Price", property.Price);
                        command.Parameters.AddWithValue("@Location", property.Location);
                        command.Parameters.AddWithValue("@Area", property.Area);
                        command.Parameters.AddWithValue("@Rooms", property.Rooms);
                        command.Parameters.AddWithValue("@Floor", property.Floor);
                        command.Parameters.AddWithValue("@BuildingFloors", property.BuildingFloors);
                        command.Parameters.AddWithValue("@PropertyType", property.PropertyType);
                        command.Parameters.AddWithValue("@BuildingType", property.BuildingType);
                        command.Parameters.AddWithValue("@PurposeType", property.PurposeType);
                        command.Parameters.AddWithValue("@OwnerType", property.OwnerType);
                        command.Parameters.AddWithValue("@Source", property.Source);
                        command.Parameters.AddWithValue("@SourceUrl", property.SourceUrl);
                        command.Parameters.AddWithValue("@IsSaved", property.IsSaved ? 1 : 0);
                        command.Parameters.AddWithValue("@IsSold", property.IsSold ? 1 : 0);
                        command.Parameters.AddWithValue("@CommissionAmount", property.CommissionAmount);
                        command.Parameters.AddWithValue("@AssignedEmployeeId", property.AssignedEmployeeId);

                        int rowsAffected = command.ExecuteNonQuery();

                        connection.Close();
                        return rowsAffected > 0;
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Əmlakı yeniləyərkən xəta: {ex.Message}", "Xəta",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
        }

        // Əmlakın şəkilini yadda saxlamaq
        public int SavePropertyImage(PropertyImage image)
        {
            try
            {
                using (var connection = new SQLiteConnection(_connectionString))
                {
                    connection.Open();

                    using (var command = new SQLiteCommand(connection))
                    {
                        command.CommandText = @"INSERT INTO PropertyImages (
                            PropertyId, ImagePath, OriginalUrl, IsMainImage, UploadDate, IsWatermarkRemoved
                        ) VALUES (
                            @PropertyId, @ImagePath, @OriginalUrl, @IsMainImage, datetime('now'), @IsWatermarkRemoved
                        )";

                        command.Parameters.AddWithValue("@PropertyId", image.PropertyId);
                        command.Parameters.AddWithValue("@ImagePath", image.ImagePath);
                        command.Parameters.AddWithValue("@OriginalUrl", image.OriginalUrl);
                        command.Parameters.AddWithValue("@IsMainImage", image.IsMainImage ? 1 : 0);
                        command.Parameters.AddWithValue("@IsWatermarkRemoved", image.IsWatermarkRemoved ? 1 : 0);

                        command.ExecuteNonQuery();

                        // Yeni əlavə edilmiş şəkilin ID-sini almaq
                        command.CommandText = "SELECT last_insert_rowid()";
                        image.Id = Convert.ToInt32(command.ExecuteScalar());
                    }

                    connection.Close();
                }

                return image.Id;
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Şəkili yadda saxlayarkən xəta: {ex.Message}", "Xəta",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
                return 0;
            }
        }

        // Bütün əmlakları almaq
        public List<Property> GetAllProperties()
        {
            var properties = new List<Property>();

            try
            {
                using (var connection = new SQLiteConnection(_connectionString))
                {
                    connection.Open();

                    using (var command = new SQLiteCommand("SELECT * FROM Properties ORDER BY ListingDate DESC", connection))
                    {
                        using (var reader = command.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                properties.Add(new Property
                                {
                                    Id = Convert.ToInt32(reader["Id"]),
                                    Title = reader["Title"].ToString(),
                                    Description = reader["Description"].ToString(),
                                    Price = Convert.ToDecimal(reader["Price"]),
                                    Location = reader["Location"].ToString(),
                                    Area = Convert.ToDouble(reader["Area"]),
                                    Rooms = Convert.ToInt32(reader["Rooms"]),
                                    Floor = Convert.ToInt32(reader["Floor"]),
                                    BuildingFloors = Convert.ToInt32(reader["BuildingFloors"]),
                                    PropertyType = Convert.ToInt32(reader["PropertyType"]),
                                    BuildingType = Convert.ToInt32(reader["BuildingType"]),
                                    PurposeType = Convert.ToInt32(reader["PurposeType"]),
                                    OwnerType = Convert.ToInt32(reader["OwnerType"]),
                                    Source = reader["Source"].ToString(),
                                    SourceUrl = reader["SourceUrl"].ToString(),
                                    ListingDate = Convert.ToDateTime(reader["ListingDate"]),
                                    IsSaved = Convert.ToBoolean(Convert.ToInt32(reader["IsSaved"])),
                                    IsSold = Convert.ToBoolean(Convert.ToInt32(reader["IsSold"])),
                                    CommissionAmount = Convert.ToDecimal(reader["CommissionAmount"]),
                                    AssignedEmployeeId = Convert.ToInt32(reader["AssignedEmployeeId"]),
                                    LastUpdated = Convert.ToDateTime(reader["LastUpdated"])
                                });
                            }
                        }
                    }

                    connection.Close();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Əmlak məlumatlarını əldə edərkən xəta: {ex.Message}", "Xəta",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

            return properties;
        }

        // Müəyyən bir əmlakın şəkillərini almaq
        public List<PropertyImage> GetPropertyImages(int propertyId)
        {
            var images = new List<PropertyImage>();

            try
            {
                using (var connection = new SQLiteConnection(_connectionString))
                {
                    connection.Open();

                    using (var command = new SQLiteCommand(connection))
                    {
                        command.CommandText = "SELECT * FROM PropertyImages WHERE PropertyId = @PropertyId ORDER BY IsMainImage DESC, UploadDate";
                        command.Parameters.AddWithValue("@PropertyId", propertyId);

                        using (var reader = command.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                images.Add(new PropertyImage
                                {
                                    Id = Convert.ToInt32(reader["Id"]),
                                    PropertyId = Convert.ToInt32(reader["PropertyId"]),
                                    ImagePath = reader["ImagePath"].ToString(),
                                    OriginalUrl = reader["OriginalUrl"].ToString(),
                                    IsMainImage = Convert.ToBoolean(Convert.ToInt32(reader["IsMainImage"])),
                                    UploadDate = Convert.ToDateTime(reader["UploadDate"]),
                                    IsWatermarkRemoved = Convert.ToBoolean(Convert.ToInt32(reader["IsWatermarkRemoved"]))
                                });
                            }
                        }
                    }

                    connection.Close();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Şəkilləri əldə edərkən xəta: {ex.Message}", "Xəta",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

            return images;
        }

        // Bütün müştəriləri almaq
        public List<Client> GetAllClients()
        {
            var clients = new List<Client>();

            try
            {
                using (var connection = new SQLiteConnection(_connectionString))
                {
                    connection.Open();

                    using (var command = new SQLiteCommand("SELECT * FROM Clients ORDER BY FullName", connection))
                    {
                        using (var reader = command.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                clients.Add(new Client
                                {
                                    Id = Convert.ToInt32(reader["Id"]),
                                    FullName = reader["FullName"].ToString(),
                                    Phone = reader["Phone"].ToString(),
                                    Email = reader["Email"].ToString(),
                                    Address = reader["Address"].ToString(),
                                    Notes = reader["Notes"].ToString(),
                                    ClientType = Convert.ToInt32(reader["ClientType"]),
                                    RegistrationDate = Convert.ToDateTime(reader["RegistrationDate"]),
                                    AssignedEmployeeId = Convert.ToInt32(reader["AssignedEmployeeId"]),
                                    IsActive = Convert.ToBoolean(Convert.ToInt32(reader["IsActive"])),
                                    LastContactDate = Convert.ToDateTime(reader["LastContactDate"])
                                });
                            }
                        }
                    }

                    connection.Close();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Müştəri məlumatlarını əldə edərkən xəta: {ex.Message}", "Xəta",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

            return clients;
        }

        // Müştəri əlavə etmək
        public int SaveClient(Client client)
        {
            try
            {
                using (var connection = new SQLiteConnection(_connectionString))
                {
                    connection.Open();

                    using (var command = new SQLiteCommand(connection))
                    {
                        // ID-yə görə mövcud olub-olmadığını yoxlamaq
                        if (client.Id > 0)
                        {
                            command.CommandText = "SELECT Id FROM Clients WHERE Id = @Id";
                            command.Parameters.AddWithValue("@Id", client.Id);

                            using (var reader = command.ExecuteReader())
                            {
                                if (reader.Read())
                                {
                                    // Mövcud müştərini yeniləmək
                                    reader.Close();
                                    command.CommandText = @"UPDATE Clients SET
                                        FullName = @FullName,
                                        Phone = @Phone,
                                        Email = @Email,
                                        Address = @Address,
                                        Notes = @Notes,
                                        ClientType = @ClientType,
                                        AssignedEmployeeId = @AssignedEmployeeId,
                                        IsActive = @IsActive,
                                        LastContactDate = datetime('now')
                                        WHERE Id = @Id";
                                }
                                else
                                {
                                    // Yeni müştəri əlavə etmək
                                    reader.Close();
                                    command.CommandText = @"INSERT INTO Clients (
                                        FullName, Phone, Email, Address, Notes, ClientType,
                                        RegistrationDate, AssignedEmployeeId, IsActive, LastContactDate
                                    ) VALUES (
                                        @FullName, @Phone, @Email, @Address, @Notes, @ClientType,
                                        datetime('now'), @AssignedEmployeeId, @IsActive, datetime('now')
                                    )";
                                }
                            }
                        }
                        else
                        {
                            // Yeni müştəri əlavə etmək
                            command.CommandText = @"INSERT INTO Clients (
                                FullName, Phone, Email, Address, Notes, ClientType,
                                RegistrationDate, AssignedEmployeeId, IsActive, LastContactDate
                            ) VALUES (
                                @FullName, @Phone, @Email, @Address, @Notes, @ClientType,
                                datetime('now'), @AssignedEmployeeId, @IsActive, datetime('now')
                            )";
                        }

                        // Parametrləri təyin etmək
                        command.Parameters.Clear();

                        if (client.Id > 0)
                        {
                            command.Parameters.AddWithValue("@Id", client.Id);
                        }

                        command.Parameters.AddWithValue("@FullName", client.FullName);
                        command.Parameters.AddWithValue("@Phone", client.Phone);
                        command.Parameters.AddWithValue("@Email", client.Email);
                        command.Parameters.AddWithValue("@Address", client.Address);
                        command.Parameters.AddWithValue("@Notes", client.Notes);
                        command.Parameters.AddWithValue("@ClientType", client.ClientType);
                        command.Parameters.AddWithValue("@AssignedEmployeeId", client.AssignedEmployeeId);
                        command.Parameters.AddWithValue("@IsActive", client.IsActive ? 1 : 0);

                        command.ExecuteNonQuery();

                        if (client.Id <= 0)
                        {
                            // Yeni əlavə edilmiş müştərinin ID-sini almaq
                            command.CommandText = "SELECT last_insert_rowid()";
                            client.Id = Convert.ToInt32(command.ExecuteScalar());
                        }
                    }

                    connection.Close();
                }

                return client.Id;
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Müştərini yadda saxlayarkən xəta: {ex.Message}", "Xəta",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
                return 0;
            }
        }

        // Əmlakı müştəri ilə əlaqələndirmək
        public bool AssociatePropertyWithClient(int propertyId, int clientId, int relationType = 0)
        {
            try
            {
                using (var connection = new SQLiteConnection(_connectionString))
                {
                    connection.Open();

                    using (var command = new SQLiteCommand(connection))
                    {
                        // Əlaqənin artıq mövcud olub-olmadığını yoxlamaq
                        command.CommandText = "SELECT Id FROM ClientProperties WHERE ClientId = @ClientId AND PropertyId = @PropertyId";
                        command.Parameters.AddWithValue("@ClientId", clientId);
                        command.Parameters.AddWithValue("@PropertyId", propertyId);

                        using (var reader = command.ExecuteReader())
                        {
                            if (reader.Read())
                            {
                                // Əlaqə artıq mövcuddur, yeniləmək lazım deyil
                                reader.Close();
                                return true;
                            }
                            else
                            {
                                // Yeni əlaqə yaratmaq
                                reader.Close();
                                command.CommandText = @"INSERT INTO ClientProperties (
                                    ClientId, PropertyId, RelationType, CreatedDate
                                ) VALUES (
                                    @ClientId, @PropertyId, @RelationType, datetime('now')
                                )";

                                command.Parameters.Clear();
                                command.Parameters.AddWithValue("@ClientId", clientId);
                                command.Parameters.AddWithValue("@PropertyId", propertyId);
                                command.Parameters.AddWithValue("@RelationType", relationType);

                                int rowsAffected = command.ExecuteNonQuery();
                                return rowsAffected > 0;
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Əmlakı müştəri ilə əlaqələndirərkən xəta: {ex.Message}", "Xəta",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
        }

        // Müştərinin maraqlandığı əmlakları almaq
        public List<Property> GetClientInterestedProperties(int clientId)
        {
            var properties = new List<Property>();

            try
            {
                using (var connection = new SQLiteConnection(_connectionString))
                {
                    connection.Open();

                    using (var command = new SQLiteCommand(connection))
                    {
                        command.CommandText = @"SELECT p.* FROM Properties p
                            INNER JOIN ClientProperties cp ON p.Id = cp.PropertyId
                            WHERE cp.ClientId = @ClientId
                            ORDER BY p.ListingDate DESC";

                        command.Parameters.AddWithValue("@ClientId", clientId);

                        using (var reader = command.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                properties.Add(new Property
                                {
                                    Id = Convert.ToInt32(reader["Id"]),
                                    Title = reader["Title"].ToString(),
                                    Description = reader["Description"].ToString(),
                                    Price = Convert.ToDecimal(reader["Price"]),
                                    Location = reader["Location"].ToString(),
                                    Area = Convert.ToDouble(reader["Area"]),
                                    Rooms = Convert.ToInt32(reader["Rooms"]),
                                    PropertyType = Convert.ToInt32(reader["PropertyType"]),
                                    PurposeType = Convert.ToInt32(reader["PurposeType"]),
                                    Source = reader["Source"].ToString(),
                                    SourceUrl = reader["SourceUrl"].ToString(),
                                    ListingDate = Convert.ToDateTime(reader["ListingDate"])
                                });
                            }
                        }
                    }

                    connection.Close();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Müştərinin maraqlandığı əmlakları əldə edərkən xəta: {ex.Message}", "Xəta",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

            return properties;
        }

        // İşçi əlavə etmək
        public int SaveEmployee(Employee employee)
        {
            try
            {
                using (var connection = new SQLiteConnection(_connectionString))
                {
                    connection.Open();

                    using (var command = new SQLiteCommand(connection))
                    {
                        // ID-yə görə mövcud olub-olmadığını yoxlamaq
                        if (employee.Id > 0)
                        {
                            command.CommandText = "SELECT Id FROM Employees WHERE Id = @Id";
                            command.Parameters.AddWithValue("@Id", employee.Id);

                            using (var reader = command.ExecuteReader())
                            {
                                if (reader.Read())
                                {
                                    // Mövcud işçini yeniləmək
                                    reader.Close();
                                    command.CommandText = @"UPDATE Employees SET
                                        FullName = @FullName,
                                        Username = @Username,
                                        PasswordHash = CASE WHEN @PasswordHash = '' THEN PasswordHash ELSE @PasswordHash END,
                                        Position = @Position,
                                        Phone = @Phone,
                                        Email = @Email,
                                        Salary = @Salary,
                                        CommissionRate = @CommissionRate,
                                        IsActive = @IsActive,
                                        IsAdmin = @IsAdmin
                                        WHERE Id = @Id";
                                }
                                else
                                {
                                    // Yeni işçi əlavə etmək
                                    reader.Close();
                                    command.CommandText = @"INSERT INTO Employees (
                                        FullName, Username, PasswordHash, Position, Phone, Email,
                                        Salary, CommissionRate, HireDate, IsActive, IsAdmin, LastLogin
                                    ) VALUES (
                                        @FullName, @Username, @PasswordHash, @Position, @Phone, @Email,
                                        @Salary, @CommissionRate, datetime('now'), @IsActive, @IsAdmin, datetime('now')
                                    )";
                                }
                            }
                        }
                        else
                        {
                            // Yeni işçi əlavə etmək
                            command.CommandText = @"INSERT INTO Employees (
                                FullName, Username, PasswordHash, Position, Phone, Email,
                                Salary, CommissionRate, HireDate, IsActive, IsAdmin, LastLogin
                            ) VALUES (
                                @FullName, @Username, @PasswordHash, @Position, @Phone, @Email,
                                @Salary, @CommissionRate, datetime('now'), @IsActive, @IsAdmin, datetime('now')
                            )";
                        }

                        // Parametrləri təyin etmək
                        command.Parameters.Clear();

                        if (employee.Id > 0)
                        {
                            command.Parameters.AddWithValue("@Id", employee.Id);
                        }

                        command.Parameters.AddWithValue("@FullName", employee.FullName);
                        command.Parameters.AddWithValue("@Username", employee.Username);
                        command.Parameters.AddWithValue("@PasswordHash", employee.PasswordHash);
                        command.Parameters.AddWithValue("@Position", employee.Position);
                        command.Parameters.AddWithValue("@Phone", employee.Phone);
                        command.Parameters.AddWithValue("@Email", employee.Email);
                        command.Parameters.AddWithValue("@Salary", employee.Salary);
                        command.Parameters.AddWithValue("@CommissionRate", employee.CommissionRate);
                        command.Parameters.AddWithValue("@IsActive", employee.IsActive ? 1 : 0);
                        command.Parameters.AddWithValue("@IsAdmin", employee.IsAdmin ? 1 : 0);

                        command.ExecuteNonQuery();

                        if (employee.Id <= 0)
                        {
                            // Yeni əlavə edilmiş işçinin ID-sini almaq
                            command.CommandText = "SELECT last_insert_rowid()";
                            employee.Id = Convert.ToInt32(command.ExecuteScalar());
                        }
                    }

                    connection.Close();
                }

                return employee.Id;
            }
            catch (Exception ex)
            {
                MessageBox.Show($"İşçini yadda saxlayarkən xəta: {ex.Message}", "Xəta",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
                return 0;
            }
        }

        // Bütün işçiləri almaq
        public List<Employee> GetAllEmployees()
        {
            var employees = new List<Employee>();

            try
            {
                using (var connection = new SQLiteConnection(_connectionString))
                {
                    connection.Open();

                    using (var command = new SQLiteCommand("SELECT * FROM Employees ORDER BY FullName", connection))
                    {
                        using (var reader = command.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                employees.Add(new Employee
                                {
                                    Id = Convert.ToInt32(reader["Id"]),
                                    FullName = reader["FullName"].ToString(),
                                    Username = reader["Username"].ToString(),
                                    PasswordHash = reader["PasswordHash"].ToString(),
                                    Position = reader["Position"].ToString(),
                                    Phone = reader["Phone"].ToString(),
                                    Email = reader["Email"].ToString(),
                                    Salary = Convert.ToDecimal(reader["Salary"]),
                                    CommissionRate = Convert.ToDecimal(reader["CommissionRate"]),
                                    HireDate = Convert.ToDateTime(reader["HireDate"]),
                                    IsActive = Convert.ToBoolean(Convert.ToInt32(reader["IsActive"])),
                                    IsAdmin = Convert.ToBoolean(Convert.ToInt32(reader["IsAdmin"])),
                                    LastLogin = Convert.ToDateTime(reader["LastLogin"])
                                });
                            }
                        }
                    }

                    connection.Close();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"İşçi məlumatlarını əldə edərkən xəta: {ex.Message}", "Xəta",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

            return employees;
        }

        // Əməliyyat əlavə etmək
        public int SaveTransaction(Transaction transaction)
        {
            try
            {
                using (var connection = new SQLiteConnection(_connectionString))
                {
                    connection.Open();

                    using (var command = new SQLiteCommand(connection))
                    {
                        // ID-yə görə mövcud olub-olmadığını yoxlamaq
                        if (transaction.Id > 0)
                        {
                            command.CommandText = "SELECT Id FROM Transactions WHERE Id = @Id";
                            command.Parameters.AddWithValue("@Id", transaction.Id);

                            using (var reader = command.ExecuteReader())
                            {
                                if (reader.Read())
                                {
                                    // Mövcud əməliyyatı yeniləmək
                                    reader.Close();
                                    command.CommandText = @"UPDATE Transactions SET
                                        PropertyId = @PropertyId,
                                        ClientId = @ClientId,
                                        EmployeeId = @EmployeeId,
                                        Amount = @Amount,
                                        CommissionAmount = @CommissionAmount,
                                        TransactionType = @TransactionType,
                                        TransactionDate = @TransactionDate,
                                        Description = @Description,
                                        IsSuccessful = @IsSuccessful,
                                        PaymentMethod = @PaymentMethod,
                                        ReferenceNumber = @ReferenceNumber
                                        WHERE Id = @Id";
                                }
                                else
                                {
                                    // Yeni əməliyyat əlavə etmək
                                    reader.Close();
                                    command.CommandText = @"INSERT INTO Transactions (
                                        PropertyId, ClientId, EmployeeId, Amount, CommissionAmount,
                                        TransactionType, TransactionDate, Description, IsSuccessful, 
                                        PaymentMethod, ReferenceNumber
                                    ) VALUES (
                                        @PropertyId, @ClientId, @EmployeeId, @Amount, @CommissionAmount,
                                        @TransactionType, @TransactionDate, @Description, @IsSuccessful, 
                                        @PaymentMethod, @ReferenceNumber
                                    )";
                                }
                            }
                        }
                        else
                        {
                            // Yeni əməliyyat əlavə etmək
                            command.CommandText = @"INSERT INTO Transactions (
                                PropertyId, ClientId, EmployeeId, Amount, CommissionAmount,
                                TransactionType, TransactionDate, Description, IsSuccessful, 
                                PaymentMethod, ReferenceNumber
                            ) VALUES (
                                @PropertyId, @ClientId, @EmployeeId, @Amount, @CommissionAmount,
                                @TransactionType, @TransactionDate, @Description, @IsSuccessful, 
                                @PaymentMethod, @ReferenceNumber
                            )";
                        }

                        // Parametrləri təyin etmək
                        command.Parameters.Clear();

                        if (transaction.Id > 0)
                        {
                            command.Parameters.AddWithValue("@Id", transaction.Id);
                        }

                        command.Parameters.AddWithValue("@PropertyId", transaction.PropertyId);
                        command.Parameters.AddWithValue("@ClientId", transaction.ClientId);
                        command.Parameters.AddWithValue("@EmployeeId", transaction.EmployeeId);
                        command.Parameters.AddWithValue("@Amount", transaction.Amount);
                        command.Parameters.AddWithValue("@CommissionAmount", transaction.CommissionAmount);
                        command.Parameters.AddWithValue("@TransactionType", transaction.TransactionType);
                        command.Parameters.AddWithValue("@TransactionDate", transaction.TransactionDate.ToString("yyyy-MM-dd HH:mm:ss"));
                        command.Parameters.AddWithValue("@Description", transaction.Description);
                        command.Parameters.AddWithValue("@IsSuccessful", transaction.IsSuccessful ? 1 : 0);
                        command.Parameters.AddWithValue("@PaymentMethod", transaction.PaymentMethod);
                        command.Parameters.AddWithValue("@ReferenceNumber", transaction.ReferenceNumber);

                        command.ExecuteNonQuery();

                        if (transaction.Id <= 0)
                        {
                            // Yeni əlavə edilmiş əməliyyatın ID-sini almaq
                            command.CommandText = "SELECT last_insert_rowid()";
                            transaction.Id = Convert.ToInt32(command.ExecuteScalar());
                        }
                    }

                    connection.Close();
                }

                return transaction.Id;
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Əməliyyatı yadda saxlayarkən xəta: {ex.Message}", "Xəta",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
                return 0;
            }
        }

        // Bütün əməliyyatları almaq
        public List<Transaction> GetAllTransactions()
        {
            var transactions = new List<Transaction>();

            try
            {
                using (var connection = new SQLiteConnection(_connectionString))
                {
                    connection.Open();

                    using (var command = new SQLiteCommand("SELECT * FROM Transactions ORDER BY TransactionDate DESC", connection))
                    {
                        using (var reader = command.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                transactions.Add(new Transaction
                                {
                                    Id = Convert.ToInt32(reader["Id"]),
                                    PropertyId = Convert.ToInt32(reader["PropertyId"]),
                                    ClientId = Convert.ToInt32(reader["ClientId"]),
                                    EmployeeId = Convert.ToInt32(reader["EmployeeId"]),
                                    Amount = Convert.ToDecimal(reader["Amount"]),
                                    CommissionAmount = Convert.ToDecimal(reader["CommissionAmount"]),
                                    TransactionType = reader["TransactionType"].ToString(),
                                    TransactionDate = Convert.ToDateTime(reader["TransactionDate"]),
                                    Description = reader["Description"].ToString(),
                                    IsSuccessful = Convert.ToBoolean(Convert.ToInt32(reader["IsSuccessful"])),
                                    PaymentMethod = reader["PaymentMethod"].ToString(),
                                    ReferenceNumber = reader["ReferenceNumber"].ToString()
                                });
                            }
                        }
                    }

                    connection.Close();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Əməliyyat məlumatlarını əldə edərkən xəta: {ex.Message}", "Xəta",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

            return transactions;
        }

        // Xərc əlavə etmək
        public int SaveExpense(Expense expense)
        {
            try
            {
                using (var connection = new SQLiteConnection(_connectionString))
                {
                    connection.Open();

                    using (var command = new SQLiteCommand(connection))
                    {
                        // ID-yə görə mövcud olub-olmadığını yoxlamaq
                        if (expense.Id > 0)
                        {
                            command.CommandText = "SELECT Id FROM Expenses WHERE Id = @Id";
                            command.Parameters.AddWithValue("@Id", expense.Id);

                            using (var reader = command.ExecuteReader())
                            {
                                if (reader.Read())
                                {
                                    // Mövcud xərci yeniləmək
                                    reader.Close();
                                    command.CommandText = @"UPDATE Expenses SET
                                        Title = @Title,
                                        Category = @Category,
                                        Amount = @Amount,
                                        ExpenseDate = @ExpenseDate,
                                        Description = @Description,
                                        EmployeeId = @EmployeeId,
                                        IsPaid = @IsPaid,
                                        PaymentDate = @PaymentDate,
                                        PaymentMethod = @PaymentMethod,
                                        ReferenceNumber = @ReferenceNumber,
                                        ReceiptImagePath = @ReceiptImagePath
                                        WHERE Id = @Id";
                                }
                                else
                                {
                                    // Yeni xərc əlavə etmək
                                    reader.Close();
                                    command.CommandText = @"INSERT INTO Expenses (
                                        Title, Category, Amount, ExpenseDate, Description, EmployeeId,
                                        IsPaid, PaymentDate, PaymentMethod, ReferenceNumber, ReceiptImagePath
                                    ) VALUES (
                                        @Title, @Category, @Amount, @ExpenseDate, @Description, @EmployeeId,
                                        @IsPaid, @PaymentDate, @PaymentMethod, @ReferenceNumber, @ReceiptImagePath
                                    )";
                                }
                            }
                        }
                        else
                        {
                            // Yeni xərc əlavə etmək
                            command.CommandText = @"INSERT INTO Expenses (
                                Title, Category, Amount, ExpenseDate, Description, EmployeeId,
                                IsPaid, PaymentDate, PaymentMethod, ReferenceNumber, ReceiptImagePath
                            ) VALUES (
                                @Title, @Category, @Amount, @ExpenseDate, @Description, @EmployeeId,
                                @IsPaid, @PaymentDate, @PaymentMethod, @ReferenceNumber, @ReceiptImagePath
                            )";
                        }

                        // Parametrləri təyin etmək
                        command.Parameters.Clear();

                        if (expense.Id > 0)
                        {
                            command.Parameters.AddWithValue("@Id", expense.Id);
                        }

                        command.Parameters.AddWithValue("@Title", expense.Title);
                        command.Parameters.AddWithValue("@Category", expense.Category);
                        command.Parameters.AddWithValue("@Amount", expense.Amount);
                        command.Parameters.AddWithValue("@ExpenseDate", expense.ExpenseDate.ToString("yyyy-MM-dd HH:mm:ss"));
                        command.Parameters.AddWithValue("@Description", expense.Description);
                        command.Parameters.AddWithValue("@EmployeeId", expense.EmployeeId.HasValue ? expense.EmployeeId.Value : DBNull.Value);
                        command.Parameters.AddWithValue("@IsPaid", expense.IsPaid ? 1 : 0);
                        command.Parameters.AddWithValue("@PaymentDate", expense.PaymentDate.HasValue ? expense.PaymentDate.Value.ToString("yyyy-MM-dd HH:mm:ss") : DBNull.Value);
                        command.Parameters.AddWithValue("@PaymentMethod", expense.PaymentMethod);
                        command.Parameters.AddWithValue("@ReferenceNumber", expense.ReferenceNumber);
                        command.Parameters.AddWithValue("@ReceiptImagePath", expense.ReceiptImagePath);

                        command.ExecuteNonQuery();

                        if (expense.Id <= 0)
                        {
                            // Yeni əlavə edilmiş xərcin ID-sini almaq
                            command.CommandText = "SELECT last_insert_rowid()";
                            expense.Id = Convert.ToInt32(command.ExecuteScalar());
                        }
                    }

                    connection.Close();
                }

                return expense.Id;
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Xərci yadda saxlayarkən xəta: {ex.Message}", "Xəta",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
                return 0;
            }
        }

        // Bütün xərcləri almaq
        public List<Expense> GetAllExpenses()
        {
            var expenses = new List<Expense>();

            try
            {
                using (var connection = new SQLiteConnection(_connectionString))
                {
                    connection.Open();

                    using (var command = new SQLiteCommand("SELECT * FROM Expenses ORDER BY ExpenseDate DESC", connection))
                    {
                        using (var reader = command.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                expenses.Add(new Expense
                                {
                                    Id = Convert.ToInt32(reader["Id"]),
                                    Title = reader["Title"].ToString(),
                                    Category = reader["Category"].ToString(),
                                    Amount = Convert.ToDecimal(reader["Amount"]),
                                    ExpenseDate = Convert.ToDateTime(reader["ExpenseDate"]),
                                    Description = reader["Description"].ToString(),
                                    EmployeeId = reader["EmployeeId"] != DBNull.Value ? (int?)Convert.ToInt32(reader["EmployeeId"]) : null,
                                    IsPaid = Convert.ToBoolean(Convert.ToInt32(reader["IsPaid"])),
                                    PaymentDate = reader["PaymentDate"] != DBNull.Value ? (DateTime?)Convert.ToDateTime(reader["PaymentDate"]) : null,
                                    PaymentMethod = reader["PaymentMethod"].ToString(),
                                    ReferenceNumber = reader["ReferenceNumber"].ToString(),
                                    ReceiptImagePath = reader["ReceiptImagePath"].ToString()
                                });
                            }
                        }
                    }

                    connection.Close();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Xərc məlumatlarını əldə edərkən xəta: {ex.Message}", "Xəta",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

            return expenses;
        }

        // Aylıq gəliri hesablamaq
        public decimal GetMonthlyRevenue()
        {
            decimal revenue = 0;

            try
            {
                using (var connection = new SQLiteConnection(_connectionString))
                {
                    connection.Open();

                    using (var command = new SQLiteCommand(connection))
                    {
                        command.CommandText = @"SELECT SUM(Amount) AS TotalRevenue 
                            FROM Transactions 
                            WHERE IsSuccessful = 1 
                            AND strftime('%Y-%m', TransactionDate) = strftime('%Y-%m', 'now')";

                        object result = command.ExecuteScalar();
                        if (result != null && result != DBNull.Value)
                        {
                            revenue = Convert.ToDecimal(result);
                        }
                    }

                    connection.Close();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Aylıq gəliri hesablayarkən xəta: {ex.Message}", "Xəta",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

            return revenue;
        }

        // Aktiv elanların sayını almaq
        public int GetActiveListingsCount()
        {
            int count = 0;

            try
            {
                using (var connection = new SQLiteConnection(_connectionString))
                {
                    connection.Open();

                    using (var command = new SQLiteCommand("SELECT COUNT(*) FROM Properties WHERE IsSold = 0", connection))
                    {
                        count = Convert.ToInt32(command.ExecuteScalar());
                    }

                    connection.Close();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Aktiv elanların sayını hesablayarkən xəta: {ex.Message}", "Xəta",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

            return count;
        }

        // Müştərilərin sayını almaq
        public int GetClientsCount()
        {
            int count = 0;

            try
            {
                using (var connection = new SQLiteConnection(_connectionString))
                {
                    connection.Open();

                    using (var command = new SQLiteCommand("SELECT COUNT(*) FROM Clients WHERE IsActive = 1", connection))
                    {
                        count = Convert.ToInt32(command.ExecuteScalar());
                    }

                    connection.Close();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Müştərilərin sayını hesablayarkən xəta: {ex.Message}", "Xəta",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

            return count;
        }

        // Gözləyən müştərilərin sayını almaq (son 30 gün ərzində əlaqə olmayan)
        public int GetPendingClientsCount()
        {
            int count = 0;

            try
            {
                using (var connection = new SQLiteConnection(_connectionString))
                {
                    connection.Open();

                    using (var command = new SQLiteCommand(connection))
                    {
                        command.CommandText = @"SELECT COUNT(*) FROM Clients 
                            WHERE IsActive = 1 
                            AND julianday('now') - julianday(LastContactDate) > 30";

                        count = Convert.ToInt32(command.ExecuteScalar());
                    }

                    connection.Close();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Gözləyən müştərilərin sayını hesablayarkən xəta: {ex.Message}", "Xəta",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

            return count;
        }
    }
}
