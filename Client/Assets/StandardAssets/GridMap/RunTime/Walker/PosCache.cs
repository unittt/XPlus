using System;
using UnityEngine;

namespace GridMap.RunTime.Walker
{
    public struct PosCache
    {
        public Vector2Int NodePoint;
        public Vector2Int Position;

        public double Key
        {
            get
            {
                var result = NodePoint.x + NodePoint.y * Math.Pow(10.0, 2.0) + Position.x * Math.Pow(10.0, 4.0) + Position.y * Math.Pow(10.0, 6.0);
                return result;
            }
        }
    }
}