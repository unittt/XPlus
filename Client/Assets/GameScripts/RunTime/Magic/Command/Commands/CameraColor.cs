using System;

namespace GameScripts.RunTime.Magic.Command
{
    [Serializable]
    [Command("像机颜色", 86)]
    public class CameraColor:CommandBase
    {
        [Argument("颜色")]
        public ComplexColor color;
        
        [Argument("渐变时间(?)")]
        public float fade_time;
        
        [Argument("还原时间(?)")]
        public float restore_time;
    }
}