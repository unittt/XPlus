using cfg.WarModule;
using Cysharp.Threading.Tasks;
using HT.Framework;
using Pb.Mmo.Common;

namespace GameScripts.RunTime.War
{
    
    /// <summary>
    /// 创建角色指令
    /// </summary>
    public class AddWarriorCmd: WarCmd
    {
        public BaseWarrior BaseWarrior;
        public int camp_id;
        public bool is_summon;


        protected override void OnExecute()
        {
           //如果是召唤兽 npc 或者 机器人召唤兽
           if (BaseWarrior is SumWarrior or NpcWarrior)
           {
               //删除对象
               WarManager.Current.DelWarriorByCampAndPos(camp_id, BaseWarrior.pos);
           }
           CreateEntity().Forget();
        }

        private async UniTaskVoid CreateEntity()
        {
            //加载战士
            var warrior = await Main.m_Entity.CreateEntity<Warrior>();
            //加入战士
            WarManager.Current.AddWarrior(BaseWarrior.wid, warrior);
            //填充战士数据
            warrior.Fill(BaseWarrior.status.model_info, (ECamp)camp_id, BaseWarrior.pos);
            //设置战士的坐标 和 欧拉角
            var camp = (ECamp)camp_id;

            // 等待模型装载完毕
            await warrior.IsBusyWait;

            //如果是玩家 或者召唤物
            if (BaseWarrior is PlayerWarrior or SumWarrior)
            {
                //更新自动施法
                warrior.UpdateAutoMagic();
            }

            //如果是玩家 并且拥有特殊技能
            if (warrior.m_ID == WarManager.Current.m_HeroWid) //&& warrior.GetSpecialSkillList() > 0)
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

            }

            SetCompleted();
        }
    }
}