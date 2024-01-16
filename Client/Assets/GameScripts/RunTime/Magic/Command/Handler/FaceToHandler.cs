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
                 if (warrior.RotateObj)
                 {
                     if (commandData.face_to == FaceType.Default)
                     {
                         warrior.SetLocalEulerAngles(Vector3.zero);
                         var angle = warrior.GetDefaultRotateAngle();
                         warrior.RotateObj.transform.DOLocalRotate(angle, commandData.time);
                         return;
                     }

                     var atkObj = MagicUnit.AtkObj;
                     var vicObj = MagicUnit.GetVicObjFirst();
                     
                     if (commandData.face_to == FaceType.Fixed_pos)
                     {
                         // var vEndPos = CalcPos(args.pos, atkObj, vicObj, false);
                         MagicTools.WarGetLocalPosByType(commandData.pos.BasePosition, atkObj, vicObj);
                     }
                     
                     
                     // var atkObj = self.m_MagicUnit:GetAtkObj()
                     // var vicObj = self.m_MagicUnit:GetVicObjFirst()
                     
                     // if (commandData.face_to == FaceType.Fixed_pos)
                     // {
                     //     var vEndPos = self:CalcPos(args.pos, atkObj, vicObj, false)
                     // }
                 }
             }
        }

        private void CalcPos(PositionType positionType,Warrior atkObj, Warrior vicObj, bool isFaceDir = true)
        {
            var vPos = MagicTools.WarGetLocalPosByType(positionType, atkObj, vicObj);

            //追击状态直接跳到目标的前面
            var vPrePos = MagicUnit.IsPursued ? vicObj.GetOriginPos() : Vector3.zero;

            MagicTools.GetRelativeObj(positionType, atkObj, vicObj, isFaceDir, vPrePos);
        }

        
    }
}