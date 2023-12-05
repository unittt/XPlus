using System.Collections;
using System.Collections.Generic;

namespace GameScripts.RunTime.Battle.Report.Data
{
    
    /// <summary>
    /// 一个技能数据。
    /// </summary>
    public class BatRoundSkillData : BatRoundBehaveData
    {
        public List<BatRoundSkillResultData> results { get; private set; }
        // public SkillTemplate skillTpl { get; private set; }
        public bool doPerform { get; private set; }
        public List<int> skillEffects { get; private set; }
        public bool isCombo { get; private set; }

        public BatRoundSkillData(BatRoundStageType stageType) : base(stageType)
        {
            Type = BattleRoundBehaveType.SKILL;
            results = new List<BatRoundSkillResultData>();
            skillEffects = new List<int>();
        }

        public void Parse(IDictionary data, bool doPerform)
        {
            
        }
        
        public bool isCounterAttacksDone
        {
            get
            {
                int len = results.Count;
                for (int i = 0; i < len; i++)
                {
                    if (results[i].counterattack != null)
                    {
                        if (!results[i].counterattack.isDone)
                        {
                            return false;
                        }
                    }
                }
                return true;
            }
        }
        
        public override bool isDone
        {
            get
            {
                return base.isDone && isCounterAttacksDone;
            }
            set
            {
                base.isDone = value;
            }
        }
    }
}
