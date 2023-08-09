using System;
using System.Collections.Generic;
using HT.Framework;
using UnityEngine;
using UnityEngine.UI;


public class PatchWindow : MonoBehaviour, IPatchListener
{
	/// <summary>
	/// 对话框封装类
	/// </summary>
	private class MessageBox
	{
		private GameObject _cloneObject;
		private Text _content;
		private Button _btnOK;
		private Action _clickOK;

		public bool ActiveSelf => _cloneObject.activeSelf;

		public void Create(GameObject cloneObject)
		{
			_cloneObject = cloneObject;
			_content = cloneObject.transform.Find("txt_content").GetComponent<Text>();
			_btnOK = cloneObject.transform.Find("btn_ok").GetComponent<Button>();
			_btnOK.onClick.AddListener(OnClickYes);
		}

		public void Show(string content, System.Action clickOK)
		{
			_content.text = content;
			_clickOK = clickOK;
			_cloneObject.SetActive(true);
			_cloneObject.transform.SetAsLastSibling();
		}

		public void Hide()
		{
			_content.text = string.Empty;
			_clickOK = null;
			_cloneObject.SetActive(false);
		}

		private void OnClickYes()
		{
			_clickOK?.Invoke();
			Hide();
		}
	}

	private readonly List<MessageBox> _msgBoxList = new List<MessageBox>();

	// UGUI相关
	private GameObject _messageBoxObj;
	private Slider _slider;
	private Text _tips;

	private void Awake()
	{
		_slider = transform.Find("UIWindow/Slider").GetComponent<Slider>();
		_tips = transform.Find("UIWindow/Slider/txt_tips").GetComponent<Text>();
		_tips.text = "Initializing the game world !";
		_messageBoxObj = transform.Find("UIWindow/MessgeBox").gameObject;
		_messageBoxObj.SetActive(false);
		PatchManager.SetListener(this);

	}

	private void Start()
	{
		Main.m_Procedure.AnyProcedureSwitchEvent += ProcedureSwitchEvent;
	}

	private void ProcedureSwitchEvent(ProcedureBase arg1, ProcedureBase arg2)
	{
		Main.m_Procedure.AnyProcedureSwitchEvent -= ProcedureSwitchEvent;
		GameObject.Destroy(gameObject);
	}

	/// <summary>
	/// 当初始化失败
	/// </summary>
	public void OnInitializeFailed()
	{
		ShowMessageBox($"Failed to initialize package !", PatchManager.UserTryInitialize);
	}

	/// <summary>
	/// 当补丁流程步骤改变
	/// </summary>
	public void OnPatchStatesChange(string tips)
	{
		_tips.text = tips;
	}

	/// <summary>
	/// 当发现更新文件
	/// </summary>
	public void OnFoundUpdateFiles(int totalCount, long totalSizeBytes)
	{
		var sizeMB = totalSizeBytes / 1048576f;
		sizeMB = Mathf.Clamp(sizeMB, 0.1f, float.MaxValue);
		var totalSizeMB = sizeMB.ToString("f1");
		ShowMessageBox($"Found update patch files, Total count {totalCount.ToString()} Total size {totalSizeMB}MB",
			PatchManager.UserBeginDownloadWebFiles);
	}

	/// <summary>
	/// 当下载进度更新
	/// </summary>
	public void OnDownloadProgressUpdate(int totalDownloadCount, int currentDownloadCount,
		long totalDownloadSizeBytes, long currentDownloadSizeBytes)
	{
		_slider.value = (float)currentDownloadCount / totalDownloadCount;
		var currentSizeMB = (currentDownloadSizeBytes / 1048576f).ToString("f1");
		var totalSizeMB = (totalDownloadSizeBytes / 1048576f).ToString("f1");
		_tips.text =
			$"{currentDownloadCount.ToString()}/{totalDownloadCount.ToString()} {currentSizeMB}MB/{totalSizeMB}MB";
	}

	/// <summary>
	/// 资源版本号更新失败
	/// </summary>
	public void OnPackageVersionUpdateFailed()
	{
		ShowMessageBox($"Failed to update static version, please check the network status.",
			PatchManager.UserTryUpdatePackageVersion);
	}

	/// <summary>
	/// 补丁清单更新失败
	/// </summary>
	public void OnPatchManifestUpdateFailed()
	{
		ShowMessageBox($"Failed to update patch manifest, please check the network status.",
			PatchManager.UserTryUpdatePatchManifest);
	}

	/// <summary>
	/// 网络文件下载失败
	/// </summary>
	public void OnWebFileDownloadFailed(string fileName, string error)
	{
		ShowMessageBox($"Failed to download file : {fileName}", PatchManager.UserTryDownloadWebFiles);
	}


	/// <summary>
	/// 显示对话框
	/// </summary>
	private void ShowMessageBox(string content, System.Action ok)
	{
		// 尝试获取一个可用的对话框
		MessageBox msgBox = null;
		foreach (var item in _msgBoxList)
		{
			if (item.ActiveSelf) continue;
			msgBox = item;
			break;
		}

		// 如果没有可用的对话框，则创建一个新的对话框
		if (msgBox == null)
		{
			msgBox = new MessageBox();
			var cloneObject = Instantiate(_messageBoxObj, _messageBoxObj.transform.parent);
			msgBox.Create(cloneObject);
			_msgBoxList.Add(msgBox);
		}

		// 显示对话框
		msgBox.Show(content, ok);
	}
}
