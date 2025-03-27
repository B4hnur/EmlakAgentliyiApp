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
    public partial class EmployeeManagementForm : Form
    {
        private readonly DatabaseManager _dbManager;
        private int _selectedEmployeeId = 0;

        public EmployeeManagementForm(DatabaseManager dbManager)
        {
            InitializeComponent();

            _dbManager = dbManager;

            // Set up event handlers
            this.Load += EmployeeManagementForm_Load;
            tabControl.SelectedIndexChanged += TabControl_SelectedIndexChanged;
        }

        private void EmployeeManagementForm_Load(object sender, EventArgs e)
        {
            // Set the form title
            this.Text = "İşçilərin İdarəetməsi";

            // Load employees
            LoadEmployees();
        }

        private void LoadEmployees(string searchText = "")
        {
            try
            {
                lvwEmployees.Items.Clear();

                using (var connection = _dbManager.GetConnection())
                {
                    string query = @"SELECT Id, FullName, Phone, Email, Position, Salary, CommissionRate, HireDate, IsActive
                                   FROM Employees
                                   WHERE 1=1";

                    if (!string.IsNullOrEmpty(searchText))
                    {
                        query += @" AND (FullName LIKE @SearchText OR Phone LIKE @SearchText OR Email LIKE @SearchText OR Position LIKE @SearchText)";
                    }

                    query += " ORDER BY FullName";

                    using (var command = new SQLiteCommand(query, connection))
                    {
                        if (!string.IsNullOrEmpty(searchText))
                        {
                            command.Parameters.AddWithValue("@SearchText", $"%{searchText}%");
                        }

                        using (var reader = command.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                int id = reader.GetInt32(0);
                                string fullName = reader.GetString(1);
                                string phone = reader.GetString(2);
                                string email = reader.IsDBNull(3) ? "" : reader.GetString(3);
                                string position = reader.GetString(4);
                                double salary = reader.GetDouble(5);
                                double commissionRate = reader.GetDouble(6);
                                DateTime hireDate = DateTime.Parse(reader.GetString(7));
                                bool isActive = reader.GetInt32(8) == 1;

                                ListViewItem item = new ListViewItem(fullName);
                                item.SubItems.Add(phone);
                                item.SubItems.Add(position);
                                item.SubItems.Add(salary.ToString("N0") + " ₼");
                                item.SubItems.Add(commissionRate.ToString("0.##") + "%");
                                item.SubItems.Add(hireDate.ToString("dd.MM.yyyy"));
                                item.SubItems.Add(isActive ? "Aktiv" : "Deaktiv");

                                // Store the employee ID in the Tag property
                                item.Tag = id;

                                lvwEmployees.Items.Add(item);
                            }
                        }
                    }
                }

                // Update status
                lblStatus.Text = $"{lvwEmployees.Items.Count} işçi tapıldı";
            }
            catch (Exception ex)
            {
                MessageBox.Show($"İşçiləri yükləyərkən xəta baş verdi: {ex.Message}", "Xəta", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void ClearInputFields()
        {
            txtFullName.Text = "";
            txtPhone.Text = "";
            txtEmail.Text = "";
            txtPosition.Text = "";
            txtSalary.Text = "";
            txtCommissionRate.Text = "";
            dtpHireDate.Value = DateTime.Now;
            chkIsActive.Checked = true;
            _selectedEmployeeId = 0;

            // Update the button state
            btnSave.Text = "Əlavə Et";
            btnDelete.Enabled = false;
        }

        private void LoadEmployeeDetails(int employeeId)
        {
            try
            {
                using (var connection = _dbManager.GetConnection())
                {
                    using (var command = new SQLiteCommand(
                        @"SELECT FullName, Phone, Email, Position, Salary, CommissionRate, HireDate, IsActive 
                        FROM Employees 
                        WHERE Id = @Id", connection))
                    {
                        command.Parameters.AddWithValue("@Id", employeeId);

                        using (var reader = command.ExecuteReader())
                        {
                            if (reader.Read())
                            {
                                txtFullName.Text = reader.GetString(0);
                                txtPhone.Text = reader.GetString(1);
                                txtEmail.Text = reader.IsDBNull(2) ? "" : reader.GetString(2);
                                txtPosition.Text = reader.GetString(3);
                                txtSalary.Text = reader.GetDouble(4).ToString("0.##");
                                txtCommissionRate.Text = reader.GetDouble(5).ToString("0.##");
                                dtpHireDate.Value = DateTime.Parse(reader.GetString(6));
                                chkIsActive.Checked = reader.GetInt32(7) == 1;

                                // Store the selected ID
                                _selectedEmployeeId = employeeId;

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
                MessageBox.Show($"İşçi məlumatlarını yükləyərkən xəta baş verdi: {ex.Message}", "Xəta", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnSearch_Click(object sender, EventArgs e)
        {
            string searchText = txtSearch.Text.Trim();
            LoadEmployees(searchText);
        }

        private void btnClear_Click(object sender, EventArgs e)
        {
            txtSearch.Text = "";
            LoadEmployees();
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

                if (string.IsNullOrWhiteSpace(txtPosition.Text))
                {
                    MessageBox.Show("Vəzifə daxil edilməlidir!", "Xəta", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    txtPosition.Focus();
                    return;
                }

                // Validate numeric fields
                if (!double.TryParse(txtSalary.Text, out double salary) || salary < 0)
                {
                    MessageBox.Show("Əmək haqqı düzgün daxil edilməlidir!", "Xəta", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    txtSalary.Focus();
                    return;
                }

                if (!double.TryParse(txtCommissionRate.Text, out double commissionRate) || commissionRate < 0)
                {
                    MessageBox.Show("Komissiya faizi düzgün daxil edilməlidir!", "Xəta", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    txtCommissionRate.Focus();
                    return;
                }

                string fullName = txtFullName.Text.Trim();
                string phone = txtPhone.Text.Trim();
                string email = txtEmail.Text.Trim();
                string position = txtPosition.Text.Trim();
                bool isActive = chkIsActive.Checked;

                using (var connection = _dbManager.GetConnection())
                {
                    if (_selectedEmployeeId == 0)
                    {
                        // Insert new employee
                        using (var command = new SQLiteCommand(
                            @"INSERT INTO Employees (FullName, Phone, Email, Position, Salary, CommissionRate, HireDate, IsActive)
                            VALUES (@FullName, @Phone, @Email, @Position, @Salary, @CommissionRate, @HireDate, @IsActive)", connection))
                        {
                            command.Parameters.AddWithValue("@FullName", fullName);
                            command.Parameters.AddWithValue("@Phone", phone);
                            command.Parameters.AddWithValue("@Email", string.IsNullOrEmpty(email) ? (object)DBNull.Value : email);
                            command.Parameters.AddWithValue("@Position", position);
                            command.Parameters.AddWithValue("@Salary", salary);
                            command.Parameters.AddWithValue("@CommissionRate", commissionRate);
                            command.Parameters.AddWithValue("@HireDate", dtpHireDate.Value.ToString("yyyy-MM-dd"));
                            command.Parameters.AddWithValue("@IsActive", isActive ? 1 : 0);

                            command.ExecuteNonQuery();
                        }

                        MessageBox.Show("İşçi uğurla əlavə edildi!", "Məlumat", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                    else
                    {
                        // Update existing employee
                        using (var command = new SQLiteCommand(
                            @"UPDATE Employees
                            SET FullName = @FullName, Phone = @Phone, Email = @Email, Position = @Position, 
                                Salary = @Salary, CommissionRate = @CommissionRate, HireDate = @HireDate, IsActive = @IsActive
                            WHERE Id = @Id", connection))
                        {
                            command.Parameters.AddWithValue("@FullName", fullName);
                            command.Parameters.AddWithValue("@Phone", phone);
                            command.Parameters.AddWithValue("@Email", string.IsNullOrEmpty(email) ? (object)DBNull.Value : email);
                            command.Parameters.AddWithValue("@Position", position);
                            command.Parameters.AddWithValue("@Salary", salary);
                            command.Parameters.AddWithValue("@CommissionRate", commissionRate);
                            command.Parameters.AddWithValue("@HireDate", dtpHireDate.Value.ToString("yyyy-MM-dd"));
                            command.Parameters.AddWithValue("@IsActive", isActive ? 1 : 0);
                            command.Parameters.AddWithValue("@Id", _selectedEmployeeId);

                            command.ExecuteNonQuery();
                        }

                        MessageBox.Show("İşçi məlumatları uğurla yeniləndi!", "Məlumat", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                }

                // Clear input fields
                ClearInputFields();

                // Reload employees
                LoadEmployees();

                // Switch to employees tab
                tabControl.SelectedTab = tabEmployees;
            }
            catch (Exception ex)
            {
                MessageBox.Show($"İşçi məlumatlarını saxlayarkən xəta baş verdi: {ex.Message}", "Xəta", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            ClearInputFields();

            // Switch to employees tab
            tabControl.SelectedTab = tabEmployees;
        }

        private void btnNewEmployee_Click(object sender, EventArgs e)
        {
            ClearInputFields();

            // Switch to edit tab
            tabControl.SelectedTab = tabEdit;

            // Focus on name field
            txtFullName.Focus();
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            if (_selectedEmployeeId == 0)
            {
                return;
            }

            try
            {
                // Confirm deletion
                if (MessageBox.Show("Bu işçini silmək istədiyinizə əminsiniz?", "Təsdiq", MessageBoxButtons.YesNo, MessageBoxIcon.Question) != DialogResult.Yes)
                {
                    return;
                }

                // Check if employee has any transactions
                bool hasTransactions = false;

                using (var connection = _dbManager.GetConnection())
                {
                    using (var command = new SQLiteCommand(
                        "SELECT COUNT(*) FROM Transactions WHERE EmployeeId = @EmployeeId", connection))
                    {
                        command.Parameters.AddWithValue("@EmployeeId", _selectedEmployeeId);
                        int count = Convert.ToInt32(command.ExecuteScalar());

                        hasTransactions = count > 0;
                    }

                    // Also check for expenses
                    if (!hasTransactions)
                    {
                        using (var command = new SQLiteCommand(
                            "SELECT COUNT(*) FROM Expenses WHERE EmployeeId = @EmployeeId", connection))
                        {
                            command.Parameters.AddWithValue("@EmployeeId", _selectedEmployeeId);
                            int count = Convert.ToInt32(command.ExecuteScalar());

                            hasTransactions = count > 0;
                        }
                    }
                }

                if (hasTransactions)
                {
                    MessageBox.Show("Bu işçi ilə bağlı əməliyyatlar və ya xərclər var. Əvvəlcə bunları silməlisiniz.", "Xəta", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                // Delete employee
                using (var connection = _dbManager.GetConnection())
                {
                    using (var command = new SQLiteCommand(
                        "DELETE FROM Employees WHERE Id = @Id", connection))
                    {
                        command.Parameters.AddWithValue("@Id", _selectedEmployeeId);
                        command.ExecuteNonQuery();
                    }
                }

                MessageBox.Show("İşçi uğurla silindi!", "Məlumat", MessageBoxButtons.OK, MessageBoxIcon.Information);

                // Clear input fields
                ClearInputFields();

                // Reload employees
                LoadEmployees();

                // Switch to employees tab
                tabControl.SelectedTab = tabEmployees;
            }
            catch (Exception ex)
            {
                MessageBox.Show($"İşçini silməyə çalışarkən xəta baş verdi: {ex.Message}", "Xəta", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void lvwEmployees_DoubleClick(object sender, EventArgs e)
        {
            if (lvwEmployees.SelectedItems.Count > 0)
            {
                int employeeId = (int)lvwEmployees.SelectedItems[0].Tag;
                LoadEmployeeDetails(employeeId);
            }
        }

        private void TabControl_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (tabControl.SelectedTab == tabEmployees)
            {
                LoadEmployees();
            }
        }
    }
}
