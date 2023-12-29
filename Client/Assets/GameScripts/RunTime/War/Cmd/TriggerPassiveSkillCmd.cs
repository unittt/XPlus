using System.Collections.Generic;
using HT.Framework;

namespace GameScripts.RunTime.War
{
    /// <summary>
    /// 被动技能
    /// </summary>
    public class TriggerPassiveSkillCmd : WarCmd
    {
        public int wid;

        public int pfid;
        public Dictionary<string,int> keyList;

        protected override void OnExecute()
        {
            
            $"CWarCmd.TriggerPassiveSkill 指令触发被动技能 {pfid.ToString()}".Info();
            if (!WarManager.Current.TryGetWarrior(wid,out var warrior))
            {
                return;
            }
            
            //触发被动技能
            warrior.TriggerPassiveSkill(pfid, keyList);

            if (keyList is null || keyList.Count == 0)
            {
                return;
            }
            
        }
    }
}