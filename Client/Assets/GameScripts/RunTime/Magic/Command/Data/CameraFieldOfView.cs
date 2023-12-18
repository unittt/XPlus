using System;

namespace GameScripts.RunTime.Magic.Command
{
    [Serializable]
    [Command("像机FieldOfView", 87)]
    public class CameraFieldOfView : CommandData
    {
        [Argument("起始值")] public float start_val = 26;

        [Argument("结束值")] public float end_val = 26;

        [Argument("渐变时间")] public float fade_time;
    }
}