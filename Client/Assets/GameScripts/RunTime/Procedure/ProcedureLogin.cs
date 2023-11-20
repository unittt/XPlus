using GameScript.RunTime.Network;
using GameScript.RunTime.UI;
using HT.Framework;

namespace GameScript.RunTime.Procedure
{
    
    /// <summary>
    /// 登录流程
    /// </summary>
    public class ProcedureLogin : ProcedureBase
    {
        /// <summary>
        /// 进入流程
        /// </summary>
        /// <param name="lastProcedure">上一个离开的流程</param>
        public override void OnEnter(ProcedureBase lastProcedure)
        {
            base.OnEnter(lastProcedure);
            GameNetManager.ConnectServerFailEvent += OnConnectServerFailEvent;
            GameNetManager.ConnectServerSuccessEvent += OnConnectServerSuccessEvent;
            GameNetManager.ConnectServer();
        }

        // private static void RequestLoginVerify()
        // {
        //     // 此处仅作为为发送msg的示例 参数需要自己根据实际情况填写
        //
        //     new RequestCommand
        //     {
        //         Title = "登录验证",
        //         CmdMerge = LoginCmd.LoginVerify.CmdMerge(),
        //         // 请求参数
        //         RequestData = () =>
        //         {
        //             var verify = new LoginVerify
        //             {
        //                 Jwt = "1"
        //             };
        //
        //             return verify;
        //         },
        //         // 请求回调
        //         Callback = result =>
        //         {
        //             var userInfo = result.GetValue<UserInfo>();
        //             var cmdToString = result.CmdToString();
        //
        //             $"接收到消息 Cmd:{cmdToString}   userInfo:{userInfo.Nickname}".Info();
        //
        //             // 查询背包
        //             RequestBag();
        //         }
        //     }.Execute();
        //
        //     // 广播监听
        //     new CommandListenBroadcast
        //     {
        //         Title = "获得新物品",
        //         CmdMerge = CommonCmd.BroadcastShowItem.CmdMerge(),
        //         Callback = result =>
        //         {
        //             var showItemMessages = result.ListValue<ShowItemMessage>();
        //             foreach (var showItemMessage in showItemMessages)
        //             {
        //                 $"获得新物品 {showItemMessage}".Info();
        //             }
        //         }
        //     }.Listen();
        // }


        // private static async UniTaskVoid RequestLoginVerifyTaskVoid()
        // {
        //     var commandResult = await new RequestCommand
        //     {
        //         Title = "登录验证",
        //         CmdMerge = LoginCmd.LoginVerify.CmdMerge(),
        //         // 请求参数
        //         RequestData = () =>
        //         {
        //             var verify = new LoginVerify
        //             {
        //                 Jwt = "1",
        //                 Account = "zhangchao",
        //                 Password = "zhangchaomima"
        //             };
        //
        //             
        //             return verify;
        //         }
        //     }.ExecuteAndWait();
        //     
        //
        //    var userInfo = commandResult.GetValue<UserInfo>();
        //    var cmdToString = commandResult.CmdToString();
        //    $"接收到消息 Cmd:{cmdToString}   userInfo:{userInfo.Nickname}".Info();
        //
        //
        //
        //    var commandResultBag = await new RequestCommand
        //    {
        //        Title = "查询背包",
        //        CmdMerge = BagCmd.Bag.CmdMerge(),
        //    }.ExecuteAndWait();
        //
        //    var itemMap = commandResultBag.GetValue<BagMessage>().ItemMap;
        //    foreach (var kv in itemMap)
        //    {
        //        var key = kv.Key;
        //        var bagItemMessage = kv.Value;
        //    
        //        $"物品{key} {bagItemMessage}".Info();
        //    }
        // }
        

        // private static void RequestBag()
        // {
        //     new RequestCommand
        //     {
        //         Title = "查询背包",
        //         CmdMerge = BagCmd.Bag.CmdMerge(),
        //         // 请求回调
        //         Callback = result =>
        //         {
        //             var bagMessage = result.GetValue<BagMessage>();
        //
        //             var itemMap = bagMessage.ItemMap;
        //             foreach (var kv in itemMap)
        //             {
        //                 var key = kv.Key;
        //                 var bagItemMessage = kv.Value;
        //
        //                 $"物品{key} {bagItemMessage}".Info();
        //             }
        //         }
        //     }.Execute();
        // }


        private void OnConnectServerSuccessEvent(GameTcpChannel arg)
        {
            //服务器连接成功 后打开登陆界面
            Main.m_UI.OpenUI<UILogin>();
        }

        private void OnConnectServerFailEvent(GameTcpChannel arg)
        {
            // Log.Info($"服务器连接失败");
        }
    }
}