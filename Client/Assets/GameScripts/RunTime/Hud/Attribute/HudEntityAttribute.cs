using System;
using HT.Framework;

namespace GameScripts.RunTime.Hud.Attribute
{
    [AttributeUsage(AttributeTargets.Class, AllowMultiple = false, Inherited = false)]
    public sealed class HudEntityAttribute: EntityResourceAttribute
    {
        public bool IsSingle { get; private set; }

        public HudEntityAttribute(string location, bool isSingle) : base(location, true)
        {
            IsSingle = isSingle;
        }
    }
}