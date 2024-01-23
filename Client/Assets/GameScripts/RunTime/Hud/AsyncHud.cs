

using HT.Framework;
using UnityEngine;

namespace GameScripts.RunTime.Hud
{
    public abstract class AsyncHud: EntityLogicBase
    {
        public Transform Container;
        
        public virtual void Fill(params object[] args)
        {
            
        }
    }
}