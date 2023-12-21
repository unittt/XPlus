using System.Collections.Generic;

namespace GameScripts.RunTime.War
{
    public class GoBackCmd : WarCmd
    {
        public List<int>  wid_list;
        public bool wait;

        public override void Execute()
        {
            foreach (var wid in wid_list)
            {
                if (WarManager.Current.TryGetWarrior(wid, out var warrior))
                {
                 
                }
            }
        }
    }
}