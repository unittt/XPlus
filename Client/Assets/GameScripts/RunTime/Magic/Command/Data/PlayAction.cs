using System;
using GameScripts.RunTime.Utility.Selector;

namespace GameScripts.RunTime.Magic.Command
{
    [Serializable]
    [Command("播放动作",10)]
    public class PlayAction:CommandData
    {
        [Argument("动作")]
        // [SelectHandler(typeof(SelectorHandler_Bool))]
        public string action_name;
        
        [Argument("执行人")]
        [SelectHandler(typeof(SelectorHandler_Enum<ExecutorType>))]
        public ExecutorType executor;

        [Argument("起始帧(?)")]
        public int start_frame;
        
        [Argument("结束帧(?)")]
        public int end_frame;
        
        [Argument("动作时间(?)")]
        public float action_time;
        
        [Argument("备用动作(?)")]
        // [SelectHandler(typeof(SelectorHandler_Bool))]
        public string bak_action_name;
    }
}