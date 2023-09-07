using Google.Protobuf;

public static class PackageUtils
{
    // 接收服务器是java 需要特殊处理
    public static byte[] IntToArr(int num)
    {
        byte[] data = new byte[4];
        data[3] = (byte)(num & 0xff);
        data[2] = (byte)(num >> 8 & 0xff);
        data[1] = (byte)(num >> 16 & 0xff);
        data[0] = (byte)(num >> 24 & 0xff);
        return data;
    }
    
    // 做偏移 将数据转string
    public static byte[] UnPackMessage(byte[] packet, int offset, int length)
    {
        byte[] final = new byte[length];
        for (var i = 0; i < final.Length; i++)
        {
            final[i] = packet[i + offset];
        }

        return final;
    }
    
    /// <summary>
    /// 反序列化protobuf
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="dataBytes"></param>
    /// <returns></returns>
    public static T DeserializeMsg<T>(byte[] dataBytes) where T : IMessage, new()
    {
        T msg = new T();
        msg = (T)msg.Descriptor.Parser.ParseFrom(dataBytes);
        return msg;
    }
    
    public static T DeserializeByByteArray<T>(byte[] dataBytes) where T : IMessage, new()
    {
        var stream = new CodedInputStream(dataBytes);
        var msg = new T();
        msg.MergeFrom(stream);
        return msg;
    }

    public static T GetMessage<T>(this ProtocolTcpNetworkInfo protocolTcpNetwork) where T : IMessage, new()
    {
        var t = DeserializeByByteArray<T>(protocolTcpNetwork.Body);
        return t;
    }
}
