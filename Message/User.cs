using System;

namespace Message
{
    /// <summary>
    /// Class stores information on connected Users
    /// </summary>
    [Serializable]
    public class User
    {
        /// <summary>
        /// Status values for user
        /// </summary>
        public enum Flag
        {
            BUSY,
            NEEDS_HELP,
            AVAILABLE,
            UNKOWN
        }

        /// <summary>
        /// User handle
        /// </summary>
        public string Handle { get; private set; }

        /// <summary>
        /// User status
        /// </summary>
        public Flag Status { get; set; }

        /// <summary>
        /// Custom user message
        /// </summary>
        public string UserMessage { get; set; }

        /// <summary>
        /// Initalize a new User
        /// </summary>
        /// <param name="handle">User handle</param>
        /// <param name="status">User status</param>
        public User(string handle, Flag status)
        {
            this.Handle = handle;
            this.Status = status;
        }
    }
}
