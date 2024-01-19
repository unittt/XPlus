using cfg.WarModule;
using Cysharp.Threading.Tasks;
using HT.Framework;
using Pb.Mmo.Common;
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
        /// <param name="startPos"></param>
        /// <param name="endPos"></param>
        public static Vector3 GetRootDir(Vector3 startPos, Vector3 endPos)
        {
            var dir = (endPos - startPos).normalized;
            return WarManager.Current.Root.TransformDirection(dir);
        }


        public static Vector3 GetDefalutRotateAngle(ECamp camp)
        {
            if (camp == ECamp.A)
            {
                return new Vector3(0, -50, 0);
            }
            
            return new Vector3(0, 130, 0);
        }

        /// <summary>
        /// 根据阵营和阵营站位获取世界坐标
        /// </summary>
        /// <param name="camp"></param>
        /// <param name="index"></param>
        /// <returns></returns>
        public static Vector3 GetPositionByCampAndIndex(ECamp camp, int index)
        {
            return TableGlobal.Instance.TbWarPosition.GetPosition(camp, index);
        }


        
        
        
        // public static async UniTask<Warrior> CreateWarrior(int campId,BaseWarrior baseWarrior)
        // {
        //     var warrior = await Main.m_Entity.CreateEntity<Warrior>();
        //
        //     baseWarrior.wid;
        //     warrior.AssembleModel();
        //     return warrior;
        // }
    }
}