using System;
using GameScripts.RunTime.War;
using UnityEngine;

namespace GameScripts.RunTime.Magic
{
    public static class MagicTools
    {
        
        public static Vector3 WarGetLocalPosByType(PositionType positionType,Warrior atkObj, Warrior vicObj)
        {
            switch (positionType)
            {
                case PositionType.None:
                    return Vector3.zero;
                case PositionType.AttackerPosition: //获取攻击者位置
                   
                    break;
                case PositionType.VictimPosition: //获取受击者位置
                    
                    break;
                case PositionType.AttackerLineup: //攻击者阵法站位
                    
                    break;
                case PositionType.VictimLineup: //受击者阵法站位
                   
                    break;
                case PositionType.BattlefieldCenter: //战场中心
                    break;
                case PositionType.AttackerTeamCenter: //攻击队伍中心
                    break;
                case PositionType.VictimTeamCenter: //受击队伍中心
                    break;
                case PositionType.CameraPosition: //像机位置
                    break;
                default:
                   return Vector3.zero;
            }
            return Vector3.zero;
        }


        public static Transform GetCalcPosObj(Transform tm, Transform faceTm, Vector3 prePos = default)
        {
            MagicManager.Current.CalcPosTransform.SetParent(tm);
            var pos = tm.position;
            pos.y = 0;
            MagicManager.Current.CalcPosTransform.position = pos;

            if (faceTm == null) return MagicManager.Current.CalcPosTransform;
            
            var facePos = prePos == default ? faceTm.position : prePos;
            facePos.y = 0;
            MagicManager.Current.CalcPosTransform.LookAt(facePos,tm.up);
            return MagicManager.Current.CalcPosTransform;
        }
        
        
        public static Transform GetRelativeObj(PositionType positionType, Warrior atkObj, Warrior vicObj, bool faceDir)
        {
            switch (positionType)
            {
                case PositionType.None:
                    break;
                case PositionType.AttackerPosition:
                    
                    var faceTm = (faceDir && vicObj != null) ? vicObj.Entity.transform : null;
                    return GetCalcPosObj(atkObj._actor.Entity.transform, faceTm);
                
                case PositionType.VictimPosition:

                    var faceTm2 = (faceDir && vicObj != null) ? atkObj.Entity.transform : null;
                    var prePos = Vector3.zero;
                    if (atkObj != null && vicObj != null)
                    {
                        prePos = atkObj.GetNormalAttackPos(vicObj);
                    }
                    return GetCalcPosObj(atkObj._actor.Entity.transform, faceTm2,prePos);
                 
                case PositionType.AttackerLineup:
                    break;
                case PositionType.VictimLineup:
                    break;
                case PositionType.BattlefieldCenter:
                    break;
                case PositionType.AttackerTeamCenter:
                    break;
                case PositionType.VictimTeamCenter:
                    break;
                case PositionType.CameraPosition:
                    return  Camera.main.transform;
                default:
                    return null;
            }
        }

        public static Vector3 CalcRelativePos(Transform relative, float angle, float dis)
        {
            if (dis == 0)
            {
                return Vector3.zero;
            }
            
            var rad = Mathf.Deg2Rad * angle;
            var xOffset = Mathf.Sin(rad) * dis;
            var zOffset = Mathf.Cos(rad) * dis;
            
            var pos = new Vector3(xOffset, 0, zOffset);
            return relative.TransformVector(pos);
        }

        public static Vector3 CalcDepth(Vector3 vPos, float posInfoDepth)
        {
            return Vector3.zero;
        }
    }
}