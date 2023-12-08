namespace GameScripts.RunTime.Magic.Command
{
    public struct ComplexColor : IComplex
    {
        [Argument("r")] public float r;
        [Argument("g")] public float g;
        [Argument("b")] public float b;
        [Argument("a")] public float a;
    }
}