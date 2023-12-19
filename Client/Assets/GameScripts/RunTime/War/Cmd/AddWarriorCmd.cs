using Pb.Mmo.Common;

namespace GameScripts.RunTime.War
{
    public class AddWarriorCmd: WarCmd
    {
        public BaseWarrior Warrior;
        public int camp_id;
        public bool is_summon;
    }
}