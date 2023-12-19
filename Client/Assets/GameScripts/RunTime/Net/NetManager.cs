using HT.Framework;

namespace GameScripts.RunTime.Net
{
    public  class NetManager : SingletonBase<NetManager>
    {
        /// <summary>
        /// 是否是Proto记录
        /// </summary>
        /// <returns></returns>
        public bool IsProtoRecord()
        {
            return false;
        }
    }
}