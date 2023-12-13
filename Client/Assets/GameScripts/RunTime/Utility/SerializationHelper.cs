using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

namespace GameScripts.RunTime.Utility
{
    public static  class SerializationHelper
    {
        public static byte[] SerializeToBytes<T>(T obj)
        {
            using (var memoryStream = new MemoryStream())
            {
                var formatter = new BinaryFormatter();
                formatter.Serialize(memoryStream, obj);
                return memoryStream.ToArray();
            }
        }

        public static T DeserializeFromBytes<T>(byte[] data)
        {
            using (var memoryStream = new MemoryStream(data))
            {
                var formatter = new BinaryFormatter();
                return (T)formatter.Deserialize(memoryStream);
            }
        }
    }
}