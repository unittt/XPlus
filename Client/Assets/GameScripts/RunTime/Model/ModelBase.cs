using UnityEngine;

namespace GameScripts.RunTime.Model
{
    /// <summary>
    /// 模型基类，负责模型的动画，材质，颜色
    /// </summary>
    public class ModelBase
    {
        private Animator _animator;
        private float _normalizedTime;
        private float _fixedTime;
        private float _iDuration;
        
        public void SetAnimatorCullModel(AnimatorCullingMode animatorCullingMode)
        {
            _animator.cullingMode = animatorCullingMode;
        }

        /// <summary>
        ///  加载新的动作控制器(场景：1 战斗:2 创角 3 4: 结婚)
        /// </summary>
        public void ReloadAnimator(RuntimeAnimatorController controller)
        {
            _animator.runtimeAnimatorController = controller;
        }

        /// <summary>
        /// 设置材质效果
        /// </summary>
        public void SetMatEffect()
        {
            
        }

        /// <summary>
        /// 设置法术效果
        /// </summary>
        public void SetMagicEffect()
        {
            
        }

        /// <summary>
        /// 初始化meshrender组件
        /// </summary>
        public virtual void InitSkinMeshRender()
        {
            
        }

        /// <summary>
        /// 显示或者隐藏模型
        /// </summary>
        /// <param name="active"></param>
        public void SetActive(bool active)
        {
            
        }

        /// <summary>
        /// 设置染色颜色
        /// </summary>
        public void SetRanseColor()
        {
            // if self.m_SkinnedMeshRenderer and ranseInfo then
            //
            // if ranseInfo[define.Ranse.PartType.hair] then 		
            // self.m_SkinnedMeshRenderer.material:SetColor("_rColor",  ranseInfo[define.Ranse.PartType.hair])
            // end 
            //
            // if ranseInfo[define.Ranse.PartType.clothes] then
            // self.m_SkinnedMeshRenderer.material:SetColor("_gColor",  ranseInfo[define.Ranse.PartType.clothes])
            // end 
            //
            // if ranseInfo[define.Ranse.PartType.other] then 
            // self.m_SkinnedMeshRenderer.material:SetColor("_bColor",  ranseInfo[define.Ranse.PartType.other])
            // end 
            //
            // if ranseInfo[define.Ranse.PartType.pant] then 
            // self.m_SkinnedMeshRenderer.material:SetColor("_aColor",  ranseInfo[define.Ranse.PartType.pant])
            // end
            //
            //     end 
        }

        
        /// <summary>
        /// 设置染色材质
        /// </summary>
        /// <param name="material"></param>
        public void SetRanseMat(Material material)
        {
            // self.m_SkinnedMeshRenderer.material = self.m_RanseMat
        }

        /// <summary>
        /// 恢复默认材质
        /// </summary>
        public void RecoverMat()
        {
            
        }

        /// <summary>
        /// 设置模型的颜色
        /// </summary>
        public void SetModelColor(Color color)
        {
            // printc("-----------SetModelColor---透明度")
            // if self.m_SkinnedMeshRenderer then
            // self.m_SkinnedMeshRenderer.material:SetColor("_ColorAlpha", color)
            // end 
            //
            // if self.m_ExModelRender then 
            // self.m_ExModelRender.material:SetColor("_ColorAlpha", color)
            // end 
        }

        /// <summary>
        /// 恢复模型的颜色
        /// </summary>
        public void RecoverColor()
        {
            // if self.m_SkinnedMeshRenderer then
            // self.m_SkinnedMeshRenderer.material:SetColor("_ColorAlpha", Color.white)
            // end
            //
            // if self.m_ExModelRender then 
            // self.m_ExModelRender.material:SetColor("_ColorAlpha", Color.white)
            // end 
        }

        /// <summary>
        /// 设置恢复模型透明度
        /// </summary>
        public void RecoverModelAlpha()
        {
            // if self.m_SkinnedMeshRenderer then
            // self.m_SkinnedMeshRenderer.material:SetColor("_Alpha", Color.white)
            // end
            //
            // if self.m_ExModelRender then 
            // self.m_ExModelRender.material:SetColor("_Alpha", Color.white)
            // end
        }


        /// <summary>
        /// 设置模型的大小
        /// </summary>
        public void SetSize(float size)
        {
            
        }

        public void SetAnimationSpeed(float speed)
        {
            
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