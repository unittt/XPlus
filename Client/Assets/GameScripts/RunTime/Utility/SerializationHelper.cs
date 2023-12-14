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

        /// <summary>
        /// 深拷贝(效率较低，并且要求所有涉及的类型都是可序列化的  不建议使用)
        /// </summary>
        /// <param name="obj"></param>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public static T DeepClone<T>(T obj) where T : class
        {
            using var ms = new MemoryStream();
            var formatter = new BinaryFormatter();
            formatter.Serialize(ms, obj);
            ms.Position = 0;
            return (T)formatter.Deserialize(ms);
        }

    }
}