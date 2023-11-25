using System;
using System.IO;
using UnityEditor;
using UnityEngine;

namespace GridMap
{
    public static class EditorGlobalTools
    {
        
        /// <summary>
        /// 将GUI坐标点转换为世界坐标
        /// </summary>
        /// <param name="position"></param>
        /// <returns></returns>
        public static Vector2 GUIPointToWorldOrigin(Vector2 position)
        {
            var worldRay = HandleUtility.GUIPointToWorldRay(position);
            return worldRay.origin;
        }
        
      
        /// <summary>
        ///  将绝对路径转换为相对于 Application.dataPath 的路径
        /// </summary>
        /// <param name="absolutePath"></param>
        /// <returns></returns>
        public static string AbsoluteToRelativePath(string absolutePath)
        {
            var absoluteUri = new Uri(absolutePath);
            var dataPathUri = new Uri(Application.dataPath);
            // 使用 Uri 的 MakeRelativeUri 方法来计算相对路径
            var relativeUri = dataPathUri.MakeRelativeUri(absoluteUri);
            // 将 Uri 转换为字符串
            var relativePath = Uri.UnescapeDataString(relativeUri.ToString());
            return relativePath;
        }
        
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
            var json = JsonUtility.ToJson(mapData);
            File.WriteAllText(mapData.AssetPath, json);
            // 刷新Asset数据库，以便Unity编辑器能够检测到新文件
            AssetDatabase.Refresh();
            Debug.Log("MapData保存成功");
        }
    }
}