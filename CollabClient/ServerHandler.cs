using Client;
using Message;

namespace CollabClient
{
    class ServerHandler : IServerHandler
    {
        public delegate void messageHandler(Message.Message m);
        private messageHandler messageHandlerCallback;
        public delegate void disconnectEvent();
        private disconnectEvent disconnectCallback;

        public ServerHandler()
        {
        }

        public void setMessageHandlerCallback(messageHandler messageHandlerCallback)
        {
            this.messageHandlerCallback = messageHandlerCallback;
        }

        public void setDisconnectCallback(disconnectEvent disconnectCallback)
        {
            this.disconnectCallback = disconnectCallback;
        }

        public void onMessageReceived(byte[] m)
        {
            Message.Message message = MessageUtility.deserializeMessage(m);
            if (this.messageHandlerCallback != null)
            {
                this.messageHandlerCallback(message);
            }
        }

        public void onServerDisconnected()
        {
            if (this.disconnectCallback != null)
            {
                this.disconnectCallback();
            }
        }
    }
}
