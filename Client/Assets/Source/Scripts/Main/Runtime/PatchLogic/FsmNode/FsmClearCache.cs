using ZEngine.Utility.State;

    /// <summary>
    /// 清理未使用的缓存文件
    /// </summary>
    internal sealed class FsmClearCache : StateBase
    {
        
        public override void OnEnter()
        {
            PatchManager.Listener.OnPatchStatesChange("清理未使用的缓存文件！");
            var package = YooAsset.YooAssets.GetAssetsPackage("DefaultPackage");
            var operation = package.ClearUnusedCacheFilesAsync();
            operation.Completed += Operation_Completed;
        }
        
        private void Operation_Completed(YooAsset.AsyncOperationBase obj)
        {
            _machine.ChangeState<FsmPatchDone>();
        }
    }
