using HT.Framework;

namespace HotUpdate
{

    /// <summary>
    /// 发现更新文件
    /// </summary>
    public sealed class EventFoundUpdateFiles : EventHandlerBase
    {
        public int TotalCount { get; private set; }
        public long TotalSizeBytes { get; private set; }

        internal EventFoundUpdateFiles Fill(int totalCount, long totalSizeBytes)
        {
            TotalCount = totalCount;
            TotalSizeBytes = totalSizeBytes;
            return this;
        }

        public override void Reset()
        {

        }
    }
}