using GameScript.RunTime.Config;

namespace GameScripts.RunTime.Utility.Selector
{
    /// <summary>
    /// 动画选择器
    /// </summary>
    public class SelectorHandler_AnimationClip : SelectorHandler
    {
        public SelectorHandler_AnimationClip()
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