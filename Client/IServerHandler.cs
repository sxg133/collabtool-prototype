
namespace Client
{
    /// <summary>
    /// Interface for handling server events on client side
    /// </summary>
    public interface IServerHandler
    {
        /// <summary>
        /// Message received from server
        /// </summary>
        /// <param name="message">Byte array received from server</param>
        void onMessageReceived(byte[] message);

        /// <summary>
        /// Server has disconnected from client
        /// </summary>
        void onServerDisconnected();
    }
}
