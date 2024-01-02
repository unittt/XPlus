using System.Collections.Generic;
using System.Threading;
using GameScripts.RunTime.Magic.Command;
using GameScripts.RunTime.Magic.Command.Handler;
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
        public bool IsPursued { get; set; }

        private Dictionary<string, object> data = new(); // 用于存储攻击对象、受害对象等数据
        private List<CmdHandler> _handlerList = new(); // 执行播放技能指令列表
        private float _elapsedTime; // 已执行时间
        private bool _isRunning;
        private bool _isActive;
        private bool m_IsEndIdx = false;
        private string m_NextObjectName;
        private bool m_Running;
        private int? m_LastHitInfoIndex;

        public string m_RunEnv;

      
        public Warrior AtkObj;


        private int m_CurCmdIdx;

        public void Fill(int magicID, int shape, int magicIndex,bool isPursued, Warrior atkObj, List<Warrior> refVicObjs, List<CommandData> dataList)
        {
            //设置法术单元编号
            ID = Interlocked.Increment(ref MagicManager.CurUnitIdx);
            MagicID = magicID;
            Shape = shape;
            MagicIdx = magicIndex;
            IsPursued = isPursued;
            
            foreach (var cmdData in dataList)
            {
                var handlerType = MagicManager.Current.GetHandlerType(cmdData);
                var cmdHandler = Main.m_ReferencePool.Spawn(handlerType).Cast<CmdHandler>();
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

            var oAtkObj = GetAtkObj();
            oAtkObj?.SetPlayMagicID(MagicID);

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

        public void Update()
        {
            // if (!_isRunning)
            // {
            //     return;
            // }

            _elapsedTime += Time.deltaTime;

            for (var i = m_CurCmdIdx; i < _handlerList.Count; i++)
            {

                if ( _handlerList[i].StartTime >= _elapsedTime)
                {
                    //开始执行
                    _handlerList[i].Execute();
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
            _elapsedTime = 0;

            foreach (var handler in _handlerList)
            {
                Main.m_ReferencePool.Despawn(handler);
            }
            data.Clear();
        }

        public void SetIsEndIdx(bool isEnd)
        {
            m_IsEndIdx = isEnd;
        }

        public void ControlNextObject(string name)
        {
            m_NextObjectName = name;
        }
        

        /// <summary>
        /// 获得攻击对象
        /// </summary>
        /// <returns></returns>
        public Warrior GetAtkObj()
        {
            return null;
        }

        /// <summary>
        /// 获取受影响的战士列表
        /// </summary>
        /// <param name="vicObjs"></param>
        public void GetVicObjs(List<Warrior> vicObjs)
        {
            vicObjs.Clear();
            
        }

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

     
    }
}