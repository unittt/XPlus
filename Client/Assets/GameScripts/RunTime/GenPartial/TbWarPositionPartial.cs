using UnityEngine;

namespace cfg.WarModule
{
    public partial class TbWarPosition
    {
        public Vector3 GetPosition(ECamp camp, int index)
        {
            foreach (var warPosition in _dataList)
            {
                if (warPosition.Camp == camp && warPosition.Index == index)
                {
                   return new Vector3(warPosition.X, 0, warPosition.Z);
                }
            }
            return Vector3.zero;
        }
    }
}