using System.Collections.Generic;
using Cysharp.Threading.Tasks;
using HT.Framework;

namespace GameScript.RunTime.Network
{
    /// <summary>
    /// 游戏通讯管理
    /// </summary>
    public static class GameNetManager
    {
        public static int MsgIdSeq;

        /// <summary>
        /// 请求回调
        /// </summary>
        public static readonly Dictionary<int, CallbackDelegate> CallbackDictionary = new();

        /// <summary>
        /// 异步等待回调
        /// </summary>
        public static readonly Dictionary<int, UniTaskCompletionSource <CommandResult>> CommandResultDictionary = new();
        
        /// <summary>
        /// 广播监听
        /// </summary>
        public static readonly Dictionary<int, CommandListenBroadcast> ListenBroadcastDictionary = new();


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

        // ReSharper disable Unity.PerformanceAnalysis
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


        #region Event

        /// <summary>
        /// 开始连接服务器
        /// </summary>
        private static void OnBeginConnectServerEvent(ProtocolChannelBase channelBase)
        {
            BeginConnectServerEvent?.Invoke(channelBase.Cast<GameTcpChannel>());
        }

        /// <summary>
        /// 服务器连接成功
        /// </summary>
        private static void OnConnectServerSuccessEvent(ProtocolChannelBase channelBase)
        {
            ConnectServerSuccessEvent?.Invoke(channelBase.Cast<GameTcpChannel>());
        }

        /// <summary>
        /// 服务器连接失败
        /// </summary>
        private static void OnConnectServerFailEvent(ProtocolChannelBase channelBase)
        {
            ConnectServerFailEvent?.Invoke(channelBase.Cast<GameTcpChannel>());
        }

        /// <summary>
        /// 当发送消息成功
        /// </summary>
        private static void OnSendMessageEvent(ProtocolChannelBase channelBase)
        {
            SendMessageEvent?.Invoke(channelBase.Cast<GameTcpChannel>());
        }

        /// <summary>
        /// 接收消息成功事件
        /// </summary>
        /// <param name="channelBase"></param>
        /// <param name="networkMessage"></param>
        private static void OnReceiveMessage(ProtocolChannelBase channelBase, INetworkMessage networkMessage)
        {
            var result = networkMessage.Cast<CommandResult>();
            var externalMessage = result.ExternalMessage;
            
            if (externalMessage.CmdCode == 0)
            {
                // 心跳不处理
                return;
            }
            
            var msgId = externalMessage.MsgId;
            var responseStatus = externalMessage.ResponseStatus;
            if (responseStatus == 0)
            {
                $"接收消息 - [msgId:{msgId.ToString()}]{result.CmdToString()} - [响应状态:{externalMessage.ResponseStatus.ToString()}] ".Info();
            }
            else
            {
                $"接收消息 - [msgId:{msgId.ToString()}]{result.CmdToString()} - [响应状态:{externalMessage.ResponseStatus.ToString()}] - [消息:{externalMessage.ValidMsg}]".Warning();
            }
          
            //1. 如果有 callback ，优先交给 callback 处理
            if (CallbackDictionary.TryGetValue(msgId, out var callBack))
            {
                callBack.Invoke(result);
                CallbackDictionary.Remove(msgId);
            }
            
            //2. 异步等待 设置结果
            if (CommandResultDictionary.TryGetValue(msgId, out  var  taskCompletionSource))
            {
                taskCompletionSource.TrySetResult(result);
                CommandResultDictionary.Remove(msgId);
            }

            // 广播监听
            if (!ListenBroadcastDictionary.TryGetValue(result.CmdMerge, out var broadcast)) return;
            {
                var cmdToString = result.CmdToString();
                $"广播监听回调[{broadcast.Title}]通知 {cmdToString}".Info();

                var callbackDelegate = broadcast.Callback;
                callbackDelegate.Invoke(result);
            }
        }

        /// <summary>
        /// 断开了服务器
        /// </summary>
        private static void OnDisconnectServerEvent(ProtocolChannelBase channelBase)
        {
            DisconnectServerEvent?.Invoke(channelBase.Cast<GameTcpChannel>());
        }

        #endregion
    }
}