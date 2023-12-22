namespace GameScripts.RunTime.War
{
    public class CheckNotifyCmds : WarCmd
    {

        public Vary vary;
        public int iFlag;
        
        protected override void OnExecute()
        {
            foreach (var cmd in vary.notify_cmds)
            {
                
            }
        }
    }
}