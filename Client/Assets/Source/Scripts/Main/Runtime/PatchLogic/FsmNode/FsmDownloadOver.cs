using ZEngine.Utility.State;

/// <summary>
/// 下载完毕
/// </summary>
internal class FsmDownloadOver : StateBase
{
    private StateMachine _machine;

    public override void OnEnter()
    {
        Machine.SwitchState<FsmClearCache>();
    }
}
