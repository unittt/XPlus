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
        
        // TODO: 这里可以播放一个 Splash 动画
        // ...
        // if (GameEntryMain.Base.EditorResourceMode)
        // {
        //     // 编辑器模式
        //     // Log.Info("Updatable resource mode detected.");
        //     // ChangeState<ProcedureLoadAssembly>(procedureOwner);
        //     
        //     //进入主入口
        // }
        // else if (GameEntryMain.Resource.ResourceMode == ResourceMode.Package)
        // {
        //     // 单机模式
        //     // Log.Info("Package resource mode detected.");
        //     // ChangeState<ProcedureInitResources>(procedureOwner);
        //     
        //     //更新资源
        //     //ProcedureLoadAssembly
        //     //进入主入口
        // }
        // else
        // {
        //     // 可更新模式
        //     // Log.Info("Updatable resource mode detected.");
        //     // ChangeState<ProcedureCheckVersion>(procedureOwner);
        //     
        //     //检查版本
        //     //更新资源
        //     //ProcedureLoadAssembly
        //     //进入主入口
        // }
        
        //检查热更资源版本
        Main.m_Procedure.SwitchProcedure<ProcedureCheckVersion>();
    }
}