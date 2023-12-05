using System.Collections;
using GameScripts.RunTime.Utility;

namespace GameScripts.RunTime.Battle.Report.Data
{
    /// <summary>
    /// 技能效果数据。
    /// </summary>
    public class BatRoundSkillResultData
    {
        /// <summary>
        /// 目标的UUID
        /// </summary>
        public string TargetUUID { get; private set; }
        
        /// <summary>
        /// 生命值变化。
        /// </summary>
        /// <value>The hp diff.</value>
        public int hpDiff { get; private set; }

        /// <summary>
        /// 魔法值变化。
        /// </summary>
        /// <value>The mp diff.</value>
        public int mpDiff { get; private set; }

        /// <summary>
        /// 怒气值变化。
        /// </summary>
        /// <value>The sp diff.</value>
        public int spDiff { get; private set; }
        /// <summary>
        /// 是否暴击。
        /// </summary>
        /// <value><c>true</c> if is fatal; otherwise, <c>false</c>.</value>
        public bool isCrit { get; private set; }

        /// <summary>
        /// 是否闪避。
        /// </summary>
        /// <value><c>true</c> if is dodgy; otherwise, <c>false</c>.</value>
        public bool isDodgy { get; private set; }

        /// <summary>
        /// 是否格挡。
        /// </summary>
        /// <value><c>true</c> if is block; otherwise, <c>false</c>.</value>
        public bool isDefense { get; private set; }
        
        /// <summary>
        /// 反击技能。
        /// </summary>
        /// <value>The counterattack.</value>
        public BatRoundSkillData counterattack { get; private set; }

        /// <summary>
        /// 逃跑结果。
        /// </summary>
        /// <value>The escape result.</value>
        public bool isEscaped { get; private set; }
        
        /// <summary>
        /// 是否击飞。
        /// </summary>
        /// <value><c>true</c> if is deadFly; otherwise, <c>false</c>.</value>
        public bool isDeadFly { get; private set; }
        
        /// <summary>
        /// 是否被捕捉。
        /// </summary>
        /// <value><c>true</c> if is beCaught; otherwise, <c>false</c>.</value>
        public bool isBeCaught { get; private set; }
        
        /// <summary>
        /// 是嗑药成功。
        /// </summary>
        /// <value><c>true</c> if is useDrugsSuccess; otherwise, <c>false</c>.</value>
        public bool isUseDrugsSuccess { get; private set; }
        
        /// <summary>
        /// 是否不冒泡提示属性变化。
        /// </summary>
        /// <value><c>true</c> if is bubbleValueChange; otherwise, <c>false</c>.</value>
        public bool isNoBubble { get; private set; }
        
        /// <summary>
        /// 召唤来的宠物数据。
        /// </summary>
        public BatCharacterStatusData summonTargetStatusData { get; private set; }
        public bool isSummonSuccess { get; private set; }
        
        public string errorMsg { get; private set; }
        
        
        public BatRoundBuffData buffData { get; private set; }

        public bool hasChivalric { get; private set; }

        public int chivalricId { get; private set; }

        public bool chivalricStatusChanged { get; private set; }
        

        // private BatCharacter mTarget = null;

        private BatRoundStageType mStageType = BatRoundStageType.NONE;
        
        
        public BatRoundSkillResultData(BatRoundStageType stageType)
        {
            mStageType = stageType;
        }

        public void Parse(IDictionary data)
        {
            TargetUUID = JsonHelper.GetStringData(BattleReportDef.REPORT_ITEM_TARGET.ToString(), data);
            hpDiff = JsonHelper.GetIntData(BattleReportDef.REPORT_ITEM_HP.ToString(), data);
            mpDiff = JsonHelper.GetIntData(BattleReportDef.REPORT_ITEM_MP.ToString(), data);
            spDiff = JsonHelper.GetIntData(BattleReportDef.REPORT_ITEM_SP.ToString(), data);
            isCrit = JsonHelper.GetBoolData(BattleReportDef.REPORT_ITEM_FATAL.ToString(), data);
            isDodgy = JsonHelper.GetBoolData(BattleReportDef.REPORT_ITEM_DODGY.ToString(), data);
            isEscaped = JsonHelper.GetBoolData(BattleReportDef.REPORT_ITEM_ESCAPE.ToString(), data);
            isDeadFly = JsonHelper.GetBoolData(BattleReportDef.REPORT_ITEM_DEAD_FLY.ToString(), data);
            isBeCaught = JsonHelper.GetBoolData(BattleReportDef.REPORT_ITEM_BE_CAUGHT.ToString(), data);
            isUseDrugsSuccess = JsonHelper.GetBoolData(BattleReportDef.REPORT_ITEM_USE_DRUGS.ToString(), data);
            isNoBubble = JsonHelper.GetBoolData(BattleReportDef.REPORT_ITEM_NO_POP.ToString(), data);
            summonTargetStatusData = new BatCharacterStatusData();
            // summonTargetStatusData.Parse(
            //     JsonHelper.GetDictData(BattleReportDef.REPORT_ITEM_SUMMON_PET.ToString(), data));
            isSummonSuccess = JsonHelper.GetBoolData(BattleReportDef.REPORT_ITEM_SUMMON_PET_RESULT.ToString(), data);

            chivalricStatusChanged = data.Contains(BattleReportDef.REPORT_ITEM_CHIVALRIC.ToString());
            hasChivalric = JsonHelper.GetBoolData(BattleReportDef.REPORT_ITEM_CHIVALRIC.ToString(), data);
            chivalricId = JsonHelper.GetIntData(BattleReportDef.REPORT_ITEM_CHIVALRIC_ID.ToString(), data);

            //TODO 反击

            int buffId = JsonHelper.GetIntData(BattleReportDef.REPORT_ITEM_BUFF_ID.ToString(), data);
            // if (PropertyUtil.IsLegalID(buffId))
            // {
            //     buffData = new BatRoundBuffData(mStageType);
            //     buffData.Parse(data);
            // }

            errorMsg = JsonHelper.GetStringData(BattleReportDef.BATTLE_ERROR.ToString(), data);
        }
    }
}