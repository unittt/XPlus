using System.IO;
using UnityEditor;

namespace GridMap
{
    public static class MapDataExtensions
    {
        /// <summary>
        /// 删除文件
        /// </summary>
        public static void DeleteFile(this MapData mapData)
        {
            if (string.IsNullOrEmpty(mapData.AssetPath))
            {
                return;
            }

            AssetDatabase.DeleteAsset(mapData.AssetPath);
        }

        /// <summary>
        /// 保存到本地
        /// </summary>
        public static void Save(this MapData mapData)
        {
            File.WriteAllBytes(mapData.AssetPath, mapData.Serialize());
        }
    }
}