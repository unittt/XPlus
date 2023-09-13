using GameScript.RunTime.Network;
using GameScript.RunTime.UI;
using HT.Framework;
using Pb.Mmo;

namespace GameScript.RunTime.Procedure
{
    /// <summary>
    /// 登录流程
    /// </summary>
    public sealed class ProcedureLogin : ProcedureBase
    {

        /// <summary>
        /// 进入流程
        /// </summary>
        /// <param name="lastProcedure">上一个离开的流程</param>
        public override void OnEnter(ProcedureBase lastProcedure)
        {
            base.OnEnter(lastProcedure);
            Log.Info("进入登录流程");
            Main.m_UI.OpenUI<UILogin>();

            GameNetManager.ConnectServerFailEvent += OnConnectServerFailEvent;
            GameNetManager.ConnectServerSuccessEvent += OnConnectServerSuccessEvent;
            GameNetManager.Subscribe(1, 1, OnReceiveMessage);
            GameNetManager.ConnectServer();
        }

        private void SendMsg()
        {
            //此处仅作为为发送msg的示例 参数需要自己根据实际情况填写
            LoginVerify loginVerify = new LoginVerify();
            loginVerify.Jwt = "1";
            GameNetManager.SendMessage(1, 1, loginVerify);
        }

        private void OnReceiveMessage(GameNetworkMessage arg)
        {
            var userInfo = arg.GetMessage<UserInfo>();

            Log.Info($"接收到消息 Cmd:{arg.Cmd}   subCmd:{arg.SubCmd}   userInfo:{userInfo.Nickname}");
        }

        private void OnConnectServerSuccessEvent(GameTcpChannel arg)
        {
            Log.Info($"服务器连接成功");
            SendMsg();
        }

        private void OnConnectServerFailEvent(GameTcpChannel arg)
        {
            Log.Info($"服务器连接失败");
        }
    }
}