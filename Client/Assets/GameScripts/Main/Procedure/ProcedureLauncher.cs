using UnityEngine;
using HT.Framework;

/// <summary>
/// 初始流程（运行 Main 场景会首先进入此流程）
/// </summary>
public sealed class ProcedureLauncher : ProcedureBase
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
    /// 流程帧刷新
    /// </summary>
    public override void OnUpdate()
    {
        Main.m_Procedure.SwitchProcedure<ProcedureSplash>();
    }
}