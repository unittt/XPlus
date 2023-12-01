using System.Collections.Generic;

namespace Pb.Mmo.Common
{
    public class PerformUnit
    {
        public int pf_id = 1; //招式id
        public int cd = 2; //cd结束回合数
    }

    
    public class BuffAttr
    {
        public string key;
        public int value;
    }

    public class BuffUnit
    {
        public int buff_id = 3;
        public int bout = 4;
        public List<BuffAttr>  attrlist;
    }

    public class StatusBuffUnit
    {
        public int status_id = 1;
        public List<BuffAttr> attrlist;
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
        public int horse = 8; //坐骑
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
    
    
    public class WarriorStatus
    {
        public string mask;
        public int hp = 2;
        public int mp = 3;
        public int max_hp = 4;
        public int max_mp = 5;
        public ModelInfo model_info;
        public string name;
        public int aura = 8; //灵气
        public int status = 9; //状态,1:活着,2:死亡
        public int auto_perform = 10; //自动战斗招式
        public int is_auto = 11; //是否自动战斗
        public int max_sp = 12; //最大怒气
        public int sp = 13; //怒气
        public int item_use_cnt1 = 14; //使用3级药或酒
        public int item_use_cnt2 = 15; //使用2级药
        public int cmd = 16; //是否下达指令
        public int school = 17; //门派
        public int grade = 18; //等级
        public string title; //称谓
        public int zhenqi = 20; //真气
    }
    
    
    public class PlayerWarrior
    {
        public int wid = 1;
        public int pid = 2;
        public int pos = 3;
        public PerformUnit pflist;
        public WarriorStatus status;
        public int appoint = 6; //1.指挥 　0.非指挥
        public int appointop = 7; //1.每回合清除 　0.每回合不清除
        public BuffUnit buff_list; //buff列表
        public List<StatusBuffUnit> status_list; //战斗状态列表
    }



    public class NpcWarrior
    {
        public int wid = 1;
        public int pos = 2;
        public WarriorStatus status;
        public List<BuffUnit> buff_list; //buff列表
        public int specail_id = 5; //特殊npc标志
        public List<StatusBuffUnit> status_list; //战斗状态列表
    }

    public class SumWarrior
    {
        public int wid = 1;
        public int pos = 2;
        public int owner = 3; //主人
        public int sum_id = 4;
        public List<PerformUnit> pflist;
        public WarriorStatus status;
        public List<BuffUnit> buff_list; //buff列表
        public List<StatusBuffUnit> status_list; //战斗状态列表
    }


    public class PartnerWarrior
    {
        public int wid = 1;
        public int pid = 2;
        public int pos = 3;
        public List<PerformUnit> pflist;
        public WarriorStatus status;
        public List<BuffUnit> buff_list; //buff列表
        public List<StatusBuffUnit> status_list; //战斗状态列表
    }


    public class RoPlayerWarrior
    {
        public int wid = 1;
        public int pos = 2;
        public WarriorStatus status;
        public List<BuffUnit> buff_list; //buff列表
        public List<StatusBuffUnit> status_list; //战斗状态列表
    }

    public class RoPartnerWarrior
    {
        public int wid = 1;
        public int pos = 2;
        public WarriorStatus status;
        public List<BuffUnit> buff_list; //buff列表
        public List<StatusBuffUnit> status_list; //战斗状态列表
    }

    public class RoSummonWarrior
    {
        public int wid = 1;
        public int pos = 2;
        public WarriorStatus status;
        public List<BuffUnit> buff_list; //buff列表
        public List<StatusBuffUnit> status_list; //战斗状态列表
    }
}