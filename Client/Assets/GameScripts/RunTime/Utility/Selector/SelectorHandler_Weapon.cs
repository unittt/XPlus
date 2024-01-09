namespace GameScripts.RunTime.Utility.Selector
{
    /// <summary>
    /// 武器查找
    /// </summary>
    public class SelectorHandler_Weapon : SelectorHandlerAsset
    {
        protected override string Path =>"Assets/GameRes/Model/Weapon";
        protected override string Filter => "t:GameObject";
    }
}