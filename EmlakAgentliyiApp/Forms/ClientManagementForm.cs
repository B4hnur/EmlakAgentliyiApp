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

namespace DashinmazEmlakApp.Forms
{
    public partial class ClientManagementForm : Form
    {
        private readonly DatabaseManager _dbManager;
        private int _selectedClientId = 0;

        public ClientManagementForm(DatabaseManager dbManager)
        {
            InitializeComponent();

            _dbManager = dbManager;

            // Set up event handlers
            this.Load += ClientManagementForm_Load;
            tabControl.SelectedIndexChanged += TabControl_SelectedIndexChanged;
        }

        private void ClientManagementForm_Load(object sender, EventArgs e)
        {
            // Set the form title
            this.Text = "Müştəri İdarəetməsi";

            // Initialize the client type combo box
            InitializeClientTypeComboBox();

            // Load clients
            LoadClients();
        }

        private void InitializeClientTypeComboBox()
        {
            // Client type for new client
            cmbClientType.Items.Clear();
            cmbClientType.Items.Add(new { Text = "Alıcı", Value = ClientType.Buyer });
            cmbClientType.Items.Add(new { Text = "Satıcı", Value = ClientType.Seller });
            cmbClientType.Items.Add(new { Text = "Hər ikisi", Value = ClientType.Both });
            cmbClientType.DisplayMember = "Text";
            cmbClientType.ValueMember = "Value";
            cmbClientType.SelectedIndex = 0;

            // Client type for filter
            cmbFilterClientType.Items.Clear();
            cmbFilterClientType.Items.Add(new { Text = "Hamısı", Value = -1 });
            cmbFilterClientType.Items.Add(new { Text = "Alıcı", Value = (int)ClientType.Buyer });
            cmbFilterClientType.Items.Add(new { Text = "Satıcı", Value = (int)ClientType.Seller });
            cmbFilterClientType.Items.Add(new { Text = "Hər ikisi", Value = (int)ClientType.Both });
            cmbFilterClientType.DisplayMember = "Text";
            cmbFilterClientType.ValueMember = "Value";
            cmbFilterClientType.SelectedIndex = 0;
        }

        private void LoadClients(string searchText = "", int clientType = -1)
        {
            try
            {
                lvwClients.Items.Clear();

                using (var connection = _dbManager.GetConnection())
                {
                    string query = @"SELECT Id, FullName, Phone, Email, Address, ClientType, Notes, CreatedDate 
                                    FROM Clients
                                    WHERE 1=1";

                    if (!string.IsNullOrEmpty(searchText))
                    {
                        query += @" AND (FullName LIKE @SearchText OR Phone LIKE @SearchText OR Email LIKE @SearchText OR Address LIKE @SearchText)";
                    }

                    if (clientType >= 0)
                    {
                        query += @" AND ClientType = @ClientType";
                    }

                    query += " ORDER BY FullName";

                    using (var command = new SQLiteCommand(query, connection))
                    {
                        if (!string.IsNullOrEmpty(searchText))
                        {
                            command.Parameters.AddWithValue("@SearchText", $"%{searchText}%");
                        }

                        if (clientType >= 0)
                        {
                            command.Parameters.AddWithValue("@ClientType", clientType);
                        }

                        using (var reader = command.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                int id = reader.GetInt32(0);
                                string fullName = reader.GetString(1);
                                string phone = reader.GetString(2);
                                string email = reader.IsDBNull(3) ? "" : reader.GetString(3);
                                string address = reader.IsDBNull(4) ? "" : reader.GetString(4);
                                ClientType type = (ClientType)reader.GetInt32(5);
                                string typeText = GetClientTypeText(type);
                                string createdDate = DateTime.Parse(reader.GetString(7)).ToString("dd.MM.yyyy");

                                ListViewItem item = new ListViewItem(fullName);
                                item.SubItems.Add(phone);
                                item.SubItems.Add(email);
                                item.SubItems.Add(address);
                                item.SubItems.Add(typeText);
                                item.SubItems.Add(createdDate);

                                // Store the client ID in the Tag property
                                item.Tag = id;

                                lvwClients.Items.Add(item);
                            }
                        }
                    }
                }

                // Update status
                lblStatus.Text = $"{lvwClients.Items.Count} müştəri tapıldı";
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Müştəriləri yükləyərkən xəta baş verdi: {ex.Message}", "Xəta", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private string GetClientTypeText(ClientType type)
        {
            switch (type)
            {
                case ClientType.Buyer:
                    return "Alıcı";
                case ClientType.Seller:
                    return "Satıcı";
                case ClientType.Both:
                    return "Hər ikisi";
                default:
                    return "";
            }
        }

        private void ClearInputFields()
        {
            txtFullName.Text = "";
            txtPhone.Text = "";
            txtEmail.Text = "";
            txtAddress.Text = "";
            txtNotes.Text = "";
            cmbClientType.SelectedIndex = 0;
            _selectedClientId = 0;

            // Update the button state
            btnSave.Text = "Əlavə Et";
            btnDelete.Enabled = false;
        }

        private void LoadClientDetails(int clientId)
        {
            try
            {
                using (var connection = _dbManager.GetConnection())
                {
                    using (var command = new SQLiteCommand(
                        @"SELECT FullName, Phone, Email, Address, ClientType, Notes 
                        FROM Clients 
                        WHERE Id = @Id", connection))
                    {
                        command.Parameters.AddWithValue("@Id", clientId);

                        using (var reader = command.ExecuteReader())
                        {
                            if (reader.Read())
                            {
                                txtFullName.Text = reader.GetString(0);
                                txtPhone.Text = reader.GetString(1);
                                txtEmail.Text = reader.IsDBNull(2) ? "" : reader.GetString(2);
                                txtAddress.Text = reader.IsDBNull(3) ? "" : reader.GetString(3);

                                // Set client type combo box
                                ClientType type = (ClientType)reader.GetInt32(4);
                                for (int i = 0; i < cmbClientType.Items.Count; i++)
                                {
                                    if ((ClientType)((dynamic)cmbClientType.Items[i]).Value == type)
                                    {
                                        cmbClientType.SelectedIndex = i;
                                        break;
                                    }
                                }

                                txtNotes.Text = reader.IsDBNull(5) ? "" : reader.GetString(5);

                                // Store the selected ID
                                _selectedClientId = clientId;

                                // Update button state
                                btnSave.Text = "Yenilə";
                                btnDelete.Enabled = true;

                                // Switch to edit tab
                                tabControl.SelectedTab = tabEdit;
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Müştəri məlumatlarını yükləyərkən xəta baş verdi: {ex.Message}", "Xəta", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnSearch_Click(object sender, EventArgs e)
        {
            string searchText = txtSearch.Text.Trim();
            int clientType = ((dynamic)cmbFilterClientType.SelectedItem).Value;

            LoadClients(searchText, clientType);
        }

        private void btnClear_Click(object sender, EventArgs e)
        {
            txtSearch.Text = "";
            cmbFilterClientType.SelectedIndex = 0;

            LoadClients();
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            try
            {
                // Validate required fields
                if (string.IsNullOrWhiteSpace(txtFullName.Text))
                {
                    MessageBox.Show("Ad və soyad daxil edilməlidir!", "Xəta", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    txtFullName.Focus();
                    return;
                }

                if (string.IsNullOrWhiteSpace(txtPhone.Text))
                {
                    MessageBox.Show("Telefon nömrəsi daxil edilməlidir!", "Xəta", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    txtPhone.Focus();
                    return;
                }

                string fullName = txtFullName.Text.Trim();
                string phone = txtPhone.Text.Trim();
                string email = txtEmail.Text.Trim();
                string address = txtAddress.Text.Trim();
                ClientType clientType = (ClientType)((dynamic)cmbClientType.SelectedItem).Value;
                string notes = txtNotes.Text.Trim();

                using (var connection = _dbManager.GetConnection())
                {
                    if (_selectedClientId == 0)
                    {
                        // Insert new client
                        using (var command = new SQLiteCommand(
                            @"INSERT INTO Clients (FullName, Phone, Email, Address, ClientType, Notes, CreatedDate)
                            VALUES (@FullName, @Phone, @Email, @Address, @ClientType, @Notes, @CreatedDate)", connection))
                        {
                            command.Parameters.AddWithValue("@FullName", fullName);
                            command.Parameters.AddWithValue("@Phone", phone);
                            command.Parameters.AddWithValue("@Email", string.IsNullOrEmpty(email) ? (object)DBNull.Value : email);
                            command.Parameters.AddWithValue("@Address", string.IsNullOrEmpty(address) ? (object)DBNull.Value : address);
                            command.Parameters.AddWithValue("@ClientType", (int)clientType);
                            command.Parameters.AddWithValue("@Notes", string.IsNullOrEmpty(notes) ? (object)DBNull.Value : notes);
                            command.Parameters.AddWithValue("@CreatedDate", DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"));

                            command.ExecuteNonQuery();
                        }

                        MessageBox.Show("Müştəri uğurla əlavə edildi!", "Məlumat", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                    else
                    {
                        // Update existing client
                        using (var command = new SQLiteCommand(
                            @"UPDATE Clients
                            SET FullName = @FullName, Phone = @Phone, Email = @Email, Address = @Address, 
                                ClientType = @ClientType, Notes = @Notes
                            WHERE Id = @Id", connection))
                        {
                            command.Parameters.AddWithValue("@FullName", fullName);
                            command.Parameters.AddWithValue("@Phone", phone);
                            command.Parameters.AddWithValue("@Email", string.IsNullOrEmpty(email) ? (object)DBNull.Value : email);
                            command.Parameters.AddWithValue("@Address", string.IsNullOrEmpty(address) ? (object)DBNull.Value : address);
                            command.Parameters.AddWithValue("@ClientType", (int)clientType);
                            command.Parameters.AddWithValue("@Notes", string.IsNullOrEmpty(notes) ? (object)DBNull.Value : notes);
                            command.Parameters.AddWithValue("@Id", _selectedClientId);

                            command.ExecuteNonQuery();
                        }

                        MessageBox.Show("Müştəri məlumatları uğurla yeniləndi!", "Məlumat", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                }

                // Clear input fields
                ClearInputFields();

                // Reload clients
                LoadClients();

                // Switch to clients tab
                tabControl.SelectedTab = tabClients;
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Müştəri məlumatlarını saxlayarkən xəta baş verdi: {ex.Message}", "Xəta", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            ClearInputFields();

            // Switch to clients tab
            tabControl.SelectedTab = tabClients;
        }

        private void btnNewClient_Click(object sender, EventArgs e)
        {
            ClearInputFields();

            // Switch to edit tab
            tabControl.SelectedTab = tabEdit;

            // Focus on name field
            txtFullName.Focus();
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            if (_selectedClientId == 0)
            {
                return;
            }

            try
            {
                // Confirm deletion
                if (MessageBox.Show("Bu müştərini silmək istədiyinizə əminsiniz?", "Təsdiq", MessageBoxButtons.YesNo, MessageBoxIcon.Question) != DialogResult.Yes)
                {
                    return;
                }

                // Check if client has any transactions
                bool hasTransactions = false;

                using (var connection = _dbManager.GetConnection())
                {
                    using (var command = new SQLiteCommand(
                        "SELECT COUNT(*) FROM Transactions WHERE ClientId = @ClientId", connection))
                    {
                        command.Parameters.AddWithValue("@ClientId", _selectedClientId);
                        int count = Convert.ToInt32(command.ExecuteScalar());

                        hasTransactions = count > 0;
                    }
                }

                if (hasTransactions)
                {
                    MessageBox.Show("Bu müştəri ilə bağlı əməliyyatlar var. Əvvəlcə əməliyyatları silməlisiniz.", "Xəta", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                // Delete client
                using (var connection = _dbManager.GetConnection())
                {
                    using (var command = new SQLiteCommand(
                        "DELETE FROM Clients WHERE Id = @Id", connection))
                    {
                        command.Parameters.AddWithValue("@Id", _selectedClientId);
                        command.ExecuteNonQuery();
                    }
                }

                MessageBox.Show("Müştəri uğurla silindi!", "Məlumat", MessageBoxButtons.OK, MessageBoxIcon.Information);

                // Clear input fields
                ClearInputFields();

                // Reload clients
                LoadClients();

                // Switch to clients tab
                tabControl.SelectedTab = tabClients;
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Müştərini silməyə çalışarkən xəta baş verdi: {ex.Message}", "Xəta", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void lvwClients_DoubleClick(object sender, EventArgs e)
        {
            if (lvwClients.SelectedItems.Count > 0)
            {
                int clientId = (int)lvwClients.SelectedItems[0].Tag;
                LoadClientDetails(clientId);
            }
        }

        private void TabControl_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (tabControl.SelectedTab == tabClients)
            {
                LoadClients();
            }
        }

        private void lvwClients_SelectedIndexChanged(object sender, EventArgs e)
        {

        }
    }
}
