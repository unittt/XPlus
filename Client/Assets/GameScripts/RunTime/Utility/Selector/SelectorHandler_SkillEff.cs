namespace GameScripts.RunTime.Utility.Selector
{
    /// <summary>
    /// 技能特效选择器
    /// </summary>
    public class SelectorHandler_SkillEff : SelectorHandlerAsset
    {
        protected override string Path => "Assets/GameRes/Effect/Magic";

        protected override string Filter => "t:GameObject";
    }
}