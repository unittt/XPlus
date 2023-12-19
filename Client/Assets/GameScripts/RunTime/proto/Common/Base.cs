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
        public int shape;
        public int scale ; /*缩放比例*/
        public int color; /*染色*/
        public int mutate_texture; /*变色贴图*/
        public int weapon; /*关联武器id*/
        public int adorn ; /*装饰*/
        public int figure ; /*造型id @model.key, 用以取代前面几项，前几项仅覆盖默认设置时使用（player不使用此项，仅npc等使用）*/
        public int horse; //坐骑
        public int isbianshen ; //是否变身 1.是 0.否
        public int ranse_clothes ; //外观染色
        public int ranse_hair; //头发染色
        public int ranse_pant; //头发染色
        public int ranse_summon ; //宠物染色
        public int shizhuang; //时装
        public int ranse_shizhuang; //时装染色
        public int fuhun; //武器附魂 1.是 0.否
        public int follow_spirit; // 跟随器灵
        public int show_wing; // 显示翅膀id
    }
    
    
    public class WarriorStatus
    {
        public string name;
        public int hp;
        public int mp;
        public int max_hp;
        public int max_mp;
        public int aura; //灵气
        /// <summary>
        /// 状态,1:活着,2:死亡
        /// </summary>
        public int status;
        public int auto_perform; //自动战斗招式
        public int is_auto; //是否自动战斗
        public int max_sp ; //最大怒气
        public int sp; //怒气
        public int item_use_cnt1 ; //使用3级药或酒
        public int item_use_cnt2 ; //使用2级药
        public int cmd; //是否下达指令
        public int school ; //门派
        public int grade ; //等级
        public string title; //称谓
        public int zhenqi; //真气
        
        public ModelInfo model_info;
    }

    
    /// <summary>
    /// 基础信息
    /// </summary>

    public  class BaseWarrior
    {
        /// <summary>
        /// 在房间中的唯一标识符
        /// </summary>
        public int wid;
        /// <summary>
        /// 战场上的位置
        /// </summary>
        public int pos;
        /// <summary>
        /// 状态
        /// </summary>
        public WarriorStatus status;
    }
    
    /// <summary>
    /// 玩家
    /// </summary>
    public class PlayerWarrior : BaseWarrior
    {
        /// <summary>
        /// 玩家的唯一标识符
        /// </summary>
        public int pid = 2;

        /// <summary>
        /// 是否为指挥
        /// </summary>
        public bool appoint;

        /// <summary>
        /// true 每回合清除 false 每回合不清除
        /// </summary>
        public bool appointop;

        public List<PerformUnit> pflist;
    }
    
    /// <summary>
    /// npc
    /// </summary>
    public class NpcWarrior :BaseWarrior
    {
        /// <summary>
        /// 特殊npc标志
        /// </summary>
        public int specail_id;
    }
    
    /// <summary>
    /// 召唤兽
    /// </summary>
    public class SumWarrior : BaseWarrior
    {
        /// <summary>
        /// 主人
        /// </summary>
        public int owner = 3; 
        /// <summary>
        /// 召唤兽唯一标识符
        /// </summary>
        public int sum_id = 4;
        
        public List<PerformUnit> pflist;
    }
    
    /// <summary>
    /// 伙伴
    /// </summary>
    public class PartnerWarrior : BaseWarrior
    {
        
        /// <summary>
        /// 玩家的唯一标识符
        /// </summary>
        public int pid = 2;
        
        public List<PerformUnit> pflist;
    }
}