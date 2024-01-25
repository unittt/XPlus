using GameScripts.RunTime.Magic;
using HT.Framework;

namespace GameScripts.RunTime.Hud
{
    
    public abstract class HudEntityLogicBase: EntityLogicBase
    {

        public HudContainerLogic Container;
        public abstract BodyPart Part { get; }
        
        /// <summary>
        /// 当出现了一个相同的Hud
        /// </summary>
        public virtual void OnCreateNewHud()
        {
            
        }
    }
}