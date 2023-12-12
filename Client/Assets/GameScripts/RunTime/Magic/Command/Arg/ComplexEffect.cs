using GameScripts.RunTime.Utility.Selector;

namespace GameScripts.RunTime.Magic.Command
{
    public sealed class ComplexEffect : ComplexBase
    {
        [Argument("路径")]
        public string Path;
        
        [Argument("缓存")]
        [SelectHandler(typeof(SelectorHandler_Bool))]
        public bool Cached;
        
        [Argument("层级")] 
        [SelectHandler(typeof(SelectorHandler_Enum<MagicLayer>))]
        public MagicLayer MagicLayer;
        
        [Argument("预加载")] 
        [SelectHandler(typeof(SelectorHandler_Bool))]
        public bool Preload;
    }
}