using System.Collections.Generic;
using UnityEngine;

namespace GameScripts.RunTime.Map
{
    public class GridMapConfig
    {
        public string id;
        public int xTile;
        public int yTile;

        public List<GridMapEffectData> fgEffectList = new();
        public List<GridMapEffectData> bgEffectList = new();
        public List<GridMapEffectData> tfEffectList = new();
        public List<GridMapTransferData> transferList = new();
    }
    
    public class GridMapEffectData
    {
        public string name;
        public Vector2 pos;
        public Vector3 rotation;
        public Vector3 scale;
    }

    public class GridMapTransferData
    {
        public int idx;
        public Vector2 pos;
        public Vector2 size;
    }
}