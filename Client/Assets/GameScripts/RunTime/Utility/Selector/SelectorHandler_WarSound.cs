namespace GameScripts.RunTime.Utility.Selector
{

    /// <summary>
    /// 战斗音效选择
    /// </summary>
    public class SelectorHandler_WarSound:SelectorHandlerAsset
    {
        protected override string Path =>"Assets/GameRes/Audio/Sound/War";
        protected override string Filter => "t:AudioClip";
    }
}