using System;
using HT.Framework;

namespace GameScripts.RunTime.Utility.Timer
{
    public static partial class TimerManager
    {
        #region 公共方法
        /// <summary>
        /// 是否存在
        /// </summary>
        /// <param name="id">协程迭代器ID</param>
        /// <returns>是否存在</returns>
        public static bool IsExist(int id)
        {
            return _timerInstances.ContainsKey(id);
        }
        /// <summary>
        /// 是否正在运行中
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public static bool IsRunning(int id)
        {
            if (!IsExist(id)) return false;
            return _timerInstances[id].State == TimerState.Running;
        }
        /// <summary>
        /// 是否暂停
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public static bool IsPause(int id)
        {
            if (!IsExist(id)) return false;
            return _timerInstances[id].State == TimerState.Pause;
        }

        /// <summary>
        /// 暂停计时器
        /// </summary>
        /// <param name="id"></param>
        public static void PauseTimer(int id)
        {
            if (!IsRunning(id)) return;
            _timerInstances[id].Pause();
        }

        /// <summary>
        /// 恢复计时器
        /// </summary>
        /// <param name="id"></param>
        public static void ResumeTimer(int id)
        {
            if (!IsPause(id)) return;
            _timerInstances[id].Resume();
        }

        /// <summary>
        /// 关闭计时器
        /// </summary>
        /// <param name="id"></param>
        public static void StopTimer(int id)
        {
            if (!_timerInstances.TryGetValue(id, out var timer)) return;
            UnRegister(timer);
        }
        #endregion
        
        #region 定时器
         /// <summary>
         /// 注册定时器
         /// </summary>
         /// <param name="delayTime">延迟时间</param>
         /// <param name="onCompleteAction">结束回调</param>
         /// <param name="delayTime">是否忽略时间速率</param>
         /// <returns></returns>
         public static int RegisterTimer(float delayTime, HTFAction onCompleteAction, bool isIgnoreTimeScale = true)
         {
             return RegisterTimer(delayTime, 0, 1, null, null, onCompleteAction, isIgnoreTimeScale);
         }
         /// <summary>
         /// 注册定时器
         /// </summary>
         /// <param name="delayTime">延迟时间</param>
         /// <param name="interval">间隔秒数</param>
         /// <param name="loop">循环次数</param>
         /// <param name="onStartAction">开始回调</param>
         /// <param name="onUpdateAction">运行中回调</param>
         /// <param name="onCompleteAction">结束回调</param>
         /// <param name="delayTime">是否忽略时间速率</param>
         /// <returns></returns>
         public static int RegisterTimer(float delayTime, float interval, int loop, HTFAction onStartAction,
             HTFAction<int> onUpdateAction, HTFAction onCompleteAction, bool isIgnoreTimeScale = true)
         {
             var timer = Main.m_ReferencePool.Spawn<Timer>();
             timer.Fill(delayTime, interval, loop, isIgnoreTimeScale, onStartAction, onUpdateAction, onCompleteAction);
             Register(timer);
             return timer.ID;
         }
         #endregion
         
         #region 秒表
         /// <summary>
         /// 注册秒表
         /// </summary>
         /// <param name="delayTime">延迟时间</param>
         /// <param name="interval">间隔秒数</param>
         /// <param name="onCompleteAction">结束回调</param>
         /// <param name="isIgnoreTimeScale">是否忽略时间速率</param>
         /// <returns></returns>
         public static int RegisterStopwatch(float delayTime,float interval,  HTFAction<float> onCompleteAction, bool isIgnoreTimeScale = true)
         {
             return RegisterStopwatch(delayTime, interval, null, null, onCompleteAction, isIgnoreTimeScale);
         }
         /// <summary>
         /// 注册秒表
         /// </summary>
         /// <param name="delayTime">延迟时间</param>
         /// <param name="interval">间隔秒数</param>
         /// <param name="onStartAction">开始回调</param>
         /// <param name="onUpdateAction">运行中回调</param>
         /// <param name="onCompleteAction">结束回调</param>
         /// <param name="isIgnoreTimeScale">是否忽略时间速率</param>
         /// <returns></returns>
         public static int RegisterStopwatch(float delayTime, float interval, HTFAction onStartAction,
             HTFAction<float> onUpdateAction, HTFAction<float> onCompleteAction, bool isIgnoreTimeScale = true)
         {
             var timer = Main.m_ReferencePool.Spawn<Stopwatch>();
             timer.Fill(delayTime, interval, isIgnoreTimeScale, onStartAction, onUpdateAction, onCompleteAction);
             Register(timer);
             return timer.ID;
         }
         #endregion
         
         #region 闹钟
         public static int RegisterAlarmBySeconds(float seconds,HTFAction<TimeSpan,TimeSpan> onUpdateAction, HTFAction onCompleteAction, TimeUnit timeUnit = TimeUnit.Second)
         {
             var endTimeSpan = new TimeSpan(Now.AddSeconds(seconds).Ticks);
             return RegisterAlarm(NowTS,endTimeSpan,onUpdateAction,onCompleteAction, timeUnit);
         }
         public static int RegisterAlarmMinutes(float minutes, HTFAction<TimeSpan,TimeSpan> onUpdateAction,HTFAction onCompleteAction, TimeUnit timeUnit = TimeUnit.Second)
         {
             var endTimeSpan = new TimeSpan(Now.AddMinutes(minutes).Ticks);
             return RegisterAlarm(NowTS,endTimeSpan,onUpdateAction,onCompleteAction,timeUnit);
         }
         public static int RegisterAlarmByHours(float hours, HTFAction<TimeSpan,TimeSpan> onUpdateAction,HTFAction onCompleteAction, TimeUnit timeUnit = TimeUnit.Second)
         {
             var endTimeSpan = new TimeSpan(Now.AddHours(hours).Ticks);
             return RegisterAlarm(NowTS,endTimeSpan,onUpdateAction,onCompleteAction,timeUnit);
         }
         public static int RegisterByDays(float days, HTFAction<TimeSpan,TimeSpan> onUpdateAction,HTFAction onCompleteAction, TimeUnit timeUnit = TimeUnit.Second)
         {
             var endTimeSpan = new TimeSpan(Now.AddDays(days).Ticks);
             return RegisterAlarm(NowTS,endTimeSpan,onUpdateAction,onCompleteAction,timeUnit);
         }

         public static int RegisterAlarmByDateTime(DateTime endDateTime,  HTFAction<TimeSpan,TimeSpan> onUpdateAction,HTFAction onCompleteAction, TimeUnit timeUnit = TimeUnit.Second)
         {
             var endTimeSpan = new TimeSpan(endDateTime.Ticks);
             return RegisterAlarm(NowTS,endTimeSpan,onUpdateAction,onCompleteAction,timeUnit);
         }

         /// <summary>
         /// 注册闹钟
         /// </summary>
         /// <param name="startTimeSpan">闹钟开启时间</param>
         /// <param name="endTimeSpan">闹钟结束时间</param>
         /// <param name="onUpdateAction">运行中回调(剩余时间,持续时间)</param>
         /// <param name="onCompleteAction"><完成回调/param>
         /// <param name="timeUnit">剩余时间刷新频率(秒 分 小时 天进行回调)</param>
         /// <returns></returns>
         public static int RegisterAlarm(TimeSpan startTimeSpan, TimeSpan endTimeSpan,
             HTFAction<TimeSpan, TimeSpan> onUpdateAction, HTFAction onCompleteAction,
             TimeUnit timeUnit = TimeUnit.Second)
         {
             var timer = Main.m_ReferencePool.Spawn<Alarm>();
             timer.Fill(startTimeSpan, endTimeSpan, timeUnit, onUpdateAction, onCompleteAction);
             Register(timer);
             return timer.ID;
         }

         #endregion
         
    }
}


