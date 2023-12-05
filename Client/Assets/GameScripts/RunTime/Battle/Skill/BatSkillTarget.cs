using GameScripts.RunTime.Battle.Character;
using GameScripts.RunTime.Battle.Report.Data;

namespace GameScripts.RunTime.Battle.Skill
{
    /// <summary>
    /// 一个战斗技能的目标以及该目标在技能影响下的结果
    /// </summary>
    public class BatSkillTarget
    {
        /// <summary>
        /// 技能作用于目标时的结果数据，这些数据包括目标角色、血量变化、魔法值变化等信息
        /// </summary>
         public BatRoundSkillResultData mData = null;

        public BatSkillTarget(BatRoundSkillResultData data)
        {
            this.mData = data;
        }

        /// <summary>
        /// 目标角色对象
        /// </summary>
        // public BatCharacter character
        // {
        //     get
        //     {
        //         return mData != null ? mData.target : null;
        //     }
        // }


        public int hpDiff
        {
            get
            {
                return mData != null ? mData.hpDiff : 0;
            }
        }

        public int mpDiff
        {
            get
            {
                return mData != null ? mData.mpDiff : 0;
            }
        }

        public int spDiff
        {
            get
            {
                return mData != null ? mData.spDiff : 0;
            }
        }

        public bool isCrit
        {
            get
            {
                return mData != null ? mData.isCrit : false;
            }
        }

        public bool isDodgy
        {
            get
            {
                return mData != null ? mData.isDodgy : false;
            }
        }

        public bool isDefense
        {
            get
            {
                return mData != null ? mData.isDefense : false;
            }
        }

        /// <summary>
        /// 技能给目标角色附加的 Buff（增益或减益效果）的详细信息
        /// </summary>
        public BatRoundBuffData buffData
        {
            get
            {
                return mData != null ? mData.buffData : null;
            }
        }

        
        /// <summary>
        /// 是否被捕捉
        /// </summary>
        public bool isBeCaught
        {
            get
            {
                return mData != null ? mData.isBeCaught : false;
            }
        }

        /// <summary>
        /// 是否逃脱
        /// </summary>
        public bool isEscaped
        {
            get
            {
                return mData != null ? mData.isEscaped : false;
            }
        }
        
        /// <summary>
        /// 是否死亡飞行
        /// </summary>
        public bool isDeadFly
        {
            get
            {
                return mData != null ? mData.isDeadFly : false;
            }
        }
        
        public bool isNoBubble
        {
            get
            {
                return mData != null ? mData.isNoBubble : false;
            }
        }
        
        /// <summary>
        /// 目标角色唯一标识符
        /// </summary>
        public string targetUUID
        {
            get
            {
                return mData != null ? mData.TargetUUID : null;
            }
        }
        
        /// <summary>
        /// 召唤结果状态
        /// </summary>
        public BatCharacterStatusData summonResultStatusData
        {
            get
            {
                return mData != null ? mData.summonTargetStatusData : null;
            }
        }
        
        /// <summary>
        /// 召唤成功标识
        /// </summary>
        public bool isSummonSuccess
        {
            get
            {
                return mData != null ? mData.isSummonSuccess : false;
            }
        }

        public bool chivalricStatusChanged
        {
            get
            {
                return mData != null ? mData.chivalricStatusChanged : false;
            }
        }

        public bool hasChivalric
        {
            get
            {
                return mData != null ? mData.hasChivalric : false;
            }
        }

        public int chivalricId
        {
            get
            {
                return mData != null ? mData.chivalricId : 0;
            }
        }
    }
}