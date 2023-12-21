using Pb.Mmo.Common;

namespace GameScripts.RunTime.War
{
    public class AddWarriorCmd: WarCmd
    {
        public BaseWarrior Warrior;
        public int camp_id;
        public bool is_summon;


        protected override void OnExecute()
        {
           //如果是召唤兽 npc 或者 机器人召唤兽
           if (Warrior is SumWarrior or NpcWarrior)
           {
               WarManager.Current.DelWarriorByCampAndPos(camp_id, Warrior.pos);
           }

           var warrior = WarTools.CreateWarrior(camp_id, Warrior);
           WarManager.Current.AddWarrior(warrior);

           if (Warrior is PlayerWarrior or SumWarrior)
           {
               // warrior.UpdateAutoMagic();
           }

           //如果是玩家 并且拥有特殊技能
           if (warrior.m_ID ==   WarManager.Current.m_HeroWid) //&& warrior.GetSpecialSkillList() > 0)
           {
               //刷新特殊技能
           }

           //召唤宠物需要特效
           if (is_summon)
           {
               // warrior.ShowSummonEffect();
           }
           else
           {
               // warrior.ShowWarAnim();
           }
           
        }
    }
}