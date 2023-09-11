using System;
using System.IO;
using HT.Framework;
using System.Net.Sockets;
using Com.Iohao.Message;
using Google.Protobuf;

/// <summary>
/// Protocol通信管道
/// </summary>
public class ProtocolChannel : ProtocolChannelBase
{
    
    private int _readOffset;
    
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
    public override byte[] EncapsulatedMessage(INetworkMessage networkMessage)
    {
        var protocolTcpNetworkInfo = networkMessage.Cast<ProtocolTcpNetworkInfo>();
        var externalMessage = new ExternalMessage
        {
            CmdMerge = protocolTcpNetworkInfo.CmdMerge,
            ProtocolSwitch = 0,
            CmdCode = protocolTcpNetworkInfo.Cmd
        };
        if (protocolTcpNetworkInfo.Message != null)
        {
            externalMessage.Data = protocolTcpNetworkInfo.Message.ToByteString();
        }
        
        var byteMsg = externalMessage.ToByteArray();
    
        // 在数据前加4个字节 用来描述数据长度
        var package = new byte[byteMsg.Length + 4];
        var intArr = PackageUtils.IntToArr(byteMsg.Length);
        intArr.CopyTo(package, 0);
        byteMsg.CopyTo(package, intArr.Length);
        
        return package;
    }
    
    //接受前面的4个长度
    /// <summary>
    /// 接收消息
    /// </summary>
    /// <param name="client">客户端</param>
    /// <returns>接收到的消息对象</returns>
    protected override INetworkMessage ReceiveMessage(Socket client)
    {
        try
        {
            // 接收消息体长度字段（4字节）
            var bodyLengthBytes = new byte[4];
            client.Receive(bodyLengthBytes, 4, 0);
            // 解析消息体长度
            var recBodyLength = BitConverter.ToInt32(bodyLengthBytes, 0);
            // 接收消息体
            var recBytesBody = new byte[recBodyLength];
            var bytesRead = 0;
            while (bytesRead < recBodyLength)
            {
                var bytesReceived = client.Receive(recBytesBody, bytesRead, recBodyLength - bytesRead, 0);
                if (bytesReceived == 0)
                {
                    // 连接关闭或未收到任何数据。
                    return null;
                }
                bytesRead += bytesReceived;
            }

            //调用解析方法处理包体数据
            var externalMessage = PackageUtils.DeserializeByByteArray<ExternalMessage>(recBytesBody);
            var networkInfo = new ProtocolTcpNetworkInfo(externalMessage.CmdMerge, externalMessage.Data.ToByteArray());
            return networkInfo;
        }
        catch (Exception e)
        {
            return null;
        }
    }
}