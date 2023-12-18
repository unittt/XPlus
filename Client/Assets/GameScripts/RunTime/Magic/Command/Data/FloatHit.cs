using System;
using GameScripts.RunTime.Utility.Selector;

namespace GameScripts.RunTime.Magic.Command
{
    
    [Serializable]
    [Command("浮空", 99)]
    public class FloatHit:CommandData
    {
        [Argument("执行人")]
        [SelectHandler(typeof(SelectorHandler_Enum<ExecutorType>))]
        public ExecutorType executor;

        [Argument("upFloat速度")] public float up_speed = 10;

        [Argument("upFloat时间")] public float up_time = 0.2f;

        [Argument("hitFloat速度")] public float hit_speed = 8;

        [Argument("hitFloat时间")] public float hit_time = 0.2f;

        [Argument("落地时间")] public float down_time = 1;

        [Argument("躺地时间")] public float lie_time = 0.5f;
    }
}