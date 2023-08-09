using System;
using UnityEngine;
using YooAsset;
using ZEngine.Utility.State;


/// <summary>
    /// 更新资源清单
    /// </summary>
    public sealed class FsmUpdateManifest : StateBase
    {
        private StateMachine _machine;


        public override void OnEnter()
        {
            PatchManager.Listener.OnPatchStatesChange("更新资源清单！");
            UpdateManifest().Forget();
        }
        
        private async UniTaskVoid UpdateManifest()
        {
            await UniTask.Delay(TimeSpan.FromSeconds(0.5f));
            var package = YooAssets.GetAssetsPackage("DefaultPackage");
            var operation = package.UpdatePackageManifestAsync(PatchManager.PackageVersion);
            await operation.ToUniTask();

            if(operation.Status == EOperationStatus.Succeed)
            {
                Machine.SwitchState<FsmCreateDownloader>();
            }
            else
            {
                Debug.LogWarning(operation.Error);
                PatchManager.Listener.OnPatchManifestUpdateFailed();
            }
        }
    }