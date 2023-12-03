using UnityEngine;

namespace GameScript.RunTime.Config
{
    public static class LayerConfig
    {
        public static readonly int Map = LayerMask.NameToLayer("Map");
        public static readonly int MapWalker = LayerMask.NameToLayer("MapWalker");
        public static readonly int War = LayerMask.NameToLayer("War");
    }
}