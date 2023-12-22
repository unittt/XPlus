using System.Collections.Generic;

namespace GameScripts.RunTime.War
{
    public class Vary
    {
        public bool IsLock;
        public bool Status;

        public List<int> hp_list;
        public List<int> damage_list;
        public List<int> addMp_list;
        public List<int> notify_cmds;
        public List<int> buff_list;

        public Dictionary<string, string> CmdDic;
        public int? ProtectId;
        public List<TriggerPassiveSkill> trigger_passive;
    }
}