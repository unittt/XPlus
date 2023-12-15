using HT.Framework;
using cfg;
using Cysharp.Threading.Tasks;
using Luban;
using UnityEngine;


/// <summary>
/// luban配置表 全局访问
/// </summary>
public class TableGlobal
{
    public static Tables Instance { get; private set; }

    private TableGlobal()
    {
        
    }
    
    public static async UniTask Init()
    {
      
        // var bytesInstances = await Main.m_Resource.LoadRawFileDataByTag("data");
        
        var bytesInstances = await Main.m_Resource.LoadAssetByTag<TextAsset>("data");
        Instance = new Tables((file) => new ByteBuf(bytesInstances[file].bytes));
    }
}

