using System.Collections.Generic;
using cfg.SkillModule;
using Cysharp.Threading.Tasks;
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
        
        public void FirstSpecityWar()
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
                map_id = 0,
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

            //5.回合2
            //6.回合3
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
                camp_id = 1,
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
                addPlayerWarrior.warrior.status.model_info.weapon = 21006;
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
            
            AddPartnerWarrior("许仙三魂",21011,2,2,2);
            AddPartnerWarrior("许仙七魄",21012,3,3,3);
            
            
            
            // 敌方========================================================================
            
            var addNpcWarrior = new GS2CWarAddNpcWarrior
            {
                camp_id = 2,
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
                camp_id = 1,
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
            
            
            //3.回合开始
            var gs2CWarBoutStart = new GS2CWarBoutStart
            {
                war_id = war_id,
                bout_id = 1,
                left_time = 30
            };
            NetWar.Current.GS2CWarBoutStart(gs2CWarBoutStart);
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