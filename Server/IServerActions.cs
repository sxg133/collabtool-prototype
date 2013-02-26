using System.Net.Sockets;

namespace Server
{
    /// <summary>
    /// Interface for handling server events
    /// </summary>
    public interface IServerActions
    {
        /// <summary>
        /// Fired when server starts listening
        /// </summary>
        void onServerStarted();

        /// <summary>
        /// Fired when server stops listening
        /// </summary>
        void onServerStopped();

        /// <summary>
        /// Fired when a new client is accepted
        /// </summary>
        /// <param name="client">The accepted client</param>
        void onClientAccepted(TcpClient client);

        /// <summary>
        /// Fired when all client connections are closed
        /// </summary>
        void onAllConnectionsClosed();

        /// <summary>
        /// A new user has been accepted
        /// </summary>
        /// <param name="handle">handle of the new user</param>
        void newUserAccepted(string handle);

        /// <summary>
        /// A user has disconnected
        /// </summary>
        /// <param name="handle">handle of the disconnected user</param>
        void userDisconnected(string handle);
    }
}
