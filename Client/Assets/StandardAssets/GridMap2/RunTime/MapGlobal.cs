using System;
using UnityEngine;

public static class MapGlobal
{
    
    public const string EDITOR_MAP_ID_KEY = "editormapidkey";
    
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
