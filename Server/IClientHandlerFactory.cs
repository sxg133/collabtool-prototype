
namespace Server
{
    /// <summary>
    /// Create the appropriate Client Handler
    /// </summary>
    public interface IClientHandlerFactory
    {
        /// <summary>
        /// Get the appropriate client handler
        /// </summary>
        /// <returns>Instance of IClientHandler</returns>
        IClientHandler getClientHandler();
    }
}
