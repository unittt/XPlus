using GameScript.RunTime.Config;

namespace GameScripts.RunTime.Utility.Selector
{
    /// <summary>
    /// 动画选择器
    /// </summary>
    public class SelectorHandler_Animation : SelectorHandler
    {
        public SelectorHandler_Animation()
        {
            foreach (var clip in AnimationConfig.Clips)
            {
                Terms.Add(clip);
            }
        }
        
        internal override object GetTermValue(string value)
        {
            return value;
        }
    }
}