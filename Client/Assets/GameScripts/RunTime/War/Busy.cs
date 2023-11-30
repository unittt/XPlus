using System;

namespace GameScripts.RunTime.War
{
    [Flags]
    public enum Busy
    {
        None = 0,
        /// <summary>
        /// 角色正在改变其形状或外观的状态
        /// </summary>
        changeshape = 1 << 0,
        /// <summary>
        /// 等待一定时间的状态
        /// </summary>
        WaitTime = 1 << 1,
        /// <summary>
        /// 等待接收或处理攻击的状态
        /// </summary>
        WaitHit = 1 << 2,
        /// <summary>
        /// 防御状态
        /// </summary>
        Defend = 1 << 3,
        /// <summary>
        /// 角色漂浮或半空中的状态
        /// </summary>
        Floating = 1 << 4,
        /// <summary>
        /// 被击中或受到伤害的状态。
        /// </summary>
        Hit = 1 << 5,
        /// <summary>
        /// 角色正在使用物品
        /// </summary>
        ItemUse = 1 << 6,
        /// <summary>
        /// 角色移动到特定位置
        /// </summary>
        RunTo = 1 << 7,
        /// <summary>
        /// 逃离
        /// </summary>
        Escape = 1 << 8,
        /// <summary>
        /// 角色飞出或被击退
        /// </summary>
        FlyOut = 1 << 9,
        /// <summary>
        /// 隐身或消失
        /// </summary>
        FadeDel = 1 << 10,
        /// <summary>
        /// 躲避
        /// </summary>
        Dodge = 1 << 11,
        /// <summary>
        /// 创建角色
        /// </summary>
        Create = 1 << 12,
        /// <summary>
        /// 可能代表快速移动或传送，在许多游戏中通常称为“闪烁”。
        /// </summary>
        Blink = 1 << 13,
        /// <summary>
        /// 重生
        /// </summary>
        PassiveReborn = 1 << 14,
    }
}