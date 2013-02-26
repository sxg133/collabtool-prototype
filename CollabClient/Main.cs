using Message;
using System;
using System.Collections.Generic;
using System.Windows.Forms;
using System.Diagnostics;
using System.Runtime.InteropServices;
using UserPanel;

namespace CollabClient
{
    public partial class Main : Form
    {
        [DllImport("user32.dll", CharSet = CharSet.Auto, ExactSpelling = true)]
        private static extern IntPtr GetForegroundWindow();

        [DllImport("user32.dll", CharSet = CharSet.Auto, SetLastError = true)]
        private static extern int GetWindowThreadProcessId(IntPtr handle, out int processId);

        [DllImport("user32.dll")]
        static extern bool FlashWindow(IntPtr hwnd, bool bInvert);

        #region Constants

        private readonly string STATUS_AVAILABLE = "Available";
        private readonly string STATUS_BUSY = "Busy";
        private readonly string STATUS_NEEDS_HELP = "I Need Help";

        #endregion

        #region Delegates

        private delegate void setUsernameCallback(string username);
        private delegate void userCallback(User u);
        private delegate void cbStatusCallback(string newStatus);
        private delegate void noArgsCallback();

        #endregion

        #region Private Members

        private List<User> users;
        private User thisUser;

        #endregion

        public Main()
        {
            InitializeComponent();
        }

        #region Event Triggers

        private void Main_Load(object sender, EventArgs e)
        {
            Connection conn = Connection.getInstance();
            conn.setMessageHandler(new ServerHandler.messageHandler(this.onMessageReceived));
            conn.setDisconnectedCallback(new ServerHandler.disconnectEvent(this.closeApp));
            conn.openConnection();
        }

        private void Main_FormClosed(object sender, FormClosedEventArgs e)
        {
            Connection.getInstance().closeConnection();
        }

        private void cbStatus_SelectedIndexChanged(object sender, EventArgs e)
        {
            string newStatus = cbStatus.SelectedItem.ToString();
            if (newStatus.Equals(STATUS_AVAILABLE))
            {
                this.thisUser.Status = User.Flag.AVAILABLE;
            }
            else if (newStatus.Equals(STATUS_BUSY))
            {
                this.thisUser.Status = User.Flag.BUSY;
            }
            else if (newStatus.Equals(STATUS_NEEDS_HELP))
            {
                this.thisUser.Status = User.Flag.NEEDS_HELP;
            }
            UpdateUserMessage m = new UpdateUserMessage(this.thisUser);
            Connection.getInstance().sendMessage(m.toArray());
        }

        private void bUpdateUserMessage_Click(object sender, EventArgs e)
        {
            this.thisUser.UserMessage = tUserMessage.Text;
            UpdateUserMessage m = new UpdateUserMessage(this.thisUser);
            Connection.getInstance().sendMessage(m.toArray());
        }

        #endregion

        #region Cross-Thread Operations

        private void setUsername(string username)
        {
            if (this.lUserName.InvokeRequired)
            {
                setUsernameCallback suc = new setUsernameCallback(setUsername);
                this.Invoke(suc, new object[] { username });
            }
            else
            {
                this.thisUser = new User(username, User.Flag.BUSY);
                this.lUserName.Text = this.thisUser.Handle;
            }
        }

        private void clearUserPanel()
        {
            if (pUsers.InvokeRequired)
            {
                noArgsCallback upc = new noArgsCallback(this.clearUserPanel);
                this.Invoke(upc);
            }
            else
            {
                pUsers.Controls.Clear();
            }
        }

        private void addUser(User u)
        {
            if (pUsers.InvokeRequired)
            {
                userCallback auc = new userCallback(this.addUser);
                this.Invoke(auc, new object[] { u });
            }
            else
            {
                UserPane up = new UserPane(u.Handle);
                up.Status = u.Status;
                up.UserMessage = u.UserMessage;
                up.Top = up.Height * (users.Count - 1);
                pUsers.Controls.Add(up);
            }
        }

        private void organizeUserPanelControls()
        {
            if (pUsers.InvokeRequired)
            {
                noArgsCallback upc = new noArgsCallback(this.organizeUserPanelControls);
                this.Invoke(upc);
            }
            else
            {
                for (int i = 0; i < pUsers.Controls.Count; i++)
                {
                    Control c = pUsers.Controls[i];
                    c.Top = c.Height * i;
                }
            }
        }

        private void closeApp()
        {
            if (this.InvokeRequired)
            {
                noArgsCallback cac = new noArgsCallback(this.closeApp);
                this.Invoke(cac);
            }
            else
            {
                this.Close();
            }
        }

        private void invokeFlashWindow()
        {
            if (this.InvokeRequired)
            {
                noArgsCallback nac = new noArgsCallback(this.invokeFlashWindow);
                this.Invoke(nac);
            }
            else
            {
                FlashWindow(this.Handle, true);
            }
        }

        private void removeUserPanel(User u)
        {
            if (pUsers.InvokeRequired)
            {
                userCallback ruc = new userCallback(this.removeUserPanel);
                this.Invoke(ruc, new object[] { u });
            }
            else
            {
                int i;
                for (i = pUsers.Controls.Count-1; i >= 0; i--)
                {
                    UserPane p = pUsers.Controls[i] as UserPane;
                    if (p.Username.Equals(u.Handle, StringComparison.CurrentCultureIgnoreCase))
                    {
                        pUsers.Controls.RemoveAt(i);
                        break;
                    }
                }
                for (; i < pUsers.Controls.Count; i++)
                {
                    UserPane p = pUsers.Controls[i] as UserPane;
                    p.Top = p.Top - p.Height;
                }
            }
        }

        #endregion

        #region Message Handling

        private void onMessageReceived(Message.Message m)
        {
            if (!Connection.getInstance().isConnected)
            {
                return;
            }
            if (m.Type == MessageType.LIST_USERS)
            {
                handleListUsersMessage((ListUsersMessage)m);
            }
            else if (m.Type == MessageType.GET_HANDLE)
            {
                setUsername(getUsername((m as Message.GetHandleMessage).ExistingHandles));
                Connection.getInstance().sendMessage(this.thisUser.Handle);
                //updateStatus(this.thisUser.Status);
            }
            else if (m.Type == MessageType.NEW_USER || m.Type == MessageType.UPDATE_USER)
            {
                updateOrAddUser((m as UserMessage).User);
            }
            else if (m.Type == MessageType.REMOVE_USER)
            {
                removeUser((m as RemoveUserMessage).User);
            }
        }

        private void removeUser(User u)
        {
            for (int i = this.users.Count - 1; i >= 0; i--)
            {
                if (this.users[i].Handle.Equals(u.Handle, StringComparison.CurrentCultureIgnoreCase))
                {
                    this.users.RemoveAt(i);
                    break;
                }
            }
            //refreshUsers(this.users);
            removeUserPanel(u);
        }

        private void updateOrAddUser(User u, string userMessage = "")
        {
            if (u.Handle.Equals(this.thisUser.Handle, StringComparison.CurrentCultureIgnoreCase))
            {
                return;
            }
            bool found = false;
            bool statuschanged = false;
            for (int i = 0; i < users.Count; i++)
            {
                User user = users[i];
                if (user.Handle.Equals(u.Handle, StringComparison.CurrentCultureIgnoreCase))
                {
                    found = true;
                    statuschanged = user.Status != u.Status;
                    user.Status = u.Status;
                    user.UserMessage = u.UserMessage;
                }
                if ((pUsers.Controls[i] as UserPane).Username.Equals(user.Handle, StringComparison.CurrentCultureIgnoreCase))
                {
                    (pUsers.Controls[i] as UserPane).Status = user.Status;
                    (pUsers.Controls[i] as UserPane).UserMessage = user.UserMessage;
                }
            }
            if (!found)
            {
                this.users.Add(u);
                addUser(u);
                statuschanged = true;
            }
            if (u.Status == User.Flag.NEEDS_HELP && statuschanged && !ApplicationIsActivated())
            {
                invokeFlashWindow();
            }
        }

        private void refreshUsers(List<User> updatedUsers)
        {
            clearUserPanel();
            this.users = updatedUsers;
            foreach(User u in users)
            {
                addUser(u);
            }
            organizeUserPanelControls();
        }

        private void handleListUsersMessage(ListUsersMessage m)
        {
            refreshUsers(m.Users);
        }

        private string getUsername(List<string> existingNames)
        {
            //return System.Security.Principal.WindowsIdentity.GetCurrent().Name;
            UsernameDialog ud = new UsernameDialog(existingNames);
            DialogResult dr = ud.ShowDialog();
            if (dr == System.Windows.Forms.DialogResult.Cancel)
            {
                this.closeApp();
            }
            else
            {
                return ud.Username;
            }
            return null;
        }

        private void updateStatus(User.Flag newStatus)
        {
            this.thisUser.Status = newStatus;
            string newStatusStr = "";
            switch (this.thisUser.Status)
            {
                case User.Flag.AVAILABLE:
                    newStatusStr = STATUS_AVAILABLE;
                    break;
                case User.Flag.BUSY:
                    newStatusStr = STATUS_BUSY;
                    break;
                case User.Flag.NEEDS_HELP:
                    newStatusStr = STATUS_NEEDS_HELP;
                    break;
                default:
                    break;
            }
            updateCBStatus(newStatusStr);
            UpdateUserMessage um = new UpdateUserMessage(this.thisUser);
            byte[] m = um.toArray();
            Connection.getInstance().sendMessage(m);
        }

        private void updateCBStatus(string newStatus)
        {
            if (cbStatus.InvokeRequired)
            {
                cbStatusCallback csc = new cbStatusCallback(this.updateCBStatus);
                this.Invoke(csc, new object[] { newStatus });
            }
            else
            {
                cbStatus.SelectedItem = newStatus;
            }
        }

        private bool ApplicationIsActivated()
        {
            var activatedHandle = GetForegroundWindow();
            if (activatedHandle == IntPtr.Zero)
            {
                return false;       // No window is currently activated
            }

            var procId = Process.GetCurrentProcess().Id;
            int activeProcId;
            GetWindowThreadProcessId(activatedHandle, out activeProcId);

            return activeProcId == procId;
        }

        #endregion
    }
}
