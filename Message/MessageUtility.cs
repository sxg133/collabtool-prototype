using System;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

namespace Message
{
    /// <summary>
    /// Utility class for messages
    /// </summary>
    public static class MessageUtility
    {
        /// <summary>
        /// Deserialize a string to message
        /// </summary>
        /// <param name="m">string to deserialize</param>
        /// <returns>Deserialized message</returns>
        public static Message deserializeMessage(string m)
        {
            byte[] bytes = Convert.FromBase64String(m);
            return deserializeMessage(bytes);
        }

        /// <summary>
        /// Deserialize a string to message
        /// </summary>
        /// <param name="m">byte array to deserialize</param>
        /// <returns>Deserialized message</returns>
        public static Message deserializeMessage(byte[] m)
        {
            return deserializeMessage(m, m.Length);
        }

        /// <summary>
        /// Deserialize a string to message
        /// </summary>
        /// <param name="m">byte array to deserialize</param>
        /// <param name="length">number of bytes to deserialize</param>
        /// <returns>Deserialized message</returns>
        public static Message deserializeMessage(byte[] m, int length)
        {
            MemoryStream ms = new MemoryStream(m, 0, length);
            ms.Seek(0, SeekOrigin.Begin);
            ms.Flush();
            return (Message)(new BinaryFormatter().Deserialize(ms));
        }
    }
}
