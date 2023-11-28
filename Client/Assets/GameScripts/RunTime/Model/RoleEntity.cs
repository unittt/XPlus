using HT.Framework;

namespace GameScripts.RunTime.Model
{
    /// <summary>
    /// 角色实体
    /// </summary>
    [EntityResource("RoleEntity",true)]
    public class RoleEntity : EntityLogicBase
    {
        
        
        public override void OnInit()
        {
            base.OnInit();
            //1.创建角色
          
        }

        public void Fill(int actorID,int weaponID)
        {
            //1.填充演员
            //3.填充武器
            //4.填充翅膀
            //5.填充坐骑
        }

        public void ChangeShape()
        {
            // Main.m_Resource.LoadAsset<>()
        }
        
        /// <summary>
        /// 设置身体的贴图
        /// </summary>
        public void SetBodyTexture()
        {
            
        }

        /// <summary>
        /// 设置武器的贴图
        /// </summary>
        public void SetMountTexture()
        {
            
        }
        
        // function CActor.OnTextureDone(self, type, prefab, errcode)
        //     if prefab then
        // if type == "body" then
        //     self.m_MainModel:SetBodyMatTexture(prefab)
        // elseif type == "mount" then
        //     self.m_MainModel:SetMountMatTexture(prefab)
        // end
        //     elseif errcode then
        // printc("CActor.OnTextureDone: ", errcode)
        // end
        //     end

        /// <summary>
        /// 设置身体的shader
        /// </summary>
        public void SetBodyShader()
        {
            
        }

        /// <summary>
        /// 刷新角色模型的shader
        /// </summary>
        private void UpdateShaderInfo()
        {
            // self.m_MainModel:UpdateShaderInfo()	
        }

        /// <summary>
        /// 删除角色模型的材质球
        /// </summary>
        private void DelMaterial()
        {
            // self.m_MainModel:DelMaterial(sMatPath)
        }

        /// <summary>
        /// 设置武器颜色
        /// </summary>
        public void SetWeaponMatColor()
        {
            
        }
    }
}