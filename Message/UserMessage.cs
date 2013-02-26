using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Message
{
    /// <summary>
    /// Generic user message for updating a particular user
    /// </summary>
    [Serializable]
    public class UserMessage : Message
    {
        /// <summary>
        /// User associated with message
        /// </summary>
        public User User { get; protected set; }

        /// <summary>
        /// Default constructor
        /// </summary>
        /// <param name="user">User that has been updated</param>
        public UserMessage(User user)
        {
            this.User = user;
        }
    }
}
