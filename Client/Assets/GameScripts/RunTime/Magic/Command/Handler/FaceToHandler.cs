using System.Collections.Generic;
using DG.Tweening;
using GameScripts.RunTime.War;
using UnityEngine;

namespace GameScripts.RunTime.Magic.Command.Handler
{
    /// <summary>
    /// 调整正面方向
    /// </summary>
    public class FaceToHandler:CmdHandlerBase<FaceTo>
    {
        private List<Warrior> _warriors = new();
        

        protected override void OnExecute()
        {
             GetExecutors(Data.executor,_warriors);
             foreach (var warrior in _warriors)
             {
                 var rotateObj = warrior.RotateTransform;
                 if (rotateObj)
                 {
                     if (Data.face_to == FaceType.Default)
                     {
                         //设置角色的eulerAngles
                         warrior.SetLocalEulerAngles(Vector3.zero);
                         //获得角色的默认角度
                         var angle = warrior.GetDefaultRotateAngle();
                         //旋转到默认角度
                         rotateObj.transform.DOLocalRotate(angle, Data.time);
                         return;
                     }

                     var atkObj = Unit.AtkObj;
                     var vicObj = Unit.GetVicObjFirst();
                     
                     if (Data.face_to == FaceType.Fixed_pos)
                     {
                         var endPos = MagicTools.CalcPos(Data.pos, atkObj, vicObj, false);
                         var calcPosTransform = MagicManager.Current.GetCalcPosTransform();
                         calcPosTransform.SetParent(rotateObj, false);
                         calcPosTransform.LookAt(endPos, rotateObj.up);
                         rotateObj.DOLocalRotate(calcPosTransform.localEulerAngles, Data.time);
                         MagicTools.WarGetLocalPosByType(Data.pos.BasePosition, atkObj, vicObj);
                     }
                     else if (Data.face_to == FaceType.Lerp_pos)
                     {
                         var beginV = rotateObj.localEulerAngles;
                         var endV = beginV + new Vector3(-Data.v_dis, Data.h_dis, 0);

                         var mode = (Mathf.Abs(Data.v_dis) > 359 || Mathf.Abs(Data.h_dis) > 359)
                             ? RotateMode.FastBeyond360
                             : RotateMode.Fast;
                         rotateObj.DOLocalRotate(endV, Data.time,mode);
                     }
                     else if (Data.face_to == FaceType.Look_at)
                     {
                         var endPos = MagicTools.CalcPos(Data.pos, atkObj, vicObj, false);
                         var root = WarManager.Current.Root;
                         var vUp = root.up;
                         // local oAction = CLookAt.New(oRotateObj, args.time, vEndPos, vUp);
                         rotateObj.DOLookAt(endPos, Data.time, AxisConstraint.None, vUp);
                     }
                     else if (Data.face_to == FaceType.Random)
                     {
                         var endValue = new Vector3
                                        (
                                         Random.Range(Data.randomPosition.x_min, Data.randomPosition.x_max), 
                                         Random.Range(Data.randomPosition.y_min, Data.randomPosition.y_max),
                                         Random.Range(Data.randomPosition.z_min, Data.randomPosition.z_max)
                                         );
                         rotateObj.DOLocalRotate(endValue, Data.time);
                     }
                 }
             }
        }
    }
}