using System;
using System.Windows.Forms;

namespace FurnitureStoreApp
{
    // Issue #12 — форма детального просмотра персональных данных
    public partial class PersonalDataForm : Form
    {
        public PersonalDataForm(string fullName, string phone)
        {
            InitializeComponent();
            lblNameValue.Text  = string.IsNullOrEmpty(fullName) ? "—" : fullName;
            lblPhoneValue.Text = string.IsNullOrEmpty(phone)    ? "—" : phone;
        }

        private void btnClose_Click(object sender, EventArgs e) => this.Close();
    }
}
