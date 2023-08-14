using UnityEngine;
using UniFramework.Event;
using UniFramework.Singleton;
using YooAsset;
using ZEngine.Utility.State;

public class PatchManager : SingletonInstance<PatchManager>, ISingleton
{
	/// <summary>
	/// 运行模式
	/// </summary>
	public EPlayMode PlayMode { private set; get; }

	/// <summary>
	/// 包裹的版本信息
	/// </summary>
	public string PackageVersion { set; get; }

	/// <summary>
	/// 下载器
	/// </summary>
	public ResourceDownloaderOperation Downloader { set; get; }


	private bool _isRun = false;
	private EventGroup _eventGroup = new EventGroup();
	private StateMachine _machine;

	void ISingleton.OnCreate(object createParam)
	{
	}
	void ISingleton.OnDestroy()
	{
		_eventGroup.RemoveAllListener();
	}
	void ISingleton.OnUpdate()
	{
		if (_machine != null)
			_machine.Update();
	}

	/// <summary>
	/// 开启流程
	/// </summary>
	public void Run(EPlayMode playMode)
	{
		if (_isRun == false)
		{
			_isRun = true;
			PlayMode = playMode;

			// 注册监听事件
			_eventGroup.AddListener<UserEventDefine.UserTryInitialize>(OnHandleEventMessage);
			_eventGroup.AddListener<UserEventDefine.UserBeginDownloadWebFiles>(OnHandleEventMessage);
			_eventGroup.AddListener<UserEventDefine.UserTryUpdatePackageVersion>(OnHandleEventMessage);
			_eventGroup.AddListener<UserEventDefine.UserTryUpdatePatchManifest>(OnHandleEventMessage);
			_eventGroup.AddListener<UserEventDefine.UserTryDownloadWebFiles>(OnHandleEventMessage);

			Debug.Log("开启补丁更新流程...");
			_machine = new StateMachine(this);
			_machine.AddState<FsmPatchPrepare>();
			_machine.AddState<FsmInitialize>();
			_machine.AddState<FsmUpdateVersion>();
			_machine.AddState<FsmUpdateManifest>();
			_machine.AddState<FsmCreateDownloader>();
			_machine.AddState<FsmDownloadFiles>();
			_machine.AddState<FsmDownloadOver>();	
			_machine.AddState<FsmClearCache>();
			_machine.AddState<FsmPatchDone>();
			_machine.Run<FsmPatchPrepare>();
		}
		else
		{
			Debug.LogWarning("补丁更新已经正在进行中!");
		}
	}

	/// <summary>
	/// 接收事件
	/// </summary>
	private void OnHandleEventMessage(IEventMessage message)
	{
		if (message is UserEventDefine.UserTryInitialize)
		{
			_machine.SwitchState<FsmInitialize>();
		}
		else if (message is UserEventDefine.UserBeginDownloadWebFiles)
		{
			_machine.SwitchState<FsmDownloadFiles>();
		}
		else if (message is UserEventDefine.UserTryUpdatePackageVersion) 
		{
			_machine.SwitchState<FsmUpdateVersion>(); 
		}
		else if (message is UserEventDefine.UserTryUpdatePatchManifest)
		{
			_machine.SwitchState<FsmUpdateManifest>();
		}
		else if (message is UserEventDefine.UserTryDownloadWebFiles)
		{
			_machine.SwitchState<FsmCreateDownloader>();
		}
		else
		{
			throw new System.NotImplementedException($"{message.GetType()}");
		}
	}
}