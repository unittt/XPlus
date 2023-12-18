using System;

namespace GameScripts.RunTime.Magic.Command
{
    [Serializable]
    [Command("震屏",81)]
    public class ShakeScreen:CommandData
    {
        [Argument("时间")]
        public float shake_time;

        [Argument("幅度")] public float shake_dis = 0.1f;

        [Argument("频率")] public float shake_rate = 1;
    }
}