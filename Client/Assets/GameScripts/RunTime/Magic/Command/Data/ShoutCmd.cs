using System;
using GameScripts.RunTime.Utility.Selector;

namespace GameScripts.RunTime.Magic.Command
{
    [Serializable]
    [Command("喊招",1003)]
    public class ShoutCmd: CommandData
    {
        [Argument("存在时间")]
        public float alive_time = 1.5f;
        
        /// <summary>
        /// 是否显示文字
        /// </summary>
        [Argument("显示文字")]
        [SelectHandler(typeof(SelectorHandler_Bool))]
        public bool show;
        
        [Argument("播放音效")]
        [SelectHandler(typeof(SelectorHandler_Bool))]
        public bool shot;
    }
}