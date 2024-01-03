
namespace GameScripts.RunTime.Utility.Selector
{
    /// <summary>
    /// bool选择器
    /// </summary>
    public class SelectorHandler_Bool:SelectorHandler
    {
        
        public SelectorHandler_Bool()
        {
            Terms.Add(bool.FalseString);
            Terms.Add(bool.TrueString);
        }
        
        internal override object GetTermValue(string value)
        {
            return value == bool.TrueString;
        }
    }
}