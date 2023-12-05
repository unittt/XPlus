using System.Collections;
using System.Collections.Generic;
using GameScripts.RunTime.Utility;

namespace GameScripts.RunTime.Battle.Report.Data
{
    /// <summary>
    /// 管理一个完整战斗回合中的数据
    /// </summary>
    public class BatRoundData
    {
        /// <summary>
        /// 攻击方数据
        /// </summary>
        public List<BatCharacterStatusData> attackerStatus { get; private set; }

        /// <summary>
        /// 攻击方额外信息
        /// </summary>
        public IDictionary attakerStatusAdd { get; private set; }

        /// <summary>
        /// 防守方数据
        /// </summary>
        public List<BatCharacterStatusData> defenderStatus { get; private set; }

        /// <summary>
        /// 防守方额外信息
        /// </summary>
        public IDictionary defenderStatusAdd { get; private set; }

        /// <summary>
        /// 回合阶段数据
        /// </summary>
        public List<BatRoundStageData> startDatas { get; private set; }

        public List<BatRoundStageData> progressDatas { get; private set; }
        public List<BatRoundStageData> endDatas { get; private set; }

        /// <summary>
        /// 回合编号
        /// </summary>
        public int roundNum { get; private set; }

        /// <summary>
        /// 是否为状态回合
        /// </summary>
        public bool isStatusRound { get; private set; }

        /// <summary>
        /// 是否为初始回合
        /// </summary>
        public bool isInitRound { get; private set; }

        /// <summary>
        /// 是否为结束回合
        /// </summary>
        public bool isFinalRound { get; private set; }

        /// <summary>
        /// 存储战斗的结果，如胜利、失败或其他状态
        /// </summary>
        public int battleResult { get; private set; }

        public long pvpAtkerId { get; set; }
        public long pvpDeferId { get; set; }
        public long pvpRoundCreateServerTime { get; set; }
        public long pvpRoundBroadcastServerTime { get; set; }
        public float pvpRoundCreateClientTime { get; set; }
        public float pvpRoundFinishClientTime { get; set; }
        public bool pvpRoundIsAutoBattle { get; set; }

        public long teamRoundCreateServerTime { get; set; }
        public long teamRoundBroadcastServerTime { get; set; }
        public float teamRoundCreateClientTime { get; set; }
        public float teamRoundFinishClientTime { get; set; }
        public bool teamRoundIsAutoBattle { get; set; }

        /// <summary>
        /// 回合的播放速度
        /// </summary>
        public int roundPlaySpeed { get; set; }
        /// <summary>
        /// 计算执行这个回合所花费的时间
        /// </summary>
        private float mSecondsCost = 0;


        public BatRoundData()
        {
            attackerStatus = new List<BatCharacterStatusData>();
            defenderStatus = new List<BatCharacterStatusData>();
            startDatas = new List<BatRoundStageData>();
            progressDatas = new List<BatRoundStageData>();
            endDatas = new List<BatRoundStageData>();
        }

        public void Parse(IDictionary data)
        {
            IList atkerStatusData = JsonHelper.GetListData(BattleReportDef.ATTACKERS.ToString(), data);
            attakerStatusAdd = JsonHelper.GetDictData(BattleReportDef.ATTACKERS_ADD.ToString(), data);
            IList defenderStatusData = JsonHelper.GetListData(BattleReportDef.DEFENDERS.ToString(), data);
            defenderStatusAdd = JsonHelper.GetDictData(BattleReportDef.DEFENDERS_ADD.ToString(), data);
            IDictionary batStartData = JsonHelper.GetDictData(BattleReportDef.BATTLE_START.ToString(), data);
            IDictionary batRoundData = JsonHelper.GetDictData(BattleReportDef.BATTLE_ROUND.ToString(), data);
            IDictionary batEndData = JsonHelper.GetDictData(BattleReportDef.BATTLE_END.ToString(), data);

            this.roundNum = JsonHelper.GetIntData(BattleReportDef.BATTLE_ROUND_NUM.ToString(), data);
            this.isFinalRound = data.Contains(BattleReportDef.BATTLE_RESULT.ToString());
            this.battleResult = isFinalRound
                ? JsonHelper.GetIntData(BattleReportDef.BATTLE_RESULT.ToString(), data)
                : 0;
            if (batStartData == null && batRoundData == null && batEndData == null)
            {
                this.isStatusRound = true;
                if (this.roundNum == 1)
                {
                    this.isInitRound = true;
                }

                Parse(atkerStatusData, defenderStatusData, null);
            }
            else
            {
                if (batStartData != null)
                {
                    Parse(atkerStatusData, defenderStatusData, batStartData);
                }

                if (batRoundData != null)
                {
                    Parse(atkerStatusData, defenderStatusData, batRoundData);
                }

                if (batEndData != null)
                {
                    Parse(atkerStatusData, defenderStatusData, batEndData);
                }
            }

            roundPlaySpeed = JsonHelper.GetIntData(BattleReportDef.BATTLE_SPEED.ToString(), data, 1);
        }


        private void Parse(IList atkerStatusData, IList defenderStatusData, IDictionary roundData)
        {
            int len = 0;
            if (atkerStatusData != null)
            {
                len = atkerStatusData.Count;
                for (int i = 0; i < len; i++)
                {
                    BatCharacterStatusData chaData = new BatCharacterStatusData();
                    // chaData.Parse((IDictionary)atkerStatusData[i]);
                    attackerStatus.Add(chaData);
                }
            }

            if (defenderStatusData != null)
            {
                len = defenderStatusData.Count;
                for (int i = 0; i < len; i++)
                {
                    BatCharacterStatusData chaData = new BatCharacterStatusData();
                    // chaData.Parse((IDictionary)defenderStatusData[i]);
                    defenderStatus.Add(chaData);
                }
            }

            if (roundData != null)
            {
                IList roundStartData = JsonHelper.GetListData(BattleReportDef.BATTLE_ROUND_START.ToString(), roundData);
                IList roundProgressData =
                    JsonHelper.GetListData(BattleReportDef.BATTLE_ROUND_IN_PROGRESS.ToString(), roundData);
                IList roundEndData = JsonHelper.GetListData(BattleReportDef.BATTLE_ROUND_END.ToString(), roundData);

                ParseRoundStartData(roundStartData, startDatas);
                ParseRoundProcessData(roundProgressData, progressDatas);
                ParseRoundEndData(roundEndData, endDatas);
            }
        }



        private void ParseRoundStartData(IList data, List<BatRoundStageData> stageDatas)
        {
            BatRoundStageStartData stageData = new BatRoundStageStartData();
            stageData.Parse(data);
            stageDatas.Add(stageData);
            mSecondsCost += stageData.secondsCost;
        }

        private void ParseRoundProcessData(IList data, List<BatRoundStageData> stageDatas)
        {
            if (data != null && stageDatas != null)
            {
                int len = data.Count;
                for (int i = 0; i < len; i++)
                {
                    // BatRoundStageProcessData stageData = new BatRoundStageProcessData();
                    // stageData.Parse((data[i]));
                    // stageDatas.Add(stageData);
                    // mSecondsCost += stageData.secondsCost;
                }
            }
        }

        private void ParseRoundEndData(IList data, List<BatRoundStageData> stageDatas)
        {
            // BatRoundStageEndData stageData = new BatRoundStageEndData();
            // stageData.Parse(data);
            // stageDatas.Add(stageData);
            // mSecondsCost += stageData.secondsCost;
        }

        public bool isStartDone
        {
            get
            {
                int len = startDatas.Count;
                for (int i = 0; i < len; i++)
                {
                    if (!startDatas[i].isDone)
                    {
                        return false;
                    }
                }

                return true;
            }
        }


        public bool isProgressDone
        {
            get
            {
                int len = progressDatas.Count;
                for (int i = 0; i < len; i++)
                {
                    if (!progressDatas[i].isDone)
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
                int len = endDatas.Count;
                for (int i = 0; i < len; i++)
                {
                    if (!endDatas[i].isDone)
                    {
                        return false;
                    }
                }

                return true;
            }
        }

        public bool isDone
        {
            get { return isStartDone && isProgressDone && isEndDone; }
        }

        public float secondsCost
        {
            get { return mSecondsCost; }
        }

        public bool hasAttacker(string uuidS, long uuidL)
        {
            int len = attackerStatus.Count;
            for (int i = 0; i < len; i++)
            {
                // if (attackerStatus[i].uuidS == uuidS && attackerStatus[i].uuidL == uuidL)
                // {
                //     return true;
                // }
            }

            return false;
        }

        public bool hasDefender(string uuidS, long uuidL)
        {
            int len = defenderStatus.Count;
            for (int i = 0; i < len; i++)
            {
                // if (defenderStatus[i].uuidS == uuidS && defenderStatus[i].uuidL == uuidL)
                // {
                //     return true;
                // }
            }

            return false;
        }
    }
}