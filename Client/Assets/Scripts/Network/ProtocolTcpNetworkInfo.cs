using System.IO;
using HT.Framework;
using Google.Protobuf;

public sealed class ProtocolTcpNetworkInfo : INetworkMessage
{
    public static int LENGTH = 15;

    /// <summary>
    /// 消息总长度
    /// </summary>
    public ushort sumLength;
    /// <summary>
    /// 请求命令类型 0心跳1业务 2字节
    /// </summary>
    public ushort cmdCode;
    /// <summary>
    /// 协议开关 1字节
    /// </summary>
    public ushort protocolSwich;
    /// <summary>
    /// mergecmd 4字节
    /// </summary>
    public uint mergeCmd;
    /// <summary>
    /// 返回状态码 2字节
    /// </summary>
    public ushort responseStatus;
    /// <summary>
    /// 请求体长度 4字节
    /// </summary>
    public uint dataLen;

    
    public ProtocolTcpNetworkInfo(int checkCode, long sessionid, int command, int subcommand, int encrypt, int returnCode, IMessage message)
    {
       
    }
    public ProtocolTcpNetworkInfo()
    {
    }
    
    public byte[] ToByteArray()
    {
        using (MemoryStream memoryStream = new MemoryStream())
        using (BinaryWriter binaryWriter = new BinaryWriter(memoryStream))
        {
            binaryWriter.Write(sumLength);
            binaryWriter.Write(cmdCode);
            binaryWriter.Write(protocolSwich);
            binaryWriter.Write(mergeCmd);
            binaryWriter.Write(responseStatus);
            binaryWriter.Write(dataLen);

            return memoryStream.ToArray();
        }
    }

    public void Read(byte[] data)
    {
        using (MemoryStream memoryStream = new MemoryStream(data))
        using (BinaryReader binaryReader = new BinaryReader(memoryStream))
        {
            sumLength = binaryReader.ReadUInt16();
            cmdCode = binaryReader.ReadUInt16();
            protocolSwich = binaryReader.ReadUInt16();
            mergeCmd = binaryReader.ReadUInt32();
            responseStatus = binaryReader.ReadUInt16();
            dataLen = binaryReader.ReadUInt32();
        }
    }
}
