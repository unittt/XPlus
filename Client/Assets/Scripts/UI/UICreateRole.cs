﻿using System.Collections.Generic;
using cfg.SkillModule;
using cfg.SystemModule;
using Cysharp.Threading.Tasks;
using HT.Framework;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// 新建UI逻辑类
/// </summary>
[UIResource( "UICreateRole",UIType.Camera)]
public class UICreateRole : UILogicResident
{
	private Image _imgRoleName;
	private Text _racedesc;
	private Image _race;
	
	private Transform _schoolContainer;
	private ToggleGroup _schoolGroup;
	private Image _characteristic;

	private Transform _skillContainer;
	private ToggleGroup _skillGroup;
	private Text _skillDes;
	
	private Dictionary<ESchoolType, GameObject> _schoolInstance;
	private Dictionary<int, GameObject> _skillInstacne;
	
	/// <summary>
	/// 初始化
	/// </summary>
    public override void OnInit()
    {
        base.OnInit();
        _schoolInstance = new Dictionary<ESchoolType, GameObject>();
        _skillInstacne = new Dictionary<int, GameObject>();
        
        _imgRoleName = UIEntity.GetComponentByChild<Image>("RoleNameSp");
        _race = UIEntity.GetComponentByChild<Image>("RightContainer/Race");
        _racedesc = UIEntity.GetComponentByChild<Text>("RightContainer/Racedesc");
       
        
        var roleBox= UIEntity.FindChildren("RoleBox").transform;
        var toggles = new List<Toggle>();
        roleBox.GetComponentsInSons(toggles);
        
        _characteristic = UIEntity.GetComponentByChild<Image>("RightContainer/School/Characteristic/Name");

        for (var i = 0; i < toggles.Count; i++)
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

        _schoolContainer = UIEntity.FindChildren("RightContainer/School/SchoolContainer").transform;
        _schoolGroup = _schoolContainer.GetComponent<ToggleGroup>();
        
        _skillContainer = UIEntity.FindChildren("RightContainer/School/SkillContainer").transform;
        _skillGroup = _skillContainer.GetComponent<ToggleGroup>();
        _skillDes = UIEntity.GetComponentByChild<Text>("RightContainer/School/SkillDes");
    }
	
	
	/// <summary>
	/// 打开UI
	/// </summary>
	public override void OnOpen(params object[] args)
	{
		base.OnOpen(args);
		OnSelectedRole(0);
		//创建场景预制件
		LoadRoleCreateMainScene();
	}
	
	

	private async UniTask LoadRoleCreateMainScene()
	{
		//角色对应的门派
		// _roleCreateMainScene = await Main.m_Resource.LoadPrefab("RoleCreateMainScene", null);
	}
	
	private async UniTaskVoid OnSelectedRole(int roleIndex)
	{
		var roleType = TableGlobal.Instance.TbRoleType.DataList[roleIndex];
		//角色名称
		_imgRoleName.sprite = await  Main.m_Resource.LoadAsset<Sprite>(roleType.NamePath);
		_imgRoleName.SetNativeSize();
		
		//描述
		_racedesc.text = roleType.Racedesc;
		//种族
		var path = $"Assets/GameRes/Atlas/StaticAtlas/RoleCreateAtlas/Image/h7_zuqun_0{(int)roleType.Race + 1}.png";
		_race.sprite = await Main.m_Resource.LoadAsset<Sprite>(path);
		_race.SetNativeSize();

		RefreshSchool(roleType.SchoolList);
	}
	
	/// <summary>
	/// 刷新门派
	/// </summary>
	/// <param name="schoolTypes"></param>
	private async UniTask RefreshSchool(List<ESchoolType> schoolTypes)
	{
		foreach (var entity in _schoolInstance.Values)
		{
			entity.SetActive(false);
		}
		
		foreach (var eSchoolType in schoolTypes)
		{
			if (!_schoolInstance.TryGetValue(eSchoolType, out  var entity))
			{
				var school = TableGlobal.Instance.TbSchool[eSchoolType];
				entity = await LoadSchool(school);
				_schoolInstance.Add(eSchoolType,entity);
			}
			
			entity.SetActive(true);
		}
		
		//默认为第一个门派
		var tg = _schoolInstance[schoolTypes[0]].GetComponent<Toggle>();
		tg.isOn = false;
		tg.isOn = true;
	}
	
	/// <summary>
	/// 加载门派信息
	/// </summary>
	/// <param name="school"></param>
	/// <returns></returns>
	private async UniTask<GameObject> LoadSchool(School school)
	{
		//角色对应的门派
		var schoolEntity = await Main.m_Resource.LoadPrefab("PartSchool", _schoolContainer, true);
		
		var icon = schoolEntity.GetComponent<Image>();
		icon.sprite = await Main.m_Resource.LoadAsset<Sprite>(school.IconPath);
		icon.SetNativeSize();

		icon = schoolEntity.GetComponentByChild<Image>("HighIcon");
		icon.sprite = await Main.m_Resource.LoadAsset<Sprite>(school.HighligtedIconPath);
		icon.SetNativeSize();
		
		icon = schoolEntity.GetComponentByChild<Image>("Name");
		icon.sprite = await Main.m_Resource.LoadAsset<Sprite>(school.NamePath);
		icon.SetNativeSize();
		
		var toggle = schoolEntity.GetComponent<Toggle>();
		toggle.group = _schoolGroup;
		var eSchoolType = school.SchoolType;
		toggle.onValueChanged.AddListener((result) =>
		{
			if (result)
			{
				OnSwitchSchool(eSchoolType);
			}
		});
		return schoolEntity;
	}

	/// <summary>
	/// 切换门派
	/// </summary>
	/// <param name="schoolType"></param>
	private async UniTaskVoid OnSwitchSchool(ESchoolType schoolType)
	{
		var school = TableGlobal.Instance.TbSchool[schoolType];
		_characteristic.sprite  = await Main.m_Resource.LoadAsset<Sprite>(school.Characteristic);
		_characteristic.SetNativeSize();
		RefreshSKill(school.SkillList);
	}

	/// <summary>
	/// 刷新技能
	/// </summary>
	/// <param name="skillList"></param>
	private async UniTask RefreshSKill(List<int> skillList)
	{
		foreach (var entity in _skillInstacne.Values)
		{
			entity.SetActive(false);
		}

		foreach (var skillID in skillList)
		{
			if (!_skillInstacne.TryGetValue(skillID, out var entity))
			{
				var schoolSkill = TableGlobal.Instance.TbSchoolSkill[skillID];
				entity = await LoadSkill(schoolSkill);
				_skillInstacne.Add(skillID, entity);
			}
			
			entity.SetActive(true);
		}
		
		//默认为第一个技能
		var tg = _skillInstacne[skillList[0]].GetComponent<Toggle>();
		tg.isOn = false;
		tg.isOn = true;
	}
	
	private async UniTask<GameObject> LoadSkill(SchoolSkill skill)
	{

		//角色对应的门派
		var entity = await Main.m_Resource.LoadPrefab("PartSkill", _skillContainer, true);
		
		var icon = entity.GetComponentByChild<Image>("icon");
		icon.sprite = await Main.m_Resource.LoadAsset<Sprite>(skill.Icon.ToString());
		icon.SetNativeSize();
		
		var toggle = entity.GetComponent<Toggle>();
		toggle.group = _skillGroup;
		var skillID = skill.Id;
		toggle.onValueChanged.AddListener((result) =>
		{
			if (result)
			{
				OnSwitchSkill(skillID);
			}
		});
		return entity;
	}

	private void OnSwitchSkill(int skillID)
	{
		_skillDes.text = TableGlobal.Instance.TbSchoolSkill[skillID].Rolecreatedesc;
	}
}