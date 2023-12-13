using System;

namespace GameScripts.RunTime.Magic.Command
{

    [Serializable]
    [Command("隐藏UI", 90)]
    public class HideUI : CommandBase
    {
        [Argument("持续时间(0一直隐藏)")] public float time;
    }
}