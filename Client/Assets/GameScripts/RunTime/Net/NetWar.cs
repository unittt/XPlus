using GameScripts.RunTime.War;
using HT.Framework;
using Pb.Mmo.Common;

namespace GameScripts.RunTime.Net
{
    public class NetWar : SingletonBase<NetWar>
    {

        /// <summary>
        /// 显示战场
        /// </summary>
        /// <param name="pbData"></param>
        public void GS2CShowWar(GS2CShowWar pbData)
        {

            if (NetManager.Current.IsProtoRecord())
            {
                
            }
            else
            {
                
                // WarManager.Current.m_IsClientRecord = false;
                // WarManager.Current.m_IsPlayRecord
            }
            
            WarManager.Current.StartWar(pbData);
        }

        
        /// <summary>
        /// 回合开始
        /// </summary>
        /// <param name="gs2CWarBoutStart"></param>
        public void GS2CWarBoutStart(GS2CWarBoutStart gs2CWarBoutStart)
        {
            if ( WarManager.Current.WarID != gs2CWarBoutStart.war_id)
            {
                return;
            }

            var boutStart = Main.m_ReferencePool.Spawn<BoutStartCmd>();
            boutStart.bout_id = gs2CWarBoutStart.bout_id;
            boutStart.left_time = gs2CWarBoutStart.left_time;
            // WarManager.Current.SetVaryCmd(null);
            WarManager.Current.InsertCmd(boutStart);
            WarManager.Current.m_ProtoBout = gs2CWarBoutStart.bout_id;
        }

        /// <summary>
        /// 回合结束
        /// </summary>
        /// <param name="boutEnd"></param>
        /// <exception cref="NotImplementedException"></exception>
        public void GS2CWarBoutEnd(GS2CWarBoutEnd boutEnd)
        {
            // WarManager.Current.SetVaryCmd(null);
            var boutEndCmd = Main.m_ReferencePool.Spawn<BoutEndCmd>();
            WarManager.Current.InsertCmd(boutEndCmd);
        }
        

        #region 添加角色
        /// <summary>
        /// 加入一个玩家
        /// </summary>
        /// <param name="addPlayerWarrior"></param>
        public void GS2CWarAddWarrior(GS2CWarAddPlayerWarrior addPlayerWarrior)
        {
          
            //如果战场编号不一致 return
            if ( WarManager.Current.WarID != addPlayerWarrior.war_id)
            {
                return;
            }
           
            //判断是不是当前玩家
            if (addPlayerWarrior.warrior.pid == WarManager.Current.GetHeroPid())
            {
                WarManager.Current.m_AllyCamp = addPlayerWarrior.camp_id;
                WarManager.Current.m_HeroWid = addPlayerWarrior.war_id;
            }
            
            //根据阵营 添加对应人数
            if ( WarManager.Current.m_AllyCamp == addPlayerWarrior.camp_id)
            {
                WarManager.Current.m_AllyPlayerCnt += 1;
            }
            else
            {
                WarManager.Current.m_EnemyPlayerCnt += 1;
            }

            //添加玩家指令
            var addWarriorCmd =  Main.m_ReferencePool.Spawn<AddWarriorCmd>();
            addWarriorCmd.BaseWarrior = addPlayerWarrior.warrior;
            addWarriorCmd.camp_id = addPlayerWarrior.camp_id;
            addWarriorCmd.is_summon = addPlayerWarrior.is_summon;
            //插入指令
            WarManager.Current.InsertCmd(addWarriorCmd);
        }
        
        public void GS2CWarAddNpcWarrior(GS2CWarAddNpcWarrior addNpcWarrior)
        {
            //添加玩家指令
            var addWarriorCmd =  Main.m_ReferencePool.Spawn<AddWarriorCmd>();
            addWarriorCmd.BaseWarrior = addNpcWarrior.warrior;
            addWarriorCmd.camp_id = addNpcWarrior.camp_id;
            addWarriorCmd.is_summon = addNpcWarrior.is_summon;

            //插入指令
            WarManager.Current.InsertCmd(addWarriorCmd);
        }

        public void GS2CWarAddPartnerWarrior(GS2CWarAddPartnerWarrior addPartnerWarrior)
        {
            //添加玩家指令
            var addWarriorCmd =  Main.m_ReferencePool.Spawn<AddWarriorCmd>();
            addWarriorCmd.BaseWarrior = addPartnerWarrior.warrior;
            addWarriorCmd.camp_id = addPartnerWarrior.camp_id;
            addWarriorCmd.is_summon = addPartnerWarrior.is_summon;

            //插入指令
            WarManager.Current.InsertCmd(addWarriorCmd);
        }
        #endregion
        
        /// <summary>
        /// 玩家进入战斗
        /// </summary>
        /// <param name="warEnter"></param>
        public void GS2CPlayerWarriorEnter(GS2CPlayerWarriorEnter warEnter)
        {
            //如果战场编号不一致 return
            if ( WarManager.Current.WarID != warEnter.war_id)
            {
                return;
            }

            if (warEnter.wid == WarManager.Current.m_HeroWid)
            {
                WarManager.Current.SetFightSummons(warEnter.sum_list);
            }
        }

        /// <summary>
        /// 释放技能
        /// </summary>
        /// <param name="bossMagic"></param>
        public void GS2CWarSkill(GS2CWarSkill bossMagic)
        {
            //如果战场编号不一致 return
            if ( WarManager.Current.WarID != bossMagic.war_id)
            {
                return;
            }

            var cmd = Main.m_ReferencePool.Spawn<MagicCmd>();
            cmd.atkid = bossMagic.action_wlist[0];
            cmd.vicid_list = bossMagic.select_wlist;
            //无视服务端的变量名skill_id， magic_id
            //客户端法术只有magic_id， magic_index
            cmd.magic_id = bossMagic.skill_id;
            cmd.magic_index = bossMagic.magic_id;

            // WarManager.Current.AddBoutMagicInfo(bossMagic.action_wlist[1], bossMagic.select_wlist, bossMagic.skill_id,
                // bossMagic.magic_id, cmd.ID);
            //插入指令
            WarManager.Current.InsertCmd(cmd);
        }

        /// <summary>
        /// 处理伤害
        /// </summary>
        /// <param name="gs2CWarDamage"></param>
        public void GS2CWarDamage(GS2CWarDamage gs2CWarDamage)
        {
            //如果战场编号不一致 return
            if ( WarManager.Current.WarID != gs2CWarDamage.war_id)
            {
                return;
            }
            var cmd = Main.m_ReferencePool.Spawn<WarDamageCmd>();
            cmd.wid = gs2CWarDamage.wid;
            cmd.type = gs2CWarDamage.type;
            cmd.damage = gs2CWarDamage.damage;
            cmd.iscrit = gs2CWarDamage.damage;
            WarManager.Current.InsertCmd(cmd);
        }

        /// <summary>
        /// 更新状态
        /// </summary>
        /// <param name="gs2CWarWarriorStatus"></param>
        public void GS2CWarWarriorStatus(GS2CWarWarriorStatus gs2CWarWarriorStatus)
        {
            //如果战场编号不一致 return
            if ( WarManager.Current.WarID != gs2CWarWarriorStatus.war_id)
            {
                return;
            }

            if (gs2CWarWarriorStatus.Status.cmd == 1)
            {
                if ( WarManager.Current.TryGetWarrior(gs2CWarWarriorStatus.wid,out var warrior))
                {
                    // warrior.SetOrderDone(true);
                }
               
            }

            if (gs2CWarWarriorStatus.wid == WarManager.Current.m_HeroWid && gs2CWarWarriorStatus.Status.is_auto)
            {
                //设置玩家为自动战斗
                // WarManager.Current.SetAutoWar(status.is_auto, false);
            }
            
            var cmd = Main.m_ReferencePool.Spawn<WarriorStatusCmd>();
            cmd.wid = gs2CWarWarriorStatus.wid;
            cmd.status = gs2CWarWarriorStatus.Status;
            WarManager.Current.InsertCmd(cmd);
        }

        /// <summary>
        /// 归位
        /// </summary>
        /// <param name="gs2CWarGoback"></param>
        public void GS2CWarGoback(GS2CWarGoback gs2CWarGoback)
        {
            //如果战场编号不一致 return
            if ( WarManager.Current.WarID != gs2CWarGoback.war_id)
            {
                return;
            }
            
            var cmd = Main.m_ReferencePool.Spawn<GoBackCmd>();
            cmd.wid_list = gs2CWarGoback.action_wid;
            cmd.wait = true;
            WarManager.Current.InsertCmd(cmd);
        }
    }
}