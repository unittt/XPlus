namespace GameScripts.RunTime.Magic.Command
{
    [Command("加载UI", 92)]
    public class LoadUI: CommandBase
    {
        [Argument("路径")]
        public string path;
        
        [Argument("时间",1)]
        public string time;
    }
}