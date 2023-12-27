namespace GameScripts.RunTime.War
{
    /// <summary>
    /// 回合开始指令
    /// </summary>
    public class BoutStartCmd : WarCmd
    {
        /// <summary>
        /// 回合id
        /// </summary>
        public int bout_id;
        /// <summary>
        /// 倒计时
        /// </summary>
        public int left_time;
        
        protected override void OnExecute()
        {
            WarManager.Current.WarBoutStart(bout_id, left_time);
            SetCompleted();
        }
    }
}