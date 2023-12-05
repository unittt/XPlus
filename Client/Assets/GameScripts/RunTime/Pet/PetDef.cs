namespace GameScripts.RunTime.Pet
{
    public class PetDef
    {
        
    }
    
    public enum PetType
    {
        NONE,
        /// <summary>
        /// 主将(1)。
        /// </summary>
        LEADER,

        /// <summary>
        /// 宠物(2)。
        /// </summary>
        PET,

        /// <summary>
        /// 伙伴(3)。
        /// </summary>
        FRIEND,

        /// <summary>
        /// 怪物(4)。
        /// </summary>
        MONSTER,
        
        /// <summary>
        /// 骑宠(5)。
        /// </summary>
        PET_FOR_RIDE
    }
}