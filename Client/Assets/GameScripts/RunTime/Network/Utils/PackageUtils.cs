using Google.Protobuf;

namespace GameScript.RunTime.Network
{
    public static class PackageUtils
    {
        /// <summary>
        /// 接收服务器是java 需要特殊处理
        /// </summary>
        /// <param name="num"></param>
        /// <returns></returns>
        public static byte[] IntToArr(int num)
        {
            var data = new byte[4];
            data[3] = (byte)(num & 0xff);
            data[2] = (byte)(num >> 8 & 0xff);
            data[1] = (byte)(num >> 16 & 0xff);
            data[0] = (byte)(num >> 24 & 0xff);
            return data;
        }

        /// <summary>
        /// 按字节数组反序列化
        /// </summary>
        /// <param name="dataBytes"></param>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public static T DeserializeByByteArray<T>(byte[] dataBytes) where T : IMessage, new()
        {
            var stream = new CodedInputStream(dataBytes);
            var msg = new T();
            msg.MergeFrom(stream);
            return msg;
        }
    }
}