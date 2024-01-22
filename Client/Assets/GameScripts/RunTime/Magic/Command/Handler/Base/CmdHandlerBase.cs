using HT.Framework;

namespace GameScripts.RunTime.Magic.Command.Handler
{
    public abstract class CmdHandlerBase<T> : CmdHandler where T: CommandData
    {
        public T Data { get; private set; }

        public override void Fill(CommandData commandData, MagicUnit unit)
        {
            base.Fill(commandData,unit);
            Data = commandData.Cast<T>();
        }
    }
}