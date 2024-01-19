using HT.Framework;

namespace GameScripts.RunTime.Magic.Command.Handler
{
    public abstract class CmdHandlerBase<T> : CmdHandler where T: CommandData
    {
        public override void Fill(CommandData commandData, MagicUnit unit)
        {
            base.Fill(commandData,unit);
            var data = Data.Cast<T>();
            OnFill(data);
        }

        protected abstract void OnFill(T commandData);

    }
}