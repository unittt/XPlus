
    using ZEngine.Utility.State;

    /// <summary>
    /// 流程更新完毕
    /// </summary>
    internal class FsmPatchDone : StateBase
    {
        public override void OnEnter()
        {
            PatchManager.Listener.OnPatchStatesChange("开始游戏！");
            PatchManager.ThrowDone();
        }
    }
