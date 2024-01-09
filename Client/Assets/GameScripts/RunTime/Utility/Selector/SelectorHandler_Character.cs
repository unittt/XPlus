namespace GameScripts.RunTime.Utility.Selector
{
    /// <summary>
    /// 角色模型查找
    /// </summary>
    public class SelectorHandler_Character : SelectorHandlerAsset
    {
        protected override string Path =>"Assets/GameRes/Model/Character";
        protected override string Filter => "t:GameObject";
    }
}