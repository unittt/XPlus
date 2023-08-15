using HT.Framework;

namespace HotUpdate
{
    /// <summary>
    /// 网络文件下载失败
    /// </summary>
    public sealed class EventWebFileDownloadFailed : EventHandlerBase
    {
        public string FileName { get; private set; }
        public string Error { get; private set; }

        internal EventWebFileDownloadFailed Fill(string fileName, string error)
        {
            FileName = fileName;
            Error = error;
            return this;
        }

        public override void Reset()
        {

        }
    }
}
