using System.Collections.Generic;
using UnityEngine;
using HT.Framework;
using System;
using System.Linq;

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
    public List<GridMapInfo> InfoList = new List<GridMapInfo>();

    public event Action OnInfoListValueChanged;


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

    /// <summary>
    /// 是否存在此编号的地图
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    public bool IsExistsID(int id)
    {
        return InfoList.Any(mapInfo => mapInfo.ID == id);
    }

    /// <summary>
    /// 获取GridMapInfo
    /// </summary>
    /// <param name="id"></param>
    /// <param name="gridMapInfo"></param>
    /// <returns></returns>
    public bool TryGetGridMapInfo(int id, out GridMapInfo gridMapInfo)
    {
        gridMapInfo = null;
        foreach (var mapInfo in InfoList.Where(mapInfo => mapInfo.ID == id))
        {
            gridMapInfo = mapInfo;
            return true;
        }
        return false;
    }
    
    public void AddMapInfo(int id, string tileFolder)
    {
        if (IsExistsID(id))
        {
            return;
        }

        var gridMapInfo = new GridMapInfo
        {
            ID = id,
            CrateTime = DateTime.Now.ToString(),
            TileFolder = tileFolder
        };
        InfoList.Add(gridMapInfo);
        OnInfoListValueChanged?.Invoke();
    }
}

[Serializable]
public class GridMapInfo
{
    public int ID;
    public string CrateTime;
    public string TileFolder;
    public string Describe;
    /// <summary>
    /// 行数(y)
    /// </summary>
    public int Rows;
    /// <summary>
    /// 列数(x)
    /// </summary>
    public int Columns;
}