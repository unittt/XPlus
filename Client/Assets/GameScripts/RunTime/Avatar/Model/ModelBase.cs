using GridMap.RunTime.Walker;
using UnityEngine;

namespace GameScripts.RunTime.Avatar.Model
{
    public class ModelBase : MonoBehaviour
    {

        [SerializeField]
        private Animator _animator;

        public MapWalker _mapwalker;
        

        /// <summary>
        /// 设置动画速度
        /// </summary>
        /// <param name="speed"></param>
        public void SetAnimationSpeed(float speed)
        {
            
        }

        private void Start()
        {
            // Play("idleCity", 1);
            _mapwalker.OnStartMove += OnStartMove;
            _mapwalker.OnEndMove += OnEndMove;
        }

        private void OnStartMove()
        {
            // Play("run", 1);
        }
        
        private void OnEndMove()
        {
            // CrossFade("idleCity",0.3f,1);
        }
        
      
    }
}