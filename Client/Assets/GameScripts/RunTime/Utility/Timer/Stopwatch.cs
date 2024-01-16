using System.Threading;
using HT.Framework;
using UnityEngine;

namespace GameScripts.RunTime.Utility.Timer
{
    /// <summary>
    /// 秒表
    /// </summary>
    internal sealed class Stopwatch : ITimer
    {
        private bool _isIgnoreTimeScale;    //是否忽略时间速率
        private bool _isDelay;  //是否延迟
        private float _delayTime;    //延迟时间
        private float _targetTime;  //下次执行的目标时间
        private float _interval;    //每次间隔秒数
        private float _pauseRemainingTime;  //暂停时 当前时间与目标时间之间的差值
        
        private HTFAction _onStartAction;  //开始运行委托
        private HTFAction<float> _onUpdateAction;    //运行中的委托
        private HTFAction<float> _onCompleteAction;   //结束运行委托

        private float _launchTime;  //启动时间
        private float _pauseTime;   //暂停时间
        private float _pauseWaitTime;   //暂停等待时长
        private float NowTime => _isIgnoreTimeScale ?  Time.realtimeSinceStartup: Time.time;
        
        public int ID { get; private set; }
        /// <summary>
        /// 定时器状态
        /// </summary>
        public TimerState State { get; private set; }
        
        /// <summary>
        /// 初始化秒表
        /// </summary>
        /// <param name="delayTime">延迟时间</param>
        /// <param name="interval">间隔秒数</param>
        /// <param name="isIgnoreTimeScale">是否忽略时间速率</param>
        /// <param name="onStartAction">开始回调</param>
        /// <param name="onUpdateAction">运行中回调</param>
        /// <param name="onCompleteAction">结束回调</param>
        public void Fill(float delayTime, float interval,bool isIgnoreTimeScale,HTFAction onStartAction, HTFAction<float> onUpdateAction,HTFAction<float> onCompleteAction)
        {
            ID = Interlocked.Increment(ref TimerManager.CurUnitIdx);
            _interval = interval;
            _onStartAction = onStartAction;
            _onUpdateAction = onUpdateAction;
            _onCompleteAction = onCompleteAction;

            _delayTime = delayTime;
            _isDelay = delayTime > 0;
            _pauseWaitTime = 0;
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
                
                if (!_isDelay)
                {
                    _pauseTime = NowTime;
                }
            }
        }

        public void Resume()
        {
            if (State == TimerState.Pause)
            {
                State = TimerState.Running;
                _targetTime = NowTime + _pauseRemainingTime;

                //暂停等待时间 = 当前时间 - 暂停时间
                if (!_isDelay) _pauseWaitTime += NowTime - _pauseTime;
            }
        }

        public void Stop()
        {
            if (State != TimerState.Done)
            {
                State = TimerState.Done;
                _onCompleteAction?.Invoke(NowTime - _launchTime);
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
                _launchTime = NowTime;
                _onStartAction?.Invoke();
            }
            
            //间隔m_Interval时间循环执行
            if (_targetTime > NowTime) return;
            
            _targetTime = NowTime + _interval;
            _onUpdateAction?.Invoke(NowTime - _launchTime - _pauseWaitTime);
        }

        public void Reset()
        {
            
        }
    }
}