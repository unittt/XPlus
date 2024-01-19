using System;
using GameScripts.RunTime.Magic.Command;
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
                relativeObj =  GetCalcPosObj(atkObj.transform, faceTm);
            }
            else if (positionType == PositionType.VictimPosition)
            {
                var faceTm = (faceDir && vicObj != null) ? atkObj.Entity.transform : null;
                var prePos = Vector3.zero;
                if (atkObj != null && vicObj != null)
                {
                    prePos = atkObj.GetNormalAttackPos(vicObj);
                }
                relativeObj =  GetCalcPosObj(atkObj.transform, faceTm,prePos);
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

        public static Vector3 GetDir(Warrior warrior, MoveDirection commandDataDir)
        {
            return commandDataDir switch
            {
                MoveDirection.LocalUp => warrior.LocalUp,
                MoveDirection.LocalRight => warrior.LocalRight,
                MoveDirection.LocalForward => warrior.LocalForward,
                MoveDirection.WorldUp => Vector3.up,
                MoveDirection.WorldRight => Vector3.right,
                MoveDirection.WorldForward => Vector3.forward,
                _ => Vector3.zero
            };
        }
        
        
        public static Vector3 CalcPos(ComplexPosition complexPosition,Warrior atkObj, Warrior vicObj, bool faceDir)
        {
            var pos = WarGetLocalPosByType(complexPosition.BasePosition, atkObj, vicObj);
         
            var oRelative = GetRelativeObj(complexPosition.BasePosition, atkObj, vicObj, faceDir);
            if (oRelative)
            {
                pos += CalcRelativePos(oRelative, complexPosition.RelativeAngle, complexPosition.RelativeDistance);
            }

            pos = CalcDepth(pos, complexPosition.Depth);
            return pos;
        }

        public static Vector3 GetExecutorLocalAngle(Warrior warrior, ExecutorDirection commandDataEffectDirType)
        {
            var rotateObj = warrior.RotateTransform;
            var localEulerAngles = rotateObj.localEulerAngles;
            if (commandDataEffectDirType == ExecutorDirection.Backward)
            {
                localEulerAngles.y += 180;
            }
            else if (commandDataEffectDirType == ExecutorDirection.Right)
            {
                localEulerAngles.y += 90;
            }
            else if (commandDataEffectDirType == ExecutorDirection.Left)
            {
                localEulerAngles.y -= 90;
            }

            return localEulerAngles;
        }

        public static Vector3 GetExecutorDirPos(Warrior warrior, ExecutorDirection effectDirType, Vector3 pos)
        {
            var dirPos = Vector3.zero;
            switch (effectDirType)
            {
                case ExecutorDirection.Forward:
                    dirPos = pos + warrior.RotateTransform.forward;
                    break;
                case ExecutorDirection.Backward:
                    dirPos = pos + -warrior.RotateTransform.forward;
                    break;
                case ExecutorDirection.Left:
                    dirPos = pos + -warrior.RotateTransform.right;
                    break;
                case ExecutorDirection.Right:
                    dirPos = pos + warrior.RotateTransform.right;
                    break;
                case ExecutorDirection.Up:
                    
                    dirPos = pos +warrior.RotateTransform.up;
                    break;
                case ExecutorDirection.Down:
                    dirPos = pos + -warrior.RotateTransform.up;
                    break;
            }

            return dirPos;
        }
    }
}