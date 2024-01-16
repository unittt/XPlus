using System;
using System.Collections.Generic;
using Cysharp.Threading.Tasks.Linq;
using HT.Framework;
using UnityEngine;

namespace GameScripts.RunTime.Utility.Timer
{
    /// <summary>
    /// 定时器管理
    /// </summary>
    public static partial class TimerManager
    {
        
        internal static int CurUnitIdx;
        /// <summary>
        /// Zero
        /// </summary>
        public static readonly TimeSpan Zero = new(0);
        /// <summary>
        /// 应用启动时间
        /// </summary>
        public static DateTime StartupDateTime { get; private set; }
        /// <summary>
        /// 应用当前时间
        /// </summary>
        public static DateTime Now => StartupDateTime.AddSeconds(Time.realtimeSinceStartup);
        /// <summary>
        /// 应用当前时间跨度
        /// </summary>
        public static TimeSpan NowTS => new(Now.Ticks);
        
        private static readonly Dictionary<int, ITimer> _timerInstances; // 定时器实例
        private static readonly List<ITimer> _ruingInstances; //运行的定时器实体
        
        static TimerManager()
        {
            _timerInstances = new Dictionary<int, ITimer>();
            _ruingInstances = new List<ITimer>();
            
            UniTaskAsyncEnumerable.EveryUpdate().Where(_ => _timerInstances.Count > 0).ForEachAsync(_ =>
            {
                _ruingInstances.Clear();
                _ruingInstances.AddRange( _timerInstances.Values);
               
                foreach (var timer in _ruingInstances)
                {
                    if (timer.State == TimerState.Running)
                    {
                        timer.OnUpdate();
                    }
                    
                    if (timer.State == TimerState.Done)
                    {
                        UnRegister(timer);
                    }
                }
            });
        }
        
        /// <summary>
        /// 注册定时器
        /// </summary>
        /// <param name="timer"></param>
        private static void Register(ITimer timer)
        {
            if (_timerInstances.ContainsKey(timer.ID))
            {
                Log.Error("计时器 重复注册");
                return;
            }
            _timerInstances.Add(timer.ID, timer);
            timer.Launch();
        }

        /// <summary>
        /// 移除定时器
        /// </summary>
        /// <param name="timer">定时器</param>
        private static void UnRegister(ITimer timer)
        {
            if (!_timerInstances.Remove(timer.ID)) return;
            timer.Stop();
            Main.m_ReferencePool.Despawn(timer);
        }

        /// <summary>
        /// 清除所有订阅的计时器
        /// </summary>
        public static void ClearSubscribe()
        {
            foreach (var time in _timerInstances.Values)
            {
                Main.m_ReferencePool.Despawn(time);
            }

            _timerInstances.Clear();
        }
    }
}
 