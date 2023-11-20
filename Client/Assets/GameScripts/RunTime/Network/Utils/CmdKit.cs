namespace GameScript.RunTime.Network
{
    /// <summary>
    /// 对外服的协议说明
    /// https://www.yuque.com/iohao/game/xeokui#Taqym
    /// </summary>
    public static class CmdKit
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

        public static string CmdToString(int cmdMerge)
        {
            var cmd = GetCmd(cmdMerge);
            var subCmd = GetSubCmd(cmdMerge);
            return $"[cmd:{cmd} - subCmd:{subCmd} - cmdMerge:{cmdMerge}]".ToString();
        }

        public static int CmdMerge(this LoginCmd theCmd)
        {
            const int cmd = (int)LoginCmd.Cmd;

            return GetMergeCmd(cmd, (int)theCmd);
        }

        public static int CmdMerge(this CommonCmd theCmd)
        {
            const int cmd = (int)CommonCmd.Cmd;

            return GetMergeCmd(cmd, (int)theCmd);
        }

        public static int CmdMerge(this BagCmd theCmd)
        {
            const int cmd = (int)BagCmd.Cmd;

            return GetMergeCmd(cmd, (int)theCmd);
        }
    }
}