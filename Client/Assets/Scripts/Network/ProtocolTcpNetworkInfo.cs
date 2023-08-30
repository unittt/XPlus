using Google.Protobuf;
using HT.Framework;

public sealed class ProtocolTcpNetworkInfo : INetworkMessage
{
    /// <summary>
    /// 消息头长度
    /// </summary>
    public const int LENGTH = 15;

    /// <summary>
    /// 消息总长度 2字节
    /// </summary>
    public ushort SumLength;
    /// <summary>
    /// 请求命令类型 0心跳1业务 2字节
    /// </summary>
    public ushort CmdCode;
    /// <summary>
    /// 协议开关 1字节
    /// </summary>
    public ushort ProtocolSwitch;
    /// <summary>
    /// MergeCmd 4字节
    /// </summary>
    public int MergeCmd;
    /// <summary>
    /// 返回状态码 2字节
    /// </summary>
    public ushort ResponseStatus;
    /// <summary>
    /// 请求体长度 4字节
    /// </summary>
    public int DataLen;
    /// <summary>
    /// 包体
    /// </summary>
    public byte[] Body;

    public ProtocolTcpNetworkInfo()
    {
        
    }
    
    public ProtocolTcpNetworkInfo(ushort cmdCode, ushort protocolSwitch, int mergeCmd, ushort responseStatus, IMessage body)
    {
        CmdCode = cmdCode;
        ProtocolSwitch = protocolSwitch;
        MergeCmd = mergeCmd;
        ResponseStatus = responseStatus;
        Body = body.ToByteArray();
        DataLen = Body.Length;
        SumLength = (ushort)(LENGTH + DataLen);
    }
}
