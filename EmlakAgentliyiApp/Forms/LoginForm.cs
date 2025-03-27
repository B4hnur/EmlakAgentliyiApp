using System;
using System.Windows.Forms;

namespace EmlakAgentliyiApp.Forms
{
    public partial class LoginForm : Form
    {
        private readonly Services.DatabaseService _databaseService;

        public LoginForm()
        {
            InitializeComponent();
            _databaseService = new Services.DatabaseService();
        }

        private void LoginForm_Load(object sender, EventArgs e)
        {
            txtUsername.Focus();
        }

        private void btnLogin_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtUsername.Text) || string.IsNullOrWhiteSpace(txtPassword.Text))
            {
                MessageBox.Show("İstifadəçi adı və şifrə daxil edilməlidir!", "Xəbərdarlıq",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            // Check login credentials
            bool isValid = _databaseService.ValidateUser(txtUsername.Text.Trim(), txtPassword.Text);

            if (isValid)
            {
                // Store logged in user in session
                // _databaseService.SetLoggedInUser(txtUsername.Text.Trim());

                DialogResult = DialogResult.OK;
                Close();
            }
            else
            {
                MessageBox.Show("Yanlış istifadəçi adı və ya şifrə!", "Xəta",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.Cancel;
            Close();
        }

        private void txtPassword_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                btnLogin_Click(sender, e);
            }
        }
    }
}
