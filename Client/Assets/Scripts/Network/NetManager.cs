using HT.Framework;
using System.Collections.Generic;
using Cysharp.Threading.Tasks;
using Google.Protobuf;
using UnityEngine;

/// <summary>
/// 游戏逻辑的网络管理
/// </summary>
public static class NetManager
{
    
    private static Dictionary<int, HTFAction<ProtocolTcpNetworkInfo>> _eventHandlerList = new();
    
    // 是否启用心跳
    public static bool isUsePing = true;
    // 心跳间隔时间
    public static int pingInterval = 4000;

    //上一次发送PING的时间
    static float lastPingTime = 0;

    //上一次收到PONG的时间
    static float lastPongTime = 0;

    private static bool _success;
    
    /// <summary>
    /// 连接服务器
    /// </summary>
    public static void ConnectServer()
    {
        Main.m_Network.BeginConnectServerEvent += OnBeginConnectServerEvent;
        Main.m_Network.ConnectServerSuccessEvent += OnConnectServerSuccessEvent;
        Main.m_Network.ConnectServerFailEvent += OnConnectServerFailEvent;
        Main.m_Network.DisconnectServerEvent += OnDisconnectServerEvent;
        Main.m_Network.ReceiveMessageEvent += OnReceiveMessage;
        Main.m_Network.SendMessageEvent += OnSendMessageEvent;
        Main.m_Network.ConnectServer<ProtocolChannel>();
        
        PingUpdate().Forget();
    }
    
    /// <summary>
    /// 开始连接服务器
    /// </summary>
    /// <param name="arg"></param>
    private static void OnBeginConnectServerEvent(ProtocolChannelBase arg)
    {
        Log.Info("开始连接服务器");
    }
    
    /// <summary>
    /// 服务器连接成功
    /// </summary>
    /// <param name="arg"></param>
    private static void OnConnectServerSuccessEvent(ProtocolChannelBase arg)
    {
        Log.Info("服务器连接成功");
        _success = true;
    }
    
    /// <summary>
    /// 服务器连接失败
    /// </summary>
    /// <param name="arg"></param>
    private static void OnConnectServerFailEvent(ProtocolChannelBase arg)
    {
        Log.Info("服务器连接失败");
    }

    /// <summary>
    /// 断开了服务器
    /// </summary>
    /// <param name="arg"></param>
    private static void OnDisconnectServerEvent(ProtocolChannelBase arg)
    {
        Log.Info("断开了服务器");
    }
    
    /// <summary>
    /// 当发送消息成功
    /// </summary>
    /// <param name="arg"></param>
    private static void OnSendMessageEvent(ProtocolChannelBase arg)
    {
       
    }
    
    /// <summary>
    /// 接受到消息
    /// </summary>
    /// <param name="channel"></param>
    /// <param name="networkMessage"></param>
    private static void OnReceiveMessage(ProtocolChannelBase channel, INetworkMessage networkMessage)
    {
       
        var tcpNetworkInfo = networkMessage.Cast<ProtocolTcpNetworkInfo>();
        //广播消息
        if (_eventHandlerList.ContainsKey(tcpNetworkInfo.CmdMerge))
        {
            _eventHandlerList[tcpNetworkInfo.CmdMerge].Invoke(tcpNetworkInfo);
        }
    }
    
    /// <summary>
    /// 发送消息
    /// </summary>
    /// <param name="cmd"></param>
    /// <param name="subCmd"></param>
    /// <param name="message"></param>
    /// <returns></returns>
    public static bool SendMessage(int cmd, int subCmd, IMessage message)
    {
        var info = new ProtocolTcpNetworkInfo(cmd,subCmd,message);
        var isSend = Main.m_Network.SendMessage<ProtocolChannel>(info);
        return isSend;
    }

    
    /// <summary>
    /// 注册消息监听
    /// </summary>
    /// <param name="cmd"></param>
    /// <param name="subCmd"></param>
    /// <param name="handler"></param>
    public static void Subscribe(int cmd, int subCmd, HTFAction<ProtocolTcpNetworkInfo> handler)
    {
        var mergeCmd = CmdMgr.getMergeCmd(cmd, subCmd);

        if (!_eventHandlerList.ContainsKey(mergeCmd))
        {
            _eventHandlerList.Add(mergeCmd, null);
        }
        _eventHandlerList[mergeCmd] += handler;
    }

    /// <summary>
    /// 移除消息监听
    /// </summary>
    /// <param name="cmd"></param>
    /// <param name="subCmd"></param>
    /// <param name="handler"></param>
    public static void Unsubscribe(int cmd, int subCmd,HTFAction<ProtocolTcpNetworkInfo> handler)
    {
        var mergeCmd = CmdMgr.getMergeCmd(cmd, subCmd);
        if (_eventHandlerList.ContainsKey(mergeCmd))
        {
            _eventHandlerList[mergeCmd] -= handler;
        }
    }
    
    // 发送PING协议
    private static  async UniTaskVoid  PingUpdate()
    {
        // 是否启用
        if (!isUsePing)
        {
            return;
        }

        await UniTask.WaitUntil(() => _success);

        while (true)
        {
            SendMessage(0, 0, null);
            await UniTask.Delay(pingInterval);
        }
    }

    // 心跳包回应
    private static void OnPong(byte[] message)
    {
        lastPongTime = Time.time;
    }
}
