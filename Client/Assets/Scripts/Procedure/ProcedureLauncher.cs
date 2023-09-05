using Cysharp.Threading.Tasks;
using UnityEngine;
using HT.Framework;

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
        LoadConfig().Forget();
    }

    
    /// <summary>
    /// 加载配置
    /// </summary>
    private async UniTask LoadConfig()
    {
        //等待资源管理器初始化完成
        await UniTask.WaitUntil( ()=>Main.m_Resource.IsInitialized);
        await TableGlobal.Init();
        Main.m_Procedure.SwitchProcedure<ProcedureLogin>();
    }
    
}