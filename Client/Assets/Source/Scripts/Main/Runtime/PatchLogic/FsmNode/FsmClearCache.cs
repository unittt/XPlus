using ZEngine.Utility.State;

/// <summary>
/// 清理未使用的缓存文件
/// </summary>
internal class FsmClearCache : StateBase
{
	
	public override void OnEnter()
	{
		PatchEventDefine.PatchStatesChange.SendEventMessage("清理未使用的缓存文件！");
		var package = YooAsset.YooAssets.GetPackage("DefaultPackage");
		var operation = package.ClearUnusedCacheFilesAsync();
		operation.Completed += Operation_Completed;
	}

	private void Operation_Completed(YooAsset.AsyncOperationBase obj)
	{
		Machine.SwitchState<FsmPatchDone>();
	}
}