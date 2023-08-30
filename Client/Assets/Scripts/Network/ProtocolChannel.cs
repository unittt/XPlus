using System;
using System.Net;
using HT.Framework;
using System.Net.Sockets;

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
        var protocolTcpNetworkInfo = message.Cast<ProtocolTcpNetworkInfo>();
        var sumLengthByte = BitConverter.GetBytes(IPAddress.HostToNetworkOrder(protocolTcpNetworkInfo.SumLength));
        var cmdCodeByte = BitConverter.GetBytes(IPAddress.HostToNetworkOrder(protocolTcpNetworkInfo.CmdCode));
        var protocolSwitchByte = BitConverter.GetBytes(IPAddress.HostToNetworkOrder(protocolTcpNetworkInfo.ProtocolSwitch));
        var mergeCmdByte = BitConverter.GetBytes(IPAddress.HostToNetworkOrder(protocolTcpNetworkInfo.MergeCmd));
        var responseStatusByte = BitConverter.GetBytes(IPAddress.HostToNetworkOrder(protocolTcpNetworkInfo.ResponseStatus));
        var dataLenByte = BitConverter.GetBytes(IPAddress.HostToNetworkOrder(protocolTcpNetworkInfo.DataLen));
        
        var totalByte = new byte[ProtocolTcpNetworkInfo.LENGTH + protocolTcpNetworkInfo.Body.Length];
        sumLengthByte.CopyTo(totalByte, 0);
        cmdCodeByte.CopyTo(totalByte, 2);
        protocolSwitchByte.CopyTo(totalByte, 4);
        mergeCmdByte.CopyTo(totalByte, 5);
        responseStatusByte.CopyTo(totalByte, 9);
        dataLenByte.CopyTo(totalByte, 11);
        protocolTcpNetworkInfo.Body.CopyTo(totalByte,ProtocolTcpNetworkInfo.LENGTH);
        return totalByte;

    }

    /// <summary>
    /// 接收消息
    /// </summary>
    /// <param name="client">客户端</param>
    /// <returns>接收到的消息对象</returns>
    protected override INetworkMessage ReceiveMessage(Socket client)
    {
        try
        {
            //接收消息头（消息总长度2字节 + 请求命令2字节 + 协议开关1字节 + MergeCmd4字节 + 返回码2字节 +包体长度4字节  = 15字节）
            var recvHeadLength = ProtocolTcpNetworkInfo.LENGTH;
            var recvBytesHead = new byte[recvHeadLength];
            while (recvHeadLength > 0)
            {
                byte[] recvBytes1 = new byte[ProtocolTcpNetworkInfo.LENGTH];
                int alreadyRecvHead = 0;
                if (recvHeadLength >= recvBytes1.Length)
                {
                    alreadyRecvHead = client.Receive(recvBytes1, recvBytes1.Length, 0);
                }
                else
                {
                    alreadyRecvHead = client.Receive(recvBytes1, recvHeadLength, 0);
                }

                recvBytes1.CopyTo(recvBytesHead, recvBytesHead.Length - recvHeadLength);
                recvHeadLength -= alreadyRecvHead;
            }

            //接收消息体（消息体的长度存储在消息头的4至8索引位置的字节里）11-15
            var bodyLengthBytes = new byte[4];
            Array.Copy(recvBytesHead, 11, bodyLengthBytes, 0, 4);
            int recvBodyLength = IPAddress.NetworkToHostOrder(BitConverter.ToInt32(bodyLengthBytes, 0));
            byte[] recvBytesBody = new byte[recvBodyLength];
            while (recvBodyLength > 0)
            {
                byte[] recvBytes2 = new byte[recvBodyLength < 1024 ? recvBodyLength : 1024];
                int alreadyRecvBody = 0;
                if (recvBodyLength >= recvBytes2.Length)
                {
                    alreadyRecvBody = client.Receive(recvBytes2, recvBytes2.Length, 0);
                }
                else
                {
                    alreadyRecvBody = client.Receive(recvBytes2, recvBodyLength, 0);
                }

                recvBytes2.CopyTo(recvBytesBody, recvBytesBody.Length - recvBodyLength);
                recvBodyLength -= alreadyRecvBody;
            }

            //解析消息
            var info = new ProtocolTcpNetworkInfo();
            info.SumLength = (ushort)IPAddress.NetworkToHostOrder(BitConverter.ToUInt16(recvBytesHead, 0));
            info.CmdCode = (ushort)IPAddress.NetworkToHostOrder(BitConverter.ToUInt16(recvBytesHead, 2));
            info.ProtocolSwitch = (ushort)IPAddress.NetworkToHostOrder(BitConverter.ToUInt16(recvBytesHead, 4));
            info.MergeCmd = IPAddress.NetworkToHostOrder(BitConverter.ToInt32(recvBytesHead, 5));
            info.ResponseStatus = (ushort)IPAddress.NetworkToHostOrder(BitConverter.ToUInt16(recvBytesHead, 9));
            info.DataLen = IPAddress.NetworkToHostOrder(BitConverter.ToInt32(recvBytesHead, 11));

            info.Body = new byte[info.DataLen];
            Array.Copy(recvBytesBody, ProtocolTcpNetworkInfo.LENGTH, info.Body, 0, info.Body.Length);

            return info;
        }
        catch (Exception)
        {
            return null;
        }
    }
}