using System.Collections.Generic;
using DG.Tweening;
using GameScripts.RunTime.War;
using UnityEngine;

namespace GameScripts.RunTime.Magic.Command.Handler
{
    /// <summary>
    /// 调整正面方法
    /// </summary>
    public class FaceToHandler:CmdHandlerBase<FaceTo>
    {
        private List<Warrior> _warriors = new();
        
        protected override void OnFill(FaceTo commandData)
        {
             GetExecutors(commandData.executor,_warriors);
             foreach (var warrior in _warriors)
             {
                 var rotateObj = warrior.RotateObj;
                 if (rotateObj)
                 {
                     if (commandData.face_to == FaceType.Default)
                     {
                         //设置角色的eulerAngles
                         warrior.SetLocalEulerAngles(Vector3.zero);
                         //获得角色的默认角度
                         var angle = warrior.GetDefaultRotateAngle();
                         //旋转到默认角度
                         rotateObj.transform.DOLocalRotate(angle, commandData.time);
                         return;
                     }

                     var atkObj = MagicUnit.AtkObj;
                     var vicObj = MagicUnit.GetVicObjFirst();
                     
                     if (commandData.face_to == FaceType.Fixed_pos)
                     {
                         var endPos = CalcPos(commandData.pos, atkObj, vicObj, false);
                         var calcPosTransform = MagicManager.Current.GetCalcPosTransform();
                         calcPosTransform.SetParent(rotateObj, false);
                         calcPosTransform.LookAt(endPos, rotateObj.up);
                         rotateObj.DOLocalRotate(calcPosTransform.localEulerAngles, commandData.time);
                         MagicTools.WarGetLocalPosByType(commandData.pos.BasePosition, atkObj, vicObj);
                     }
                     else if (commandData.face_to == FaceType.Lerp_pos)
                     {
                         var beginV = rotateObj.localEulerAngles;
                         var endV = beginV + new Vector3(-commandData.v_dis, commandData.h_dis, 0);

                         var mode = (Mathf.Abs(commandData.v_dis) > 359 || Mathf.Abs(commandData.h_dis) > 359)
                             ? RotateMode.FastBeyond360
                             : RotateMode.Fast;
                         rotateObj.DOLocalRotate(endV, commandData.time,mode);
                     }
                     else if (commandData.face_to == FaceType.Look_at)
                     {
                         var endPos = CalcPos(commandData.pos, atkObj, vicObj, false);
                         var root = WarManager.Current.Root;
                         var vUp = root.up;
                         local oAction = CLookAt.New(oRotateObj, args.time, vEndPos, vUp);
                     }
                     else if (commandData.face_to == FaceType.Random)
                     {
                         
                     }
                 }
             }
        }

        private Vector3 CalcPos(ComplexPosition complexPosition,Warrior atkObj, Warrior vicObj, bool faceDir)
        {
            var pos = MagicTools.WarGetLocalPosByType(complexPosition.BasePosition, atkObj, vicObj);
         
            var oRelative = MagicTools.GetRelativeObj(complexPosition.BasePosition, atkObj, vicObj, faceDir);
            if (oRelative)
            {
                pos += MagicTools.CalcRelativePos(oRelative, complexPosition.RelativeAngle, complexPosition.RelativeDistance);
            }

            pos = MagicTools.CalcDepth(pos, complexPosition.Depth);
            return pos;
        }
    }
}