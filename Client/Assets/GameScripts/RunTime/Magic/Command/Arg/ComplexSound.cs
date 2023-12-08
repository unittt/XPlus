namespace GameScripts.RunTime.Magic.Command
{
    public struct ComplexSound:IComplex
    {
        [Argument("音效路径")]
        public string SoundPath;
    }
}