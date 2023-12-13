using System;

namespace GameScripts.RunTime.Magic.Command
{
    [Serializable]
    public sealed class ComplexSound:ComplexBase
    {
        [Argument("音效路径")]
        public string SoundPath;
    }
}