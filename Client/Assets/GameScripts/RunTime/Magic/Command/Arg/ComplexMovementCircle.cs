namespace GameScripts.RunTime.Magic.Command
{
    /// <summary>
    /// 圆弧
    /// </summary>
    public struct ComplexMovementCircle:IComplex
    {
        [Argument("插值次数",5)] 
        public int lerp_cnt;
        
        [Argument("自定义起点")] 
        public ComplexPosition begin_relative;
        
        [Argument("自定义终点")] 
        public ComplexPosition end_relative;
    }
}