using System;
using System.Threading;
using HT.Framework;

namespace GameScripts.RunTime.Utility.Timer
{
    /// <summary>
    /// 闹钟
    /// </summary>
    internal sealed class Alarm : ITimer
    {
        private const float Threshold = 0.3f;
        private TimeSpan _startTimeSpan; //启动时间
        private TimeSpan _endTimeSpan; //结束时间
        private HTFAction<TimeSpan,TimeSpan> _onUpdateAction;
        private HTFAction _onCompleteAction;
        
        private TimeUnit _unit; //刷新单位
        private int _preTimeValue; 
        
        /// <summary>
        /// 定时器编号
        /// </summary>
        public int ID { get; private set; }
        public TimerState State { get; private set; }
        
        /// <summary>
        /// 初始化闹钟
        /// </summary>
        /// <param name="startTimeSpan">起始时间</param>
        /// <param name="endTimeSpan">结束时间</param>
        /// <param name="timeUnit">刷新的时间单位</param>
        /// <param name="onUpdateAction">运行中回调</param>
        /// <param name="onCompleteAction">完成回调</param>
        public void Fill(TimeSpan startTimeSpan, TimeSpan endTimeSpan,TimeUnit timeUnit, HTFAction<TimeSpan,TimeSpan> onUpdateAction,HTFAction onCompleteAction)
        {
            ID = Interlocked.Increment(ref TimerManager.CurUnitIdx);;
            
            _startTimeSpan = startTimeSpan;
            _endTimeSpan = endTimeSpan;
            _unit = timeUnit;
            _onUpdateAction = onUpdateAction;
            _onCompleteAction = onCompleteAction;
        }
        
        public void Launch()
        {
            State = TimerState.Running;
        }

        public void Pause()
        {
            if (State == TimerState.Running)
            {
                State = TimerState.Pause;
            }
        }

        public void Resume()
        { 
            if (State == TimerState.Pause)
            {
                State = TimerState.Running;
            }
        }

        public void Stop()
        {
            if (State != TimerState.Done)
            {
                State = TimerState.Done;
            }
        }

        public void OnUpdate()
        {
            var now = TimerManager.NowTS;
            
            //剩余时间
            var remainingTime = (_endTimeSpan - now);
            if (remainingTime.TotalSeconds < Threshold)
            {
                remainingTime = TimerManager.Zero;
            }
            
            var nowValue = _unit switch
            {
                TimeUnit.Day => remainingTime.Days,
                TimeUnit.Hour => remainingTime.Hours,
                TimeUnit.Minute => remainingTime.Minutes,
                _ => remainingTime.Seconds
            };

            if (_preTimeValue != nowValue)
            {
                _preTimeValue = nowValue;
                //持续时间
                var durationTime = now - _startTimeSpan;
                _onUpdateAction?.Invoke(remainingTime, durationTime);
            }

            if (now <= _endTimeSpan) return;
            Stop();
            _onCompleteAction.Invoke();
        }

        public void Reset()
        {
            
        }
    }
}