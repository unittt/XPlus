using GameScripts.RunTime.War;
using UnityEngine;

namespace GameScripts.RunTime.Magic
{
    public static class MagicTools
    {
        public static Vector3 GetLocalPosByType(string sEnv,PositionType positionType,Warrior atkObj, Warrior vicObj)
        {
            if (sEnv == "war")
            {
                return WarGetLocalPosByType(positionType);
            }

            return Vector3.zero;
        }


        public static Vector3 WarGetLocalPosByType(PositionType positionType)
        {
            return Vector3.zero;
        }

        public static Vector3 GetRelativeObj(PositionType posInfoBasePosition, Warrior atkObj, Warrior vicObj, bool faceDir, Vector3 vPrePos)
        {
            return Vector3.zero;
        }

        public static Vector3 CalcRelativePos(Vector3 oRelative, float posInfoRelativeAngle, float posInfoRelativeDistance)
        {
            return Vector3.zero;
        }

        public static Vector3 CalcDepth(Vector3 vPos, float posInfoDepth)
        {
            return Vector3.zero;
        }
    }
}