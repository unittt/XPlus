using UnityEngine;
using ZEngine.Utility.State;


/// <summary>
    /// 流程准备工作
    /// </summary>
    internal class FsmPatchPrepare : StateBase
    {
        private StateMachine _machine;


        public override void OnEnter()
        {
            // 加载更新面板
            var go = Resources.Load<GameObject>("PatchWindow");
            GameObject.Instantiate(go);
            Machine.SwitchState<FsmInitialize>();
        }
        
    }
