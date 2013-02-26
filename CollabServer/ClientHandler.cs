using System;
using System.Collections.Generic;
using System.IO;
using System.Net.Sockets;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;
using Message;
using Server;

namespace CollabServer
{
    class ClientHandler : IClientHandler
    {
        private TcpClient client;
        private NetworkStream ns;

        public delegate void messageReceivedCallback(Message.Message m);
        messageReceivedCallback mrc;

        public void setMessageReceivedCallback(messageReceivedCallback mrc)
        {
            this.mrc = mrc;
        }

        public void setClient(TcpClient client)
        {
            this.client = client;
        }

        public string getClientHandle(List<string> existingHandles)
        {
            List<User> users = new List<User>();
            ns = this.client.GetStream();
            return getHandle(existingHandles);
        }

        public void listenForMessages()
        {
            byte[] m = new byte[this.client.ReceiveBufferSize];
            int bytesRead = 0;
            int totalBytesRead = 0;
            BinaryFormatter bf = new BinaryFormatter();
            using (ns)
            {
                while (true)
                {
                    List<byte> bytes = new List<byte>();
                    bytesRead = 0;
                    totalBytesRead = 0;
                    try
                    {
                        do
                        {
                            bytesRead = ns.Read(m, 0, m.Length);
                            for (int i = 0; i < bytesRead; i++)
                            {
                                bytes.Add(m[i]);
                            }
                            totalBytesRead += bytesRead;
                        } while (ns.DataAvailable);
                    }
                    catch (IOException) // server disconnected
                    {
                        break;
                    }
                    if (totalBytesRead == 0)
                    {
                        break;
                    }
                    mrc(MessageUtility.deserializeMessage(bytes.ToArray(), bytesRead));
                    ns.Flush();
                }
            }
        }

        private string getHandle(List<string> existingHandles)
        {
            string username = "";
            ASCIIEncoding encoder = new ASCIIEncoding();
            GetHandleMessage initmessage = new GetHandleMessage(existingHandles);
            byte[] firstAttempt = initmessage.toArray();
            //initmessage.FirstAttempt = false;
            //byte[] repeatAttempt = initmessage.toArray();
            ns.Write(firstAttempt, 0, firstAttempt.Length);
            byte[] m = new byte[this.client.ReceiveBufferSize];
            int bytesRead = 0;
            int totalBytesRead;
            while (String.IsNullOrWhiteSpace(username) || String.IsNullOrEmpty(username))
            {
                List<byte> bytes = new List<byte>();
                bytesRead = 0;
                totalBytesRead = 0;
                try
                {
                    do
                    {
                        bytesRead = ns.Read(m, 0, m.Length);
                        for (int i = 0; i < bytesRead; i++)
                        {
                            bytes.Add(m[i]);
                        }
                        totalBytesRead += bytesRead;
                    } while (ns.DataAvailable);
                }
                catch (IOException) // server disconnected
                {
                    throw new Server.ServerException("Server disconnected");
                }
                if (bytesRead == 0)
                {
                    throw new Server.ServerException("Client disconnected before passing handle.");
                }
                byte[] message = bytes.ToArray();
                string response = encoder.GetString(message, 0, message.Length).TrimEnd(new char[] { (char)0, ' ' });
                if (!existingHandles.Contains(response) && !String.IsNullOrWhiteSpace(response))
                {
                    username = response;
                }
                ns.Flush();
            }
            return username;
        }
    }
}
