using System.Collections;

namespace GameScripts.RunTime.Battle.Report.Data
{
    public class BatRoundStageEndData:BatRoundStageData
    {
        public BatRoundStageEndData(BatRoundStageType stageType) : base(BatRoundStageType.END)
        {
        }

        public override void Parse(object data)
        {
            ParseBehaveItemDatas((IList)data, endItems);
        }

        public override float secondsCost => 0;
    }
}