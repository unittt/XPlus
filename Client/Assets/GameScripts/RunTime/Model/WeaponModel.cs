namespace  GameScripts.RunTime.Model
{
    /// <summary>
    /// 武器模型
    /// </summary>
    public class WeaponModel : ModelBase
    {
        /// <summary>
        /// 初始化SkinMeshRender
        /// </summary>
        // public override void InitSkinMeshRender()
        // {
        //     // local modelTrans = self.m_Transform:Find("weapon" .. tostring(self.m_ModelInfo.shape))
        //     // if modelTrans then
        //     // self.m_SkinnedMeshRenderer = modelTrans:GetComponent(classtype.Renderer)
        //     // self.m_DefaultMat =  self.m_SkinnedMeshRenderer.material
        //     // end
        //     //
        //     // if self.m_MatTrans then
        //     // local renderer = self.m_MatTrans:GetComponent(classtype.Renderer)
        //     // if renderer then
        //     // self.m_FumoMat = renderer.materials[renderer.materials.Length-1]
        //     // end
        //     //     end
        // }

        /// <summary>
        /// 设置武器符文等级
        /// </summary>
        public void SetWeaponEffectLevel()
        {
            
        }
        
        protected override string GetLocation()
        {
            return "";
        }
    }
}