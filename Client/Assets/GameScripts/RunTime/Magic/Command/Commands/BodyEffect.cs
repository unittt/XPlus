using GameScripts.RunTime.Utility.Selector;

namespace GameScripts.RunTime.Magic.Command
{
    
    [Command("绑定身体",60)]
    public class BodyEffect:CommandBase
    {
        [Argument("绑定人")]
        [SelectHandler(typeof(SelectorHandler_Enum<ExecutorType>))]
        public ExecutorType executor;
        
        [Argument("特效")]
        public ComplexEffect effect;
        
        [Argument("高度")]
        public float height;
        
        [Argument("存在时间")]
        public float alive_time;
        
        
        [Argument("绑定类型")]
        [SelectHandler(typeof(SelectorHandler_Enum<BindType>))]
        public BindType bindType;
        
        [Argument("绑定部位","IsShowBodyPart")]
        [SelectHandler(typeof(SelectorHandler_Enum<BodyPart>))]
        public BodyPart body_part;

        [Argument("挂载节点(?)","IsShowNode")]
        public int bind_idx;

        private bool IsShowBodyPart()
        {
            return bindType == BindType.Pos;
        }
        
        private bool IsShowNode()
        {
            return bindType == BindType.Node;
        }
    }
}