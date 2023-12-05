namespace GameScripts.RunTime.Battle.Report.Data
{
    
    /// <summary>
    /// 一条行为数据。
    /// </summary>
    public abstract class BatRoundBehaveData
    {
        public BattleRoundBehaveType Type { get; protected set; }
        
        public string HostUUID { get; protected set; }

        protected bool mIsDone = false;
        
        public BatRoundStageType StageType { get; private set; }
        
        public BatRoundBehaveData(BatRoundStageType stageType)
        {
            StageType = stageType;
            mIsDone = false;
        }
        
        public virtual bool isDone
        {
            get
            {
                return mIsDone;
            }
            set
            {
                mIsDone = value;
            }
        }
    }
}