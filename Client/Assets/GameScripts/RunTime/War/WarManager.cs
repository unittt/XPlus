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
        
        
        public static void InitWar()
        {
          
            InitWarAsync().Forget();
        }

        private static async UniTaskVoid InitWarAsync()
        {
            //1.加载场景
            await Main.m_Entity.CreateEntity<WarRoot>();
            //2.加载角色
            //1.己方
            //2.敌方
            
            
            
            
            
            //3.开始回合
            
            
        }

        private int war_id = 1;
        private int war_pid = 1;
        private void AddWarriors()
        {


            var pflist = new PerformUnit();
            pflist.pf_id = 1101;
            
            
            var hero = new PlayerWarrior();
            hero.wid = war_id;
            hero.pid = war_pid;
            hero.pos = 1;
            hero.appoint = 1;
            hero.pflist = pflist;
            hero.status = new WarriorStatus();
            hero.status.mask = "7fff";
            hero.status.auto_perform = pflist[1];
            hero.status.name = "测试A";
            hero.status.hp = 12000;
            hero.status.max_hp = 12000;
            hero.status.sp = 30;
            hero.status.max_mp = 200;
            hero.status.sp = 30;
            hero.status.max_sp = 150;
            hero.status.status = 1;
            hero.status.model_info = new ModelInfo();
            hero.status.model_info.figure = 1110;
            hero.status.model_info.shape = 1110;
            hero.status.model_info.weapon = 21006;
        }
    }
}