using System;
using System.Collections.Generic;
using Cysharp.Threading.Tasks;
using HT.Framework;
using Pb.Mmo.Common;

namespace GameScripts.RunTime.War
{
    /// <summary>
    /// 战斗管理器
    /// </summary>
    public class WarManager
    {

        private Dictionary<int, Warrior> _warriors = new Dictionary<int, Warrior>();


        public static int heroWid;

        public static event Action OnEnter;
        public static event Action OnLeave;

        
        /// <summary>
        /// 启动战斗
        /// </summary>
        public  static async UniTaskVoid Start()
        {
            //1.加载场景
            await Main.m_Entity.CreateEntity<WarRoot>("warRoot");
            //2.播放背景音乐
            
        }

        /// <summary>
        /// 加载角色
        /// </summary>
        public static async UniTaskVoid AddWarriors()
        {
            //1.加载玩家角色
            //2.加载敌方角色
            //3.玩家角色进入战斗
        }


        /// <summary>
        /// 玩家进入房间
        /// </summary>
        /// <param name="data"></param>
        private static async UniTaskVoid PlayerWarriorEnter(GS2CPlayerWarriorEnter data)
        {
            
        }


        /// <summary>
        /// 回合开始
        /// </summary>
        /// <param name="data"></param>
        public static void WarBoutStart(GS2CWarBoutStart data)
        {
            
        }

        /// <summary>
        /// 回合结束
        /// </summary>
        /// <param name="data"></param>
        public static void WarBoutEnd(GS2CWarBoutEnd data)
        {
            
        }


        /// <summary>
        /// 回合1
        /// </summary>
        public static void Bout1()
        {
            
            //主角
            //1.技能攻击(选择一个技能释放)
            //2.敌人受击
            //3.刷新角色状态
            //4.如果是近战攻击 归位
            
            
            //回合结束
            //回合开始
        }
        
        
        public static void Bout2()
        {
            
            //主角
            //1.技能攻击(选择一个技能释放)
            //2.敌人受击
            //3.刷新角色状态
            //4.如果是近战攻击 归位
            
            
            //回合结束
            //战斗结束
        }

        private  static async UniTaskVoid InitWarAsync()
        {
            //1.加载场景
            await Main.m_Entity.CreateEntity<WarRoot>("warRoot");
            //3.开始回合
        }

        

        // private static int war_id = 1;
        // private static int war_pid = 1;
        // private static async UniTask AddWarriors()
        // {
        //
        //     //己方
        //     for (var i = 1; i < 11; i++)
        //     {
        //         await NewWarrior(ECamp.A, i, 1110,"--------");
        //     }
        //     
        //     //敌方
        //     for (var i = 1; i < 11; i++)
        //     {
        //         await NewWarrior(ECamp.B, i, 5115,"--------");
        //     }
        // }
        //
        //
        // private static async UniTask NewWarrior(ECamp camp, int index,int figure, string name)
        // {
        //     var pflist = new List<PerformUnit>();
        //   
        //     var performUnit =  new PerformUnit();
        //     performUnit.pf_id = 1101;
        //     pflist.Add(performUnit);
        //     
        //     var hero = new GS2CWarAddWarrior();
        //     hero.camp_id = (int)camp;
        //     hero.war_id = war_id;
        //     hero.warrior = new PlayerWarrior();
        //     hero.warrior.baseWarrior = new BaseWarrior();
        //     hero.warrior.baseWarrior.wid = war_id;
        //     hero.warrior.pid = war_pid;
        //     hero.warrior.baseWarrior.pos = index;
        //
        //     hero.warrior.baseWarrior.status = new WarriorStatus();
        //     hero.warrior.baseWarrior.status.auto_perform = pflist[0].pf_id;
        //     hero.warrior.baseWarrior.status.name = name;
        //     hero.warrior.baseWarrior.status.hp = 12000;
        //     hero.warrior.baseWarrior.status.max_hp = 12000;
        //     hero.warrior.baseWarrior.status.sp = 30;
        //     hero.warrior.baseWarrior.status.max_mp = 200;
        //     hero.warrior.baseWarrior.status.sp = 30;
        //     hero.warrior.baseWarrior.status.max_sp = 150;
        //     hero.warrior.baseWarrior.status.status = 1;
        //     hero.warrior.baseWarrior.status.model_info = new ModelInfo();
        //     hero.warrior.baseWarrior.status.model_info.figure = figure;
        //     hero.warrior.baseWarrior.status.model_info.shape = figure;
        //     hero.warrior.baseWarrior.status.model_info.weapon = 9;
        //     
        //     //1.根据阵营 和 阵营 id 获得坐标
        //     var position1 = TableGlobal.Instance.TbWarPosition.GetPosition((ECamp)hero.camp_id, index);
        //     //2.
        //     await CreateWarrior((ECamp)hero.camp_id, position1, hero.warrior.baseWarrior.status.model_info);
        // }
        
        // private static async UniTask CreateWarrior(ECamp camp,Vector3 position, ModelInfo info)
        // {
        //     
        //     //设置玩家ID 
        //     //设置战士ID
        //     //阵营
        //     //战士名称
        //     //技能列表
        //     //技能冷却时间表
        //     //更改战士的外观
        //     //设置战士的状态 和 buff
        //     var warrior = await Main.m_Entity.CreateEntity<Warrior>();
        //
        //     var warRoot =  Main.m_Entity.GetEntity<WarRoot>("warRoot");
        //     warrior.Entity.transform.SetParent(warRoot.Entity.transform, false);
        //     warrior.Entity.transform.localPosition = position;
        //     warrior.Entity.transform.localEulerAngles = WarTools.GetDefalutRotateAngle(camp);
        //     warrior.AssembleModel(info);
        // }
    }
}