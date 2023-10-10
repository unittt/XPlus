using UnityEngine;
using HT.Framework;
using System;

/// <summary>
/// 新建数据集
/// </summary>
[Serializable]
public sealed class GridMapConfig : DataSetBase
{
    /// <summary>
    /// 编辑场景的地址
    /// </summary>
    public string ScenePath;
    /// <summary>
    /// 文件保存的路径
    /// </summary>
    public string DataFolderPath;
    /// <summary>
    /// 贴图命名规则
    /// </summary>
    public string TextureNameRule = "tile_[ID]_[X]_[Y]";
    /// <summary>
    /// 默认的贴图格子的大小
    /// </summary>
    public float TextureSize = 1;
    /// <summary>
    /// 默认的节点大小
    /// </summary>
    public float NodeSize = 1;
    
    
    private static GridMapConfig _instance;

    public static GridMapConfig Instance
    {
        get
        {
            if (_instance == null)
            {
                _instance = Resources.Load<GridMapConfig>("GridMapConfig");
            }
            return _instance;
        }
    }
}