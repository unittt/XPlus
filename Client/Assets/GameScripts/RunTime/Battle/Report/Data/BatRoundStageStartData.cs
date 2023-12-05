using System.Collections;

namespace GameScripts.RunTime.Battle.Report.Data
{
    public class BatRoundStageStartData: BatRoundStageData
    {
        public BatRoundStageStartData() : base(BatRoundStageType.START)
        {

        }
        
        public override void Parse(object data)
        {
            ParseBehaveItemDatas((IList)data, startItems);
        }

        public override float secondsCost => 0;
    }
}