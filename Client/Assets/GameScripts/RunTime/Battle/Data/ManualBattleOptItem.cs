using GameScripts.RunTime.Pet;

namespace GameScripts.RunTime.Battle.Data
{
    /// <summary>
    /// 存储和处理手动战斗选项的数据
    /// </summary>
    public class ManualBattleOptItem
    {
        /// <summary>
        /// 该战斗选项关联的角色或宠物类型
        /// </summary>
        public PetType type { get; private set; }
        /// <summary>
        /// 攻击者的位置。这可能用于指定发起攻击的角色或单位的位置。
        /// </summary>
        public int attackerPos;
        /// <summary>
        /// 要使用的技能的ID
        /// </summary>
        public int skillId;
        /// <summary>
        /// 目标的位置。这用于指定技能或攻击的目标。
        /// </summary>
        public int targetPos;
        /// <summary>
        /// 使用的道具模板ID
        /// </summary>
        public int itemTplId;
        /// <summary>
        /// 召唤宠物的唯一标识符（UUID）
        /// </summary>
        public long summonPetUUID;
        /// <summary>
        /// 一个布尔值，指示是否需要选择目标
        /// </summary>
        public bool needSelectTarget;
        /// <summary>
        /// 一个布尔值，指示这个战斗选项是否已经完成或处理
        /// </summary>
        public bool isDone;

        public ManualBattleOptItem(PetType type)
        {
            this.type = type;
        }

        public void Reset()
        {
            attackerPos = 0;
            skillId = 0;
            targetPos = 0;
            itemTplId = 0;
            summonPetUUID = 0;
            needSelectTarget = false;
            isDone = false;
        }
    }
}