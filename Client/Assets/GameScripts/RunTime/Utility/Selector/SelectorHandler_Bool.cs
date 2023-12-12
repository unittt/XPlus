
namespace GameScripts.RunTime.Utility.Selector
{
    /// <summary>
    /// bool选择器
    /// </summary>
    public class SelectorHandler_Bool:SelectorHandler
    {
        
        public SelectorHandler_Bool()
        {
            Elements.Add(bool.FalseString);
            Elements.Add(bool.TrueString);
        }
        
        public override object GetValue(string value)
        {
            return value == bool.TrueString;
        }
    }
}