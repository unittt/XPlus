using System.Collections.Generic;
using HT.Framework;
using UnityEngine;

namespace GameScripts.RunTime.Magic
{
    public class MagicCmd : IReference
    {

        private string funcName;
        private float startTime;
        private Dictionary<string, object> args;
        private MagicUnit magicUnit;
        private List<GameObject> effectObjs;
        
        
        /// <summary>
        /// 
        /// </summary>
        public void Fill(string funcName, float startTime, Dictionary<string, object> args, MagicUnit magicUnit)
        {
            this.funcName = funcName;
            this.startTime = startTime;
            this.args = args;
            this.magicUnit = magicUnit;
            this.effectObjs = new List<GameObject>();
        }
        
        public void Execute()
        {
            // 这里需要用反射或者其他机制来根据 funcName 调用相应的方法
            // 在 C# 中，你可能需要一个 switch 语句或者字典来映射方法名称到具体的实现
        }
        
        // public Vector3 CalcPos(PositionInfo posInfo, AttackObject atkObj, VictimObject vicObj, bool faceDir)
        // {
        //     // 这里应实现计算位置的逻辑
        //     // 伪代码：Vector3 position = CalculatePositionBasedOn(posInfo, atkObj, vicObj, faceDir);
        //     // return position;
        //     return new Vector3();
        // }
        
        public void Reset()
        {
            
        }
    }
}