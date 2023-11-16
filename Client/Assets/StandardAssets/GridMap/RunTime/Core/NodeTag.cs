namespace GridMap
{
    /// <summary>
    /// nodeTag标签
    /// </summary>
    public enum NodeTag : uint
    {
        /// <summary>
        /// 阻塞
        /// </summary>
        Obstacle = 0,
        /// <summary>
        /// 可行走
        /// </summary>
        WALK = 1,
        /// <summary>
        /// 透明
        /// </summary>
        Transparent = 2,
    }
}
