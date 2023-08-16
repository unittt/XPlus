using UnityEngine;
using UnityEditor;
using HT.Framework;
/// <summary>
/// 热更设置项
/// </summary>
public class HotfixSettingItem: SettingItemBase
{
    /// <summary>
    /// 设置面板的显示名称
    /// </summary>
    public override string Name
    {
        get
        {
            return "Hotfix";
        }
    }

    private HotFixSettings _hotFixSettings;
    
    /// <summary>
    /// 开始设置
    /// </summary>
    public override void OnBeginSetting()
    {
        _hotFixSettings  = Resources.Load<HotFixSettings>("HotFixSettings");
    }
    /// <summary>
    /// 设置面板UI
    /// </summary>
    public override void OnSettingGUI()
    {
        base.OnSettingGUI();
        if (_hotFixSettings)
        {
            GUILayout.BeginVertical();
            _hotFixSettings.HostServerURL = EditorGUILayout.TextField("关卡等级",  _hotFixSettings.HostServerURL);
            _hotFixSettings.FallbackHostServerURL = EditorGUILayout.TextField("关卡等级",  _hotFixSettings.FallbackHostServerURL);
            _hotFixSettings.WindowsUpdateDataURL = EditorGUILayout.TextField("关卡等级",  _hotFixSettings.WindowsUpdateDataURL);
            _hotFixSettings.IOSUpdateDataURL = EditorGUILayout.TextField("关卡等级",  _hotFixSettings.IOSUpdateDataURL);
            _hotFixSettings.AndroidUpdateDataURL = EditorGUILayout.TextField("关卡等级",  _hotFixSettings.AndroidUpdateDataURL);
            GUILayout.EndVertical();
        }
    }
    /// <summary>
    /// 结束设置
    /// </summary>
    public override void OnEndSetting()
    {
        base.OnEndSetting();
    }
    /// <summary>
    /// 重置所有设置
    /// </summary>
    public override void OnReset()
    {
        _hotFixSettings.HostServerURL = "";
        _hotFixSettings.FallbackHostServerURL = "";
        _hotFixSettings.WindowsUpdateDataURL = "";
        _hotFixSettings.IOSUpdateDataURL = "";
        _hotFixSettings.AndroidUpdateDataURL = "";
    }
}