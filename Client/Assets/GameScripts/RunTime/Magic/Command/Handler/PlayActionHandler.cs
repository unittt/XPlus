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
        protected override void OnFill(PlayAction commandData)
        {
            //1. 获取执行者
            List<Warrior> warriors = new List<Warrior>();
            GetExecutors(commandData.executor,warriors);

            foreach (var warrior in warriors)
            {
                if (ModelTools.IsCommonState(commandData.action_name))
                {
                    // warrior.m_Actor
                    ActorPlay(warrior,commandData.action_name, commandData.action_time, commandData.start_frame, commandData.end_frame);
                }
                else if (warrior.PlayCombo(commandData.action_name))
                {
                    warrior.SetComboHitEvent(null);
                }
                else if (string.IsNullOrEmpty(commandData.bak_action_name))
                {
                    ActorPlay(warrior, commandData.bak_action_name);
                }
            }
        }

        private void ActorPlay(ActorEntity actor, string actionName,float actionTime = 0, int startFrame = 0, int endFrame = 0)
        {
            // if (actionTime > 0)
            // {
            //     if (startFrame > 0)
            //     {
            //         actor.AdjustSpeedPlayInFrame(sState, iTime, iStartFrame, iEndFrame);
            //     }
            //     else
            //     {
            //         actor.AdjustSpeedPlay(sState, iTime);
            //     }
            // }
            // else
            // {
            //     if (startFrame > 0)
            //     {
            //         actor.PlayInFrame(sState, iStartFrame, iEndFrame);
            //     }
            //     else
            //     {
            //         actor.Play(sState);
            //     }
            // }
        }
    }
}