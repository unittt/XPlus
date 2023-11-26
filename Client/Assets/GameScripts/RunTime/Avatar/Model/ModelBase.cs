using GameScripts.RunTime.Avatar.Tools;
using GridMap.RunTime.Walker;
using UnityEngine;

namespace GameScripts.RunTime.Avatar.Model
{
    public class ModelBase : MonoBehaviour
    {

        [SerializeField]
        private Animator _animator;

        public MapWalker _mapwalker;
        private float _normalizedTime;
        private float _fixedTime;
        private float _iDuration;

        /// <summary>
        /// 设置动画速度
        /// </summary>
        /// <param name="speed"></param>
        public void SetAnimationSpeed(float speed)
        {
            
        }

        private void Start()
        {
            Play("idleCity", 1);
            _mapwalker.OnStartMove += OnStartMove;
            _mapwalker.OnEndMove += OnEndMove;
        }

        private void OnStartMove()
        {
            Play("run", 1);
        }
        
        private void OnEndMove()
        {
            CrossFade("idleCity",0.3f,1);
        }
        
        /// <summary>
        /// 播放动画
        /// </summary>
        /// <param name="sState"></param>
        /// <param name="normalizedTime"></param>
        public void Play(string sState, float normalizedTime)
        {
            _normalizedTime = normalizedTime;

            var iHash = ModelTools.StateToHash(sState);
            _animator?.Play(iHash,0);
        }

        public void PlayInFixedTime(string sState, float fixedTime)
        {
            _fixedTime = fixedTime;
            var iHash = ModelTools.StateToHash(sState);
            _animator?.PlayInFixedTime(iHash,0,fixedTime);
        }

        /// <summary>
        /// 渐变某状态
        /// </summary>
        /// <param name="sState"></param>
        /// <param name="iDuration"></param>
        /// <param name="normalizedTime"></param>
        public void CrossFade(string sState,float iDuration, float normalizedTime)
        {
            _iDuration = iDuration;
            _normalizedTime = normalizedTime;
            var iHash = ModelTools.StateToHash(sState);
            _animator.CrossFade(iHash, iDuration, 0, normalizedTime);
        }
        
        public void CrossFadeInFixedTime(string sState,float iDuration, float fixedTime)
        {
            _iDuration = iDuration;
            _fixedTime = fixedTime;
            var iHash = ModelTools.StateToHash(sState);
            _animator.CrossFadeInFixedTime(iHash, iDuration, 0, _fixedTime);
        }
    }
}