using Google.Protobuf;
using HT.Framework;

public sealed class ProtocolTcpNetworkInfo : INetworkMessage
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
    /// 消息
    /// </summary>
    public readonly IMessage Message;

    /// <summary>
    /// 包体
    /// </summary>
    public readonly byte[] Body;
    
    /// <summary>
    /// 业务路由
    /// </summary>
    public int CmdMerge => CmdMgr.getMergeCmd(Cmd, SubCmd);

    /// <summary>
    /// 
    /// </summary>
    /// <param name="cmd">主命令</param>
    /// <param name="subCmd">子命令</param>
    /// <param name="message"></param>
    public ProtocolTcpNetworkInfo(int cmd, int subCmd, IMessage message)
    {
        Cmd = cmd;
        Message = message;
        SubCmd = subCmd;
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="cmd">主命令</param>
    /// <param name="cmdMerge"></param>
    /// <param name="body"></param>
    public ProtocolTcpNetworkInfo(int cmdMerge,byte[] body)
    {
        Cmd = CmdMgr.getCmd(cmdMerge);
        SubCmd = CmdMgr.getSubCmd(cmdMerge);
        Body = body;
    }
}
