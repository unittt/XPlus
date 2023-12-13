using System;

namespace GameScripts.RunTime.Magic.Command
{

    [Serializable]
    public sealed class ComplexRandomPosition : ComplexBase
    {
        [Argument("x最小值")] public float x_min;
        [Argument("x最大值")] public float x_max;
        [Argument("y最小值")] public float y_min;
        [Argument("y最大值")] public float y_max;
        [Argument("z最小值")] public float z_min;
        [Argument("z最大值")] public float z_max;
    }
}