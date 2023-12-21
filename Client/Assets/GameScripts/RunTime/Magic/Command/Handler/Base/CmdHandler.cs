using System.Collections.Generic;
using GameScripts.RunTime.War;
using HT.Framework;

namespace GameScripts.RunTime.Magic.Command.Handler
{
    public abstract class CmdHandler: IReference
    {
        public MagicUnit MagicUnit { get; private set; }

        public virtual void Fill(CommandData commandData)
        {
            
        }
        
        public virtual void Reset()
        {
           
        }

        public void GetExecutors(ExecutorType executorType, List<Warrior> warriors)
        {

            warriors.Clear();

            var isAlly = true;
            var isAtk = true;
            var isVic = true;
            var isAlive = false;
            
            switch (executorType)
            {
                case ExecutorType.Attacker:
                    warriors.Add(MagicUnit.GetAtkObj());
                    return;
                case ExecutorType.Victim:
                    warriors.Add(MagicUnit.GetVicObjFirst());
                    return;
                case ExecutorType.AllVictims:
                    MagicUnit.GetVicObjs(warriors);
                    return;
                case ExecutorType.Camera:
                    break;
                case ExecutorType.Allies:
                    break;
                case ExecutorType.AlliesExceptAttacker:
                    isAtk = false;
                    break;
                case ExecutorType.LivingAllies:
                    isAlive = true;
                    break;
                case ExecutorType.Enemies:
                    isAlly = false;
                    break;
                case ExecutorType.EnemiesExceptVictim:
                    isAlly = false;
                    isVic = false;
                    break;
            }

            MagicUnit.GetTargets(isAlly, isAtk, isVic, isAlive, warriors);
        }
    }
}