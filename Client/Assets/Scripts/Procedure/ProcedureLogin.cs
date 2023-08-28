using HT.Framework;
using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 登录流程
/// </summary>
public class ProcedureLogin : ProcedureBase
{
    public override void OnEnter(ProcedureBase lastProcedure)
    {
        base.OnEnter(lastProcedure);
        Main.m_UI.OpenUI<UILogin>();
    }
}
