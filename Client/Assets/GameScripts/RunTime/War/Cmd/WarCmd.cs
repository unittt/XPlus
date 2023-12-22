using System;
using System.Collections.Generic;
using System.Threading;
using Cysharp.Threading.Tasks;
using HT.Framework;

namespace GameScripts.RunTime.War
{
    
    /*
     * CWarCmd 类似乎是游戏中用于管理和执行战斗命令的一个类。它包含了多个函数，用于处理不同类型的战斗操作和逻辑。下面是对这个类及其主要方法的概述：
        构造函数 (ctor): 初始化命令实例，包括命令ID、功能和相关状态。

        执行命令 (Excute): 执行指定的命令。如果命令是一个函数，则直接执行；如果是一个方法的名称，则调用相应的类方法。

        清除状态变更 (ClearVary): 清除记录在 m_VaryInfo 中的所有战士的状态变更。

        处理单个战士的状态变更 (ClearWarriorVary): 对单个战士执行状态变更，包括伤害、增益、buff等。

        锁定状态变更 (LockVary): 锁定指定的状态变更。

        设置和获取变更 (SetVary, GetVary, GetWarriorVary): 设置和获取特定战士的状态变更信息。

        等待条件满足 (WaitOne, WaitAll): 等待一个或所有指定对象满足某个条件。

        插入删除或活着状态 (InsertDelOrAlive): 根据战士的状态变更信息，处理战士的删除或存活状态。

        战斗结果 (WarResult): 处理战斗胜利或失败的结果。

        战斗结束 (End): 处理战斗结束的逻辑。

        回合开始和结束 (BoutStart, BoutEnd): 处理战斗回合的开始和结束。

        添加和删除战士 (AddWarrior, DelWarrior): 在战斗中添加或删除战士。

        处理魔法命令 (Magic): 执行魔法相关的命令和逻辑。

        处理伤害 (ExcuteDamage): 执行对战士的伤害计算和显示。

        触发被动技能 (TriggerPassiveSkill): 触发战士的被动技能。

        更新队伍指令 (RefreshAllTeamCmd): 更新所有战士的队伍指令。

        更新特殊技能冷却 (RefreshPerformCD): 更新特殊技能的冷却时间。

        使用道具 (WarUseItem): 处理在战斗中使用道具的逻辑。

        这个类提供了一个全面的框架来管理和执行战斗中的各种命令和事件，从基础的攻击和防御到复杂的魔法和技能效果。它是实现复杂战斗系统的关键组件。
     */
    public abstract class WarCmd : IReference
    {

        private static int RefID;
        /// <summary>
        /// 指令编号
        /// </summary>
        public int ID { get; private set; }

        public bool IsUsed { get; private set; }
        public Action CallBack;

        
        public WarCmdStatus Status { get; private set; }

        public Dictionary<int, Vary> m_VaryInfo;

        public void Fill(Action action)
        {
            ID =  Interlocked.Increment(ref RefID);
            CallBack = action;
        }


        public virtual void Execute()
        {
            var cancelToken = new CancellationToken();
            ExecuteInternal(cancelToken).Forget();
        }

        private async UniTaskVoid ExecuteInternal(CancellationToken token)
        {
            Status = WarCmdStatus.Wait;
            await UniTask.WaitUntil(IsCanExecute, cancellationToken: token);
            Status = WarCmdStatus.Running;
            OnExecute();
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
            Log.Info("1111");
            // return null;
        }

        
        /// <summary>
        /// 清除变化
        /// </summary>
        public void ClearVary()
        {
            foreach (var pair in m_VaryInfo)
            {
                ClearWarriorVary(pair.Key);
            }
        }

        /// <summary>
        /// 清除战士变化
        /// </summary>
        /// <param name="wid"></param>
        private void ClearWarriorVary(int wid)
        {
            // AssetDatabase.(value)
            // 逻辑实现，根据游戏的具体需求
            if (m_VaryInfo.TryGetValue(wid, out var dVary))
            {
                // if (WarManager.Current.TryGetWarrior(wid, out var oWarrior) && oWarrior.IsBusy("PassiveReborn"))
                // {
                //     CheckPassiveRebornVary(dVary, wid);
                //     return;
                // }
                
                
                var keys = new List<string> { "damage_list", "addMp_list", "buff_list" };
                // foreach (var key in keys)
                // {
                //     if (dVary.ContainsKey(key))
                //     {
                //         var list = dVary[key];
                //         if (list != null)
                //         {
                //             // foreach (var oCmd in list)
                //             // {
                //             //     oCmd.Execute();
                //             // }
                //         }
                //
                //         dVary[key] = null;
                //     }
                // }

                // if (oWarrior is not null)
                // {
                    // if (dVary.ContainsKey("hp_list") && dVary["hp_list"] != null)
                    // {
                    //     var lHP = dVary["hp_list"];
                    //     lHP[0] = lHP[lHP.Count - 1]; // 使用最后的血量更新
                    //     oWarrior.RefreshBlood(dVary);
                    //     WarManager.Current.WarriorStatusChange(wid);
                    // }
                    //
                    // if (dVary.ContainsKey("status"))
                    // {
                    //     bool bAlive = (dVary["status"] == WarStatus.Alive);
                    //     if (!bAlive)
                    //     {
                    //         CheckDelVary(dVary);
                    //     }
                    //
                    //     oWarrior.SetAlive(bAlive);
                    // }
                    //
                    // oWarrior.UpdateStatus(dVary);
                // }
                // else if (dVary.ContainsKey("add_warriorcmd"))
                // {
                //     var oAddCmd = dVary["add_warriorcmd"];
                //     if (oAddCmd != null)
                //     {
                //         // oAddCmd.Execute();
                //     }
                //
                //     dVary["add_warriorcmd"] = null;
                // }
                //
                // dVary["hp_list"] = null;
            }
        }

        public void CheckDelVary()
        {
            
        }
        

        private void CheckPassiveRebornVary(Dictionary<string, object> dVary, int wid)
        {
           
        }

        public void SetVary(int wid, string key, object v)
        {
            // if (!m_VaryInfo.TryGetValue(wid, out var d))
            // {
            //     //实例化一个对象出来
            //     d = new Dictionary<string, object>();
            // }
            //
            // d[key] = v;
            // m_VaryInfo[wid] = d;

        }

        public object GetVary(int wid, string key)
        {
            // if (m_VaryInfo.TryGetValue(wid, out var d))
            // {
            //     if (d.ContainsKey(key))
            //     {
            //         return d[key];
            //     }
            // }
            //
            return null;
        }

        public Vary GetWarriorVary(int wid)
        {
            if (m_VaryInfo.TryGetValue(wid, out var vary)) return vary;
            vary = new Vary();
            m_VaryInfo.Add(wid, vary);
            return vary;
        }
        

        public void Reset()
        {
            ID = 0;
            IsUsed = false;
            m_VaryInfo.Clear();
            Status = WarCmdStatus.Idle;
        }
        
        public void LocakVary(object vary, bool b)
        {
            
        }

        public void LockVary(Vary vary, bool p1)
        {
        
        }
    }

    public enum WarCmdStatus
    {
        /// <summary>
        /// 闲置
        /// </summary>
        Idle,
        /// <summary>
        /// 等待
        /// </summary>
        Wait,
        /// <summary>
        /// 运行中
        /// </summary>
        Running,
        /// <summary>
        /// 完成
        /// </summary>
        Completed
    }
}