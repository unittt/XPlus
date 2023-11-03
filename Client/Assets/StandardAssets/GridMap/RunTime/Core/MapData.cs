using System;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

namespace GridMap
{

    /// <summary>
    /// 地图数据
    /// </summary>
    [Serializable]
    public sealed class MapData
    {
        /// <summary>
        /// 编号
        /// </summary>
        public int ID;

        /// <summary>
        /// 块宽高度
        /// </summary>
        public int BlockHeight = 1;
        /// <summary>
        /// 块宽数量
        /// </summary>
        public int BlockWidth = 1;
        /// <summary>
        ///贴图格子的大小
        /// </summary>
        public float BlockSize = 1;

        /// <summary>
        /// 贴图资源文件夹
        /// </summary>
        public string BlockTextureFolder;

        /// <summary>
        /// 格网图数据
        /// </summary>
        public byte[] GraphData;

        /// <summary>
        /// 文件地址
        /// </summary>
        public string AssetPath;

        public byte[] Serialize()
        {
            using var memoryStream = new MemoryStream();
            var binaryFormatter = new BinaryFormatter();
            binaryFormatter.Serialize(memoryStream, this);
            return memoryStream.ToArray();
        }

        public static MapData Deserialize(byte[] serializedData)
        {
            using var memoryStream = new MemoryStream(serializedData);
            var binaryFormatter = new BinaryFormatter();
            return (MapData)binaryFormatter.Deserialize(memoryStream);
        }
    }
}