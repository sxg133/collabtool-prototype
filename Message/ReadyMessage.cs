using System;

namespace Message
{
    // NOTE : THIS MESSAGE IS NOT CURRENTLY IN USE

    [Serializable]
    public class ReadyMessage : Message
    {
        public ReadyMessage()
        {
            this.Type = MessageType.READY;
        }
    }
}
