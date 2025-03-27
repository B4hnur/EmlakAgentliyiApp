namespace DashinmazEmlakApp.Forms
{
    partial class FinancialReportForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FinancialReportForm));
            this.tabControl = new System.Windows.Forms.TabControl();
            this.tabSummary = new System.Windows.Forms.TabPage();
            this.lblStatus = new System.Windows.Forms.Label();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.lblRentalsCount = new System.Windows.Forms.Label();
            this.lblSalesCount = new System.Windows.Forms.Label();
            this.label13 = new System.Windows.Forms.Label();
            this.label12 = new System.Windows.Forms.Label();
            this.lvwTopEmployees = new System.Windows.Forms.ListView();
            this.colEmployee = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.colTransactionCount = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.colEmployeeCommission = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.label11 = new System.Windows.Forms.Label();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.lblNetProfit = new System.Windows.Forms.Label();
            this.lblTotalSalaries = new System.Windows.Forms.Label();
            this.lblTotalExpenses = new System.Windows.Forms.Label();
            this.lblTotalCommission = new System.Windows.Forms.Label();
            this.lblRentalRevenue = new System.Windows.Forms.Label();
            this.lblSalesRevenue = new System.Windows.Forms.Label();
            this.lblPeriod = new System.Windows.Forms.Label();
            this.label10 = new System.Windows.Forms.Label();
            this.label9 = new System.Windows.Forms.Label();
            this.label8 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.panel1 = new System.Windows.Forms.Panel();
            this.btnGenerateReport = new System.Windows.Forms.Button();
            this.dtpEndDate = new System.Windows.Forms.DateTimePicker();
            this.label3 = new System.Windows.Forms.Label();
            this.dtpStartDate = new System.Windows.Forms.DateTimePicker();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.tabTransactions = new System.Windows.Forms.TabPage();
            this.lblTransStatus = new System.Windows.Forms.Label();
            this.lvwTransactions = new System.Windows.Forms.ListView();
            this.colDate = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.colType = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.colProperty = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.colAddress = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.colClient = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.colEmp = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.colAmount = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.colCommission = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.panel2 = new System.Windows.Forms.Panel();
            this.btnLoadTransactions = new System.Windows.Forms.Button();
            this.dtpTransEndDate = new System.Windows.Forms.DateTimePicker();
            this.label14 = new System.Windows.Forms.Label();
            this.dtpTransStartDate = new System.Windows.Forms.DateTimePicker();
            this.label15 = new System.Windows.Forms.Label();
            this.label16 = new System.Windows.Forms.Label();
            this.tabExpenses = new System.Windows.Forms.TabPage();
            this.lblTotalExpensesAmount = new System.Windows.Forms.Label();
            this.label19 = new System.Windows.Forms.Label();
            this.label18 = new System.Windows.Forms.Label();
            this.lvwExpensesByCategory = new System.Windows.Forms.ListView();
            this.colCategory = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.colTotal = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.lblExpensesStatus = new System.Windows.Forms.Label();
            this.lvwExpenses = new System.Windows.Forms.ListView();
            this.colExpDate = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.colExpCategory = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.colDescription = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.colExpEmployee = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.colExpAmount = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.panel3 = new System.Windows.Forms.Panel();
            this.btnLoadExpenses = new System.Windows.Forms.Button();
            this.dtpExpenseEndDate = new System.Windows.Forms.DateTimePicker();
            this.label17 = new System.Windows.Forms.Label();
            this.dtpExpenseStartDate = new System.Windows.Forms.DateTimePicker();
            this.label20 = new System.Windows.Forms.Label();
            this.label21 = new System.Windows.Forms.Label();
            this.tabControl.SuspendLayout();
            this.tabSummary.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.panel1.SuspendLayout();
            this.tabTransactions.SuspendLayout();
            this.panel2.SuspendLayout();
            this.tabExpenses.SuspendLayout();
            this.panel3.SuspendLayout();
            this.SuspendLayout();
            // 
            // tabControl
            // 
            this.tabControl.Controls.Add(this.tabSummary);
            this.tabControl.Controls.Add(this.tabTransactions);
            this.tabControl.Controls.Add(this.tabExpenses);
            this.tabControl.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tabControl.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tabControl.Location = new System.Drawing.Point(0, 0);
            this.tabControl.Name = "tabControl";
            this.tabControl.SelectedIndex = 0;
            this.tabControl.Size = new System.Drawing.Size(884, 661);
            this.tabControl.TabIndex = 0;
            this.tabControl.SelectedIndexChanged += new System.EventHandler(this.TabControl_SelectedIndexChanged);
            // 
            // tabSummary
            // 
            this.tabSummary.Controls.Add(this.lblStatus);
            this.tabSummary.Controls.Add(this.groupBox2);
            this.tabSummary.Controls.Add(this.groupBox1);
            this.tabSummary.Controls.Add(this.panel1);
            this.tabSummary.Location = new System.Drawing.Point(4, 26);
            this.tabSummary.Name = "tabSummary";
            this.tabSummary.Padding = new System.Windows.Forms.Padding(3);
            this.tabSummary.Size = new System.Drawing.Size(876, 631);
            this.tabSummary.TabIndex = 0;
            this.tabSummary.Text = "Ümumi Məlumat";
            this.tabSummary.UseVisualStyleBackColor = true;
            // 
            // lblStatus
            // 
            this.lblStatus.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.lblStatus.AutoSize = true;
            this.lblStatus.Location = new System.Drawing.Point(8, 608);
            this.lblStatus.Name = "lblStatus";
            this.lblStatus.Size = new System.Drawing.Size(43, 17);
            this.lblStatus.TabIndex = 3;
            this.lblStatus.Text = "Hazır";
            // 
            // groupBox2
            // 
            this.groupBox2.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
            | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBox2.Controls.Add(this.lblRentalsCount);
            this.groupBox2.Controls.Add(this.lblSalesCount);
            this.groupBox2.Controls.Add(this.label13);
            this.groupBox2.Controls.Add(this.label12);
            this.groupBox2.Controls.Add(this.lvwTopEmployees);
            this.groupBox2.Controls.Add(this.label11);
            this.groupBox2.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.groupBox2.Location = new System.Drawing.Point(587, 101);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(281, 504);
            this.groupBox2.TabIndex = 2;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Statistika";
            // 
            // lblRentalsCount
            // 
            this.lblRentalsCount.AutoSize = true;
            this.lblRentalsCount.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblRentalsCount.Location = new System.Drawing.Point(6, 78);
            this.lblRentalsCount.Name = "lblRentalsCount";
            this.lblRentalsCount.Size = new System.Drawing.Size(126, 17);
            this.lblRentalsCount.TabIndex = 5;
            this.lblRentalsCount.Text = "0 kirayə əməliyyatı";
            // 
            // lblSalesCount
            // 
            this.lblSalesCount.AutoSize = true;
            this.lblSalesCount.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblSalesCount.Location = new System.Drawing.Point(6, 54);
            this.lblSalesCount.Name = "lblSalesCount";
            this.lblSalesCount.Size = new System.Drawing.Size(113, 17);
            this.lblSalesCount.TabIndex = 4;
            this.lblSalesCount.Text = "0 satış əməliyyatı";
            // 
            // label13
            // 
            this.label13.AutoSize = true;
            this.label13.Location = new System.Drawing.Point(6, 28);
            this.label13.Name = "label13";
            this.label13.Size = new System.Drawing.Size(132, 17);
            this.label13.TabIndex = 3;
            this.label13.Text = "Əməliyyat Statistikası";
            // 
            // label12
            // 
            this.label12.AutoSize = true;
            this.label12.Location = new System.Drawing.Point(6, 115);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(81, 17);
            this.label12.TabIndex = 2;
            this.label12.Text = "Ən Yaxşı İşçilər";
            // 
            // lvwTopEmployees
            // 
            this.lvwTopEmployees.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
            | System.Windows.Forms.AnchorStyles.Left)
            | System.Windows.Forms.AnchorStyles.Right)));
            this.lvwTopEmployees.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.colEmployee,
            this.colTransactionCount,
            this.colEmployeeCommission});
            this.lvwTopEmployees.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lvwTopEmployees.FullRowSelect = true;
            this.lvwTopEmployees.HideSelection = false;
            this.lvwTopEmployees.Location = new System.Drawing.Point(9, 135);
            this.lvwTopEmployees.Name = "lvwTopEmployees";
            this.lvwTopEmployees.Size = new System.Drawing.Size(266, 363);
            this.lvwTopEmployees.TabIndex = 1;
            this.lvwTopEmployees.UseCompatibleStateImageBehavior = false;
            this.lvwTopEmployees.View = System.Windows.Forms.View.Details;
            // 
            // colEmployee
            // 
            this.colEmployee.Text = "İşçi";
            this.colEmployee.Width = 120;
            // 
            // colTransactionCount
            // 
            this.colTransactionCount.Text = "Sayı";
            this.colTransactionCount.Width = 40;
            // 
            // colEmployeeCommission
            // 
            this.colEmployeeCommission.Text = "Komissiya";
            this.colEmployeeCommission.Width = 100;
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.Location = new System.Drawing.Point(78, 108);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(0, 17);
            this.label11.TabIndex = 0;
            // 
            // groupBox1
            // 
            this.groupBox1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
            | System.Windows.Forms.AnchorStyles.Left)
            | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBox1.Controls.Add(this.lblNetProfit);
            this.groupBox1.Controls.Add(this.lblTotalSalaries);
            this.groupBox1.Controls.Add(this.lblTotalExpenses);
            this.groupBox1.Controls.Add(this.lblTotalCommission);
            this.groupBox1.Controls.Add(this.lblRentalRevenue);
            this.groupBox1.Controls.Add(this.lblSalesRevenue);
            this.groupBox1.Controls.Add(this.lblPeriod);
            this.groupBox1.Controls.Add(this.label10);
            this.groupBox1.Controls.Add(this.label9);
            this.groupBox1.Controls.Add(this.label8);
            this.groupBox1.Controls.Add(this.label7);
            this.groupBox1.Controls.Add(this.label6);
            this.groupBox1.Controls.Add(this.label5);
            this.groupBox1.Controls.Add(this.label4);
            this.groupBox1.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.groupBox1.Location = new System.Drawing.Point(8, 101);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(573, 504);
            this.groupBox1.TabIndex = 1;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Maliyyə Hesabatı";
            // 
            // lblNetProfit
            // 
            this.lblNetProfit.AutoSize = true;
            this.lblNetProfit.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblNetProfit.ForeColor = System.Drawing.Color.Green;
            this.lblNetProfit.Location = new System.Drawing.Point(155, 254);
            this.lblNetProfit.Name = "lblNetProfit";
            this.lblNetProfit.Size = new System.Drawing.Size(44, 21);
            this.lblNetProfit.TabIndex = 13;
            this.lblNetProfit.Text = "0 ₼";
            // 
            // lblTotalSalaries
            // 
            this.lblTotalSalaries.AutoSize = true;
            this.lblTotalSalaries.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblTotalSalaries.Location = new System.Drawing.Point(155, 225);
            this.lblTotalSalaries.Name = "lblTotalSalaries";
            this.lblTotalSalaries.Size = new System.Drawing.Size(31, 17);
            this.lblTotalSalaries.TabIndex = 12;
            this.lblTotalSalaries.Text = "0 ₼";
            // 
            // lblTotalExpenses
            // 
            this.lblTotalExpenses.AutoSize = true;
            this.lblTotalExpenses.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblTotalExpenses.Location = new System.Drawing.Point(155, 196);
            this.lblTotalExpenses.Name = "lblTotalExpenses";
            this.lblTotalExpenses.Size = new System.Drawing.Size(31, 17);
            this.lblTotalExpenses.TabIndex = 11;
            this.lblTotalExpenses.Text = "0 ₼";
            // 
            // lblTotalCommission
            // 
            this.lblTotalCommission.AutoSize = true;
            this.lblTotalCommission.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblTotalCommission.Location = new System.Drawing.Point(155, 167);
            this.lblTotalCommission.Name = "lblTotalCommission";
            this.lblTotalCommission.Size = new System.Drawing.Size(31, 17);
            this.lblTotalCommission.TabIndex = 10;
            this.lblTotalCommission.Text = "0 ₼";
            // 
            // lblRentalRevenue
            // 
            this.lblRentalRevenue.AutoSize = true;
            this.lblRentalRevenue.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblRentalRevenue.Location = new System.Drawing.Point(155, 138);
            this.lblRentalRevenue.Name = "lblRentalRevenue";
            this.lblRentalRevenue.Size = new System.Drawing.Size(31, 17);
            this.lblRentalRevenue.TabIndex = 9;
            this.lblRentalRevenue.Text = "0 ₼";
            // 
            // lblSalesRevenue
            // 
            this.lblSalesRevenue.AutoSize = true;
            this.lblSalesRevenue.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblSalesRevenue.Location = new System.Drawing.Point(155, 109);
            this.lblSalesRevenue.Name = "lblSalesRevenue";
            this.lblSalesRevenue.Size = new System.Drawing.Size(31, 17);
            this.lblSalesRevenue.TabIndex = 8;
            this.lblSalesRevenue.Text = "0 ₼";
            // 
            // lblPeriod
            // 
            this.lblPeriod.AutoSize = true;
            this.lblPeriod.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblPeriod.Location = new System.Drawing.Point(155, 65);
            this.lblPeriod.Name = "lblPeriod";
            this.lblPeriod.Size = new System.Drawing.Size(169, 17);
            this.lblPeriod.TabIndex = 7;
            this.lblPeriod.Text = "01.01.2023 - 31.01.2023";
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Location = new System.Drawing.Point(31, 254);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(100, 17);
            this.label10.TabIndex = 6;
            this.label10.Text = "Ümumi Qazanc:";
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(31, 225);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(120, 17);
            this.label9.TabIndex = 5;
            this.label9.Text = "İşçi əmək haqqları:";
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(31, 196);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(95, 17);
            this.label8.TabIndex = 4;
            this.label8.Text = "Ümumi xərclər:";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(31, 167);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(122, 17);
            this.label7.TabIndex = 3;
            this.label7.Text = "Ümumi komissiya:";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(31, 138);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(90, 17);
            this.label6.TabIndex = 2;
            this.label6.Text = "Kirayə gəliri:";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(31, 109);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(78, 17);
            this.label5.TabIndex = 1;
            this.label5.Text = "Satış gəliri:";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(31, 65);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(42, 17);
            this.label4.TabIndex = 0;
            this.label4.Text = "Dövr:";
            // 
            // panel1
            // 
            this.panel1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
            | System.Windows.Forms.AnchorStyles.Right)));
            this.panel1.BackColor = System.Drawing.Color.AliceBlue;
            this.panel1.Controls.Add(this.btnGenerateReport);
            this.panel1.Controls.Add(this.dtpEndDate);
            this.panel1.Controls.Add(this.label3);
            this.panel1.Controls.Add(this.dtpStartDate);
            this.panel1.Controls.Add(this.label2);
            this.panel1.Controls.Add(this.label1);
            this.panel1.Location = new System.Drawing.Point(8, 6);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(860, 89);
            this.panel1.TabIndex = 0;
            // 
            // btnGenerateReport
            // 
            this.btnGenerateReport.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnGenerateReport.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(45)))), ((int)(((byte)(52)))), ((int)(((byte)(71)))));
            this.btnGenerateReport.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnGenerateReport.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnGenerateReport.ForeColor = System.Drawing.Color.White;
            this.btnGenerateReport.Location = new System.Drawing.Point(695, 35);
            this.btnGenerateReport.Name = "btnGenerateReport";
            this.btnGenerateReport.Size = new System.Drawing.Size(147, 38);
            this.btnGenerateReport.TabIndex = 5;
            this.btnGenerateReport.Text = "Hesabatı Hazırla";
            this.btnGenerateReport.UseVisualStyleBackColor = false;
            this.btnGenerateReport.Click += new System.EventHandler(this.btnGenerateReport_Click);
            // 
            // dtpEndDate
            // 
            this.dtpEndDate.Location = new System.Drawing.Point(425, 48);
            this.dtpEndDate.Name = "dtpEndDate";
            this.dtpEndDate.Size = new System.Drawing.Size(200, 25);
            this.dtpEndDate.TabIndex = 4;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(422, 24);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(72, 17);
            this.label3.TabIndex = 3;
            this.label3.Text = "Bitmə tarixi";
            // 
            // dtpStartDate
            // 
            this.dtpStartDate.Location = new System.Drawing.Point(177, 48);
            this.dtpStartDate.Name = "dtpStartDate";
            this.dtpStartDate.Size = new System.Drawing.Size(200, 25);
            this.dtpStartDate.TabIndex = 2;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(174, 24);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(95, 17);
            this.label2.TabIndex = 1;
            this.label2.Text = "Başlanğıc tarixi";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(3, 13);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(145, 21);
            this.label1.TabIndex = 0;
            this.label1.Text = "Maliyyə Hesabatı";
            // 
            // tabTransactions
            // 
            this.tabTransactions.Controls.Add(this.lblTransStatus);
            this.tabTransactions.Controls.Add(this.lvwTransactions);
            this.tabTransactions.Controls.Add(this.panel2);
            this.tabTransactions.Location = new System.Drawing.Point(4, 26);
            this.tabTransactions.Name = "tabTransactions";
            this.tabTransactions.Padding = new System.Windows.Forms.Padding(3);
            this.tabTransactions.Size = new System.Drawing.Size(876, 631);
            this.tabTransactions.TabIndex = 1;
            this.tabTransactions.Text = "Əməliyyatlar";
            this.tabTransactions.UseVisualStyleBackColor = true;
            // 
            // lblTransStatus
            // 
            this.lblTransStatus.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.lblTransStatus.AutoSize = true;
            this.lblTransStatus.Location = new System.Drawing.Point(8, 608);
            this.lblTransStatus.Name = "lblTransStatus";
            this.lblTransStatus.Size = new System.Drawing.Size(43, 17);
            this.lblTransStatus.TabIndex = 5;
            this.lblTransStatus.Text = "Hazır";
            // 
            // lvwTransactions
            // 
            this.lvwTransactions.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
            | System.Windows.Forms.AnchorStyles.Left)
            | System.Windows.Forms.AnchorStyles.Right)));
            this.lvwTransactions.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.colDate,
            this.colType,
            this.colProperty,
            this.colAddress,
            this.colClient,
            this.colEmp,
            this.colAmount,
            this.colCommission});
            this.lvwTransactions.FullRowSelect = true;
            this.lvwTransactions.HideSelection = false;
            this.lvwTransactions.Location = new System.Drawing.Point(8, 101);
            this.lvwTransactions.Name = "lvwTransactions";
            this.lvwTransactions.Size = new System.Drawing.Size(860, 504);
            this.lvwTransactions.TabIndex = 1;
            this.lvwTransactions.UseCompatibleStateImageBehavior = false;
            this.lvwTransactions.View = System.Windows.Forms.View.Details;
            // 
            // colDate
            // 
            this.colDate.Text = "Tarix";
            this.colDate.Width = 80;
            // 
            // colType
            // 
            this.colType.Text = "Növü";
            this.colType.Width = 50;
            // 
            // colProperty
            // 
            this.colProperty.Text = "Əmlak";
            this.colProperty.Width = 150;
            // 
            // colAddress
            // 
            this.colAddress.Text = "Ünvan";
            this.colAddress.Width = 150;
            // 
            // colClient
            // 
            this.colClient.Text = "Müştəri";
            this.colClient.Width = 120;
            // 
            // colEmp
            // 
            this.colEmp.Text = "İşçi";
            this.colEmp.Width = 120;
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
            // panel2
            // 
            this.panel2.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
            | System.Windows.Forms.AnchorStyles.Right)));
            this.panel2.BackColor = System.Drawing.Color.AliceBlue;
            this.panel2.Controls.Add(this.btnLoadTransactions);
            this.panel2.Controls.Add(this.dtpTransEndDate);
            this.panel2.Controls.Add(this.label14);
            this.panel2.Controls.Add(this.dtpTransStartDate);
            this.panel2.Controls.Add(this.label15);
            this.panel2.Controls.Add(this.label16);
            this.panel2.Location = new System.Drawing.Point(8, 6);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(860, 89);
            this.panel2.TabIndex = 0;
            // 
            // btnLoadTransactions
            // 
            this.btnLoadTransactions.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnLoadTransactions.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(45)))), ((int)(((byte)(52)))), ((int)(((byte)(71)))));
            this.btnLoadTransactions.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnLoadTransactions.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnLoadTransactions.ForeColor = System.Drawing.Color.White;
            this.btnLoadTransactions.Location = new System.Drawing.Point(695, 35);
            this.btnLoadTransactions.Name = "btnLoadTransactions";
            this.btnLoadTransactions.Size = new System.Drawing.Size(147, 38);
            this.btnLoadTransactions.TabIndex = 5;
            this.btnLoadTransactions.Text = "Yüklə";
            this.btnLoadTransactions.UseVisualStyleBackColor = false;
            this.btnLoadTransactions.Click += new System.EventHandler(this.btnLoadTransactions_Click);
            // 
            // dtpTransEndDate
            // 
            this.dtpTransEndDate.Location = new System.Drawing.Point(425, 48);
            this.dtpTransEndDate.Name = "dtpTransEndDate";
            this.dtpTransEndDate.Size = new System.Drawing.Size(200, 25);
            this.dtpTransEndDate.TabIndex = 4;
            // 
            // label14
            // 
            this.label14.AutoSize = true;
            this.label14.Location = new System.Drawing.Point(422, 24);
            this.label14.Name = "label14";
            this.label14.Size = new System.Drawing.Size(72, 17);
            this.label14.TabIndex = 3;
            this.label14.Text = "Bitmə tarixi";
            // 
            // dtpTransStartDate
            // 
            this.dtpTransStartDate.Location = new System.Drawing.Point(177, 48);
            this.dtpTransStartDate.Name = "dtpTransStartDate";
            this.dtpTransStartDate.Size = new System.Drawing.Size(200, 25);
            this.dtpTransStartDate.TabIndex = 2;
            // 
            // label15
            // 
            this.label15.AutoSize = true;
            this.label15.Location = new System.Drawing.Point(174, 24);
            this.label15.Name = "label15";
            this.label15.Size = new System.Drawing.Size(95, 17);
            this.label15.TabIndex = 1;
            this.label15.Text = "Başlanğıc tarixi";
            // 
            // label16
            // 
            this.label16.AutoSize = true;
            this.label16.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label16.Location = new System.Drawing.Point(3, 13);
            this.label16.Name = "label16";
            this.label16.Size = new System.Drawing.Size(106, 21);
            this.label16.TabIndex = 0;
            this.label16.Text = "Əməliyyatlar";
            // 
            // tabExpenses
            // 
            this.tabExpenses.Controls.Add(this.lblTotalExpensesAmount);
            this.tabExpenses.Controls.Add(this.label19);
            this.tabExpenses.Controls.Add(this.label18);
            this.tabExpenses.Controls.Add(this.lvwExpensesByCategory);
            this.tabExpenses.Controls.Add(this.lblExpensesStatus);
            this.tabExpenses.Controls.Add(this.lvwExpenses);
            this.tabExpenses.Controls.Add(this.panel3);
            this.tabExpenses.Location = new System.Drawing.Point(4, 26);
            this.tabExpenses.Name = "tabExpenses";
            this.tabExpenses.Size = new System.Drawing.Size(876, 631);
            this.tabExpenses.TabIndex = 2;
            this.tabExpenses.Text = "Xərclər";
            this.tabExpenses.UseVisualStyleBackColor = true;
            // 
            // lblTotalExpensesAmount
            // 
            this.lblTotalExpensesAmount.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.lblTotalExpensesAmount.Font = new System.Drawing.Font("Segoe UI", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblTotalExpensesAmount.Location = new System.Drawing.Point(664, 608);
            this.lblTotalExpensesAmount.Name = "lblTotalExpensesAmount";
            this.lblTotalExpensesAmount.Size = new System.Drawing.Size(204, 20);
            this.lblTotalExpensesAmount.TabIndex = 11;
            this.lblTotalExpensesAmount.Text = "0 ₼";
            this.lblTotalExpensesAmount.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // label19
            // 
            this.label19.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.label19.AutoSize = true;
            this.label19.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label19.Location = new System.Drawing.Point(547, 608);
            this.label19.Name = "label19";
            this.label19.Size = new System.Drawing.Size(111, 17);
            this.label19.TabIndex = 10;
            this.label19.Text = "Ümumi xərclər:";
            // 
            // label18
            // 
            this.label18.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.label18.AutoSize = true;
            this.label18.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label18.Location = new System.Drawing.Point(664, 101);
            this.label18.Name = "label18";
            this.label18.Size = new System.Drawing.Size(143, 17);
            this.label18.TabIndex = 9;
            this.label18.Text = "Kateqoriyaya görə xərclər";
            // 
            // lvwExpensesByCategory
            // 
            this.lvwExpensesByCategory.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
            | System.Windows.Forms.AnchorStyles.Right)));
            this.lvwExpensesByCategory.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.colCategory,
            this.colTotal});
            this.lvwExpensesByCategory.FullRowSelect = true;
            this.lvwExpensesByCategory.HideSelection = false;
            this.lvwExpensesByCategory.Location = new System.Drawing.Point(664, 121);
            this.lvwExpensesByCategory.Name = "lvwExpensesByCategory";
            this.lvwExpensesByCategory.Size = new System.Drawing.Size(204, 484);
            this.lvwExpensesByCategory.TabIndex = 8;
            this.lvwExpensesByCategory.UseCompatibleStateImageBehavior = false;
            this.lvwExpensesByCategory.View = System.Windows.Forms.View.Details;
            // 
            // colCategory
            // 
            this.colCategory.Text = "Kateqoriya";
            this.colCategory.Width = 120;
            // 
            // colTotal
            // 
            this.colTotal.Text = "Məbləğ";
            this.colTotal.Width = 80;
            // 
            // lblExpensesStatus
            // 
            this.lblExpensesStatus.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.lblExpensesStatus.AutoSize = true;
            this.lblExpensesStatus.Location = new System.Drawing.Point(8, 608);
            this.lblExpensesStatus.Name = "lblExpensesStatus";
            this.lblExpensesStatus.Size = new System.Drawing.Size(43, 17);
            this.lblExpensesStatus.TabIndex = 7;
            this.lblExpensesStatus.Text = "Hazır";
            // 
            // lvwExpenses
            // 
            this.lvwExpenses.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
            | System.Windows.Forms.AnchorStyles.Left)
            | System.Windows.Forms.AnchorStyles.Right)));
            this.lvwExpenses.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.colExpDate,
            this.colExpCategory,
            this.colDescription,
            this.colExpEmployee,
            this.colExpAmount});
            this.lvwExpenses.FullRowSelect = true;
            this.lvwExpenses.HideSelection = false;
            this.lvwExpenses.Location = new System.Drawing.Point(8, 101);
            this.lvwExpenses.Name = "lvwExpenses";
            this.lvwExpenses.Size = new System.Drawing.Size(650, 504);
            this.lvwExpenses.TabIndex = 6;
            this.lvwExpenses.UseCompatibleStateImageBehavior = false;
            this.lvwExpenses.View = System.Windows.Forms.View.Details;
            // 
            // colExpDate
            // 
            this.colExpDate.Text = "Tarix";
            this.colExpDate.Width = 80;
            // 
            // colExpCategory
            // 
            this.colExpCategory.Text = "Kateqoriya";
            this.colExpCategory.Width = 100;
            // 
            // colDescription
            // 
            this.colDescription.Text = "Təsvir";
            this.colDescription.Width = 200;
            // 
            // colExpEmployee
            // 
            this.colExpEmployee.Text = "İşçi";
            this.colExpEmployee.Width = 120;
            // 
            // colExpAmount
            // 
            this.colExpAmount.Text = "Məbləğ";
            this.colExpAmount.Width = 80;
            // 
            // panel3
            // 
            this.panel3.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
            | System.Windows.Forms.AnchorStyles.Right)));
            this.panel3.BackColor = System.Drawing.Color.AliceBlue;
            this.panel3.Controls.Add(this.btnLoadExpenses);
            this.panel3.Controls.Add(this.dtpExpenseEndDate);
            this.panel3.Controls.Add(this.label17);
            this.panel3.Controls.Add(this.dtpExpenseStartDate);
            this.panel3.Controls.Add(this.label20);
            this.panel3.Controls.Add(this.label21);
            this.panel3.Location = new System.Drawing.Point(8, 6);
            this.panel3.Name = "panel3";
            this.panel3.Size = new System.Drawing.Size(860, 89);
            this.panel3.TabIndex = 1;
            // 
            // btnLoadExpenses
            // 
            this.btnLoadExpenses.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnLoadExpenses.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(45)))), ((int)(((byte)(52)))), ((int)(((byte)(71)))));
            this.btnLoadExpenses.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnLoadExpenses.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnLoadExpenses.ForeColor = System.Drawing.Color.White;
            this.btnLoadExpenses.Location = new System.Drawing.Point(695, 35);
            this.btnLoadExpenses.Name = "btnLoadExpenses";
            this.btnLoadExpenses.Size = new System.Drawing.Size(147, 38);
            this.btnLoadExpenses.TabIndex = 5;
            this.btnLoadExpenses.Text = "Yüklə";
            this.btnLoadExpenses.UseVisualStyleBackColor = false;
            this.btnLoadExpenses.Click += new System.EventHandler(this.btnLoadExpenses_Click);
            // 
            // dtpExpenseEndDate
            // 
            this.dtpExpenseEndDate.Location = new System.Drawing.Point(425, 48);
            this.dtpExpenseEndDate.Name = "dtpExpenseEndDate";
            this.dtpExpenseEndDate.Size = new System.Drawing.Size(200, 25);
            this.dtpExpenseEndDate.TabIndex = 4;
            // 
            // label17
            // 
            this.label17.AutoSize = true;
            this.label17.Location = new System.Drawing.Point(422, 24);
            this.label17.Name = "label17";
            this.label17.Size = new System.Drawing.Size(72, 17);
            this.label17.TabIndex = 3;
            this.label17.Text = "Bitmə tarixi";
            // 
            // dtpExpenseStartDate
            // 
            this.dtpExpenseStartDate.Location = new System.Drawing.Point(177, 48);
            this.dtpExpenseStartDate.Name = "dtpExpenseStartDate";
            this.dtpExpenseStartDate.Size = new System.Drawing.Size(200, 25);
            this.dtpExpenseStartDate.TabIndex = 2;
            // 
            // label20
            // 
            this.label20.AutoSize = true;
            this.label20.Location = new System.Drawing.Point(174, 24);
            this.label20.Name = "label20";
            this.label20.Size = new System.Drawing.Size(95, 17);
            this.label20.TabIndex = 1;
            this.label20.Text = "Başlanğıc tarixi";
            // 
            // label21
            // 
            this.label21.AutoSize = true;
            this.label21.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label21.Location = new System.Drawing.Point(3, 13);
            this.label21.Name = "label21";
            this.label21.Size = new System.Drawing.Size(62, 21);
            this.label21.TabIndex = 0;
            this.label21.Text = "Xərclər";
            // 
            // FinancialReportForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(884, 661);
            this.Controls.Add(this.tabControl);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MinimumSize = new System.Drawing.Size(900, 700);
            this.Name = "FinancialReportForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Maliyyə Hesabatları";
            this.Load += new System.EventHandler(this.FinancialReportForm_Load);
            this.tabControl.ResumeLayout(false);
            this.tabSummary.ResumeLayout(false);
            this.tabSummary.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.tabTransactions.ResumeLayout(false);
            this.tabTransactions.PerformLayout();
            this.panel2.ResumeLayout(false);
            this.panel2.PerformLayout();
            this.tabExpenses.ResumeLayout(false);
            this.tabExpenses.PerformLayout();
            this.panel3.ResumeLayout(false);
            this.panel3.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TabControl tabControl;
        private System.Windows.Forms.TabPage tabSummary;
        private System.Windows.Forms.TabPage tabTransactions;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.TabPage tabExpenses;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.DateTimePicker dtpStartDate;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.DateTimePicker dtpEndDate;
        private System.Windows.Forms.Button btnGenerateReport;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.Label lblPeriod;
        private System.Windows.Forms.Label lblSalesRevenue;
        private System.Windows.Forms.Label lblRentalRevenue;
        private System.Windows.Forms.Label lblTotalCommission;
        private System.Windows.Forms.Label lblTotalExpenses;
        private System.Windows.Forms.Label lblTotalSalaries;
        private System.Windows.Forms.Label lblNetProfit;
        private System.Windows.Forms.Label lblStatus;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.ListView lvwTopEmployees;
        private System.Windows.Forms.ColumnHeader colEmployee;
        private System.Windows.Forms.ColumnHeader colTransactionCount;
        private System.Windows.Forms.ColumnHeader colEmployeeCommission;
        private System.Windows.Forms.Label label12;
        private System.Windows.Forms.Label label13;
        private System.Windows.Forms.Label lblSalesCount;
        private System.Windows.Forms.Label lblRentalsCount;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.Button btnLoadTransactions;
        private System.Windows.Forms.DateTimePicker dtpTransEndDate;
        private System.Windows.Forms.Label label14;
        private System.Windows.Forms.DateTimePicker dtpTransStartDate;
        private System.Windows.Forms.Label label15;
        private System.Windows.Forms.Label label16;
        private System.Windows.Forms.ListView lvwTransactions;
        private System.Windows.Forms.ColumnHeader colDate;
        private System.Windows.Forms.ColumnHeader colType;
        private System.Windows.Forms.ColumnHeader colProperty;
        private System.Windows.Forms.ColumnHeader colAddress;
        private System.Windows.Forms.ColumnHeader colClient;
        private System.Windows.Forms.ColumnHeader colEmp;
        private System.Windows.Forms.ColumnHeader colAmount;
        private System.Windows.Forms.ColumnHeader colCommission;
        private System.Windows.Forms.Label lblTransStatus;
        private System.Windows.Forms.Panel panel3;
        private System.Windows.Forms.Button btnLoadExpenses;
        private System.Windows.Forms.DateTimePicker dtpExpenseEndDate;
        private System.Windows.Forms.Label label17;
        private System.Windows.Forms.DateTimePicker dtpExpenseStartDate;
        private System.Windows.Forms.Label label20;
        private System.Windows.Forms.Label label21;
        private System.Windows.Forms.ListView lvwExpenses;
        private System.Windows.Forms.ColumnHeader colExpDate;
        private System.Windows.Forms.ColumnHeader colExpCategory;
        private System.Windows.Forms.ColumnHeader colDescription;
        private System.Windows.Forms.ColumnHeader colExpEmployee;
        private System.Windows.Forms.ColumnHeader colExpAmount;
        private System.Windows.Forms.Label lblExpensesStatus;
        private System.Windows.Forms.Label label18;
        private System.Windows.Forms.ListView lvwExpensesByCategory;
        private System.Windows.Forms.ColumnHeader colCategory;
        private System.Windows.Forms.ColumnHeader colTotal;
        private System.Windows.Forms.Label lblTotalExpensesAmount;
        private System.Windows.Forms.Label label19;
    }
}
