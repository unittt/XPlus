using System;
using System.Collections.Generic;
using cfg.WarModule;
using Cysharp.Threading.Tasks;
using HT.Framework;
using Pb.Mmo.Common;
using UnityEngine;

namespace GameScripts.RunTime.War
{
    /// <summary>
    /// 战斗管理器
    /// </summary>
    public class WarManager
    {

        private Dictionary<int, Warrior> _warriors = new Dictionary<int, Warrior>();


        public Action OnEnter;
        public Action OnLeave;

        public static void InitWar()
        {
          
            InitWarAsync().Forget();
        }

        private  static async UniTaskVoid InitWarAsync()
        {
            //1.加载场景
            await Main.m_Entity.CreateEntity<WarRoot>("warRoot");
            //2.加载角色
            //1.己方
            //2.敌方
            
            
            //3.开始回合

             AddWarriors();
        }

        private static int war_id = 1;
        private static int war_pid = 1;
        private static void AddWarriors()
        {

            for (var i = 1; i < 10; i++)
            {
                NewWarrior(ECamp.A, i, "--------");
                NewWarrior(ECamp.B, i, "--------");
            }
        }


        private static void NewWarrior(ECamp camp, int index, string name)
        {
            var pflist = new List<PerformUnit>();
          
            var performUnit =  new PerformUnit();
            performUnit.pf_id = 1101;
            pflist.Add(performUnit);
            
            var hero = new GS2CWarAddWarrior();
            hero.camp_id = (int)camp;
            hero.war_id = war_id;
            hero.warrior = new PlayerWarrior();
            hero.warrior.baseWarrior = new BaseWarrior();
            hero.warrior.baseWarrior.wid = war_id;
            hero.warrior.pid = war_pid;
            hero.warrior.baseWarrior.pos = index;

            hero.warrior.baseWarrior.status = new WarriorStatus();
            hero.warrior.baseWarrior.status.auto_perform = pflist[0].pf_id;
            hero.warrior.baseWarrior.status.name = name;
            hero.warrior.baseWarrior.status.hp = 12000;
            hero.warrior.baseWarrior.status.max_hp = 12000;
            hero.warrior.baseWarrior.status.sp = 30;
            hero.warrior.baseWarrior.status.max_mp = 200;
            hero.warrior.baseWarrior.status.sp = 30;
            hero.warrior.baseWarrior.status.max_sp = 150;
            hero.warrior.baseWarrior.status.status = 1;
            hero.warrior.baseWarrior.status.model_info = new ModelInfo();
            hero.warrior.baseWarrior.status.model_info.figure = 1110;
            hero.warrior.baseWarrior.status.model_info.shape = 1110;
            hero.warrior.baseWarrior.status.model_info.weapon = 9;
            
            //1.根据阵营 和 阵营 id 获得坐标
            var position1 = TableGlobal.Instance.TbWarPosition.GetPosition((ECamp)hero.camp_id, index);
            //2.
            CreateWarrior((ECamp)hero.camp_id, position1, hero.warrior.baseWarrior.status.model_info).Forget();
        }
        
        
        public void AddWarrior(GS2CWarAddWarrior pbData)
        {
         
           
        }

        private static async UniTaskVoid CreateWarrior(ECamp camp,Vector3 position, ModelInfo info)
        {
            
            //设置玩家ID 
            //设置战士ID
            //阵营
            //战士名称
            //技能列表
            //技能冷却时间表
            //更改战士的外观
            //设置战士的状态 和 buff
            var warrior = await Main.m_Entity.CreateEntity<Warrior>();

            var warRoot =  Main.m_Entity.GetEntity<WarRoot>("warRoot");
            warrior.Entity.transform.SetParent(warRoot.Entity.transform, false);
            warrior.Entity.transform.localPosition = position;
            warrior.Entity.transform.localEulerAngles = WarTools.GetDefalutRotateAngle(camp);
            warrior.AssembleModel(info);
        }
    }
}