﻿using System.Collections;
using UniFramework.Machine;
using UniFramework.Singleton;
using UnityEngine;
using YooAsset;

/// <summary>
/// 更新资源清单
/// </summary>
public class FsmUpdateManifest : StateBase
{
	public override void OnEnter()
	{
		PatchEventDefine.PatchStatesChange.SendEventMessage("更新资源清单！");
		UniSingleton.StartCoroutine(UpdateManifest());
	}
	
	private IEnumerator UpdateManifest()
	{
		yield return new WaitForSecondsRealtime(0.5f);
		
		var package = YooAssets.GetPackage("DefaultPackage");
		var operation = package.UpdatePackageManifestAsync(PatchManager.Instance.PackageVersion);
		yield return operation;

		if(operation.Status == EOperationStatus.Succeed)
		{
			Machine.SwitchState<FsmCreateDownloader>();
		}
		else
		{
			Debug.LogWarning(operation.Error);
			PatchEventDefine.PatchManifestUpdateFailed.SendEventMessage();
		}
	}
}