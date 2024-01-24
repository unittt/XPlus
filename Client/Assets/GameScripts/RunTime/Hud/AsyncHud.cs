using GameScripts.RunTime.Magic;
using HT.Framework;

namespace GameScripts.RunTime.Hud
{
    public abstract class AsyncHud: EntityLogicBase
    {
        
        public abstract BodyPart Part { get; }

        public virtual void Fill(params object[] args)
        {
            //节点
            //
        }

        /// <summary>
        /// 当出现了一个相同的Hud
        /// </summary>
        public virtual void OnCreateNewHud()
        {
            
        }
    }
}