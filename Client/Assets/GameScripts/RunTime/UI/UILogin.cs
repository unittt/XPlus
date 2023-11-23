using System.Text.RegularExpressions;
using Cysharp.Threading.Tasks;
using GameScript.RunTime.Config;
using GameScript.RunTime.Network;
using GameScript.RunTime.Procedure;
using HT.Framework;
using Pb.Mmo;
using TMPro;
using UnityEngine.UI;

namespace GameScript.RunTime.UI
{

	/// <summary>
	/// 登录
	/// </summary>
	[UIResource("UILogin", UIType.Camera)]
	public sealed class UILogin : UILogicResident
	{
		
		private InputField _account;
		private InputField _password;
		protected override bool IsAutomate => false;

		/// <summary>
		/// 初始化
		/// </summary>
		public override void OnInit()
		{
			base.OnInit();
			
			_account = UIEntity.GetComponentByChild<InputField>("AccountPart/UserNameInput");
			_password = UIEntity.GetComponentByChild<InputField>("AccountPart/PwdInput");
			
			UIEntity.GetComponentByChild<Button>("SignUpBtn").onClick.AddListener(OnClickSignUp);
			UIEntity.GetComponentByChild<Button>("LoginBtn").onClick.AddListener(() => _= OnClickLogin());
		}

		
		private void OnClickSignUp()
		{
			Main.m_UI.OpenUI<UIRegister>();
		}
		
		private async UniTaskVoid OnClickLogin()
		{
			// var acc = _account.text;
			// var pwd = _password.text;
			// // if (CheckAccount(acc) | CheckPassword(pwd))
			// // {
			// // 	Main.m_UI.OpenUI<UIMessage>("账号或密码不符合规范", null);
			// // 	return;
			// // }
			//
			// var commandResult = await new RequestCommand
			// {
			// 	Title = "登录验证",
			// 	CmdMerge = LoginCmd.LoginVerify.CmdMerge(),
			// 	// 请求参数
			// 	RequestData = () =>
			// 	{
			// 		var verify = new LoginVerify
			// 		{
			// 			Jwt = "1",
			// 			Account = acc,
			// 			Password = pwd
			// 		};
			//         
			// 		return verify;
			// 	}
			// }.ExecuteAndWait();
			//
			//
			// if (commandResult.ExternalMessage.ResponseStatus != GameCode.Success)
			// {
			// 	// Main.m_UI.OpenUI<UIMessage>(commandResult.ExternalMessage.ValidMsg, null);
			// 	return;
			// }
			//
			// //关闭界面 切换game流程
			Close();
			
			Main.m_Procedure.SwitchProcedure<ProcedureGame>();
		}
		
		// 校验创建事件页面账号格式
		private static bool CheckAccount(string accountNumber)
		{
			var valicateAccount = "^[\\w@\\$\\^!~,.\\*]{5,20}+$";
			var regex = new Regex(valicateAccount);
			var matches = regex.IsMatch(accountNumber);
			return matches;
		}

		// 密码校验
		private static bool CheckPassword(string passWord)
		{
			var valicatePassword = "^[\\w@\\$\\^!~,.\\*]{8,16}+$";
			var regex = new Regex(valicatePassword);
			var matches = regex.IsMatch(passWord);
			return matches;
		}
	}
}