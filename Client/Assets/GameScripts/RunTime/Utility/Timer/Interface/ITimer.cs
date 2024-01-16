using HT.Framework;

namespace GameScripts.RunTime.Utility.Timer
{
    /// <summary>
    /// 计时器工具接口
    /// </summary>
    public interface ITimer : IReference
    {
        /// <summary>
        /// 编号
        /// </summary>
        int ID { get;}
        /// <summary>
        /// 状态
        /// </summary>
        TimerState State { get; }
        
        /// <summary>
        /// 启动计时
        /// </summary>
        void Launch();

        /// <summary>
        /// 暂停
        /// </summary>
        void Pause();

        /// <summary>
        /// 恢复/继续
        /// </summary>
        void Resume();

        /// <summary>
        /// 停止
        /// </summary>
        void Stop();
        
        /// <summary>
        /// 刷新
        /// </summary>
        void OnUpdate();
    }
    
    /// <summary>
    /// 计时器状态
    /// </summary>
    public enum TimerState
    {
        /// <summary>
        /// 运行中
        /// </summary>
        Running,
        /// <summary>
        /// 暂停
        /// </summary>
        Pause,
        /// <summary>
        /// 完成
        /// </summary>
        Done
    }
}