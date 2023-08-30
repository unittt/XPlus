using System;
using System.IO;
using System.Net;
using HT.Framework;
using System.Net.Sockets;
using System.Threading;
using Google.Protobuf;

/// <summary>
/// Protocol通信管道
/// </summary>
public class ProtocolChannel : ProtocolChannelBase
{
    /// <summary>
    /// 通信协议
    /// </summary>
    public override ProtocolType Protocol
    {
        get
        {
            return ProtocolType.Tcp;
        }
    }
    /// <summary>
    /// 通道类型
    /// </summary>
    public override SocketType Way
    {
        get
        {
            return SocketType.Stream;
        }
    }
    /// <summary>
    /// 是否需要保持连接
    /// </summary>
    public override bool IsNeedConnect
    {
        get
        {
            return true;
        }
    }

    /// <summary>
    /// 是否是断开连接请求
    /// </summary>
    /// <param name="message">消息对象</param>
    /// <returns>是否是断开连接请求</returns>
    public override bool IsDisconnectRequest(INetworkMessage message)
    {
        return false;
    }
    /// <summary>
    /// 封装消息
    /// </summary>
    /// <param name="message">消息对象</param>
    /// <returns>封装后的字节数组</returns>
    public override byte[] EncapsulatedMessage(INetworkMessage message)
    {
        ProtocolTcpNetworkInfo protocolTcpNetworkInfo = message.Cast<ProtocolTcpNetworkInfo>();
        byte[] cmdCodeByte = BitConverter.GetBytes(IPAddress.HostToNetworkOrder(protocolTcpNetworkInfo.cmdCode));
        byte[] protocolSwitchByte = BitConverter.GetBytes(IPAddress.HostToNetworkOrder(protocolTcpNetworkInfo.protocolSwitch));
        byte[] cmdMergeByte = BitConverter.GetBytes(IPAddress.HostToNetworkOrder(protocolTcpNetworkInfo.cmdMerge));
        byte[] responseStatusByte = BitConverter.GetBytes(IPAddress.HostToNetworkOrder(protocolTcpNetworkInfo.responseStatus));
        byte[] validMsgByte = BitConverter.GetBytes(IPAddress.HostToNetworkOrder(protocolTcpNetworkInfo.validMsg));
        byte[] dataByte = BitConverter.GetBytes(IPAddress.HostToNetworkOrder(protocolTcpNetworkInfo.data));
        byte[] msgIdByte = BitConverter.GetBytes(IPAddress.HostToNetworkOrder(protocolTcpNetworkInfo.msgId));
        
        byte[] totalByte = new byte[32 ];
        cmdCodeByte.CopyTo(totalByte, 0);
        protocolSwitchByte.CopyTo(totalByte, 4);
        cmdMergeByte.CopyTo(totalByte, 8);
        responseStatusByte.CopyTo(totalByte, 16);
        validMsgByte.CopyTo(totalByte, 20);
        dataByte.CopyTo(totalByte, 24);
        msgIdByte.CopyTo(totalByte, 28);
        return totalByte;
    }
    /// <summary>
    /// 接收消息
    /// </summary>
    /// <param name="client">客户端</param>
    /// <returns>接收到的消息对象</returns>
    protected override INetworkMessage ReceiveMessage(Socket client)
    {
        // try
        //     {
        //         //接收消息头（消息校验码4字节 + 消息体长度4字节 + 身份ID8字节 + 主命令4字节 + 子命令4字节 + 加密方式4字节 + 返回码4字节 = 32字节）
        //         int recvHeadLength = 32;
        //         byte[] recvBytesHead = new byte[recvHeadLength];
        //         while (recvHeadLength > 0)
        //         {
        //             byte[] recvBytes1 = new byte[32];
        //             int alreadyRecvHead = 0;
        //             if (recvHeadLength >= recvBytes1.Length)
        //             {
        //                 alreadyRecvHead = client.Receive(recvBytes1, recvBytes1.Length, 0);
        //             }
        //             else
        //             {
        //                 alreadyRecvHead = client.Receive(recvBytes1, recvHeadLength, 0);
        //             }
        //             recvBytes1.CopyTo(recvBytesHead, recvBytesHead.Length - recvHeadLength);
        //             recvHeadLength -= alreadyRecvHead;
        //         }
        //
        //         //接收消息体（消息体的长度存储在消息头的4至8索引位置的字节里）
        //         byte[] bodyLengthBytes = new byte[4];
        //         Array.Copy(recvBytesHead, 4, bodyLengthBytes, 0, 4);
        //         int recvBodyLength = IPAddress.NetworkToHostOrder(BitConverter.ToInt32(bodyLengthBytes, 0));
        //         byte[] recvBytesBody = new byte[recvBodyLength];
        //         while (recvBodyLength > 0)
        //         {
        //             byte[] recvBytes2 = new byte[recvBodyLength < 1024 ? recvBodyLength : 1024];
        //             int alreadyRecvBody = 0;
        //             if (recvBodyLength >= recvBytes2.Length)
        //             {
        //                 alreadyRecvBody = client.Receive(recvBytes2, recvBytes2.Length, 0);
        //             }
        //             else
        //             {
        //                 alreadyRecvBody = client.Receive(recvBytes2, recvBodyLength, 0);
        //             }
        //             recvBytes2.CopyTo(recvBytesBody, recvBytesBody.Length - recvBodyLength);
        //             recvBodyLength -= alreadyRecvBody;
        //         }
        //
        //         //解析消息
        //         TcpNetworkInfo info = new TcpNetworkInfo();
        //         info.CheckCode = IPAddress.NetworkToHostOrder(BitConverter.ToInt32(recvBytesHead, 0));
        //         info.BodyLength = IPAddress.NetworkToHostOrder(BitConverter.ToInt32(recvBytesHead, 4));
        //         info.Sessionid = IPAddress.NetworkToHostOrder(BitConverter.ToInt64(recvBytesHead, 8));
        //         info.Command = IPAddress.NetworkToHostOrder(BitConverter.ToInt32(recvBytesHead, 16));
        //         info.Subcommand = IPAddress.NetworkToHostOrder(BitConverter.ToInt32(recvBytesHead, 20));
        //         info.Encrypt = IPAddress.NetworkToHostOrder(BitConverter.ToInt32(recvBytesHead, 24));
        //         info.ReturnCode = IPAddress.NetworkToHostOrder(BitConverter.ToInt32(recvBytesHead, 28));
        //         info.Messages = new List<string>();
        //         for (int i = 0; i < recvBytesBody.Length;)
        //         {
        //             byte[] bytes = new byte[4];
        //             Array.Copy(recvBytesBody, i, bytes, 0, 4);
        //             i += 4;
        //             int number = IPAddress.NetworkToHostOrder(BitConverter.ToInt32(bytes, 0));
        //
        //             bytes = new byte[number];
        //             Array.Copy(recvBytesBody, i, bytes, 0, number);
        //             i += number;
        //             info.Messages.Add(Encoding.UTF8.GetString(bytes, 0, bytes.Length));
        //         }
        //         return info;
        //     }
        //     catch (Exception)
        //     {
        //         return null;
        //     }
        return null;
    }
}