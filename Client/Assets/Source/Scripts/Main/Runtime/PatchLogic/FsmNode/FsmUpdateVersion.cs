using System;
using UnityEngine;
using YooAsset;
using ZEngine.Utility.State;


/// <summary>
	/// 更新资源版本号
	/// </summary>
	internal class FsmUpdateVersion : StateBase
	{
		private StateMachine _machine;

		public override void OnEnter()
		{
			PatchManager.Listener.OnPatchStatesChange("获取最新的资源版本 !");
			GetStaticVersion().Forget();
		}
		
		private async UniTaskVoid GetStaticVersion()
		{
			await UniTask.Delay(TimeSpan.FromSeconds(0.5f));
			var package = YooAssets.GetAssetsPackage("DefaultPackage");
			var operation = package.UpdatePackageVersionAsync();
			await operation.ToUniTask();

			if (operation.Status == EOperationStatus.Succeed)
			{
				PatchManager.PackageVersion = operation.PackageVersion;
				Machine.SwitchState<FsmUpdateManifest>();
			}
			else
			{
				Debug.LogWarning(operation.Error);
				PatchManager.Listener.OnPackageVersionUpdateFailed();
			}
		}
	}