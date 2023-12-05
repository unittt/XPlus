using System.Collections;
using System.Collections.Generic;

namespace GameScripts.RunTime.Battle.Report.Data
{

    /// <summary>
    /// 一回合的一个阶段中所有的行为数据。
    /// </summary>
    public abstract class BatRoundStageData
    {
        public List<BatRoundBehaveData> startItems { get; protected set; }
        public List<BatRoundBehaveData> exeItems { get; protected set; }
        public List<BatRoundBehaveData> defItems { get; protected set; }
        public List<BatRoundBehaveData> adjustItems { get; protected set; }
        public List<BatRoundBehaveData> endItems { get; protected set; }

        protected float mSecondsCost = 0;

        private BatRoundStageType mStageType = BatRoundStageType.NONE;

        public BatRoundStageData(BatRoundStageType stageType)
        {
            startItems = new List<BatRoundBehaveData>();
            exeItems = new List<BatRoundBehaveData>();
            defItems = new List<BatRoundBehaveData>();
            adjustItems = new List<BatRoundBehaveData>();
            endItems = new List<BatRoundBehaveData>();
            mStageType = stageType;
        }

        public abstract void Parse(object data);
        public abstract float secondsCost { get; }


        protected void ParseBehaveItemDatas(IList datas, List<BatRoundBehaveData> items)
        {
            if (datas != null && items != null)
            {
                int len = datas.Count;
                for (int i = 0; i < len; i++)
                {
                    IDictionary data = (IDictionary)(datas[i]);

                    if (data.Contains(BattleReportDef.RECORD_CONTENT_SKILLID.ToString()))
                    {
                        //技能。
                        BatRoundSkillData skillData = new BatRoundSkillData(mStageType);
                        skillData.Parse(data, items == exeItems);
                        // if (skillData.skillTpl.Id == BatSkillID.USE_ITEM)
                        // {
                        //     int skillResLen = skillData.results.Count;
                        //     for (int j = 0; j < skillResLen; j++)
                        //     {
                        //         if (!skillData.results[j].isUseDrugsSuccess)
                        //         {
                        //             // ClientLog.LogWarning("不体现嗑药失败的战报。");
                        //             continue;
                        //         }
                        //     }
                        // }

                        items.Add(skillData);
                        mSecondsCost += BattleDef.DEFAULT_SKILL_SECONDS_COST;
                    }
                    else
                    {
                        // ClientLog.LogError("并没有解析技能以外的行为数据!");
                    }
                }
            }
        }


        public bool isStartDone
        {
            get
            {
                var len = startItems.Count;
                for (var i = 0; i < len; i++)
                {
                    if (!startItems[i].isDone)
                    {
                        return false;
                    }
                }

                return true;
            }
        }

        public bool isExecuteDone
        {
            get
            {
                var len = exeItems.Count;
                for (var i = 0; i < len; i++)
                {
                    if (!exeItems[i].isDone)
                    {
                        return false;
                    }
                }

                return true;
            }
        }

        public bool isDefenceDone
        {
            get
            {
                var len = defItems.Count;
                for (var i = 0; i < len; i++)
                {
                    if (!defItems[i].isDone)
                    {
                        return false;
                    }
                }

                return true;
            }
        }

        public bool isAdjustDone
        {
            get
            {
                var len = adjustItems.Count;
                for (var i = 0; i < len; i++)
                {
                    if (!adjustItems[i].isDone)
                    {
                        return false;
                    }
                }

                return true;
            }
        }

        public bool isEndDone
        {
            get
            {
                var len = endItems.Count;
                for (var i = 0; i < len; i++)
                {
                    if (!endItems[i].isDone)
                    {
                        return false;
                    }
                }

                return true;
            }
        }

        public bool isDone => isStartDone && isExecuteDone && isDefenceDone && isAdjustDone && isEndDone;
    }
}