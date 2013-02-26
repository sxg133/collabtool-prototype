using Message;
using Client;
using System;
using System.Net.Sockets;

namespace CollabClient
{
    class Connection : IDisposable
    {
        private static Connection conn;
        private int port;
        private Client.Client client;
        private ServerHandler serverHandler;

        public bool isConnected
        {
            get
            {
                return this.client != null && this.client.IsListening;
            }
        }

        private Connection()
        {
            this.port = Properties.Settings.Default.ConnectionPort;
            serverHandler = new ServerHandler();
            this.client = new Client.Client(port, serverHandler);
        }

        public static Connection getInstance()
        {
            if (conn == null)
            {
                conn = new Connection();
            }
            return conn;
        }

        public void setMessageHandler(ServerHandler.messageHandler messageHandlerCallback)
        {
            this.serverHandler.setMessageHandlerCallback(messageHandlerCallback);
        }

        public void setDisconnectedCallback(ServerHandler.disconnectEvent disconnectCallback)
        {
            this.serverHandler.setDisconnectCallback(disconnectCallback);
        }

        public void openConnection()
        {
            this.client.connect();
        }

        public void closeConnection()
        {
            this.client.close();
        }

        public void sendMessage(string message)
        {
            try
            {
                this.client.sendMessage(message);
            }
            catch (ClientException)
            {
                // TODO : DO SOMETHING WITH THIS EXCEPTION
            }
        }

        public void sendMessage(byte[] message)
        {
            try
            {
                this.client.sendMessage(message);
            }
            catch (ClientException)
            {
                // TODO : DO SOMETHING WITH THIS EXCEPTION
            }
        }

        public void Dispose()
        {
            this.closeConnection();
        }
    }
}
