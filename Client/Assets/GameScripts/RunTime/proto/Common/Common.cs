namespace Pb.Mmo.Common
{

    public class PosInfo
    {
        public int v = 1;
        public int x = 2;
        public int y = 3;
        public int z = 4;
        public int face_x = 5;
        public int face_y = 6;
        public int face_z = 7;
    }


    public class ModelInfo
    {
        public int shape = 1;
        public int scale = 2; /*缩放比例*/
        public int color = 3; /*染色*/
        public int mutate_texture = 4; /*变色贴图*/
        public int weapon = 5; /*关联武器id*/
        public int adorn = 6; /*装饰*/
        public int figure = 7; /*造型id @model.key, 用以取代前面几项，前几项仅覆盖默认设置时使用（player不使用此项，仅npc等使用）*/
        public int horse = 8;//坐骑
        public int isbianshen = 9; //是否变身 1.是 0.否
        public int ranse_clothes = 10; //外观染色
        public int ranse_hair = 11; //头发染色
        public int ranse_pant = 12; //头发染色
        public int ranse_summon = 13; //宠物染色
        public int shizhuang = 14; //时装
        public int ranse_shizhuang = 15; //时装染色
        public int fuhun = 16; //武器附魂 1.是 0.否
        public int follow_spirit = 18; // 跟随器灵
        public int show_wing = 19; // 显示翅膀id
    }


    public class SimpleRole
    {
        public int pid = 1;
        public string name;
        public int grade = 3;
        public ModelInfo model_info;
        public int icon = 5;
        public int school = 6;
    }

    public class TitleInfo
    {
        public int tid = 1;
        public string name;
        public int achieve_time = 3;
    }

    public class TouxianInfo
    {
        public int tid = 1;
        public string name;
        public int level = 3;
        public int score = 4;
        public int school = 5;
    }

    public class SimplePlayer
    {
        public int pid = 1;
        public string name;
        public int grade = 3;
        public int school = 4;
        public int icon = 5;
    }


    public class SecondPropUnit
    {
        public int baseValue = 1; //基础属性，
        public int extra = 2; //附属属性，来自装备、道具等
        public int ratio = 3; //百分比，来自外部加成
        public string name; //属性名字(宠物单独的不需要这个字段)
    }

    public class EngageInfo
    {
        public int pid = 1;

        public string name;

        // public ItemInfo equip = 3;
        public int active = 4; //1 主动订婚
        public int etype = 5; //订婚类型
        public int status = 6; //
        public int marry_time = 7; // 
    }

    public class Role
    {
        public string mask;
        public int grade = 2;
        public string name;
        public string title_list;
        public int goldcoin = 5;
        public int gold = 6;
        public int silver = 7;
        public int exp = 8;
        public int chubeiexp = 9;
        public int max_hp = 10;
        public int max_mp = 11;
        public int hp = 12;
        public int mp = 13;
        public int energy = 14;
        public int physique = 15;
        public int strength = 16;
        public int magic = 17;
        public int endurance = 18;
        public int agility = 19;
        public int phy_attack = 20;
        public int phy_defense = 21;
        public int mag_attack = 22;
        public int mag_defense = 23;
        public int cure_power = 24;
        public int speed = 25;
        public int seal_ratio = 26;
        public int res_seal_ratio = 27;
        public int phy_critical_ratio = 28;
        public int res_phy_critical_ratio = 29;
        public int mag_critical_ratio = 30;
        public int res_mag_critical_ratio = 31;
        public ModelInfo model_info;
        public int school = 33;
        public int point = 34;
        public int critical_multiple = 35; //暴击倍数
        public int gold_over = 36; //金币溢出

        public int silver_over = 37; //银币溢出

        // repeated FollowerInfo followers = 38;               //跟随信息
        public int sex = 39; //性别
        public TitleInfo title_info; //称谓信息
        public int upvote_amount = 41; //点赞数
        public int achieve = 42; //成就
        public int score = 43; //评分
        public string position; //地理位置
        public int position_hide = 45; //0-隐藏地理位置，1-显示位置
        public int rename = 46; //改名次数
        public int org_id = 47; //帮派ID
        public int org_status = 48; //帮派状态
        public int org_offer = 49; //帮派offer
        public int skill_point = 50; //技能点数量
        public string orgname;
        public int icon = 52;
        public int show_id = 53; //靓号ID
        public int org_pos = 54;
        public int sp = 55; //怒气
        public int max_sp = 56; //怒气上限
        public ModelInfo model_info_changed; //变身后造型
        public int rplgoldcoin = 58; //系统绑定元宝
        public int fly_height = 59; //飞行高度
        public int wuxun = 60; //武勋值
        public int jjcpoint = 61; //竞技场积分
        public int vigor = 62; //精气值
        public int leaderpoint = 63; //功勋值
        public int xiayipoint = 64; //狭义值
        public int summonpoint = 65; //宠物合成积分
        public int storypoint = 66; //剧情技能点 
        public TitleInfo title_info_changed; //变身后的称谓信息
        public SecondPropUnit prop_info; //人物次级计算属性
        public EngageInfo engage_info; //订婚目标
        public int gold_owe = 70; //金币溢出
        public int silver_owe = 71; //银币溢出
        public int goldcoin_owe = 72; //元宝欠费
        public int truegoldcoin_owe = 73; //非绑定元宝欠费
        public int chumopoint = 74; //除魔值
    }
}