namespace GameScripts.RunTime.Magic.Command
{
    [Command("像机FieldOfView", 87)]
    public class CameraFieldOfView : CommandBase
    {
        [Argument("起始值", 26)] public float start_val;

        [Argument("结束值", 26)] public float end_val;

        [Argument("渐变时间")] public float fade_time;
    }
}