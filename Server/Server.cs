using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Threading;
using System.Text;

namespace Server
{
    /// <summary>
    /// Generic server using TCP Listener
    /// </summary>
    public class Server : IDisposable
    {
        #region Private Members

        private IServerActions serverActions;
        private IClientHandlerFactory clientHandlerFactory;
        private TcpListener listener;
        private Thread listenThread;
        private volatile Dictionary<string, TcpClient> clients;
        private object clientLock = new object();

        #endregion

        #region Properties

        /// <summary>
        /// IP address of TCP listener
        /// </summary>
        public IPAddress Address { get; private set; }

        /// <summary>
        /// port number of TCP listener
        /// </summary>
        public int Port { get; private set; }

        /// <summary>
        /// Is the Server currently listening for connections
        /// </summary>
        public bool IsListening { get; private set; }

        #endregion

        #region Constructors

        /// <summary>
        /// Initialize a new server
        /// </summary>
        /// <param name="port">port number of TCP listener</param>
        /// <param name="serverActions">Instance of server actions class</param>
        /// <param name="clientHandler">Instance of client handler class</param>
        public Server(int port, IClientHandlerFactory clientHandlerFacotry, IServerActions serverActions = null) : this(IPAddress.Loopback, port, clientHandlerFacotry, serverActions) { }

        /// <summary>
        /// Initialize a new server
        /// </summary>
        /// <param name="addr">IP address of TCP listener</param>
        /// <param name="port">port number of TCP listener</param>
        /// <param name="serverActions">Instance of server actions class</param>
        /// <param name="clientHandler">Instance of client handler class</param>
        public Server(IPAddress addr, int port, IClientHandlerFactory clientHandlerFacotry, IServerActions serverActions = null)
        {
            this.Address = addr;
            this.Port = port;
            this.clientHandlerFactory = clientHandlerFacotry;
            this.serverActions = serverActions;

            this.listener = new TcpListener(Address, Port);
            clients = new Dictionary<string, TcpClient>();
        }

        #endregion

        #region Public Methods

        /// <summary>
        /// Start listening for clients
        /// </summary>
        public void start()
        {
            if (this.IsListening)
            {
                return;
            }
            this.listenThread = new Thread(new ThreadStart(listening));
            this.listenThread.Start();
            this.IsListening = true;
            if (serverActions != null)
            {
                serverActions.onServerStarted();
            }
        }

        /// <summary>
        /// Close all client connections
        /// </summary>
        public void closeAllConnections() 
        {
            foreach (TcpClient c in clients.Values)
            {
                c.Close();
            }
            lock (clientLock)
            {
                this.clients = new Dictionary<string, TcpClient>();
            }
        }

        /// <summary>
        /// close all connections and stop listening
        /// </summary>
        public void stop() 
        {
            if (!this.IsListening)
            {
                return;
            }
            closeAllConnections();
            this.listenThread.Abort();
            this.listener.Stop();
            this.IsListening = false;
            if (serverActions != null)
            {
                serverActions.onServerStopped();
            }
        }

        public void Dispose()
        {
            this.stop();
        }

        /// <summary>
        /// Send message to all open connections
        /// </summary>
        /// <param name="message">string message</param>
        public void sendMessageToAll(string message)
        {
            ASCIIEncoding encoder = new ASCIIEncoding();
            byte[] m = encoder.GetBytes(message);
            sendMessageToAll(m);
        }

        /// <summary>
        /// Send message to all open connections
        /// </summary>
        /// <param name="message">byte[] message</param>
        public void sendMessageToAll(byte[] message)
        {
            foreach (TcpClient c in clients.Values)
            {
                NetworkStream ns = c.GetStream();
                ns.Write(message, 0, message.Length);
                ns.Flush();
            }
        }

        /// <summary>
        /// Send a message to a specific client
        /// </summary>
        /// <param name="clientHandle">Handle of the TCP client</param>
        /// <param name="message">Message to send</param>
        public void sendMessageToClient(string clientHandle, string message)
        {
            ASCIIEncoding encoder = new ASCIIEncoding();
            byte[] m = encoder.GetBytes(message);
            sendMessageToClient(clientHandle, m);
        }

        /// <summary>
        /// Send a message to a specific client
        /// </summary>
        /// <param name="clientHandle">Handle of the TCP client</param>
        /// <param name="message">Message to send</param>
        public void sendMessageToClient(string clientHandle, byte[] message)
        {
            if (!clients.ContainsKey(clientHandle))
            {
                throw new ServerException(
                    String.Format("No client with the handle \"{0}\" was found.", clientHandle)
                );
            }
            TcpClient c = clients[clientHandle];
            NetworkStream ns = c.GetStream();
            ns.Write(message, 0, message.Length);
            ns.Flush();
        }

        #endregion

        #region Private Methods

        private void listening()
        {
            // start listener
            this.listener.Start();

            while (true)
            {
                // block actions until new client is accepted
                TcpClient client = this.listener.AcceptTcpClient();

                if (serverActions != null)
                {
                    serverActions.onClientAccepted(client);
                }

                // start client handler thread
                Thread handleClientThread = new Thread(new ParameterizedThreadStart(handleNewClient));
                handleClientThread.Start(client);
            }
        }

        private void handleNewClient(object client)
        {
            string cHandle = "";
            IClientHandler clientHandler = clientHandlerFactory.getClientHandler();
            TcpClient c = (TcpClient)client;
            clientHandler.setClient(c);
            try
            {
                while (clients.ContainsKey(cHandle) || String.IsNullOrEmpty(cHandle))
                {
                    List<string> existingHandles = clients.Keys.ToList<string>();
                    cHandle = clientHandler.getClientHandle(existingHandles);
                }
                lock (clientLock)
                {
                    clients.Add(cHandle, c);
                }
            }
            catch (ServerException) // client disconnected
            {
                return;
            }
            this.serverActions.newUserAccepted(cHandle);
            clientHandler.listenForMessages();
            lock (clientLock)
            {
                clients.Remove(cHandle);
            }
            this.serverActions.userDisconnected(cHandle);
        }

        #endregion
    }

    /// <summary>
    /// Custom exception for server errors
    /// </summary>
    public class ServerException : Exception
    {
        /// <summary>
        /// Server error message
        /// </summary>
        public new string Message { get; private set; }

        /// <summary>
        /// ID of server error message
        /// </summary>
        public int MessageID { get; private set; }

        /// <summary>
        /// Initialize server exception
        /// </summary>
        /// <param name="message">Error message</param>
        /// <param name="messageID">Error message ID</param>
        public ServerException(string message, int messageID = -1)
        {
            this.Message = message;
            this.MessageID = messageID;
        }
    }
}
