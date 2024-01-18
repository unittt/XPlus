using System;
using DG.Tweening;
using GameScripts.RunTime.Utility.Selector;

namespace GameScripts.RunTime.Magic.Command
{
    /// <summary>
    /// 直线移动
    /// </summary>
    [Serializable]
    public sealed class ComplexMovementLine : ComplexBase
    {
        [Argument("渐变曲线")] [SelectHandler(typeof(SelectorHandler_Enum<Ease>))]
        public Ease ease_type = Ease.Linear;

        #region 起点
        [Argument("起始点类型")]
        [SelectHandler(typeof(SelectorHandler_Enum<LocationType>))]
        public LocationType begin_type;
        
        [Argument("预设起点","IsShowBeginPrepare")]
        public string begin_prepare;
        
        [Argument("自定义起点","IsShowBeginRelative")]
        public ComplexPosition begin_relative = new();
        #endregion
        
        #region 终点
        [Argument("终点类型")]
        [SelectHandler(typeof(SelectorHandler_Enum<LocationType>))]
        public LocationType end_type;
        
        [Argument("预设终点","IsShowEndPrepare")]
        public string end_prepare;
        
        [Argument("自定义终点","IsShowEndRelative")]
        public ComplexPosition end_relative = new();
        #endregion

        [Argument("计算朝向")] [SelectHandler(typeof(SelectorHandler_Bool))]
        public bool calc_face = true;

        /// <summary>
        /// 面向终点
        /// </summary>
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