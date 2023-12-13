using System;
using GameScripts.RunTime.Utility.Selector;

namespace GameScripts.RunTime.Magic.Command
{
    
    [Command("模型颜色", 91)]
    [Serializable]
    public sealed class ActorColor:CommandBase
    {
        [Argument("执行人")]
        [SelectHandler(typeof(SelectorHandler_Enum<ExecutorType>))]
        public ExecutorType executor;
        
        [Argument("颜色")]
        public ComplexColor color;
        
        [Argument("持续时间(?)")]
        public float alive_time;
        
        [Argument("渐变时间(?)")]
        public float fade_time;
    }
}