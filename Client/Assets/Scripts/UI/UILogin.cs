using HT.Framework;
using UnityEngine.UI;

/// <summary>
/// 登录
/// </summary>
[UIResource("UILogin", UIType.Camera)]
public class UILogin : UILogicResident
{
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

	/// <summary>
	/// 打开UI
	/// </summary>
    public override void OnOpen(params object[] args)
    {
        base.OnOpen(args);
    }
	/// <summary>
	/// 关闭UI
	/// </summary>
    public override void OnClose()
    {
        base.OnClose();
    }
	/// <summary>
	/// 销毁UI
	/// </summary>
    public override void OnDestroy()
    {
        base.OnDestroy();
    }
	/// <summary>
	/// UI逻辑刷新
	/// </summary>
    public override void OnUpdate()
    {
        base.OnUpdate();
    }
}