using System.Collections;
using GameScripts.RunTime.Utility;

namespace GameScripts.RunTime.Battle.Report.Data
{
    public class BatRoundStageProcessData:BatRoundStageData
    {
        public BatRoundStageProcessData(BatRoundStageType stageType) : base(BatRoundStageType.PROGRESS)
        {
        }

        public override void Parse(object data)
        {
            var dic = (IDictionary)data;
            var startDataList = JsonHelper.GetListData(BattleReportDef.BATTLE_ACTION_START.ToString(), dic);
            var exeDataList = JsonHelper.GetListData(BattleReportDef.BATTLE_ACTION_EXECUTE.ToString(), dic);
            var defDataList = JsonHelper.GetListData(BattleReportDef.BATTLE_ACTION_DEFENCE.ToString(), dic);
            var adjustDataList = JsonHelper.GetListData(BattleReportDef.BATTLE_ACTION_ADJUST.ToString(), dic);
            var endDataList = JsonHelper.GetListData(BattleReportDef.BATTLE_ACTION_END.ToString(), dic);

            ParseBehaveItemDatas(startDataList, startItems);
            ParseBehaveItemDatas(exeDataList, exeItems);
            ParseBehaveItemDatas(defDataList, defItems);
            ParseBehaveItemDatas(adjustDataList, adjustItems);
            ParseBehaveItemDatas(endDataList, endItems);
        }

        public override float secondsCost => mSecondsCost;
    }
}