using System.Collections.Generic;
using GameScripts.RunTime.Utility;
using GameScripts.RunTime.War;
using HT.Framework;

namespace GameScripts.RunTime.Magic.Command.Handler
{
    public abstract class CmdHandler: IReference
    {

        public MagicUnit Unit { get; private set; }

        public CommandData Data { get; private set; }

        public float StartTime => Data.StartTime;

        /// <summary>
        /// 状态
        /// </summary>
        public CmdStatus Status { get; private set; }


        public virtual void Fill(CommandData commandData, MagicUnit unit)
        {
            Data = commandData;
            Unit = unit;
        }

        /// <summary>
        /// 尝试执行
        /// </summary>
        /// <returns></returns>
        public bool TryExecute()
        {
            if (StartTime > Unit.ElapsedTime || Status != CmdStatus.Idle || !IsCanExecute()) return false;
            Status = CmdStatus.Running;
            Log.Info($"执行施法指令：{GetType()}");
            OnExecute();
            return true;
        }
        
        /// <summary>
        /// 是否能够执行
        /// </summary>
        /// <returns></returns>
        protected virtual bool IsCanExecute()
        {
            return true;
        }
        
        /// <summary>
        /// 当执行
        /// </summary>
        protected virtual void OnExecute()
        {
           
        }
        
        /// <summary>
        /// 设置为完成
        /// </summary>
        public void SetCompleted()
        {
            if (Status == CmdStatus.Completed) return;
            Status = CmdStatus.Completed;
            OnCompleted();
        }


        /// <summary>
        /// 当完成时
        /// </summary>
        protected virtual void OnCompleted()
        {
            
        }
        
        public virtual void Reset()
        {
            Status = CmdStatus.Idle;
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