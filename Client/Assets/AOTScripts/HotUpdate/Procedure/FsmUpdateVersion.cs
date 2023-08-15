using System.Collections;
using UniFramework.Machine;
using UniFramework.Singleton;
using UnityEngine;
using YooAsset;


/// <summary>
/// 更新资源版本号
/// </summary>
internal class FsmUpdateVersion : StateBase
{
	public override void OnEnter()
	{
		PatchEventDefine.PatchStatesChange.SendEventMessage("获取最新的资源版本 !");
		UniSingleton.StartCoroutine(GetStaticVersion());
	}
	
	private IEnumerator GetStaticVersion()
	{
		yield return new WaitForSecondsRealtime(0.5f);

		var package = YooAssets.GetPackage("DefaultPackage");
		var operation = package.UpdatePackageVersionAsync();
		yield return operation;

		if (operation.Status == EOperationStatus.Succeed)
		{
			PatchManager.Instance.PackageVersion = operation.PackageVersion;
			Debug.Log($"远端最新版本为: {operation.PackageVersion}");
			Machine.SwitchState<FsmUpdateManifest>();
		}
		else
		{
			Debug.LogWarning(operation.Error);
			PatchEventDefine.PackageVersionUpdateFailed.SendEventMessage();
		}
	}
}