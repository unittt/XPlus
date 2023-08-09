using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using HT.Framework;


/// <summary>
/// 检查热更资源版本流程
/// </summary>
public class ProcedureCheckVersion : ProcedureBase
{
    /// <summary>
    /// 流程初始化
    /// </summary>
    public override void OnInit()
    {
		base.OnInit();
    }
    /// <summary>
    /// 进入流程
    /// </summary>
    /// <param name="lastProcedure">上一个离开的流程</param>
    public override void OnEnter(ProcedureBase lastProcedure)
    {
        base.OnEnter(lastProcedure);
        
        //检查设备是否能够访问互联网
        if (Application.internetReachability == NetworkReachability.NotReachable)
        {
            Log.Info("当前设备没有连接网络");
            return;
        }
        
        //下载配置表版本文件
        //下载程序集版本文件
        //下载资源版本文件
    }

    private void OnDone()
    {
        Main.m_Procedure.SwitchProcedure<ProcedureUpdateResources>();
    }
    
    /// <summary>
    /// 离开流程
    /// </summary>
    /// <param name="nextProcedure">下一个进入的流程</param>
    public override void OnLeave(ProcedureBase nextProcedure)
    {
        base.OnLeave(nextProcedure);
    }
    /// <summary>
    /// 流程帧刷新
    /// </summary>
    public override void OnUpdate()
    {
        base.OnUpdate();
    }
    /// <summary>
    /// 流程帧刷新（秒）
    /// </summary>
    public override void OnUpdateSecond()
    {
        base.OnUpdateSecond();
    }
}