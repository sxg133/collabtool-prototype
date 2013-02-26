using System;

namespace Message
{
    /// <summary>
    /// A new user has connected to the server
    /// </summary>
    [Serializable]
    public class NewUserMessage : UserMessage
    {
        public NewUserMessage(User u)
            : base(u)
        {
            this.Type = MessageType.NEW_USER;
        }
    }

    /// <summary>
    /// An existing user has updated their status
    /// </summary>
    [Serializable]
    public class UpdateUserMessage : UserMessage
    {
        public UpdateUserMessage(User u)
            : base(u)
        {
            this.Type = MessageType.UPDATE_USER;
        }
    }

    /// <summary>
    /// A user has disconnected from the server
    /// </summary>
    [Serializable]
    public class RemoveUserMessage : UserMessage
    {
        public RemoveUserMessage(User u)
            : base(u)
        {
            this.Type = MessageType.REMOVE_USER;
        }
    }
}
