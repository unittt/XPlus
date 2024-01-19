namespace GameScripts.RunTime.Magic.Command.Handler
{
    /// <summary>
    /// 受击处理
    /// </summary>
    public class VicHitInfoHandler : CmdHandlerBase<VicHitInfo>
    {
        protected override void OnFill(VicHitInfo commandData)
        {
            Unit.AddHitInfo();
        }
    }
}