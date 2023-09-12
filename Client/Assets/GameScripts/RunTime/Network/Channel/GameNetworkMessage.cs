using Google.Protobuf;
using HT.Framework;

namespace GameScript.RunTime.Network
{
    public sealed class GameNetworkMessage :INetworkMessage
    {
        /// <summary>
        /// 主命令
        /// </summary>
        public readonly int Cmd;
        /// <summary>
        /// 子命令
        /// </summary>
        public readonly int SubCmd;
        /// <summary>
        /// 消息的字节string数据
        /// </summary>
        public readonly ByteString MsgByteString;
        
        /// <summary>
        /// 业务路由
        /// </summary>
        public int CmdMerge => CmdMgr.GetMergeCmd(Cmd, SubCmd);
        
        /// <summary>
        /// 
        /// </summary>
        /// <param name="cmd">主命令</param>
        /// <param name="subCmd">子命令</param>
        /// <param name="message"></param>
        public GameNetworkMessage(int cmd, int subCmd, IMessage message)
        {
            Cmd = cmd;
            SubCmd = subCmd;
            MsgByteString = message.ToByteString();
        }
        
        /// <summary>
        /// 
        /// </summary>
        /// <param name="cmdMerge"></param>
        /// <param name="msgByteString"></param>
        public GameNetworkMessage(int cmdMerge, ByteString msgByteString)
        {
            Cmd = CmdMgr.GetCmd(cmdMerge);
            SubCmd = CmdMgr.GetSubCmd(cmdMerge);
            MsgByteString = msgByteString;
        }
    }
}