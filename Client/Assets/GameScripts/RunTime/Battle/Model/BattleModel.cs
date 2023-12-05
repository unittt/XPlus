using System.Collections.Generic;
using GameScripts.RunTime.Battle.Data;
using GameScripts.RunTime.Battle.Report.Data;
using GameScripts.RunTime.Pet;
using HT.Framework;
using UnityEngine;

namespace GameScripts.RunTime.Battle.Model
{
    /// <summary>
    /// 管理战斗场景中的数据 和 逻辑
    /// </summary>
    public class BattleModel :SingletonBase<BattleModel>
    {
        // 玩家自动技能改变事件
        public const string PLAYER_AUTO_SKILL_CHANE = "PLAYER_AUTO_SKILL_CHANE";
        // 宠物自动技能改变事件
        public const string PET_AUTO_SKILL_CHANGE = "PET_AUTO_SKILL_CHANGE";
        
        /// <summary>
        /// 战斗总时间
        /// </summary>
        public float BattleTime;
        /// <summary>
        /// 战斗固定时间
        /// </summary>
        public float BattleFixedTime;
        
        public float viewportWidth { get; set; }
        public float viewportHeight { get; set; }
        /// <summary>
        /// 战斗结束返回类型
        /// </summary>
        public int battleToBackType { get; set; }
        /// <summary>
        /// 战斗类型（PVE、PVP等）
        /// </summary>
        public BattleType battleType { get; set; }
        /// <summary>
        /// 战斗子类型（自动、手动）
        /// </summary>
        public BattleSubType battleSubType { get; set; }
        /// <summary>
        /// 回合数据列表
        /// </summary>
        public List<BatRoundData> roundData { get; set; }
        /// <summary>
        /// 上一回合数据
        /// </summary>
        public BatRoundData lastRoundData { get; set; }
        /// <summary>
        /// 当前回合数据
        /// </summary>
        public BatRoundData curRoundData { get; set; }
        /// <summary>
        /// 当前回合状态
        /// </summary>
        public BattleRoundStatusType curRoundStatus { get; set; }
        /// <summary>
        /// 当前回合编号
        /// </summary>
        public int curRoundNum { get; set; }
        /// <summary>
        /// 当前回合剩余等待时间
        /// </summary>
        public float curRoundWaitTimeLeft { get; set; }
        
        /// <summary>
        /// 主角色手动操作选项
        /// </summary>
        public ManualBattleOptItem mainRoleManualOptItem { get; private set; }
        /// <summary>
        /// 主宠物手动操作选项
        /// </summary>
        public ManualBattleOptItem mainPetManualOptItem { get; private set; }
        
        /// <summary>
        ///  角色站位类型（攻击者或防御者）
        /// </summary>
        public BatCharacterSiteType selfSiteType { get; set; }
        
        /// <summary>
        /// 队长激活技能ID
        /// </summary>
        public int leaderActivedSkillId { get; private set; }
        /// <summary>
        /// 宠物激活技能ID
        /// </summary>
        public int petActivedSkillId { get; private set; }

        /// <summary>
        /// 是否可以更新
        /// </summary>
        public bool canUpdate { get; set; }
        
        /// <summary>
        /// 需要隐藏准备标志的角色列表
        /// </summary>
        public List<long> charactersNeedHidePrepareSign { get; set; }
        
        /// <summary>
        /// 本场战斗中死亡的宠物ID列表
        /// </summary>
        public List<long> deadPetIds = new List<long>();
        
        /// <summary>
        /// 战斗结果（1:胜利；2:失败）
        /// </summary>
        private int battleResult = 0;
        
        // 不可销毁的资源路径列表
        private List<string> mUndisposableResPathList = new List<string>();

        public float maxModelHeight;
        // 当前PVP目标
        // public GCBattleStartPvpInvite pvptarget { get; set; }
        
        // 角色信息视图
        // public RoleInfoView roleInfoView { get; set; }
        
        // 快速战斗速度
        public int battleSpeedFast
        {
            get
            {
                return 0;
                // return ConstantModel.Ins.GetIntValueByKey(ServerConstantDef.BATTLE_SPEEDUP_X);
            }
        }
        
        // 战斗结果访问器
        public int BattleResult
        {
            get { return battleResult; }
            set { battleResult = value; }
        }
        
        public BattleModel()
        {
            
            roundData = new List<BatRoundData>();
            mainRoleManualOptItem = new ManualBattleOptItem(PetType.LEADER);
            mainPetManualOptItem = new ManualBattleOptItem(PetType.PET);
            charactersNeedHidePrepareSign = new List<long>();
        }
        
        /// <summary>
        /// 根据实时帧率修正速度。
        /// </summary>
        /// <param name="speed">默认帧率下的速度。</param>
        /// <returns>实时速度。</returns>
        public float ReviseSpeed(float speed)
        {
            return speed * (Application.targetFrameRate / (Time.timeScale / Time.deltaTime));
        }
        
        public void SetResUndisposable(string resPath)
        {
            if (!mUndisposableResPathList.Contains(resPath))
            {
                // SourceManager.Ins.ignoreDispose(resPath);
                mUndisposableResPathList.Add(resPath);
            }
        }
        
        public void SetAllResDisposable(bool clearAllRes)
        {
            int len = mUndisposableResPathList.Count;
            for (int i = 0; i < len; i++)
            {
                // SourceManager.Ins.unignoreDispose(mUndisposableResPathList[i]);
                if (clearAllRes)
                {
                    // SourceManager.Ins.ClearAllReference(mUndisposableResPathList[i]);
                }
            }
            mUndisposableResPathList.Clear();
        }


        /// <summary>
        /// 切换主动的技能
        /// </summary>
        /// <param name="petType"></param>
        /// <param name="skillId"></param>
        public void ChangeActiveSkill(PetType petType, int skillId)
        {
            if (petType == PetType.LEADER)
            {
                // ClientLog.LogWarning("接收到后台推送 主将技能切换为  " + skillId);
                leaderActivedSkillId = skillId;
                // dispatchChangeEvent(PLAYER_AUTO_SKILL_CHANE,skillId);
            }
            else if (petType == PetType.PET)
            {
                // ClientLog.LogWarning("接收到后台推送 宠物技能切换为  " + skillId);
                petActivedSkillId = skillId;
                // dispatchChangeEvent(PET_AUTO_SKILL_CHANGE,skillId);
            }

            // if (BattleUI.ins.isShown)
            // {
            //     BattleUI.ins.ChangeActivedSkill(petType, skillId);
            // }
        }
    }
}