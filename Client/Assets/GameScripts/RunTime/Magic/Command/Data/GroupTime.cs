using System;

namespace GameScripts.RunTime.Magic.Command
{
    [Serializable]
    [Command("指令组时间",1002)]
    public class GroupTime:CommandData
    {
        [Argument("组时间")]
        public float duration;
    }
}