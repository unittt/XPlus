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
            var calcPosTransform = MagicManager.Current.GetCalcPosTransform();
            calcPosTransform.SetParent(tm);
            var pos = tm.position;
            pos.y = 0;
            calcPosTransform.position = pos;

            if (faceTm == null) return calcPosTransform;
            
            var facePos = prePos == default ? faceTm.position : prePos;
            facePos.y = 0;
            calcPosTransform.LookAt(facePos,tm.up);
            return calcPosTransform;
        }
        
        
        public static Transform GetRelativeObj(PositionType positionType, Warrior atkObj, Warrior vicObj, bool faceDir)
        {
            Transform relativeObj;
            
            if (positionType == PositionType.AttackerPosition)
            {
                var faceTm = (faceDir && vicObj != null) ? vicObj.Entity.transform : null;
                relativeObj =  GetCalcPosObj(atkObj._actor.Entity.transform, faceTm);
            }
            else if (positionType == PositionType.VictimPosition)
            {
                var faceTm = (faceDir && vicObj != null) ? atkObj.Entity.transform : null;
                var prePos = Vector3.zero;
                if (atkObj != null && vicObj != null)
                {
                    prePos = atkObj.GetNormalAttackPos(vicObj);
                }
                relativeObj =  GetCalcPosObj(atkObj._actor.Entity.transform, faceTm,prePos);
            }
            else if (positionType == PositionType.CameraPosition)
            {
                relativeObj = Camera.main.transform;
            }
            else if (positionType is PositionType.AttackerTeamCenter or PositionType.BattlefieldCenter)
            {
                relativeObj = GetCalcPosObj(WarManager.Current.Root, null);
                relativeObj.localEulerAngles = atkObj.GetDefaultRotateAngle();
            }
            else if (positionType == PositionType.VictimTeamCenter)
            {
                relativeObj = GetCalcPosObj(WarManager.Current.Root, null);
                relativeObj.localEulerAngles = vicObj.GetDefaultRotateAngle();
            }
            else
            {
                relativeObj = WarManager.Current.Root;
            }
            
            return relativeObj;
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

        public static Vector3 CalcDepth(Vector3 pos, float posInfoDepth)
        {
            pos.y += posInfoDepth;
            return pos;
        }
    }
}