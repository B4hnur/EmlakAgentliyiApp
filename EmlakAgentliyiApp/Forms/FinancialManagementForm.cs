using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using DashinmazEmlakApp.Models;
using DashinmazEmlakApp.Services;
using DashinmazEmlakApp.Utilities;
using DashinmazEmlakApp.Database;

namespace DashinmazEmlakApp.Forms
{
    public partial class FinancialManagementForm : Form
    {
        private readonly DatabaseService _databaseService;
        private readonly FinancialCalculationService _financialService;
        private List<Transaction> _transactions;
        private List<Expense> _expenses;
        private List<Employee> _employees;
        private List<Client> _clients;
        private List<Property> _properties;
        private Transaction _selectedTransaction;
        private Expense _selectedExpense;
        private bool _isAddingNewTransaction;
        private bool _isAddingNewExpense;

        public FinancialManagementForm()
        {
            InitializeComponent();
            _databaseService = new DatabaseService();
            _financialService = new FinancialCalculationService();
            _transactions = new List<Transaction>();
            _expenses = new List<Expense>();
            _employees = new List<Employee>();
            _clients = new List<Client>();
            _properties = new List<Property>();
        }

        private async void FinancialManagementForm_Load(object sender, EventArgs e)
        {
            tabControl.SelectedIndex = 0;

            // Status və progress bar göstər
            lblStatus.Text = "Məlumatlar yüklənir...";
            progressBar.Visible = true;

            // Əsas məlumatları yüklə
            await LoadBasicData();

            // Tab seçiminə görə lazımi məlumatları yüklə
            await LoadDataForSelectedTab();

            // Status və progress bar gizlət
            lblStatus.Text = "Məlumatlar yükləndi.";
            progressBar.Visible = false;
        }

        private async Task LoadBasicData()
        {
            try
            {
                // İşçiləri yüklə
                _employees = await Task.Run(() => _databaseService.GetAllEmployees());

                // Müştəriləri yüklə
                _clients = await Task.Run(() => _databaseService.GetAllClients());

                // Əmlakları yüklə
                _properties = await Task.Run(() => _databaseService.GetAllProperties());

                // ComboBox-ları doldur
                InitializeComboBoxes();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Əsas məlumatları yükləyərkən xəta baş verdi: {ex.Message}", "Xəta",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void InitializeComboBoxes()
        {
            // Əməliyyat yaratma komboboxları
            cmbTransactionType.Items.Clear();
            cmbTransactionType.Items.AddRange(new string[] { "Satış", "Kirayə", "Deposit", "Komissiya", "Digər" });
            cmbTransactionType.SelectedIndex = 0;

            cmbPaymentMethod.Items.Clear();
            cmbPaymentMethod.Items.AddRange(new string[] { "Nağd", "Bank köçürməsi", "Kredit kartı", "Digər" });
            cmbPaymentMethod.SelectedIndex = 0;

            // Xərc yaratma komboboxları
            cmbExpenseCategory.Items.Clear();
            cmbExpenseCategory.Items.AddRange(Expense.GetExpenseCategories());
            cmbExpenseCategory.SelectedIndex = 0;

            cmbExpensePaymentMethod.Items.Clear();
            cmbExpensePaymentMethod.Items.AddRange(new string[] { "Nağd", "Bank köçürməsi", "Kredit kartı", "Digər" });
            cmbExpensePaymentMethod.SelectedIndex = 0;

            // İşçi komboboxlarını doldur
            cmbEmployee.Items.Clear();
            cmbEmployee.DisplayMember = "FullName";
            cmbEmployee.ValueMember = "Id";
            cmbEmployee.DataSource = new List<Employee>(_employees);

            cmbExpenseEmployee.Items.Clear();
            cmbExpenseEmployee.DisplayMember = "FullName";
            cmbExpenseEmployee.ValueMember = "Id";
            cmbExpenseEmployee.DataSource = new List<Employee>(_employees);

            // Müştəri komboboxunu doldur
            cmbClient.Items.Clear();
            cmbClient.DisplayMember = "FullName";
            cmbClient.ValueMember = "Id";
            cmbClient.DataSource = new List<Client>(_clients);

            // Əmlak komboboxunu doldur
            cmbProperty.Items.Clear();
            cmbProperty.DisplayMember = "Title";
            cmbProperty.ValueMember = "Id";
            cmbProperty.DataSource = new List<Property>(_properties);
        }

        private async Task LoadDataForSelectedTab()
        {
            if (tabControl.SelectedTab == tabTransactions)
            {
                await LoadTransactions();
            }
            else if (tabControl.SelectedTab == tabExpenses)
            {
                await LoadExpenses();
            }
            else if (tabControl.SelectedTab == tabSummary)
            {
                await LoadFinancialSummary();
            }
        }

        private async void tabControl_SelectedIndexChanged(object sender, EventArgs e)
        {
            lblStatus.Text = "Məlumatlar yüklənir...";
            progressBar.Visible = true;

            await LoadDataForSelectedTab();

            lblStatus.Text = "Məlumatlar yükləndi.";
            progressBar.Visible = false;
        }

        #region Transaction Tab İşləmləri

        private async Task LoadTransactions()
        {
            try
            {
                _transactions = await Task.Run(() => _databaseService.GetAllTransactions());

                // ListView'i doldur
                lvTransactions.Items.Clear();

                foreach (var transaction in _transactions)
                {
                    var item = new ListViewItem(transaction.TransactionDate.ToShortDateString());
                    item.SubItems.Add(transaction.TransactionType);

                    // Uyğun işçi və müştəri adlarını tap
                    string employeeName = "-";
                    var employee = _employees.FirstOrDefault(e => e.Id == transaction.EmployeeId);
                    if (employee != null)
                    {
                        employeeName = employee.FullName;
                    }

                    string clientName = "-";
                    var client = _clients.FirstOrDefault(c => c.Id == transaction.ClientId);
                    if (client != null)
                    {
                        clientName = client.FullName;
                    }

                    string propertyTitle = "-";
                    var property = _properties.FirstOrDefault(p => p.Id == transaction.PropertyId);
                    if (property != null)
                    {
                        propertyTitle = property.Title;
                    }

                    item.SubItems.Add(employeeName);
                    item.SubItems.Add(clientName);
                    item.SubItems.Add(propertyTitle);
                    item.SubItems.Add(transaction.Amount.ToString("C"));
                    item.SubItems.Add(transaction.CommissionAmount.ToString("C"));
                    item.SubItems.Add(transaction.PaymentMethod);
                    item.SubItems.Add(transaction.IsSuccessful ? "Tamamlanıb" : "Gözləmədə");

                    item.Tag = transaction;
                    lvTransactions.Items.Add(item);
                }

                // Sütunların ölçülərini avtomatik tənzimlə
                lvTransactions.AutoResizeColumns(ColumnHeaderAutoResizeStyle.HeaderSize);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Əməliyyatları yükləyərkən xəta baş verdi: {ex.Message}", "Xəta",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void lvTransactions_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (lvTransactions.SelectedItems.Count > 0 && !_isAddingNewTransaction)
            {
                _selectedTransaction = lvTransactions.SelectedItems[0].Tag as Transaction;
                if (_selectedTransaction != null)
                {
                    DisplayTransactionDetails(_selectedTransaction);
                    EnableTransactionButtons();
                }
            }
            else if (_isAddingNewTransaction)
            {
                // Seçim qadağandır - yeni əməliyyat əlavə edilir
                if (lvTransactions.SelectedItems.Count > 0)
                {
                    lvTransactions.SelectedItems[0].Selected = false;
                }
            }
            else
            {
                DisableTransactionDetails();
                DisableTransactionButtons();
            }
        }

        private void DisplayTransactionDetails(Transaction transaction)
        {
            // Transaction details form control values
            dtpTransactionDate.Value = transaction.TransactionDate;
            cmbTransactionType.SelectedItem = transaction.TransactionType;
            txtAmount.Text = transaction.Amount.ToString();
            txtCommissionAmount.Text = transaction.CommissionAmount.ToString();
            cmbPaymentMethod.SelectedItem = transaction.PaymentMethod;
            txtReferenceNumber.Text = transaction.ReferenceNumber;
            rtbTransactionDescription.Text = transaction.Description;
            chkIsSuccessful.Checked = transaction.IsSuccessful;

            // Find and select the associated entities
            SelectEntityInComboBox(cmbEmployee, transaction.EmployeeId);
            SelectEntityInComboBox(cmbClient, transaction.ClientId);
            SelectEntityInComboBox(cmbProperty, transaction.PropertyId);

            // Disable editing
            DisableTransactionDetails();
        }

        private void SelectEntityInComboBox(ComboBox comboBox, int id)
        {
            if (comboBox.DataSource != null)
            {
                // Try to find the item with the matching ID
                int index = -1;

                for (int i = 0; i < comboBox.Items.Count; i++)
                {
                    dynamic item = comboBox.Items[i];
                    if (item.Id == id)
                    {
                        index = i;
                        break;
                    }
                }

                if (index >= 0)
                {
                    comboBox.SelectedIndex = index;
                }
            }
        }

        private void btnAddTransaction_Click(object sender, EventArgs e)
        {
            _isAddingNewTransaction = true;
            _selectedTransaction = new Transaction();

            // Formanı təmizlə
            ClearTransactionForm();

            // Redaktə etməyə icazə ver
            EnableTransactionDetails();

            // Buttonları yenilə
            btnSaveTransaction.Visible = true;
            btnCancelTransaction.Visible = true;
            btnEditTransaction.Visible = false;
            btnAddTransaction.Visible = false;
            btnDeleteTransaction.Visible = false;

            // Liste seçimi silmək
            if (lvTransactions.SelectedItems.Count > 0)
            {
                lvTransactions.SelectedItems[0].Selected = false;
            }
        }

        private void btnEditTransaction_Click(object sender, EventArgs e)
        {
            if (_selectedTransaction != null)
            {
                EnableTransactionDetails();

                // Buttonları yenilə
                btnSaveTransaction.Visible = true;
                btnCancelTransaction.Visible = true;
                btnEditTransaction.Visible = false;
                btnAddTransaction.Visible = false;
                btnDeleteTransaction.Visible = false;
            }
        }

        private async void btnSaveTransaction_Click(object sender, EventArgs e)
        {
            if (!ValidateTransactionForm())
            {
                return;
            }

            try
            {
                // Məlumatları əldə et
                _selectedTransaction.TransactionDate = dtpTransactionDate.Value;
                _selectedTransaction.TransactionType = cmbTransactionType.SelectedItem.ToString();
                _selectedTransaction.Amount = decimal.Parse(txtAmount.Text);
                _selectedTransaction.CommissionAmount = decimal.Parse(txtCommissionAmount.Text);
                _selectedTransaction.PaymentMethod = cmbPaymentMethod.SelectedItem.ToString();
                _selectedTransaction.ReferenceNumber = txtReferenceNumber.Text;
                _selectedTransaction.Description = rtbTransactionDescription.Text;
                _selectedTransaction.IsSuccessful = chkIsSuccessful.Checked;

                // Əlaqəli obyektləri təyin et
                if (cmbEmployee.SelectedItem is Employee employee)
                {
                    _selectedTransaction.EmployeeId = employee.Id;
                }

                if (cmbClient.SelectedItem is Client client)
                {
                    _selectedTransaction.ClientId = client.Id;
                }

                if (cmbProperty.SelectedItem is Property property)
                {
                    _selectedTransaction.PropertyId = property.Id;
                }

                // Yadda saxla
                int transactionId = await Task.Run(() => _databaseService.SaveTransaction(_selectedTransaction));

                if (transactionId > 0)
                {
                    _selectedTransaction.Id = transactionId;

                    // Yeni əlavə edilirdisə, siyahıya əlavə et
                    if (_isAddingNewTransaction)
                    {
                        _transactions.Add(_selectedTransaction);
                        _isAddingNewTransaction = false;
                    }

                    // Əməliyyatları yenilə
                    await LoadTransactions();

                    MessageBox.Show("Əməliyyat uğurla yadda saxlanıldı!", "Uğurlu",
                        MessageBoxButtons.OK, MessageBoxIcon.Information);

                    // Buttonları yenilə
                    btnSaveTransaction.Visible = false;
                    btnCancelTransaction.Visible = false;
                    btnEditTransaction.Visible = true;
                    btnAddTransaction.Visible = true;
                    btnDeleteTransaction.Visible = true;

                    DisableTransactionDetails();
                }
                else
                {
                    MessageBox.Show("Əməliyyatı yadda saxlayarkən xəta baş verdi!", "Xəta",
                        MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Əməliyyatı yadda saxlayarkən xəta baş verdi: {ex.Message}", "Xəta",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnCancelTransaction_Click(object sender, EventArgs e)
        {
            // Redaktə etməyə qadağa qoy
            DisableTransactionDetails();

            // Əvvəlki vəziyyəti bərpa et
            if (_isAddingNewTransaction)
            {
                _isAddingNewTransaction = false;
                ClearTransactionForm();
            }
            else if (_selectedTransaction != null)
            {
                DisplayTransactionDetails(_selectedTransaction);
            }

            // Buttonları yenilə
            btnSaveTransaction.Visible = false;
            btnCancelTransaction.Visible = false;
            btnEditTransaction.Visible = true;
            btnAddTransaction.Visible = true;
            btnDeleteTransaction.Visible = true;
        }

        private async void btnDeleteTransaction_Click(object sender, EventArgs e)
        {
            if (_selectedTransaction != null)
            {
                if (MessageBox.Show("Bu əməliyyatı silmək istədiyinizə əminsiniz?", "Təsdiqləmə",
                    MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                {
                    try
                    {
                        // Əməliyyatı uğursuz kimi işarələ (tam silmək əvəzinə status dəyişdirilir)
                        _selectedTransaction.IsSuccessful = false;

                        bool success = await Task.Run(() => _databaseService.SaveTransaction(_selectedTransaction) > 0);

                        if (success)
                        {
                            // Əməliyyatları yenilə
                            await LoadTransactions();

                            MessageBox.Show("Əməliyyat uğurla silindi!", "Uğurlu",
                                MessageBoxButtons.OK, MessageBoxIcon.Information);

                            ClearTransactionForm();
                            DisableTransactionDetails();
                            DisableTransactionButtons();
                        }
                        else
                        {
                            MessageBox.Show("Əməliyyatı silərkən xəta baş verdi!", "Xəta",
                                MessageBoxButtons.OK, MessageBoxIcon.Error);
                        }
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show($"Əməliyyatı silərkən xəta baş verdi: {ex.Message}", "Xəta",
                            MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }
        }

        private bool ValidateTransactionForm()
        {
            // Məbləğ yoxlaması
            if (string.IsNullOrWhiteSpace(txtAmount.Text) || !decimal.TryParse(txtAmount.Text, out _))
            {
                MessageBox.Show("Xahiş olunur düzgün məbləğ daxil edin!", "Xəbərdarlıq",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtAmount.Focus();
                return false;
            }

            // Komissiya məbləği yoxlaması
            if (string.IsNullOrWhiteSpace(txtCommissionAmount.Text) || !decimal.TryParse(txtCommissionAmount.Text, out _))
            {
                MessageBox.Show("Xahiş olunur düzgün komissiya məbləği daxil edin!", "Xəbərdarlıq",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtCommissionAmount.Focus();
                return false;
            }

            // Əməliyyat növü yoxlaması
            if (cmbTransactionType.SelectedIndex < 0)
            {
                MessageBox.Show("Xahiş olunur əməliyyat növünü seçin!", "Xəbərdarlıq",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                cmbTransactionType.Focus();
                return false;
            }

            // İşçi yoxlaması
            if (cmbEmployee.SelectedIndex < 0)
            {
                MessageBox.Show("Xahiş olunur işçini seçin!", "Xəbərdarlıq",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                cmbEmployee.Focus();
                return false;
            }

            return true;
        }

        private void ClearTransactionForm()
        {
            dtpTransactionDate.Value = DateTime.Now;
            if (cmbTransactionType.Items.Count > 0) cmbTransactionType.SelectedIndex = 0;
            txtAmount.Text = "0";
            txtCommissionAmount.Text = "0";
            if (cmbPaymentMethod.Items.Count > 0) cmbPaymentMethod.SelectedIndex = 0;
            txtReferenceNumber.Text = string.Empty;
            rtbTransactionDescription.Text = string.Empty;
            chkIsSuccessful.Checked = false;

            if (cmbEmployee.Items.Count > 0) cmbEmployee.SelectedIndex = 0;
            if (cmbClient.Items.Count > 0) cmbClient.SelectedIndex = 0;
            if (cmbProperty.Items.Count > 0) cmbProperty.SelectedIndex = 0;
        }

        private void EnableTransactionDetails()
        {
            dtpTransactionDate.Enabled = true;
            cmbTransactionType.Enabled = true;
            txtAmount.ReadOnly = false;
            txtCommissionAmount.ReadOnly = false;
            cmbPaymentMethod.Enabled = true;
            txtReferenceNumber.ReadOnly = false;
            rtbTransactionDescription.ReadOnly = false;
            chkIsSuccessful.Enabled = true;

            cmbEmployee.Enabled = true;
            cmbClient.Enabled = true;
            cmbProperty.Enabled = true;
        }

        private void DisableTransactionDetails()
        {
            dtpTransactionDate.Enabled = false;
            cmbTransactionType.Enabled = false;
            txtAmount.ReadOnly = true;
            txtCommissionAmount.ReadOnly = true;
            cmbPaymentMethod.Enabled = false;
            txtReferenceNumber.ReadOnly = true;
            rtbTransactionDescription.ReadOnly = true;
            chkIsSuccessful.Enabled = false;

            cmbEmployee.Enabled = false;
            cmbClient.Enabled = false;
            cmbProperty.Enabled = false;
        }

        private void EnableTransactionButtons()
        {
            btnEditTransaction.Enabled = true;
            btnDeleteTransaction.Enabled = true;
        }

        private void DisableTransactionButtons()
        {
            btnEditTransaction.Enabled = false;
            btnDeleteTransaction.Enabled = false;
        }

        #endregion

        #region Expense Tab İşləmləri

        private async Task LoadExpenses()
        {
            try
            {
                _expenses = await Task.Run(() => _databaseService.GetAllExpenses());

                // ListView'i doldur
                lvExpenses.Items.Clear();

                foreach (var expense in _expenses)
                {
                    var item = new ListViewItem(expense.ExpenseDate.ToShortDateString());
                    item.SubItems.Add(expense.Title);
                    item.SubItems.Add(expense.Category);
                    item.SubItems.Add(expense.Amount.ToString("C"));

                    // Uyğun işçi adını tap
                    string employeeName = "-";
                    if (expense.EmployeeId.HasValue)
                    {
                        var employee = _employees.FirstOrDefault(e => e.Id == expense.EmployeeId.Value);
                        if (employee != null)
                        {
                            employeeName = employee.FullName;
                        }
                    }

                    item.SubItems.Add(employeeName);
                    item.SubItems.Add(expense.IsPaid ? "Ödənilib" : "Ödənilməyib");
                    item.SubItems.Add(expense.PaymentMethod);

                    item.Tag = expense;
                    lvExpenses.Items.Add(item);
                }

                // Sütunların ölçülərini avtomatik tənzimlə
                lvExpenses.AutoResizeColumns(ColumnHeaderAutoResizeStyle.HeaderSize);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Xərcləri yükləyərkən xəta baş verdi: {ex.Message}", "Xəta",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void lvExpenses_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (lvExpenses.SelectedItems.Count > 0 && !_isAddingNewExpense)
            {
                _selectedExpense = lvExpenses.SelectedItems[0].Tag as Expense;
                if (_selectedExpense != null)
                {
                    DisplayExpenseDetails(_selectedExpense);
                    EnableExpenseButtons();
                }
            }
            else if (_isAddingNewExpense)
            {
                // Seçim qadağandır - yeni xərc əlavə edilir
                if (lvExpenses.SelectedItems.Count > 0)
                {
                    lvExpenses.SelectedItems[0].Selected = false;
                }
            }
            else
            {
                DisableExpenseDetails();
                DisableExpenseButtons();
            }
        }

        private void DisplayExpenseDetails(Expense expense)
        {
            // Expense details form control values
            txtExpenseTitle.Text = expense.Title;

            // Find category in the combo box
            if (expense.Category != null)
            {
                for (int i = 0; i < cmbExpenseCategory.Items.Count; i++)
                {
                    if (cmbExpenseCategory.Items[i].ToString() == expense.Category)
                    {
                        cmbExpenseCategory.SelectedIndex = i;
                        break;
                    }
                }
            }

            dtpExpenseDate.Value = expense.ExpenseDate;
            txtExpenseAmount.Text = expense.Amount.ToString();
            rtbExpenseDescription.Text = expense.Description;

            // Find payment method in the combo box
            if (expense.PaymentMethod != null)
            {
                for (int i = 0; i < cmbExpensePaymentMethod.Items.Count; i++)
                {
                    if (cmbExpensePaymentMethod.Items[i].ToString() == expense.PaymentMethod)
                    {
                        cmbExpensePaymentMethod.SelectedIndex = i;
                        break;
                    }
                }
            }

            txtExpenseReference.Text = expense.ReferenceNumber;
            chkExpenseIsPaid.Checked = expense.IsPaid;

            if (expense.PaymentDate.HasValue)
            {
                dtpExpensePaymentDate.Value = expense.PaymentDate.Value;
            }
            else
            {
                dtpExpensePaymentDate.Value = DateTime.Now;
            }

            // Select employee if assigned
            if (expense.EmployeeId.HasValue)
            {
                SelectEntityInComboBox(cmbExpenseEmployee, expense.EmployeeId.Value);
            }
            else
            {
                cmbExpenseEmployee.SelectedIndex = -1;
            }

            // Disable editing
            DisableExpenseDetails();
        }

        private void btnAddExpense_Click(object sender, EventArgs e)
        {
            _isAddingNewExpense = true;
            _selectedExpense = new Expense();

            // Formanı təmizlə
            ClearExpenseForm();

            // Redaktə etməyə icazə ver
            EnableExpenseDetails();

            // Buttonları yenilə
            btnSaveExpense.Visible = true;
            btnCancelExpense.Visible = true;
            btnEditExpense.Visible = false;
            btnAddExpense.Visible = false;
            btnDeleteExpense.Visible = false;

            // Liste seçimi silmək
            if (lvExpenses.SelectedItems.Count > 0)
            {
                lvExpenses.SelectedItems[0].Selected = false;
            }
        }

        private void btnEditExpense_Click(object sender, EventArgs e)
        {
            if (_selectedExpense != null)
            {
                EnableExpenseDetails();

                // Buttonları yenilə
                btnSaveExpense.Visible = true;
                btnCancelExpense.Visible = true;
                btnEditExpense.Visible = false;
                btnAddExpense.Visible = false;
                btnDeleteExpense.Visible = false;
            }
        }

        private async void btnSaveExpense_Click(object sender, EventArgs e)
        {
            if (!ValidateExpenseForm())
            {
                return;
            }

            try
            {
                // Məlumatları əldə et
                _selectedExpense.Title = txtExpenseTitle.Text;
                _selectedExpense.Category = cmbExpenseCategory.SelectedItem.ToString();
                _selectedExpense.ExpenseDate = dtpExpenseDate.Value;
                _selectedExpense.Amount = decimal.Parse(txtExpenseAmount.Text);
                _selectedExpense.Description = rtbExpenseDescription.Text;
                _selectedExpense.PaymentMethod = cmbExpensePaymentMethod.SelectedItem.ToString();
                _selectedExpense.ReferenceNumber = txtExpenseReference.Text;
                _selectedExpense.IsPaid = chkExpenseIsPaid.Checked;

                if (chkExpenseIsPaid.Checked)
                {
                    _selectedExpense.PaymentDate = dtpExpensePaymentDate.Value;
                }
                else
                {
                    _selectedExpense.PaymentDate = null;
                }

                // Əlaqəli işçini təyin et
                if (cmbExpenseEmployee.SelectedItem is Employee employee)
                {
                    _selectedExpense.EmployeeId = employee.Id;
                }
                else
                {
                    _selectedExpense.EmployeeId = null;
                }

                // Yadda saxla
                int expenseId = await Task.Run(() => _databaseService.SaveExpense(_selectedExpense));

                if (expenseId > 0)
                {
                    _selectedExpense.Id = expenseId;

                    // Yeni əlavə edilirdisə, siyahıya əlavə et
                    if (_isAddingNewExpense)
                    {
                        _expenses.Add(_selectedExpense);
                        _isAddingNewExpense = false;
                    }

                    // Xərcləri yenilə
                    await LoadExpenses();

                    MessageBox.Show("Xərc uğurla yadda saxlanıldı!", "Uğurlu",
                        MessageBoxButtons.OK, MessageBoxIcon.Information);

                    // Buttonları yenilə
                    btnSaveExpense.Visible = false;
                    btnCancelExpense.Visible = false;
                    btnEditExpense.Visible = true;
                    btnAddExpense.Visible = true;
                    btnDeleteExpense.Visible = true;

                    DisableExpenseDetails();
                }
                else
                {
                    MessageBox.Show("Xərci yadda saxlayarkən xəta baş verdi!", "Xəta",
                        MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Xərci yadda saxlayarkən xəta baş verdi: {ex.Message}", "Xəta",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnCancelExpense_Click(object sender, EventArgs e)
        {
            // Redaktə etməyə qadağa qoy
            DisableExpenseDetails();

            // Əvvəlki vəziyyəti bərpa et
            if (_isAddingNewExpense)
            {
                _isAddingNewExpense = false;
                ClearExpenseForm();
            }
            else if (_selectedExpense != null)
            {
                DisplayExpenseDetails(_selectedExpense);
            }

            // Buttonları yenilə
            btnSaveExpense.Visible = false;
            btnCancelExpense.Visible = false;
            btnEditExpense.Visible = true;
            btnAddExpense.Visible = true;
            btnDeleteExpense.Visible = true;
        }

        private async void btnDeleteExpense_Click(object sender, EventArgs e)
        {
            if (_selectedExpense != null)
            {
                if (MessageBox.Show("Bu xərci silmək istədiyinizə əminsiniz?", "Təsdiqləmə",
                    MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                {
                    try
                    {
                        // Xərci ödənilməmiş kimi işarələ (tam silmək əvəzinə status dəyişdirilir)
                        _selectedExpense.IsPaid = false;

                        bool success = await Task.Run(() => _databaseService.SaveExpense(_selectedExpense) > 0);

                        if (success)
                        {
                            // Xərcləri yenilə
                            await LoadExpenses();

                            MessageBox.Show("Xərc uğurla silindi!", "Uğurlu",
                                MessageBoxButtons.OK, MessageBoxIcon.Information);

                            ClearExpenseForm();
                            DisableExpenseDetails();
                            DisableExpenseButtons();
                        }
                        else
                        {
                            MessageBox.Show("Xərci silərkən xəta baş verdi!", "Xəta",
                                MessageBoxButtons.OK, MessageBoxIcon.Error);
                        }
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show($"Xərci silərkən xəta baş verdi: {ex.Message}", "Xəta",
                            MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }
        }

        private bool ValidateExpenseForm()
        {
            // Başlıq yoxlaması
            if (string.IsNullOrWhiteSpace(txtExpenseTitle.Text))
            {
                MessageBox.Show("Xahiş olunur xərcin adını daxil edin!", "Xəbərdarlıq",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtExpenseTitle.Focus();
                return false;
            }

            // Məbləğ yoxlaması
            if (string.IsNullOrWhiteSpace(txtExpenseAmount.Text) || !decimal.TryParse(txtExpenseAmount.Text, out _))
            {
                MessageBox.Show("Xahiş olunur düzgün məbləğ daxil edin!", "Xəbərdarlıq",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtExpenseAmount.Focus();
                return false;
            }

            // Kateqoriya yoxlaması
            if (cmbExpenseCategory.SelectedIndex < 0)
            {
                MessageBox.Show("Xahiş olunur xərc kateqoriyasını seçin!", "Xəbərdarlıq",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                cmbExpenseCategory.Focus();
                return false;
            }

            return true;
        }

        private void ClearExpenseForm()
        {
            txtExpenseTitle.Text = string.Empty;
            if (cmbExpenseCategory.Items.Count > 0) cmbExpenseCategory.SelectedIndex = 0;
            dtpExpenseDate.Value = DateTime.Now;
            txtExpenseAmount.Text = "0";
            rtbExpenseDescription.Text = string.Empty;
            if (cmbExpensePaymentMethod.Items.Count > 0) cmbExpensePaymentMethod.SelectedIndex = 0;
            txtExpenseReference.Text = string.Empty;
            chkExpenseIsPaid.Checked = false;
            dtpExpensePaymentDate.Value = DateTime.Now;
            cmbExpenseEmployee.SelectedIndex = -1;
        }

        private void EnableExpenseDetails()
        {
            txtExpenseTitle.ReadOnly = false;
            cmbExpenseCategory.Enabled = true;
            dtpExpenseDate.Enabled = true;
            txtExpenseAmount.ReadOnly = false;
            rtbExpenseDescription.ReadOnly = false;
            cmbExpensePaymentMethod.Enabled = true;
            txtExpenseReference.ReadOnly = false;
            chkExpenseIsPaid.Enabled = true;
            dtpExpensePaymentDate.Enabled = true;
            cmbExpenseEmployee.Enabled = true;
        }

        private void DisableExpenseDetails()
        {
            txtExpenseTitle.ReadOnly = true;
            cmbExpenseCategory.Enabled = false;
            dtpExpenseDate.Enabled = false;
            txtExpenseAmount.ReadOnly = true;
            rtbExpenseDescription.ReadOnly = true;
            cmbExpensePaymentMethod.Enabled = false;
            txtExpenseReference.ReadOnly = true;
            chkExpenseIsPaid.Enabled = false;
            dtpExpensePaymentDate.Enabled = false;
            cmbExpenseEmployee.Enabled = false;
        }

        private void EnableExpenseButtons()
        {
            btnEditExpense.Enabled = true;
            btnDeleteExpense.Enabled = true;
        }

        private void DisableExpenseButtons()
        {
            btnEditExpense.Enabled = false;
            btnDeleteExpense.Enabled = false;
        }

        #endregion

        #region Summary Tab İşləmləri

        private async Task LoadFinancialSummary()
        {
            try
            {
                // Hesabat dövrlərini təyin et
                DateTime startOfMonth = new DateTime(DateTime.Now.Year, DateTime.Now.Month, 1);
                DateTime startOfPreviousMonth = startOfMonth.AddMonths(-1);
                DateTime startOfYear = new DateTime(DateTime.Now.Year, 1, 1);

                // Cari ay üçün maliyyə hesabatı
                var currentMonthReport = await Task.Run(() => _financialService.CalculateFinancialReport(
                    _transactions, _expenses, startOfMonth, DateTime.Now));

                // Əvvəlki ay üçün maliyyə hesabatı
                var previousMonthReport = await Task.Run(() => _financialService.CalculateFinancialReport(
                    _transactions, _expenses, startOfPreviousMonth, startOfMonth.AddDays(-1)));

                // Cari il üçün maliyyə hesabatı
                var yearToDateReport = await Task.Run(() => _financialService.CalculateFinancialReport(
                    _transactions, _expenses, startOfYear, DateTime.Now));

                // Hesabat dövrlərinin adlarını
                lblCurrentMonthTitle.Text = $"Cari ay ({startOfMonth:MMMM yyyy})";
                lblPreviousMonthTitle.Text = $"Əvvəlki ay ({startOfPreviousMonth:MMMM yyyy})";
                lblYearToDateTitle.Text = $"İlin əvvəlindən ({startOfYear:dd.MM.yyyy} - {DateTime.Now:dd.MM.yyyy})";

                // Cari ay məlumatları
                lblCurrentMonthRevenue.Text = currentMonthReport.TotalRevenue.ToString("C");
                lblCurrentMonthExpenses.Text = currentMonthReport.TotalExpenses.ToString("C");
                lblCurrentMonthProfit.Text = currentMonthReport.NetProfit.ToString("C");
                lblCurrentMonthCommission.Text = currentMonthReport.TotalCommission.ToString("C");

                // Cari ay göstəricilərinin rengi
                lblCurrentMonthProfit.ForeColor = currentMonthReport.NetProfit >= 0 ? Color.Green : Color.Red;

                // Əvvəlki ay məlumatları
                lblPreviousMonthRevenue.Text = previousMonthReport.TotalRevenue.ToString("C");
                lblPreviousMonthExpenses.Text = previousMonthReport.TotalExpenses.ToString("C");
                lblPreviousMonthProfit.Text = previousMonthReport.NetProfit.ToString("C");
                lblPreviousMonthCommission.Text = previousMonthReport.TotalCommission.ToString("C");

                // Əvvəlki ay göstəricilərinin rengi
                lblPreviousMonthProfit.ForeColor = previousMonthReport.NetProfit >= 0 ? Color.Green : Color.Red;

                // İlin əvvəlindən məlumatlar
                lblYearToDateRevenue.Text = yearToDateReport.TotalRevenue.ToString("C");
                lblYearToDateExpenses.Text = yearToDateReport.TotalExpenses.ToString("C");
                lblYearToDateProfit.Text = yearToDateReport.NetProfit.ToString("C");
                lblYearToDateCommission.Text = yearToDateReport.TotalCommission.ToString("C");

                // İlin əvvəlindən göstəricilərinin rengi
                lblYearToDateProfit.ForeColor = yearToDateReport.NetProfit >= 0 ? Color.Green : Color.Red;

                // İşçi bonuslarını hesabla və göstər
                await LoadEmployeeCommissions();

                // Xərc kateqoriyalarını hesabla
                LoadExpenseCategories(yearToDateReport.ExpensesByCategory);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Maliyyə hesabatını yükləyərkən xəta baş verdi: {ex.Message}", "Xəta",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private async Task LoadEmployeeCommissions()
        {
            try
            {
                lvEmployeeCommissions.Items.Clear();

                // Cari ay üçün işçi komissiyalarını hesabla
                var employeeCommissions = await Task.Run(() => _financialService.CalculateEmployeeCommissions(
                    _transactions, _employees, new DateTime(DateTime.Now.Year, DateTime.Now.Month, 1), DateTime.Now));

                foreach (var commission in employeeCommissions)
                {
                    var item = new ListViewItem(commission.EmployeeName);
                    item.SubItems.Add(commission.TransactionCount.ToString());
                    item.SubItems.Add(commission.TotalAmount.ToString("C"));
                    item.SubItems.Add(commission.CommissionAmount.ToString("C"));

                    lvEmployeeCommissions.Items.Add(item);
                }

                // Sütunların ölçülərini avtomatik tənzimlə
                lvEmployeeCommissions.AutoResizeColumns(ColumnHeaderAutoResizeStyle.HeaderSize);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"İşçi komissiyalarını hesablayarkən xəta baş verdi: {ex.Message}", "Xəta",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void LoadExpenseCategories(Dictionary<string, decimal> expensesByCategory)
        {
            try
            {
                lvExpenseCategories.Items.Clear();

                foreach (var category in expensesByCategory)
                {
                    var item = new ListViewItem(category.Key);
                    item.SubItems.Add(category.Value.ToString("C"));

                    lvExpenseCategories.Items.Add(item);
                }

                // Sütunların ölçülərini avtomatik tənzimlə
                lvExpenseCategories.AutoResizeColumns(ColumnHeaderAutoResizeStyle.HeaderSize);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Xərc kateqoriyalarını yükləyərkən xəta baş verdi: {ex.Message}", "Xəta",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        #endregion

        private void btnRefresh_Click(object sender, EventArgs e)
        {
            RefreshCurrentTab();
        }

        private async void RefreshCurrentTab()
        {
            lblStatus.Text = "Məlumatlar yenilənir...";
            progressBar.Visible = true;

            await LoadDataForSelectedTab();

            lblStatus.Text = "Məlumatlar yeniləndi.";
            progressBar.Visible = false;
        }

        // Təsadüfi rəqəmsal dəyişiklikləri kontrol etmək
        private void txtNumericOnly_KeyPress(object sender, KeyPressEventArgs e)
        {
            // Yalnız rəqəmlər, nöqtə və idarəetmə simvollarına icazə ver
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar) && e.KeyChar != '.')
            {
                e.Handled = true;
            }

            // Nöqtədən sonra ikinci nöqtəyə icazə vermə
            TextBox textBox = sender as TextBox;
            if (e.KeyChar == '.' && textBox.Text.Contains('.'))
            {
                e.Handled = true;
            }
        }

        // Ödəniş statusu dəyişdikdə
        private void chkExpenseIsPaid_CheckedChanged(object sender, EventArgs e)
        {
            // Ödəniş tarixi sahəsini aktivləşdir/deaktivləşdir
            dtpExpensePaymentDate.Enabled = chkExpenseIsPaid.Checked && chkExpenseIsPaid.Enabled;
        }

        // Əməliyyat növü dəyişdikdə komissiya hesabla
        private void txtAmount_TextChanged(object sender, EventArgs e)
        {
            if (!txtAmount.ReadOnly)
            {
                CalculateCommission();
            }
        }

        private void cmbTransactionType_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cmbTransactionType.Enabled)
            {
                CalculateCommission();
            }
        }

        private void cmbEmployee_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cmbEmployee.Enabled)
            {
                CalculateCommission();
            }
        }

        private void CalculateCommission()
        {
            try
            {
                if (decimal.TryParse(txtAmount.Text, out decimal amount) &&
                    cmbEmployee.SelectedItem is Employee employee &&
                    cmbTransactionType.SelectedItem != null)
                {
                    string transactionType = cmbTransactionType.SelectedItem.ToString();
                    decimal commissionRate = employee.CommissionRate;

                    // Satış və kirayə əməliyyatları üçün komissiya hesabla
                    if (transactionType == "Satış" || transactionType == "Kirayə")
                    {
                        decimal commission = _financialService.CalculateCommissionAmount(amount, commissionRate, transactionType);
                        txtCommissionAmount.Text = commission.ToString();
                    }
                    else
                    {
                        // Digər əməliyyat növləri üçün sıfır və ya xüsusi komissiya tətbiq et
                        txtCommissionAmount.Text = "0";
                    }
                }
            }
            catch
            {
                // Xəta halında komissiya sıfır
                txtCommissionAmount.Text = "0";
            }
        }
    }
}
