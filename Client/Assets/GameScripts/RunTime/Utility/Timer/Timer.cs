using System;
using System.Threading;
using HT.Framework;
using UnityEngine;

namespace GameScripts.RunTime.Utility.Timer
{
    /// <summary>
    /// 定时器
    /// </summary>
    internal sealed class Timer : ITimer
    {
        private bool _isIgnoreTimeScale;    //是否忽略时间速率
        private bool _isDelay;  //是否延迟
        private float _delayTime;    //延迟时间
        private float _targetTime;  //下次执行的目标时间
        private int _currLoop;  // 当前运行了多少次
        private float _interval;    //每次间隔秒数
        private int _loop;  //循环次数(-1表示无限循环，0表示循环一次)
        private float _pauseRemainingTime;  //暂停时 当前时间与目标时间之间的差值
        
        private HTFAction _onStartAction;  //开始运行委托
        private HTFAction<int> _onUpdateAction;    //运行中的委托
        private HTFAction _onCompleteAction;   //结束运行委托
        
        private float NowTime => _isIgnoreTimeScale ?  Time.realtimeSinceStartup: Time.time;
        
        /// <summary>
        /// 定时器编号
        /// </summary>
        public int ID { get; private set; }
        /// <summary>
        /// 定时器状态
        /// </summary>
        public TimerState State { get; private set; }
        
        /// <summary>
        /// 初始化定时器
        /// </summary>
        /// <param name="delayTime">延迟时间</param>
        /// <param name="interval">间隔秒数</param>
        /// <param name="loop">循环次数</param>
        /// <param name="isIgnoreTimeScale">是否忽略时间速率</param>
        /// <param name="onStartAction">开始回调</param>
        /// <param name="onUpdateAction">运行中回调</param>
        /// <param name="onCompleteAction">结束回调</param>
        /// <returns></returns>
        internal void Fill(float delayTime, float interval, int loop,bool isIgnoreTimeScale, HTFAction onStartAction, HTFAction<int> onUpdateAction, HTFAction onCompleteAction)
        {
            ID = Interlocked.Increment(ref TimerManager.CurUnitIdx);
            _interval = interval;
            _loop = loop;
            _onStartAction = onStartAction;
            _onUpdateAction = onUpdateAction;
            _onCompleteAction = onCompleteAction;
            _currLoop = 0;

            _delayTime = delayTime;
            _isDelay = delayTime > 0;
            
            _isIgnoreTimeScale = isIgnoreTimeScale;
        }
        
        public void Launch()
        {
            _targetTime = NowTime + _delayTime;
            State = TimerState.Running;
        }

        public void Pause()
        {
            if (State == TimerState.Running)
            {
                State = TimerState.Pause;
                _pauseRemainingTime = _targetTime - NowTime;
            }
        }

        public void Resume()
        {
            if (State == TimerState.Pause)
            {
                State = TimerState.Running;
                _targetTime = NowTime + _pauseRemainingTime;
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
            //先处理延迟，过了延迟时间，第一次开始执行
            if (_isDelay)
            {
                if (_targetTime > NowTime) return;

                _isDelay = false;
                _targetTime = NowTime;
                _onStartAction?.Invoke();
            }
            
            //间隔m_Interval时间循环执行
            if (_targetTime > NowTime) return;
            
            _targetTime = NowTime + _interval;
            _onUpdateAction?.Invoke(_loop - _currLoop);
            
            if (_loop <= -1) return;
            _currLoop++;
            
            //定时器完成
            if (_currLoop < _loop) return;
            
            Stop();
            _onCompleteAction?.Invoke();
        }

        public void Reset()
        {
            
        }
    }
}