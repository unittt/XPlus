using System.Collections.Generic;

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
            if ( WarManager.Current.TryGetWarrior(wid,out var warrior))
            {
                warrior.TriggerPassiveSkill(pfid);
                //执行技能
                
                
                
            }
        }
    }
}