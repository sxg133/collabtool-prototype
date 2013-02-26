using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;

namespace Client
{
    /// <summary>
    /// Generic tcp client
    /// </summary>
    public class Client : IDisposable
    {
        #region Private Members

        private TcpClient client;
        private IServerHandler serverHandler;
        private Thread listenThread;

        #endregion

        #region Properties

        /// <summary>
        /// Port to connect to
        /// </summary>
        public int Port { get; private set; }

        /// <summary>
        /// IP address to connect to
        /// </summary>
        public IPAddress Address { get; private set; }

        /// <summary>
        /// Is the client listening for messages from server
        /// </summary>
        public bool IsListening { get; private set; }

        #endregion

        #region Constructors

        /// <summary>
        /// Initialize new client
        /// </summary>
        /// <param name="port">Port to connect to</param>
        /// <param name="serverHandler">Instance of server handler interface</param>
        public Client(int port, IServerHandler serverHandler) : this(IPAddress.Loopback, port, serverHandler) { }

        /// <summary>
        /// Initialize new client
        /// </summary>
        /// <param name="addr">IP address to connect to</param>
        /// <param name="port">Port to connect to</param>
        /// <param name="serverHandler">Instance of server handler interface</param>
        public Client(IPAddress addr, int port, IServerHandler serverHandler)
        {
            this.Port = port;
            this.Address = addr;
            this.serverHandler = serverHandler;

            this.client = new TcpClient();
        }

        #endregion

        #region Public Methods

        /// <summary>
        /// Connect to server
        /// </summary>
        public void connect()
        {
            if (this.IsListening)
            {
                return;
            }
            this.client.Connect(Address, Port);
            listenThread = new Thread(new ThreadStart(listen));
            listenThread.Start();
            this.IsListening = true;
            // TODO : TRIGGER CLIENT ACTION
        }

        /// <summary>
        /// Close server connection
        /// </summary>
        public void close()
        {
            if (!this.IsListening)
            {
                return;
            }
            listenThread.Abort();
            this.client.Close();
            this.IsListening = false;
            // TODO : TRIGGER CLIENT ACTION
        }

        /// <summary>
        /// Send message to server
        /// </summary>
        /// <param name="message">Message to send</param>
        public void sendMessage(string message)
        {
            ASCIIEncoding encoder = new ASCIIEncoding();
            byte[] m = encoder.GetBytes(message);
            this.sendMessage(m);
        }

        /// <summary>
        /// Send message to server
        /// </summary>
        /// <param name="message">Message to send</param>
        public void sendMessage(byte[] message)
        {
            if (!client.Connected)
            {
                throw new ClientException("Client not connected.");
            }
            if (message == null)
            {
                //throw new ClientException("message is null");
                // this is in a separate thread so I can't catch it -- just return for now and fix on rewrite
                return;
            }
            NetworkStream ns = client.GetStream();
            ns.Write(message, 0, message.Length);
            ns.Flush();
        }

        /// <summary>
        /// Clean up connection before destroying object
        /// </summary>
        public void Dispose()
        {
            this.close();
        }

        #endregion

        #region Private Methods

        private void listen()
        {
            using (NetworkStream ns = this.client.GetStream())
            {
                byte[] m = new byte[this.client.ReceiveBufferSize];
                int bytesRead = 0;
                int totalBytesRead = 0;
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
                    catch (IOException) // server has shut down
                    {
                        //throw new ClientException("Server has disconnected.");
                        serverHandler.onServerDisconnected();
                        break;
                    }

                    // if no bytes are read, server has disconnected
                    if (totalBytesRead == 0)
                    {
                        break;
                    }

                    serverHandler.onMessageReceived(bytes.ToArray());
                    ns.Flush();
                }
            }
        }

        #endregion
    }

    public class ClientException : Exception
    {
        /// <summary>
        /// Client error message
        /// </summary>
        public new string Message { get; private set; }

        /// <summary>
        /// ID of client error message
        /// </summary>
        public int MessageID { get; private set; }

        /// <summary>
        /// Initialize client exception
        /// </summary>
        /// <param name="message">Error message</param>
        /// <param name="messageID">Error message ID</param>
        public ClientException(string message, int messageID = -1)
        {
            this.Message = message;
            this.MessageID = messageID;
        }
    }
}
