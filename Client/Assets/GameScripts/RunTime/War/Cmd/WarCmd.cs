using System;
using System.Collections.Generic;
using System.Threading;
using GameScript.RunTime.Network;
using HT.Framework;
using UnityEditor;

namespace GameScripts.RunTime.War
{
    public abstract class WarCmd : IReference
    {
        
        public int CmdIndex { get; private set; }

        public bool IsUsed { get; private set; }
        public Action CallBack;

        private Dictionary<int, Dictionary<string, object>> m_VaryInfo = new();

        public void Fill(Action action)
        {
            CmdIndex =  Interlocked.Increment(ref WarManager.Current.CmdIndex);
            CallBack = action;
        }


        public void Execute()
        {
            if (IsUsed)return;
            IsUsed = true;
            CallBack?.Invoke();
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
                if (WarManager.Current.TryGetWarrior(wid, out var oWarrior) && oWarrior.IsBusy("PassiveReborn"))
                {
                    CheckPassiveRebornVary(dVary, wid);
                    return;
                }

                var keys = new List<string> { "damage_list", "addMp_list", "buff_list" };
                foreach (var key in keys)
                {
                    if (dVary.ContainsKey(key))
                    {
                        var list = dVary[key];
                        if (list != null)
                        {
                            // foreach (var oCmd in list)
                            // {
                            //     oCmd.Execute();
                            // }
                        }

                        dVary[key] = null;
                    }
                }

                if (oWarrior is not null)
                {
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
                }
                else if (dVary.ContainsKey("add_warriorcmd"))
                {
                    var oAddCmd = dVary["add_warriorcmd"];
                    if (oAddCmd != null)
                    {
                        // oAddCmd.Execute();
                    }

                    dVary["add_warriorcmd"] = null;
                }

                dVary["hp_list"] = null;
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
            if (!m_VaryInfo.TryGetValue(wid, out var d))
            {
                //实例化一个对象出来
                d = new Dictionary<string, object>();
            }

            d[key] = v;
            m_VaryInfo[wid] = d;

        }

        public object GetVary(int wid, string key)
        {
            if (m_VaryInfo.TryGetValue(wid, out var d))
            {
                if (d.ContainsKey(key))
                {
                    return d[key];
                }
            }

            return null;
        }

        public bool TryGetWarriorVary(int wid, out Dictionary<string, object> d)
        {
            return m_VaryInfo.TryGetValue(wid, out d);
        }
        

        public void Reset()
        {
            CmdIndex = 0;
            IsUsed = false;
            m_VaryInfo.Clear();
        }

        public Vary GetWarriorVary(int warriorMID)
        {
            return null;
        }

        public void LocakVary(object vary, bool b)
        {
            
        }

        public void LockVary(Vary vary, bool p1)
        {
        
        }
    }
}