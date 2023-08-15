namespace UniFramework.Machine
{
    /// <summary>
    /// 节点状态接口
    /// </summary>
    public abstract class StateBase
    {
        public StateMachine Machine { get; internal set; }

        /// <summary>
        /// 初始化
        /// </summary>
        public virtual void OnInit()
        {
            
        }

        /// <summary>
        /// 进入状态
        /// </summary>
        public virtual void OnEnter()
        {
            
        }

        /// <summary>
        /// 每帧更新状态
        /// </summary>
        public virtual void OnUpdate()
        {
            
        }

        /// <summary>
        /// 离开状态
        /// </summary>
        public virtual void OnExit()
        {
            
        }
    }
}