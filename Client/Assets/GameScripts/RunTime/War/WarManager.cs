using System;
using System.Collections.Generic;
using Cysharp.Threading.Tasks;
using GridMap;
using HT.Framework;
using Pb.Mmo.Common;
using UnityEngine;

namespace GameScripts.RunTime.War
{
    /// <summary>
    /// 战斗管理器
    /// </summary>
    public sealed class WarManager : SingletonBehaviourBase<WarManager>,IUpdateFrame
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
        /// 是否刚刚开始战斗
        /// </summary>
        private static bool m_IsWarStart;

        /// <summary>
        /// 当前回合数
        /// </summary>
        public int m_Bout { get; private set; }
        
        /// <summary>
        /// 当前协议属于哪一Bout(回合)
        /// </summary>
        public int m_ProtoBout { get; set; }




        private List<WarCmd> _cmdList = new();
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
        public async UniTaskVoid StartWar(GS2CShowWar gs2CShowWar)
        {
            // if (AttrManager.Current.pid == 0)
            // {
            //     Log.Error("pid为0，标识客户端已进入重登流程");
            //     return;
            // }
           
            //1.清理数据
            Clear();
            //2.重置数据
            ResetFields(gs2CShowWar);
            //3.关闭移动
            WarTouch.SetLock(false);
            
            //4.播放背景音乐
            _bout = 1;
            _actionFlag = 1;
            _isWarStart = true;
            _chatMsgCnt = 0;
            
            //加载地图数据
            var textAsset = await Main.m_Resource.LoadAsset<TextAsset>("mapdata_1010");
            var mapData = MapData.Deserialize(textAsset.text);
            MapManager.Current.SetMapData(mapData);
            MapManager.Current.ShowByPosition(new Vector2(gs2CShowWar.x, gs2CShowWar.y));
            
            _root.gameObject.SetActive(true);
            //触发战斗开始事件
            m_WaitTime = false;
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
        public void WarBoutStart(GS2CWarBoutStart data)
        {
            
            if (m_IsWarStart)
            {
                //战斗刚开始 刷新所有位置
                RefreshAllPos();
            }
            
            //记录回合
            m_Bout = data.bout_id;
            //遍历战士
            foreach (var warrior in _warriors.Values)
            {
                //触发战士的回合开始
                warrior.Bout();
            }
        }

        /// <summary>
        /// 刷新全部站位
        /// </summary>
        private void RefreshAllPos()
        {
            // --刷新全部站位
            // for wid, oWarrior in pairs(self.m_Warriors) do
            //     oWarrior:UpdateOriginPos()
            // end
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
            // if (warCmd is BoutEndCmd)
            // {
            //     for (var i = 0; i < _cmdList.Count; i++)
            //     {
            //         if (_cmdList[i] is not BoutStartCmd) continue;
            //         _cmdList[i + 1] = warCmd;
            //         return;
            //     }
            //     
            //     warCmd.TryExecute();
            //     return;
            // }
            
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

        
        private bool m_WaitTime = true;


        public void OnUpdateFrame()
        {
            UpdateCmds();
        }

        private void UpdateCmds()
        {
            if (m_WaitTime || _cmdList.Count == 0)return;

            var cmd = _cmdList[0];
            if (cmd.Status == WarCmdStatus.Idle)
            {
                cmd.TryExecute();
            }

            if (cmd.Status != WarCmdStatus.Completed) return;
            //移除
            _cmdList.Remove(cmd);
            Main.m_ReferencePool.Despawn(cmd);
        }

        /// <summary>
        /// 添加回合释放信息
        /// </summary>
        public void AddBoutMagicInfo()
        {
            
        }

        /// <summary>
        /// 回合开始
        /// </summary>
        /// <param name="boutID"></param>
        /// <param name="leftTime"></param>
        public void WarBoutStart(int boutID, int leftTime)
        {
            _bout = boutID;
        }

        public void BoutEnd()
        {
           //设置当前为回合结束
        }

        public  Warrior GetWarriorByPos(int campID, int warriorPos)
        {
          return null;
        }

        /// <summary>
        /// 根据阵营和坐标删除战士
        /// </summary>
        /// <param name="campID"></param>
        /// <param name="warriorPos"></param>
        public  void DelWarriorByCampAndPos(int campID, int warriorPos)
        {
            //1.阵营和坐标查找到战士
            //2.删除战士
        }

        /// <summary>
        /// 删除战士
        /// </summary>
        /// <param name="wid"></param>
        public void DelWarrior(int wid)
        {
            
        }

        public void AddWarrior(Warrior warrior)
        {
            
        }

        public Warrior GetWarrior(int wid)
        {
            return null;
        }
    }
}