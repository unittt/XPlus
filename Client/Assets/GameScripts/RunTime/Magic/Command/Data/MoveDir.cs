using System;
using GameScripts.RunTime.Utility.Selector;

namespace GameScripts.RunTime.Magic.Command
{
    [Serializable]
    [Command("移动位置(方向+速度)",13)]
    public class MoveDir:CommandData
    {
        [Argument("执行人")]
        [SelectHandler(typeof(SelectorHandler_Enum<ExecutorType>))]
        public ExecutorType executor;
        
        [Argument("方向")]
        [SelectHandler(typeof(SelectorHandler_Enum<MoveDirection>))]
        public MoveDirection dir;
        
        [Argument("速度")]
        public float speed;
        
        [Argument("时间")]
        public float move_time;
    }
}