using Message;
using Server;
using System;
using System.Collections.Generic;

namespace CollabServer
{
    class ClientHandlerFactory : IClientHandlerFactory
    {
        ClientHandler.messageReceivedCallback mrc;

        public ClientHandlerFactory(ClientHandler.messageReceivedCallback mrc)
        {
            this.mrc = mrc;
        }

        public IClientHandler getClientHandler()
        {
            ClientHandler ch = new ClientHandler();
            ch.setMessageReceivedCallback(mrc);
            return ch;
        }
    }
}
