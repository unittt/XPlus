using System.Collections;
using GameScripts.RunTime.Utility;

namespace GameScripts.RunTime.Battle.Report.Data
{
    /// <summary>
    /// 一条buff数据。
    /// </summary>
    public class BatRoundBuffData : BatRoundBehaveData
    {
        
        public int UUID { get; private set; }
        public int ID { get; private set; }
        
        /// <summary>
        /// 状态
        /// </summary>
        public SkillBuffStateType StateType { get; private set; }

        /// <summary>
        /// 回合
        /// </summary>
        public int RoundLeft { get; private set; }

        public BatRoundBuffData(BatRoundStageType stageType) : base(stageType)
        {
            Type = BattleRoundBehaveType.BUFF;
        }

        public void Parse(IDictionary data)
        {
            UUID = JsonHelper.GetIntData(BattleReportDef.REPORT_ITEM_BUFF_UUID.ToString(), data);
            ID = JsonHelper.GetIntData(BattleReportDef.REPORT_ITEM_BUFF_ID.ToString(), data);
            // tpl = SkillBuffTemplateDB.Instance.getTemplate(id);
            HostUUID = JsonHelper.GetStringData(BattleReportDef.REPORT_ITEM_TARGET.ToString(), data);
            StateType = (SkillBuffStateType)(JsonHelper.GetIntData(BattleReportDef.REPORT_ITEM_BUFF_STATE.ToString(), data));
            RoundLeft = JsonHelper.GetIntData(BattleReportDef.REPORT_ITEM_BUFF_LEFT.ToString(), data);
        }
    }
}