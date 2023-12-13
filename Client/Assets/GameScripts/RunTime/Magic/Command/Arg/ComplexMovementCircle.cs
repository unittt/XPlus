using System;

namespace GameScripts.RunTime.Magic.Command
{
    /// <summary>
    /// 圆弧
    /// </summary>
    [Serializable]
    public sealed class ComplexMovementCircle:ComplexBase
    {
        [Argument("插值次数")] 
        public int lerp_cnt = 5;
        
        [Argument("自定义起点")] 
        public ComplexPosition begin_relative;
        
        [Argument("自定义终点")] 
        public ComplexPosition end_relative;
    }
}