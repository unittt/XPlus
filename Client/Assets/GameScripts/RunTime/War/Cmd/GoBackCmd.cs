using System.Collections.Generic;
using HT.Framework;

namespace GameScripts.RunTime.War
{
    public class GoBackCmd : WarCmd
    {
        public List<int>  wid_list;
        public bool wait;

        protected override void OnExecute()
        {
            Log.Info("go back a ............");
            foreach (var wid in wid_list)
            {
                if (WarManager.Current.TryGetWarrior(wid, out var warrior))
                {
                    warrior.GoBack(1);
                }
            }
        }
    }
}