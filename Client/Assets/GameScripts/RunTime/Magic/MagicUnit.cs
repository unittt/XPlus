using System.Collections.Generic;
using GameScripts.RunTime.War;
using HT.Framework;

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
        public bool m_IsPursued { get; set; }

        private Dictionary<string, object> data = new(); // 用于存储攻击对象、受害对象等数据
        private List<MagicCmd> _commandList = new(); // 执行播放技能指令列表
        private float _elapsedTime; // 已执行时间
        private bool _isRunning;
        private bool _isActive;
        private bool m_IsEndIdx = false;
        private string m_NextObjectName;
        private bool m_Running;
        private int? m_LastHitInfoIndex;

        public string m_RunEnv;
        
        
        public void Reset()
        {
            data.Clear();
            _commandList.Clear();
        }

        public void SetIsEndIdx(bool isEnd)
        {
            m_IsEndIdx = isEnd;
        }

        public void ControlNextObject(string name)
        {
            m_NextObjectName = name;
        }

        public void Start(WarCmd oCmd)
        {
            m_Running = true;

            var oAtkObj = GetAtkObj();
            if (oAtkObj is not null)
            {
                oAtkObj.SetPlayMagicID(MagicID);
            }

            if (oCmd is not  null && m_LastHitInfoIndex.HasValue)
            {
                List<Warrior> vicObjs = new List<Warrior>();
                GetVicObjs(vicObjs);

                foreach (var warrior  in vicObjs)
                {
                    var vary = oCmd.GetWarriorVary(warrior.m_ID);
                    oCmd.LockVary(vary, true);

                    if (vary != null && vary.ProtectId.HasValue)
                    {
                        var protectVary = oCmd.GetWarriorVary(vary.ProtectId.Value);
                        if (protectVary != null)
                        {
                            oCmd.LockVary(protectVary, true);
                        }
                    }
                    
                    warrior.MagicID = MagicID;
                }
            }

            // PreLoadRes();
            // m_StartCallback?.Invoke();
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
    }
}