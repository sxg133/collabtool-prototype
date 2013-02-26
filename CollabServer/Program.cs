using Message;
using Server;
using System;
using System.Collections.Generic;

namespace CollabServer
{
    class Program
    {
        private static Server.Server s;
        private static List<User> users;

        static void Main(string[] args)
        {
            int port = 9000;
            if (args.Length > 1)
            {
                if (args[0].Equals("-p"))
                {
                    Int32.TryParse(args[1], out port);
                }
            }

            users = new List<User>();
            ServerActions.clientAcceptedCallback cac = new ServerActions.clientAcceptedCallback(clientAccepted);
            ServerActions.clientDisconnectedCallback cdc = new ServerActions.clientDisconnectedCallback(clientDisconnected);
            ServerActions sa = new ServerActions();
            sa.setClientAcceptedCallback(cac);
            sa.setClientDisconnectedCallback(cdc);

            ClientHandler.messageReceivedCallback mrc = new ClientHandler.messageReceivedCallback(handleMessage);
            ClientHandlerFactory chf = new ClientHandlerFactory(mrc);

            using (s = new Server.Server(port, chf, sa))
            {
                string cmd = "";
                while (!(cmd = Console.ReadLine()).Equals("exit", StringComparison.CurrentCultureIgnoreCase))
                {
                    if ("start".Equals(cmd, StringComparison.CurrentCultureIgnoreCase))
                    {
                        s.start();
                    }
                    else if ("stop".Equals(cmd, StringComparison.CurrentCultureIgnoreCase))
                    {
                        s.stop();
                    }
                }
            }
        }


        private static void handleMessage(Message.Message m)
        {
            s.sendMessageToAll(m.toArray());
            if (m.Type == MessageType.UPDATE_USER || m.Type == MessageType.NEW_USER)
            {
                addOrUpdateUser((m as UserMessage).User);
            }
            else if (m.Type == MessageType.REMOVE_USER)
            {
                removeUser((m as RemoveUserMessage).User);
            }
        }

        private static void clientAccepted(string handle)
        {
            ListUsersMessage m = new ListUsersMessage(users);
            s.sendMessageToClient(handle, m.toArray());
        }

        private static void clientDisconnected(string handle)
        {
            foreach (User user in users)
            {
                if (handle.Equals(user.Handle, StringComparison.CurrentCultureIgnoreCase))
                {
                    removeUser(user);
                    RemoveUserMessage m = new RemoveUserMessage(user);
                    s.sendMessageToAll(m.toArray());
                    break;
                }
            }
        }

        private static void addOrUpdateUser(User u)
        {
            bool found = false;
            foreach (User user in users)
            {
                if (user.Handle.Equals(u.Handle, StringComparison.CurrentCultureIgnoreCase))
                {
                    found = true;
                    user.Status = u.Status;
                    user.UserMessage = u.UserMessage;
                }
            }
            if (!found)
            {
                users.Add(u);
            }
        }

        private static void removeUser(User u)
        {
            for (int i = users.Count - 1; i >= 0; i--)
            {
                if (users[i].Handle.Equals(u.Handle, StringComparison.CurrentCultureIgnoreCase))
                {
                    users.RemoveAt(i);
                    break;
                }
            }
        }
    }
}
