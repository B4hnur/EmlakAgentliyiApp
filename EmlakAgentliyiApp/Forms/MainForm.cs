using System;
using System.Drawing;
using System.Windows.Forms;
using EmlakAgentliyiApp.Forms;
using DasinmazEmlakAgentligi.Services;
using DasinmazEmlakAgentligi.Database;
using EmlakAgentliyiApp.Utils;
using EmlakAgentliyiApp.Forms;
using RealEstateManager.Services;
using RealEstateManager;
using DashinmazEmlakApp.Forms;

namespace DasinmazEmlakAgentligi
{
    public partial class MainForm : Form
    {
        private WebScraperService _scraperService;
        private DatabaseManager _databaseManager;

        public MainForm()
        {
            InitializeComponent();
            InitializeServices();
            CustomizeDesign();
        }

        private void InitializeServices()
        {
            _scraperService = new WebScraperService();
            _databaseManager = new DatabaseManager();
        }

        private void CustomizeDesign()
        {
            // Form dizaynını tənzimləyirik
            this.Text = "Daşınmaz Əmlak Agentliyi İdarəetmə Sistemi";
            this.Icon = SystemIcons.Application;
            this.WindowState = FormWindowState.Maximized;

            // Arxa plan rəngini təyin edirik
            this.BackColor = ColorTranslator.FromHtml("#f5f5f5");
        }

        private void MainForm_Load(object sender, EventArgs e)
        {
            LoadDashboardData();
        }

        private void LoadDashboardData()
        {
            try
            {
                // Burada dashboard-a lazım olan məlumatları yükləyirik
                // Aktiv elanların sayı, gəlirlər, xərclər və s.
                int activeListingCount = _databaseManager.GetActiveListingCount();
                decimal monthlyIncome = _databaseManager.GetMonthlyIncome();
                decimal monthlyExpenses = _databaseManager.GetMonthlyExpenses();

                lblActiveListings.Text = activeListingCount.ToString();
                lblMonthlyIncome.Text = monthlyIncome.ToString("C");
                lblMonthlyExpenses.Text = monthlyExpenses.ToString("C");
                lblNetIncome.Text = (monthlyIncome - monthlyExpenses).ToString("C");

                // Son fəaliyyətləri yükləyirik
                var recentActivities = _databaseManager.GetRecentActivities(10);
                lstRecentActivities.Items.Clear();

                foreach (var activity in recentActivities)
                {
                    lstRecentActivities.Items.Add(activity);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Məlumatları yükləyərkən xəta baş verdi: {ex.Message}",
                    "Xəta", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnPropertyListings_Click(object sender, EventArgs e)
        {
            OpenForm(new PropertyListingForm());
        }

        private void btnClientManagement_Click(object sender, EventArgs e)
        {
            OpenForm(new ClientManagementForm());
        }

        private void btnEmployeeManagement_Click(object sender, EventArgs e)
        {
            OpenForm(new EmployeeManagementForm());
        }

        private void btnFinancialReports_Click(object sender, EventArgs e)
        {
            OpenForm(new FinancialReportForm());
        }

        private void btnScrapeListings_Click(object sender, EventArgs e)
        {
            try
            {
                Cursor = Cursors.WaitCursor;

                // Web scraping əməliyyatını başladırıq
                int newListingsCount = _scraperService.ScrapeNewListings();

                Cursor = Cursors.Default;

                MessageBox.Show($"{newListingsCount} yeni elan əlavə edildi.",
                    "Məlumat", MessageBoxButtons.OK, MessageBoxIcon.Information);

                // Dashboard məlumatlarını yeniləyirik
                LoadDashboardData();
            }
            catch (Exception ex)
            {
                Cursor = Cursors.Default;
                MessageBox.Show($"Elanları scrape edərkən xəta baş verdi: {ex.Message}",
                    "Xəta", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnRefresh_Click(object sender, EventArgs e)
        {
            LoadDashboardData();
        }

        private void OpenForm(Form form)
        {
            form.StartPosition = FormStartPosition.CenterScreen;
            form.ShowDialog();

            // Form bağlandıqdan sonra dashboard məlumatlarını yeniləyirik
            LoadDashboardData();
        }

        private void MainForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (MessageBox.Show("Proqramı bağlamaq istədiyinizə əminsiniz?",
                "Təsdiq", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.No)
            {
                e.Cancel = true;
            }
        }

        private void lstRecentActivities_SelectedIndexChanged(object sender, EventArgs e)
        {

        }
    }
}
