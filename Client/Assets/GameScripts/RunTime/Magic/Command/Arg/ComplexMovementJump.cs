using DG.Tweening;
using GameScripts.RunTime.Utility.Selector;

namespace GameScripts.RunTime.Magic.Command
{
    /// <summary>
    /// 跳跃
    /// </summary>
    public struct ComplexMovementJump: IComplex
    {
        [Argument("跳跃力度",1)] 
        public int jump_power;
        
        [Argument("跳跃次数",1)] 
        public int jump_num;
        
        
        [Argument("渐变曲线")] 
        [SelectHandler(typeof(SelectorHandler_Enum<Ease>))]
        public Ease ease_type;

        #region 起点
        [Argument("起始点类型")]
        [SelectHandler(typeof(SelectorHandler_Enum<LocationType>))]
        public LocationType begin_type;
        
        // [Argument("预设起点","IsShowBeginPrepare")]
        // public string begin_prepare;
        
        [Argument("自定义起点","IsShowBeginRelative")]
        public ComplexPosition begin_relative;
        #endregion
        
        #region 终点
        [Argument("终点类型")]
        [SelectHandler(typeof(SelectorHandler_Enum<LocationType>))]
        public LocationType end_type;
        
        // [Argument("预设终点","IsShowEndPrepare")]
        // public string end_prepare;
        
        [Argument("自定义终点","IsShowEndRelative")]
        public ComplexPosition end_relative;
        #endregion
     
        [Argument("计算朝向",true)] 
        [SelectHandler(typeof(SelectorHandler_Bool))]
        public bool calc_face;
        
        [Argument("面对终点",true)]
        [SelectHandler(typeof(SelectorHandler_Bool))]
        public bool look_at_pos;

        #region 处理参数显示
        private bool IsShowBeginPrepare()
        {
            return begin_type == LocationType.Prepare;
        }
        
        private bool IsShowBeginRelative()
        {
            return begin_type == LocationType.Relative;
        }
        
        private bool IsShowEndPrepare()
        {
            return end_type == LocationType.Prepare;
        }
        
        private bool IsShowEndRelative()
        {
            return end_type == LocationType.Relative;
        }
        #endregion
    }
}