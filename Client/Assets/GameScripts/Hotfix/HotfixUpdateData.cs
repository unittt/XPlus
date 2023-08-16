using DG.Tweening;

/// <summary>
/// 热更新数据
/// </summary>
public class HotfixUpdateData
{
    /// <summary>
    /// 当前版本信息。
    /// </summary>
    public string CurrentVersion;

    /// <summary>
    /// 是否底包更新。
    /// </summary>
    public UpdateType UpdateType;

    /// <summary>
    /// 是否强制更新。
    /// </summary>
    public UpdateStyle UpdateStyle;

    /// <summary>
    /// 是否提示。
    /// </summary>
    public UpdateNotice UpdateNotice;

    /// <summary>
    /// 热更资源地址。
    /// </summary>
    public string HostServerURL;

    /// <summary>
    /// 备用热更资源地址。
    /// </summary>
    public string FallbackHostServerURL;
}

/// <summary>
/// 强制更新类型。
/// </summary>
public enum UpdateStyle
{
    None = 0,
    Force = 1, //强制(不更新无法进入游戏。)
    Optional = 2, //非强制(不更新可以进入游戏。)
}

/// <summary>
/// 是否提示更新。
/// </summary>
public enum UpdateNotice
{
    None = 0,
    Notice = 1, //提示
    NoNotice = 2, //非提示
}