using System;
using System.Collections.Generic;
using System.Text;

namespace FredsSubmarineControlShared
{
    public class FunctionCallMessage
    {
        public int MessageId { get; private set; }
        public string FunctionName { get; private set; }
        public Dictionary<string, object> Arguments { get; private set; }

        public FunctionCallMessage()
        {
            MessageId = 0;
            FunctionName = null;
            Arguments = new Dictionary<string, object>();
        }
        public FunctionCallMessage(int messageId, string functionName, Dictionary<string, object> arguments)
        {
            MessageId = messageId;
            FunctionName = functionName;
            Arguments = arguments;
        }

        public void WriteTo(AetherStream.AetherStream stream)
        {
            stream.WriteInt(MessageId);
            stream.WriteString(FunctionName);
            stream.WriteDict(Arguments);
        }

        public void ReadFrom(AetherStream.AetherStream stream)
        {
            MessageId = stream.ReadNext<int>();
            FunctionName = stream.ReadNext<string>();
            Arguments = stream.ReadNextDictionary<string, object>();
        }
    }
}
