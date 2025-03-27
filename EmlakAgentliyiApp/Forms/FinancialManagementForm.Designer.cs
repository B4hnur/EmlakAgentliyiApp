namespace DashinmazEmlakApp.Forms
{
    partial class FinancialManagementForm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.panelTop = new System.Windows.Forms.Panel();
            this.btnRefresh = new System.Windows.Forms.Button();
            this.lblTitle = new System.Windows.Forms.Label();
            this.panelBottom = new System.Windows.Forms.Panel();
            this.progressBar = new System.Windows.Forms.ProgressBar();
            this.lblStatus = new System.Windows.Forms.Label();
            this.panelMain = new System.Windows.Forms.Panel();
            this.tabControl = new System.Windows.Forms.TabControl();
            this.tabTransactions = new System.Windows.Forms.TabPage();
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.lvTransactions = new System.Windows.Forms.ListView();
            this.colTransactionDate = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.colTransactionType = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.colEmployee = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.colClient = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.colProperty = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.colAmount = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.colCommission = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.colPaymentMethod = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.colStatus = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.cmbProperty = new System.Windows.Forms.ComboBox();
            this.label10 = new System.Windows.Forms.Label();
            this.cmbClient = new System.Windows.Forms.ComboBox();
            this.label9 = new System.Windows.Forms.Label();
            this.cmbEmployee = new System.Windows.Forms.ComboBox();
            this.label8 = new System.Windows.Forms.Label();
            this.rtbTransactionDescription = new System.Windows.Forms.RichTextBox();
            this.label7 = new System.Windows.Forms.Label();
            this.chkIsSuccessful = new System.Windows.Forms.CheckBox();
            this.txtReferenceNumber = new System.Windows.Forms.TextBox();
            this.label6 = new System.Windows.Forms.Label();
            this.cmbPaymentMethod = new System.Windows.Forms.ComboBox();
            this.label5 = new System.Windows.Forms.Label();
            this.txtCommissionAmount = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.txtAmount = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.cmbTransactionType = new System.Windows.Forms.ComboBox();
            this.label2 = new System.Windows.Forms.Label();
            this.dtpTransactionDate = new System.Windows.Forms.DateTimePicker();
            this.label1 = new System.Windows.Forms.Label();
            this.panelTransactionButtons = new System.Windows.Forms.Panel();
            this.btnCancelTransaction = new System.Windows.Forms.Button();
            this.btnSaveTransaction = new System.Windows.Forms.Button();
            this.btnDeleteTransaction = new System.Windows.Forms.Button();
            this.btnEditTransaction = new System.Windows.Forms.Button();
            this.btnAddTransaction = new System.Windows.Forms.Button();
            this.tabExpenses = new System.Windows.Forms.TabPage();
            this.splitContainer2 = new System.Windows.Forms.SplitContainer();
            this.lvExpenses = new System.Windows.Forms.ListView();
            this.colExpenseDate = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.colExpenseTitle = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.colExpenseCategory = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.colExpenseAmount = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.colExpenseEmployee = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.colExpenseStatus = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.colExpensePaymentMethod = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.cmbExpenseEmployee = new System.Windows.Forms.ComboBox();
            this.label19 = new System.Windows.Forms.Label();
            this.dtpExpensePaymentDate = new System.Windows.Forms.DateTimePicker();
            this.label18 = new System.Windows.Forms.Label();
            this.chkExpenseIsPaid = new System.Windows.Forms.CheckBox();
            this.txtExpenseReference = new System.Windows.Forms.TextBox();
            this.label11 = new System.Windows.Forms.Label();
            this.cmbExpensePaymentMethod = new System.Windows.Forms.ComboBox();
            this.label12 = new System.Windows.Forms.Label();
            this.rtbExpenseDescription = new System.Windows.Forms.RichTextBox();
            this.label13 = new System.Windows.Forms.Label();
            this.txtExpenseAmount = new System.Windows.Forms.TextBox();
            this.label14 = new System.Windows.Forms.Label();
            this.dtpExpenseDate = new System.Windows.Forms.DateTimePicker();
            this.label15 = new System.Windows.Forms.Label();
            this.cmbExpenseCategory = new System.Windows.Forms.ComboBox();
            this.label16 = new System.Windows.Forms.Label();
            this.txtExpenseTitle = new System.Windows.Forms.TextBox();
            this.label17 = new System.Windows.Forms.Label();
            this.panelExpenseButtons = new System.Windows.Forms.Panel();
            this.btnCancelExpense = new System.Windows.Forms.Button();
            this.btnSaveExpense = new System.Windows.Forms.Button();
            this.btnDeleteExpense = new System.Windows.Forms.Button();
            this.btnEditExpense = new System.Windows.Forms.Button();
            this.btnAddExpense = new System.Windows.Forms.Button();
            this.tabSummary = new System.Windows.Forms.TabPage();
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.groupBox5 = new System.Windows.Forms.GroupBox();
            this.lvExpenseCategories = new System.Windows.Forms.ListView();
            this.colCategoryName = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.colCategoryTotal = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.groupBox4 = new System.Windows.Forms.GroupBox();
            this.lvEmployeeCommissions = new System.Windows.Forms.ListView();
            this.colEmployeeName = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.colTransactionCount = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.colTotalTransactions = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.colTotalCommission = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.tableLayoutPanel2 = new System.Windows.Forms.TableLayoutPanel();
            this.label36 = new System.Windows.Forms.Label();
            this.label35 = new System.Windows.Forms.Label();
            this.label34 = new System.Windows.Forms.Label();
            this.label33 = new System.Windows.Forms.Label();
            this.lblYearToDateCommission = new System.Windows.Forms.Label();
            this.lblYearToDateProfit = new System.Windows.Forms.Label();
            this.lblYearToDateExpenses = new System.Windows.Forms.Label();
            this.lblYearToDateRevenue = new System.Windows.Forms.Label();
            this.label28 = new System.Windows.Forms.Label();
            this.lblPreviousMonthCommission = new System.Windows.Forms.Label();
            this.lblPreviousMonthProfit = new System.Windows.Forms.Label();
            this.lblPreviousMonthExpenses = new System.Windows.Forms.Label();
            this.lblPreviousMonthRevenue = new System.Windows.Forms.Label();
            this.label23 = new System.Windows.Forms.Label();
            this.lblCurrentMonthCommission = new System.Windows.Forms.Label();
            this.lblCurrentMonthProfit = new System.Windows.Forms.Label();
            this.lblCurrentMonthExpenses = new System.Windows.Forms.Label();
            this.lblCurrentMonthRevenue = new System.Windows.Forms.Label();
            this.label20 = new System.Windows.Forms.Label();
            this.lblYearToDateTitle = new System.Windows.Forms.Label();
            this.lblPreviousMonthTitle = new System.Windows.Forms.Label();
            this.lblCurrentMonthTitle = new System.Windows.Forms.Label();
            this.panelTop.SuspendLayout();
            this.panelBottom.SuspendLayout();
            this.panelMain.SuspendLayout();
            this.tabControl.SuspendLayout();
            this.tabTransactions.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).BeginInit();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.panelTransactionButtons.SuspendLayout();
            this.tabExpenses.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer2)).BeginInit();
            this.splitContainer2.Panel1.SuspendLayout();
            this.splitContainer2.Panel2.SuspendLayout();
            this.splitContainer2.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.panelExpenseButtons.SuspendLayout();
            this.tabSummary.SuspendLayout();
            this.tableLayoutPanel1.SuspendLayout();
            this.groupBox5.SuspendLayout();
            this.groupBox4.SuspendLayout();
            this.groupBox3.SuspendLayout();
            this.tableLayoutPanel2.SuspendLayout();
            this.SuspendLayout();
            // 
            // panelTop
            // 
            this.panelTop.Controls.Add(this.btnRefresh);
            this.panelTop.Controls.Add(this.lblTitle);
            this.panelTop.Dock = System.Windows.Forms.DockStyle.Top;
            this.panelTop.Location = new System.Drawing.Point(0, 0);
            this.panelTop.Name = "panelTop";
            this.panelTop.Size = new System.Drawing.Size(984, 50);
            this.panelTop.TabIndex = 0;
            // 
            // btnRefresh
            // 
            this.btnRefresh.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnRefresh.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(52)))), ((int)(((byte)(152)))), ((int)(((byte)(219)))));
            this.btnRefresh.FlatAppearance.BorderSize = 0;
            this.btnRefresh.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnRefresh.ForeColor = System.Drawing.Color.White;
            this.btnRefresh.Location = new System.Drawing.Point(842, 12);
            this.btnRefresh.Name = "btnRefresh";
            this.btnRefresh.Size = new System.Drawing.Size(130, 30);
            this.btnRefresh.TabIndex = 1;
            this.btnRefresh.Text = "Yenilə";
            this.btnRefresh.UseVisualStyleBackColor = false;
            this.btnRefresh.Click += new System.EventHandler(this.btnRefresh_Click);
            // 
            // lblTitle
            // 
            this.lblTitle.AutoSize = true;
            this.lblTitle.Font = new System.Drawing.Font("Microsoft Sans Serif", 14F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblTitle.Location = new System.Drawing.Point(12, 14);
            this.lblTitle.Name = "lblTitle";
            this.lblTitle.Size = new System.Drawing.Size(194, 24);
            this.lblTitle.TabIndex = 0;
            this.lblTitle.Text = "Maliyyə İdarəetməsi";
            // 
            // panelBottom
            // 
            this.panelBottom.Controls.Add(this.progressBar);
            this.panelBottom.Controls.Add(this.lblStatus);
            this.panelBottom.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panelBottom.Location = new System.Drawing.Point(0, 600);
            this.panelBottom.Name = "panelBottom";
            this.panelBottom.Size = new System.Drawing.Size(984, 40);
            this.panelBottom.TabIndex = 1;
            // 
            // progressBar
            // 
            this.progressBar.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.progressBar.Location = new System.Drawing.Point(289, 9);
            this.progressBar.Name = "progressBar";
            this.progressBar.Size = new System.Drawing.Size(683, 20);
            this.progressBar.TabIndex = 1;
            this.progressBar.Visible = false;
            // 
            // lblStatus
            // 
            this.lblStatus.AutoSize = true;
            this.lblStatus.Location = new System.Drawing.Point(12, 13);
            this.lblStatus.Name = "lblStatus";
            this.lblStatus.Size = new System.Drawing.Size(83, 13);
            this.lblStatus.TabIndex = 0;
            this.lblStatus.Text = "Məlumatlar hazır";
            // 
            // panelMain
            // 
            this.panelMain.Controls.Add(this.tabControl);
            this.panelMain.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelMain.Location = new System.Drawing.Point(0, 50);
            this.panelMain.Name = "panelMain";
            this.panelMain.Size = new System.Drawing.Size(984, 550);
            this.panelMain.TabIndex = 2;
            // 
            // tabControl
            // 
            this.tabControl.Controls.Add(this.tabTransactions);
            this.tabControl.Controls.Add(this.tabExpenses);
            this.tabControl.Controls.Add(this.tabSummary);
            this.tabControl.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tabControl.Location = new System.Drawing.Point(0, 0);
            this.tabControl.Name = "tabControl";
            this.tabControl.SelectedIndex = 0;
            this.tabControl.Size = new System.Drawing.Size(984, 550);
            this.tabControl.TabIndex = 0;
            this.tabControl.SelectedIndexChanged += new System.EventHandler(this.tabControl_SelectedIndexChanged);
            // 
            // tabTransactions
            // 
            this.tabTransactions.Controls.Add(this.splitContainer1);
            this.tabTransactions.Location = new System.Drawing.Point(4, 22);
            this.tabTransactions.Name = "tabTransactions";
            this.tabTransactions.Padding = new System.Windows.Forms.Padding(3);
            this.tabTransactions.Size = new System.Drawing.Size(976, 524);
            this.tabTransactions.TabIndex = 0;
            this.tabTransactions.Text = "Əməliyyatlar";
            this.tabTransactions.UseVisualStyleBackColor = true;
            // 
            // splitContainer1
            // 
            this.splitContainer1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer1.Location = new System.Drawing.Point(3, 3);
            this.splitContainer1.Name = "splitContainer1";
            // 
            // splitContainer1.Panel1
            // 
            this.splitContainer1.Panel1.Controls.Add(this.lvTransactions);
            // 
            // splitContainer1.Panel2
            // 
            this.splitContainer1.Panel2.Controls.Add(this.groupBox1);
            this.splitContainer1.Panel2.Controls.Add(this.panelTransactionButtons);
            this.splitContainer1.Size = new System.Drawing.Size(970, 518);
            this.splitContainer1.SplitterDistance = 500;
            this.splitContainer1.TabIndex = 0;
            // 
            // lvTransactions
            // 
            this.lvTransactions.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.colTransactionDate,
            this.colTransactionType,
            this.colEmployee,
            this.colClient,
            this.colProperty,
            this.colAmount,
            this.colCommission,
            this.colPaymentMethod,
            this.colStatus});
            this.lvTransactions.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lvTransactions.FullRowSelect = true;
            this.lvTransactions.HideSelection = false;
            this.lvTransactions.Location = new System.Drawing.Point(0, 0);
            this.lvTransactions.MultiSelect = false;
            this.lvTransactions.Name = "lvTransactions";
            this.lvTransactions.Size = new System.Drawing.Size(500, 518);
            this.lvTransactions.TabIndex = 0;
            this.lvTransactions.UseCompatibleStateImageBehavior = false;
            this.lvTransactions.View = System.Windows.Forms.View.Details;
            this.lvTransactions.SelectedIndexChanged += new System.EventHandler(this.lvTransactions_SelectedIndexChanged);
            // 
            // colTransactionDate
            // 
            this.colTransactionDate.Text = "Tarix";
            this.colTransactionDate.Width = 80;
            // 
            // colTransactionType
            // 
            this.colTransactionType.Text = "Əməliyyat";
            this.colTransactionType.Width = 70;
            // 
            // colEmployee
            // 
            this.colEmployee.Text = "İşçi";
            this.colEmployee.Width = 80;
            // 
            // colClient
            // 
            this.colClient.Text = "Müştəri";
            this.colClient.Width = 80;
            // 
            // colProperty
            // 
            this.colProperty.Text = "Əmlak";
            this.colProperty.Width = 80;
            // 
            // colAmount
            // 
            this.colAmount.Text = "Məbləğ";
            this.colAmount.Width = 80;
            // 
            // colCommission
            // 
            this.colCommission.Text = "Komissiya";
            this.colCommission.Width = 80;
            // 
            // colPaymentMethod
            // 
            this.colPaymentMethod.Text = "Ödəniş növü";
            this.colPaymentMethod.Width = 80;
            // 
            // colStatus
            // 
            this.colStatus.Text = "Status";
            this.colStatus.Width = 80;
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.cmbProperty);
            this.groupBox1.Controls.Add(this.label10);
            this.groupBox1.Controls.Add(this.cmbClient);
            this.groupBox1.Controls.Add(this.label9);
            this.groupBox1.Controls.Add(this.cmbEmployee);
            this.groupBox1.Controls.Add(this.label8);
            this.groupBox1.Controls.Add(this.rtbTransactionDescription);
            this.groupBox1.Controls.Add(this.label7);
            this.groupBox1.Controls.Add(this.chkIsSuccessful);
            this.groupBox1.Controls.Add(this.txtReferenceNumber);
            this.groupBox1.Controls.Add(this.label6);
            this.groupBox1.Controls.Add(this.cmbPaymentMethod);
            this.groupBox1.Controls.Add(this.label5);
            this.groupBox1.Controls.Add(this.txtCommissionAmount);
            this.groupBox1.Controls.Add(this.label4);
            this.groupBox1.Controls.Add(this.txtAmount);
            this.groupBox1.Controls.Add(this.label3);
            this.groupBox1.Controls.Add(this.cmbTransactionType);
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Controls.Add(this.dtpTransactionDate);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.groupBox1.Location = new System.Drawing.Point(0, 0);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(466, 468);
            this.groupBox1.TabIndex = 1;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Əməliyyat məlumatları";
            // 
            // cmbProperty
            // 
            this.cmbProperty.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.cmbProperty.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbProperty.FormattingEnabled = true;
            this.cmbProperty.Location = new System.Drawing.Point(132, 410);
            this.cmbProperty.Name = "cmbProperty";
            this.cmbProperty.Size = new System.Drawing.Size(318, 21);
            this.cmbProperty.TabIndex = 20;
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Location = new System.Drawing.Point(13, 413);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(73, 13);
            this.label10.TabIndex = 19;
            this.label10.Text = "Əlaqəli əmlak:";
            // 
            // cmbClient
            // 
            this.cmbClient.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.cmbClient.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbClient.FormattingEnabled = true;
            this.cmbClient.Location = new System.Drawing.Point(132, 375);
            this.cmbClient.Name = "cmbClient";
            this.cmbClient.Size = new System.Drawing.Size(318, 21);
            this.cmbClient.TabIndex = 18;
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(13, 378);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(78, 13);
            this.label9.TabIndex = 17;
            this.label9.Text = "Əlaqəli müştəri:";
            // 
            // cmbEmployee
            // 
            this.cmbEmployee.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.cmbEmployee.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbEmployee.FormattingEnabled = true;
            this.cmbEmployee.Location = new System.Drawing.Point(132, 340);
            this.cmbEmployee.Name = "cmbEmployee";
            this.cmbEmployee.Size = new System.Drawing.Size(318, 21);
            this.cmbEmployee.TabIndex = 16;
            this.cmbEmployee.SelectedIndexChanged += new System.EventHandler(this.cmbEmployee_SelectedIndexChanged);
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(13, 343);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(56, 13);
            this.label8.TabIndex = 15;
            this.label8.Text = "Məsul işçi:";
            // 
            // rtbTransactionDescription
            // 
            this.rtbTransactionDescription.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.rtbTransactionDescription.Location = new System.Drawing.Point(132, 220);
            this.rtbTransactionDescription.Name = "rtbTransactionDescription";
            this.rtbTransactionDescription.Size = new System.Drawing.Size(318, 84);
            this.rtbTransactionDescription.TabIndex = 14;
            this.rtbTransactionDescription.Text = "";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(13, 223);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(38, 13);
            this.label7.TabIndex = 13;
            this.label7.Text = "Əlavə:";
            // 
            // chkIsSuccessful
            // 
            this.chkIsSuccessful.AutoSize = true;
            this.chkIsSuccessful.Location = new System.Drawing.Point(132, 310);
            this.chkIsSuccessful.Name = "chkIsSuccessful";
            this.chkIsSuccessful.Size = new System.Drawing.Size(127, 17);
            this.chkIsSuccessful.TabIndex = 12;
            this.chkIsSuccessful.Text = "Əməliyyat tamamlanıb";
            this.chkIsSuccessful.UseVisualStyleBackColor = true;
            // 
            // txtReferenceNumber
            // 
            this.txtReferenceNumber.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtReferenceNumber.Location = new System.Drawing.Point(132, 190);
            this.txtReferenceNumber.Name = "txtReferenceNumber";
            this.txtReferenceNumber.Size = new System.Drawing.Size(318, 20);
            this.txtReferenceNumber.TabIndex = 11;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(13, 193);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(85, 13);
            this.label6.TabIndex = 10;
            this.label6.Text = "Qəbz/İstinad №:";
            // 
            // cmbPaymentMethod
            // 
            this.cmbPaymentMethod.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.cmbPaymentMethod.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbPaymentMethod.FormattingEnabled = true;
            this.cmbPaymentMethod.Location = new System.Drawing.Point(132, 160);
            this.cmbPaymentMethod.Name = "cmbPaymentMethod";
            this.cmbPaymentMethod.Size = new System.Drawing.Size(318, 21);
            this.cmbPaymentMethod.TabIndex = 9;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(13, 163);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(70, 13);
            this.label5.TabIndex = 8;
            this.label5.Text = "Ödəniş növü:";
            // 
            // txtCommissionAmount
            // 
            this.txtCommissionAmount.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtCommissionAmount.Location = new System.Drawing.Point(132, 130);
            this.txtCommissionAmount.Name = "txtCommissionAmount";
            this.txtCommissionAmount.Size = new System.Drawing.Size(318, 20);
            this.txtCommissionAmount.TabIndex = 7;
            this.txtCommissionAmount.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtNumericOnly_KeyPress);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(13, 133);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(56, 13);
            this.label4.TabIndex = 6;
            this.label4.Text = "Komissiya:";
            // 
            // txtAmount
            // 
            this.txtAmount.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtAmount.Location = new System.Drawing.Point(132, 100);
            this.txtAmount.Name = "txtAmount";
            this.txtAmount.Size = new System.Drawing.Size(318, 20);
            this.txtAmount.TabIndex = 5;
            this.txtAmount.TextChanged += new System.EventHandler(this.txtAmount_TextChanged);
            this.txtAmount.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtNumericOnly_KeyPress);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(13, 103);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(45, 13);
            this.label3.TabIndex = 4;
            this.label3.Text = "Məbləğ:";
            // 
            // cmbTransactionType
            // 
            this.cmbTransactionType.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.cmbTransactionType.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbTransactionType.FormattingEnabled = true;
            this.cmbTransactionType.Location = new System.Drawing.Point(132, 70);
            this.cmbTransactionType.Name = "cmbTransactionType";
            this.cmbTransactionType.Size = new System.Drawing.Size(318, 21);
            this.cmbTransactionType.TabIndex = 3;
            this.cmbTransactionType.SelectedIndexChanged += new System.EventHandler(this.cmbTransactionType_SelectedIndexChanged);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(13, 73);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(82, 13);
            this.label2.TabIndex = 2;
            this.label2.Text = "Əməliyyat növü:";
            // 
            // dtpTransactionDate
            // 
            this.dtpTransactionDate.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.dtpTransactionDate.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.dtpTransactionDate.Location = new System.Drawing.Point(132, 40);
            this.dtpTransactionDate.Name = "dtpTransactionDate";
            this.dtpTransactionDate.Size = new System.Drawing.Size(318, 20);
            this.dtpTransactionDate.TabIndex = 1;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(13, 43);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(33, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = "Tarix:";
            // 
            // panelTransactionButtons
            // 
            this.panelTransactionButtons.Controls.Add(this.btnCancelTransaction);
            this.panelTransactionButtons.Controls.Add(this.btnSaveTransaction);
            this.panelTransactionButtons.Controls.Add(this.btnDeleteTransaction);
            this.panelTransactionButtons.Controls.Add(this.btnEditTransaction);
            this.panelTransactionButtons.Controls.Add(this.btnAddTransaction);
            this.panelTransactionButtons.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panelTransactionButtons.Location = new System.Drawing.Point(0, 468);
            this.panelTransactionButtons.Name = "panelTransactionButtons";
            this.panelTransactionButtons.Size = new System.Drawing.Size(466, 50);
            this.panelTransactionButtons.TabIndex = 0;
            // 
            // btnCancelTransaction
            // 
            this.btnCancelTransaction.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnCancelTransaction.BackColor = System.Drawing.Color.Gray;
            this.btnCancelTransaction.FlatAppearance.BorderSize = 0;
            this.btnCancelTransaction.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnCancelTransaction.ForeColor = System.Drawing.Color.White;
            this.btnCancelTransaction.Location = new System.Drawing.Point(377, 10);
            this.btnCancelTransaction.Name = "btnCancelTransaction";
            this.btnCancelTransaction.Size = new System.Drawing.Size(80, 30);
            this.btnCancelTransaction.TabIndex = 4;
            this.btnCancelTransaction.Text = "İmtina";
            this.btnCancelTransaction.UseVisualStyleBackColor = false;
            this.btnCancelTransaction.Visible = false;
            this.btnCancelTransaction.Click += new System.EventHandler(this.btnCancelTransaction_Click);
            // 
            // btnSaveTransaction
            // 
            this.btnSaveTransaction.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnSaveTransaction.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(46)))), ((int)(((byte)(204)))), ((int)(((byte)(113)))));
            this.btnSaveTransaction.FlatAppearance.BorderSize = 0;
            this.btnSaveTransaction.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnSaveTransaction.ForeColor = System.Drawing.Color.White;
            this.btnSaveTransaction.Location = new System.Drawing.Point(291, 10);
            this.btnSaveTransaction.Name = "btnSaveTransaction";
            this.btnSaveTransaction.Size = new System.Drawing.Size(80, 30);
            this.btnSaveTransaction.TabIndex = 3;
            this.btnSaveTransaction.Text = "Saxla";
            this.btnSaveTransaction.UseVisualStyleBackColor = false;
            this.btnSaveTransaction.Visible = false;
            this.btnSaveTransaction.Click += new System.EventHandler(this.btnSaveTransaction_Click);
            // 
            // btnDeleteTransaction
            // 
            this.btnDeleteTransaction.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(231)))), ((int)(((byte)(76)))), ((int)(((byte)(60)))));
            this.btnDeleteTransaction.Enabled = false;
            this.btnDeleteTransaction.FlatAppearance.BorderSize = 0;
            this.btnDeleteTransaction.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnDeleteTransaction.ForeColor = System.Drawing.Color.White;
            this.btnDeleteTransaction.Location = new System.Drawing.Point(174, 10);
            this.btnDeleteTransaction.Name = "btnDeleteTransaction";
            this.btnDeleteTransaction.Size = new System.Drawing.Size(80, 30);
            this.btnDeleteTransaction.TabIndex = 2;
            this.btnDeleteTransaction.Text = "Sil";
            this.btnDeleteTransaction.UseVisualStyleBackColor = false;
            this.btnDeleteTransaction.Click += new System.EventHandler(this.btnDeleteTransaction_Click);
            // 
            // btnEditTransaction
            // 
            this.btnEditTransaction.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(52)))), ((int)(((byte)(152)))), ((int)(((byte)(219)))));
            this.btnEditTransaction.Enabled = false;
            this.btnEditTransaction.FlatAppearance.BorderSize = 0;
            this.btnEditTransaction.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnEditTransaction.ForeColor = System.Drawing.Color.White;
            this.btnEditTransaction.Location = new System.Drawing.Point(88, 10);
            this.btnEditTransaction.Name = "btnEditTransaction";
            this.btnEditTransaction.Size = new System.Drawing.Size(80, 30);
            this.btnEditTransaction.TabIndex = 1;
            this.btnEditTransaction.Text = "Düzəliş";
            this.btnEditTransaction.UseVisualStyleBackColor = false;
            this.btnEditTransaction.Click += new System.EventHandler(this.btnEditTransaction_Click);
            // 
            // btnAddTransaction
            // 
            this.btnAddTransaction.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(46)))), ((int)(((byte)(204)))), ((int)(((byte)(113)))));
            this.btnAddTransaction.FlatAppearance.BorderSize = 0;
            this.btnAddTransaction.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnAddTransaction.ForeColor = System.Drawing.Color.White;
            this.btnAddTransaction.Location = new System.Drawing.Point(3, 10);
            this.btnAddTransaction.Name = "btnAddTransaction";
            this.btnAddTransaction.Size = new System.Drawing.Size(80, 30);
            this.btnAddTransaction.TabIndex = 0;
            this.btnAddTransaction.Text = "Yeni";
            this.btnAddTransaction.UseVisualStyleBackColor = false;
            this.btnAddTransaction.Click += new System.EventHandler(this.btnAddTransaction_Click);
            // 
            // tabExpenses
            // 
            this.tabExpenses.Controls.Add(this.splitContainer2);
            this.tabExpenses.Location = new System.Drawing.Point(4, 22);
            this.tabExpenses.Name = "tabExpenses";
            this.tabExpenses.Padding = new System.Windows.Forms.Padding(3);
            this.tabExpenses.Size = new System.Drawing.Size(976, 524);
            this.tabExpenses.TabIndex = 1;
            this.tabExpenses.Text = "Xərclər";
            this.tabExpenses.UseVisualStyleBackColor = true;
            // 
            // splitContainer2
            // 
            this.splitContainer2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer2.Location = new System.Drawing.Point(3, 3);
            this.splitContainer2.Name = "splitContainer2";
            // 
            // splitContainer2.Panel1
            // 
            this.splitContainer2.Panel1.Controls.Add(this.lvExpenses);
            // 
            // splitContainer2.Panel2
            // 
            this.splitContainer2.Panel2.Controls.Add(this.groupBox2);
            this.splitContainer2.Panel2.Controls.Add(this.panelExpenseButtons);
            this.splitContainer2.Size = new System.Drawing.Size(970, 518);
            this.splitContainer2.SplitterDistance = 500;
            this.splitContainer2.TabIndex = 0;
            // 
            // lvExpenses
            // 
            this.lvExpenses.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.colExpenseDate,
            this.colExpenseTitle,
            this.colExpenseCategory,
            this.colExpenseAmount,
            this.colExpenseEmployee,
            this.colExpenseStatus,
            this.colExpensePaymentMethod});
            this.lvExpenses.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lvExpenses.FullRowSelect = true;
            this.lvExpenses.HideSelection = false;
            this.lvExpenses.Location = new System.Drawing.Point(0, 0);
            this.lvExpenses.MultiSelect = false;
            this.lvExpenses.Name = "lvExpenses";
            this.lvExpenses.Size = new System.Drawing.Size(500, 518);
            this.lvExpenses.TabIndex = 0;
            this.lvExpenses.UseCompatibleStateImageBehavior = false;
            this.lvExpenses.View = System.Windows.Forms.View.Details;
            this.lvExpenses.SelectedIndexChanged += new System.EventHandler(this.lvExpenses_SelectedIndexChanged);
            // 
            // colExpenseDate
            // 
            this.colExpenseDate.Text = "Tarix";
            this.colExpenseDate.Width = 80;
            // 
            // colExpenseTitle
            // 
            this.colExpenseTitle.Text = "Başlıq";
            this.colExpenseTitle.Width = 120;
            // 
            // colExpenseCategory
            // 
            this.colExpenseCategory.Text = "Kateqoriya";
            this.colExpenseCategory.Width = 80;
            // 
            // colExpenseAmount
            // 
            this.colExpenseAmount.Text = "Məbləğ";
            this.colExpenseAmount.Width = 80;
            // 
            // colExpenseEmployee
            // 
            this.colExpenseEmployee.Text = "İşçi";
            this.colExpenseEmployee.Width = 80;
            // 
            // colExpenseStatus
            // 
            this.colExpenseStatus.Text = "Status";
            this.colExpenseStatus.Width = 80;
            // 
            // colExpensePaymentMethod
            // 
            this.colExpensePaymentMethod.Text = "Ödəniş növü";
            this.colExpensePaymentMethod.Width = 80;
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.cmbExpenseEmployee);
            this.groupBox2.Controls.Add(this.label19);
            this.groupBox2.Controls.Add(this.dtpExpensePaymentDate);
            this.groupBox2.Controls.Add(this.label18);
            this.groupBox2.Controls.Add(this.chkExpenseIsPaid);
            this.groupBox2.Controls.Add(this.txtExpenseReference);
            this.groupBox2.Controls.Add(this.label11);
            this.groupBox2.Controls.Add(this.cmbExpensePaymentMethod);
            this.groupBox2.Controls.Add(this.label12);
            this.groupBox2.Controls.Add(this.rtbExpenseDescription);
            this.groupBox2.Controls.Add(this.label13);
            this.groupBox2.Controls.Add(this.txtExpenseAmount);
            this.groupBox2.Controls.Add(this.label14);
            this.groupBox2.Controls.Add(this.dtpExpenseDate);
            this.groupBox2.Controls.Add(this.label15);
            this.groupBox2.Controls.Add(this.cmbExpenseCategory);
            this.groupBox2.Controls.Add(this.label16);
            this.groupBox2.Controls.Add(this.txtExpenseTitle);
            this.groupBox2.Controls.Add(this.label17);
            this.groupBox2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.groupBox2.Location = new System.Drawing.Point(0, 0);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(466, 468);
            this.groupBox2.TabIndex = 2;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Xərc məlumatları";
            // 
            // cmbExpenseEmployee
            // 
            this.cmbExpenseEmployee.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.cmbExpenseEmployee.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbExpenseEmployee.FormattingEnabled = true;
            this.cmbExpenseEmployee.Location = new System.Drawing.Point(132, 410);
            this.cmbExpenseEmployee.Name = "cmbExpenseEmployee";
            this.cmbExpenseEmployee.Size = new System.Drawing.Size(318, 21);
            this.cmbExpenseEmployee.TabIndex = 22;
            // 
            // label19
            // 
            this.label19.AutoSize = true;
            this.label19.Location = new System.Drawing.Point(13, 413);
            this.label19.Name = "label19";
            this.label19.Size = new System.Drawing.Size(56, 13);
            this.label19.TabIndex = 21;
            this.label19.Text = "Məsul işçi:";
            // 
            // dtpExpensePaymentDate
            // 
            this.dtpExpensePaymentDate.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.dtpExpensePaymentDate.Enabled = false;
            this.dtpExpensePaymentDate.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.dtpExpensePaymentDate.Location = new System.Drawing.Point(132, 380);
            this.dtpExpensePaymentDate.Name = "dtpExpensePaymentDate";
            this.dtpExpensePaymentDate.Size = new System.Drawing.Size(318, 20);
            this.dtpExpensePaymentDate.TabIndex = 20;
            // 
            // label18
            // 
            this.label18.AutoSize = true;
            this.label18.Location = new System.Drawing.Point(13, 383);
            this.label18.Name = "label18";
            this.label18.Size = new System.Drawing.Size(67, 13);
            this.label18.TabIndex = 19;
            this.label18.Text = "Ödəniş tarixi:";
            // 
            // chkExpenseIsPaid
            // 
            this.chkExpenseIsPaid.AutoSize = true;
            this.chkExpenseIsPaid.Location = new System.Drawing.Point(132, 350);
            this.chkExpenseIsPaid.Name = "chkExpenseIsPaid";
            this.chkExpenseIsPaid.Size = new System.Drawing.Size(64, 17);
            this.chkExpenseIsPaid.TabIndex = 18;
            this.chkExpenseIsPaid.Text = "Ödənilib";
            this.chkExpenseIsPaid.UseVisualStyleBackColor = true;
            this.chkExpenseIsPaid.CheckedChanged += new System.EventHandler(this.chkExpenseIsPaid_CheckedChanged);
            // 
            // txtExpenseReference
            // 
            this.txtExpenseReference.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtExpenseReference.Location = new System.Drawing.Point(132, 190);
            this.txtExpenseReference.Name = "txtExpenseReference";
            this.txtExpenseReference.Size = new System.Drawing.Size(318, 20);
            this.txtExpenseReference.TabIndex = 11;
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.Location = new System.Drawing.Point(13, 193);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(85, 13);
            this.label11.TabIndex = 10;
            this.label11.Text = "Qəbz/İstinad №:";
            // 
            // cmbExpensePaymentMethod
            // 
            this.cmbExpensePaymentMethod.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.cmbExpensePaymentMethod.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbExpensePaymentMethod.FormattingEnabled = true;
            this.cmbExpensePaymentMethod.Location = new System.Drawing.Point(132, 160);
            this.cmbExpensePaymentMethod.Name = "cmbExpensePaymentMethod";
            this.cmbExpensePaymentMethod.Size = new System.Drawing.Size(318, 21);
            this.cmbExpensePaymentMethod.TabIndex = 9;
            // 
            // label12
            // 
            this.label12.AutoSize = true;
            this.label12.Location = new System.Drawing.Point(13, 163);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(70, 13);
            this.label12.TabIndex = 8;
            this.label12.Text = "Ödəniş növü:";
            // 
            // rtbExpenseDescription
            // 
            this.rtbExpenseDescription.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.rtbExpenseDescription.Location = new System.Drawing.Point(132, 220);
            this.rtbExpenseDescription.Name = "rtbExpenseDescription";
            this.rtbExpenseDescription.Size = new System.Drawing.Size(318, 120);
            this.rtbExpenseDescription.TabIndex = 14;
            this.rtbExpenseDescription.Text = "";
            // 
            // label13
            // 
            this.label13.AutoSize = true;
            this.label13.Location = new System.Drawing.Point(13, 223);
            this.label13.Name = "label13";
            this.label13.Size = new System.Drawing.Size(38, 13);
            this.label13.TabIndex = 13;
            this.label13.Text = "Əlavə:";
            // 
            // txtExpenseAmount
            // 
            this.txtExpenseAmount.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtExpenseAmount.Location = new System.Drawing.Point(132, 130);
            this.txtExpenseAmount.Name = "txtExpenseAmount";
            this.txtExpenseAmount.Size = new System.Drawing.Size(318, 20);
            this.txtExpenseAmount.TabIndex = 7;
            this.txtExpenseAmount.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtNumericOnly_KeyPress);
            // 
            // label14
            // 
            this.label14.AutoSize = true;
            this.label14.Location = new System.Drawing.Point(13, 133);
            this.label14.Name = "label14";
            this.label14.Size = new System.Drawing.Size(45, 13);
            this.label14.TabIndex = 6;
            this.label14.Text = "Məbləğ:";
            // 
            // dtpExpenseDate
            // 
            this.dtpExpenseDate.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.dtpExpenseDate.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.dtpExpenseDate.Location = new System.Drawing.Point(132, 100);
            this.dtpExpenseDate.Name = "dtpExpenseDate";
            this.dtpExpenseDate.Size = new System.Drawing.Size(318, 20);
            this.dtpExpenseDate.TabIndex = 5;
            // 
            // label15
            // 
            this.label15.AutoSize = true;
            this.label15.Location = new System.Drawing.Point(13, 103);
            this.label15.Name = "label15";
            this.label15.Size = new System.Drawing.Size(33, 13);
            this.label15.TabIndex = 4;
            this.label15.Text = "Tarix:";
            // 
            // cmbExpenseCategory
            // 
            this.cmbExpenseCategory.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.cmbExpenseCategory.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbExpenseCategory.FormattingEnabled = true;
            this.cmbExpenseCategory.Location = new System.Drawing.Point(132, 70);
            this.cmbExpenseCategory.Name = "cmbExpenseCategory";
            this.cmbExpenseCategory.Size = new System.Drawing.Size(318, 21);
            this.cmbExpenseCategory.TabIndex = 3;
            // 
            // label16
            // 
            this.label16.AutoSize = true;
            this.label16.Location = new System.Drawing.Point(13, 73);
            this.label16.Name = "label16";
            this.label16.Size = new System.Drawing.Size(60, 13);
            this.label16.TabIndex = 2;
            this.label16.Text = "Kateqoriya:";
            // 
            // txtExpenseTitle
            // 
            this.txtExpenseTitle.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtExpenseTitle.Location = new System.Drawing.Point(132, 40);
            this.txtExpenseTitle.Name = "txtExpenseTitle";
            this.txtExpenseTitle.Size = new System.Drawing.Size(318, 20);
            this.txtExpenseTitle.TabIndex = 1;
            // 
            // label17
            // 
            this.label17.AutoSize = true;
            this.label17.Location = new System.Drawing.Point(13, 43);
            this.label17.Name = "label17";
            this.label17.Size = new System.Drawing.Size(38, 13);
            this.label17.TabIndex = 0;
            this.label17.Text = "Başlıq:";
            // 
            // panelExpenseButtons
            // 
            this.panelExpenseButtons.Controls.Add(this.btnCancelExpense);
            this.panelExpenseButtons.Controls.Add(this.btnSaveExpense);
            this.panelExpenseButtons.Controls.Add(this.btnDeleteExpense);
            this.panelExpenseButtons.Controls.Add(this.btnEditExpense);
            this.panelExpenseButtons.Controls.Add(this.btnAddExpense);
            this.panelExpenseButtons.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panelExpenseButtons.Location = new System.Drawing.Point(0, 468);
            this.panelExpenseButtons.Name = "panelExpenseButtons";
            this.panelExpenseButtons.Size = new System.Drawing.Size(466, 50);
            this.panelExpenseButtons.TabIndex = 1;
            // 
            // btnCancelExpense
            // 
            this.btnCancelExpense.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnCancelExpense.BackColor = System.Drawing.Color.Gray;
            this.btnCancelExpense.FlatAppearance.BorderSize = 0;
            this.btnCancelExpense.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnCancelExpense.ForeColor = System.Drawing.Color.White;
            this.btnCancelExpense.Location = new System.Drawing.Point(377, 10);
            this.btnCancelExpense.Name = "btnCancelExpense";
            this.btnCancelExpense.Size = new System.Drawing.Size(80, 30);
            this.btnCancelExpense.TabIndex = 4;
            this.btnCancelExpense.Text = "İmtina";
            this.btnCancelExpense.UseVisualStyleBackColor = false;
            this.btnCancelExpense.Visible = false;
            this.btnCancelExpense.Click += new System.EventHandler(this.btnCancelExpense_Click);
            // 
            // btnSaveExpense
            // 
            this.btnSaveExpense.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnSaveExpense.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(46)))), ((int)(((byte)(204)))), ((int)(((byte)(113)))));
            this.btnSaveExpense.FlatAppearance.BorderSize = 0;
            this.btnSaveExpense.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnSaveExpense.ForeColor = System.Drawing.Color.White;
            this.btnSaveExpense.Location = new System.Drawing.Point(291, 10);
            this.btnSaveExpense.Name = "btnSaveExpense";
            this.btnSaveExpense.Size = new System.Drawing.Size(80, 30);
            this.btnSaveExpense.TabIndex = 3;
            this.btnSaveExpense.Text = "Saxla";
            this.btnSaveExpense.UseVisualStyleBackColor = false;
            this.btnSaveExpense.Visible = false;
            this.btnSaveExpense.Click += new System.EventHandler(this.btnSaveExpense_Click);
            // 
            // btnDeleteExpense
            // 
            this.btnDeleteExpense.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(231)))), ((int)(((byte)(76)))), ((int)(((byte)(60)))));
            this.btnDeleteExpense.Enabled = false;
            this.btnDeleteExpense.FlatAppearance.BorderSize = 0;
            this.btnDeleteExpense.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnDeleteExpense.ForeColor = System.Drawing.Color.White;
            this.btnDeleteExpense.Location = new System.Drawing.Point(174, 10);
            this.btnDeleteExpense.Name = "btnDeleteExpense";
            this.btnDeleteExpense.Size = new System.Drawing.Size(80, 30);
            this.btnDeleteExpense.TabIndex = 2;
            this.btnDeleteExpense.Text = "Sil";
            this.btnDeleteExpense.UseVisualStyleBackColor = false;
            this.btnDeleteExpense.Click += new System.EventHandler(this.btnDeleteExpense_Click);
            // 
            // btnEditExpense
            // 
            this.btnEditExpense.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(52)))), ((int)(((byte)(152)))), ((int)(((byte)(219)))));
            this.btnEditExpense.Enabled = false;
            this.btnEditExpense.FlatAppearance.BorderSize = 0;
            this.btnEditExpense.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnEditExpense.ForeColor = System.Drawing.Color.White;
            this.btnEditExpense.Location = new System.Drawing.Point(88, 10);
            this.btnEditExpense.Name = "btnEditExpense";
            this.btnEditExpense.Size = new System.Drawing.Size(80, 30);
            this.btnEditExpense.TabIndex = 1;
            this.btnEditExpense.Text = "Düzəliş";
            this.btnEditExpense.UseVisualStyleBackColor = false;
            this.btnEditExpense.Click += new System.EventHandler(this.btnEditExpense_Click);
            // 
            // btnAddExpense
            // 
            this.btnAddExpense.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(46)))), ((int)(((byte)(204)))), ((int)(((byte)(113)))));
            this.btnAddExpense.FlatAppearance.BorderSize = 0;
            this.btnAddExpense.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnAddExpense.ForeColor = System.Drawing.Color.White;
            this.btnAddExpense.Location = new System.Drawing.Point(3, 10);
            this.btnAddExpense.Name = "btnAddExpense";
            this.btnAddExpense.Size = new System.Drawing.Size(80, 30);
            this.btnAddExpense.TabIndex = 0;
            this.btnAddExpense.Text = "Yeni";
            this.btnAddExpense.UseVisualStyleBackColor = false;
            this.btnAddExpense.Click += new System.EventHandler(this.btnAddExpense_Click);
            // 
            // tabSummary
            // 
            this.tabSummary.Controls.Add(this.tableLayoutPanel1);
            this.tabSummary.Location = new System.Drawing.Point(4, 22);
            this.tabSummary.Name = "tabSummary";
            this.tabSummary.Padding = new System.Windows.Forms.Padding(3);
            this.tabSummary.Size = new System.Drawing.Size(976, 524);
            this.tabSummary.TabIndex = 2;
            this.tabSummary.Text = "Maliyyə Hesabatı";
            this.tabSummary.UseVisualStyleBackColor = true;
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.ColumnCount = 2;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel1.Controls.Add(this.groupBox5, 1, 1);
            this.tableLayoutPanel1.Controls.Add(this.groupBox4, 0, 1);
            this.tableLayoutPanel1.Controls.Add(this.groupBox3, 0, 0);
            this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel1.Location = new System.Drawing.Point(3, 3);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 2;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(970, 518);
            this.tableLayoutPanel1.TabIndex = 0;
            // 
            // groupBox5
            // 
            this.groupBox5.Controls.Add(this.lvExpenseCategories);
            this.groupBox5.Dock = System.Windows.Forms.DockStyle.Fill;
            this.groupBox5.Location = new System.Drawing.Point(488, 262);
            this.groupBox5.Name = "groupBox5";
            this.groupBox5.Size = new System.Drawing.Size(479, 253);
            this.groupBox5.TabIndex = 2;
            this.groupBox5.TabStop = false;
            this.groupBox5.Text = "Xərc kateqoriyaları";
            // 
            // lvExpenseCategories
            // 
            this.lvExpenseCategories.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.colCategoryName,
            this.colCategoryTotal});
            this.lvExpenseCategories.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lvExpenseCategories.HideSelection = false;
            this.lvExpenseCategories.Location = new System.Drawing.Point(3, 16);
            this.lvExpenseCategories.Name = "lvExpenseCategories";
            this.lvExpenseCategories.Size = new System.Drawing.Size(473, 234);
            this.lvExpenseCategories.TabIndex = 0;
            this.lvExpenseCategories.UseCompatibleStateImageBehavior = false;
            this.lvExpenseCategories.View = System.Windows.Forms.View.Details;
            // 
            // colCategoryName
            // 
            this.colCategoryName.Text = "Kateqoriya";
            this.colCategoryName.Width = 200;
            // 
            // colCategoryTotal
            // 
            this.colCategoryTotal.Text = "Məbləğ";
            this.colCategoryTotal.Width = 100;
            // 
            // groupBox4
            // 
            this.groupBox4.Controls.Add(this.lvEmployeeCommissions);
            this.groupBox4.Dock = System.Windows.Forms.DockStyle.Fill;
            this.groupBox4.Location = new System.Drawing.Point(3, 262);
            this.groupBox4.Name = "groupBox4";
            this.groupBox4.Size = new System.Drawing.Size(479, 253);
            this.groupBox4.TabIndex = 1;
            this.groupBox4.TabStop = false;
            this.groupBox4.Text = "İşçi bonusları (cari ay)";
            // 
            // lvEmployeeCommissions
            // 
            this.lvEmployeeCommissions.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.colEmployeeName,
            this.colTransactionCount,
            this.colTotalTransactions,
            this.colTotalCommission});
            this.lvEmployeeCommissions.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lvEmployeeCommissions.HideSelection = false;
            this.lvEmployeeCommissions.Location = new System.Drawing.Point(3, 16);
            this.lvEmployeeCommissions.Name = "lvEmployeeCommissions";
            this.lvEmployeeCommissions.Size = new System.Drawing.Size(473, 234);
            this.lvEmployeeCommissions.TabIndex = 0;
            this.lvEmployeeCommissions.UseCompatibleStateImageBehavior = false;
            this.lvEmployeeCommissions.View = System.Windows.Forms.View.Details;
            // 
            // colEmployeeName
            // 
            this.colEmployeeName.Text = "İşçi";
            this.colEmployeeName.Width = 150;
            // 
            // colTransactionCount
            // 
            this.colTransactionCount.Text = "Əməliyyat sayı";
            this.colTransactionCount.Width = 100;
            // 
            // colTotalTransactions
            // 
            this.colTotalTransactions.Text = "Ümumi məbləğ";
            this.colTotalTransactions.Width = 100;
            // 
            // colTotalCommission
            // 
            this.colTotalCommission.Text = "Komissiya";
            this.colTotalCommission.Width = 100;
            // 
            // groupBox3
            // 
            this.tableLayoutPanel1.SetColumnSpan(this.groupBox3, 2);
            this.groupBox3.Controls.Add(this.tableLayoutPanel2);
            this.groupBox3.Dock = System.Windows.Forms.DockStyle.Fill;
            this.groupBox3.Location = new System.Drawing.Point(3, 3);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(964, 253);
            this.groupBox3.TabIndex = 0;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "Ümumi maliyyə göstəriciləri";
            this.groupBox3.Enter += new System.EventHandler(this.groupBox3_Enter);
            // 
            // tableLayoutPanel2
            // 
            this.tableLayoutPanel2.ColumnCount = 4;
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 25F));
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 25F));
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 25F));
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 25F));
            this.tableLayoutPanel2.Controls.Add(this.label36, 3, 0);
            this.tableLayoutPanel2.Controls.Add(this.label35, 2, 0);
            this.tableLayoutPanel2.Controls.Add(this.label34, 1, 0);
            this.tableLayoutPanel2.Controls.Add(this.label33, 0, 0);
            this.tableLayoutPanel2.Controls.Add(this.lblYearToDateCommission, 3, 3);
            this.tableLayoutPanel2.Controls.Add(this.lblYearToDateProfit, 2, 3);
            this.tableLayoutPanel2.Controls.Add(this.lblYearToDateExpenses, 1, 3);
            this.tableLayoutPanel2.Controls.Add(this.lblYearToDateRevenue, 0, 3);
            this.tableLayoutPanel2.Controls.Add(this.label28, 0, 2);
            this.tableLayoutPanel2.Controls.Add(this.lblPreviousMonthCommission, 3, 2);
            this.tableLayoutPanel2.Controls.Add(this.lblPreviousMonthProfit, 2, 2);
            this.tableLayoutPanel2.Controls.Add(this.lblPreviousMonthExpenses, 1, 2);
            this.tableLayoutPanel2.Controls.Add(this.lblPreviousMonthRevenue, 0, 2);
            this.tableLayoutPanel2.Controls.Add(this.label23, 0, 1);
            this.tableLayoutPanel2.Controls.Add(this.lblCurrentMonthCommission, 3, 1);
            this.tableLayoutPanel2.Controls.Add(this.lblCurrentMonthProfit, 2, 1);
            this.tableLayoutPanel2.Controls.Add(this.lblCurrentMonthExpenses, 1, 1);
            this.tableLayoutPanel2.Controls.Add(this.lblCurrentMonthRevenue, 0, 1);
            this.tableLayoutPanel2.Controls.Add(this.label20, 0, 3);
            this.tableLayoutPanel2.Controls.Add(this.lblYearToDateTitle, 0, 3);
            this.tableLayoutPanel2.Controls.Add(this.lblPreviousMonthTitle, 0, 2);
            this.tableLayoutPanel2.Controls.Add(this.lblCurrentMonthTitle, 0, 1);
            this.tableLayoutPanel2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel2.Location = new System.Drawing.Point(3, 16);
            this.tableLayoutPanel2.Name = "tableLayoutPanel2";
            this.tableLayoutPanel2.RowCount = 4;
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 20F));
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 25F));
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 25F));
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 30F));
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tableLayoutPanel2.Size = new System.Drawing.Size(958, 234);
            this.tableLayoutPanel2.TabIndex = 0;
            // 
            // label36
            // 
            this.label36.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label36.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label36.Location = new System.Drawing.Point(720, 0);
            this.label36.Name = "label36";
            this.label36.Size = new System.Drawing.Size(235, 38);
            this.label36.TabIndex = 22;
            this.label36.Text = "Komissiya";
            this.label36.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label35
            // 
            this.label35.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label35.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label35.Location = new System.Drawing.Point(481, 0);
            this.label35.Name = "label35";
            this.label35.Size = new System.Drawing.Size(233, 38);
            this.label35.TabIndex = 21;
            this.label35.Text = "Xalis Gəlir";
            this.label35.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label34
            // 
            this.label34.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label34.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label34.Location = new System.Drawing.Point(242, 0);
            this.label34.Name = "label34";
            this.label34.Size = new System.Drawing.Size(233, 38);
            this.label34.TabIndex = 20;
            this.label34.Text = "Xərclər";
            this.label34.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label33
            // 
            this.label33.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label33.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label33.Location = new System.Drawing.Point(3, 0);
            this.label33.Name = "label33";
            this.label33.Size = new System.Drawing.Size(233, 38);
            this.label33.TabIndex = 19;
            this.label33.Text = "Gəlir";
            this.label33.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lblYearToDateCommission
            // 
            this.lblYearToDateCommission.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblYearToDateCommission.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblYearToDateCommission.Location = new System.Drawing.Point(242, 212);
            this.lblYearToDateCommission.Name = "lblYearToDateCommission";
            this.lblYearToDateCommission.Size = new System.Drawing.Size(233, 22);
            this.lblYearToDateCommission.TabIndex = 18;
            this.lblYearToDateCommission.Text = "0 ₼";
            this.lblYearToDateCommission.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lblYearToDateProfit
            // 
            this.lblYearToDateProfit.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblYearToDateProfit.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblYearToDateProfit.ForeColor = System.Drawing.Color.Green;
            this.lblYearToDateProfit.Location = new System.Drawing.Point(3, 212);
            this.lblYearToDateProfit.Name = "lblYearToDateProfit";
            this.lblYearToDateProfit.Size = new System.Drawing.Size(233, 22);
            this.lblYearToDateProfit.TabIndex = 17;
            this.lblYearToDateProfit.Text = "0 ₼";
            this.lblYearToDateProfit.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lblYearToDateExpenses
            // 
            this.lblYearToDateExpenses.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblYearToDateExpenses.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblYearToDateExpenses.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.lblYearToDateExpenses.Location = new System.Drawing.Point(720, 192);
            this.lblYearToDateExpenses.Name = "lblYearToDateExpenses";
            this.lblYearToDateExpenses.Size = new System.Drawing.Size(235, 20);
            this.lblYearToDateExpenses.TabIndex = 16;
            this.lblYearToDateExpenses.Text = "0 ₼";
            this.lblYearToDateExpenses.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lblYearToDateRevenue
            // 
            this.lblYearToDateRevenue.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblYearToDateRevenue.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblYearToDateRevenue.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(64)))), ((int)(((byte)(0)))));
            this.lblYearToDateRevenue.Location = new System.Drawing.Point(3, 192);
            this.lblYearToDateRevenue.Name = "lblYearToDateRevenue";
            this.lblYearToDateRevenue.Size = new System.Drawing.Size(233, 20);
            this.lblYearToDateRevenue.TabIndex = 15;
            this.lblYearToDateRevenue.Text = "0 ₼";
            this.lblYearToDateRevenue.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label28
            // 
            this.label28.AutoSize = true;
            this.label28.Location = new System.Drawing.Point(3, 134);
            this.label28.Name = "label28";
            this.label28.Size = new System.Drawing.Size(0, 13);
            this.label28.TabIndex = 14;
            // 
            // lblPreviousMonthCommission
            // 
            this.lblPreviousMonthCommission.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblPreviousMonthCommission.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblPreviousMonthCommission.Location = new System.Drawing.Point(720, 134);
            this.lblPreviousMonthCommission.Name = "lblPreviousMonthCommission";
            this.lblPreviousMonthCommission.Size = new System.Drawing.Size(235, 58);
            this.lblPreviousMonthCommission.TabIndex = 13;
            this.lblPreviousMonthCommission.Text = "0 ₼";
            this.lblPreviousMonthCommission.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lblPreviousMonthProfit
            // 
            this.lblPreviousMonthProfit.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblPreviousMonthProfit.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblPreviousMonthProfit.ForeColor = System.Drawing.Color.Green;
            this.lblPreviousMonthProfit.Location = new System.Drawing.Point(481, 134);
            this.lblPreviousMonthProfit.Name = "lblPreviousMonthProfit";
            this.lblPreviousMonthProfit.Size = new System.Drawing.Size(233, 58);
            this.lblPreviousMonthProfit.TabIndex = 12;
            this.lblPreviousMonthProfit.Text = "0 ₼";
            this.lblPreviousMonthProfit.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lblPreviousMonthExpenses
            // 
            this.lblPreviousMonthExpenses.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblPreviousMonthExpenses.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblPreviousMonthExpenses.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.lblPreviousMonthExpenses.Location = new System.Drawing.Point(242, 134);
            this.lblPreviousMonthExpenses.Name = "lblPreviousMonthExpenses";
            this.lblPreviousMonthExpenses.Size = new System.Drawing.Size(233, 58);
            this.lblPreviousMonthExpenses.TabIndex = 11;
            this.lblPreviousMonthExpenses.Text = "0 ₼";
            this.lblPreviousMonthExpenses.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lblPreviousMonthRevenue
            // 
            this.lblPreviousMonthRevenue.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblPreviousMonthRevenue.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblPreviousMonthRevenue.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(64)))), ((int)(((byte)(0)))));
            this.lblPreviousMonthRevenue.Location = new System.Drawing.Point(481, 86);
            this.lblPreviousMonthRevenue.Name = "lblPreviousMonthRevenue";
            this.lblPreviousMonthRevenue.Size = new System.Drawing.Size(233, 48);
            this.lblPreviousMonthRevenue.TabIndex = 10;
            this.lblPreviousMonthRevenue.Text = "0 ₼";
            this.lblPreviousMonthRevenue.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label23
            // 
            this.label23.AutoSize = true;
            this.label23.Location = new System.Drawing.Point(242, 38);
            this.label23.Name = "label23";
            this.label23.Size = new System.Drawing.Size(0, 13);
            this.label23.TabIndex = 9;
            // 
            // lblCurrentMonthCommission
            // 
            this.lblCurrentMonthCommission.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblCurrentMonthCommission.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblCurrentMonthCommission.Location = new System.Drawing.Point(242, 86);
            this.lblCurrentMonthCommission.Name = "lblCurrentMonthCommission";
            this.lblCurrentMonthCommission.Size = new System.Drawing.Size(233, 48);
            this.lblCurrentMonthCommission.TabIndex = 8;
            this.lblCurrentMonthCommission.Text = "0 ₼";
            this.lblCurrentMonthCommission.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lblCurrentMonthProfit
            // 
            this.lblCurrentMonthProfit.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblCurrentMonthProfit.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblCurrentMonthProfit.ForeColor = System.Drawing.Color.Green;
            this.lblCurrentMonthProfit.Location = new System.Drawing.Point(3, 86);
            this.lblCurrentMonthProfit.Name = "lblCurrentMonthProfit";
            this.lblCurrentMonthProfit.Size = new System.Drawing.Size(233, 48);
            this.lblCurrentMonthProfit.TabIndex = 7;
            this.lblCurrentMonthProfit.Text = "0 ₼";
            this.lblCurrentMonthProfit.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lblCurrentMonthExpenses
            // 
            this.lblCurrentMonthExpenses.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblCurrentMonthExpenses.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblCurrentMonthExpenses.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.lblCurrentMonthExpenses.Location = new System.Drawing.Point(720, 38);
            this.lblCurrentMonthExpenses.Name = "lblCurrentMonthExpenses";
            this.lblCurrentMonthExpenses.Size = new System.Drawing.Size(235, 48);
            this.lblCurrentMonthExpenses.TabIndex = 6;
            this.lblCurrentMonthExpenses.Text = "0 ₼";
            this.lblCurrentMonthExpenses.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lblCurrentMonthRevenue
            // 
            this.lblCurrentMonthRevenue.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblCurrentMonthRevenue.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblCurrentMonthRevenue.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(64)))), ((int)(((byte)(0)))));
            this.lblCurrentMonthRevenue.Location = new System.Drawing.Point(481, 38);
            this.lblCurrentMonthRevenue.Name = "lblCurrentMonthRevenue";
            this.lblCurrentMonthRevenue.Size = new System.Drawing.Size(233, 48);
            this.lblCurrentMonthRevenue.TabIndex = 5;
            this.lblCurrentMonthRevenue.Text = "0 ₼";
            this.lblCurrentMonthRevenue.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label20
            // 
            this.label20.AutoSize = true;
            this.label20.Location = new System.Drawing.Point(481, 192);
            this.label20.Name = "label20";
            this.label20.Size = new System.Drawing.Size(0, 13);
            this.label20.TabIndex = 4;
            // 
            // lblYearToDateTitle
            // 
            this.lblYearToDateTitle.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblYearToDateTitle.Location = new System.Drawing.Point(242, 192);
            this.lblYearToDateTitle.Name = "lblYearToDateTitle";
            this.lblYearToDateTitle.Size = new System.Drawing.Size(233, 20);
            this.lblYearToDateTitle.TabIndex = 3;
            this.lblYearToDateTitle.Text = "İlin əvvəlindən";
            this.lblYearToDateTitle.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            // 
            // lblPreviousMonthTitle
            // 
            this.lblPreviousMonthTitle.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblPreviousMonthTitle.Location = new System.Drawing.Point(720, 86);
            this.lblPreviousMonthTitle.Name = "lblPreviousMonthTitle";
            this.lblPreviousMonthTitle.Size = new System.Drawing.Size(233, 24);
            this.lblPreviousMonthTitle.TabIndex = 2;
            this.lblPreviousMonthTitle.Text = "Əvvəlki ay";
            this.lblPreviousMonthTitle.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            // 
            // lblCurrentMonthTitle
            // 
            this.lblCurrentMonthTitle.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblCurrentMonthTitle.Location = new System.Drawing.Point(3, 38);
            this.lblCurrentMonthTitle.Name = "lblCurrentMonthTitle";
            this.lblCurrentMonthTitle.Size = new System.Drawing.Size(233, 24);
            this.lblCurrentMonthTitle.TabIndex = 0;
            this.lblCurrentMonthTitle.Text = "Cari ay";
            this.lblCurrentMonthTitle.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            // 
            // FinancialManagementForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(984, 640);
            this.Controls.Add(this.panelMain);
            this.Controls.Add(this.panelBottom);
            this.Controls.Add(this.panelTop);
            this.Name = "FinancialManagementForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Maliyyə İdarəetməsi";
            this.Load += new System.EventHandler(this.FinancialManagementForm_Load);
            this.panelTop.ResumeLayout(false);
            this.panelTop.PerformLayout();
            this.panelBottom.ResumeLayout(false);
            this.panelBottom.PerformLayout();
            this.panelMain.ResumeLayout(false);
            this.tabControl.ResumeLayout(false);
            this.tabTransactions.ResumeLayout(false);
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).EndInit();
            this.splitContainer1.ResumeLayout(false);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.panelTransactionButtons.ResumeLayout(false);
            this.tabExpenses.ResumeLayout(false);
            this.splitContainer2.Panel1.ResumeLayout(false);
            this.splitContainer2.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer2)).EndInit();
            this.splitContainer2.ResumeLayout(false);
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.panelExpenseButtons.ResumeLayout(false);
            this.tabSummary.ResumeLayout(false);
            this.tableLayoutPanel1.ResumeLayout(false);
            this.groupBox5.ResumeLayout(false);
            this.groupBox4.ResumeLayout(false);
            this.groupBox3.ResumeLayout(false);
            this.tableLayoutPanel2.ResumeLayout(false);
            this.tableLayoutPanel2.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panelTop;
        private System.Windows.Forms.Panel panelBottom;
        private System.Windows.Forms.Panel panelMain;
        private System.Windows.Forms.Label lblTitle;
        private System.Windows.Forms.Button btnRefresh;
        private System.Windows.Forms.ProgressBar progressBar;
        private System.Windows.Forms.Label lblStatus;
        private System.Windows.Forms.TabControl tabControl;
        private System.Windows.Forms.TabPage tabTransactions;
        private System.Windows.Forms.TabPage tabExpenses;
        private System.Windows.Forms.TabPage tabSummary;
        private System.Windows.Forms.SplitContainer splitContainer1;
        private System.Windows.Forms.ListView lvTransactions;
        private System.Windows.Forms.Panel panelTransactionButtons;
        private System.Windows.Forms.Button btnCancelTransaction;
        private System.Windows.Forms.Button btnSaveTransaction;
        private System.Windows.Forms.Button btnDeleteTransaction;
        private System.Windows.Forms.Button btnEditTransaction;
        private System.Windows.Forms.Button btnAddTransaction;
        private System.Windows.Forms.ColumnHeader colTransactionDate;
        private System.Windows.Forms.ColumnHeader colTransactionType;
        private System.Windows.Forms.ColumnHeader colEmployee;
        private System.Windows.Forms.ColumnHeader colClient;
        private System.Windows.Forms.ColumnHeader colProperty;
        private System.Windows.Forms.ColumnHeader colAmount;
        private System.Windows.Forms.ColumnHeader colCommission;
        private System.Windows.Forms.ColumnHeader colPaymentMethod;
        private System.Windows.Forms.ColumnHeader colStatus;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.ComboBox cmbProperty;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.ComboBox cmbClient;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.ComboBox cmbEmployee;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.RichTextBox rtbTransactionDescription;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.CheckBox chkIsSuccessful;
        private System.Windows.Forms.TextBox txtReferenceNumber;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.ComboBox cmbPaymentMethod;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.TextBox txtCommissionAmount;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox txtAmount;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.ComboBox cmbTransactionType;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.DateTimePicker dtpTransactionDate;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.SplitContainer splitContainer2;
        private System.Windows.Forms.ListView lvExpenses;
        private System.Windows.Forms.Panel panelExpenseButtons;
        private System.Windows.Forms.Button btnCancelExpense;
        private System.Windows.Forms.Button btnSaveExpense;
        private System.Windows.Forms.Button btnDeleteExpense;
        private System.Windows.Forms.Button btnEditExpense;
        private System.Windows.Forms.Button btnAddExpense;
        private System.Windows.Forms.ColumnHeader colExpenseDate;
        private System.Windows.Forms.ColumnHeader colExpenseTitle;
        private System.Windows.Forms.ColumnHeader colExpenseCategory;
        private System.Windows.Forms.ColumnHeader colExpenseAmount;
        private System.Windows.Forms.ColumnHeader colExpenseEmployee;
        private System.Windows.Forms.ColumnHeader colExpenseStatus;
        private System.Windows.Forms.ColumnHeader colExpensePaymentMethod;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.ComboBox cmbExpenseEmployee;
        private System.Windows.Forms.Label label19;
        private System.Windows.Forms.DateTimePicker dtpExpensePaymentDate;
        private System.Windows.Forms.Label label18;
        private System.Windows.Forms.CheckBox chkExpenseIsPaid;
        private System.Windows.Forms.TextBox txtExpenseReference;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.ComboBox cmbExpensePaymentMethod;
        private System.Windows.Forms.Label label12;
        private System.Windows.Forms.RichTextBox rtbExpenseDescription;
        private System.Windows.Forms.Label label13;
        private System.Windows.Forms.TextBox txtExpenseAmount;
        private System.Windows.Forms.Label label14;
        private System.Windows.Forms.DateTimePicker dtpExpenseDate;
        private System.Windows.Forms.Label label15;
        private System.Windows.Forms.ComboBox cmbExpenseCategory;
        private System.Windows.Forms.Label label16;
        private System.Windows.Forms.TextBox txtExpenseTitle;
        private System.Windows.Forms.Label label17;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.GroupBox groupBox4;
        private System.Windows.Forms.GroupBox groupBox5;
        private System.Windows.Forms.ListView lvExpenseCategories;
        private System.Windows.Forms.ListView lvEmployeeCommissions;
        private System.Windows.Forms.ColumnHeader colEmployeeName;
        private System.Windows.Forms.ColumnHeader colTransactionCount;
        private System.Windows.Forms.ColumnHeader colTotalTransactions;
        private System.Windows.Forms.ColumnHeader colTotalCommission;
        private System.Windows.Forms.ColumnHeader colCategoryName;
        private System.Windows.Forms.ColumnHeader colCategoryTotal;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel2;
        private System.Windows.Forms.Label lblCurrentMonthTitle;
        private System.Windows.Forms.Label lblPreviousMonthTitle;
        private System.Windows.Forms.Label lblYearToDateTitle;
        private System.Windows.Forms.Label label20;
        private System.Windows.Forms.Label lblCurrentMonthRevenue;
        private System.Windows.Forms.Label lblCurrentMonthExpenses;
        private System.Windows.Forms.Label lblCurrentMonthProfit;
        private System.Windows.Forms.Label lblCurrentMonthCommission;
        private System.Windows.Forms.Label label23;
        private System.Windows.Forms.Label lblPreviousMonthRevenue;
        private System.Windows.Forms.Label lblPreviousMonthExpenses;
        private System.Windows.Forms.Label lblPreviousMonthProfit;
        private System.Windows.Forms.Label lblPreviousMonthCommission;
        private System.Windows.Forms.Label label28;
        private System.Windows.Forms.Label lblYearToDateRevenue;
        private System.Windows.Forms.Label lblYearToDateExpenses;
        private System.Windows.Forms.Label lblYearToDateProfit;
        private System.Windows.Forms.Label lblYearToDateCommission;
        private System.Windows.Forms.Label label33;
        private System.Windows.Forms.Label label34;
        private System.Windows.Forms.Label label35;
        private System.Windows.Forms.Label label36;
    }
}
