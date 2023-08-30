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

            LoginVerify loginVerif = new LoginVerify();
            ProtocolTcpNetworkInfo info = new ProtocolTcpNetworkInfo(1,1, CmdMgr.getMergeCmd(1, 0),0,loginVerif);
            var isSend = Main.m_Network.SendMessage<ProtocolChannel>(info);
            Log.Info("发送消息"+ isSend);
        };
        Main.m_Network.ConnectServerFailEvent += (channel) =>
        {
            Log.Info(channel.ToString() + " 连接服务器失败！");
        };
        Main.m_Network.ConnectServer<ProtocolChannel>();

        Main.m_Network.ReceiveMessageEvent += OnReceiveMessage;
    }

    private void OnReceiveMessage(ProtocolChannelBase arg1, INetworkMessage arg2)
    {
        Log.Info("接受到消息" );
    }
}
