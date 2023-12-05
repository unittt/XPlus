using GameScripts.RunTime.Pet;

namespace GameScripts.RunTime.Battle.Report.Data
{
    /// <summary>
    /// 回合开始时的角色状态。
    /// </summary>
    public class BatCharacterStatusData
    {
        public PetType PType { get; private set; }
        /// <summary>
        /// 战斗对象唯一Id。
        /// </summary>
        public string UUID { get; private set; }
        /// <summary>
        /// 所属玩家唯一Id。
        /// </summary>
        /// <value>The UUID.</value>
        public long OwnerUUID { get; private set; }
        
        /// <summary>
        /// 显示的模型id
        /// </summary>
        public string DisplayModelId { get; private set; }

        /// <summary>
        /// 名字
        /// </summary>
        public string Name { get; private set; }
        
        /// <summary>
        /// 位置，从1开始。
        /// </summary>
        /// <value>The position.</value>
        public int Pos { get; private set; }
        
        
        /// <summary>
        /// 血量。
        /// </summary>
        /// <value>The hp.</value>
        public int HP { get; private set; }
        
        /// <summary>
        /// 血量上限。
        /// </summary>
        /// <value>The max hp.</value>
        public int MaxHP { get; private set; }
        
        /// <summary>
        /// 魔法。
        /// </summary>
        /// <value>The mp.</value>
        public int MP { get; private set; }

        /// <summary>
        /// 魔法上限。
        /// </summary>
        /// <value>The max mp.</value>
        public int MaxMP { get; private set; }
        
        /// <summary>
        /// 怒气。
        /// </summary>
        /// <value>The sp.</value>
        public int SP { get; private set; }
        
        /// <summary>
        /// 怒气上限。
        /// </summary>
        /// <value>The max hp.</value>
        public int MaxSP { get; private set; }
        
        
        /// <summary>
        /// 是否可被捕捉。
        /// </summary>
        /// <value>isCanBeChatched.</value>
        public bool IsCanBeChatched { get; private set; }
        
        /// <summary>
        /// 是否是变异体。
        /// </summary>
        /// <value>isVariant.</value>
        public bool IsVariant { get; private set; }
    }
}