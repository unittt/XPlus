using Pb.Mmo.Common;

namespace GameScripts.RunTime.War
{
    
    /// <summary>
    /// 更新状态指令
    /// </summary>
    public class WarriorStatusCmd : WarCmd
    {
        public int wid;
        public WarriorStatus status;

        protected override void OnExecute()
        {
            SetCompleted();
            
        }
    }
}