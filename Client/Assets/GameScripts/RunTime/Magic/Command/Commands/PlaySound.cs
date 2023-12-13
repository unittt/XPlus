using System;

namespace GameScripts.RunTime.Magic.Command
{
    [Serializable]
    [Command("音效",2)]
    public class PlaySound: CommandBase
    {
        [Argument("音效资源")]
        public ComplexSound sound;
    }
}