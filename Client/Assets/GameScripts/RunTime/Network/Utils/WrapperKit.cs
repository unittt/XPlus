using System.Collections.Generic;
using System.Linq;
using Com.Iohao.Message;
using Google.Protobuf;

namespace GameScript.RunTime.Network
{
    public static class WrapperKit
    {
        public static ByteValueList OfListByteValue(IEnumerable<IMessage> values)
        {
            var byteValueList = new ByteValueList();

            var enumerable = values.ToList();
            if (!enumerable.Any())
            {
                return byteValueList;
            }

            var list = enumerable
                .Select(value => value.ToByteString())
                .ToList();

            byteValueList.Values.AddRange(list);

            return byteValueList;
        }
    }
}