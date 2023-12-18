using System;
using UnityEngine;

namespace GameScripts.RunTime.Magic.Command
{
    [Serializable]
    public sealed class ComplexColor : ComplexBase
    {
        [Argument("r")] public int r;
        [Argument("g")] public int g;
        [Argument("b")] public int b;
        [Argument("a")] public int a;


        public Color GetColor()
        {
            return new Color(r / 255f, g / 255f, b / 255f, a / 255f);
        }
    }
}