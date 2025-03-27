using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SQLite;
using DashinmazEmlakApp.Database;
using DashinmazEmlakApp.Database.Models;
using DashinmazEmlakApp.Utilities;

namespace DashinmazEmlakApp.Forms
{
    public partial class TransactionForm : Form
    {
        private readonly DatabaseManager _dbManager;
        private int _selectedTransactionId = 0;
        private List<Property> _properties = new List<Property>();
        private List<Client> _clients = new List<Client>();
        private List<Employee> _employees = new List<Employee>();

        public TransactionForm(DatabaseManager dbManager)
        {
            InitializeComponent();

            _dbManager = dbManager;

            // Set up event handlers
            this.Load += TransactionForm_Load;
            tabControl.SelectedIndexChanged += TabControl_SelectedIndexChanged;
        }

        private void TransactionForm_Load(object sender, EventArgs e)
        {
            // Set the form title
            this.Text = "Əmlak Əməliyyatları";

            // Load filter comboboxes
            LoadTransactionTypeFilters();

            // Set default date ranges
            dtpStartDate.Value = new DateTime(DateTime.Now.Year, DateTime.Now.Month, 1);
            dtpEndDate.Value = DateTime.Now;

            // Load transactions
            LoadTransactions();

            // Load data for new transaction form
            LoadEmployees();
            LoadClients();
            LoadProperties();

            // Initialize new transaction combo boxes
            InitializeTransactionComboBoxes();

            // Set transaction date to today
            dtpTransactionDate.Value = DateTime.Now;

            // Setup autocomplete fields
            SetupAutoComplete();
        }

        private void LoadTransactionTypeFilters()
        {
            cmbFilterTransactionType.Items.Clear();
            cmbFilterTransactionType.Items.Add(new { Text = "Hamısı", Value = -1 });
            cmbFilterTransactionType.Items.Add(new { Text = "Satış", Value = (int)TransactionType.Sale });
            cmbFilterTransactionType.Items.Add(new { Text = "Kirayə", Value = (int)TransactionType.Rent });
            cmbFilterTransactionType.DisplayMember = "Text";
            cmbFilterTransactionType.ValueMember = "Value";
            cmbFilterTransactionType.SelectedIndex = 0;
        }

        private void InitializeTransactionComboBoxes()
        {
            // Transaction Type
            cmbTransactionType.Items.Clear();
            cmbTransactionType.Items.Add(new { Text = "Satış", Value = TransactionType.Sale });
            cmbTransactionType.Items.Add(new { Text = "Kirayə", Value = TransactionType.Rent });
            cmbTransactionType.DisplayMember = "Text";
            cmbTransactionType.ValueMember = "Value";
            cmbTransactionType.SelectedIndex = 0;

            // Properties
            RefreshPropertyComboBox();

            // Clients
            RefreshClientComboBox();

            // Employees
            RefreshEmployeeComboBox();
        }

        private void SetupAutoComplete()
        {
            // Set up autocomplete for property search
            txtPropertySearch.AutoCompleteMode = AutoCompleteMode.SuggestAppend;
            txtPropertySearch.AutoCompleteSource = AutoCompleteSource.CustomSource;

            AutoCompleteStringCollection propertyCollection = new AutoCompleteStringCollection();
            foreach (var property in _properties)
            {
                propertyCollection.Add(property.Title);
                if (!string.IsNullOrEmpty(property.Address))
                {
                    propertyCollection.Add(property.Address);
                }
            }
            txtPropertySearch.AutoCompleteCustomSource = propertyCollection;

            // Set up autocomplete for client search
            txtClientSearch.AutoCompleteMode = AutoCompleteMode.SuggestAppend;
            txtClientSearch.AutoCompleteSource = AutoCompleteSource.CustomSource;

            AutoCompleteStringCollection clientCollection = new AutoCompleteStringCollection();
            foreach (var client in _clients)
            {
                clientCollection.Add(client.FullName);
                if (!string.IsNullOrEmpty(client.Phone))
                {
                    clientCollection.Add(client.Phone);
                }
            }
            txtClientSearch.AutoCompleteCustomSource = clientCollection;
        }

        private void LoadTransactions(string searchText = "", int transactionType = -1)
        {
            try
            {
                lvwTransactions.Items.Clear();

                DateTime startDate = dtpStartDate.Value.Date;
                DateTime endDate = dtpEndDate.Value.Date.AddHours(23).AddMinutes(59).AddSeconds(59);

                using (var connection = _dbManager.GetConnection())
                {
                    string query = @"SELECT t.Id, t.TransactionDate, t.TransactionType, p.Title, p.Address, 
                                        c.FullName, e.FullName, t.Amount, t.Commission, t.Notes
                                   FROM Transactions t
                                   JOIN Properties p ON t.PropertyId = p.Id
                                   JOIN Clients c ON t.ClientId = c.Id
                                   JOIN Employees e ON t.EmployeeId = e.Id
                                   WHERE t.TransactionDate BETWEEN @StartDate AND @EndDate";

                    if (!string.IsNullOrEmpty(searchText))
                    {
                        query += @" AND (p.Title LIKE @SearchText OR p.Address LIKE @SearchText OR c.FullName LIKE @SearchText OR e.FullName LIKE @SearchText)";
                    }

                    if (transactionType >= 0)
                    {
                        query += @" AND t.TransactionType = @TransactionType";
                    }

                    query += " ORDER BY t.TransactionDate DESC";

                    using (var command = new SQLiteCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("@StartDate", startDate.ToString("yyyy-MM-dd"));
                        command.Parameters.AddWithValue("@EndDate", endDate.ToString("yyyy-MM-dd HH:mm:ss"));

                        if (!string.IsNullOrEmpty(searchText))
                        {
                            command.Parameters.AddWithValue("@SearchText", $"%{searchText}%");
                        }

                        if (transactionType >= 0)
                        {
                            command.Parameters.AddWithValue("@TransactionType", transactionType);
                        }

                        using (var reader = command.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                int id = reader.GetInt32(0);
                                DateTime transDate = DateTime.Parse(reader.GetString(1));
                                TransactionType type = (TransactionType)reader.GetInt32(2);
                                string propertyTitle = reader.GetString(3);
                                string propertyAddress = reader.GetString(4);
                                string clientName = reader.GetString(5);
                                string employeeName = reader.GetString(6);
                                double amount = reader.GetDouble(7);
                                double commission = reader.GetDouble(8);
                                string notes = reader.IsDBNull(9) ? "" : reader.GetString(9);

                                ListViewItem item = new ListViewItem(transDate.ToString("dd.MM.yyyy"));
                                item.SubItems.Add(type == TransactionType.Sale ? "Satış" : "Kirayə");
                                item.SubItems.Add(propertyTitle);
                                item.SubItems.Add(propertyAddress);
                                item.SubItems.Add(clientName);
                                item.SubItems.Add(employeeName);
                                item.SubItems.Add(amount.ToString("N0") + " ₼");
                                item.SubItems.Add(commission.ToString("N0") + " ₼");

                                // Store the transaction ID and notes in the Tag property
                                item.Tag = new Tuple<int, string>(id, notes);

                                lvwTransactions.Items.Add(item);
                            }
                        }
                    }
                }

                // Update status
                lblStatus.Text = $"{lvwTransactions.Items.Count} əməliyyat tapıldı";
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Əməliyyatları yükləyərkən xəta baş verdi: {ex.Message}", "Xəta", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void LoadEmployees()
        {
            try
            {
                _employees.Clear();

                using (var connection = _dbManager.GetConnection())
                {
                    using (var command = new SQLiteCommand(
                        @"SELECT Id, FullName, CommissionRate
                        FROM Employees
                        WHERE IsActive = 1
                        ORDER BY FullName", connection))
                    {
                        using (var reader = command.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                Employee employee = new Employee
                                {
                                    Id = reader.GetInt32(0),
                                    FullName = reader.GetString(1),
                                    CommissionRate = reader.GetDouble(2)
                                };

                                _employees.Add(employee);
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"İşçiləri yükləyərkən xəta baş verdi: {ex.Message}", "Xəta", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void LoadClients()
        {
            try
            {
                _clients.Clear();

                using (var connection = _dbManager.GetConnection())
                {
                    using (var command = new SQLiteCommand(
                        @"SELECT Id, FullName, Phone, ClientType
                        FROM Clients
                        ORDER BY FullName", connection))
                    {
                        using (var reader = command.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                Client client = new Client
                                {
                                    Id = reader.GetInt32(0),
                                    FullName = reader.GetString(1),
                                    Phone = reader.GetString(2),
                                    Type = (ClientType)reader.GetInt32(3)
                                };

                                _clients.Add(client);
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Müştəriləri yükləyərkən xəta baş verdi: {ex.Message}", "Xəta", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void LoadProperties()
        {
            try
            {
                _properties.Clear();

                using (var connection = _dbManager.GetConnection())
                {
                    using (var command = new SQLiteCommand(
                        @"SELECT Id, Title, Address, Price, PropertyType, Purpose
                        FROM Properties
                        WHERE Status = 0
                        ORDER BY Title", connection))
                    {
                        using (var reader = command.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                Property property = new Property
                                {
                                    Id = reader.GetInt32(0),
                                    Title = reader.GetString(1),
                                    Address = reader.GetString(2),
                                    Price = reader.GetDouble(3),
                                    Type = (PropertyType)reader.GetInt32(4),
                                    Purpose = (Purpose)reader.GetInt32(5)
                                };

                                _properties.Add(property);
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Əmlakları yükləyərkən xəta baş verdi: {ex.Message}", "Xəta", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void RefreshPropertyComboBox()
        {
            cmbProperty.Items.Clear();

            foreach (var property in _properties)
            {
                cmbProperty.Items.Add(new
                {
                    Text = $"{property.Title} ({property.Address})",
                    Value = property.Id,
                    Property = property
                });
            }

            cmbProperty.DisplayMember = "Text";
            cmbProperty.ValueMember = "Value";

            if (cmbProperty.Items.Count > 0)
            {
                cmbProperty.SelectedIndex = 0;
            }
        }

        private void RefreshClientComboBox()
        {
            cmbClient.Items.Clear();

            // Filter clients based on transaction type
            TransactionType transactionType = (TransactionType)((dynamic)cmbTransactionType.SelectedItem).Value;

            var filteredClients = _clients.Where(c =>
                c.Type == ClientType.Both ||
                (transactionType == TransactionType.Sale && c.Type == ClientType.Buyer) ||
                (transactionType == TransactionType.Rent && c.Type == ClientType.Buyer)).ToList();

            foreach (var client in filteredClients)
            {
                cmbClient.Items.Add(new
                {
                    Text = $"{client.FullName} ({client.Phone})",
                    Value = client.Id
                });
            }

            cmbClient.DisplayMember = "Text";
            cmbClient.ValueMember = "Value";

            if (cmbClient.Items.Count > 0)
            {
                cmbClient.SelectedIndex = 0;
            }
        }

        private void RefreshEmployeeComboBox()
        {
            cmbEmployee.Items.Clear();

            foreach (var employee in _employees)
            {
                cmbEmployee.Items.Add(new
                {
                    Text = employee.FullName,
                    Value = employee.Id,
                    CommissionRate = employee.CommissionRate
                });
            }

            cmbEmployee.DisplayMember = "Text";
            cmbEmployee.ValueMember = "Value";

            if (cmbEmployee.Items.Count > 0)
            {
                cmbEmployee.SelectedIndex = 0;
            }
        }

        private void ClearInputFields()
        {
            txtAmount.Text = "";
            txtCommission.Text = "";
            txtNotes.Text = "";
            dtpTransactionDate.Value = DateTime.Now;

            cmbTransactionType.SelectedIndex = 0;

            // Reset the selected transaction ID
            _selectedTransactionId = 0;

            // Update button text
            btnSave.Text = "Əlavə Et";
            btnDelete.Enabled = false;

            // Force recalculation of commission
            cmbEmployee_SelectedIndexChanged(null, null);
        }

        private void LoadTransactionDetails(int transactionId)
        {
            try
            {
                using (var connection = _dbManager.GetConnection())
                {
                    using (var command = new SQLiteCommand(
                        @"SELECT TransactionType, PropertyId, ClientId, EmployeeId, Amount, Commission, TransactionDate, Notes
                        FROM Transactions
                        WHERE Id = @Id", connection))
                    {
                        command.Parameters.AddWithValue("@Id", transactionId);

                        using (var reader = command.ExecuteReader())
                        {
                            if (reader.Read())
                            {
                                TransactionType type = (TransactionType)reader.GetInt32(0);
                                int propertyId = reader.GetInt32(1);
                                int clientId = reader.GetInt32(2);
                                int employeeId = reader.GetInt32(3);
                                double amount = reader.GetDouble(4);
                                double commission = reader.GetDouble(5);
                                DateTime transactionDate = DateTime.Parse(reader.GetString(6));
                                string notes = reader.IsDBNull(7) ? "" : reader.GetString(7);

                                // Set transaction type
                                for (int i = 0; i < cmbTransactionType.Items.Count; i++)
                                {
                                    if ((TransactionType)((dynamic)cmbTransactionType.Items[i]).Value == type)
                                    {
                                        cmbTransactionType.SelectedIndex = i;
                                        break;
                                    }
                                }

                                // Set property
                                for (int i = 0; i < cmbProperty.Items.Count; i++)
                                {
                                    if ((int)((dynamic)cmbProperty.Items[i]).Value == propertyId)
                                    {
                                        cmbProperty.SelectedIndex = i;
                                        break;
                                    }
                                }

                                // Set client
                                for (int i = 0; i < cmbClient.Items.Count; i++)
                                {
                                    if ((int)((dynamic)cmbClient.Items[i]).Value == clientId)
                                    {
                                        cmbClient.SelectedIndex = i;
                                        break;
                                    }
                                }

                                // Set employee
                                for (int i = 0; i < cmbEmployee.Items.Count; i++)
                                {
                                    if ((int)((dynamic)cmbEmployee.Items[i]).Value == employeeId)
                                    {
                                        cmbEmployee.SelectedIndex = i;
                                        break;
                                    }
                                }

                                // Set other fields
                                txtAmount.Text = amount.ToString("0.##");
                                txtCommission.Text = commission.ToString("0.##");
                                dtpTransactionDate.Value = transactionDate;
                                txtNotes.Text = notes;

                                // Store the selected transaction ID
                                _selectedTransactionId = transactionId;

                                // Update button text
                                btnSave.Text = "Yenilə";
                                btnDelete.Enabled = true;

                                // Switch to edit tab
                                tabControl.SelectedTab = tabNewTransaction;
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Əməliyyat məlumatlarını yükləyərkən xəta baş verdi: {ex.Message}", "Xəta", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnSearch_Click(object sender, EventArgs e)
        {
            string searchText = txtSearch.Text.Trim();
            int transactionType = ((dynamic)cmbFilterTransactionType.SelectedItem).Value;

            LoadTransactions(searchText, transactionType);
        }

        private void btnClear_Click(object sender, EventArgs e)
        {
            txtSearch.Text = "";
            cmbFilterTransactionType.SelectedIndex = 0;

            LoadTransactions();
        }

        private void btnNewTransaction_Click(object sender, EventArgs e)
        {
            ClearInputFields();

            // Switch to new transaction tab
            tabControl.SelectedTab = tabNewTransaction;
        }

        private void cmbTransactionType_SelectedIndexChanged(object sender, EventArgs e)
        {
            // Update client combo box based on transaction type
            RefreshClientComboBox();

            // Update property price based on transaction type and selected property
            UpdatePropertyPrice();
        }

        private void cmbProperty_SelectedIndexChanged(object sender, EventArgs e)
        {
            UpdatePropertyPrice();
        }

        private void UpdatePropertyPrice()
        {
            if (cmbProperty.SelectedIndex >= 0)
            {
                try
                {
                    Property selectedProperty = ((dynamic)cmbProperty.SelectedItem).Property;

                    if (selectedProperty != null)
                    {
                        // Get transaction type
                        TransactionType transactionType = (TransactionType)((dynamic)cmbTransactionType.SelectedItem).Value;

                        // Set amount based on transaction type and property
                        if (transactionType == TransactionType.Sale)
                        {
                            txtAmount.Text = selectedProperty.Price.ToString("0.##");
                        }
                        else
                        {
                            // For rental, we'll show the price but allow user to adjust for monthly rent
                            txtAmount.Text = selectedProperty.Price.ToString("0.##");
                        }

                        // Calculate commission
                        CalculateCommission();
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Error updating property price: {ex.Message}");
                }
            }
        }

        private void cmbEmployee_SelectedIndexChanged(object sender, EventArgs e)
        {
            CalculateCommission();
        }

        private void txtAmount_TextChanged(object sender, EventArgs e)
        {
            CalculateCommission();
        }

        private void CalculateCommission()
        {
            try
            {
                if (cmbEmployee.SelectedIndex >= 0 && !string.IsNullOrEmpty(txtAmount.Text))
                {
                    double commissionRate = ((dynamic)cmbEmployee.SelectedItem).CommissionRate;
                    double amount = double.Parse(txtAmount.Text);
                    double commission = amount * commissionRate / 100;

                    txtCommission.Text = commission.ToString("0.##");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error calculating commission: {ex.Message}");
            }
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            try
            {
                // Validate required fields
                if (cmbProperty.SelectedIndex < 0)
                {
                    MessageBox.Show("Əmlak seçilməlidir!", "Xəta", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    cmbProperty.Focus();
                    return;
                }

                if (cmbClient.SelectedIndex < 0)
                {
                    MessageBox.Show("Müştəri seçilməlidir!", "Xəta", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    cmbClient.Focus();
                    return;
                }

                if (cmbEmployee.SelectedIndex < 0)
                {
                    MessageBox.Show("İşçi seçilməlidir!", "Xəta", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    cmbEmployee.Focus();
                    return;
                }

                if (!double.TryParse(txtAmount.Text, out double amount) || amount <= 0)
                {
                    MessageBox.Show("Məbləğ düzgün daxil edilməlidir!", "Xəta", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    txtAmount.Focus();
                    return;
                }

                if (!double.TryParse(txtCommission.Text, out double commission) || commission < 0)
                {
                    MessageBox.Show("Komissiya düzgün daxil edilməlidir!", "Xəta", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    txtCommission.Focus();
                    return;
                }

                // Get selected values
                TransactionType transactionType = (TransactionType)((dynamic)cmbTransactionType.SelectedItem).Value;
                int propertyId = (int)((dynamic)cmbProperty.SelectedItem).Value;
                int clientId = (int)((dynamic)cmbClient.SelectedItem).Value;
                int employeeId = (int)((dynamic)cmbEmployee.SelectedItem).Value;
                DateTime transactionDate = dtpTransactionDate.Value;
                string notes = txtNotes.Text.Trim();

                using (var connection = _dbManager.GetConnection())
                {
                    // Start a transaction to ensure all operations succeed or fail together
                    using (var transaction = connection.BeginTransaction())
                    {
                        try
                        {
                            if (_selectedTransactionId == 0)
                            {
                                // Insert new transaction
                                using (var command = new SQLiteCommand(
                                    @"INSERT INTO Transactions 
                                    (PropertyId, ClientId, EmployeeId, TransactionType, Amount, Commission, TransactionDate, Notes)
                                    VALUES 
                                    (@PropertyId, @ClientId, @EmployeeId, @TransactionType, @Amount, @Commission, @TransactionDate, @Notes)",
                                    connection, transaction))
                                {
                                    command.Parameters.AddWithValue("@PropertyId", propertyId);
                                    command.Parameters.AddWithValue("@ClientId", clientId);
                                    command.Parameters.AddWithValue("@EmployeeId", employeeId);
                                    command.Parameters.AddWithValue("@TransactionType", (int)transactionType);
                                    command.Parameters.AddWithValue("@Amount", amount);
                                    command.Parameters.AddWithValue("@Commission", commission);
                                    command.Parameters.AddWithValue("@TransactionDate", transactionDate.ToString("yyyy-MM-dd HH:mm:ss"));
                                    command.Parameters.AddWithValue("@Notes", string.IsNullOrEmpty(notes) ? (object)DBNull.Value : notes);

                                    command.ExecuteNonQuery();
                                }

                                // Update property status
                                PropertyStatus newStatus =
                                    transactionType == TransactionType.Sale ? PropertyStatus.Sold : PropertyStatus.Rented;

                                using (var command = new SQLiteCommand(
                                    @"UPDATE Properties 
                                    SET Status = @Status
                                    WHERE Id = @Id", connection, transaction))
                                {
                                    command.Parameters.AddWithValue("@Status", (int)newStatus);
                                    command.Parameters.AddWithValue("@Id", propertyId);

                                    command.ExecuteNonQuery();
                                }

                                transaction.Commit();
                                MessageBox.Show("Əməliyyat uğurla əlavə edildi!", "Məlumat", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            }
                            else
                            {
                                // Get the current property ID for the transaction
                                int currentPropertyId = 0;
                                using (var command = new SQLiteCommand(
                                    "SELECT PropertyId FROM Transactions WHERE Id = @Id", connection, transaction))
                                {
                                    command.Parameters.AddWithValue("@Id", _selectedTransactionId);
                                    currentPropertyId = Convert.ToInt32(command.ExecuteScalar());
                                }

                                // Update transaction
                                using (var command = new SQLiteCommand(
                                    @"UPDATE Transactions
                                    SET PropertyId = @PropertyId, ClientId = @ClientId, EmployeeId = @EmployeeId,
                                        TransactionType = @TransactionType, Amount = @Amount, Commission = @Commission,
                                        TransactionDate = @TransactionDate, Notes = @Notes
                                    WHERE Id = @Id", connection, transaction))
                                {
                                    command.Parameters.AddWithValue("@PropertyId", propertyId);
                                    command.Parameters.AddWithValue("@ClientId", clientId);
                                    command.Parameters.AddWithValue("@EmployeeId", employeeId);
                                    command.Parameters.AddWithValue("@TransactionType", (int)transactionType);
                                    command.Parameters.AddWithValue("@Amount", amount);
                                    command.Parameters.AddWithValue("@Commission", commission);
                                    command.Parameters.AddWithValue("@TransactionDate", transactionDate.ToString("yyyy-MM-dd HH:mm:ss"));
                                    command.Parameters.AddWithValue("@Notes", string.IsNullOrEmpty(notes) ? (object)DBNull.Value : notes);
                                    command.Parameters.AddWithValue("@Id", _selectedTransactionId);

                                    command.ExecuteNonQuery();
                                }

                                // If property changed, update both properties' statuses
                                if (currentPropertyId != propertyId)
                                {
                                    // Reset old property status
                                    using (var command = new SQLiteCommand(
                                        @"UPDATE Properties 
                                        SET Status = @Status
                                        WHERE Id = @Id", connection, transaction))
                                    {
                                        command.Parameters.AddWithValue("@Status", (int)PropertyStatus.Available);
                                        command.Parameters.AddWithValue("@Id", currentPropertyId);

                                        command.ExecuteNonQuery();
                                    }

                                    // Set new property status
                                    PropertyStatus newStatus =
                                        transactionType == TransactionType.Sale ? PropertyStatus.Sold : PropertyStatus.Rented;

                                    using (var command = new SQLiteCommand(
                                        @"UPDATE Properties 
                                        SET Status = @Status
                                        WHERE Id = @Id", connection, transaction))
                                    {
                                        command.Parameters.AddWithValue("@Status", (int)newStatus);
                                        command.Parameters.AddWithValue("@Id", propertyId);

                                        command.ExecuteNonQuery();
                                    }
                                }

                                transaction.Commit();
                                MessageBox.Show("Əməliyyat uğurla yeniləndi!", "Məlumat", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            }
                        }
                        catch (Exception ex)
                        {
                            transaction.Rollback();
                            throw new Exception($"Əməliyyat baş tutmadı: {ex.Message}");
                        }
                    }
                }

                // Clear input fields
                ClearInputFields();

                // Refresh properties and transactions
                LoadProperties();
                RefreshPropertyComboBox();
                LoadTransactions();

                // Switch to transactions tab
                tabControl.SelectedTab = tabTransactions;
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Xəta baş verdi: {ex.Message}", "Xəta", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            ClearInputFields();

            // Switch to transactions tab
            tabControl.SelectedTab = tabTransactions;
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            if (_selectedTransactionId == 0)
            {
                return;
            }

            try
            {
                // Confirm deletion
                if (MessageBox.Show("Bu əməliyyatı silmək istədiyinizə əminsiniz?", "Təsdiq", MessageBoxButtons.YesNo, MessageBoxIcon.Question) != DialogResult.Yes)
                {
                    return;
                }

                using (var connection = _dbManager.GetConnection())
                {
                    // Start a transaction
                    using (var transaction = connection.BeginTransaction())
                    {
                        try
                        {
                            // Get the property ID for the transaction
                            int propertyId = 0;
                            using (var command = new SQLiteCommand(
                                "SELECT PropertyId FROM Transactions WHERE Id = @Id", connection, transaction))
                            {
                                command.Parameters.AddWithValue("@Id", _selectedTransactionId);
                                propertyId = Convert.ToInt32(command.ExecuteScalar());
                            }

                            // Delete transaction
                            using (var command = new SQLiteCommand(
                                "DELETE FROM Transactions WHERE Id = @Id", connection, transaction))
                            {
                                command.Parameters.AddWithValue("@Id", _selectedTransactionId);
                                command.ExecuteNonQuery();
                            }

                            // Reset property status to available
                            using (var command = new SQLiteCommand(
                                @"UPDATE Properties 
                                SET Status = @Status
                                WHERE Id = @Id", connection, transaction))
                            {
                                command.Parameters.AddWithValue("@Status", (int)PropertyStatus.Available);
                                command.Parameters.AddWithValue("@Id", propertyId);

                                command.ExecuteNonQuery();
                            }

                            transaction.Commit();
                        }
                        catch (Exception ex)
                        {
                            transaction.Rollback();
                            throw new Exception($"Əməliyyat silinmədi: {ex.Message}");
                        }
                    }
                }

                MessageBox.Show("Əməliyyat uğurla silindi!", "Məlumat", MessageBoxButtons.OK, MessageBoxIcon.Information);

                // Clear input fields
                ClearInputFields();

                // Refresh properties and transactions
                LoadProperties();
                RefreshPropertyComboBox();
                LoadTransactions();

                // Switch to transactions tab
                tabControl.SelectedTab = tabTransactions;
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Əməliyyatı silməyə çalışarkən xəta baş verdi: {ex.Message}", "Xəta", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void lvwTransactions_DoubleClick(object sender, EventArgs e)
        {
            if (lvwTransactions.SelectedItems.Count > 0)
            {
                int transactionId = ((Tuple<int, string>)lvwTransactions.SelectedItems[0].Tag).Item1;
                LoadTransactionDetails(transactionId);
            }
        }

        private void TabControl_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (tabControl.SelectedTab == tabTransactions)
            {
                LoadTransactions();
            }
        }

        private void lvwTransactions_MouseClick(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Right && lvwTransactions.SelectedItems.Count > 0)
            {
                contextMenuStrip1.Show(lvwTransactions, e.Location);
            }
        }

        private void viewDetailsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (lvwTransactions.SelectedItems.Count > 0)
            {
                var tag = (Tuple<int, string>)lvwTransactions.SelectedItems[0].Tag;
                int transactionId = tag.Item1;
                string notes = tag.Item2;

                StringBuilder detailText = new StringBuilder();
                detailText.AppendLine("Əməliyyat Detalları:");
                detailText.AppendLine("");
                detailText.AppendLine($"Tarix: {lvwTransactions.SelectedItems[0].SubItems[0].Text}");
                detailText.AppendLine($"Növü: {lvwTransactions.SelectedItems[0].SubItems[1].Text}");
                detailText.AppendLine($"Əmlak: {lvwTransactions.SelectedItems[0].SubItems[2].Text}");
                detailText.AppendLine($"Ünvan: {lvwTransactions.SelectedItems[0].SubItems[3].Text}");
                detailText.AppendLine($"Müştəri: {lvwTransactions.SelectedItems[0].SubItems[4].Text}");
                detailText.AppendLine($"İşçi: {lvwTransactions.SelectedItems[0].SubItems[5].Text}");
                detailText.AppendLine($"Məbləğ: {lvwTransactions.SelectedItems[0].SubItems[6].Text}");
                detailText.AppendLine($"Komissiya: {lvwTransactions.SelectedItems[0].SubItems[7].Text}");

                if (!string.IsNullOrEmpty(notes))
                {
                    detailText.AppendLine("");
                    detailText.AppendLine("Qeydlər:");
                    detailText.AppendLine(notes);
                }

                MessageBox.Show(detailText.ToString(), "Əməliyyat Detalları", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void editToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (lvwTransactions.SelectedItems.Count > 0)
            {
                int transactionId = ((Tuple<int, string>)lvwTransactions.SelectedItems[0].Tag).Item1;
                LoadTransactionDetails(transactionId);
            }
        }

        private void deleteToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (lvwTransactions.SelectedItems.Count > 0)
            {
                int transactionId = ((Tuple<int, string>)lvwTransactions.SelectedItems[0].Tag).Item1;
                _selectedTransactionId = transactionId;
                btnDelete_Click(null, null);
            }
        }

        private void btnSearchProperty_Click(object sender, EventArgs e)
        {
            string searchText = txtPropertySearch.Text.Trim();
            if (string.IsNullOrEmpty(searchText)) return;

            // Find matching property in the combo box
            for (int i = 0; i < cmbProperty.Items.Count; i++)
            {
                var item = (dynamic)cmbProperty.Items[i];
                if (item.Text.Contains(searchText))
                {
                    cmbProperty.SelectedIndex = i;
                    break;
                }
            }
        }

        private void btnSearchClient_Click(object sender, EventArgs e)
        {
            string searchText = txtClientSearch.Text.Trim();
            if (string.IsNullOrEmpty(searchText)) return;

            // Find matching client in the combo box
            for (int i = 0; i < cmbClient.Items.Count; i++)
            {
                var item = (dynamic)cmbClient.Items[i];
                if (item.Text.Contains(searchText))
                {
                    cmbClient.SelectedIndex = i;
                    break;
                }
            }
        }
    }
}
