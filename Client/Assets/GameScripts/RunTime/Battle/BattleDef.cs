namespace GameScripts.RunTime.Battle
{
    public class BattleDef
    {
        /// <summary>
        /// 手动战斗回合倒计时。
        /// </summary>
        public const float MANUAL_ROUND_CD_SECONDS = 30.0f;

        /// <summary>
        /// 自动战斗回合倒计时。
        /// </summary>
        public const float AUTO_ROUND_CD_SECONDS = 3.0f;

        public const float SKILL_START_DELAY = 0.0f;

        public const float BULLET_THROW_SPEED = 0.5f;
        public const float BULLET_THROW_ANGLE = 20f;
        public const float BULLET_SHOT_SPEED = 1f;

        public const float CHARACTER_MOVE_SPEED = 25f;

        public const float IMPACT_EFFECT_MAX_AGE = 1.0f;

        public const float DEFAULT_SKILL_SECONDS_COST = 3.0f;

        //public static readonly int PLAY_REPORT_FAST_SPEED = 3;

        public const int PLAY_REPORT_NOR_SPEED = 1;
    }
    
    /// <summary>
    /// 战斗类型
    /// </summary>
    public enum BattleType
    {
        NONE,
        /// <summary>
        /// 播放战斗报告或重播战斗的模式
        /// </summary>
        PLAY_BATTLE_REPORT,
        /// <summary>
        /// 玩家与环境
        /// </summary>
        PVE,
        /// <summary>
        /// 玩家之间的对战模式
        /// </summary>
        PVP,
        /// <summary>
        /// 团队与环境的战斗
        /// </summary>
        TEAM_PVE,
        /// <summary>
        /// 团队间的对战模式
        /// </summary>
        TEAM_PVP
    }

    /// <summary>
    /// 自动与手动控制
    /// </summary>
    public enum BattleSubType
    {
        NONE,
        /// <summary>
        /// 自动
        /// </summary>
        AUTO,
        /// <summary>
        /// 手动
        /// </summary>
        MANUAL
    }

    /// <summary>
    /// 战斗回合阶段
    /// </summary>
    public enum BattleRoundStatusType
    {
        NONE,
        /// <summary>
        /// 表示正在请求回合数据或准备回合的状态
        /// </summary>
        ROUND_REQUESTING,
        /// <summary>
        /// 回合初始化的开始阶段
        /// </summary>
        ROUND_INIT_START,
        /// <summary>
        /// 回合初始化过程中
        /// </summary>
        ROUND_INIT_PROGRESS,
        /// <summary>
        /// 回合初始化完成
        /// </summary>
        ROUND_INIT_FINISH,
        /// <summary>
        /// 回合开始的初始阶段
        /// </summary>
        ROUND_START_START,
        /// <summary>
        /// 回合开始过程中
        /// </summary>
        ROUND_START_PROGRESS,
        /// <summary>
        /// 回合开始的结束阶段
        /// </summary>
        ROUND_START_FINISH,
        /// <summary>
        /// 回合进行的开始阶段
        /// </summary>
        ROUND_PROGRESS_START,
        /// <summary>
        /// 回合进行过程中
        /// </summary>
        ROUND_PROGRESS_PROGRESS,
        /// <summary>
        /// 回合进行的结束阶段
        /// </summary>
        ROUND_PROGRESS_FINISH,
        /// <summary>
        /// 回合结束的开始阶段
        /// </summary>
        ROUND_END_START,
        /// <summary>
        /// 回合结束过程中
        /// </summary>
        ROUND_END_PROGRESS,
        /// <summary>
        /// 回合结束的最后阶段
        /// </summary>
        ROUND_END_FINISH
    }

    public enum BattleBehavStatusType
    {
        NONE,
        BEHAV_START,
        BEHAV_EXECUTE,
        BEHAV_DEFENCE,
        BEHAV_ADJUST,
        BEHAV_END
    }
    
    
    public enum BattleRoundBehaveType
    {
        NONE,
        SKILL,
        BUFF
    }
    
    public struct BatCharacterStatus
    {
        /** 正常状态*/
        public const int NORMAL = 0;
        /** 死亡*/
        public const int DEAD = 1;
        /** 被控制（被眩晕）*/
        public const int DISABLE = 2;
        /** 禁止普通攻击*/
        public const int FORBID_NORMAL = 4;
        /** 禁止技能攻击（被沉默）*/
        public const int FORBID_SKILL = 8;
        /** 被捕捉了 */
        public const int BE_CAUGHT = 16;
        /** 防御 */
        public const int DEFENSE = 32;
        /** 无敌 */
        public const int NBDZT = 64;
        /** 混乱 */
        public const int CHAOS = 128;
        /** 逃跑 */
        public const int ESCAPE = 256;
        /** 被击飞 */
        public const int DEAD_FLY = 512;
    }
    
    /// <summary>
    /// 武将战斗类型，区分攻击方式。
    /// </summary>
    public enum BatCharacterAttackType
    {
        NONE,

        /// <summary>
        /// 力量型（物理攻击）(1)。
        /// </summary>
        STRENGTH,

        /// <summary>
        /// 智力型（法术攻击）(2)。
        /// </summary>
        INTELLECT
    }
    
    
    public enum BatCharacterSiteType
    {
        NONE,
        ATTACKER,
        DEFENDER
    }
    
    /// <summary>
    /// 特效出现的位置类型。
    /// </summary>
    public enum SkillEffectPosType
    {
        /// <summary>
        /// 无(0)。
        /// </summary>
        NONE,
        /// <summary>
        /// 脚下的地面(1)。
        /// </summary>
        GROUND,
        /// <summary>
        /// 周身(2)。
        /// </summary>
        BODY,
        /// <summary>
        /// 左手(3)。
        /// </summary>
        LEFT_HAND,
        /// <summary>
        /// 右手(4)。
        /// </summary>
        RIGHT_HAND,
        /// <summary>
        /// 头顶(5)。
        /// </summary>
        HEAD_TOP,
        /// <summary>
        /// 子弹发射点(6)。
        /// </summary>
        FIRE_ROOT
    }
    
    /// <summary>
    /// 特效类型。
    /// </summary>
    public enum SkillEffectType
    {
        /// <summary>
        /// 无(0)。
        /// </summary>
        NONE,
        /// <summary>
        /// 地面特效(1)。
        /// </summary>
        GROUND,
        /// <summary>
        /// 身体特效(2)。
        /// </summary>
        BODY,
        /// <summary>
        /// 子弹(3)。
        /// </summary>
        BULLET
    }
    
    /// <summary>
    /// 特效出现目标。
    /// </summary>
    public enum SkillEffectShowTarget
    {
        /// <summary>
        /// 无(0)。
        /// </summary>
        NONE,
        /// <summary>
        /// 自己(1)。
        /// </summary>
        SELF,
        /// <summary>
        /// 敌方技能目标(2)。
        /// </summary>
        ENEMY_SKILL_TARGET,
        /// <summary>
        /// 己方技能目标(3)。
        /// </summary>
        SELF_SKILL_TARGET,
        /// <summary>
        /// 全屏(4)。
        /// </summary>
        FULL_SCREEN,
        /// <summary>
        /// 所有目标（包括敌方和己方）。
        /// </summary>
        ALL_TARGETS
    }
    
    /// <summary>
    /// 技能动作类型。
    /// </summary>
    public enum SkillActionType
    {
        /// <summary>
        /// 无(0)。
        /// </summary>
        NONE,
        /// <summary>
        /// 投掷(1)。
        /// </summary>
        THROW,
        /// <summary>
        /// 射击(2)。
        /// </summary>
        SHOT,
        /// <summary>
        /// 其它(3)。
        /// </summary>
        OTHERS
    }
    
    /// <summary>
    /// 技能特效影响目标类型。
    /// </summary>
    public enum SkillEffectImpactTargetType
    {
        /// <summary>
        /// 无(0)。
        /// </summary>
        NONE,
        /// <summary>
        /// 单个目标(1)。
        /// </summary>
        EACH,
        /// <summary>
        /// 所有目标(2)。
        /// </summary>
        ALL
    }
    
    public enum SkillImpactEffectPosType
    {
        /// <summary>
        /// 无(0)。
        /// </summary>
        NONE,
        /// <summary>
        /// 头顶(1)。
        /// </summary>
        HEAD_TOP,
        /// <summary>
        /// 胸口(2)。
        /// </summary>
        CHEST,
        /// <summary>
        /// 脚下(3)。
        /// </summary>
        FOOT
    }
    
    
    public enum SkillBuffPosType
    {
        /// <summary>
        /// 无(0)。
        /// </summary>
        NONE,
        /// <summary>
        /// 头顶(1)。
        /// </summary>
        HEAD_TOP,
        /// <summary>
        /// 脚下(2)。
        /// </summary>
        FOOT,
        /// <summary>
        /// 周身(3)。
        /// </summary>
        BODY
    }
    
    
    /// <summary>
    /// 回合状态
    /// </summary>
    public enum BatRoundStageType
    {
        NONE,
        START,
        PROGRESS,
        END
    }
    
    /// <summary>
    /// 技能Buff状态
    /// </summary>
    public enum SkillBuffStateType
    {
        NONE,
        /// <summary>
        /// 添加(1)。
        /// </summary>
        ADD,
        /// <summary>
        /// 执行中(2)。
        /// </summary>
        ING,
        /// <summary>
        /// 删除(3)。
        /// </summary>
        REMOVE
    }
    
    public enum SkillTargetType
    {
        NONE,
        /// <summary>
        /// 敌方。
        /// </summary>
        ENEMY,
        /// <summary>
        /// 己方。
        /// </summary>
        OUR,
        /// <summary>
        /// 自己。
        /// </summary>
        MYSELF,
        /// <summary>
        /// 主将。
        /// </summary>
        LEADER,
        /// <summary>
        /// 宠物。
        /// </summary>
        PET,
        /// <summary>
        /// 敌方可捕捉单位。
        /// </summary>
        ENEMY_CAN_CATCH,
        /// <summary>
        /// 己方死亡单位。
        /// </summary>
        OUR_DEAD,
        /// <summary>
        /// 己方含死亡单位。
        /// </summary>
        OUR_ALL,
        /// <summary>
        /// 属于自己的所有单位。
        /// </summary>
        MY_OWN_ALL,
        /// <summary>
        /// 己方宠物。
        /// </summary>
        MY_OWN_PET,
        /// <summary>
        /// 敌方宠物。
        /// </summary>
        ENEMY_PET,
        /// <summary>
        /// 全体活的单位。
        /// </summary>
        ALL_NOT_DEAD
    }
    
    public enum SkillTargetRangeType
    {
        NONE,
        RANDOM,
        SINGLE,
        ALL,
        CROSS
    }

    public struct BatSkillID
    {
        public const int NORMAL_ATTACK = 1;
        public const int CATCH = 2;
        public const int DEFENSE = 3;
        public const int ESCAPE = 4;
        public const int USE_ITEM = 5;
        public const int SUMMON = 6;

        public static bool HasValue(int id)
        {
            return id == NORMAL_ATTACK ||
                   id == CATCH ||
                   id == DEFENSE || id == ESCAPE || id == USE_ITEM || id == SUMMON;
        }
    }

    public struct BatBuffID
    {
        /// <summary>
        /// 防御。
        /// </summary>
        public const int DEFENSE = 3;
        /// <summary>
        /// 混乱。
        /// </summary>
        public const int CHAOS = 18;
    }
}