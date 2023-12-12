using DG.Tweening;
using GameScripts.RunTime.Utility.Selector;

namespace GameScripts.RunTime.Magic.Command
{
    /// <summary>
    /// 跳跃
    /// </summary>
    public sealed class ComplexMovementJump: ComplexBase
    {
        [Argument("跳跃力度")] 
        public int jump_power = 1;
        
        [Argument("跳跃次数")] 
        public int jump_num = 1;


        [Argument("渐变曲线")] [SelectHandler(typeof(SelectorHandler_Enum<Ease>))]
        public Ease ease_type = Ease.Linear;

        #region 起点
        [Argument("起始点类型")]
        [SelectHandler(typeof(SelectorHandler_Enum<LocationType>))]
        public LocationType begin_type;
        
        [Argument("预设起点","IsShowBeginPrepare")]
        public string begin_prepare;
        
        [Argument("自定义起点","IsShowBeginRelative")]
        public ComplexPosition begin_relative;
        #endregion
        
        #region 终点
        [Argument("终点类型")]
        [SelectHandler(typeof(SelectorHandler_Enum<LocationType>))]
        public LocationType end_type;
        
        [Argument("预设终点","IsShowEndPrepare")]
        public string end_prepare;
        
        [Argument("自定义终点","IsShowEndRelative")]
        public ComplexPosition end_relative;
        #endregion

        [Argument("计算朝向")] [SelectHandler(typeof(SelectorHandler_Bool))]
        public bool calc_face = true;

        [Argument("面对终点")] [SelectHandler(typeof(SelectorHandler_Bool))]
        public bool look_at_pos = true;

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