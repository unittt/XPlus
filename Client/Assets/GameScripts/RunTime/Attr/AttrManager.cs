using System.Collections.Generic;
using cfg.SystemModule;
using HT.Framework;
using Pb.Mmo.Common;
using UnityEngine;

namespace GameScripts.RunTime.Attr
{
    public class AttrManager : SingletonBase<AttrManager>
    {
        public int pid = 0;
        public int grade = 0;
        public string name = "";
        public List<int> title_list;
        /// <summary>
        /// 元宝
        /// </summary>
        public int goldcoin = 0;
        /// <summary>
        /// 金币
        /// </summary>
        public int gold = 0;
        /// <summary>
        /// 银币
        /// </summary>
        public int silver = 0;

        /// <summary>
        /// 绑定元宝
        /// </summary>
        public int rplgoldcoin = 0;

        public int exp = 0;
        public int chubeiexp = 0;

        public int max_hp = 0;
        public int max_mp = 0;
        public int hp = 0;
        public int mp = 0;
        public int energy = 0;
        public int physique = 0;
        public int strength = 0;
        public int magic = 0;
        public int endurance = 0;
        public int agility = 0;
        public int phy_attack = 0;
        public int phy_defense = 0;
        public int mag_attack = 0;
        public int mag_defense = 0;
        public int cure_power = 0;
        /// <summary>
        ///  玩家评分
        /// </summary>
        public int score = 0 ;

        
        public int speed = 0;
        public int seal_ratio = 0;
        public int res_seal_ratio = 0;
        public int phy_critical_ratio = 0;
        public int res_phy_critical_ratio = 0;
        public int mag_critical_ratio = 0;
        public int res_mag_critical_ratio = 0;
        public ModelInfo model_info;
        public ESchoolType? school;
        public int point = 0;
        public int activepoint = 0;
        public int sex = 0;
        public int server_grade = 0;
        public int days = 0;
        /// <summary>
        /// 跟随宠物列表
        /// </summary>
        public List<int> followers;

        /// <summary>
        /// 默认选择的方案
        /// </summary>
        public int g_SelectedPlan = 1;

        public int race = 0;
        public List<int> upvoteInfo;
        public int org_id = 0;
        public int org_status = 0;
        /// <summary>
        /// 当前帮贡
        /// </summary>
        public int org_offer = 0;

        /// <summary>
        /// 技能点数
        /// </summary>
        public int skill_point = 0;

        public string orgname = "";
        
        public int icon = 1110;
        public bool show_id;
        public List<int> org_skill;
        public int m_BadgeInfo;
        /// <summary>
        /// --帮派职位
        /// </summary>
        public int org_pos = 0;

        /// <summary>
        /// 全身颜色
        /// </summary>
        public Color color = Color.white;

        public int roletype = 0;
        public int sp = 0;
        public int max_sp = 0;
        /// <summary>
        /// --帮派竞赛状态 1005保护
        /// </summary>
        public int orgmatch_state = 0;

        public int vigor = 0;
        public int storypoint = 0;
        /// <summary>
        ///  画舫灯谜临时称谓
        /// </summary>
        public int title_info_changed;

        public int engageInfo;
        public bool makeDragAuto = true;

        public int m_AssistExp = 0;
        public int m_MaxAssistExp = 0;
    }
}