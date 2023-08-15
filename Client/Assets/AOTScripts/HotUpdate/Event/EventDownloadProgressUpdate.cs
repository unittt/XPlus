using HT.Framework;

namespace HotUpdate
{
    /// <summary>
    /// 下载进度更新
    /// </summary>
    public sealed class EventDownloadProgressUpdate : EventHandlerBase
    {
        public int TotalDownloadCount { get; private set; }
        public int CurrentDownloadCount { get; private set; }
        public long TotalDownloadSizeBytes { get; private set; }
        public long CurrentDownloadSizeBytes { get; private set; }

        internal EventDownloadProgressUpdate Fill(int totalDownloadCount, int currentDownloadCount, long totalDownloadSizeBytes, long currentDownloadSizeBytes)
        {
            TotalDownloadCount = totalDownloadCount;
            CurrentDownloadCount = currentDownloadCount;
            TotalDownloadSizeBytes = totalDownloadSizeBytes;
            CurrentDownloadSizeBytes = currentDownloadSizeBytes;
            return this;
        }

        public override void Reset()
        {

        }
    }
}