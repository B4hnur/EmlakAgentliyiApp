using System;
using System.Data.SQLite;
using System.IO;
using System.Configuration;

namespace DasinmazEmlakAgentligi.Database
{
    public static class SQLiteInitializer
    {
        private static readonly string _connectionString = ConfigurationManager.ConnectionStrings["DasinmazEmlakDb"].ConnectionString;

        public static void Initialize()
        {
            string databaseFolderPath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory);
            string databaseFilePath = Path.Combine(databaseFolderPath, "DasinmazEmlak.db");

            bool createTables = false;

            // Verilənlər bazası faylı mövcud deyilsə, yaradırıq
            if (!File.Exists(databaseFilePath))
            {
                SQLiteConnection.CreateFile(databaseFilePath);
                createTables = true;
            }

            // Qovluqlar yaradırıq
            CreateRequiredDirectories();

            if (createTables)
            {
                // Cədvəlləri yaradırıq
                CreateTables();
            }
        }

        private static void CreateRequiredDirectories()
        {
            // Şəkillər qovluğu
            string imagesFolder = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, ConfigurationManager.AppSettings["ImagesFolder"]);
            if (!Directory.Exists(imagesFolder))
            {
                Directory.CreateDirectory(imagesFolder);
            }

            // Backup qovluğu
            string backupFolder = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, ConfigurationManager.AppSettings["BackupFolder"]);
            if (!Directory.Exists(backupFolder))
            {
                Directory.CreateDirectory(backupFolder);
            }

            // Logs qovluğu
            string logsFolder = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, ConfigurationManager.AppSettings["LogsFolder"]);
            if (!Directory.Exists(logsFolder))
            {
                Directory.CreateDirectory(logsFolder);
            }
        }

        private static void CreateTables()
        {
            try
            {
                using (SQLiteConnection connection = new SQLiteConnection(_connectionString))
                {
                    connection.Open();

                    // Properties cədvəli
                    string createPropertiesTableQuery = @"
                    CREATE TABLE IF NOT EXISTS Properties (
                        Id INTEGER PRIMARY KEY AUTOINCREMENT,
                        Title TEXT NOT NULL,
                        Description TEXT,
                        Price DECIMAL(18,2) NOT NULL,
                        Currency TEXT NOT NULL,
                        PropertyType INTEGER NOT NULL,
                        Purpose INTEGER NOT NULL,
                        Address TEXT,
                        City TEXT,
                        District TEXT,
                        Area DECIMAL(18,2),
                        Rooms INTEGER,
                        Floor INTEGER,
                        TotalFloors INTEGER,
                        OwnerName TEXT,
                        OwnerPhone TEXT,
                        OwnerEmail TEXT,
                        IsAgencyProperty BOOLEAN NOT NULL DEFAULT 0,
                        Status INTEGER NOT NULL,
                        CreatedDate DATETIME NOT NULL,
                        ModifiedDate DATETIME NOT NULL,
                        OriginalListingUrl TEXT,
                        OriginalListingSite TEXT
                    );";

                    using (SQLiteCommand command = new SQLiteCommand(createPropertiesTableQuery, connection))
                    {
                        command.ExecuteNonQuery();
                    }

                    // PropertyImages cədvəli
                    string createPropertyImagesTableQuery = @"
                    CREATE TABLE IF NOT EXISTS PropertyImages (
                        Id INTEGER PRIMARY KEY AUTOINCREMENT,
                        PropertyId INTEGER NOT NULL,
                        FilePath TEXT NOT NULL,
                        SortOrder INTEGER NOT NULL DEFAULT 0,
                        FOREIGN KEY(PropertyId) REFERENCES Properties(Id)
                    );";

                    using (SQLiteCommand command = new SQLiteCommand(createPropertyImagesTableQuery, connection))
                    {
                        command.ExecuteNonQuery();
                    }

                    // Clients cədvəli
                    string createClientsTableQuery = @"
                    CREATE TABLE IF NOT EXISTS Clients (
                        Id INTEGER PRIMARY KEY AUTOINCREMENT,
                        FullName TEXT NOT NULL,
                        Phone TEXT,
                        Email TEXT,
                        Address TEXT,
                        Details TEXT,
                        ClientType INTEGER NOT NULL,
                        CreatedDate DATETIME NOT NULL,
                        ModifiedDate DATETIME NOT NULL
                    );";

                    using (SQLiteCommand command = new SQLiteCommand(createClientsTableQuery, connection))
                    {
                        command.ExecuteNonQuery();
                    }

                    // Employees cədvəli
                    string createEmployeesTableQuery = @"
                    CREATE TABLE IF NOT EXISTS Employees (
                        Id INTEGER PRIMARY KEY AUTOINCREMENT,
                        FullName TEXT NOT NULL,
                        Phone TEXT,
                        Email TEXT,
                        Position TEXT,
                        BaseSalary DECIMAL(18,2) NOT NULL,
                        CommissionRate DECIMAL(18,2) NOT NULL,
                        HireDate DATETIME NOT NULL,
                        IsActive BOOLEAN NOT NULL DEFAULT 1
                    );";

                    using (SQLiteCommand command = new SQLiteCommand(createEmployeesTableQuery, connection))
                    {
                        command.ExecuteNonQuery();
                    }

                    // Transactions cədvəli
                    string createTransactionsTableQuery = @"
                    CREATE TABLE IF NOT EXISTS Transactions (
                        Id INTEGER PRIMARY KEY AUTOINCREMENT,
                        PropertyId INTEGER NOT NULL,
                        ClientId INTEGER NOT NULL,
                        EmployeeId INTEGER NOT NULL,
                        TransactionType INTEGER NOT NULL,
                        Amount DECIMAL(18,2) NOT NULL,
                        CommissionAmount DECIMAL(18,2) NOT NULL,
                        Currency TEXT NOT NULL,
                        Notes TEXT,
                        TransactionDate DATETIME NOT NULL,
                        FOREIGN KEY(PropertyId) REFERENCES Properties(Id),
                        FOREIGN KEY(ClientId) REFERENCES Clients(Id),
                        FOREIGN KEY(EmployeeId) REFERENCES Employees(Id)
                    );";

                    using (SQLiteCommand command = new SQLiteCommand(createTransactionsTableQuery, connection))
                    {
                        command.ExecuteNonQuery();
                    }

                    // Expenses cədvəli
                    string createExpensesTableQuery = @"
                    CREATE TABLE IF NOT EXISTS Expenses (
                        Id INTEGER PRIMARY KEY AUTOINCREMENT,
                        ExpenseType INTEGER NOT NULL,
                        Amount DECIMAL(18,2) NOT NULL,
                        Currency TEXT NOT NULL,
                        Description TEXT,
                        ExpenseDate DATETIME NOT NULL
                    );";

                    using (SQLiteCommand command = new SQLiteCommand(createExpensesTableQuery, connection))
                    {
                        command.ExecuteNonQuery();
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Verilənlər bazası cədvəllərini yaradarkən xəta baş verdi: " + ex.Message);
            }
        }
    }
}
