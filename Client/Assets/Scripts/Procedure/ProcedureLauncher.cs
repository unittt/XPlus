using System.Collections.Generic;
using Cysharp.Threading.Tasks;
using UnityEngine;
using HT.Framework;
using Luban;
using YooAsset;
using AssetInfo = HT.Framework.AssetInfo;

/// <summary>
/// 启动流程
/// </summary>
public class ProcedureLauncher : ProcedureBase
{
    /// <summary>
    /// 流程初始化
    /// </summary>
    public override void OnInit()
    {
        //设置目标帧率
        Application.targetFrameRate = 60;
        //关闭多点触碰
        Input.multiTouchEnabled = false;
        //开启后台运行
        Application.runInBackground = true;
        //设置语言环境
        System.Globalization.CultureInfo.DefaultThreadCurrentCulture = new System.Globalization.CultureInfo("en-US");
    }
    /// <summary>
    /// 进入流程
    /// </summary>
    /// <param name="lastProcedure">上一个离开的流程</param>
    public override void OnEnter(ProcedureBase lastProcedure)
    {
        base.OnEnter(lastProcedure);

      
        // 

        // Main.m_Resource.load
        xxxxxx().Forget();
    }

    private static Dictionary<string, byte[]> _Bytes = new Dictionary<string, byte[]>();

    private async UniTask xxxxxx()
    {
        var asset = new AssetInfo("DefaultPackage","ai_tbbehaviortree","");
        var file = await Main.m_Resource.LoadRawFileDataAsync(asset);


        
        var package = YooAssets.GetPackage("DefaultPackage");
        var x = package.GetAssetInfos("data");
        foreach (var info in x)
        {
           // var xxxxx
           var xxx =  package.LoadRawFileAsync(info);
           await xxx.ToUniTask();
           _Bytes.Add(info.Address, xxx.GetRawFileData());
        }
        // AllAssetsOperationHandle handle = package.LoadAllAssetsAsync("ai_tbbehaviortree");
        // await handle.ToUniTask();

        var tables = new cfg.Tables((file) => new ByteBuf(_Bytes[file]));

        foreach (var item in  tables.TbItem.DataList)
        {
            Log.Info( item.Name);
        }
        
        foreach (var item in  tables.TbItem2.DataList)
        {
            Log.Info( item.Name);
        }
      
    }
    
    private static ByteBuf LoadByteBuf(string file)
    {
        // return new ByteBuf(File.ReadAllBytes($"{Application.dataPath}/../../GenerateDatas/bytes/{file}.bytes"));
        return new ByteBuf(_Bytes[file]);
    }

    /// <summary>
    /// 流程帧刷新
    /// </summary>
    public override void OnUpdate()
    {
        base.OnUpdate();
    }

}