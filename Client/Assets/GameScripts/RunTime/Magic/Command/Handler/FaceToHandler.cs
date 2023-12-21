using System.Collections.Generic;
using GameScripts.RunTime.War;

namespace GameScripts.RunTime.Magic.Command.Handler
{
    /// <summary>
    /// 调整正面方法
    /// </summary>
    public class FaceToHandler:CmdHandlerBase<FaceTo>
    {
        protected override void OnFill(FaceTo commandData)
        {
            var warriors = new List<Warrior>();
             GetExecutors(commandData.executor,warriors);
             foreach (var warrior in warriors)
             {
                 if (warrior.RotateObj)
                 {
                     if (commandData.face_to == FaceType.Default)
                     {
                         var xx = warrior.GetDefaultRotateAngle();
                         return;
                         
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
    }
}