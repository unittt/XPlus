using System.Collections.Generic;

namespace GameScripts.RunTime.War
{
    /// <summary>
    /// 施法指令
    /// </summary>
    public class MagicCmd : WarCmd
    {
        public List<int> atkid_list;
        public List<int> vicid_list;
        public int magic_id;
        public int magic_index;
    }
}