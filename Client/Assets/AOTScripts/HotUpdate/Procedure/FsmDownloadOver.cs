using UniFramework.Machine;

/// <summary>
/// 下载完毕
/// </summary>
internal class FsmDownloadOver : StateBase
{
	public override void OnEnter()
	{
		Machine.SwitchState<FsmClearCache>();
	}
}