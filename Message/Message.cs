using System;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

namespace Message
{
    /// <summary>
    /// Message Types
    /// </summary>
    [Serializable]
    public enum MessageType
    {
        NEW_USER,
        UPDATE_USER,
        REMOVE_USER,
        LIST_USERS,
        GET_HANDLE,
        READY
    }

    /// <summary>
    /// Generic message
    /// </summary>
    [Serializable]
    public abstract class Message
    {
        /// <summary>
        /// Message type
        /// </summary>
        public MessageType Type { get; protected set; }

        /// <summary>
        /// Seralize messages into string
        /// </summary>
        /// <returns>Serialized message</returns>
        public string serializeMessageToString(Message m)
        {
            return Convert.ToBase64String(this.toArray());
        }

        /// <summary>
        /// Seralize messages into byte array
        /// </summary>
        /// <returns>Serialized message</returns>
        public byte[] toArray()
        {
            MemoryStream ms = new MemoryStream();
            new BinaryFormatter().Serialize(ms, this);
            ms.Flush();
            ms.Seek(0, SeekOrigin.Begin);
            return ms.ToArray();
        }
    }
}
