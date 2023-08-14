using System.Collections;
using UniFramework.Singleton;
using YooAsset;
using ZEngine.Utility.State;

/// <summary>
/// 下载更新文件
/// </summary>
public class FsmDownloadFiles : StateBase
{
	public override void OnEnter()
	{
		PatchEventDefine.PatchStatesChange.SendEventMessage("开始下载补丁文件！");
		UniSingleton.StartCoroutine(BeginDownload());
	}

	
	private IEnumerator BeginDownload()
	{
		var downloader = PatchManager.Instance.Downloader;

		// 注册下载回调
		downloader.OnDownloadErrorCallback = PatchEventDefine.WebFileDownloadFailed.SendEventMessage;
		downloader.OnDownloadProgressCallback = PatchEventDefine.DownloadProgressUpdate.SendEventMessage;
		downloader.BeginDownload();
		yield return downloader;

		// 检测下载结果
		if (downloader.Status != EOperationStatus.Succeed)
			yield break;

		Machine.SwitchState<FsmPatchDone>();
	}
}