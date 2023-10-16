using System;
using UnityEditor;
using UnityEngine;

namespace GridMap
{
    public static class MapGlobal
    {

        public const string EDITOR_MAP_ID_KEY = "editormapidkey";

        /// <summary>
        /// 获取格子贴图
        /// </summary>
        /// <param name="textureFolder"></param>
        /// <param name="id"></param>
        /// <param name="x"></param>
        /// <param name="y"></param>
        /// <returns></returns>
        public static Texture GetGridTexture(string textureFolder, int id, int x, int y)
        {
            var textureName = GridMapConfig.Instance.TextureNameRule;
            textureName = textureName.Replace("[ID]", id.ToString());
            textureName = textureName.Replace("[X]", x.ToString());
            textureName = textureName.Replace("[Y]", y.ToString());
            var path = $"{textureFolder}/{textureName}.png";
            return AssetDatabase.LoadAssetAtPath<Texture>(path);
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
    }
}
