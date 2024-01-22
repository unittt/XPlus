using System.Collections.Generic;
using GameScripts.RunTime.Model;
using GameScripts.RunTime.War;
using HT.Framework;

namespace GameScripts.RunTime.Magic.Command.Handler
{
    /// <summary>
    /// 播放动作
    /// </summary>
    public class PlayActionHandler: CmdHandlerBase<PlayAction>
    {

        private List<Warrior> _warriors = new();
        
        
        protected override void OnExecute()
        {
            //1. 获取执行者
            GetExecutors(Data.executor,_warriors);

            foreach (var warrior in _warriors)
            {
                if (ModelTools.IsCommonState(Data.action_name))
                {
                    // warrior.m_Actor
                    ActorPlay(warrior,Data.action_name, Data.action_time, Data.start_frame, Data.end_frame);
                }
                else if (warrior.PlayCombo(Data.action_name))
                {
                    warrior.SetComboHitEvent(Unit.CombHit);
                }
                else if (string.IsNullOrEmpty(Data.bak_action_name))
                {
                    ActorPlay(warrior, Data.bak_action_name);
                }
            }
        }

        private void ActorPlay(ActorEntity actor, string state,float actionTime = 0, int startFrame = 0, int endFrame = 0)
        {
            Log.Info($"播放动作{state}  {actionTime}    {startFrame}  {endFrame}");
            if (actionTime > 0)
            {
                if (startFrame > 0)
                {
                    actor.AdjustSpeedPlayInFrame(state, actionTime, startFrame, endFrame, SetCompleted);
                }
                else
                {
                    actor.AdjustSpeedPlay(state, actionTime,SetCompleted);
                }
            }
            else
            {
                if (startFrame > 0)
                {
                    actor.PlayInFrame(state, startFrame, endFrame, SetCompleted);
                }
                else
                {
                    actor.Play(state,0,1,SetCompleted);
                }
            }
        }
    }
}