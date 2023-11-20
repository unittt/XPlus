namespace GameScript.RunTime.Config
{
    public static class GameCode
    {
        #region 公共响应码
        /// <summary>
        /// 成功
        /// </summary>
        public const uint Success = 0;

        /// <summary>
        /// 响应超时
        /// </summary>
        public const uint Timeout = 1;
        #endregion

        #region 登录响应码

        /// <summary>
        /// 账号或密码错误
        /// </summary>
        public const uint InvalidCredentials = 1001;

        /// <summary>
        /// 账号密码不符合规范
        /// </summary>
        public const uint InvalidAccountFormat = 1002;

        /// <summary>
        /// 账号已存在
        /// </summary>
        public const uint AccountAlreadyExists = 1003;

        #endregion
    }
}