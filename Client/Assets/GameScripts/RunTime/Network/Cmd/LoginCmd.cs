namespace GameScript.RunTime.Network
{
    public enum LoginCmd
    {
        Cmd = ModuleCmd.LoginCmd,
    
        /// <summary>
        /// 登录验证
        /// </summary>
        LoginVerify = 1,
        
        UserIp = 2,
        RegisterAccount = 3,
    }
}