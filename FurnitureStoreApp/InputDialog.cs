using System;
using System.Windows.Forms;

namespace FurnitureStoreApp
{
    public partial class InputDialog : Form
    {
        public string InputText { get; private set; }

        public InputDialog(string prompt, string title, string defaultValue = "")
        {
            InitializeComponent();
            this.Text      = title;
            lblPrompt.Text = prompt;
            txtInput.Text  = defaultValue;
            txtInput.MaxLength = 100;
            txtInput.SelectAll();
        }

        private void btnOK_Click(object sender, EventArgs e)
        {
            InputText = txtInput.Text;
            DialogResult = DialogResult.OK;
            Close();
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.Cancel;
            Close();
        }
    }
}