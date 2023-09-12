using HT.Framework;
using UnityEngine.UI;

namespace GameScript.RunTime.UI
{

	/// <summary>
	/// 登录
	/// </summary>
	[UIResource("UILogin", UIType.Camera)]
	public sealed class UILogin : UILogicResident
	{
		protected override bool IsAutomate => false;

		/// <summary>
		/// 初始化
		/// </summary>
		public override void OnInit()
		{
			base.OnInit();
			UIEntity.GetComponentByChild<Button>("LoginBtn").onClick.AddListener(OnClickLogin);
		}

		private void OnClickLogin()
		{
			Close();
			Main.m_UI.OpenUI<UICreateRole>();
		}
	}
}