using GameScripts.RunTime.Utility.Selector;

namespace GameScripts.RunTime.Magic.Command
{
    [Command("调整正面方向",11)]
    public class FaceTo: CommandBase
    {
        [Argument("执行人")]
        [SelectHandler(typeof(EnumSelectorHandler<ExecutorType>))]
        public ExecutorType excutor;
        
        [Argument("方向类型")]
        [SelectHandler(typeof(EnumSelectorHandler<FaceType>))]
        public FaceType face_to;

        [Argument("水平移动")]
        public float h_dis;
        [Argument("垂直移动")]
        public float v_dis;
        
        [Argument("随机方向")]
        public ComplexRandomPosition randomPosition;

        [Argument("旋转时间")]
        public float time;
    }
}