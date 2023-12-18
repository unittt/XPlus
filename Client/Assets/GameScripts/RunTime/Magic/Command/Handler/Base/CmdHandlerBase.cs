using HT.Framework;

namespace GameScripts.RunTime.Magic.Command.Handler
{
    public abstract class CmdHandlerBase<T> : CmdHandler where T: CommandData
    {
        public override void Fill(CommandData commandData)
        {
            var data = commandData.Cast<T>();
            OnFill(data);
        }

        protected abstract void OnFill(T commandData);

    }
}