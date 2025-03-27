using System;
using System.Data;
using System.Data.SQLite;
using System.IO;
using DashinmazEmlakApp.Utilities;

namespace DashinmazEmlakApp.Database
{
    /// <summary>
    /// Verilənlər bazası ilə əlaqə qurmaq üçün sinif
    /// </summary>
    public class DatabaseManager
    {
        private static readonly string DatabaseFileName = Constants.DatabaseFileName;
        private static readonly string ConnectionString = $"Data Source={DatabaseFileName};Version=3;";

        /// <summary>
        /// Verilənlər bazası əlaqəsini qaytarır
        /// </summary>
        /// <returns>SQLite əlaqəsi</returns>
        public static SQLiteConnection GetConnection()
        {
            return new SQLiteConnection(ConnectionString);
        }

        /// <summary>
        /// Verilənlər bazasının mövcudluğunu yoxlayır
        /// </summary>
        /// <returns>Baza mövcuddursa true, əks halda false</returns>
        public static bool DatabaseExists()
        {
            return File.Exists(DatabaseFileName);
        }

        /// <summary>
        /// Verilənlər bazasını yaradır və struktur cədvəlləri qurur
        /// </summary>
        /// <returns>Əməliyyat uğurlu olarsa true, əks halda false</returns>
        public static bool InitializeDatabase()
        {
            try
            {
                if (!DatabaseExists())
                {
                    SQLiteConnection.CreateFile(DatabaseFileName);

                    using (var connection = GetConnection())
                    {
                        connection.Open();

                        // Property (Əmlak) cədvəli
                        ExecuteNonQuery(connection, @"
                            CREATE TABLE Properties (
                                Id                INTEGER PRIMARY KEY AUTOINCREMENT,
                                Title             TEXT,
                                Description       TEXT,
                                Type              INTEGER,
                                BuildingType      INTEGER,
                                ListingType       INTEGER,
                                Area              REAL,
                                Rooms             INTEGER,
                                Floor             INTEGER,
                                TotalFloors       INTEGER,
                                Address           TEXT,
                                City              TEXT,
                                District          TEXT,
                                Price             DECIMAL(18,2),
                                ContactPhone      TEXT,
                                ContactEmail      TEXT,
                                OwnerId           INTEGER,
                                EmployeeId        INTEGER,
                                Status            INTEGER,
                                ListingDate       DATETIME,
                                LastUpdated       DATETIME,
                                ImagePaths        TEXT,
                                SourceUrl         TEXT,
                                Features          TEXT,
                                Notes             TEXT
                            )
                        ");

                        // Client (Müştəri) cədvəli
                        ExecuteNonQuery(connection, @"
                            CREATE TABLE Clients (
                                Id                INTEGER PRIMARY KEY AUTOINCREMENT,
                                FirstName         TEXT,
                                LastName          TEXT,
                                Phone             TEXT,
                                AlternativePhone  TEXT,
                                Email             TEXT,
                                Address           TEXT,
                                Type              INTEGER,
                                Status            INTEGER,
                                RegistrationDate  DATETIME,
                                LastContactDate   DATETIME,
                                Notes             TEXT,
                                AssignedEmployeeId INTEGER,
                                BudgetLimit       DECIMAL(18,2),
                                DesiredPropertyTypes TEXT,
                                DesiredDistricts  TEXT,
                                MinRooms          INTEGER,
                                MinArea           REAL
                            )
                        ");

                        // Employee (İşçi) cədvəli
                        ExecuteNonQuery(connection, @"
                            CREATE TABLE Employees (
                                Id                INTEGER PRIMARY KEY AUTOINCREMENT,
                                FirstName         TEXT,
                                LastName          TEXT,
                                Role              INTEGER,
                                Status            INTEGER,
                                Phone             TEXT,
                                Email             TEXT,
                                Address           TEXT,
                                HireDate          DATETIME,
                                TerminationDate   DATETIME,
                                Salary            DECIMAL(18,2),
                                CommissionRate    DECIMAL(6,2),
                                Notes             TEXT,
                                IdNumber          TEXT,
                                Username          TEXT,
                                PasswordHash      TEXT,
                                SystemRole        TEXT,
                                CreatedDate       DATETIME,
                                LastUpdated       DATETIME
                            )
                        ");

                        // Transaction (Əməliyyat) cədvəli
                        ExecuteNonQuery(connection, @"
                            CREATE TABLE Transactions (
                                Id                INTEGER PRIMARY KEY AUTOINCREMENT,
                                Type              INTEGER,
                                Amount            DECIMAL(18,2),
                                Date              DATETIME,
                                Status            INTEGER,
                                PaymentMethod     INTEGER,
                                PropertyId        INTEGER,
                                ClientId          INTEGER,
                                EmployeeId        INTEGER,
                                Description       TEXT,
                                ReferenceNumber   TEXT,
                                Notes             TEXT,
                                IsSuccessful      BOOLEAN,
                                CreatedDate       DATETIME,
                                LastUpdated       DATETIME
                            )
                        ");

                        // Expense (Xərc) cədvəli
                        ExecuteNonQuery(connection, @"
                            CREATE TABLE Expenses (
                                Id                INTEGER PRIMARY KEY AUTOINCREMENT,
                                Name              TEXT,
                                Type              INTEGER,
                                Amount            DECIMAL(18,2),
                                Date              DATETIME,
                                EmployeeId        INTEGER,
                                PropertyId        INTEGER,
                                Description       TEXT,
                                ReceiptPath       TEXT,
                                Status            INTEGER,
                                IsPaid            BOOLEAN,
                                PaidDate          DATETIME,
                                CreatedDate       DATETIME,
                                LastUpdated       DATETIME,
                                Notes             TEXT
                            )
                        ");

                        // InterestedClients (Maraqlanan müştərilər) cədvəli - many-to-many relation
                        ExecuteNonQuery(connection, @"
                            CREATE TABLE InterestedClients (
                                PropertyId        INTEGER,
                                ClientId          INTEGER,
                                InterestDate      DATETIME,
                                Notes             TEXT,
                                PRIMARY KEY (PropertyId, ClientId)
                            )
                        ");

                        LoggingHelper.LogInfo("Verilənlər bazası uğurla yaradıldı");
                    }
                }

                return true;
            }
            catch (Exception ex)
            {
                LoggingHelper.LogError($"Verilənlər bazası yaradılarkən xəta: {ex.Message}");
                return false;
            }
        }

        /// <summary>
        /// SQL sorğusunu yerinə yetirir və təsirlənən sətirlərin sayını qaytarır
        /// </summary>
        private static int ExecuteNonQuery(SQLiteConnection connection, string commandText, params SQLiteParameter[] parameters)
        {
            using (var command = new SQLiteCommand(commandText, connection))
            {
                if (parameters != null)
                {
                    command.Parameters.AddRange(parameters);
                }

                return command.ExecuteNonQuery();
            }
        }
    }
}