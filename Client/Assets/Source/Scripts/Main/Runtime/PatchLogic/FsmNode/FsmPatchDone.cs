using ZEngine.Utility.State;

/// <summary>
/// 流程更新完毕
/// </summary>
internal class FsmPatchDone : StateBase
{
	public override void OnEnter()
	{
		// PatchEventDefine.PatchStatesChange.SendEventMessage("开始游戏！");
		//
		// // 创建游戏管理器
		// UniSingleton.CreateSingleton<GameManager>();
		//
		// // 开启游戏流程
		// GameManager.Instance.Run();
	}
}