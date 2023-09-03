using HT.Framework;
using System.Collections.Generic;
using cfg;
using Cysharp.Threading.Tasks;
using Luban;


/// <summary>
/// luban配置表 全局访问
/// </summary>
public static class TableGlobal
{
    public static Tables Instance { get; private set; }

    public static async UniTask Init()
    {
        var assetInfos = Main.m_Resource.GetAssetInfos("data");
        var bytesInstances = new Dictionary<string, byte[]>();
        foreach (var info in assetInfos)
        {
            var bytes = await Main.m_Resource.LoadRawFileDataAsync(info);
            bytesInstances.Add(info.Address, bytes);
        }

        Instance = new Tables((file) => new ByteBuf(bytesInstances[file]));
    }
}

