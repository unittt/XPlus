using System;
using GameScripts.RunTime.Utility.Selector;

namespace GameScripts.RunTime.Magic.Command
{
    /// <summary>
    /// 人物特写
    /// </summary>
    [Serializable]
    [Command("人物特写", 84)]
    public class CameraTarget: CommandBase
    {

        [Argument("绑定人")] [SelectHandler(typeof(SelectorHandler_Enum<ExecutorType>))]
        public ExecutorType executor;

        [Argument("移动类型")] [SelectHandler(typeof(SelectorHandler_Enum<CloseUpMoveType>))]
        public CloseUpMoveType move_type;

        [Argument("相机位置","IsShowCameraPos")] public ComplexPosition camera_pos;

        [Argument("人物位置","IsShowActorPos")] public ComplexPosition actor_pos;

        private bool IsShowCameraPos()
        {
            return move_type == CloseUpMoveType.Cam;
        }
        
        private bool IsShowActorPos()
        {
            return move_type == CloseUpMoveType.Actor;
        }
    }
}