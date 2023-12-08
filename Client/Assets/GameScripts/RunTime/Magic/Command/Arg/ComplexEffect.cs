namespace GameScripts.RunTime.Magic.Command
{
    public struct ComplexEffect : IComplex
    {
        [Argument("路径")] public string Path;
        [Argument("缓存")] public bool Cached;
        [Argument("层级")] public string MagicLayer;
        [Argument("预加载")] public bool Preload;
    }
}