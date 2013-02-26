using Message;
using Server;
using System;
using System.Net.Sockets;

namespace CollabServer
{
    class ServerActions : IServerActions
    {
        public delegate void clientAcceptedCallback(string handle);
        clientAcceptedCallback cac;

        public delegate void clientDisconnectedCallback(string handle);
        clientDisconnectedCallback cdc;

        public void setClientAcceptedCallback(clientAcceptedCallback cac)
        {
            this.cac = cac;
        }

        public void setClientDisconnectedCallback(clientDisconnectedCallback cdc)
        {
            this.cdc = cdc;
        }

        public void onServerStarted()
        {
            Console.WriteLine("server started");
        }

        public void onServerStopped()
        {
            Console.WriteLine("server stopped");
        }

        public void onClientAccepted(TcpClient client)
        {
            Console.WriteLine("accepted new client at remote endpoint {0}", client.Client.RemoteEndPoint);
        }

        public void onAllConnectionsClosed()
        {
            Console.WriteLine("all connections closed");
        }

        public void newUserAccepted(string handle)
        {
            Console.WriteLine("new client handle accepted: {0}", handle);
            if (this.cac != null)
            {
                this.cac(handle);
            }
        }

        public void userDisconnected(string handle)
        {
            Console.WriteLine("client disconnected: {0}", handle);
            if (this.cdc != null)
            {
                this.cdc(handle);
            }
        }
    }
}
