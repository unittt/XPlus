using System.Collections.Generic;
using System.Linq;
using Com.Iohao.Message;
using Google.Protobuf;
using HT.Framework;

namespace GameScript.RunTime.Network
{
    public class CommandResult : INetworkMessage
    {
        public readonly ExternalMessage ExternalMessage;
        private IMessage _value;
        public readonly int CmdMerge;

        public CommandResult(ExternalMessage externalMessage)
        {
            ExternalMessage = externalMessage;
            CmdMerge = externalMessage.CmdMerge;
        }

        public T GetValue<T>() where T : IMessage, new()
        {
            if (_value != null)
            {
                return (T)_value;
            }

            var byteArray = ExternalMessage.Data.ToByteArray();

            return (T)(_value = PackageUtils.DeserializeByByteArray<T>(byteArray));
        }

        public string CmdToString()
        {
            return CmdKit.CmdToString(CmdMerge);
        }

        public IEnumerable<T> ListValue<T>() where T : IMessage, new()
        {
            return GetValue<ByteValueList>()
                .Values
                .Select(value => value.ToByteArray())
                .Select(PackageUtils.DeserializeByByteArray<T>)
                .ToList();
        }

        public List<string> ListString()
        {
            return GetValue<StringValueList>()
                .Values
                .ToList();
        }
    }
}