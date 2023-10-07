using System.Collections.Generic;
using UnityEngine;
using HT.Framework;
using System;
using System.Linq;

/// <summary>
/// 新建数据集
/// </summary>
[Serializable]
[CreateAssetMenu(menuName = "HTFramework DataSet/GridMapDataSet")]
public class GridMapConfig : DataSetBase
{
    /// <summary>
    /// 编辑场景的地址
    /// </summary>
    public string ScenePath;
    public List<GridMapInfo> InfoList = new List<GridMapInfo>();

    public event Action OnInfoListValueChanged; 

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
    
    public void AddMapInfo(int id, string des)
    {
        if (IsExistsID(id))
        {
            return;
        }

        var gridMapInfo = new GridMapInfo
        {
            ID = id,
            CrateTime = DateTime.Now.ToString(),
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
    public string Describe;
}