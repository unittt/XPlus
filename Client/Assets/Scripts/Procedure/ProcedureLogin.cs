using HT.Framework;
using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 登录流程
/// </summary>
public class ProcedureLogin : ProcedureBase
{
    public override void OnEnter(ProcedureBase lastProcedure)
    {
        base.OnEnter(lastProcedure);
        Main.m_UI.OpenUI<UILogin>();
        
       
        Main.m_Network.BeginConnectServerEvent += (channel) =>
        {
            Log.Info(channel.ToString() + " 开始连接服务器......");
        };
        Main.m_Network.ConnectServerSuccessEvent += (channel) =>
        {
            Log.Info(channel.ToString() + " 连接服务器成功！");
        };
        Main.m_Network.ConnectServerFailEvent += (channel) =>
        {
            Log.Info(channel.ToString() + " 连接服务器失败！");
        };
        Main.m_Network.ConnectServer<ProtocolChannel>();
    }
}
