using UnityEngine;
using HT.Framework;
using System;

/// <summary>
/// 新建数据集
/// </summary>
[Serializable]
[CreateAssetMenu(menuName = "HTFramework DataSet/HotFixSettings")]
public class HotFixSettings : DataSetBase
{
    public string ResourceVersionFileName = "ResourceVersion.txt";
    /// <summary>
    /// 默认的资源服务器下载地址
    /// </summary>
    public string HostServerURL = "http://127.0.0.1:8081";
    /// <summary>
    /// 备用的资源服务器下载地址
    /// </summary>
    public string FallbackHostServerURL = "http://127.0.0.1:8081";
    
    public string WindowsUpdateDataURL = "http://127.0.0.1";
    public string IOSUpdateDataURL = "http://127.0.0.1";
    public string AndroidUpdateDataURL = "http://127.0.0.1";
    
    /// <summary>
    /// 获取URL
    /// </summary>
    public string GetUpdateURL
    {
        get
        {
#if UNITY_EDITOR_WIN || UNITY_STANDALONE_WIN
            return WindowsUpdateDataURL;
#elif UNITY_IOS
             return IOSUpdateDataURL;
#elif UNITY_ANDROID
             return AndroidUpdateDataURL;
#endif
            return "";
        }
    }

    /// <summary>
    /// 通过Json数据填充数据集
    /// </summary>
    public override void Fill(JsonData data)
    {
        base.Fill(data);
    }
	/// <summary>
    /// 将数据集打包为Json数据
    /// </summary>
    public override JsonData Pack()
    {
        JsonData data = new JsonData();
        return data;
    }
}