using GameScripts.RunTime.Select.Attributes;

namespace GameScripts.RunTime.Select.Complex
{
    public class ComplexEffect
    {
        [Argument("路径")]
        public string Path;
        [Argument("缓存")]
        public bool Cached;
        [Argument("层级")]
        public string MagicLayer;
        [Argument("预加载")]
        public bool Preload;
    }
}