using GameScripts.RunTime.Utility.Selector;

namespace GameScripts.RunTime.Magic.Command
{
    [Command("调整正面方向",11)]
    public class FaceTo: CommandBase
    {
        [Argument("执行人")]
        [SelectHandler(typeof(EnumSelectorHandler<ExecutorType>))]
        public ExecutorType executor;
        
        [Argument("方向类型")]
        [SelectHandler(typeof(EnumSelectorHandler<FaceType>))]
        public FaceType face_to;

        [Argument("位置","IsShowPos")]
        public ComplexPosition pos;
        
        [Argument("水平移动","IsShowLerp_pos")]
        public float h_dis;
        [Argument("垂直移动","IsShowLerp_pos")]
        public float v_dis;
        
        [Argument("随机方向","IsShowRandomPosition")]
        public ComplexRandomPosition randomPosition;

        [Argument("旋转时间")]
        public float time;


        private bool IsShowPos()
        {
            return face_to is FaceType.Fixed_pos or FaceType.Look_at;
        }
        
        private bool IsShowLerp_pos()
        {
            return face_to == FaceType.Lerp_pos;
        }

        private bool IsShowRandomPosition()
        {
            return face_to == FaceType.Random;
        }
    }
}