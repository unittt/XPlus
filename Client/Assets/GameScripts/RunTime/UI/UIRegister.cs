using System;
using Cysharp.Threading.Tasks;
using GameScript.RunTime.Config;
using GameScript.RunTime.Network;
using HT.Framework;
using Pb.Mmo;
using UnityEngine.UI;

namespace GameScript.RunTime.UI
{
    /// <summary>
    /// 注册界面
    /// </summary>
    [UIResource("UIRegister", UIType.Camera)]
    public class UIRegister : UILogicResident
    {
        private InputField _account;
        private InputField _password;

        public override void OnInit()
        {
            _account = UIEntity.GetComponentByChild<InputField>("AccountPart/UserNameInput");
            _password = UIEntity.GetComponentByChild<InputField>("AccountPart/PwdInput");

            UIEntity.GetComponentByChild<Button>("CloseBtn").onClick.AddListener(Close);
            UIEntity.GetComponentByChild<Button>("RegisterBtn").onClick.AddListener(() => _= OnClickRegister());
            
        }


        public override void OnOpen(params object[] args)
        {
            base.OnOpen(args);
            _account.text = string.Empty;
            _password.text = string.Empty;
        }

        private async UniTaskVoid OnClickRegister()
        {
            
            var commandResult = await new RequestCommand
            {
                Title = "注册账号",
                CmdMerge = LoginCmd.RegisterAccount.CmdMerge(),
                // 请求参数
                RequestData = () =>
                {
                    var verify = new RegisterAccount
                    {
                        Account =  _account.text,
                        Password =  _password.text
                    };
			         
                    return verify;
                }
            }.ExecuteAndWait();
            
            if (commandResult.ExternalMessage.ResponseStatus != GameCode.Success)
            {
                Log.Info($"注册失败:{commandResult.ExternalMessage.ValidMsg}");
                // Main.m_UI.OpenUI<UIMessage>(commandResult.ExternalMessage.ValidMsg, null);
                return;
            }
            // Main.m_UI.OpenUI<UIMessage>("注册成功", null);
            
            Log.Info("注册成功");
            Close();
        }
    }
}