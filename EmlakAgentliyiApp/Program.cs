using System;
using System.Windows.Forms;
using DashinmazEmlakApp.Database;
using DashinmazEmlakApp.Utilities;
using DasinmazEmlakAgentligi.Database;
using DasinmazEmlakAgentligi;

namespace DashinmazEmlakApp
{
    static class Program
    {
        /// <summary>
        ///  Tətbiqin əsas giriş nöqtəsi
        /// </summary>
        [STAThread]
        static void Main()
        {
            try
            {
                Application.SetHighDpiMode(HighDpiMode.SystemAware);
                Application.EnableVisualStyles();
                Application.SetCompatibleTextRenderingDefault(false);

                // Verilənlər bazası menecerini yaradırıq
                DatabaseManager databaseManager = new DatabaseManager(Constants.DatabasePath);

                // Verilənlər bazasını inisializasiya edirik
                databaseManager.InitializeDatabase();

                // Əsas formanı yaradırıq və tətbiqi başladırıq
                Application.Run(new MainForm(databaseManager));
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Tətbiq işə salınarkən xəta baş verdi: {ex.Message}", "Xəta",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);

                // Xəta jurnalını yazırıq
                LoggingHelper.LogError("Tətbiq işə salınarkən xəta baş verdi", ex);
            }
        }
    }
}