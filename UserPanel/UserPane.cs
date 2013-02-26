using Message;
using System;
using System.Drawing;
using System.Windows.Forms;

namespace UserPanel
{
    public partial class UserPane : UserControl
    {
        private User.Flag flag;
        private delegate void toolTipStatus(string status);
        private string userMessage;

        public string UserMessage { 
            get { return userMessage; } 
            set 
            {
                if (String.IsNullOrEmpty(value))
                {
                    this.UserMessage = this.Username;
                }
                else
                {
                    this.userMessage = value;
                }
                updateToolTipMessage(this.userMessage); 
            } 
        }

        public string Username { get; private set; }
        public User.Flag Status
        {
            get
            {
                return flag;
            }
            set
            {
                updateStatus(value);
            }
        }

        public UserPane(string username)
        {
            this.Username = username;
            InitializeComponent();
            lUsername.Text = this.Username;
            ttMain.SetToolTip(lUsername, lUsername.Text);
        }

        private void updateStatus(User.Flag flag)
        {
            this.flag = flag;
            string status = "";
            switch (this.flag)
            {
                case User.Flag.AVAILABLE:
                    pStatus.BackColor = Color.Green;
                    status = "Available";
                    break;
                case User.Flag.BUSY:
                    pStatus.BackColor = Color.Red;
                   status = "Busy";
                    break;
                case User.Flag.NEEDS_HELP:
                    pStatus.BackColor = Color.Yellow;
                    status = "Needs Help";
                    break;
                default:
                    break;
            }
            updateToolTipStatus(status);
        }

        private void updateToolTipStatus(string status)
        {
            if (pStatus.InvokeRequired)
            {
                toolTipStatus tts = new toolTipStatus(this.updateToolTipStatus);
                this.Invoke(tts, new object[] { status });
            }
            else
            {
                ttMain.SetToolTip(pStatus, status);
            }
        }

        private void updateToolTipMessage(string message)
        {
            if (lUsername.InvokeRequired)
            {
                toolTipStatus tts = new toolTipStatus(this.updateToolTipMessage);
                this.Invoke(tts, new object[] { message });
            }
            else
            {
                ttMain.SetToolTip(lUsername, message);
            }
        }
    }
}
