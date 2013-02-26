using System.Collections.Generic;
using System.Net.Sockets;

namespace Server
{
    /// <summary>
    /// Interface for handling client connections
    /// </summary>
    public interface IClientHandler
    {
        /// <summary>
        /// Set a new client
        /// </summary>
        /// <param name="client">Newly connected client</param>
        void setClient(TcpClient client);

        /// <summary>
        /// Returns the handle for the connected client
        /// </summary>
        /// <param name="existingHandles">A list of existing handles</param>
        /// <returns>The handle for the new client</returns>
        string getClientHandle(List<string> existingHandles);

        /// <summary>
        /// Listens for messages from the client
        /// </summary>
        void listenForMessages();
    }
}
