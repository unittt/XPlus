using HT.Framework;
/// <summary>
///闪屏流程
/// </summary>
public class ProcedureSplash : ProcedureBase
{
    /// <summary>
    /// 进入流程
    /// </summary>
    /// <param name="lastProcedure">上一个离开的流程</param>
    public override void OnEnter(ProcedureBase lastProcedure)
    {
       
    }

    public override void OnUpdate()
    {
        // 播放 Splash 动画
        //Splash.Active(splashTime:3f);
        //热更新阶段文本初始化
        // LoadText.Instance.InitConfigData(null);
        // //热更新UI初始化
        // UILoadMgr.Initialize();
        
        //检查热更资源版本
        Main.m_Procedure.SwitchProcedure<ProcedureInitPackage>();
    }
}