using UnityEngine;

namespace GameScripts.RunTime.War
{
    public static class WarTools
    {
        public static float GetHorizontalDis(Vector3 pos1, Vector3 pos2)
        {
            return Mathf.Sqrt(Mathf.Pow(pos1.x - pos2.x, 2) + Mathf.Pow(pos1.z - pos2.z, 2));
        }
        
        public static bool CheckInDistance(Vector3 pos1, Vector3 pos2, float max)
        {
            return (Mathf.Pow(pos1.x - pos2.x, 2) + Mathf.Pow(pos1.z - pos2.z, 2)) <= Mathf.Pow(max, 2);
        }

        
        /// <summary>
        /// 获得相对于root中的世界空间
        /// </summary>
        /// <param name="root"></param>
        /// <param name="startPos"></param>
        /// <param name="endPos"></param>
        public static Vector3 GetRootDir( Transform root,Vector3 startPos, Vector3 endPos)
        {
            var dir = (endPos - startPos).normalized;
            return root.TransformDirection(dir);
        }
        
    }
}