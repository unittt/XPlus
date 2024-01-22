using System.Collections.Generic;
using GameScripts.RunTime.War;

namespace GameScripts.RunTime.Magic.Command.Handler
{
    
    /// <summary>
    /// 处理模型颜色
    /// </summary>
    public sealed class ActorColorHandler:CmdHandlerBase<ActorColor>
    {
        private List<Warrior> _warriors;

        // protected override void OnFill(ActorColor commandData)
        // {
        //     GetExecutors( commandData.executor, _warriors);
        //     if (_warriors.Count == 0) return;
        //     
        //     var color = commandData.color.GetColor();
        //     foreach (var warrior in _warriors)
        //     {
        //         // warrior.m_FootshadowObj.SetActive(false);
        //         // if (commandData.fade_time > 0)
        //         // {
        //         //     
        //         // }
        //     }
        // }
    }
}