using System.Collections.Generic;

namespace Pb.Mmo.Common
{

    public class GS2CShowWar
    {
        public int war_id = 1;
        public int war_type = 2;
        public int sky_war = 3;
        public int weather = 4;
        public int is_bosswar = 5;
        public string tollgate_group; // 关卡组
        public int tollgate_id = 7; // 关卡id
        public int barrage_show = 8; // 弹幕显示 0-不显示 1-显示名字+弹幕 2-显示弹幕
        public int barrage_send = 9; // 是否能发弹幕 0-不能 1-能
        public int map_id = 10; // 战斗场景
        public int x = 11; // 坐标x
        public int y = 12; // 坐标y
        public int sys_type = 13; // 系统玩法类型
    }


    public class GS2CWarResult
    {
        public int war_id = 1;
        public int bout_id = 2;
    }

    public class GS2CWarBoutStart
    {

        public int war_id = 1;
        public int bout_id = 2;
        public int left_time = 3;
    }

    public class GS2CWarBoutEnd
    {
        public int war_id = 1;
    }


    public class GS2CWarAddWarrior
    {
        public int war_id = 1;
        public int camp_id = 2;
        public int type = 3; /*1 player,2 npc,3 summon,4 partner,5 roplayer,6 rosummon,7 ropartner */

        public PlayerWarrior warrior;
        public NpcWarrior npcwarrior;
        public SumWarrior sumwarrior;
        public PartnerWarrior partnerwarrior;
        public RoPlayerWarrior roplayerwarrior;
        public RoPartnerWarrior ropartnerwarrior;
        public RoSummonWarrior rosummonwarrior;
        public int add_type = 11; //是否立即执行插入
        public int is_summon = 12; //战斗中召唤入场
    }

    public class GS2CWarDelWarrior
    {
        public int war_id = 1;
        public int wid = 2;
        public int type = 3;
        public int war_end = 4;
    }

    public class  GS2CWarCampFmtInfo {
        public int war_id = 1;         //战斗id
        public int fmt_id1 = 2;        //阵营1 阵法id
        public int fmt_grade1 = 3;     //阵营1 阵法等级
        public int fmt_id2 = 4;        //阵营2 阵法id
        public int fmt_grade2 = 5;     //阵营2 阵法等级
    }



    //玩家战士进入战斗
    public class GS2CPlayerWarriorEnter
    {
        public int war_id = 1;
        public int wid = 2;
        public List<int> sum_list;
    }
}