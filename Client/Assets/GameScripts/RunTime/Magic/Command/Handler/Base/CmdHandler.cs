using System.Collections.Generic;
using GameScripts.RunTime.War;
using HT.Framework;

namespace GameScripts.RunTime.Magic.Command.Handler
{
    public abstract class CmdHandler: IReference
    {
        public virtual void Fill(CommandData commandData)
        {
            
        }
        
        public virtual void Reset()
        {
           
        }

        public void GetExecutors(ExecutorType executorType, List<Warrior> warriors)
        {
      
        }
    }
}