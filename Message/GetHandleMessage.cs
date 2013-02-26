using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Message
{
    /// <summary>
    /// Message sent to client to get user handle
    /// </summary>
    [Serializable]
    public class GetHandleMessage : ListUsersMessage
    {
        /// <summary>
        /// Is this the first attempt at getting the handle?
        /// </summary>
        public bool FirstAttempt { get; set; }

        /// <summary>
        /// List of existing handles on the server
        /// </summary>
        public List<string> ExistingHandles { get; private set; }

        /// <summary>
        /// Constructor accepting a list of users which is converted into a string list
        /// </summary>
        /// <param name="users">List of users that are already connected to the server</param>
        public GetHandleMessage(List<User> users) : base(users)
        {
            this.FirstAttempt = true;
            this.Type = MessageType.GET_HANDLE;
            ExistingHandles = new List<string>();
            foreach (User u in users)
            {
                ExistingHandles.Add(u.Handle);
            }
        }

        /// <summary>
        /// Constructor accepting only a string list of handles
        /// </summary>
        /// <param name="existingHandles">List of handles already connected to the server</param>
        public GetHandleMessage(List<string> existingHandles) : base(null)
        {
            this.ExistingHandles = existingHandles;
            this.FirstAttempt = false;
            this.Type = MessageType.GET_HANDLE;
        }
    }
}
