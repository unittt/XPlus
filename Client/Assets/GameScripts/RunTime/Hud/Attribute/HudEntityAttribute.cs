using System;

namespace GameScripts.RunTime.Hud.Attribute
{
    [AttributeUsage(AttributeTargets.Class, AllowMultiple = false, Inherited = false)]
    public sealed class HudEntityAttribute: System.Attribute
    {
        public bool IsSingle { get; private set; }

        public HudEntityAttribute(bool isSingle = true)
        {
            IsSingle = isSingle;
        }
    }
}