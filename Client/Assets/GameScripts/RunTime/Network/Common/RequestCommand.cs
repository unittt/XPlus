using System.Threading;
using System.Threading.Tasks;
using Com.Iohao.Message;
using Cysharp.Threading.Tasks;
using Google.Protobuf;
using HT.Framework;
using Pb.Mmo;

namespace GameScript.RunTime.Network
{
    /// <summary>
    /// 创建请求参数
    /// </summary>
    public delegate IMessage RequestDataDelegate();

    /// <summary>
    /// 请求回调
    /// </summary>
    public delegate void CallbackDelegate(CommandResult result);

    /// <summary>
    /// 请求命令
    /// </summary>
    public sealed class RequestCommand : INetworkMessage
    {
        private readonly int _msgId = Interlocked.Increment(ref GameNetManager.MsgIdSeq);
        public string Title;
        public int CmdMerge;

        /// <summary>
        /// 创建请求参数
        /// </summary>
        public RequestDataDelegate RequestData;

        /// <summary>
        /// 请求回调
        /// </summary>
        public CallbackDelegate Callback;

        /// <summary>
        /// 执行请求命令
        /// </summary>
        public void Execute()
        {
            // 保存回调
            if (Callback != null)
            {
                // see GameNetManager.OnReceiveMessage
                GameNetManager.CallbackDictionary.Add(_msgId, Callback);
            }

            // 发送请求
            // 1 GameTcpChannel.EncapsulatedMessage
            // 2 ToExternal
            Main.m_Network.SendMessage<GameTcpChannel>(this);
        }

        public async UniTask<CommandResult> ExecuteAndWait()
        {
            var taskCompletionSource = new UniTaskCompletionSource<CommandResult>();
            GameNetManager.CommandResultDictionary.Add(_msgId,taskCompletionSource);
            Main.m_Network.SendMessage<GameTcpChannel>(this);
            return await taskCompletionSource.Task;
        }

        public ExternalMessage ToExternal()
        {
            // https://www.yuque.com/iohao/game/xeokui#gZWhm
            var externalMessage = new ExternalMessage
            {
                CmdCode = 1,
                CmdMerge = CmdMerge,
                MsgId = _msgId
            };

            var cmdToString = CmdKit.CmdToString(CmdMerge);


            if (RequestData == null)
            {
                $"发起[{Title}]请求 - [msgId:{_msgId}] {cmdToString}".Info();
                return externalMessage;
            }

            var data = RequestData.Invoke();
            externalMessage.Data = data.ToByteString();

            $"发起[{Title}]请求 - [msgId:{_msgId}] {cmdToString} {data}".Info();

            return externalMessage;
        }
    }

    /// <summary>
    /// 广播监听配置
    /// </summary>
    public sealed class CommandListenBroadcast
    {
        public string Title;
        public int CmdMerge;
        public CallbackDelegate Callback;

        /// <summary>
        /// 监听
        /// </summary>
        public void Listen()
        {
            // see GameNetManager.OnReceiveMessage
            GameNetManager.ListenBroadcastDictionary.Add(CmdMerge, this);
        }
    }
}