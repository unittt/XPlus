using System;
using GameScripts.RunTime.Utility.Selector;

namespace GameScripts.RunTime.Magic.Command
{
    [Serializable]
    public sealed class ComplexSound:ComplexBase
    {
        [Argument("音效")]
        [SelectHandler(typeof(SelectorHandler_WarSound))]
        public string SoundPath;
    }
}