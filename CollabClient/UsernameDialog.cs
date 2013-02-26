using System;
using System.Collections.Generic;
using System.Windows.Forms;

namespace CollabClient
{
    public partial class UsernameDialog : Form
    {
        List<string> existingNames;

        public string Username { get; private set; }

        public UsernameDialog(List<string> existingNames)
        {
            this.existingNames = existingNames;
            InitializeComponent();
            lError.Text = "";
        }

        private void bOK_Click(object sender, System.EventArgs e)
        {
            string enteredName = tName.Text;
            if (String.IsNullOrWhiteSpace(enteredName))
            {
                lError.Text = "You must enter a name.";
                return;
            }
            if (existingNames.Contains(enteredName))
            {
                lError.Text = "Someone is already using this name!";
                return;
            }
            this.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.Username = enteredName;
            this.Close();
        }

        private void bCancel_Click(object sender, EventArgs e)
        {
            this.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.Close();
        }
    }
}
