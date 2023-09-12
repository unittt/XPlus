using System;
using System.Net.Sockets;
using Com.Iohao.Message;
using Google.Protobuf;
using HT.Framework;

namespace GameScript.RunTime.Network
{
    /// <summary>
    /// 游戏的TCP协议通道
    /// </summary>
    public sealed class GameTcpChannel : ProtocolChannelBase
    {
        
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
            var gameNetworkMessage = message.Cast<GameNetworkMessage>();
            var externalMessage = new ExternalMessage
            {
                CmdCode = gameNetworkMessage.Cmd,
                CmdMerge = gameNetworkMessage.CmdMerge,
                ProtocolSwitch = 0,
            };

            if (gameNetworkMessage.MsgByteString != null)
            {
                externalMessage.Data = gameNetworkMessage.MsgByteString;
            }

            var byteMsg = externalMessage.ToByteArray();
            // 在数据前加4个字节 用来描述数据长度
            var package = new byte[byteMsg.Length + 4];
            var intArr = PackageUtils.IntToArr(byteMsg.Length);
            intArr.CopyTo(package, 0);
            byteMsg.CopyTo(package, intArr.Length);

            return package;
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
                // 接收消息体长度字段（4字节）
                var bodyLengthBytes = new byte[4];
                var l = client.Receive(bodyLengthBytes);
                if (l == 0)
                {
                    return null;
                }
                if (l != 4)
                {
                    // var bytes = new byte[client.ReceiveBufferSize];
                    // var receive = client.Receive(buffer: bytes);
                   "出现错误了，已清空缓冲区，消息可能丢失".Error();
                }
                
                Array.Reverse(bodyLengthBytes);
                
                var length = BitConverter.ToInt32(bodyLengthBytes, 0);
                var dataBytes = new byte[length];
                var dataLength = 0;
                while (true) 
                {
                    if (dataLength == length)
                    {
                        break;
                    }
                    dataLength += client.Receive(dataBytes, dataLength, dataBytes.Length, SocketFlags.None);
                }
                
                //调用解析方法处理包体数据
                var externalMessage = PackageUtils.DeserializeByByteArray<ExternalMessage>(dataBytes);
                var gameNetworkMessage = new GameNetworkMessage(externalMessage.CmdMerge, externalMessage.Data);
                return gameNetworkMessage;
            }
            catch (Exception)
            {
                return null;
            }
        }
    }
}