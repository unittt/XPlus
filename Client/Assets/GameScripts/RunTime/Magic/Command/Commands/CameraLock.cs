using System;
using GameScripts.RunTime.Utility.Selector;

namespace GameScripts.RunTime.Magic.Command
{
    [Serializable]
    [Command("像机锁定", 88)]
    public class CameraLock : CommandBase
    {
        [Argument("玩家移动像机")]
        [SelectHandler(typeof(SelectorHandler_Bool))]
        public bool player_swipe;
    }
}