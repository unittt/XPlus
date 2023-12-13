using System;
using GameScripts.RunTime.Utility.Selector;

namespace GameScripts.RunTime.Magic.Command
{
    /// <summary>
    /// 自定位置
    /// </summary>
    [Serializable]
    public sealed class ComplexPosition : ComplexBase
    {
        [Argument("基本位置"), SelectHandler(typeof(SelectorHandler_Enum<PositionType>))]
        public PositionType BasePosition;

        [Argument("偏移距离")] 
        public float RelativeDistance;
        [Argument("偏移角度")] 
        public float RelativeAngle;
        [Argument("高度")]
        public float Depth;
    }
}