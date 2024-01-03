using System.Collections.Generic;
using GameScripts.RunTime.Magic.Command;
using GameScripts.RunTime.War;
using HT.Framework;
using UnityEngine;

namespace GameScripts.RunTime.Magic
{
    // public class MagicCmd : IReference
    // {
    //
    //     private string funcName;
    //     private float startTime;
    //     private Dictionary<string, object> args;
    //     private MagicUnit magicUnit;
    //     private List<GameObject> effectObjs;
    //     
    //     
    //     /// <summary>
    //     /// 
    //     /// </summary>
    //     public void Fill(string funcName, float startTime, Dictionary<string, object> args, MagicUnit magicUnit)
    //     {
    //         this.funcName = funcName;
    //         this.startTime = startTime;
    //         this.args = args;
    //         this.magicUnit = magicUnit;
    //         this.effectObjs = new List<GameObject>();
    //     }
    //     
    //     public void Execute()
    //     {
    //         // 这里需要用反射或者其他机制来根据 funcName 调用相应的方法
    //         // 在 C# 中，你可能需要一个 switch 语句或者字典来映射方法名称到具体的实现
    //     }
    //     
    //     public Vector3 CalcPos(ComplexPosition posInfo, Warrior atkObj, Warrior vicObj, bool faceDir)
    //     {
    //         var vPos = MagicTools.GetLocalPosByType(magicUnit.m_RunEnv, posInfo.BasePosition,atkObj, vicObj);
    //      
    //         //--NOTE:追击状态直接跳到目标的前面
    //         Vector3 vPrePos = Vector3.zero;
    //         if (magicUnit.m_IsPursued)
    //         {
    //             vPrePos = vicObj.GetOriginPos();
    //         }
    //
    //         var oRelative = MagicTools.GetRelativeObj(posInfo.BasePosition, atkObj, vicObj, faceDir, vPrePos);
    //         vPos += MagicTools.CalcRelativePos(oRelative, posInfo.RelativeAngle, posInfo.RelativeDistance);
    //         vPos = MagicTools.CalcDepth(vPos, posInfo.Depth);
    //         MagicManager.Current.ResetCalcPosObject();
    //         return vPos;
    //     }
    //     
    //     public void Reset()
    //     {
    //         
    //     }
    // }
}