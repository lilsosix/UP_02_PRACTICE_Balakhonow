using System;
using System.Windows.Forms;

namespace FurnitureStoreApp
{
    public partial class AdminImportMenuForm : Form
    {
        public AdminImportMenuForm()
        {
            InitializeComponent();
        }

        private void btnImport_Click(object sender, EventArgs e)
        {
            new AdminImportForm().ShowDialog();
        }

        private void btnRestore_Click(object sender, EventArgs e)
        {
            new AdminRestoreForm().ShowDialog();
        }

        private void btnExit_Click(object sender, EventArgs e)
        {
            // Возврат на форму авторизации
            InactivityWatcher.Stop();
            new LoginForm().Show();
            this.Close();
        }
    }
}
