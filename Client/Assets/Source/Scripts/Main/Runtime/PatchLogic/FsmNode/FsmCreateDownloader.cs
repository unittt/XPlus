﻿using System.Collections;
using System.Collections.Generic;
using UniFramework.Singleton;
using UnityEngine;
using YooAsset;
using ZEngine.Utility.State;

/// <summary>
/// 创建文件下载器
/// </summary>
public class FsmCreateDownloader : StateBase
{
	public override void OnEnter()
	{
		PatchEventDefine.PatchStatesChange.SendEventMessage("创建补丁下载器！");
		UniSingleton.StartCoroutine(CreateDownloader());
	}
	
	IEnumerator CreateDownloader()
	{
		yield return new WaitForSecondsRealtime(0.5f);

		int downloadingMaxNum = 10;
		int failedTryAgain = 3;
		var downloader = YooAssets.CreateResourceDownloader(downloadingMaxNum, failedTryAgain);
		PatchManager.Instance.Downloader = downloader;

		if (downloader.TotalDownloadCount == 0)
		{
			Debug.Log("Not found any download files !");
			Machine.SwitchState<FsmDownloadOver>();
		}
		else
		{
			//A total of 10 files were found that need to be downloaded
			Debug.Log($"Found total {downloader.TotalDownloadCount} files that need download ！");

			// 发现新更新文件后，挂起流程系统
			// 注意：开发者需要在下载前检测磁盘空间不足
			int totalDownloadCount = downloader.TotalDownloadCount;
			long totalDownloadBytes = downloader.TotalDownloadBytes;
			PatchEventDefine.FoundUpdateFiles.SendEventMessage(totalDownloadCount, totalDownloadBytes);
		}
	}
}