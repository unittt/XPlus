using HT.Framework;

namespace HotUpdate
{
    /// <summary>
    /// 补丁流程步骤改变
    /// </summary>
    public sealed class EventPatchStatesChange : EventHandlerBase
    {
        public string Tips { get; private set; }

        internal EventPatchStatesChange Fill(string tips)
        {
            Tips = tips;
            return this;
        }

        public override void Reset()
        {

        }
    }
}
