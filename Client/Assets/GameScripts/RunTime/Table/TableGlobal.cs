using HT.Framework;
using cfg;
using Cysharp.Threading.Tasks;
using Luban;


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
        var bytesInstances = await Main.m_Resource.LoadRawFileDataByTag("data",true);
        // foreach (var info in assetInfos)
        // {
        //     var bytes = await Main.m_Resource.LoadRawFileDataAsync(info);
        //     bytesInstances.Add(info.Address, bytes);
        // }

        Instance = new Tables((file) => new ByteBuf(bytesInstances[file]));
    }
}

