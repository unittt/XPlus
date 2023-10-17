using System.IO;
using UnityEditor;
using UnityEngine;

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
            // 刷新Asset数据库，以便Unity编辑器能够检测到新文件
            AssetDatabase.Refresh();
            Debug.Log("MapData保存成功");
        }
    }
}