using System;
using System.Collections.Generic;

namespace Message
{
    /// <summary>
    /// Message sent to client (initially) with a list of all users connected to the server
    /// </summary>
    [Serializable]
    public class ListUsersMessage : Message
    {
        /// <summary>
        /// List of users connected to the server
        /// </summary>
        public List<User> Users { get; private set; }

        /// <summary>
        /// Default constructor
        /// </summary>
        /// <param name="users">List of users connected to the server</param>
        public ListUsersMessage(List<User> users)
        {
            this.Type = MessageType.LIST_USERS;
            this.Users = users;
        }
    }
}
