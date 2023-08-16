using System.Collections;
using System.Collections.Generic;
using Cysharp.Threading.Tasks;
using UnityEngine;
using HT.Framework;
using YooAsset;


/// <summary>
/// 初始化Package
/// </summary>
public class ProcedureInitPackage : ProcedureBase
{
  
    /// <summary>
    /// 进入流程
    /// </summary>
    /// <param name="lastProcedure">上一个离开的流程</param>
    public override void OnEnter(ProcedureBase lastProcedure)
    {
        base.OnEnter(lastProcedure);
        
  
    }

    private async UniTaskVoid InitPackage()
    {
        if (Main.m_Resource.LoadMode ==  EPlayMode.HostPlayMode)
        {
            
        }
    }
    
    
    
}