using System.Collections.Generic;
using GameScripts.RunTime.War;
using HT.Framework;

namespace GameScripts.RunTime.Magic.Command.Handler
{
    public abstract class CmdHandler: IReference
    {

        public MagicUnit Unit { get; private set; }

        public CommandData Data { get; private set; }

        public float StartTime => Data.StartTime;
    

        public virtual void Fill(CommandData commandData, MagicUnit unit)
        {
            Data = commandData;
            Unit = unit;
        }

        public virtual void Reset()
        {
           
        }
        
        public virtual void Execute()
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
                    warriors.Add(Unit.AtkObj);
                    return;
                case ExecutorType.Victim:
                    warriors.Add(Unit.GetVicObjFirst());
                    return;
                case ExecutorType.AllVictims:
                    warriors.AddRange(Unit.VicObjs);
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

            Unit.GetTargets(isAlly, isAtk, isVic, isAlive, warriors);
        }

      
    }
}