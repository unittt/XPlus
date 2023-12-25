using Cysharp.Threading.Tasks;

namespace GameScripts.RunTime.War
{
    /// <summary>
    /// 回合开始指令
    /// </summary>
    public class BoutStartCmd : WarCmd
    {
        public int bout_id;
        public int left_time;
        
        protected override bool IsCanExecute()
        {
            //1.等待所有的指令执行完成
            //2.调用BoutStart(bout_id, left_time);
            return base.IsCanExecute();
        }

        // protected override void OnExecute()
        // {
        //     WarManager.Current.WarBoutStart(bout_id, left_time);
        // }

        protected override void OnExecute()
        {
            WarManager.Current.WarBoutStart(bout_id, left_time);
            // Status = WarCmdStatus.Completed;
        }
    }
}