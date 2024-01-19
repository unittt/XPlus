namespace GameScripts.RunTime.Magic.Command.Handler
{
    /// <summary>
    /// 结束指令
    /// </summary>
    public class EndHandler:CmdHandlerBase<End>
    {
        protected override void OnFill(End commandData)
        {
            Unit.End();
        }
    }
}