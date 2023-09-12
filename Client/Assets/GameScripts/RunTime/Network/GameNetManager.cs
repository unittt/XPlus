using System.Collections.Generic;
using Google.Protobuf;
using HT.Framework;

namespace GameScript.RunTime.Network
{
    /// <summary>
    /// 游戏通讯管理
    /// </summary>
    public static class GameNetManager
    {
        private static readonly Dictionary<int, HTFAction<GameNetworkMessage>> _msgEventHandlerList = new();
        
        /// <summary>
        /// 开始连接服务器事件
        /// </summary>
        public static event HTFAction<GameTcpChannel> BeginConnectServerEvent;
        /// <summary>
        /// 连接服务器成功事件
        /// </summary>
        public static event HTFAction<GameTcpChannel> ConnectServerSuccessEvent;
        /// <summary>
        /// 连接服务器失败事件
        /// </summary>
        public static event HTFAction<GameTcpChannel> ConnectServerFailEvent;
        /// <summary>
        /// 与服务器断开连接事件
        /// </summary>
        public static event HTFAction<GameTcpChannel> DisconnectServerEvent;
        /// <summary>
        /// 发送消息成功事件
        /// </summary>
        public static event HTFAction<GameTcpChannel> SendMessageEvent;

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
            Main.m_Network.ConnectServer<GameTcpChannel>();
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
            var networkMessage = new GameNetworkMessage(cmd,subCmd,message);
            var isSend = Main.m_Network.SendMessage<GameTcpChannel>(networkMessage);
            return isSend;
        }
        
        
        /// <summary>
        /// 注册消息监听
        /// </summary>
        /// <param name="cmd"></param>
        /// <param name="subCmd"></param>
        /// <param name="handler"></param>
        public static void Subscribe(int cmd, int subCmd, HTFAction<GameNetworkMessage> handler)
        {
            var mergeCmd = CmdMgr.GetMergeCmd(cmd, subCmd);

            if (!_msgEventHandlerList.ContainsKey(mergeCmd))
            {
                _msgEventHandlerList.Add(mergeCmd, null);
            }
            _msgEventHandlerList[mergeCmd] += handler;
        }

        /// <summary>
        /// 移除消息监听
        /// </summary>
        /// <param name="cmd"></param>
        /// <param name="subCmd"></param>
        /// <param name="handler"></param>
        public static void Unsubscribe(int cmd, int subCmd,HTFAction<GameNetworkMessage> handler)
        {
            var mergeCmd = CmdMgr.GetMergeCmd(cmd, subCmd);
            if (_msgEventHandlerList.ContainsKey(mergeCmd))
            {
                _msgEventHandlerList[mergeCmd] -= handler;
            }
        }
        
        #region Event
        /// <summary>
        /// 开始连接服务器
        /// </summary>
        /// <param name="arg"></param>
        private static void OnBeginConnectServerEvent(ProtocolChannelBase channelBase)
        {
            BeginConnectServerEvent?.Invoke(channelBase.Cast<GameTcpChannel>());
        }
        
        /// <summary>
        /// 服务器连接成功
        /// </summary>
        /// <param name="arg"></param>
        private static void OnConnectServerSuccessEvent(ProtocolChannelBase channelBase)
        {
            ConnectServerSuccessEvent?.Invoke(channelBase.Cast<GameTcpChannel>());
        }
        
        /// <summary>
        /// 服务器连接失败
        /// </summary>
        /// <param name="arg"></param>
        private static void OnConnectServerFailEvent(ProtocolChannelBase channelBase)
        {
            ConnectServerFailEvent?.Invoke(channelBase.Cast<GameTcpChannel>());
        }
        
        /// <summary>
        /// 当发送消息成功
        /// </summary>
        /// <param name="arg"></param>
        private static void OnSendMessageEvent(ProtocolChannelBase channelBase)
        {
            SendMessageEvent?.Invoke(channelBase.Cast<GameTcpChannel>());
        }

        /// <summary>
        /// 接受到消息
        /// </summary>
        /// <param name="channel"></param>
        /// <param name="networkMessage"></param>
        private static void OnReceiveMessage(ProtocolChannelBase channelBase, INetworkMessage networkMessage)
        {
            var gameNetworkMessage = networkMessage.Cast<GameNetworkMessage>();
            //广播消息
            if (_msgEventHandlerList.ContainsKey(gameNetworkMessage.CmdMerge))
            {
                _msgEventHandlerList[gameNetworkMessage.CmdMerge].Invoke(gameNetworkMessage);
            }
        }

        /// <summary>
        /// 断开了服务器
        /// </summary>
        /// <param name="arg"></param>
        private static void OnDisconnectServerEvent(ProtocolChannelBase channelBase)
        {
            DisconnectServerEvent?.Invoke(channelBase.Cast<GameTcpChannel>());
        }
        #endregion
    }
}