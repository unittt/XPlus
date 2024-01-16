using System.Collections.Generic;
using GameScripts.RunTime.Model;
using GameScripts.RunTime.War;

namespace GameScripts.RunTime.Magic.Command.Handler
{
    /// <summary>
    /// 播放动作
    /// </summary>
    public class PlayActionHandler: CmdHandlerBase<PlayAction>
    {

        private List<Warrior> _warriors = new();

        protected override void OnFill(PlayAction commandData)
        {
            //1. 获取执行者
            GetExecutors(commandData.executor,_warriors);

            foreach (var warrior in _warriors)
            {
                if (ModelTools.IsCommonState(commandData.action_name))
                {
                    // warrior.m_Actor
                    ActorPlay(warrior,commandData.action_name, commandData.action_time, commandData.start_frame, commandData.end_frame);
                }
                else if (warrior.PlayCombo(commandData.action_name))
                {
                    warrior.SetComboHitEvent(MagicUnit.CombHit);
                }
                else if (string.IsNullOrEmpty(commandData.bak_action_name))
                {
                    ActorPlay(warrior, commandData.bak_action_name);
                }
            }
        }
        
        private void ActorPlay(ActorEntity actor, string state,float actionTime = 0, int startFrame = 0, int endFrame = 0)
        {
            if (actionTime > 0)
            {
                if (startFrame > 0)
                {
                    actor.AdjustSpeedPlayInFrame(state, actionTime, startFrame, endFrame);
                }
                else
                {
                    actor.AdjustSpeedPlay(state, actionTime);
                }
            }
            else
            {
                if (startFrame > 0)
                {
                    actor.PlayInFrame(state, startFrame, endFrame, null);
                }
                else
                {
                    actor.Play(state);
                }
            }
        }
    }
}