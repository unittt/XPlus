using System;

namespace GameScripts.RunTime.Magic.Command
{
    [Serializable]
    [Command("设置变量名", 98)]
    public class ControlObject:CommandBase
    {
        [Argument("变量名")]
        public string name = "obj1";
    }
}