using System.Collections.Generic;
using Cysharp.Threading.Tasks;
using HT.Framework;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// 新建UI逻辑类
/// </summary>
[UIResource("AssetBundleName", "UICreateRole", "ResourcePath",UIType.Camera)]
public class UICreateRole : UILogicResident
{
	private Image _imgRoleName;
	
	/// <summary>
	/// 初始化
	/// </summary>
    public override void OnInit()
    {
        base.OnInit();
        
        _imgRoleName = UIEntity.GetComponentByChild<Image>("RoleNameSp");
        var roleBox= UIEntity.FindChildren("RoleBox").transform;
        var toggles = new List<Toggle>();
        roleBox.GetComponentsInSons<Toggle>(toggles);

        for (int i = 0; i < toggles.Count; i++)
        {
	        var toggle = toggles[i];
	        var roleIndex = i;
	        toggle.onValueChanged.AddListener((result) =>
	        {
		        if (result)
		        {
			        OnSelectedRole(roleIndex).Forget();
		        }
	        });
        }
    }
	
	
	/// <summary>
	/// 打开UI
	/// </summary>
	public override void OnOpen(params object[] args)
	{
		base.OnOpen(args);
		OnSelectedRole(0);
	}
	

	private async UniTask OnSelectedRole(int roleIndex)
	{
		var roleType = TableGlobal.Instance.TbRoleType.DataList[roleIndex];
		var dataSetInfo = new PrefabInfo("",roleType.NamePath,"");
		_imgRoleName.sprite = await  Main.m_Resource.LoadAssetAsync<Sprite>(dataSetInfo, null);
		_imgRoleName.SetNativeSize();
	}
	
}