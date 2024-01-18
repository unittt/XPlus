using System.Collections.Generic;
using cfg.SkillModule;
using GameScripts.RunTime.Attr;
using GameScripts.RunTime.Net;
using HT.Framework;
using Pb.Mmo.Common;

namespace GameScripts.RunTime.War
{
    /// <summary>
    /// 模拟战斗
    /// </summary>
    public class WarSimulate
    {
        private int war_id = 1;
        private int war_pid = 1;
        /// <summary>
        /// 近战施法
        /// </summary>
        private List<int> meleeMagic = new() {1101,1102,1104,1105,1106,1107,1601,1602,1603,1604,1606,1607,7301,7302,7303,8301,8302,8303,8101,8102,8103,8104,8201,8202,8203,101};

        public void FirstSpecifyWar()
        {
            war_id = AttrManager.Current.pid;
            
            //1.进入战斗
            var gs2CShowWar = new GS2CShowWar
            {
                war_id = war_id,
                war_type = "Guide1",
                is_bosswar = true,
                sky_war = 0,
                weather = 2,
                map_id = 1010,
                x = 0,
                y = 0
            };
            NetWar.Current.GS2CShowWar(gs2CShowWar);

            //2.加人
            AddWarriors();
       
            //3.回合开始
            var gs2CWarBoutStart = new GS2CWarBoutStart
            {
                war_id = war_id,
                bout_id = 1,
                left_time = 30
            };
            NetWar.Current.GS2CWarBoutStart(gs2CWarBoutStart);
            
            //4.回合1
            Bout0();
            //5.回合2
            Bout1();
            //6.回合3
            // Bout2();
        }
        
        void ApplyFormula(string formula)
        {
            // Example formula: "max_hp=level*9"
            string[] parts = formula.Split('=');
            string attribute = parts[0];
            string expression = parts[1];

            int level = 1; // Example level, this would come from your character data
            int result = EvaluateExpression(expression, level);
        }
        
        int EvaluateExpression(string expression, int level)
        {
            // Simple evaluation logic, expand as needed
            // For example, replace 'level' with actual level value and evaluate expression
            expression = expression.Replace("level", level.ToString());
            return (int)new System.Data.DataTable().Compute(expression, null);
        }
        
        
        
        /// <summary>
        /// 加人
        /// </summary>
        private void AddWarriors()
        {
            // 己方========================================================================
            // 主角
            // 获得全技能
            var pfList = new List<PerformUnit>();
            
            if (AttrManager.Current.school.HasValue)
            {
                var schoolSkillList = new List<SchoolActiveSkill>();
                //获得当前门派的索引技能
                foreach (var schoolActiveSkill in TableGlobal.Instance.TbSchoolActiveSkill.DataList)
                {
                    if (schoolActiveSkill.School == AttrManager.Current.school)
                    {
                        schoolSkillList.Add(schoolActiveSkill);
                    }
                }
                //排序
                schoolSkillList.Sort((x,y)=>x.SortOrder.CompareTo(y.SortOrder));

                foreach (var schoolActiveSkill in schoolSkillList)
                {
                    var performUnit = new PerformUnit();
                    performUnit.pf_id = schoolActiveSkill.Id;
                    pfList.Add(performUnit);
                }
            }
            else
            { 
                var performUnit = new PerformUnit();
                performUnit.pf_id = 1101;
                pfList.Add(performUnit);
            }


            var addPlayerWarrior = new GS2CWarAddPlayerWarrior
            {
                war_id = war_id,
                camp_id = 0,
                warrior = new PlayerWarrior
                {
                    wid = 1,
                    pid = war_pid,
                    pos = 1,
                    appoint = true,
                    pflist = pfList,
                    status = new WarriorStatus
                    {
                        auto_perform = pfList[0].pf_id,
                        name = AttrManager.Current.name,
                        hp = 12000,
                        max_hp = 12000,
                        mp = 200,
                        max_mp = 200,
                        sp = 30,
                        max_sp = 150,
                        status = 1,
                        model_info = new ModelInfo()
                    }
                }
            };

            if ( AttrManager.Current.model_info is null)
            {
                addPlayerWarrior.warrior.status.model_info.figure = 1110;
                addPlayerWarrior.warrior.status.model_info.shape = 1110;
                addPlayerWarrior.warrior.status.model_info.weapon = 1;
            }
            else
            {
                addPlayerWarrior.warrior.status.model_info.figure = AttrManager.Current.model_info.figure;
                addPlayerWarrior.warrior.status.model_info.shape = AttrManager.Current.model_info.shape;
                addPlayerWarrior.warrior.status.model_info.weapon = AttrManager.Current.model_info.weapon;
            }
            
            NetWar.Current.GS2CWarAddWarrior(addPlayerWarrior);
            
            //玩家进入战斗
            var warriorEnter = new GS2CPlayerWarriorEnter();
            warriorEnter.war_id = war_id;
            warriorEnter.wid = 1;
            NetWar.Current.GS2CPlayerWarriorEnter(warriorEnter);
            
        
            AddPartnerWarrior("许仙三魂",3106,2,2,2);
            AddPartnerWarrior("许仙七魄",3106,3,3,3);
            
            // 敌方========================================================================
            
            var addNpcWarrior = new GS2CWarAddNpcWarrior
            {
                camp_id = 1,
                war_id = war_id,
                warrior = new NpcWarrior
                {
                    wid = 16,
                    pos = 1,
                    status = new WarriorStatus
                    {
                        name = "九幽魔帝",
                        hp = 14000,
                        max_hp = 14000,
                        mp = 500,
                        max_mp = 500,
                        status = 1,
                        model_info = new ModelInfo { figure = 6101, shape = 6101 }
                    }
                }
            };
            NetWar.Current.GS2CWarAddNpcWarrior(addNpcWarrior);
        }


        /// <summary>
        /// 添加伙伴
        /// </summary>
        /// <param name="name">名称</param>
        /// <param name="figure">角色</param>
        private void AddPartnerWarrior(string name, int figure, int wid, int pid, int pos)
        {
            var addPartnerWarrior = new GS2CWarAddPartnerWarrior
            {
                camp_id = 0,
                war_id = war_id,
                warrior = new PartnerWarrior
                {
                    wid = wid,
                    pid = pid,
                    pos = pos,
                    pflist = new List<PerformUnit>
                    {
                        new() { cd = 7501 },
                        new() { cd = 7502 }
                    },
                    status = new WarriorStatus
                    {
                        name = name,
                        hp = 12000,
                        max_hp = 12000,
                        mp = 200,
                        max_mp = 200,
                        sp = 30,
                        max_sp = 150,
                        status = 1,
                        model_info = new ModelInfo
                        {
                            figure = figure,
                            shape = figure
                        }
                    }
                }
            };

            NetWar.Current.GS2CWarAddPartnerWarrior(addPartnerWarrior);
        }


        /// <summary>
        /// 回合1
        /// </summary>
        private void Bout0()
        {
            var paopao1 = new GS2CWarriorSpeak
            {
                war_id = war_id,
                wid = 16,
                content = "七魄被打散，只会失去七情六欲，不会死。"
            };
            GS2CWarPaopao(paopao1);
            
            var paopao2 = new GS2CWarriorSpeak
            {
                war_id = war_id,
                wid = 2,
                content = "那也不行啊，没有七情六欲岂不是跟木头人一样？"
            };
            GS2CWarPaopao(paopao2);
            
            var paopao3 = new GS2CWarriorSpeak
            {
                war_id = war_id,
                wid = 16,
                content = "本座是在陈述事实，不是跟你讨价还价！"
            };
            GS2CWarPaopao(paopao3);


            var boutEnd = new GS2CWarBoutEnd
            {
                war_id = war_id
            };
            NetWar.Current.GS2CWarBoutEnd(boutEnd);
        }

        private void Bout1()
        {
            
            //3.回合开始
            var gs2CWarBoutStart = new GS2CWarBoutStart
            {
                war_id = war_id,
                bout_id = 1,
                left_time = 30
            };
            
            //回合开始
            NetWar.Current.GS2CWarBoutStart(gs2CWarBoutStart);
            
            //boss
            // 技能攻击
            var skillID = 101;

            var bossMagic = new GS2CWarSkill
            {
                war_id = war_id,
                skill_id = skillID,
                magic_id = 1,
                action_wlist = new List<int> { 16 },
                select_wlist = new List<int> { 1 }
            };
            NetWar.Current.GS2CWarSkill(bossMagic);
            
            //受击
            var allyDamageList = new List<int> { -4000 };

            for (var i = 0; i < allyDamageList.Count; i++)
            {
                var gs2CWarDamage = new GS2CWarDamage
                {
                    war_id = war_id,
                    wid = i + 1,
                    type = 0,
                    damage = allyDamageList[i],
                    isCrt = true
                };
                NetWar.Current.GS2CWarDamage(gs2CWarDamage);
            }
            
            //状态
            var allyStatusList = new List<int> { 8000 };
            for (var i = 0; i < allyStatusList.Count; i++)
            {
                var gs2CWarWarriorStatus = new GS2CWarWarriorStatus
                {
                    war_id = war_id,
                    wid = i + 1,
                    type = 2,
                  
                    Status =  new WarriorStatus
                    {
                        hp =allyStatusList[i],
                        sp = 45
                    }
                };
                NetWar.Current.GS2CWarWarriorStatus(gs2CWarWarriorStatus);
            }

            if (meleeMagic.Contains(skillID))
            {
                var gs2CWarGoBack = new GS2CWarGoback
                {
                    war_id = war_id,
                    action_wid = new List<int>(){1}
                };
                NetWar.Current.GS2CWarGoback(gs2CWarGoBack);
            }
            
            //许仙三魂攻击
            skillID = 1301;
            var partner1Magic = new GS2CWarSkill
            {
                war_id = war_id,
                skill_id = skillID,
                magic_id = 1,
                action_wlist = new List<int> { 2 },
                select_wlist = new List<int> { 16 }
            };
            NetWar.Current.GS2CWarSkill(partner1Magic);
            
            //受击
            var enemyDamageList = new List<int> { -2233 };

            for (var i = 0; i < enemyDamageList.Count; i++)
            {
                var gs2CWarDamage = new GS2CWarDamage
                {
                    war_id = war_id,
                    wid = i + 1,
                    type = 0,
                    damage = enemyDamageList[i],
                    isCrt = true
                };
                NetWar.Current.GS2CWarDamage(gs2CWarDamage);
            }
            
            
            //状态
            var enemyStatusList = new List<int> { 11767 };
            for (var i = 0; i < enemyStatusList.Count; i++)
            {
                var gs2CWarWarriorStatus = new GS2CWarWarriorStatus
                {
                    war_id = war_id,
                    wid = 16 + i,
                    type = 2,
                  
                    Status =  new WarriorStatus
                    {
                        hp = enemyStatusList[i],
                        sp = 45
                    }
                };
                NetWar.Current.GS2CWarWarriorStatus(gs2CWarWarriorStatus);
            }
            
            //如果为近战攻击
            if (meleeMagic.Contains(skillID))
            {
                NetWar.Current.GS2CWarGoback(new GS2CWarGoback{war_id = war_id, action_wid = {1}});
            }
            
            //回合结束
            var boutEnd = new GS2CWarBoutEnd
            {
                war_id = war_id
            };
            NetWar.Current.GS2CWarBoutEnd(boutEnd);
        }
        
        
        private void GS2CWarPaopao(GS2CWarriorSpeak gs2CWarriorSpeak)
        {
            var warPaoPao = Main.m_ReferencePool.Spawn<WarPaoPao>();
            warPaoPao.wid = gs2CWarriorSpeak.wid;
            warPaoPao.content = gs2CWarriorSpeak.content;
            WarManager.Current.InsertCmd(warPaoPao);
        }
    }
}