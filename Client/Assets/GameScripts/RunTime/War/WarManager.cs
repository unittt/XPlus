using System;
using System.Collections.Generic;
using Cysharp.Threading.Tasks;
using GameScripts.RunTime.Attr;
using HT.Framework;
using Pb.Mmo.Common;
using UnityEngine;
using UnityEngine.Serialization;

namespace GameScripts.RunTime.War
{
    /// <summary>
    /// 战斗管理器
    /// </summary>
    public sealed class WarManager : SingletonBehaviourBase<WarManager>
    {


        [SerializeField]
        private Transform _root;
        
        private Dictionary<int, Warrior> _warriors = new Dictionary<int, Warrior>();


        public static int heroWid;

        public static event Action OnEnter;
        public static event Action OnLeave;


        #region 参数

        public int WarID { get; private set; }
        private string _warType;
        private int _warSysType;
        private int _isInResult;
        private int _warSky;
        private int _weather;
        private bool _is_bosswar;
        private int _barrage_show;
        private int _barrage_send;
        private int _sys_type;
        
        
        private int _bout;
        private int _actionFlag;
        private bool _isWarStart;
        private int _chatMsgCnt;
        #endregion


        /// <summary>
        /// 当前协议属于哪一Bout(回合)
        /// </summary>
        public int m_ProtoBout { get; set; }
    
        

        private List<WarCmd> _cmdList;
        /// <summary>
        /// 盟友阵营
        /// </summary>
        public int m_AllyCamp;

        public int m_HeroWid;
        /// <summary>
        /// 盟友人数
        /// </summary>
        public int m_AllyPlayerCnt;
        /// <summary>
        /// 敌人人数
        /// </summary>
        public int m_EnemyPlayerCnt;


        /// <summary>
        /// 启动战斗
        /// </summary>
        /// <param name="gs2CShowWar"></param>
        public void Start(GS2CShowWar gs2CShowWar)
        {
            if (AttrManager.Current.pid == 0)
            {
                Log.Error("pid为0，标识客户端已进入重登流程");
                return;
            }
            
            //清理数据
            Clear();
            //1.关闭移动
            WarTouch.SetLock(false);
            
            //2.播放背景音乐
            _bout = 1;
            _actionFlag = 1;
            _isWarStart = true;
            _chatMsgCnt = 0;
            
            
            _root.gameObject.SetActive(true);
            //触发战斗开始事件
        }


        private void ResetFields(GS2CShowWar gs2CShowWar)
        {
            WarID = gs2CShowWar.war_id;
            _warType = gs2CShowWar.war_type;
            _warSky = gs2CShowWar.sky_war;
            _weather = gs2CShowWar.weather;
            _is_bosswar = gs2CShowWar.is_bosswar;
            _barrage_show = gs2CShowWar.barrage_show;
            _barrage_send = gs2CShowWar.barrage_send;
            _sys_type = gs2CShowWar.sys_type;
        }

        
        /// <summary>
        /// 清理
        /// </summary>
        public void Clear()
        {
            //1.隐藏节点
            _root.gameObject.SetActive(false);
            //2.删除战士
            foreach (var pair in _warriors)
            {
                Main.m_Entity.DestroyEntity(pair.Value);
            }
            _warriors.Clear();
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


        #region MyRegion

        public bool TryGetWarrior(int wid,out Warrior warrior)
        {
            warrior = null;
            return true;
        }

        #endregion
        
        
        

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
        public void InsertCmd(WarCmd warCmd)
        {
            //如果是结束
            if (warCmd is BoutEndCmd)
            {
                for (var i = 0; i < _cmdList.Count; i++)
                {
                    if (_cmdList[i] is not BoutStartCmd) continue;
                    _cmdList[i + 1] = warCmd;
                    return;
                }
                
                warCmd.Execute();
                return;
            }
            
            _cmdList.Add(warCmd);
        }

        /// <summary>
        /// 获得玩家的play id
        /// </summary>
        /// <returns></returns>
        public int GetHeroPid()
        {
            return 0;
        }

        /// <summary>
        /// 设置战斗召唤
        /// </summary>
        /// <param name="warEnterSumList"></param>
        public void SetFightSummons(List<int> warEnterSumList)
        {
          
        }
    }
}