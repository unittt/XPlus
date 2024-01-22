using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using GameScripts.RunTime.Magic.Command;
using GameScripts.RunTime.Magic.Command.Handler;
using GameScripts.RunTime.Utility;
using GameScripts.RunTime.War;
using HT.Framework;
using UnityEngine;

namespace GameScripts.RunTime.Magic
{
    /// <summary>
    /// 控制魔法效果的播放、管理、更新和结束
    /// </summary>
    public class MagicUnit: IReference
    {
        
        public int ID { get; private set; }
        public int Shape { get; private set; }
        public int MagicID { get; private set; }
        public int MagicIdx { get; private set; }
        /// <summary>
        /// 是否正在追击状态
        /// </summary>
        public bool IsPursued { get; private set; }

        private Dictionary<string, object> data = new(); // 用于存储攻击对象、受害对象等数据
        private List<CmdHandler> _handlerList = new(); // 执行播放技能指令列表

        /// <summary>
        /// 已执行时间
        /// </summary>
        public float ElapsedTime { get; private set; }

        private bool _isRunning;
        private bool _isActive;
        private bool m_Running;
        private int? m_LastHitInfoIndex;


        private Action _completedCallBack;


        /// <summary>
        /// 获得攻击对象
        /// </summary>
        public Warrior AtkObj { get; private set; }
        /// <summary>
        /// 受击的对象
        /// </summary>
        public List<Warrior> VicObjs { get; }

        /// <summary>
        /// 施法指令全部完成完成
        /// </summary>
        public bool IsFinish
        {
            get
            {
                return _handlerList.All(cmdHandler => cmdHandler.Status == CmdStatus.Completed);
            }
        }

        public MagicUnit()
        {
            VicObjs = new List<Warrior>();
        }

        public void Fill(int magicID, int shape, int magicIndex,bool isPursued, Warrior atkObj, IEnumerable<Warrior> refVicObjs, List<CommandData> dataList, Action completedCallBack)
        {
            //设置法术单元编号
            ID = Interlocked.Increment(ref MagicManager.CurUnitIdx);
            MagicID = magicID;
            Shape = shape;
            MagicIdx = magicIndex;
            IsPursued = isPursued;
            AtkObj = atkObj;
            VicObjs.AddRange(refVicObjs);
            _completedCallBack = completedCallBack;
            foreach (var cmdData in dataList)
            {
                var handlerType = MagicManager.Current.GetHandlerType(cmdData);
                var cmdHandler = Main.m_ReferencePool.Spawn(handlerType).Cast<CmdHandler>();
                cmdHandler.Fill(cmdData, this);
                _handlerList.Add(cmdHandler);
            }
        }
        

        /// <summary>
        /// 法术开始
        /// </summary>
        public void Start()
        {
            Log.Info(string.Format("CMagicUnit.Start 法术开始 | magic:{0} | shape:{1} | index:{2} | mID:{3}", MagicID.ToString(), Shape.ToString(),
                MagicIdx.ToString(), ID.ToString()));

            m_Running = true;
            
            AtkObj?.SetPlayMagicID(MagicID);

            // if (oCmd is not  null && m_LastHitInfoIndex.HasValue)
            // {
            //     List<Warrior> vicObjs = new List<Warrior>();
            //     GetVicObjs(vicObjs);
            //
            //     foreach (var warrior  in vicObjs)
            //     {
            //         var vary = oCmd.GetWarriorVary(warrior.m_ID);
            //         oCmd.LockVary(vary, true);
            //
            //         if (vary != null && vary.ProtectId.HasValue)
            //         {
            //             var protectVary = oCmd.GetWarriorVary(vary.ProtectId.Value);
            //             if (protectVary != null)
            //             {
            //                 oCmd.LockVary(protectVary, true);
            //             }
            //         }
            //         
            //         warrior.MagicID = MagicID;
            //     }
            // }

            // PreLoadRes();
            // m_StartCallback?.Invoke();
        }

        internal void OnUpdate()
        {
            ElapsedTime += Time.deltaTime;

            foreach (var cmdHandler in _handlerList)
            {
                if (cmdHandler.Status == CmdStatus.Idle)
                {
                    cmdHandler.TryExecute();
                }
            }
        }
        
        public void Reset()
        {
            ID = 0;
            MagicID = 0;
            Shape = 0;
            MagicIdx = 0;
            IsPursued = false;
            ElapsedTime = 0;

            foreach (var handler in _handlerList)
            {
                Main.m_ReferencePool.Despawn(handler);
            }

            AtkObj = null;
            VicObjs.Clear();
            data.Clear();
        }



        public void End()
        {
            
        }
        

        /// <summary>
        /// 获取受影响的战士列表
        /// </summary>
        /// <param name="vicObjs"></param>
        // public List<Warrior> GetVicObjs(List<Warrior> vicObjs)
        // {
        //     vicObjs.Clear();
        //     
        // }

        /// <summary>
        /// 获得第一个受击者
        /// </summary>
        /// <returns></returns>
        public Warrior GetVicObjFirst()
        {
            return null;
        }

        public void GetTargets(bool isAlly, bool isAtk, bool isVic, bool isAlive, List<Warrior> warriors)
        {
           
        }

        public void CombHit()
        {
            // foreach (var warrior in VicObjs)
            // {
            //     warrior?.Play(AnimationConfig.HIT1);
            // }
        }


        public void AddHitInfo()
        {
           
        }

        public void OnCompleted()
        {
            _completedCallBack?.Invoke();
        }
    }
}