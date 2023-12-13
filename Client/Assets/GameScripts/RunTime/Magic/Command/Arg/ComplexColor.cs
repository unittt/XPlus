using System;

namespace GameScripts.RunTime.Magic.Command
{
    [Serializable]
    public sealed class ComplexColor : ComplexBase
    {
        [Argument("r")] public float r;
        [Argument("g")] public float g;
        [Argument("b")] public float b;
        [Argument("a")] public float a;
    }
}