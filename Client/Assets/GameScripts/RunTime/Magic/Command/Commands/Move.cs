using GameScripts.RunTime.Utility.Selector;

namespace GameScripts.RunTime.Magic.Command
{
    
    [Command("移动位置",12)]
    public class Move:CommandBase
    {
        [Argument("执行人")]
        [SelectHandler(typeof(SelectorHandler_Enum<ExecutorType>))]
        public ExecutorType executor;
        
        [Argument("移动类型")]
        [SelectHandler(typeof(SelectorHandler_Enum<MoveType>))]
        public MoveType move_type;
        
        [Argument("直线","IsShowLine")]
        public ComplexMovementLine line;
        [Argument("圆弧","IsShowCircle")]
        public ComplexMovementCircle circle;
        [Argument("跳跃","IsShowJump")]
        public ComplexMovementJump jump;
        
        [Argument("移动时间")]
        public float move_time;


        private bool IsShowLine()
        {
            return move_type == MoveType.Line;
        }
        
        private bool IsShowCircle()
        {
            return move_type == MoveType.Circle;
        }
        
        private bool IsShowJump()
        {
            return move_type == MoveType.Jump;
        }
    }
}