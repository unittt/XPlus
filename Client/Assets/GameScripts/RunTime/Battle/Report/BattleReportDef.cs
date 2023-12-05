namespace GameScripts.RunTime.Battle.Report
{
    public class BattleReportDef
    {
        /// <summary>
        /// 攻击方属性
        /// </summary>
        public const int ATTACKERS = 1;

        /// <summary>
        /// 防守方属性
        /// </summary>
        public const int DEFENDERS = 2;

        /// <summary>
        /// 攻击方附加属性
        /// </summary>
        public const int ATTACKERS_ADD = 3;

        /// <summary>
        /// 防守方附加属性
        /// </summary>
        public const int DEFENDERS_ADD = 4;

        /// <summary>
        /// 对应FightUnitType
        /// </summary>
        public const int FIGHTUNIT_TYPE = 100;

        /// <summary>
        /// 战斗对象唯一Id
        /// </summary>
        public const int FIGHTUNIT_ID = 101;

        /// <summary>
        /// 模板Id
        /// </summary>
        public const int FIGHTUNIT_TPLID = 102;

        /// <summary>
        /// 位置，从1开始
        /// </summary>
        public const int FIGHTUNIT_POSITION = 103;

        /// <summary>
        /// 对应PetAttackType
        /// </summary>
        public const int FIGHTUNIT_ATTACKTYPE = 104;

        /// <summary>
        /// 武将等级
        /// </summary>
        public const int FIGHTUNIT_LEVEL = 105;

        /// <summary>
        /// 血量
        /// </summary>
        public const int FIGHTUNIT_HP = 106;

        /// <summary>
        /// 血量上限
        /// </summary>
        public const int FIGHTUNIT_HP_MAX = 107;

        /// <summary>
        /// 魔法
        /// </summary>
        public const int FIGHTUNIT_MP = 108;

        /// <summary>
        /// 魔法上限
        /// </summary>
        public const int FIGHTUNIT_MP_MAX = 109;

        /// <summary>
        /// 怒气
        /// </summary>
        public const int FIGHTUNIT_SP = 110;

        /// <summary>
        /// 怒气上限
        /// </summary>
        public const int FIGHTUNIT_SP_MAX = 111;

        /// <summary>
        /// 状态
        /// </summary>
        public const int FIGHTUNIT_STATUS = 112;

        /// <summary>
        /// 是否可被捕捉的对象
        /// </summary>
        public const int FIGHTUNIT_CAN_BE_CAUGHT = 113;

        /// <summary>
        /// 名字
        /// </summary>
        public const int FIGHTUNIT_NAME = 114;

        /// <summary>
        /// 武将唯一Id，只有武将有，怪物等为0
        /// </summary>
        public const int FIGHTUNIT_PETUUID = 115;

        /// <summary>
        /// 所属玩家角色Id
        /// </summary>
        public const int FIGHTUNIT_OWERID = 116;

        /// <summary>
        /// 变异类型。（0:未变异，1:已变异。
        /// </summary>
        public const int FIGHTUNIT_GENETYPE = 117;

        /// <summary>
        /// 主将武器模板Id
        /// </summary>
        public const int FIGHTUNIT_LEADER_WEAPONID = 118;

        /// <summary>
        /// 战斗开始
        /// </summary>
        public const int BATTLE_START = 200;

        /// <summary>
        /// 战斗结束
        /// </summary>
        public const int BATTLE_END = 201;

        /// <summary>
        /// 战斗结果，对应BattleResult
        /// </summary>
        public const int BATTLE_RESULT = 202;

        /// <summary>
        /// 每轮战斗
        /// </summary>
        public const int BATTLE_ROUND = 203;

        /// <summary>
        /// 错误
        /// </summary>
        public const int BATTLE_ERROR = 204;

        /// <summary>
        /// 战斗，行动开始
        /// </summary>
        public const int BATTLE_ACTION_START = 205;

        /// <summary>
        /// 战斗，行动执行
        /// </summary>
        public const int BATTLE_ACTION_EXECUTE = 206;

        /// <summary>
        /// 战斗，行动防御阶段
        /// </summary>
        public const int BATTLE_ACTION_DEFENCE = 207;

        /// <summary>
        /// 战斗，行动调整阶段
        /// </summary>
        public const int BATTLE_ACTION_ADJUST = 208;

        /// <summary>
        /// 战斗，行动结束
        /// </summary>
        public const int BATTLE_ACTION_END = 209;

        /// <summary>
        /// 战斗，一轮开始
        /// </summary>
        public const int BATTLE_ROUND_START = 210;

        /// <summary>
        /// 战斗，一轮过程中
        /// </summary>
        public const int BATTLE_ROUND_IN_PROGRESS = 211;

        /// <summary>
        /// 战斗，一轮结束
        /// </summary>
        public const int BATTLE_ROUND_END = 212;

        /// <summary>
        /// 战斗，该轮的轮数
        /// </summary>
        public const int BATTLE_ROUND_NUM = 213;

        /// <summary>
        /// 战斗，剩余嗑药次数
        /// </summary>
        public const int BATTLE_LEFT_USEDRUGS_NUM = 214;

        /// <summary>
        /// 战斗，战报播放速度
        /// </summary>
        public const int BATTLE_SPEED = 215;

        /// <summary>
        /// 技能效果的主人Id
        /// </summary>
        public const int RECORD_CONTENT_OWNER = 301;

        /**技能Id*/
        public const int RECORD_CONTENT_SKILLID = 302;

        /**效果战报列表*/
        public const int RECORD_CONTENT_ITEMLIST = 303;

        /**技能效果列表*/
        public const int RECORD_CONTENT_SKILL_EFFECT_LIST = 304;

        /**效果战报列表-效果是否仙符列表*/
        public const int RECORD_CONTENT_SKILL_EFFECT_ISEMBED_LIST = 305;

        /**技能是否是连击*/
        public const int RECORD_CONTENT_SKILL_IS_COMBO = 306;

        /**一条数据的对象Id*/
        public const int REPORT_ITEM_TARGET = 401;

        /**血量变化*/
        public const int REPORT_ITEM_HP = 402;

        /**是否闪避*/
        public const int REPORT_ITEM_DODGY = 403;

        /**是否暴击*/
        public const int REPORT_ITEM_FATAL = 404;

        /**魔法变化*/
        public const int REPORT_ITEM_MP = 405;

        /**怒气变化*/
        public const int REPORT_ITEM_SP = 406;

        /**速度变化*/
        public const int REPORT_ITEM_SPEED = 407;

        /**物理攻击变化*/
        public const int REPORT_ITEM_PHYSICAL_ATTACK = 408;

        /**法术攻击变化*/
        public const int REPORT_ITEM_MAGIC_ATTACK = 409;

        /**物理防御变化*/
        public const int REPORT_ITEM_PHYSICAL_ARMOR = 410;

        /**魔法防御变化*/
        public const int REPORT_ITEM_MAGIC_ARMOR = 411;

        /**是否反击*/
        public const int REPORT_ITEM_DEFENCE_ATTACK = 412;

        /**是否连击*/
        public const int REPORT_ITEM_DOUBLE_ATTACK = 413;

        /**是否被捕捉了*/
        public const int REPORT_ITEM_BE_CAUGHT = 414;

        /**物理暴击变化*/
        public const int REPORT_ITEM_PHYSICAL_CRIT = 415;

        /**法术暴击变化*/
        public const int REPORT_ITEM_MAGIC_CRIT = 416;

        /**复活*/
        public const int REPORT_ITEM_REVIVE = 417;

        /**逃跑*/
        public const int REPORT_ITEM_ESCAPE = 418;

        /**击飞*/
        public const int REPORT_ITEM_DEAD_FLY = 419;

        /**嗑药*/
        public const int REPORT_ITEM_USE_DRUGS = 420;

        /** 寿命变化 */
        public const int REPORT_ITEM_LIFE = 421;

        /** 不冒泡的标识 */
        public const int REPORT_ITEM_NO_POP = 422;

        /** 召唤宠物 */
        public const int REPORT_ITEM_SUMMON_PET = 423;

        /** 召唤宠物结果 */
        public const int REPORT_ITEM_SUMMON_PET_RESULT = 424;

        /** 侠义之心标识Id*/
        public const int REPORT_ITEM_CHIVALRIC_ID = 430;

        /** 侠义之心标识是否清除*/
        public const int REPORT_ITEM_CHIVALRIC = 431;

        /**buffId in status*/
        public const int REPORT_ITEM_BUFF = 500;

        /**buffId*/
        public const int REPORT_ITEM_BUFF_ID = 501;

        /**buff状态，对应BuffState*/
        public const int REPORT_ITEM_BUFF_STATE = 502;

        /**buff剩余回合数*/
        public const int REPORT_ITEM_BUFF_LEFT = 503;

        /**buff唯一Id*/
        public const int REPORT_ITEM_BUFF_UUID = 504;

        /** 战报附加结果 */
        //public const int BATTLE_REPORT_ADDITION = 900;
        /** 战报附加结果的奖励信息 */
        //public const int BATTLE_REPORT_ADDITION_REWARD = 901;
        /** 战报附加结果的结果描述 */
        //public const int BATTLE_REPORT_ADDITION_RESULT_DESC = 902;
        /** 战报附加结果的奖励信息-货币类型 */
        //public const int BATTLE_REPORT_ADDITION_REWARD_CURRENCY_ID = 903;
        /** 战报附加结果的奖励信息-货币数量 */
        //public const int BATTLE_REPORT_ADDITION_REWARD_CURRENCY_AMOUNT = 904;
        /** 战报附加结果的奖励信息-道具模板Id */
        //public const int BATTLE_REPORT_ADDITION_REWARD_ITEM_ID = 905;
        /** 战报附加结果的奖励信息-道具数量 */
        //public const int BATTLE_REPORT_ADDITION_REWARD_ITEM_COUNT = 908;
        /** 战报附加结果的奖励信息-经验值 */
        //public const int BATTLE_REPORT_ADDITION_REWARD_EXP = 910;
        /** 战报附加结果的奖励信息-货币 */
        //public const int BATTLE_REPORT_ADDITION_REWARD_CURRENCY = 911;
        /** 战报附加结果的奖励信息-道具 */
        //public const int BATTLE_REPORT_ADDITION_REWARD_ITEM = 912;
    }
}