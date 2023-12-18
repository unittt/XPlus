using System;

namespace GameScripts.RunTime.Magic.Command
{
    [Serializable]
    [Command("加载UI", 92)]
    public class LoadUI: CommandData
    {
        [Argument("路径")]
        public string path;
        
        [Argument("时间")]
        public float time = 1;
    }
}