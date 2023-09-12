namespace GameScript.RunTime.Network
{
    public static class CmdMgr
    {
        /// <summary>
        /// 获取cmd
        /// </summary>
        /// <param name="merge"></param>
        /// <returns></returns>
        public static int GetCmd(int merge)
        {
            return merge >> 16;
        }

        /// <summary>
        /// 获取subCmd
        /// </summary>
        /// <param name="merge"></param>
        /// <returns></returns>
        public static int GetSubCmd(int merge)
        {
            return merge & 0xFFFF;
        }

       /// <summary>
       /// 获取mergeCmd
       /// </summary>
       /// <param name="cmd"></param>
       /// <param name="subCmd"></param>
       /// <returns></returns>
        public static int GetMergeCmd(int cmd, int subCmd)
        {
            return (cmd << 16) + subCmd;
        }
    }
}