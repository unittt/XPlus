namespace GameScripts.RunTime.Magic.Command
{
    public struct ComplexPosition:IComplex
    {
        [Argument("基本位置")]public PositionType BasePosition;
        [Argument("偏移距离")]public float RelativeDistance;
        [Argument("偏移角度")] public float RelativeAngle;
        [Argument("高度")]public float Depth;
    }
}